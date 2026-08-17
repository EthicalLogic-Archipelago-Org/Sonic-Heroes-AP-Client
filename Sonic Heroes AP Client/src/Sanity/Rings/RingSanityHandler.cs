using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Rings;

public static class RingSanityHandler
{
    public static unsafe void CheckRing(RingsData.RingData ringData, UIntPtr staticPtr, int ringIndex, Team team, Act act, string taskName)
    {
        try
        {
            // var staticRingCount =  *(byte*)(*(int*)(staticPtr + 0x2C) + 0x2);
            //LoggingHandler.LogMessage($"StaticRingCount: {staticRingCount}", TaskName, LogLevel.SuperDebug);
            LoggingHandler.LogMessage($"Got {ringData.LevelId} {team} Act {act} {ringData.Region} {ringData.LocName} Ring # {ringIndex + 1}", taskName, LogLevel.GameAction);

            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.RingSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckRingGroup(ringData, ringIndex, team, act, taskName);
            }
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.RingSanityIndividual] is not SanityEnableStatus.Disabled)
            {
                CheckRingIndividual(ringData, ringIndex, team, act, taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }



    public static unsafe void CheckRingGroup(RingsData.RingData ringData, int ringIndex, Team team, Act act, string taskName)
    {
        try
        {
            if (team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityGroup, taskName, true))
            {
                var idToSend = act is Act.Act1 ? SonicHeroesDefinitions.RingSanityGroupAct1StartId + RingsData.AllRings.IndexOf(ringData) - ringData.ID_offset : SonicHeroesDefinitions.RingSanityGroupAct2StartId + RingsData.AllRings.IndexOf(ringData) - ringData.ID_offset;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }
            
            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityGroup, taskName))
            {
                var idToSend = SonicHeroesDefinitions.RingSanityGroupNoActStartId + RingsData.AllRings.IndexOf(ringData) - ringData.ID_offset;
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
    
    public static unsafe void CheckRingIndividual(RingsData.RingData ringData, int ringIndex, Team team, Act act, string taskName)
    {
        try
        {
            if (team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityIndividual, taskName, true))
            {
                var idToSend = act is Act.Act1 ? SonicHeroesDefinitions.RingSanityIndividualAct1StartId + ringData.StartIDOffset + ringIndex : SonicHeroesDefinitions.RingSanityIndividualAct2StartId + ringData.StartIDOffset + ringIndex;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }
            
            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.HintRingSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.RingSanityIndividualNoActStartId + ringData.StartIDOffset + ringIndex;
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
    

    public static unsafe void HandleRingSanity(UIntPtr dynamicPtr, int ringIndex, string taskName)
    {
        try
        {
            var heapPtr = *(int*)(dynamicPtr + 0xD8);
            LoggingHandler.LogMessage($"Ring Pickup HeapPtr: 0x{heapPtr:x}", taskName, LogLevel.SuperDebug);
            
            var ringGroupTypeByte = *(byte*)(heapPtr + 0x29);
            var ringGroupType = (RingType)ringGroupTypeByte;
            LoggingHandler.LogMessage($"RingGroupType: {ringGroupType}", taskName, LogLevel.SuperDebug);
            
            var numRingsTotal = *(byte*)(heapPtr + 0x28);
            LoggingHandler.LogMessage($"NumRingsTotal: {numRingsTotal}", taskName, LogLevel.SuperDebug);

            if (ringGroupType is RingType.Scattered)
            {
                LoggingHandler.LogMessage($"PickUpRing End (Scattered Ring Group)", taskName, LogLevel.SuperDebug);
                return;
            }
            
            var linkedListStartPtr = *(int*)(heapPtr + 0x4C);
            //esi points to current entry
            //list is list of TObjRingSubstance
            LoggingHandler.LogMessage($"Ring LinkedListStartPtr: 0x{linkedListStartPtr:x}", taskName, LogLevel.SuperDebug);
            
            var staticPtr = *(int*)(heapPtr + 0x54);
            LoggingHandler.LogMessage($"Ring StaticPtr: 0x{staticPtr:x}", taskName, LogLevel.SuperDebug);
            
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 ringGroupPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var ringGroupsInLevel = RingsData.AllRings.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;
            
            foreach (var ringGroupData in ringGroupsInLevel)
            {
                var distance = Vector3.Distance(ringGroupPos, ringGroupData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    CheckRing(ringGroupData, (UIntPtr)staticPtr, ringIndex, (Team)team, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    
}