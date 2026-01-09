

using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Logging;

public class LogEntry(string source, string message, LogLevel logLevel)
{
    public string Source = source;
    public string Message = message;
    public LogLevel LogLevel = logLevel;
    public DateTime TimeStamp = DateTime.Now;
    
    //TODO fix this
    //public bool shouldPrint = true;
    
    public override string ToString()
    {
        return $"{TimeStamp} - {LogLevel}: {Source} - {Message}";
    }
}