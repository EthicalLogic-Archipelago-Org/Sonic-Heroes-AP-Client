
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.Logging;

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

    public static void InitConnect(string taskName)
    {
        try
        {
            foreach (var team in Enum.GetValues<Team>().Where(t => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(t, taskName)!))
            {
                //starting spawn pos here
                UnlockSpawnPosForAllLevelsForTeam(team, 0, taskName);
            }
            if (!Mod.IsDebug)
                return;
            foreach (var team in Enum.GetValues<Team>())
            {
                UnlockAllSpawnDataForTeam(team, taskName);
            }
            Mod.ArchipelagoHandler.Save(taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }

    public static void UnlockAllSpawnDataForTeam(Team team, string taskName)
    {
        foreach (var level in Enum.GetValues<LevelId>().Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24))
        {
            UnlockAllSpawnDataForTeamAndLevel(team, level, taskName);
        }
    }


    public static void UnlockAllSpawnDataForTeamAndLevel(Team team, LevelId level, string taskName)
    {
        try
        {
            Mod.SaveDataHandler.CustomSaveData!.SpawnDataUnlocks[team][level] = Enumerable
                .Repeat(true, Mod.SaveDataHandler.CustomSaveData!.SpawnDataUnlocks[team][level].Count).ToList();
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void UnlockSpawnPosForAllLevelsForTeam(Team team, int index, string taskName)
    {
        try
        {
            foreach (var pair in Mod.SaveDataHandler.CustomSaveData!.SpawnDataUnlocks[team].Where(pair => index < pair.Value.Count - 1 && index > 0))
            {
                pair.Value[index] = true;
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void UnlockSpawnPosForTeamAndLevel(Team team, LevelId level, int index, string taskName)
    {
        try
        {
            if (index > Mod.SaveDataHandler.CustomSaveData!.SpawnDataUnlocks[team][level].Count - 1 || index < 0)
            {
                LoggingHandler.LogMessage($"Index {index} Team {team} Level {level} is out of range in UnlockSpawnPosForTeamAndLevel", taskName, LogLevel.Error);
                return;
            }
            Mod.SaveDataHandler.CustomSaveData!.SpawnDataUnlocks[team][level][index] = true;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static unsafe void HandleInput(bool up, string taskName)
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
                return;
        
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
        
            var team = (Team)storyIndex;

            if (team is Team.Sonic && (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, taskName)! &&
                Mod.LevelSelectManager.ActSelectedInLevelSelect is Act.Act2)
                team = Team.SuperHardMode;
            
            //LoggingHandler.LogMessage($"HandleInput Here: Team {team} Level {level}", taskName, LogLevel.Debug);
        
            var entries = GetUnlockedSpawnData(team, level, taskName);
            var allentries = GetAllSpawnDataForLevel(team, level, taskName);

            
            if (!entries.Any())
            {
                SpawnPosIndex = 0;
                return;
            }

            if (entries.Count < 2)
            {
                SpawnPosIndex = allentries.IndexOf(entries.First());
                return;
            }

            if (!up)
            {
                var unlockedindex = entries.IndexOf(allentries[SpawnPosIndex]);
            
                if  (unlockedindex <= 0)
                    unlockedindex = entries.Count;
            
                unlockedindex--;
                //LoggingHandler.LogMessage($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}", taskName, LogLevel.Debug);
                SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
            }

            else
            {
                int unlockedindex = entries.IndexOf(allentries[SpawnPosIndex]);
            
                if  (unlockedindex >= entries.Count - 1)
                    unlockedindex = -1;
            
                unlockedindex++;
                //LoggingHandler.LogMessage($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}", taskName, LogLevel.Debug);
                SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static void UnlockSpecificSpawnData(Team team, LevelId level, int index, string taskName, bool secret = false)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.TryGetValue(team, out var teamSpawnData))
            {
                LoggingHandler.LogMessage($"Team {team} does not have any spawn data.", taskName, LogLevel.Error);
                return;
            }
            if (!teamSpawnData.ContainsKey(level))
            {
                LoggingHandler.LogMessage($"Team {team} does not have any spawn data for Level {level}.", taskName, LogLevel.Error);
                return;
            }
        
            Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level][index] = true;
        
            var entry = LevelSpawnData.AllSpawnData[team][level][index];
            LoggingHandler.LogMessage($"Unlocked spawn data for Team {team} and Level {level}. Pos is {entry.Pos}, Index in List is {LevelSpawnData.AllSpawnData[team][level].IndexOf(entry)}, Index is {index}", taskName, LogLevel.SuperDebug);
            Mod.ArchipelagoHandler.Save(taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void BonusStageUnlockCallback(Team team, LevelId level, string taskName, int keynum = 0, bool goal = false)
    {
        //this is called from Keys when already have enough keys
        //need to handle from goal
        try
        {
            if (keynum == 0 && !goal)
                return;
            
            if (keynum > 0)
            {
                if (Mod.LevelSelectManager.GetIfLevelGoaled(team, level))
                {
                    //unlocking bonus stage spawn here
                    LoggingHandler.LogMessage($"Unlocking Bonus Stage Spawn for {team} {level}", taskName, LogLevel.APAction);
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
            }

            if (goal)
            {
                var keys = Mod.SaveDataHandler.CustomSaveData.BonusKeysPickedUp[team][level].Count(key => key);

                if (keys >= Mod.LevelSelectManager.BonusKeysNeededForBonusStage || team is Team.SuperHardMode)
                {
                    //unlocking bonus stage spawn here
                    LoggingHandler.LogMessage($"Unlocking Bonus Stage Spawn for {team} {level}", taskName, LogLevel.Error);
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
            }
            Mod.ArchipelagoHandler.Save(taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static unsafe void SelectActFromLevelSelectCallback(string taskName)
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
            {
                LoggingHandler.LogMessage($"Level {levelIndex} is out of range.", taskName, LogLevel.Error);
                return;
            }
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
            
            var team = (Team)storyIndex;
            
            if (Mod.LevelSelectManager.ActSelectedInLevelSelect is Act.Act2 && team is Team.Sonic)
                team = Team.SuperHardMode;
            
            var unlockedSpawn = GetUnlockedSpawnData(team, level, taskName);
            var allSpawnForLevel = GetAllSpawnDataForLevel(team, level, taskName);
            
            
            if (unlockedSpawn.Count == 0)
            {
                LoggingHandler.LogMessage($"No Unlocked Spawn for Team {team} Level {level} Defaulting to Start", taskName, LogLevel.SuperDebug);
                SpawnPosIndex = 0;
                return;
            }
            SpawnPosIndex = allSpawnForLevel.IndexOf(unlockedSpawn.First());
            LoggingHandler.LogMessage($"Team {team} Level {level} SpawnPosIndex {SpawnPosIndex}", taskName, LogLevel.SuperDebug);
            ShouldCheckForInput = true;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static unsafe void SpawnPosCallbackChangeLevel(string taskName)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777B4);
            var team = *(int*)(baseAddress + 0x220);
            var levelSelectIndex = *(int*)(baseAddress + 0x194);
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelSelectIndex];
            
            if (!GetAllSpawnDataForLevel((Team)team, level, taskName).Any())
                return;
            if (GetAllSpawnDataForLevel((Team)team, level, taskName).Last().Bonusstage || level is LevelId.MetalMadness)
            {
                LevelSpawnGameWrites.ChangeSpawnLevelForOnSetAct((Team)team, levelSelectIndex, taskName);
            }
            if ((bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, taskName)! && (Team)team == Team.Sonic && GameStateHandler.GetCurrentAct(taskName) == Act.Act2)
                GameStateGameWrites.SetCurrentAct(Act.Act3, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static unsafe void GoToGameSpawnPosCallback(string taskName)
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
            {
                LoggingHandler.LogMessage($"Level {levelIndex} is out of range.", taskName, LogLevel.Error);
                return;
            }
            
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
        
            var actPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(actPtr + 0x2BC);
        
            var team = (Team)storyIndex;

            if (team is Team.Sonic && (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, taskName)! &&
                GameStateHandler.GetCurrentAct(taskName) == Act.Act3)
            {
                LoggingHandler.LogMessage($"SuperHardMode in GoToGameSpawnPosCallback.", taskName, LogLevel.SuperDebug);
                team = Team.SuperHardMode;
            }

            if (!GetAllSpawnDataForLevel(team, level, taskName).Any())
                return;

            if (SpawnPosIndex > GetAllSpawnDataForLevel(team, level, taskName).Count - 1)
                return;
        
            LevelSpawnEntry entry = GetAllSpawnDataForLevel(team, level, taskName)[SpawnPosIndex];
            LevelSpawnGameWrites.ChangeSpawnPos(team, level, entry, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static List<LevelSpawnEntry> GetAllSpawnDataForLevel(Team team, LevelId level, string taskName)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.TryGetValue(team, out Dictionary<LevelId, List<LevelSpawnEntry>>? teamSpawnData))
            {
                LoggingHandler.LogMessage($"Team {team} does not have any spawn data.", taskName, LogLevel.SuperDebug);
                return [];
            }
            if (!teamSpawnData.TryGetValue(level, out List<LevelSpawnEntry>? levelSpawnData))
            {
                LoggingHandler.LogMessage($"Team {team} does not have any spawn data for Level {level}.", taskName, LogLevel.SuperDebug);
                return [];
            }
            return levelSpawnData;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return [];
    }

    public static List<LevelSpawnEntry> GetUnlockedSpawnData(Team team, LevelId level, string taskName)
    {
        try
        {
            if (!LevelSpawnData.AllSpawnData.TryGetValue(team, out var teamSpawnData))
            {
                //LoggingHandler.LogMessage($"Team {team} does not have any spawn data.", taskName, LogLevel.Debug);
                return [];
            }
            if (!teamSpawnData.ContainsKey(level))
            {
                //LoggingHandler.LogMessage($"Team {team} does not have any spawn data for Level {level}.", taskName, LogLevel.Debug);
                return [];
            }

            if (ShouldIncludeSecret(team, level))
            {
                return LevelSpawnData.AllSpawnData[team][level].Where((x, index) => Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level][index]).ToList();
            }
        
            return LevelSpawnData.AllSpawnData[team][level].Where((x, index) => !x.Secret && Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level][index] ).ToList();
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return [];
    }
    
    public static string GetLevelSelectUiText(Team team, LevelId level, string taskName)
    {
        try
        {
            if (team is Team.Sonic or Team.SuperHardMode)
            {
                if (Mod.LevelSelectManager.ActSelectedInLevelSelect is Act.Act2 && (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, taskName)!)
                    team = Team.SuperHardMode;
            }
            
            if (SpawnPosIndex > GetAllSpawnDataForLevel(team, level, taskName).Count - 1 || SpawnPosIndex < 0)
            {
                LoggingHandler.LogMessage($"GetLevelSelectUiText Team {team} Level {level} Index {SpawnPosIndex} out of range", taskName, LogLevel.Error);
                SpawnPosIndex = 0;
            }
            
            var unlockedSpawnEntries = GetUnlockedSpawnData(team, level, taskName);
            
            if (unlockedSpawnEntries.Count < 1) 
                return "Start of Level";
            
            
            if (SpawnPosIndex == 0)
            {
                return "Start of Level";
            }

            if (GetAllSpawnDataForLevel(team, level, taskName)[SpawnPosIndex].Bonusstage)
                return "Bonus Stage";
                    
            var result = $"Checkpoint: {SpawnPosIndex}";

            if (GetAllSpawnDataForLevel(team, level, taskName)[SpawnPosIndex].Secret)
            {
                result += $" SECRET!";
                if (!Mod.IsDebug)
                    return "Start of Level";
            }
            return result;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return "Start of Level";
    }
    
}