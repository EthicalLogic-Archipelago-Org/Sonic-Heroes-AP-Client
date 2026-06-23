using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Tasks;

public static class CheckBobsledOnEnterLevelTask
{

    public static void DelayedCheckBobsledOnEnterLevelTask(CancellationToken token)
    {
        const string taskName = "DelayedCheckBobsledOnEnterLevelTask";
        LoggingHandler.LogMessage($"DelayedCheckBobsledOnEnterLevelTask Started", taskName, LogLevel.SuperDebug);
        Thread.Sleep(100);
        StageObjHandler.CheckBobsledOnEnterLevel(taskName);
        LoggingHandler.LogMessage($"DelayedCheckBobsledOnEnterLevelTask Finished", taskName, LogLevel.SuperDebug);
    }
}