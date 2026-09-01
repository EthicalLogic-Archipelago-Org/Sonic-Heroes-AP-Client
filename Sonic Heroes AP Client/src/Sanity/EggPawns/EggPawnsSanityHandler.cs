using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sanity.Enemy;
using Sonic_Heroes_AP_Client.Sanity.ObjSanity;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.EggPawns;

public static class EggPawnsSanityHandler
{
    public static unsafe void HandleEggPawnKilledStaticPtr(UIntPtr staticPtr, string taskName)
    {
        try
        {
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 enemyPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var pawnsInLevel = EggPawnsData.AllEggPawns.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;
            var numMatched = 0;

            foreach (var enemyData in pawnsInLevel)
            {
                var distance = Vector3.Distance(enemyPos, enemyData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    numMatched++;
                    CheckEggPawn(enemyData, (Team)team, (LevelId)level, (Act)act, taskName);
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
    
    private static void CheckEggPawn(EggPawnsData.EggPawnData eggPawn, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {team} {level} Act {act} {eggPawn.LocName} At {eggPawn.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.Debug);
            
            EnemyData.BaseEnemyData baseEnemy = EnemyData.AllEnemies.First(x => x.Team == eggPawn.Team && x.LevelId == eggPawn.LevelId && x.StageObjType == StageObjTypes.EggPawn && x.SpawnCoords == eggPawn.SpawnCoords);
            ObjSanityHandler.HandleEnemyKilledObjSanity(baseEnemy, act, taskName);
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.EggPawnSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckEggPawnGroup(eggPawn, team, level, act, taskName);
            }
        
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.EggPawnSanityFull] is not SanityEnableStatus.Disabled)
            {
                CheckEggPawnFull(eggPawn, team, level, act, taskName);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
    
    public static void CheckEggPawnGroup(EggPawnsData.EggPawnData eggPawnData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            EggPawnsData.EggPawnData eggPawn = eggPawnData;

            if (eggPawn.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                eggPawn = EggPawnsData.AllEggPawns.First(x => x.Group == eggPawn.Group * -1);
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggPawnSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggPawnSanityGroup, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.EggPawnSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + EggPawnsData.AllEggPawns.IndexOf(eggPawn) - eggPawn.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.EggPawnSanityGroupActAStartIdOffset : SonicHeroesDefinitions.EggPawnSanityGroupActBStartIdOffset;
                var idToSend = startOffset + EggPawnsData.AllEggPawns.IndexOf(eggPawn) - eggPawn.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void CheckEggPawnFull(EggPawnsData.EggPawnData eggPawnData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            EggPawnsData.EggPawnData eggPawn = eggPawnData;
            if (eggPawn.IdOffsetFull < 0)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Egg Flapper Full matched on invalid Egg Flapper", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggPawnSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.EggPawnSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.EggPawnSanityFullNoActStartIdOffset;
                var idToSend = startOffset + EggPawnsData.AllEggPawns.IndexOf(eggPawn) - eggPawn.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.EggPawnSanityFullActAStartIdOffset : SonicHeroesDefinitions.EggPawnSanityFullActBStartIdOffset;
                var idToSend = startOffset + EggPawnsData.AllEggPawns.IndexOf(eggPawn) - eggPawn.IdOffsetFull;
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