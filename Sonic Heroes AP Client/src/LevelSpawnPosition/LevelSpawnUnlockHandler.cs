
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;

namespace Sonic_Heroes_AP_Client.LevelSpawnPosition;

public static class LevelSpawnUnlockHandler
{
    public static bool ShouldCheckForInput = false;
    private static byte LastActSelected = 0x0;
    public static int SpawnPosIndex = 0;
    
    public struct TeamSpawnData
    {
        public float XSpawnPos;
        public float YSpawnPos;
        public float ZSpawnPos;
        public ushort Pitch;
        public ushort PaddingShort;
        public int PaddingInt;
        public SpawnMode Mode;
        public byte PaddingByte;
        public byte PaddingByte2;
        public byte PaddingByte3;
        public ushort RunningTime;
        public byte PaddingByte4;
        public byte PaddingByte5;
    }
    
    public static bool ShouldIncludeSecret(Team team, LevelId level)
    {
        return false;
    }
    
    public static unsafe void HandleInput(bool up)
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
                return;
        
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
        
            var actPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(actPtr + 0x2BC);
        
            var team = (Team)storyIndex;
        
            //Console.WriteLine($"HandleInput Here: Team {team} Level {level}");
        
            var entries = GetUnlockedSpawnData(team, level);
            var allentries = GetAllSpawnDataForLevel(team, level);

            if (!entries.Any() || entries.Count < 2)
            {
                SpawnPosIndex = 0;
                return;
            }

            if (!up)
            {
                var unlockedindex = entries.IndexOf(allentries[SpawnPosIndex]);
            
                if  (unlockedindex <= 0)
                    unlockedindex = entries.Count;
            
                unlockedindex--;
                //Console.WriteLine($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}");
                SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
            }

            else
            {
                int unlockedindex = entries.IndexOf(allentries[SpawnPosIndex]);
            
                if  (unlockedindex >= entries.Count - 1)
                    unlockedindex = -1;
            
                unlockedindex++;
                //Console.WriteLine($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}");
                SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void UnlockSpawnData(Team team, LevelId level, int index, bool secret = false)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.ContainsKey(team))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data.");
                return;
            }
            if (!LevelSpawnData.AllSpawnData[team].ContainsKey(level))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
                return;
            }
        
            Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] = true;
        
            //var entry = LevelSpawnData.AllSpawnData[team][level][index];
            //Console.WriteLine($"Unlocked spawn data for Team {team} and Level {level}. Pos is {entry.Pos}, Index in List is {AllSpawnData[team][level].IndexOf(entry)}, Index is {index}");
            Mod.ArchipelagoHandler!.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void BonusStageUnlockCallback(Team team, LevelId level, int keynum = 0, bool goal = false)
    {
        try
        {
            if (keynum == 0 && !goal)
                return;

            if (team is Team.Chaotix or Team.SuperHardMode)
                return;

            //TODO count keys here to determine if have enough
            if (keynum > 0)
            {
                Mod.SaveDataHandler!.CustomSaveData!.BonusKeysPickedUp[team][level][keynum - 1] = true;
                if (Mod.LevelSelectManager.GetIfLevelGoaled(team, level))
                {
                    //unlocking bonus stage spawn here
                    Console.WriteLine($"Unlocking Bonus Stage Spawn for {team} {level}");
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
            }

            if (goal)
            {
                //Mod.SaveDataHandler!.CustomSaveData!.LevelsGoaled[team][level] =  true;
                
                int keys = 0;

                foreach (var key in Mod.SaveDataHandler.CustomSaveData.BonusKeysPickedUp[team][level])
                {
                    if (key)
                        keys++;
                }

                if (keys > 0)
                {
                    //unlocking bonus stage spawn here
                    Console.WriteLine($"Unlocking Bonus Stage Spawn for {team} {level}");
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
            }
            Mod.ArchipelagoHandler!.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static unsafe void OnSetActSpawnPosCallback()
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777B4);
            var team = *(int*)(baseAddress + 0x220);
            var levelSelectIndex = *(int*)(baseAddress + 0x194);
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelSelectIndex];
            
            if (!GetAllSpawnDataForLevel((Team)team, level).Any())
                return;
            if (GetAllSpawnDataForLevel((Team)team, level).Last().Bonusstage || level is LevelId.MetalMadness)
            {
                LevelSpawnGameWrites.ChangeSpawnLevelForOnSetAct((Team)team, levelSelectIndex);
            }
            if ((bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode)! && (Team)team == Team.Sonic && GameStateHandler.GetCurrentAct() == Act.Act2)
                GameStateGameWrites.SetCurrentAct(Act.Act3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static unsafe void GoToGameSpawnPosCallback()
    {
        try
        {
            //Console.WriteLine($"Start of ChangeSpawnPos");
            
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
            {
                Console.WriteLine($"Level {levelIndex} is out of range.");
                return;
            }
            
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
        
            var actPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(actPtr + 0x2BC);
        
            var team = (Team)storyIndex;

            if (!GetAllSpawnDataForLevel(team, level).Any())
                return;

            if (SpawnPosIndex > GetAllSpawnDataForLevel(team, level).Count - 1)
                return;
        
            LevelSpawnEntry entry = GetAllSpawnDataForLevel(team, level)[SpawnPosIndex];
            LevelSpawnGameWrites.ChangeSpawnPos(team, level, entry);
            //Console.WriteLine($"End End of ChangeSpawnPos");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static List<LevelSpawnEntry> GetAllSpawnDataForLevel(Team team, LevelId level)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.ContainsKey(team))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data.");
                return [];
            }
            if (!LevelSpawnData.AllSpawnData[team].ContainsKey(level))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
                return [];
            }
            return LevelSpawnData.AllSpawnData[team][level];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return [];
    }

    public static List<LevelSpawnEntry> GetUnlockedSpawnData(Team team, LevelId level)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.ContainsKey(team))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data.");
                return [];
            }
            if (!LevelSpawnData.AllSpawnData[team].ContainsKey(level))
            {
                //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
                return [];
            }

            if (ShouldIncludeSecret(team, level))
            {
                return LevelSpawnData.AllSpawnData[team][level].Where((x, index) => Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] ).ToList();
            }
        
            return LevelSpawnData.AllSpawnData[team][level].Where((x, index) => !x.Secret && Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] ).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return [];
    }
    
    public static string GetLevelSelectUiText(Team team, LevelId level)
    {
        try
        {
            //TODO handling for SuperHard Mode if Act selected is 2
            if (GetAllSpawnDataForLevel(team, level).Count <= SpawnPosIndex)
                SpawnPosIndex = 0;
            
            var unlockedSpawnEntries = GetUnlockedSpawnData(team, level);
            
            if (unlockedSpawnEntries.Count <= 1) 
                return "Start of Level";
            
            
            if (SpawnPosIndex == 0)
            {
                return "Start of Level";
            }

            if (GetAllSpawnDataForLevel(team, level)[SpawnPosIndex].Bonusstage)
                return "Bonus Stage";
                    
            var result = $"Checkpoint: {SpawnPosIndex}";

            if (GetAllSpawnDataForLevel(team, level)[SpawnPosIndex].Secret)
            {
                result += $" SECRET!";
                if (!Mod.IsDebug)
                    return "Start of Level";
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "Start of Level";
    }
    
}