using System.Collections.Concurrent;
using System.Globalization;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using Reloaded.Imgui.Hook.Misc;
using Reloaded.Memory.Utilities;
using Reloaded.Mod.Interfaces;
using Sonic_Heroes_AP_Client.Configuration;
using static Sonic_Heroes_AP_Client.SoundHandler;

namespace Sonic_Heroes_AP_Client;

public class ArchipelagoHandler
{
    private const string GAME_NAME = "Sonic Heroes";
    public SlotData SlotData;
    private ArchipelagoSession _session;
    private LoginSuccessful _loginSuccessful;
    
    public static DateTime LastDeathLinkPacketTime = DateTime.Now;
    private string Server { get; set; }
    private int Port { get; set; }
    private string Slot { get; set; }
    private string? Seed { get; set; }
    private string Password { get; set; }
    private double SlotInstance { get; set; }
    
    public static bool IsConnected;
    public static bool IsConnecting;

    public ArchipelagoHandler(string server, int port, string slot, string password)
    {
        Server = server;
        Port = port;
        Slot = slot;
        Password = password;
    }

    private void CreateSession()
    {
        SlotInstance = DateTime.Now.ToUnixTimeStamp();
        _session = ArchipelagoSessionFactory.CreateSession(Server, Port);
        _session.MessageLog.OnMessageReceived += OnMessageReceived;
        _session.Socket.SocketClosed += OnSocketClosed;
        _session.Socket.PacketReceived += PacketReceived;
        _session.Items.ItemReceived += ItemReceived;
    }

    private void OnSocketClosed(string reason)
    {
        Logger.Log($"Connection closed ({reason}) Attempting reconnect...");
        IsConnected = false;
    }
    
    public void InitConnect()
    {
        IsConnecting = true;
        CreateSession();
        IsConnected = Connect();
        IsConnecting = false;
    }

    private bool Connect()
    {
        LoginResult result;

        try
        {
            Seed = _session.ConnectAsync()?.Result?.SeedName;
            Logger.Log(Seed + Slot);
            if (Seed != null)
            {
                Mod.SaveDataHandler!.LoadSaveData(Seed, Slot);
                if (Mod.Configuration!.MusicShuffleOptions.MusicShuffle)
                    Mod.MusicShuffleHandler!.Shuffle(int.Parse(Seed[..9]));
            }
                
            
            result = _session.LoginAsync(
                game: GAME_NAME, 
                name: Slot,
                itemsHandlingFlags: ItemsHandlingFlags.AllItems, 
                version: new Version(1, 0, 0),
                tags: new string[] {},
                password: Password
            ).Result;
        }
        catch (Exception e)
        {
            result = new LoginFailure(e.GetBaseException().Message);
        }

        if (result.Successful)
        {
            _loginSuccessful = (LoginSuccessful)result;
            SlotData = new SlotData(_loginSuccessful.SlotData);
            Mod.InitOnConnect();
            new Thread(RunCheckLocationsFromList).Start();
            //resync here
            return true;
        }
        var failure = (LoginFailure)result;
        var errorMessage = $"Failed to Connect to {Server}:{Port} as {Slot}:";
        errorMessage = failure.Errors.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        errorMessage = failure.ErrorCodes.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        Logger.Log(errorMessage);
        Logger.Log($"Attempting reconnect...");
        return false;
    }
    
    private void ItemReceived(ReceivedItemsHelper helper)
    {
        while (helper.Any())
        {
            var itemIndex = helper.Index;
            var item = helper.DequeueItem();
            
            
            Mod.ItemHandler?.HandleItem(itemIndex, item);
            
            
            
        }
    }
    
    private void PacketReceived(ArchipelagoPacketBase packet)
    {
        switch (packet)
        {
            case BouncePacket bouncePacket:
                BouncePacketReceived(bouncePacket);
                break;
            /*
            case ReceivedItemsPacket receivedItemsPacket:
                Console.WriteLine($"Received Items Packet Here. {string.Join(" ", receivedItemsPacket.Items.Select(x => x.Item.ToString("X")))}");
                foreach (var item in receivedItemsPacket.Items.ToList()) {
                    Mod.ItemHandler.HandleItem(TempIndex, item);
                    TempIndex++;
                }
                break;
            */
        }
    }

    private void HandleDeathLink(string source, string cause)
    {
        if (!SlotData.DeathLink)
            return;
        Logger.Log($"{cause}");
        Console.WriteLine($"{cause}");
        if (source == Slot)
            return;
        GameHandler.SomeoneElseDied = true;
        Mod.GameHandler.Kill();
    }

    private void HandleRingLink(string source, string amountStr)
    {
        if (!SlotData.RingLink)
            return;
        if (!Mod.ArchipelagoHandler.SlotData.RingLinkOverlord
            && Mod.GameHandler.GetCurrentLevel() == LevelId.MetalOverlord)
            return;
        if (source == SlotInstance.ToString(CultureInfo.InvariantCulture))
            return;
        if (!int.TryParse(amountStr, out var amount))
            return;
        var ringCount = Mod.GameHandler.GetRingCount();
        var newAmount = Math.Max(Math.Min(ringCount + amount, 999), 0);
        if (Mod.GameHandler.InGame() && Mod.Configuration!.PlaySounds)
        {
            switch (amount)
            {
                case 1:
                    PlaySound((int)Mod.ModuleBase, 0x1004);
                    break;
                case > 1:
                    PlaySound((int)Mod.ModuleBase, 0x1033);
                    break;
                case < 0:
                    PlaySound((int)Mod.ModuleBase, 0x1005);
                    break;
            }
        }
        Mod.GameHandler.SetRingCount(newAmount);
    }

    private string lastDeath;
    private string lastRing;
    private void BouncePacketReceived(BouncePacket packet)
    {
        if (SlotData.DeathLink)
            ProcessBouncePacket(packet, "DeathLink", ref lastDeath, (source, data) =>
                HandleDeathLink(source, data["cause"]?.ToString() ?? "Unknown")); 

        if (SlotData.RingLink)
            ProcessBouncePacket(packet, "RingLink", ref lastRing, (source, data) =>
                HandleRingLink(source, data["amount"]?.ToString() ?? "0"));
    }

    private static void ProcessBouncePacket(BouncePacket packet, string tag, ref string lastTime, Action<string, Dictionary<string, JToken>> handler)
    {
        if (!packet.Tags.Contains(tag)) return;
        if (!packet.Data.TryGetValue("time", out var timeObj)) 
            return;
        if (lastTime == timeObj.ToString())
            return;
        lastTime = timeObj.ToString();
        if (!packet.Data.TryGetValue("source", out var sourceObj)) 
            return;
        var source = sourceObj?.ToString() ?? "Unknown";
        if (packet.Data.TryGetValue("cause", out var causeObj))
        {
            var cause = causeObj?.ToString() ?? "Unknown";
            Console.WriteLine($"Received Bounce Packet with Tag: {tag} :: {cause}");
        }
        handler(source, packet.Data);
    }

    public void SendRing(int amount)
    {
        BouncePacket packet = new BouncePacket();
        var now = DateTime.Now;
        packet.Tags = new List<string>();
        packet.Tags.Add("RingLink");
        packet.Data = new Dictionary<string, JToken>();
        packet.Data.Add("time", now.ToUnixTimeStamp());
        packet.Data.Add("source", SlotInstance);
        packet.Data.Add("amount", amount);
        _session.Socket.SendPacket(packet);
    }

    private Random _random = new();
    private readonly string[] _deathMessages =
    {
        "had a skill issue (died)",
        "couldn't even beat Gamma or Beta (died)",
        "said it wasn't their fault (died)",
        "had a run in with those Eggman's robots (died)",
        "tried to code an AP game (died)",
        "didn't want to live in the same world as Charmy (died)",
        "received too many ring checks (died)",
        "tried to spin dash (died)",
        "discovered those weren't the easy ones (died)",
        "underestimated Sonic speed (died)",
        "died - MARRIAGE???? No way!",
        "didn't have just enough to pass (died)",
        "died - ANNIHILATION COMPLETED",
        "died - WORTHLESS CONSUMER MODELS",
        "couldn't even impress Sonic (died)",
        "couldn't count on Cream (died)",
        "made fun of Big's friends (died)",
        "should have had more ninja training (died)",
        "witnessed Espio's ninja power (died)",
        "was stung by Charmy (died)",
        "tried to con team Chaotix (died)",
        "tried to steal Charmy's honey (died)",
        "didn't play to win (died)",
        "was nothing but talk (died)",
        "had a lackluster performance I'd say (died)",
        "regrets leaving it to Tails (died)",
        "should have been careful not to fall (died)",
        "forgot to pay the electric bill for the office (died)",
        "should have picked Team Rose (died)"
    };
    public void SendDeath()
    {
        BouncePacket packet = new BouncePacket();
        var now = DateTime.Now;

        if (now - LastDeathLinkPacketTime < TimeSpan.FromSeconds(1))
            return;
        
        packet.Tags = new List<string> { "DeathLink" };
        packet.Data = new Dictionary<string, JToken>
        {
            { "time", now.ToUnixTimeStamp() },
            { "source", Slot },
            { "cause", $"{Slot} {_deathMessages[_random.Next(_deathMessages.Length)]}" }
        };

        if (packet.Data.TryGetValue("source", out var sourceObj))
        {
            var source = sourceObj?.ToString() ?? "Unknown";
            if (packet.Data.TryGetValue("cause", out var causeObj))
            {
                var cause = causeObj?.ToString() ?? "Unknown";
                if (packet.Data.TryGetValue("time", out var timeObj))
                {
                    var time = timeObj?.ToString() ?? "Unknown";
                    Console.WriteLine(
                        $"Sending DeathLink Packet: {source} {cause} :: with time: {time}");
                }
            }
        }
        _session.Socket.SendPacket(packet);
        LastDeathLinkPacketTime = now;
    }
    
    public void Release()
    {
        _session.SetGoalAchieved();
        _session.SetClientState(ArchipelagoClientState.ClientGoal);
    }

    public void CheckLocations(Int64[] ids)
    {
        ids.ToList().ForEach(id => _locationsToCheck.Enqueue(id + 0x93930000));
    }
    
    public void CheckLocation(Int64 id)
    {
        _locationsToCheck.Enqueue(0x93930000 + id);
    }

    private ConcurrentQueue<Int64> _locationsToCheck = new();
    public void RunCheckLocationsFromList()
    {
        while (true)
        {
            if (_locationsToCheck.TryDequeue(out var locationId))
                _session.Locations.CompleteLocationChecks(locationId);
            else
            {
                Thread.Sleep(100);
            }
        }
    }

    public bool IsLocationChecked(Int64 id)
    {
        return _session.Locations.AllLocationsChecked.Contains(id + 0x93930000);
    }

    public int CountLocationsCheckedInRange(Int64 start, Int64 end)
    {
        var startId = start + 0x93930000;
        var endId = end + 0x93930000;
        return _session.Locations.AllLocationsChecked.Count(loc => loc >= startId && loc < endId);
    }

    public void UpdateTags(List<string> tags)
    {
        var packet = new ConnectUpdatePacket
        {
            Tags = tags.ToArray(),
            ItemsHandling = ItemsHandlingFlags.AllItems
        };
        _session.Socket.SendPacket(packet);
    }
    
    static void OnMessageReceived(LogMessage message)
    {
        Logger.Log(message.ToString() ?? string.Empty);
    }

    public void Save()
    {
        Mod.SaveDataHandler?.SaveGame(Seed, Slot);
    }

    public void OnModConfigChange(IUpdatableConfigurable x)
    {
        try
        {
            if (Seed == null)
                return;
            //Console.WriteLine($"Mod Config Changed. Seed is: {Seed}");
            Mod.MusicShuffleHandler.Shuffle(int.Parse(Seed[..9]));
            
            //Console.WriteLine($"Mod Config Changed. Deathlink is now: {Mod.Configuration.TagOptions.DeathLink}");
            SlotData.CheckTags();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
}