using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBalloons;

public static class ItemBalloonsSanityHandler
{
    
    private static void CheckItemBalloon(ItemBalloonsData.ItemBalloonData itemBalloonData, StageObjTypes objType, Team team, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {itemBalloonData.Team} {itemBalloonData.LevelId} Act {act} {itemBalloonData.Reward} {objType} ({itemBalloonData.LocName}) At {itemBalloonData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

            if (team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.ItemBalloonAct1StartId + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloonData)
                    : SonicHeroesDefinitions.ItemBalloonAct2StartId + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloonData);
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.ItemBalloonNoActStartId + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloonData);
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
    
    
    private static unsafe void HandleItemBalloonStaticPtr(UIntPtr staticPtr, StageObjTypes objType, string taskName)
    {
        try
        {
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 itemBalloonBoxPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var itemBalloonBoxesInLevel = ItemBalloonsData.AllItemBalloons.Where(x => x.Team == team && x.LevelId == level).ToList();
                
            var minDistance = 999999f;

            foreach (var itemBalloonBoxData in itemBalloonBoxesInLevel)
            {
                var distance = Vector3.Distance(itemBalloonBoxPos, itemBalloonBoxData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    CheckItemBalloon(itemBalloonBoxData, objType, (Team)team, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static unsafe void HandleItemBalloonSanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", taskName, LogLevel.SuperDebug);
            HandleItemBalloonStaticPtr((UIntPtr)staticPtr, StageObjTypes.ItemBalloon,  taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
}