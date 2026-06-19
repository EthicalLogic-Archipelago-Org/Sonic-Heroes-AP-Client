using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.HintRing;

public static class HintRingSanityHandler
{

    public static unsafe void CheckHintRing(HintRingData.HintRingsData hintRingData, Act act, string taskName)
    {
        try
        {
            var log = $"Got Team {hintRingData.Team} {hintRingData.LevelId} Act {act} Hint Ring At {hintRingData.Region}";
            LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

            if (hintRingData.Team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(hintRingData.Team, SanityType.HintRingSanity, taskName, true))
            {
                var idToSend = act is Act.Act1
                    ? SonicHeroesDefinitions.HintRingAct1StartId + HintRingData.AllHintRings.IndexOf(hintRingData)
                    : SonicHeroesDefinitions.HintRingAct2StartId + HintRingData.AllHintRings.IndexOf(hintRingData);
                LoggingHandler.LogMessage($"Sending Location ID: 0x{idToSend:X} ", taskName, LogLevel.Debug);
                Mod.ArchipelagoHandler.CheckLocation(id: idToSend);
                return;
            }

            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(hintRingData.Team, SanityType.HintRingSanity, taskName))
            {
                var idToSend = SonicHeroesDefinitions.HintRingNoActStartId + HintRingData.AllHintRings.IndexOf(hintRingData);
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
    
    
    public static unsafe void HandleHintRingSanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            Vector3 hintRingPos = new Vector3(*(float*)(staticPtr + 0x0), *(float*)(staticPtr + 0x4), *(float*)(staticPtr + 0x8));
            var hintRingsInLevel = HintRingData.AllHintRings.Where(x => x.Team == team && x.LevelId == level).ToList();
            
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
                    CheckHintRing(hintRingData, (Act)act, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
}