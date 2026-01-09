
using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;

namespace Sonic_Heroes_AP_Client.Sanity.BonusKeys;

public static class KeySanityHandler
{
    public static unsafe void HandleKeySanity(int edx)
    {
        //Console.WriteLine("Running GetBonusKey Here!");
        //Console.WriteLine($"EBP is: {edx:X}");
        try
        {
            //var apHandler = !;
            
            var level = GameStateHandler.GetCurrentLevel();
            var team = GameStateHandler.GetCurrentStory();
            var act = GameStateHandler.GetCurrentAct();

            //var posPtr = *(int*)(Mod.ModuleBase + 0x5CE820);
            //Vector3 leaderPos = new Vector3(*(float*)(posPtr + 0xE8), *(float*)(posPtr + 0xEC), *(float*)(posPtr + 0xF0));

            var keyPtr = *(int*)(edx + 0x2C);
            Vector3 keyPos = new Vector3(*(float*)(keyPtr + 0x0), *(float*)(keyPtr + 0x4), *(float*)(keyPtr + 0x8));

            float minDistance = 999999f;
            
            var keylist = BonusKeyData.AllKeyPositions.Where(x => x.Team == team && x.LevelId == level).ToList();
            
            if (!keylist.Any())
            {
                Console.WriteLine($"NO KEYS FOUND FOR TEAM LEVEL ACT: {team} {level} {act} :::: coords are: {keyPos}");
            }

            for (int i = 0; i < keylist.Count(); i++)
            {
                if (Vector3.Distance(keyPos, keylist[i].Pos) > 100.0f)
                {
                    if (Vector3.Distance(keyPos, keylist[i].Pos) < minDistance)
                    {
                        minDistance = Vector3.Distance(keyPos, keylist[i].Pos);
                    }

                    //Console.WriteLine($"Entry not matching. CurrentKeys[i].Pos is: {keylist[i].Pos} and Distance is: {Vector3.Distance(keyPos, keylist[i].Pos)}");
                    if (i == keylist.Count() - 1)
                    {
                        Console.WriteLine(
                            $"NO MATCH FOUND FOR KEY at: {team} {level} {act} with coords: {keyPos}. Smallest Distance is {minDistance}");
                    }

                    continue;

                }

                //Console.WriteLine($"Match Found! Index is: {i}");
                Console.WriteLine($"Got Team {team} {level} {act} Bonus Key #{i + 1}");
                //Logger.Log($"");

                Mod.SaveDataHandler!.CustomSaveData!.BonusKeysPickedUp[(Team)team!][(LevelId)level!][i] = true;

                var keysPickedUp = Mod.SaveDataHandler.CustomSaveData.BonusKeysPickedUp[(Team)team][(LevelId)level].Count(key => key);

                if (keysPickedUp > 0)
                {
                    if (Mod.LevelSelectManager.GetIfLevelGoaled((Team)team, (LevelId)level))
                    {
                        if (SonicHeroesDefinitions.LevelToBonusStage.ContainsKey((LevelId)level))
                        {
                            LevelSpawnUnlockHandler.BonusStageUnlockCallback((Team)team, (LevelId)level, keynum: i + 1);
                        }
                    }
                }
                
                if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity)! && !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity, true)!)
                    return;

                if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.KeySanity, true)!)
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
                Mod.ArchipelagoHandler!.Save();
            //Console.WriteLine($"Key Position is: {keyPos.X}, {keyPos.Y}, {keyPos.Z}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}