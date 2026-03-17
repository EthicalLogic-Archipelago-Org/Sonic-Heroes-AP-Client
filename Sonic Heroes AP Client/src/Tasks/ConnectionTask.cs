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
                //LoggingHandler.LogMessage(message: $"", source: $"", level: LogLevel.Debug, task: taskName);
                
                Mod.ArchipelagoHandler.CreateSession(taskName);
                Mod.ArchipelagoHandler.InitConnect(taskName);
                LoggingHandler.LogMessage($"Connection Task Finished in : {taskName}", taskName, LogLevel.SuperDebug);
            }
            Thread.Sleep(2500);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}