using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Enemy;

public static class EnemySanityHandler
{

    private static void CheckEnemySanity(EnemyData.EnemySanityData enemySanityData, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {enemySanityData.Team} {enemySanityData.LevelId} Act {act} {enemySanityData.LocName} At {enemySanityData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);
            
            ObjSanityHandler.HandleEnemyKilledObjSanity(enemySanityData, act, taskName);

            if (enemySanityData.Team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(enemySanityData.Team, SanityType.HintRingSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.EnemyAct1StartId + EnemyData.AllEnemies.IndexOf(enemySanityData)
                    : SonicHeroesDefinitions.EnemyAct2StartId + EnemyData.AllEnemies.IndexOf(enemySanityData);
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(enemySanityData.Team, SanityType.HintRingSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.EnemyNoActStartId + EnemyData.AllEnemies.IndexOf(enemySanityData);
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
    

    private static unsafe void HandleEnemySanityStaticPtr(UIntPtr staticPtr, string taskName)
    {
        try
        {
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 enemyPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var enemysInLevel = EnemyData.AllEnemies.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;
            var numMatched = 0;

            foreach (var enemyData in enemysInLevel)
            {
                var distance = Vector3.Distance(enemyPos, enemyData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    numMatched++;
                    CheckEnemySanity(enemyData, (Act)act, taskName);
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
    
    
    public static unsafe void HandleEnemySanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:X}", taskName, LogLevel.SuperDebug);
            HandleEnemySanityStaticPtr((UIntPtr)staticPtr, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    

}