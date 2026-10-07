using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.EggFlapper;

public static class EggFlapperHandler
{
    public static void HandleDarkEggFlappersAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            HandleEggFlappersSpecialCases(Team.Dark, level, act, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    public static void HandleEggFlappersSpecialCases(Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            bool hasRuin = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.MovingRuinPlatform, team, SonicHeroesDefinitions.LevelIdToRegion[level], taskName);
            bool hasRuinTrigger = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.TriggerRuins, team, SonicHeroesDefinitions.LevelIdToRegion[level], taskName);
            
            
            switch (team)
            {
                case Team.Dark when level is LevelId.SeasideHill:
                    // Handle Egg Flappers at Staircase Before Corner Cave Middle After Checkpoint 1 Red Flappers when dont have Ruins
                    
                    List<Vector3> eggFlappersThatNeedRuinsNoTriggerList =
                    [
                        //Left Red Flapper
                        new Vector3(-4593.3620f, 130.9900f, -11706.1200f),
                        
                        //Center Green Flapper Bazooka
                        new Vector3(-4553.3620f, 140.9900f, -11736.1200f),
                        
                        //Right Red Flapper
                        new Vector3(-4513.3620f, 130.9900f, -11706.1200f),
                    ];
                    
                    foreach (StageObjSpawnData spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.EggFlapper, taskName))
                    {
                        foreach (Vector3 spawnPos in eggFlappersThatNeedRuinsNoTriggerList)
                        {
                            if (spawnData.IsAtPosition(spawnPos, taskName))
                            {
                                spawnData.SpawnOrDespawnObj(hasRuin, taskName);
                            }
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