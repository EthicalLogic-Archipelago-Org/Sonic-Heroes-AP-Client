using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.SingleSpring;

public static class SingleSpringHandler
{

    public static void HandleSonicSingleSpringAfterBackup(LevelId level, Act act, string taskName)
    {
        
    }
    
    
    public static void HandleDarkSingleSpringAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            switch (level)
            {
                case LevelId.SeasideHill:
                    
                    //Lower str of single springs at lower cliff route if dont have singlespring
                    List<Vector3> singleSpringsToLowerStr =
                    [
                        new Vector3(-2052.9680f, 430.9998f, -6402.4250f),
                        new Vector3(-2052.212f, 431.9998f, -6424.6100f),
                        new Vector3(-2051.968f, 431.9998f, -6444.4250f),
                        new Vector3(-1910.2780f, 457.3152f, -6400.6600f),
                        new Vector3(-1911.2780f, 458.3152f, -6372.6600f),
                    ];

                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.SingleSpring, taskName))
                    {
                        SingleSpringSpawnData singleSpring = spawnData as SingleSpringSpawnData;
                        foreach (var _ in singleSpringsToLowerStr.Where(pos => singleSpring.IsAtPosition(pos, taskName)))
                        {
                            if (!StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.SingleSpring, Team.Dark, SonicHeroesDefinitions.LevelIdToRegion[level], taskName))
                            {
                                singleSpring.SetPower(-3.5f, taskName);
                            }
                            else
                            {
                                singleSpring.ResetPower(taskName);
                            }
                            singleSpring.SpawnOrDespawnObj(true, taskName);
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