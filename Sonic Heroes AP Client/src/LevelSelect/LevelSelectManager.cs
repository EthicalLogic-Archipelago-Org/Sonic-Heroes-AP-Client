using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.LevelSelect;

/// <summary>
/// Handles the Logic with Level Select.
/// This should be initialized after SlotData
/// </summary>
public class LevelSelectManager
{
    public List<GateDatum> GateData;
    
    //public StoriesAndSanities EnabledStoriesAndSanities;
    public EnabledStories EnabledStories;
    public Dictionary<Team, Dictionary<SanityType, SanityEnableStatus>> EnabledSanities;
    
    public GoalUnlockConditions GoalUnlockConditions;
    public FinalBoss FinalBoss;

    public Act ActSelectedInLevelSelect;

    public int BonusKeysNeededForBonusStage = 1;
    
    private bool _levelSelectAllLevelsAvailableWrite;
    public bool LevelSelectAllLevelsAvailableWrite
    {
        get => _levelSelectAllLevelsAvailableWrite;
        set
        {
            _levelSelectAllLevelsAvailableWrite = value;
            LevelSelectGameWrites.SetLevelSelectAllLevelsAvailableWrite(_levelSelectAllLevelsAvailableWrite);
        }
    }


    public LevelSelectManager()
    {
        GateData = [];
        LevelSelectAllLevelsAvailableWrite = Mod.IsDebug;
        ActSelectedInLevelSelect = Act.Act1;
        
        EnabledStories = EnabledStories.None;
        EnabledSanities = Enum.GetValues<Team>().ToDictionary(team => team, _ => Enum.GetValues<SanityType>().ToDictionary(sanityType => sanityType, _ => SanityEnableStatus.Disabled));
        
        GoalUnlockConditions = GoalUnlockConditions.None;
        FinalBoss = FinalBoss.MetalMadness;
    }

    public void InitConnect(string taskName)
    {
        if (Mod.IsDebug)
        {
            ForceSuperHardModeEnable(taskName);
        }
    }

    private void ForceSuperHardModeEnable(string taskName)
    {
        EnabledStories &= ~EnabledStories.SonicActB;
        EnabledStories |= EnabledStories.SuperHardMode;
        
        if (EnabledSanities[Team.Sonic][SanityType.KeySanity] is SanityEnableStatus.BothActs)
        {
            EnabledSanities[Team.Sonic][SanityType.KeySanity] = SanityEnableStatus.Only1Set;
        }
        
        if (EnabledSanities[Team.Sonic][SanityType.CheckpointSanity] is SanityEnableStatus.BothActs)
        {
            EnabledSanities[Team.Sonic][SanityType.CheckpointSanity] = SanityEnableStatus.Only1Set;
            //EnabledSanities[Team.SuperHardMode][SanityType.CheckpointSanity] = SanityEnableStatus.Only1Set;
        }
    }


    public bool IsThisBossCompletedYet(LevelId level, string taskName)
    {
        if (level is LevelId.MetalMadness or LevelId.MetalOverlord or LevelId.SeaGate)
            return Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.MetalMadnessId);
        
        foreach (var team in Enum.GetValues<Team>().Where(x => (bool)IsThisTeamEnabled(x, taskName)!))
        {
            var tempTeam = team;
            if (tempTeam == Team.SuperHardMode)
                tempTeam = Team.Sonic;

            if (Mod.ArchipelagoHandler.IsLocationChecked(0xA0 + ((int)level - 2) * 2 + 42 * (int)tempTeam))
                return true;
        }
        return false;
    }
    
    
    public unsafe void RecalculateOpenLevels(string taskName, Team teamWithExtraLevelComplete = Team.Sonic, bool isCompletingNewLevel = false)
    {
        try
        {
            //Mod.SaveDataHandler.SaveData->EmblemCount = (byte)Mod.SaveDataHandler.CustomSaveData.Emblems;
            LoggingHandler.LogMessage($"Recalc Save Data Ptr: 0x{(UIntPtr)Mod.SaveDataHandler.SaveData:X}, {Mod.SaveDataHandler.CustomSaveData.Emblems}", taskName, LogLevel.SuperDebug);
            
            //Gate Unlocking
            //foreach (var gate in GateData.Where(gate => Mod.SaveDataHandler.CustomSaveData.GateBossComplete[gate.Index]))
                //gate.Next().IsUnlocked = true;

            foreach (var gate in GateData.Where(gate => IsThisBossCompletedYet(gate.BossLevel.LevelId, taskName)))
            {
                gate.Next().SetIsUnlocked(true, taskName);
            }
                
            //GateBoss Unlocking (Not Final Boss)
            foreach (var gate in GateData
                         .Where(gate => gate.GetIsUnlocked(taskName) && Mod.SaveDataHandler.CustomSaveData.Emblems >= gate.BossCost 
                                                        && gate.BossLevel.LevelId != LevelId.MetalMadness))
            {
                gate.BossLevel.SetIsUnlocked(true, taskName);
                
            }
            
            
            //Final Boss Here
            var finalGate = GateData.First(x => x.BossLevel.LevelId == LevelId.MetalMadness);
            
            var needCharacters = Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.AbilityCharacterUnlocks;
            var needEmblems = GoalUnlockConditions.HasFlag(GoalUnlockConditions.Emblems);
            var needEmeralds = GoalUnlockConditions.HasFlag(GoalUnlockConditions.Emeralds);
            var needLevelCompletions = GoalUnlockConditions.HasFlag(GoalUnlockConditions.LevelCompletionsAllTeams);
            var needLevelCompletionsPerStory = GoalUnlockConditions.HasFlag(GoalUnlockConditions.LevelCompletionsPerStory);
            
            var hasCharacters = true;
            var hasEmblemsForMetal = true;
            var hasEmeralds = true;
            var hasLevelCompletions = true;
            var levelCompletions = isCompletingNewLevel ? 1 : 0;
            var hasLevelCompletionsPerStory = true;
            var levelCompletionsForTeam = 0;

            List<string> finalBossUnlockRequirementsMessage = [];
            
            
            foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x, taskName)!))
            {
                levelCompletions += Mod.LevelSelectManager.GetCompletedLevelsForTeam(team);
                if (needCharacters)
                {
                    if (!AbilityCharacterManager.HasAllCharsForTeam(team, taskName))
                    {
                        finalBossUnlockRequirementsMessage.Add($"All Characters for Team: {team} For Final Boss");
                        hasCharacters = false;
                    }
                }

                if (needLevelCompletionsPerStory)
                {
                    levelCompletionsForTeam = isCompletingNewLevel && team == teamWithExtraLevelComplete ? 1 : 0;
                    
                    if (levelCompletionsForTeam + Mod.LevelSelectManager.GetCompletedLevelsForTeam(team) <
                        Mod.ArchipelagoHandler.SlotData.GoalLevelCompletionsPerStory)
                    {
                        finalBossUnlockRequirementsMessage.Add($"{Mod.ArchipelagoHandler.SlotData.GoalLevelCompletionsPerStory} Level Completions for Team: {team} For Final Boss");
                        hasLevelCompletionsPerStory = false;
                    }
                }
            }
            
            if (needEmeralds)
            {
                foreach (var emeraldData in Mod.SaveDataHandler.CustomSaveData.Emeralds.Where(emeraldData => !emeraldData.Value))
                {
                    finalBossUnlockRequirementsMessage.Add($"{emeraldData.Key} Chaos Emerald");
                    hasEmeralds = false;
                }
            }

            if (needEmblems)
            {
                hasEmblemsForMetal = Mod.SaveDataHandler.CustomSaveData.Emblems >= finalGate.BossCost;
                if (!hasEmblemsForMetal)
                    finalBossUnlockRequirementsMessage.Add($"{finalGate.BossCost} Emblems : Only Have {Mod.SaveDataHandler.CustomSaveData.Emblems}");
            }

            if (needLevelCompletions)
            {
                if (levelCompletions < Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions)
                {
                    finalBossUnlockRequirementsMessage.Add($"{Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions} Levels Goals : Only Have {levelCompletions}");
                    hasLevelCompletions = false;
                }
            }

            if (finalBossUnlockRequirementsMessage.Count != 0)
            {
                var message = "Need these for Final Boss:\n";
                foreach (var requirement in finalBossUnlockRequirementsMessage)
                {
                    message += $"{requirement}";
                    if (requirement != finalBossUnlockRequirementsMessage.Last())
                        message += ",\n";
                }
                LoggingHandler.LogMessage(message, taskName, LogLevel.Info);
            }
            finalGate.BossLevel.SetIsUnlocked(finalGate.GetIsUnlocked(taskName) && hasCharacters && hasEmblemsForMetal && hasEmeralds && hasLevelCompletions && hasLevelCompletionsPerStory, taskName);
            
            
            Mod.ArchipelagoHandler!.Save(taskName);
            
            foreach (var gate in GateData)
            {
                gate.RefreshUnlockStatus(taskName);
                gate.BossLevel.RefreshUnlockStatus(taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    /// <summary>
    /// Finds the Gate that has the specific Level and Team.
    /// </summary>
    /// <param name="levelId">The LevelId to search for</param>
    /// <param name="storyId">The Team to search for (this uses Sonic not Super Hard Mode)</param>
    /// <returns>Either the Index of the Gate or null if not found</returns>
    public int? FindGateForLevel(LevelId levelId, Team storyId)
    {
        if (storyId is Team.SuperHardMode)
            storyId = Team.Sonic;
        foreach (var gate in GateData.Where(gate => gate.Levels.Any(x => (x.LevelId == levelId && x.Story == storyId) || gate.BossLevel.LevelId == levelId)))
            return gate.Index;
        return null;
    }
    
    
    public bool GetIfLevelGoaled(Team team, LevelId level)
    {
        var locationId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0; //the + 0 is the Act

        if (team is not Team.SuperHardMode)
            return Mod.ArchipelagoHandler.IsLocationChecked(locationId) ||
                   Mod.ArchipelagoHandler.IsLocationChecked(locationId + 1);
        
        locationId = SonicHeroesDefinitions.SuperHardModeId + ((int)level - 2);
        return Mod.ArchipelagoHandler.IsLocationChecked(locationId);
    }
    

    public int GetCompletedLevelsForTeam(Team team)
    {
        var levelCompletions = 0;
        levelCompletions += Enum.GetValues<LevelId>().Where(x => x is >= LevelId.SeasideHill and <= LevelId.FinalFortress).Count(level => GetIfLevelGoaled(team, level));
        return levelCompletions;
    }
    
    
    /// <summary>
    /// Checks if the team is enabled.
    /// </summary>
    /// <param name="team">Which Team to check for?</param>
    /// <param name="bothActs">Should Both Acts be required?</param>
    /// <returns>Null if invalid (like Both Acts for SuperHard). True or False otherwise.</returns>
    public bool? IsThisTeamEnabled(Team team, string taskName, bool bothActs = false)
    {
        //LoggingHandler.LogMessage($"IsThisTeamEnabled: Team {team} BothActs: {bothActs}", taskName, LogLevel.SuperDebug, 3);
        if (team is not Team.SuperHardMode || !bothActs)
            return bothActs
                ? IsThisTeamActEnabled(team, Act.Act1, taskName) && IsThisTeamActEnabled(team, Act.Act2, taskName)
                : IsThisTeamActEnabled(team, Act.Act1, taskName) || IsThisTeamActEnabled(team, Act.Act2, taskName);
        LoggingHandler.LogMessage("Both Acts for SuperHard in IsThisTeamEnabled.", taskName, LogLevel.Error, 3);
        return null;
    }


    public bool IsThisTeamActEnabled(Team team, Act act, string  taskName)
    {
        switch (team)
        {
            case Team.SuperHardMode:
                return EnabledStories.HasFlag(EnabledStories.SuperHardMode);
            case Team.Sonic:
                switch (act)
                {
                    case Act.Act1:
                        return EnabledStories.HasFlag(EnabledStories.SonicActA);
                    case Act.Act2:
                        return EnabledStories.HasFlag(EnabledStories.SonicActB);
                    case Act.Act3:
                        LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                        //return (bool)IsThisTeamEnabled(Team.SuperHardMode)!;
                        return false;
                    default:
                        LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                        return false;
                }
            case Team.Dark:
                switch (act)
                {
                    case Act.Act1:
                        return EnabledStories.HasFlag(EnabledStories.DarkActA);
                    case Act.Act2:
                        return EnabledStories.HasFlag(EnabledStories.DarkActB);
                    case Act.Act3:
                    default:
                        LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                        return false;
                }
            case Team.Rose:
                switch (act)
                {
                    case Act.Act1:
                        return EnabledStories.HasFlag(EnabledStories.RoseActA);
                    case Act.Act2:
                        return EnabledStories.HasFlag(EnabledStories.RoseActB);
                    case Act.Act3:
                    default:
                        LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                        return false;
                }
            case Team.Chaotix:
                switch (act)
                {
                    case Act.Act1:
                        return EnabledStories.HasFlag(EnabledStories.ChaotixActA);
                    case Act.Act2:
                        return EnabledStories.HasFlag(EnabledStories.ChaotixActB);
                    case Act.Act3:
                    default:
                        LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                        return false;
                }
            default:
                LoggingHandler.LogMessage($"{act} {team} asked for in IsThisTeamActEnabled", taskName, LogLevel.Error);
                return false;
        }
    }


    /// <summary>
    /// Checks if the Sanity for the given team is enabled.
    /// </summary>
    /// <param name="team">Which Team?</param>
    /// <param name="sanity">Which Specific sanity to check for</param>
    /// <param name="bothActs">Should Both Acts be required? If False, 1 Set is checked for</param>
    /// <returns>Null if invalid, True if enabled, false if not. Will return false for 1 set if checking for both acts (and not ObjSanity).</returns>
    public bool? IsThisSanityEnabled(Team team, SanityType sanity, string taskName, bool bothActs = false)
    {
        if (team is Team.SuperHardMode && bothActs)
        {
            LoggingHandler.LogMessage("Both Acts for SuperHard in IsThisSanityEnabled.", taskName, LogLevel.Error, 3);
            return null;
        }

        if (team is Team.Sonic or Team.SuperHardMode && sanity is SanityType.ObjSanity)
        {
            LoggingHandler.LogMessage($"Obj Sanity asked for team: {team} in IsThisSanityEnabled.", taskName, LogLevel.Error, 3);
            return null;
        }

        switch (bothActs)
        {
            case true:
                return sanity switch
                {
                    SanityType.ObjSanity => EnabledSanities[team][SanityType.ObjSanity] is SanityEnableStatus.BothActs,
                    SanityType.KeySanity => EnabledSanities[team][SanityType.KeySanity] is SanityEnableStatus.BothActs,
                    SanityType.CheckpointSanity => EnabledSanities[team][SanityType.CheckpointSanity] is SanityEnableStatus.BothActs,
                    SanityType.BingoChipSanity => EnabledSanities[team][SanityType.BingoChipSanity] is SanityEnableStatus.BothActs,
                    SanityType.HintRingSanity => EnabledSanities[team][SanityType.HintRingSanity] is SanityEnableStatus.BothActs,
                    SanityType.ItemBoxSanity => EnabledSanities[team][SanityType.ItemBoxSanity] is SanityEnableStatus.BothActs,
                    SanityType.ItemBalloonSanity => EnabledSanities[team][SanityType.ItemBalloonSanity] is SanityEnableStatus.BothActs,
                    SanityType.EggFlapperSanity => EnabledSanities[team][SanityType.EggFlapperSanity] is SanityEnableStatus.BothActs,
                    SanityType.EggPawnSanity => EnabledSanities[team][SanityType.EggPawnSanity] is SanityEnableStatus.BothActs,
                    SanityType.KlagenSanity => EnabledSanities[team][SanityType.KlagenSanity] is SanityEnableStatus.BothActs,
                    SanityType.FalcoSanity => EnabledSanities[team][SanityType.FalcoSanity] is SanityEnableStatus.BothActs,
                    SanityType.EggHammerSanity => EnabledSanities[team][SanityType.EggHammerSanity] is SanityEnableStatus.BothActs,
                    SanityType.CameronSanity => EnabledSanities[team][SanityType.CameronSanity] is SanityEnableStatus.BothActs,
                    SanityType.RhinoLinerSanity => EnabledSanities[team][SanityType.RhinoLinerSanity] is SanityEnableStatus.BothActs,
                    SanityType.EggBishopSanity => EnabledSanities[team][SanityType.EggBishopSanity] is SanityEnableStatus.BothActs,
                    SanityType.E2000Sanity => EnabledSanities[team][SanityType.E2000Sanity] is SanityEnableStatus.BothActs,
                    SanityType.RingSanityGroup => EnabledSanities[team][SanityType.RingSanityGroup] is SanityEnableStatus.BothActs,
                    SanityType.RingSanityIndividual => EnabledSanities[team][SanityType.RingSanityIndividual] is SanityEnableStatus.BothActs,
                    _ => false
                };
            case false:
                return sanity switch
                {
                    SanityType.ObjSanity => EnabledSanities[team][SanityType.ObjSanity] is not SanityEnableStatus.Disabled,
                    SanityType.KeySanity => EnabledSanities[team][SanityType.KeySanity] is not SanityEnableStatus.Disabled,
                    SanityType.CheckpointSanity => EnabledSanities[team][SanityType.CheckpointSanity] is not SanityEnableStatus.Disabled,
                    SanityType.BingoChipSanity => EnabledSanities[team][SanityType.BingoChipSanity] is not SanityEnableStatus.Disabled,
                    SanityType.HintRingSanity => EnabledSanities[team][SanityType.HintRingSanity] is not SanityEnableStatus.Disabled,
                    SanityType.ItemBoxSanity => EnabledSanities[team][SanityType.ItemBoxSanity] is not SanityEnableStatus.Disabled,
                    SanityType.ItemBalloonSanity => EnabledSanities[team][SanityType.ItemBalloonSanity] is not SanityEnableStatus.Disabled,
                    SanityType.EggFlapperSanity => EnabledSanities[team][SanityType.EggFlapperSanity] is not SanityEnableStatus.Disabled,
                    SanityType.EggPawnSanity => EnabledSanities[team][SanityType.EggPawnSanity] is not SanityEnableStatus.Disabled,
                    SanityType.KlagenSanity => EnabledSanities[team][SanityType.KlagenSanity] is not SanityEnableStatus.Disabled,
                    SanityType.FalcoSanity => EnabledSanities[team][SanityType.FalcoSanity] is not SanityEnableStatus.Disabled,
                    SanityType.EggHammerSanity => EnabledSanities[team][SanityType.EggHammerSanity] is not SanityEnableStatus.Disabled,
                    SanityType.CameronSanity => EnabledSanities[team][SanityType.CameronSanity] is not SanityEnableStatus.Disabled,
                    SanityType.RhinoLinerSanity => EnabledSanities[team][SanityType.RhinoLinerSanity] is not SanityEnableStatus.Disabled,
                    SanityType.EggBishopSanity => EnabledSanities[team][SanityType.EggBishopSanity] is not SanityEnableStatus.Disabled,
                    SanityType.E2000Sanity => EnabledSanities[team][SanityType.E2000Sanity] is not SanityEnableStatus.Disabled,
                    SanityType.RingSanityGroup => EnabledSanities[team][SanityType.RingSanityGroup] is not SanityEnableStatus.Disabled,
                    SanityType.RingSanityIndividual => EnabledSanities[team][SanityType.RingSanityIndividual] is not SanityEnableStatus.Disabled,
                    _ => false
                };
        }
        
        LoggingHandler.LogMessage($"HOW DID WE GET HERE???. {team} {sanity} {bothActs} in IsThisSanityEnabled", taskName, LogLevel.Error, 3);
        return false;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    // public MissionsActive GetMissionsActiveForTeam(Team team, string taskName)
    // { 
    //     var result = MissionsActive.None;
    //     switch (team)
    //     {
    //         case Team.SuperHardMode:
    //             if ((bool)IsThisTeamEnabled(Team.SuperHardMode, taskName)!)
    //                 result = MissionsActive.SuperHard;
    //             break;
    //         case Team.Sonic:
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.SonicActA))
    //                 result |= MissionsActive.Act1;
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.SonicActB))
    //                 result |= MissionsActive.Act2;
    //             break;
    //         case Team.Dark:
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.DarkActA))
    //                 result |= MissionsActive.Act1;
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.DarkActB))
    //                 result |= MissionsActive.Act2;
    //             break;
    //         case Team.Rose:
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.RoseActA))
    //                 result |= MissionsActive.Act1;
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.RoseActB))
    //                 result |= MissionsActive.Act2;
    //             break;
    //         case Team.Chaotix:
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.ChaotixActA))
    //                 result |= MissionsActive.Act1;
    //             if (EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.ChaotixActB))
    //                 result |= MissionsActive.Act2;
    //             break;
    //         default:
    //             break;
    //     }
    //     return result;
    // }
    
}