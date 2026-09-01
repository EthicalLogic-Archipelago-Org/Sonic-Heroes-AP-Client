using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Rings;

public static class RingSanityHandler
{
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
                    CheckRing(ringGroupData, (UIntPtr)staticPtr, ringIndex, (Team)team, (LevelId)level, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
        
    
    
    public static void CheckRing(RingsData.RingData ringData, UIntPtr staticPtr, int ringIndex, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            // var staticRingCount =  *(byte*)(*(int*)(staticPtr + 0x2C) + 0x2);
            // LoggingHandler.LogMessage($"StaticRingCount: {staticRingCount}", TaskName, LogLevel.SuperDebug);
            LoggingHandler.LogMessage($"Got {ringData.LevelId} {team} Act {act} {ringData.Region} {ringData.LocName} Ring # {ringIndex + 1}", taskName, LogLevel.Debug);

            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.RingSanityGroup] is not SanityEnableStatus.Disabled)
            {
                CheckRingGroup(ringData, ringIndex, team, level, act, taskName);
            }
            
            if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.RingSanityFull] is not SanityEnableStatus.Disabled)
            {
                CheckRingFull(ringData, ringIndex, team, act, taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void CheckRingGroup(RingsData.RingData ringData, int ringIndex, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            RingsData.RingData ring = ringData;

            if (ring.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                ring = RingsData.AllRings.First(x => x.Group == ring.Group * -1);
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityGroup, taskName, bothActs: true)!;


            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.RingSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + RingsData.AllRings.IndexOf(ring) - ring.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.RingSanityGroupActAStartIdOffset : SonicHeroesDefinitions.RingSanityGroupActBStartIdOffset;
                var idToSend = startOffset + RingsData.AllRings.IndexOf(ring) - ring.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static unsafe void CheckRingFull(RingsData.RingData ringData, int ringIndex, Team team, Act act, string taskName)
    {
        try
        {
            RingsData.RingData ring = ringData;

            if (ring.IdOffsetFull == StageObjData.IdOffsetInvalid)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Ring Full matched on invalid Ring", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.RingSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.RingSanityFullNoActStartIdOffset;

                if (ring.IdOffsetFull < 0)
                {
                    startOffset += ring.IdOffsetFull * -1;
                }
                else
                {
                    startOffset += ring.IdOffsetFull;
                }
                var idToSend = startOffset + ringIndex;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.RingSanityFullActAStartIdOffset : SonicHeroesDefinitions.RingSanityFullActBStartIdOffset;
                
                if (ring.IdOffsetFull < 0)
                {
                    startOffset += ring.IdOffsetFull * -1;
                }
                else
                {
                    startOffset += ring.IdOffsetFull;
                }
                var idToSend = startOffset + ringIndex;
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