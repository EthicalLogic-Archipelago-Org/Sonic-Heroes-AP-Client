
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net.WebSockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Packets;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.UI;


namespace Sonic_Heroes_AP_Client.Archipelago;

public class ArchipelagoHandler
{
    private const string GameName = "Sonic Heroes";
    public SlotData SlotData;
    public ArchipelagoSession Session;
    private LoginSuccessful _loginSuccessful;
    
    public ConcurrentQueue<Int64> LocationsToCheck = new();
    

    private string Server { get; set; }
    private int Port { get; set; }
    public string Slot { get; set; }
    public string Seed { get; set; }
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
    
    
    public void CreateSession(string taskName)
    {
        LoggingHandler.LogMessage($"Creating Archipelago Session ({Server}:{Port})", taskName, LogLevel.SuperDebug);
        
        SlotInstance = DateTime.Now.ToUnixTimeStamp();
        Session = ArchipelagoSessionFactory.CreateSession(Server, Port);
        Session.Socket.ErrorReceived += OnError;
        Session.MessageLog.OnMessageReceived += OnMessageReceived;
        Session.Socket.SocketClosed += OnSocketClosed;
        Session.Socket.PacketReceived += PacketReceived;
        Session.Items.ItemReceived += ItemReceived;
        
        LoggingHandler.LogMessage($"Archipelago Session Created", taskName, LogLevel.SuperDebug);
    }
    
    public void InitConnect(string taskName)
    {
        try
        {
            ConnectAsync(taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    private async void ConnectAsync(string taskName)
    {
        LoginResult result;
        try
        {
            LoggingHandler.LogMessage($"ConnectAsync Start", taskName, LogLevel.Debug);
            IsConnecting = true;
            RoomInfoPacket connectAsyncResult = await Session.ConnectAsync();
            Seed = connectAsyncResult.SeedName;

        }
        catch (Exception e)
        {
            if (e is TaskCanceledException)
            {
                IsConnecting = false;
                return;
            }
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
            IsConnecting = false;
            return;
        }

        try
        {
            LoggingHandler.LogMessage($"Seed: {Seed} Slot: {Slot}", taskName, LogLevel.APAction);
            LoginResult loginAsyncResult = await Session.LoginAsync(
                 game: GameName,
                 name: Slot,
                 itemsHandlingFlags: ItemsHandlingFlags.AllItems,
                 version: new Version(1, 0, 0),
                 tags: [],
                 password: Password);
            result = loginAsyncResult;
        }
        catch (Exception e)
        {
            //LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
            LoggingHandler.LogMessage($"LoginAsync Async Failed\n{e}", taskName, LogLevel.Error);
            result = new LoginFailure(e.GetBaseException().Message);
            HandleLoginFailure(taskName, (LoginFailure)result);
            IsConnecting = false;
            return;
        }

        try
        {
            if (result.Successful)
            {
                HandleLoginSuccess(taskName, result);
            }
            else
            {
                HandleLoginFailure(taskName, (LoginFailure)result);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        IsConnecting = false;
    }


    private async void HandleLoginFailure(string taskName, LoginFailure failure)
    {
        //var result = 
        var errorMessage = $"Failed to Connect to {Server}:{Port} as {Slot}:";
        errorMessage = failure.Errors.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        errorMessage = failure.ErrorCodes.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        LoggingHandler.LogMessage($"LoginAsync Async Failed: {errorMessage}\nAttempting reconnect...", taskName, LogLevel.Error);
        
    }

    private async void HandleLoginSuccess(string taskName, LoginResult result)
    {
        LoggingHandler.LogMessage($"LoginAsync Async Successful", taskName, LogLevel.SuperDebug);
        Mod.SaveDataHandler.LoadSaveData(Seed, Slot, taskName);
        if (Mod.Configuration != null && Mod.Configuration.MusicShuffle)
            MusicShuffleHandler.Shuffle(int.Parse(Seed[..9]), taskName);
        _loginSuccessful = (LoginSuccessful)result;
        SlotData = new SlotData(_loginSuccessful.SlotData, taskName);
        Mod.InitOnConnect(taskName);
        Mod.CheckReceivedItemsTask.Start();
        Mod.CheckedLocationsTask.Start();
        IsConnected = true;
    }
    

    private bool Connect(string taskName)
    {
        LoggingHandler.LogMessage($"Connect Start", taskName, LogLevel.SuperDebug);
        LoginResult result;
        try
        {
            //Yes I am aware that this is async (this is in a Task that is separate from Game Thread)
            RoomInfoPacket connectAsyncResult = Session.ConnectAsync().Result;
            
            Seed = connectAsyncResult.SeedName;
            
            LoggingHandler.LogMessage($"Seed: {Seed} Slot: {Slot}", taskName, LogLevel.APAction);
            result = Session.LoginAsync(
                game: GameName, 
                name: Slot,
                itemsHandlingFlags: ItemsHandlingFlags.AllItems, 
                version: new Version(1, 0, 0),
                tags: [],
                password: Password
            ).Result;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"LoginAsync Async Failed\n{e}", taskName, LogLevel.Error);
            result = new LoginFailure(e.GetBaseException().Message);
        }
        
        if (result.Successful)
        {
            LoggingHandler.LogMessage($"LoginAsync Async Successful", taskName, LogLevel.SuperDebug);
            Mod.SaveDataHandler.LoadSaveData(Seed, Slot, taskName);
            if (Mod.Configuration != null && Mod.Configuration.MusicShuffle)
                MusicShuffleHandler.Shuffle(int.Parse(Seed[..9]), taskName);
            _loginSuccessful = (LoginSuccessful)result;
            SlotData = new SlotData(_loginSuccessful.SlotData, taskName);
            Mod.InitOnConnect(taskName);
            Mod.CheckReceivedItemsTask.Start();
            Mod.CheckedLocationsTask.Start();
            //resync here
            return true;
        }
        var failure = (LoginFailure)result;
        var errorMessage = $"Failed to Connect to {Server}:{Port} as {Slot}:";
        errorMessage = failure.Errors.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        errorMessage = failure.ErrorCodes.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
        LoggingHandler.LogMessage($"LoginAsync Async Failed: {errorMessage}\nAttempting reconnect...", taskName, LogLevel.Error);
        return false;
    }
    
    
    public void Release()
    {
        Session.SetGoalAchieved();
        Session.SetClientState(ArchipelagoClientState.ClientGoal);
    }
    
    
    private void OnError(Exception exception, string msg)
    {
        const string taskName = "OnError";

        if (exception is AggregateException e)
        {
            LoggingHandler.LogMessage($"Multiple errors detected: \n{string.Join("\n", e.InnerExceptions.Select(ex => ex.Message))}", taskName, LogLevel.Error);
            return;
        }
        LoggingHandler.LogMessage($"OnError: {exception}", taskName, LogLevel.Error);
    }
    
    private static void OnMessageReceived(LogMessage message)
    {
        const string taskName = "OnMessageReceived";
        LoggingHandler.LogMessage(message.ToString() ?? string.Empty, taskName, LogLevel.APAction);
        //LoggerWindow.Log(message.ToString() ?? string.Empty);
    }
    
    private static void OnSocketClosed(string reason)
    {
        const string taskName = "OnSocketClosed";
        LoggingHandler.LogMessage($"Connection closed ({reason}) Attempting reconnect...", taskName, LogLevel.Error);
        IsConnected = false;
        IsConnecting = false;
    }
    
    private static void PacketReceived(ArchipelagoPacketBase packet)
    {
        const string taskName = "PacketReceived";
        switch (packet)
        {
            case BouncePacket bouncePacket:
                BouncePacketHandler.BouncePacketReceived(bouncePacket, taskName);
                break;
            /*
            case ReceivedItemsPacket receivedItemsPacket:
                LoggingHandler.LogMessage($"Received Items Packet Here. {string.Join(" ", receivedItemsPacket.Items.Select(x => x.Item.ToString("X")))}", taskName, LogLevel.SuperDebug);
                foreach (var item in receivedItemsPacket.Items.ToList()) {
                    Mod.ItemHandler.HandleItem(TempIndex, item);
                    TempIndex++;
                }
                break;
            */
        }
    }


    private static void ItemReceived(ReceivedItemsHelper helper)
    {
        const string taskName = "ItemReceived";
        while (helper.Any())
        {
            var itemIndex = helper.Index;
            var item = helper.DequeueItem();
            
           ItemHandler.QueueItem(itemIndex, item);
        }
    }
    
    
    public static void CheckTags(string taskName)
    {
        LoggingHandler.LogMessage($"Check Tags Start", taskName, LogLevel.SuperDebug);
        List<string> tags = [];
        var deathLink = DeathLinkHandler.IsDeathLinkEnabled(taskName);
        if (deathLink)
            tags.Add("DeathLink");
        var ringLink = RingLinkHandler.IsRingLinkEnabled(taskName);
        if (ringLink)
            tags.Add("RingLink");
        Mod.ArchipelagoHandler.UpdateTags(tags);
        LoggingHandler.LogMessage($"Check Tags Done", taskName, LogLevel.SuperDebug);
    }
    
    
    public void UpdateTags(List<string> tags)
    {
        var packet = new ConnectUpdatePacket
        {
            Tags = tags.ToArray(),
            ItemsHandling = ItemsHandlingFlags.AllItems
        };
        Session.Socket.SendPacket(packet);
    }
    
    
    public void CheckLocations(Int64[] ids)
    {
        ids.ToList().ForEach(id => LocationsToCheck.Enqueue(id + SonicHeroesDefinitions.AllIdsStartOffset));
    }
    
    
    public void CheckLocation(Int64 id)
    {
        LoggingHandler.LogMessage($"Sending Location Id: {SonicHeroesDefinitions.AllIdsStartOffset + id}", "INGORE ME", LogLevel.Debug,3);
        LocationsToCheck.Enqueue(SonicHeroesDefinitions.AllIdsStartOffset + id);
    }
    
    
    public bool IsLocationChecked(Int64 id)
    {
        return Session.Locations.AllLocationsChecked.Contains(id + SonicHeroesDefinitions.AllIdsStartOffset);
    }
    
    
    public int CountLocationsCheckedInRange(Int64 start, Int64 end)
    {
        var startId = start + SonicHeroesDefinitions.AllIdsStartOffset;
        var endId = end + SonicHeroesDefinitions.AllIdsStartOffset;
        return Session.Locations.AllLocationsChecked.Count(loc => loc >= startId && loc < endId);
    }
    
    
    public void Save(string taskName)
    {
        Mod.SaveDataHandler.SaveGame(Seed, Slot, taskName);
    }
}