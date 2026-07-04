using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBoxes;

public static class ItemBoxesSanityHandler
{
    private static void CheckItemBox(ItemBoxesData.ItemBoxData itemBoxData, StageObjTypes objType, Team team, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {itemBoxData.Team} {itemBoxData.LevelId} Act {act} {itemBoxData.Reward} {objType} ({itemBoxData.LocName}) At {itemBoxData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

            if (team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.ItemBoxAct1StartId + ItemBoxesData.AllItemBoxes.IndexOf(itemBoxData)
                    : SonicHeroesDefinitions.ItemBoxAct2StartId + ItemBoxesData.AllItemBoxes.IndexOf(itemBoxData);
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.ItemBoxNoActStartId + ItemBoxesData.AllItemBoxes.IndexOf(itemBoxData);
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
    
    
    private static unsafe void HandleItemBoxStaticPtr(UIntPtr staticPtr, StageObjTypes objType, string taskName)
    {
        try
        {
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 itemBoxPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var itemBoxesInLevel = ItemBoxesData.AllItemBoxes.Where(x => x.Team == team && x.LevelId == level).ToList();
                
            var minDistance = 999999f;

            foreach (var itemBalloonBoxData in itemBoxesInLevel)
            {
                var distance = Vector3.Distance(itemBoxPos, itemBalloonBoxData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    CheckItemBox(itemBalloonBoxData, objType, (Team)team, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static unsafe void HandleItemBoxSanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", taskName, LogLevel.SuperDebug);
            HandleItemBoxStaticPtr((UIntPtr)staticPtr, StageObjTypes.ItemBox,  taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
}