

using System.Numerics;
using Newtonsoft.Json;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.PositionMapping;

public static class PositionMapping
{
    public static bool ShouldCheckPosition = false;

    public static Dictionary<LevelId, List<Vector3>> PosList = Enum.GetValues<LevelId>().ToDictionary(id => id, _ => new List<Vector3>());

    public static void UpdateLoop()
    {
        const string TaskName = "PositionMappingTask";
        try
        {
            LoggingHandler.LogMessage($"Starting UpdateLoop", TaskName, LogLevel.Debug);
            while (ShouldCheckPosition)
            {
                if (!GameStateHandler.InGame(TaskName))
                    continue;
                var pos = GameStateHandler.GetCurrentLeaderPos(TaskName);
                if (pos == null)
                    continue;
                PosList[(LevelId)GameStateHandler.GetCurrentLevel(TaskName)!].Add((Vector3)pos);
                Thread.Sleep(100);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
    }

    public static void EnablePostionMapping(string taskName)
    {
        ShouldCheckPosition = true;
        LoggingHandler.LogMessage($"Starting UpdateLoop Task", taskName, LogLevel.Debug);
        Task task = new Task(UpdateLoop);
        task.Start();
    }

    public static void DisablePostionMapping()
    {
        ShouldCheckPosition = false;
    }

    public static void WriteJsonFile(string taskName)
    {
        try
        {
            Vector3 prevPos = new Vector3(-9999, -9999, -9999);
            
            string result = "[\n";

            foreach (Vector3 pos in PosList[(LevelId)GameStateHandler.GetCurrentLevel(taskName)!])
            {
                if (Vector3.Distance(prevPos, pos) < 1)
                    continue;
                prevPos = pos;
                result += $"   Vector3({pos.X}, {pos.Y}, {pos.Z}),\n";
            }
            result += "]";
            
            var filePath = $"./Saves/PositionStuff.txt";
            LoggingHandler.LogMessage($"Saved Positions Here", taskName, LogLevel.Error);
            //var json = JsonConvert.SerializeObject(PosList, Formatting.Indented);
            File.WriteAllText(filePath, result);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
    
}