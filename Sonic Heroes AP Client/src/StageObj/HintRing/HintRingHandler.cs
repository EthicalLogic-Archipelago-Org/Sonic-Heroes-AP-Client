using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.HintRing;

public static class HintRingHandler
{
    
    public static void HandleDarkHintRingAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            switch (level)
            {
                case LevelId.SeasideHill:
                    //First Island Hint Ring Move to Center of Platform
                    Vector3 hintRingToMovePos = new Vector3(-4490.37f, 21.5f, -10896.22f);
                    float xPosToMoveTo = -4505.685f;
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.HintRing, taskName))
                    {
                        HintRingSpawnData hintRing = spawnData as HintRingSpawnData;
                        if (hintRing.IsAtPosition(hintRingToMovePos, taskName))
                        {
                            hintRing.SetSpawnPosition(hintRingToMovePos with { X = xPosToMoveTo }, taskName, true);
                        }
                    }
                    break;
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
}