
using System.Drawing;

namespace Sonic_Heroes_AP_Client.Logging;

public class LoggingHandler
{
    public static void OnModLoggerWriteLine(object? sender, (string text, Color color) e)
    {
        //do something here
        Console.WriteLine($"OnModLoggerWriteLine: {sender}");
    }
}