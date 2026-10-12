using System.Numerics;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.Ring;

public static class RingHandler
{
    
    public static void HandleDarkRingAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            HandleRingSpecialCases(Team.Dark, level, act, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }




    public static void HandleRingSpecialCases(Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            Region region = SonicHeroesDefinitions.LevelIdToRegion[level];
            
            bool canJump = AbilityCharacterManager.HasAbilityForTeamRegion(team, region, Ability.Jump, taskName);
            bool hasAnotherChar = AbilityCharacterManager.GetCharUnlock(team, FormationChar.Speed, taskName) || AbilityCharacterManager.GetCharUnlock(team, FormationChar.Power, taskName);
            bool canThundershoot = AbilityCharacterManager.HasAbilityForTeamRegion(team, region, Ability.Thundershoot, taskName) && AbilityCharacterManager.GetCharUnlock(team, FormationChar.Speed, taskName) || AbilityCharacterManager.GetCharUnlock(team, FormationChar.Power, taskName);
            bool canFly = AbilityCharacterManager.HasAbilityForTeamRegion(team, region, Ability.Flight, taskName);
            bool hasComboHeight = AbilityCharacterManager.HasComboHeightForTeamRegion(team, region, taskName);
            bool hasSingleSpring = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.SingleSpring, team, region, taskName);
            bool hasTripleSpring = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.TripleSpring, team, region, taskName);
            bool hasRuinsWithTrigger = StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.MovingRuinPlatform, team, region, taskName) && StageObjHandler.IsStageObjUnlockedForTeamRegion(StageObjTypes.TriggerRuins, team, region, taskName);
            
            switch (team)
            {
                case Team.Dark when level is LevelId.SeasideHill:
                    //Eggmans Robots Bottom Vertical Ring Line
                    Vector3 verticalRingsEggsmansRobotsBottom = new Vector3(-465.2492f, 373f, -6580.9210f);
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.Rings, taskName))
                    {
                        RingsSpawnData ring = spawnData as RingsSpawnData;
                        if (ring.IsAtPosition(verticalRingsEggsmansRobotsBottom, taskName))
                        {
                            var shouldSpawn = false;
                            //RingsAND(FlyAnyAND(JumpORThundershoot0)OR(JumpANDTripleSpring))
                            if ((canJump && hasTripleSpring) || (canFly && (canJump || hasAnotherChar)))
                            {
                                shouldSpawn = true;
                            }

                            if (!shouldSpawn)
                            {
                                ring.SpawnOrDespawnObj(false, taskName);
                            }
                        }
                    }
                    
                    //Second Ruin Between First and Second Island 3 Ring lines
                    List<Vector3> ringsAtSecondRuinBetweenIslands =
                    [
                        new Vector3(-4573.3620f, 100.99f, -11526.12f),
                        new Vector3(-4553.3620f, 100.99f, -11526.12f),
                        new Vector3(-4533.3620f, 100.99f, -11526.12f),
                    ];
                    
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.Rings, taskName))
                    {
                        RingsSpawnData ring = spawnData as RingsSpawnData;
                        foreach (var spawnPos in ringsAtSecondRuinBetweenIslands)
                        {
                            if (ring.IsAtPosition(spawnPos, taskName))
                            {
                                var shouldSpawn = false;
                                //RingsAND(RuinsTriggerAND(FlyAnyORJumpORTripleSpring))
                                if (hasRuinsWithTrigger && (canFly || canJump || hasTripleSpring))
                                {
                                    shouldSpawn = true;
                                }
                                
                                if (!shouldSpawn)
                                {
                                    ring.SpawnOrDespawnObj(false, taskName);
                                }
                            }
                        }
                    }
                    
                    
                    //First Cliff Before Big Ruin Beach Vertical Ring Line
                    Vector3 verticalRingsFirstCliff = new Vector3(66.0038f, 315f, -16160.74f);
                    foreach (var spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.Rings, taskName))
                    {
                        RingsSpawnData ring = spawnData as RingsSpawnData;
                        if (ring.IsAtPosition(verticalRingsFirstCliff, taskName))
                        {
                            var shouldSpawn = false;
                            //RingsANDSingleSpringAND(FlyAnyORJumpORThundershoot0ORComboHeight)
                            if (hasSingleSpring && (canFly || canJump || canThundershoot || hasComboHeight))
                            {
                                shouldSpawn = true;
                            }
                            
                            if (!shouldSpawn)
                            {
                                ring.SpawnOrDespawnObj(false, taskName);
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