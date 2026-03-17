
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Exceptions;

public class SonicHeroesException : Exception
{
    public SonicHeroesException(string message, string taskName) : base(message)
    {
        
    }
}