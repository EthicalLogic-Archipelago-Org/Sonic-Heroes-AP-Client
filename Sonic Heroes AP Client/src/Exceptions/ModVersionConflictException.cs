using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Exceptions;

public class ModVersionConflictException : SonicHeroesException
{
    public ModVersionConflictException(string message, string taskName) : base(message, taskName)
    {
        LoggingHandler.LogMessage($"ModVersionConflictException Test", taskName, LogLevel.Error, stackFrameOverride: 3);
    }
}