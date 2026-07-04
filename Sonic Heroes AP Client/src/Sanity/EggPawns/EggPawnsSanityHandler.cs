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
    public static void CheckEggPawn(EggPawnsData.EggPawnData eggPawn, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {eggPawn.Team} {eggPawn.LevelId} Act {act} {eggPawn.LocName} At {eggPawn.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

            int enemyIndex = EggPawnsData.AllEggPawns.IndexOf(eggPawn);
            
            var baseEnemy = EnemyData.AllEnemies.First(x => x.Team ==  eggPawn.Team && x.LevelId == eggPawn.LevelId && x.StageObjType == StageObjTypes.EggPawn && x.EnemyIndex == enemyIndex);
            
            ObjSanityHandler.HandleEnemyKilledObjSanity(baseEnemy, act, taskName);

            if (eggPawn.Team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(eggPawn.Team, SanityType.EggPawnSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.EggPawnAct1StartId + enemyIndex
                    : SonicHeroesDefinitions.EggPawnAct2StartId + enemyIndex;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(eggPawn.Team, SanityType.EggPawnSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.EggPawnNoActStartId + enemyIndex;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }
            
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    
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
                    CheckEggPawn(enemyData, (Act)act, taskName);
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
    
}