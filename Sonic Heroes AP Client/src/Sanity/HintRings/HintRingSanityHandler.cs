using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.HintRings;

public static class HintRingSanityHandler
{
    
    public static unsafe void HandleHintRingSanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 hintRingPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var hintRingsInLevel = HintRingsData.AllHintRings.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;

            foreach (var hintRingData in hintRingsInLevel)
            {
                var distance = Vector3.Distance(hintRingPos, hintRingData.SpawnCoords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }

                if (distance < StageObjData.DistanceForMatchingStageObj)
                {
                    CheckHintRing(hintRingData, (Team)team, (LevelId)level, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void CheckHintRing(HintRingsData.HintRingData hintRingData, Team team, LevelId level, Act act, string taskName)
    {
        var log = $"Got Team {team} {level} Act {act} Hint Ring At {hintRingData.Region}";
        LoggingHandler.LogMessage(log, taskName, LogLevel.Debug);
        
        if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.HintRingSanityGroup] is not SanityEnableStatus.Disabled)
        {
            CheckHintRingGroup(hintRingData, team, level, act, taskName);
        }
        
        if (Mod.LevelSelectManager.EnabledSanities[team][SanityType.HintRingSanityFull] is not SanityEnableStatus.Disabled)
        {
            CheckHintRingFull(hintRingData, team, level, act, taskName);
        }
    }


    public static void CheckHintRingGroup(HintRingsData.HintRingData hintRingData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            HintRingsData.HintRingData hintRing = hintRingData;
            if (hintRing.IdOffsetGroup == StageObjData.IdOffsetInvalid)
            {
                hintRing = HintRingsData.AllHintRings.First(x => x.Group == hintRing.Group * -1);
            }
            
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.HintRingSanityGroup, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.HintRingSanityGroup, taskName, bothActs: true)!;


            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.HintRingSanityGroupNoActStartIdOffset;
                var idToSend = startOffset + HintRingsData.AllHintRings.IndexOf(hintRing) - hintRing.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.HintRingSanityGroupActAStartIdOffset : SonicHeroesDefinitions.HintRingSanityGroupActBStartIdOffset;
                var idToSend = startOffset + HintRingsData.AllHintRings.IndexOf(hintRing) - hintRing.IdOffsetGroup;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }


    public static void CheckHintRingFull(HintRingsData.HintRingData hintRingData, Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            HintRingsData.HintRingData hintRing = hintRingData;
            if (hintRing.IdOffsetFull < 0)
            {
                //this should never happen
                LoggingHandler.LogMessage($"Hint Ring Full matched on invalid Hint Ring", taskName, LogLevel.Error);
                return;
            }
            
            bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.HintRingSanityFull, taskName, oneSet: true)!;
            bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.HintRingSanityFull, taskName, bothActs: true)!;
            
            if (oneSetEnabled)
            {
                var startOffset = SonicHeroesDefinitions.HintRingSanityFullNoActStartIdOffset;
                var idToSend = startOffset + HintRingsData.AllHintRings.IndexOf(hintRing) - hintRing.IdOffsetFull;
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
            }

            if (bothActsEnabled)
            {
                var startOffset = act is Act.Act1 ? SonicHeroesDefinitions.HintRingSanityFullActAStartIdOffset : SonicHeroesDefinitions.HintRingSanityFullActBStartIdOffset;
                var idToSend = startOffset + HintRingsData.AllHintRings.IndexOf(hintRing) - hintRing.IdOffsetFull;
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