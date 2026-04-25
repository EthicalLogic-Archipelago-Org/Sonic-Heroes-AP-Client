using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Tasks;

public static class CheckedLocationsTask
{
    // ReSharper disable once InconsistentNaming
    public static void CheckedLocationsAPTask(CancellationToken token)
    {
        const string taskName = "CheckedLocationsTask";
        while (!token.IsCancellationRequested)
        {
            if (Mod.ArchipelagoHandler.LocationsToCheck.TryDequeue(out var locationId))
            {
                LoggingHandler.LogMessage($"Checking Location ID: 0x{locationId:x}", taskName, LogLevel.SuperDebug);
                Mod.ArchipelagoHandler.Session.Locations.CompleteLocationChecks(locationId);
            }
            Thread.Sleep(100);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}