using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBalloons;

public static class ItemBalloonsSanityHandler
{
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
                    CheckItemBalloon(itemBalloonBoxData, objType, (Team)team, (LevelId)level, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    private static void CheckItemBalloon(ItemBalloonsData.ItemBalloonData itemBalloonData, StageObjTypes objType, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {team} {level} Act {act} {itemBalloonData.Reward} {objType} ({itemBalloonData.LocName}) At {itemBalloonData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.Debug);
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.ItemBalloonSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckItemBalloonGroup(itemBalloonData, team, level, act, taskName);
            }
        
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.ItemBalloonSanityFull] is not SanityEnableStatus.Disabled)
            {
                CheckItemBalloonFull(itemBalloonData, team, level, act, taskName);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }


    public static void CheckItemBalloonGroup(ItemBalloonsData.ItemBalloonData itemBalloonData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            ItemBalloonsData.ItemBalloonData itemBalloon = itemBalloonData;

            if (itemBalloon.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                itemBalloon = ItemBalloonsData.AllItemBalloons.First(x => x.Group == itemBalloon.Group * -1);
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanityGroup, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.ItemBalloonSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloon) - itemBalloon.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.ItemBalloonSanityGroupActAStartIdOffset : SonicHeroesDefinitions.ItemBalloonSanityGroupActBStartIdOffset;
                var idToSend = startOffset + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloon) - itemBalloon.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void CheckItemBalloonFull(ItemBalloonsData.ItemBalloonData itemBalloonData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            ItemBalloonsData.ItemBalloonData itemBalloon = itemBalloonData;
            if (itemBalloon.IdOffsetFull < 0)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Item Balloon Full matched on invalid Item Balloon", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ItemBalloonSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.ItemBalloonSanityFullNoActStartIdOffset;
                var idToSend = startOffset + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloon) - itemBalloon.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.ItemBalloonSanityFullActAStartIdOffset : SonicHeroesDefinitions.ItemBalloonSanityFullActBStartIdOffset;
                var idToSend = startOffset + ItemBalloonsData.AllItemBalloons.IndexOf(itemBalloon) - itemBalloon.IdOffsetFull;
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