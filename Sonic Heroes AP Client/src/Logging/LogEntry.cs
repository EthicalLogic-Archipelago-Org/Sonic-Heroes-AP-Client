

using System.Drawing;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Logging;

public class LogEntry(string source, string message, LogLevel logLevel, string taskSource)
{
    public string Source = source;
    public string TaskSource = taskSource;
    public string Message = message;
    public LogLevel LogLevel = logLevel;
    //public DateTime TimeStamp = DateTime.Now;
    
    public static readonly Dictionary<LogLevel, Color> LogColors = new ()
    {
        { LogLevel.Error , Color.Red },
        { LogLevel.APAction , Color.Orange },
        { LogLevel.GameAction , Color.Yellow },
        { LogLevel.Info , Color.Green },
        { LogLevel.Debug , Color.Blue },
        { LogLevel.SuperDebug , Color.LightGray },
    };
    
    public override string ToString()
    {
        return $"[SonicHeroesArchipelagoClient] [Task: {this.TaskSource}] [Source: {this.Source}] - {this.LogLevel}: {this.Message}";
    }

    public void LogMessage()
    {
        Mod.Logger.WriteLine(this.ToString(), LogColors[this.LogLevel]);
    }
}