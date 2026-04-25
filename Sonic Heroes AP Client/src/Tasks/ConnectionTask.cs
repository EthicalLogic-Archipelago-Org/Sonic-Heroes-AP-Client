using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Tasks;

public static class ConnectionTask
{
    // ReSharper disable once InconsistentNaming
    public static void APConnectionTask()
    {
        const string taskName = "APConnectionTask";
        
        while (true)
        {
            if (!ArchipelagoHandler.IsConnecting && !ArchipelagoHandler.IsConnected)
            {
                if (Mod.Cts != null)
                    Mod.Cts.Cancel();
                
                Mod.Cts = new();
                Mod.CheckReceivedItemsTask = new Task(() => ReceivedItemsTask.CheckReceivedItemsTask(Mod.Cts.Token), Mod.Cts.Token);
                Mod.CheckedLocationsTask = new Task(() => CheckedLocationsTask.CheckedLocationsAPTask(Mod.Cts.Token), Mod.Cts.Token);
                
                //LoggingHandler.LogMessage(message: $"", source: $"", level: LogLevel.Debug, task: taskName);
                Mod.ArchipelagoHandler.CreateSession(taskName);
                Mod.ArchipelagoHandler.InitConnect(taskName);
                //LoggingHandler.LogMessage($"Connection Task Finished in : {taskName}", taskName, LogLevel.SuperDebug);
            }
            Thread.Sleep(2500);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}