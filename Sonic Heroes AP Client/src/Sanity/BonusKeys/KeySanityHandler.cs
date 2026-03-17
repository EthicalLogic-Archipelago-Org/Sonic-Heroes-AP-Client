
using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Sanity.BonusKeys;

public static class KeySanityHandler
{
    public static unsafe void HandleKeySanity(int edx, string taskName)
    {
        try
        {
            //var apHandler = !;
            
            var level = GameStateHandler.GetCurrentLevel(taskName);
            var team = GameStateHandler.GetCurrentStory(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);

            //var posPtr = *(int*)(Mod.ModuleBase + 0x5CE820);
            //Vector3 leaderPos = new Vector3(*(float*)(posPtr + 0xE8), *(float*)(posPtr + 0xEC), *(float*)(posPtr + 0xF0));

            var keyPtr = *(int*)(edx + 0x2C);
            Vector3 keyPos = new Vector3(*(float*)(keyPtr + 0x0), *(float*)(keyPtr + 0x4), *(float*)(keyPtr + 0x8));

            float minDistance = 999999f;
            
            var keylist = BonusKeyData.AllKeyPositions.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            if (!keylist.Any())
            {
                LoggingHandler.LogMessage($"NO KEYS FOUND FOR TEAM LEVEL ACT: {team} {level} {act} :::: coords are: {keyPos}", taskName, LogLevel.Error);
            }

            for (int i = 0; i < keylist.Count(); i++)
            {
                if (Vector3.Distance(keyPos, keylist[i].Pos) > 100.0f)
                {
                    if (Vector3.Distance(keyPos, keylist[i].Pos) < minDistance)
                    {
                        minDistance = Vector3.Distance(keyPos, keylist[i].Pos);
                    }
                    
                    //LoggingHandler.LogMessage($"Entry not matching. CurrentKeys[i].Pos is: {keylist[i].Pos} and Distance is: {Vector3.Distance(keyPos, keylist[i].Pos)}", taskName, LogLevel.Error);
                    
                    if (i == keylist.Count() - 1)
                    {
                        LoggingHandler.LogMessage($"NO MATCH FOUND FOR KEY at: {team} {level} {act} with coords: {keyPos}. Smallest Distance is {minDistance}", taskName, LogLevel.Error);
                    }

                    continue;

                }
                
                LoggingHandler.LogMessage($"Got Team {team} {level} {act} Bonus Key #{i + 1}", taskName, LogLevel.APAction);

                if (!(team is Team.Rose && level is LevelId.CasinoPark && i == 3))
                    Mod.SaveDataHandler!.CustomSaveData!.BonusKeysPickedUp[(Team)team!][(LevelId)level!][i] = true;

                var keysPickedUp = Mod.SaveDataHandler.CustomSaveData.BonusKeysPickedUp[(Team)team][(LevelId)level].Count(key => key);

                if (keysPickedUp >= Mod.LevelSelectManager.BonusKeysNeededForBonusStage)
                {
                    if (Mod.LevelSelectManager.GetIfLevelGoaled((Team)team, (LevelId)level))
                    {
                        if (SonicHeroesDefinitions.LevelToBonusStage.ContainsKey((LevelId)level))
                        {
                            LevelSpawnUnlockHandler.BonusStageUnlockCallback((Team)team, (LevelId)level, taskName, keynum: i + 1);
                        }
                    }
                }
                
                if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity, taskName)! && !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity, taskName, true)!)
                    return;

                if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity, taskName, true)!)
                {
                    Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.BonusKeyNoActStartId + BonusKeyData.AllKeyPositions.IndexOf(keylist[i]));
                }

                else
                {
                    if (act == Act.Act1)
                    {
                        Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.BonusKeyAct1StartId + BonusKeyData.AllKeyPositions.IndexOf(keylist[i]));
                    }
                    else
                    {
                        Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.BonusKeyAct2StartId + BonusKeyData.AllKeyPositions.IndexOf(keylist[i]));
                    }
                }
                break;
            }
            
            if (Mod.ArchipelagoHandler.SlotData != null)
                Mod.ArchipelagoHandler!.Save(taskName);
            LoggingHandler.LogMessage($"Key Position is: {keyPos.X}, {keyPos.Y}, {keyPos.Z}", taskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
}