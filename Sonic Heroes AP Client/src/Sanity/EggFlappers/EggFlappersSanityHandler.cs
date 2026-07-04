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

    public static void CheckEggFlapper(EggFlappersData.EggFlapperData eggFlapper, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {eggFlapper.Team} {eggFlapper.LevelId} Act {act} {eggFlapper.LocName} At {eggFlapper.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

            int enemyIndex = EggFlappersData.AllEggFlappers.IndexOf(eggFlapper);
            
            var baseEnemy = EnemyData.AllEnemies.First(x => x.Team ==  eggFlapper.Team && x.LevelId == eggFlapper.LevelId && x.StageObjType == StageObjTypes.EggFlapper && x.EnemyIndex == enemyIndex);
            
            ObjSanityHandler.HandleEnemyKilledObjSanity(baseEnemy, act, taskName);

            if (eggFlapper.Team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(eggFlapper.Team, SanityType.EggFlapperSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.EggFlapperAct1StartId + enemyIndex
                    : SonicHeroesDefinitions.EggFlapperAct2StartId + enemyIndex;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(eggFlapper.Team, SanityType.EggFlapperSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.EggFlapperNoActStartId + enemyIndex;
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
                    CheckEggFlapper(enemyData, (Act)act, taskName);
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