using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sanity.Enemy;
using Sonic_Heroes_AP_Client.Sanity.ObjSanity;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.EggFlappers;

public static class EggFlappersSanityHandler
{
    public static unsafe void HandleEggFlapperKilledStaticPtr(UIntPtr staticPtr, string taskName)
    {
        try
        {
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 enemyPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var flappersInLevel = EggFlappersData.AllEggFlappers.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;
            var numMatched = 0;

            foreach (var enemyData in flappersInLevel)
            {
                var distance = Vector3.Distance(enemyPos, enemyData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    numMatched++;
                    CheckEggFlapper(enemyData, (Team)team, (LevelId)level, (Act)act, taskName);
                }
            }

            if (numMatched == 0)
            {
                LoggingHandler.LogMessage($"No Enemies Matched at 0x{staticPtr:X}", taskName, LogLevel.Debug);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
    
    private static void CheckEggFlapper(EggFlappersData.EggFlapperData eggFlapper, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {team} {level} Act {act} {eggFlapper.LocName} At {eggFlapper.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.Debug);
            
            EnemyData.BaseEnemyData baseEnemy = EnemyData.AllEnemies.First(x => x.Team == eggFlapper.Team && x.LevelId == eggFlapper.LevelId && x.StageObjType == StageObjTypes.EggFlapper && x.SpawnCoords == eggFlapper.SpawnCoords);
            ObjSanityHandler.HandleEnemyKilledObjSanity(baseEnemy, act, taskName);
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.EggFlapperSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckEggFlapperGroup(eggFlapper, team, level, act, taskName);
            }
        
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.EggFlapperSanityFull] is not SanityEnableStatus.Disabled)
            {
                CheckEggFlapperFull(eggFlapper, team, level, act, taskName);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
    public static void CheckEggFlapperGroup(EggFlappersData.EggFlapperData eggFlapperData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            EggFlappersData.EggFlapperData eggFlapper = eggFlapperData;

            if (eggFlapper.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                eggFlapper = EggFlappersData.AllEggFlappers.First(x => x.Group == eggFlapper.Group * -1);
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggFlapperSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggFlapperSanityGroup, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.EggFlapperSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + EggFlappersData.AllEggFlappers.IndexOf(eggFlapper) - eggFlapper.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.EggFlapperSanityGroupActAStartIdOffset : SonicHeroesDefinitions.EggFlapperSanityGroupActBStartIdOffset;
                var idToSend = startOffset + EggFlappersData.AllEggFlappers.IndexOf(eggFlapper) - eggFlapper.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void CheckEggFlapperFull(EggFlappersData.EggFlapperData eggFlapperData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            EggFlappersData.EggFlapperData eggFlapper = eggFlapperData;
            if (eggFlapper.IdOffsetFull < 0)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Egg Flapper Full matched on invalid Egg Flapper", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggFlapperSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggFlapperSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.EggFlapperSanityFullNoActStartIdOffset;
                var idToSend = startOffset + EggFlappersData.AllEggFlappers.IndexOf(eggFlapper) - eggFlapper.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.EggFlapperSanityFullActAStartIdOffset : SonicHeroesDefinitions.EggFlapperSanityFullActBStartIdOffset;
                var idToSend = startOffset + EggFlappersData.AllEggFlappers.IndexOf(eggFlapper) - eggFlapper.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
}