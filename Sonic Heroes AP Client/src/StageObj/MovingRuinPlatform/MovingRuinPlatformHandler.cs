using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.MovingRuinPlatform;

public static class MovingRuinPlatformHandler
{
    
    
    public static void HandleDarkMovingRuinsAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            HandleMovingRuinsSpecialCases(Team.Dark, level, act, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }


    public static void HandleMovingRuinsSpecialCases(Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            bool hasRuin = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.MovingRuinPlatform, team, SonicHeroesDefinitions.LevelIdToRegion[level], taskName);
            bool hasRuinTrigger = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.TriggerRuins, team, SonicHeroesDefinitions.LevelIdToRegion[level], taskName);
            switch (team)
            {
                case Team.Dark when level is LevelId.SeasideHill:
                    // Handle Invis collis object at big special ruins after First Bobsled
                    // Handle Big Special Ruins at Path Before Volcano (needs trigger so unspawn it and collis obj)
                    
                    
                    //Force Spawn Ruin and Invis Collis at Staircase Before Corner Cave Top
                    Vector3 movingRuinsToForceSpawn = new Vector3(-2830.42f, 490.7261f, -6470.7530f);
                    Vector3 invisCollisToForceSpawn = new Vector3(-2830.5470f, 598.9173f, -6470.9500f);

                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.MovingRuinPlatform, taskName).Where(spawnData => spawnData.IsAtPosition(movingRuinsToForceSpawn, taskName)))
                    {
                        spawnData.SpawnOrDespawnObj(true, taskName);
                    }
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.InvisibleCollisionObject, taskName).Where(spawnData => spawnData.IsAtPosition(invisCollisToForceSpawn, taskName)))
                    {
                        spawnData.SpawnOrDespawnObj(true, taskName);
                    }
                    
                    //Handle Special Ruin (with Trigger) and Invis Collis at Big Ruin After First Bobsled
                    List<Vector3> bigRuinswithTriggerList =
                    [
                        //Big Ruin After First Bobsled
                        new Vector3(-940f, -170f, -16150f),
                        
                        //Big Special Ruin Before Volcano
                        new Vector3(899.9998f, -200f, -22950.11f),
                    ];
                    
                    
                    List<Vector3> invisCollisForBigRuinWithTriggerList =
                    [
                        //Big Ruin After First Bobsled Center
                        new Vector3(-940.2097f, 29.6240f, -16150.39f),
                        //Big Ruin After First Bobsled Left
                        new Vector3(-940.2097f, 109.6240f, -16283.39f),
                        //Big Ruin After First Bobsled Right
                        new Vector3(-940.2097f, 109.6240f, -16017.39f),
                        
                        //Big Special Ruin Before Volcano Center
                        new Vector3(898.2181f, -2.01f, -22948.10f),
                        //Big Special Ruin Before Volcano Left
                        new Vector3(767.2180f, 120.50f, -22955.10f),
                        //Big Special Ruin Before Volcano Right
                        new Vector3(1032.2180f, 120.50f, -22955.10f),
                    ];

                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.MovingRuinPlatform, taskName)
                                 .SelectMany(spawnData => bigRuinswithTriggerList, (spawnData, spawnPos) => new { spawnData, spawnPos })
                                 .Where(t => t.spawnData.IsAtPosition(t.spawnPos, taskName))
                                 .Select(t => t.spawnData))
                    {
                        spawnData.SpawnOrDespawnObj(hasRuin && hasRuinTrigger, taskName);
                    }
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.InvisibleCollisionObject, taskName)
                                 .SelectMany(spawnData => invisCollisForBigRuinWithTriggerList, (spawnData, spawnPos) => new { spawnData, spawnPos })
                                 .Where(t => t.spawnData.IsAtPosition(t.spawnPos, taskName))
                                 .Select(t => t.spawnData))
                    {
                        spawnData.SpawnOrDespawnObj(hasRuin && hasRuinTrigger, taskName);
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