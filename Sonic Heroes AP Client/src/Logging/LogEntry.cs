

using System.Drawing;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Logging;

public class LogEntry(string source, string message, LogLevel logLevel)
{
    public string Source = source;
    public string Message = message;
    public LogLevel LogLevel = logLevel;
    public DateTime TimeStamp = DateTime.Now;
    
    public static readonly Dictionary<LogLevel, Color> LogColors = new ()
    {
        { LogLevel.Error , Color.Red },
        { LogLevel.APAction , Color.Orange },
        { LogLevel.GameAction , Color.GreenYellow },
        { LogLevel.Info , Color.LightBlue },
        { LogLevel.Debug , Color.Magenta },
    };
    
    public override string ToString()
    {
        return $"{this.TimeStamp} - {this.LogLevel}: {this.Source} - {this.Message}";
    }

    public void LogMessage()
    {
        Mod.Logger.WriteLine(this.ToString(), LogColors[this.LogLevel]);
    }
}