

using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Sanity.Checkpoints;

public static class CheckpointSanityHandler
{
    
    public static unsafe void HandleCheckPointSanity(int priority, int pointer, string taskName)
    {
        try
        {
            //var prevPriority = *(int*)(Mod.ModuleBase + 0x5DD6F4);
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            
            //edge case here
            if (team is Team.Sonic or Team.Dark && level is LevelId.FrogForest && priority == 6)
                priority = 5;
            
            //another edge case
            if (team is Team.Sonic or Team.SuperHardMode && level is LevelId.RailCanyon && priority == 3)
                priority = 4;
            
            Vector3 checkPointPos = new Vector3(*(float*)(pointer + 0x0), *(float*)(pointer + 0x4), *(float*)(pointer + 0x8));
            
            var matchingcheckpoints = CheckpointData.AllCheckpoints.Where(x => x.Team == team && x.LevelId == level && x.Priority == priority).ToList();
            var checkpointsinlevel = CheckpointData.AllCheckpoints.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            var minDistance = 999999f;
            
            if (!matchingcheckpoints.Any())
            {
                var log = $"NO Checkpoints FOUND FOR TEAM LEVEL ACT Priority: {team} {level} {act} {priority} :::: coords are: {checkPointPos}";
                LoggingHandler.LogMessage(log, taskName, LogLevel.Error);
            }
            else
            {
                for (int i = 0; i < matchingcheckpoints.Count; i++)
                {
                    var distance = Vector3.Distance(checkPointPos, matchingcheckpoints[i].SpawnCoords);
                    if  (distance < minDistance)
                        minDistance = distance;

                    if (distance < 100f || matchingcheckpoints.Count() == 1)
                    {
                        var log = $"Got Team {team} {level} {act} Checkpoint #{checkpointsinlevel.IndexOf(matchingcheckpoints[i]) + 1}";
                        LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);

                        LevelSpawnUnlockHandler.UnlockSpecificSpawnData((Team)team!, (LevelId)level!, checkpointsinlevel.IndexOf(matchingcheckpoints[i]) + 1, taskName);


                        if (team is Team.SuperHardMode)
                        {
                            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName)!)
                                return;
                            
                            Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.CheckpointAct2StartId + CheckpointData.AllCheckpoints.IndexOf(matchingcheckpoints[i]));
                        }
                        else
                        {
                            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName)! && !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName, true)!)
                            {
                                LoggingHandler.LogMessage($"Checkpoint Sanity is Disabled", taskName, LogLevel.Debug);
                                return;
                            }

                            if ((bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName)! && !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName, true)!)
                                //1 set
                                Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.CheckpointNoActStartId + CheckpointData.AllCheckpoints.IndexOf(matchingcheckpoints[i]));
                            else if (act is Act.Act1)
                                Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.CheckpointAct1StartId + CheckpointData.AllCheckpoints.IndexOf(matchingcheckpoints[i]));
                            else
                                Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.CheckpointAct2StartId + CheckpointData.AllCheckpoints.IndexOf(matchingcheckpoints[i]));
                        }
                        break;
                    }
                    if (i == matchingcheckpoints.Count() - 1)
                    {
                        var log =
                            $"No Matching Checkpoint Found: {team} {level} {act} {priority} with coords: {checkPointPos}. Smallest Distance is {minDistance}";
                        LoggingHandler.LogMessage(log, taskName, LogLevel.APAction);
                    }
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
}