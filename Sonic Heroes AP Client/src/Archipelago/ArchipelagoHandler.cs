
using System.Collections.Concurrent;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Packets;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.UI;


namespace Sonic_Heroes_AP_Client.Archipelago;

public class ArchipelagoHandler
{
    private const string GameName = "Sonic Heroes";
    public SlotData SlotData;
    public ArchipelagoSession _session;
    private LoginSuccessful _loginSuccessful;
    
    private ConcurrentQueue<Int64> _locationsToCheck = new();
    

    private string Server { get; set; }
    private int Port { get; set; }
    public string Slot { get; set; }
    public string? Seed { get; set; }
    private string Password { get; set; }
    public double SlotInstance { get; set; }
    
    public static bool IsConnected;
    public static bool IsConnecting;
    
    
    public ArchipelagoHandler(string server, int port, string slot, string password)
    {
        Server = server;
        Port = port;
        Slot = slot;
        Password = password;
    }
    
    
    public void CreateSession()
    {
        SlotInstance = DateTime.Now.ToUnixTimeStamp();
        _session = ArchipelagoSessionFactory.CreateSession(Server, Port);
        _session.Socket.ErrorReceived += OnError;
        _session.MessageLog.OnMessageReceived += OnMessageReceived;
        _session.Socket.SocketClosed += OnSocketClosed;
        _session.Socket.PacketReceived += PacketReceived;
        _session.Items.ItemReceived += ItemReceived;
    }
    
    public void InitConnect()
    {
        IsConnecting = true;
        IsConnected = Connect();
        IsConnecting = false;
    }
    

    private bool Connect()
    {
        LoginResult result;
        try
        {
            Seed = _session.ConnectAsync()?.Result?.SeedName;
            LoggerWindow.Log(Seed + Slot);
            if (Seed != null)
            {
                Mod.SaveDataHandler!.LoadSaveData(Seed, Slot);
                if (Mod.Configuration!.MusicShuffleOptions.MusicShuffle)
                    MusicShuffleHandler.Shuffle(int.Parse(Seed[..9]));
            }
                
            
            result = _session.LoginAsync(
                game: GameName, 
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
        LoggerWindow.Log(errorMessage);
        LoggerWindow.Log($"Attempting reconnect...");
        return false;
    }
    
    
    public void Release()
    {
        _session.SetGoalAchieved();
        _session.SetClientState(ArchipelagoClientState.ClientGoal);
    }
    
    
    private void OnError(Exception exception, string msg)
    {
        Console.WriteLine($"OnError: {exception} {msg}");
    }
    
    private static void OnMessageReceived(LogMessage message)
    {
        LoggerWindow.Log(message.ToString() ?? string.Empty);
    }
    
    private void OnSocketClosed(string reason)
    {
        LoggerWindow.Log($"Connection closed ({reason}) Attempting reconnect...");
        IsConnected = false;
    }
    
    
    private void PacketReceived(ArchipelagoPacketBase packet)
    {
        switch (packet)
        {
            case BouncePacket bouncePacket:
                BouncePacketHandler.BouncePacketReceived(bouncePacket);
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
    
    
    public void ItemReceived(ReceivedItemsHelper helper)
    {
        while (helper.Any())
        {
            var itemIndex = helper.Index;
            var item = helper.DequeueItem();
            
           ItemHandler.HandleItem(itemIndex, item);
        }
    }
    
    
    public void CheckTags()
    {
        List<string> tags = new List<string>();
        var DeathLink = Mod.Configuration!.TagOptions.DeathLink;
        if (DeathLink)
            tags.Add("DeathLink");
        var RingLink = Mod.Configuration!.TagOptions.RingLink;
        if (RingLink)
            tags.Add("RingLink");
        Mod.ArchipelagoHandler.UpdateTags(tags);
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
    
    
    public void CheckLocations(Int64[] ids)
    {
        ids.ToList().ForEach(id => _locationsToCheck.Enqueue(id + 0x93930000));
    }
    
    
    public void CheckLocation(Int64 id)
    {
        _locationsToCheck.Enqueue(0x93930000 + id);
    }
    
    
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

    
    public void Save()
    {
        try
        {
            Mod.SaveDataHandler?.SaveGame(Seed, Slot);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}