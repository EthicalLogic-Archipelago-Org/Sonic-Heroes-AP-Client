using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Tasks;

public static class ReceivedItemsTask
{
    public static void CheckReceivedItemsTask(CancellationToken token)
    {
        const string taskName = "ReceivedItemsTask";
        LoggingHandler.LogMessage($"ReceivedItemsTask Started", taskName, LogLevel.SuperDebug);
        while (!token.IsCancellationRequested)
        {
            //LoggingHandler.LogMessage($"ReceivedItems: {ItemHandler.ReceivedItems} : {ItemHandler.ReceivedItems.Count}", taskName, LogLevel.SuperDebug);
            if (ItemHandler.ReceivedItems.TryDequeue(out var itemTuple))
            {
                ItemHandler.HandleItem(itemTuple.Item1, itemTuple.Item2, taskName);
            }
            else
            {
                Thread.Sleep(100);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}