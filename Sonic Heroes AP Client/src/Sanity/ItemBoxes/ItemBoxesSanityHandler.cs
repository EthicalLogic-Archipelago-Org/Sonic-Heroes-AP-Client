using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBoxes;

public static class ItemBoxesSanityHandler
{
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
                    CheckItemBox(itemBalloonBoxData, objType, (Team)team, (LevelId)level, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    private static void CheckItemBox(ItemBoxesData.ItemBoxData itemBoxData, StageObjTypes objType, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {team} {level} Act {act} {itemBoxData.Reward} {objType} ({itemBoxData.LocName}) At {itemBoxData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.Debug);
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.ItemBoxSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckItemBoxGroup(itemBoxData, team, level, act, taskName);
            }
        
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.ItemBoxSanityFull] is not SanityEnableStatus.Disabled)
            {
                CheckItemBoxFull(itemBoxData, team, level, act, taskName);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }


    public static void CheckItemBoxGroup(ItemBoxesData.ItemBoxData itemBoxData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            ItemBoxesData.ItemBoxData itemBox = itemBoxData;

            if (itemBox.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                itemBox = ItemBoxesData.AllItemBoxes.First(x => x.Group == itemBox.Group * -1);
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanityGroup, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.ItemBoxSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + ItemBoxesData.AllItemBoxes.IndexOf(itemBox) - itemBox.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.ItemBoxSanityGroupActAStartIdOffset : SonicHeroesDefinitions.ItemBoxSanityGroupActBStartIdOffset;
                var idToSend = startOffset + ItemBoxesData.AllItemBoxes.IndexOf(itemBox) - itemBox.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void CheckItemBoxFull(ItemBoxesData.ItemBoxData itemBoxData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            ItemBoxesData.ItemBoxData itemBox = itemBoxData;
            if (itemBox.IdOffsetFull < 0)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Item Box Full matched on invalid Item Box", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBoxSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.ItemBoxSanityFullNoActStartIdOffset;
                var idToSend = startOffset + ItemBoxesData.AllItemBoxes.IndexOf(itemBox) - itemBox.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.ItemBoxSanityFullActAStartIdOffset : SonicHeroesDefinitions.ItemBoxSanityFullActBStartIdOffset;
                var idToSend = startOffset + ItemBoxesData.AllItemBoxes.IndexOf(itemBox) - itemBox.IdOffsetFull;
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