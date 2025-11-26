using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.Configuration;

namespace Sonic_Heroes_AP_Client;

public class Level
{
    public LevelId LevelId;
    public Team? Story;
    public bool IsBoss;
    private bool _isUnlocked;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set
        {
            _isUnlocked = value;
            Mod.SaveDataHandler.SetLevelActive(LevelId, IsBoss, Story, value);
        }
    }
    
    public void RefreshUnlockStatus()
    {
        IsUnlocked = _isUnlocked; 
    }
    
    public Level(string levelCode)
    {
        var storyId = levelCode[0].ToString().ToLower();
        Story = storyId switch
        {
            "s" => Team.Sonic,
            "d" => Team.Dark,
            "r" => Team.Rose,
            "c" => Team.Chaotix,
            "b" => null,
            _ => Team.Sonic
        };
        LevelId = (LevelId)int.Parse(levelCode[1..]);
        IsBoss = Story == null;
    }
}

public class GateDatum
{
    private readonly SlotData _slotData;
    public int Index { get; private set; }
    public int BossCost;
    public List<Level> Levels;
    public Level BossLevel;
    private bool _isUnlocked;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set
        {
            _isUnlocked = value;
            foreach (var level in Levels)
                level.IsUnlocked = value;
        }
    }

    public GateDatum Next()
    {
        return Index != _slotData.GateData.Count - 1 ? _slotData.GateData[Index + 1] : this;
    }

    public GateDatum Previous()
    {
        return Index != 0 ? _slotData.GateData[Index - 1] : this;
    }
    
    public void RefreshUnlockStatus()
    {
        IsUnlocked = _isUnlocked;
    }

    public GateDatum(SlotData slotData, int index, int bossCost, string[] levelIndices, string bossLevel)
    {
        _slotData = slotData;
        Index = index;
        BossCost = bossCost;
        Levels = new List<Level>();
        foreach(var levelIndex in levelIndices)
            Levels.Add(new Level(levelIndex));
        BossLevel = new Level(bossLevel);
    }
}

public class SlotData
{
    public List<GateDatum> GateData;
    public Goal Goal;
    public GoalUnlockCondition GoalUnlockCondition;
    public int GoalLevelCompletions;
    public bool RingLinkOverlord;
    public bool SkipMetalMadness
    {
        set
        {
            GameHandler.SetSkipMadness(value);
        }
    }

    public Rank RequiredRank;
    private bool _dontLoseBonusKey;
    public bool DontLoseBonusKey    
    {
        get => _dontLoseBonusKey;
        set
        {
            _dontLoseBonusKey = value;
            GameHandler.SetDontLoseBonusKey(_dontLoseBonusKey);
        }
    }
    public Dictionary<Team, MissionsActive> StoriesActive;
    private bool _modernRingLoss;
    public bool ModernRingLoss
    {
        get => _modernRingLoss;
        set
        {
            _modernRingLoss = value;
            GameHandler.SetRingLoss(_modernRingLoss);
        }
    }
    // SANITY ------
    public bool IsDarksanityActive => DarksanityCheckSize != 0;
    public int DarksanityCheckSize;
    public bool IsRosesanityActive => RosesanityCheckSize != 0;
    public int RosesanityCheckSize;
    public bool IsChaotixsanityActive => ChaotixsanityRingCheckSize != 0;
    public int ChaotixsanityRingCheckSize;
    // -------------

    public bool YamlRinglink;
    public bool RingLink;
    public bool YamlDeathlink;
    public bool DeathLink;
    
    public Dictionary<Team, int> KeySanityDict;
    
    public Dictionary<Team, int> CheckpointSanityDict;

    public bool SuperHardModeSonicAct2;

    public bool RemoveCasinoParkVIPTableLaserGate;

    public AbilityUnlockType AbilityUnlocks;

    private bool _checkPointPriorityWrite;
    public bool CheckPointPriorityWrite
    {
        get => _checkPointPriorityWrite;
        set
        {
            _checkPointPriorityWrite = value;
            //GameHandler.SetCheckPointPriorityWrite(_checkPointPriorityWrite);
        }
    }
    
    private bool _teamBlastWrite;
    public bool TeamBlastWrite
    {
        get => _teamBlastWrite;
        set
        {
            _teamBlastWrite = value;
            GameHandler.SetTeamBlastWrite(_teamBlastWrite);
        }
    }

    private bool _levelSelectAllLevelsAvailableWrite;
    public bool LevelSelectAllLevelsAvailableWrite
    {
        get => _levelSelectAllLevelsAvailableWrite;
        set
        {
            _levelSelectAllLevelsAvailableWrite = value;
            GameHandler.SetLevelSelectAllLevelsAvailableWrite(_levelSelectAllLevelsAvailableWrite);
        }
    }
    

    public SlotData(Dictionary<string, object> slotDict)
    {
        GateData = new List<GateDatum>();
        
        foreach (var x in slotDict)
        {
            Console.WriteLine($"{x.Key} {x.Value}");
        }


        try
        {
            var gateLevelCounts = ((JArray)slotDict["GateLevelCounts"]).ToObject<int[]>();
            var gateEmblemCosts = ((JArray)slotDict["GateEmblemCosts"]).ToObject<int[]>();
            var shuffledLevels = ((JArray)slotDict["ShuffledLevels"]).ToObject<string[]>();
            var shuffledBosses = ((JArray)slotDict["ShuffledBosses"]).ToObject<string[]>();
            var version = slotDict["APWorldVersion"].ToString();
            var slotVersion = version.Split(".");
            var modVersion = Mod.ModConfig.ModVersion.Split(".");
            
            if (modVersion[0] != slotVersion[0] || modVersion[1] != slotVersion[1])
            {
                while (true)
                {
                    Console.WriteLine($"Your Mod and APWorld versions are incompatible. Your Mod version is: {Mod.ModConfig.ModVersion} and your APWorld version is: {version}");
                    Logger.Log($"Your Mod and APWorld versions are incompatible. Your Mod version is: {Mod.ModConfig.ModVersion} and your APWorld version is: {version}");
                    Thread.Sleep(3000);
                }
            }
            
            var runningLevelCount = 0;
            for (var gateIndex = 0; gateIndex < gateEmblemCosts.Length; gateIndex++)
            {
                var gateLevelStrings = shuffledLevels.Skip(runningLevelCount).Take(gateLevelCounts[gateIndex]).ToArray();
                var bossLevelString = shuffledBosses[gateIndex];
                GateData.Add(new GateDatum(
                    this,
                    gateIndex,
                    gateEmblemCosts[gateIndex],
                    gateLevelStrings,
                    bossLevelString
                ));
                if (gateIndex == 0)
                    GateData[gateIndex].IsUnlocked = true;
                runningLevelCount += gateLevelCounts[gateIndex];
            }
            Goal = (Goal)(int)(long)slotDict["Goal"];
            GoalUnlockCondition = (GoalUnlockCondition)(int)(long)slotDict["GoalUnlockCondition"];
            GoalLevelCompletions = (int)(long)slotDict["GoalLevelCompletions"];
            SkipMetalMadness = (long)slotDict["SkipMetalMadness"] == 1;
            RequiredRank = (Rank)(int)(long)slotDict["RequiredRank"];
            DontLoseBonusKey = (long)slotDict["DontLoseBonusKey"] == 1;
            ModernRingLoss = (long)slotDict["ModernRingLoss"] == 1;
            StoriesActive = new Dictionary<Team, MissionsActive>
            {
                { Team.Sonic, (MissionsActive)(long)slotDict["SonicStory"] },
                { Team.Dark, (MissionsActive)(long)slotDict["DarkStory"] },
                { Team.Rose, (MissionsActive)(long)slotDict["RoseStory"] },
                { Team.Chaotix, (MissionsActive)(long)slotDict["ChaotixStory"] }
            };
            DarksanityCheckSize = (int)(long)slotDict["DarkSanity"];
            RosesanityCheckSize = (int)(long)slotDict["RoseSanity"];
            ChaotixsanityRingCheckSize = (int)(long)slotDict["ChaotixSanity"];
            
            var sonicKeySanity = (int)(long)slotDict["SonicKeySanity"];
            var darkKeySanity = (int)(long)slotDict["DarkKeySanity"];
            var roseKeySanity = (int)(long)slotDict["RoseKeySanity"];
            var chaotixKeySanity = (int)(long)slotDict["ChaotixKeySanity"];
            KeySanityDict = new Dictionary<Team, int>
            {
                { Team.Sonic, sonicKeySanity },
                { Team.Dark, darkKeySanity },
                { Team.Rose, roseKeySanity },
                { Team.Chaotix, chaotixKeySanity }
            };
            
            var sonicCheckpointSanity = (int)(long)slotDict["SonicCheckpointSanity"];
            var darkCheckpointSanity = (int)(long)slotDict["DarkCheckpointSanity"];
            var roseCheckpointSanity = (int)(long)slotDict["RoseCheckpointSanity"];
            var chaotixCheckpointSanity = (int)(long)slotDict["ChaotixCheckpointSanity"];
            CheckpointSanityDict = new Dictionary<Team, int>
            {
                { Team.Sonic, sonicCheckpointSanity },
                { Team.Dark, darkCheckpointSanity },
                { Team.Rose, roseCheckpointSanity },
                { Team.Chaotix, chaotixCheckpointSanity }
            };
            
            
            SuperHardModeSonicAct2 = (long)slotDict["SuperHardModeSonicAct2"] == 1;
            
            RemoveCasinoParkVIPTableLaserGate =  (long)slotDict["RemoveCasinoParkVIPTableLaserGate"] == 1;
            
            AbilityUnlocks = (AbilityUnlockType)(int)(long)slotDict["AbilityUnlocks"];

            CheckPointPriorityWrite = true;
            LevelSelectAllLevelsAvailableWrite = Mod.IsDebug;
            //TeamBlastWrite = false;
            
            RingLinkOverlord = (long)slotDict["RingLinkOverlord"] == 1;
            
            YamlDeathlink = (long)slotDict["DeathLink"] == 1;
            YamlRinglink = (long)slotDict["RingLink"] == 1;
            CheckTags();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Logger.Log(e.ToString());

            while (true)
            {
                Console.WriteLine($"Something went wrong.");
                Logger.Log("Something went wrong.");
                Thread.Sleep(3500);
            }
            
            
        }
    }
    
    public void CheckTags()
    {
        List<string> tags = new List<string>();
        DeathLink = Mod.Configuration?.TagOptions.DeathLink switch
        {
            Config.DeathLinkTag.UseYaml => YamlDeathlink,
            Config.DeathLinkTag.OverrideOn => true,
            Config.DeathLinkTag.OverrideOff => false,
            _ => false
        };
        if (DeathLink)
            tags.Add("DeathLink");
        RingLink = Mod.Configuration?.TagOptions.RingLink switch
        {
            Config.RingLinkTag.UseYaml => YamlRinglink,
            Config.RingLinkTag.OverrideOn => true,
            Config.RingLinkTag.OverrideOff => false,
            _ => false
        };
        if (RingLink)
            tags.Add("RingLink");
        Mod.ArchipelagoHandler.UpdateTags(tags);
    }
    
    
    
    
    public unsafe void RecalculateOpenLevels(bool isCompletingNewLevel = false)
    {
        Mod.SaveDataHandler!.SaveData->EmblemCount = (byte)Mod.SaveDataHandler.CustomSaveData.Emblems;
        
        var finalGate = GateData.First(x => x.BossLevel.LevelId == LevelId.MetalMadness);
        
        //var needAbilitiesAndEmeralds = GoalUnlockCondition is GoalUnlockCondition.AbilitiesAndEmeralds;
        //var needAbilities = GoalUnlockCondition is GoalUnlockCondition.Abilities;
        var needEmeralds = GoalUnlockCondition is GoalUnlockCondition.Emeralds;
        var needLevelCompletions = GoalUnlockCondition is GoalUnlockCondition.LevelCompletions;
        var needLevelCompletionsAndEmeralds = GoalUnlockCondition is GoalUnlockCondition.LevelCompletionsandEmeralds;

        var hasCharacters = Mod.AbilityUnlockHandler!.HasAllCharsForTeam(Team.Sonic);
        var hasAbilities = Mod.AbilityUnlockHandler!.HasAllAbilitiesandCharsandLevelUpsForTeam(Team.Sonic);
        var hasEmeralds = GoalUnlockCondition is GoalUnlockCondition.LevelCompletionsandEmeralds or GoalUnlockCondition.Emeralds;
        var hasLevelCompletions = GoalUnlockCondition is GoalUnlockCondition.LevelCompletionsandEmeralds or GoalUnlockCondition.LevelCompletions;
        var hasEmblemsForMetal = Mod.SaveDataHandler.CustomSaveData.Emblems >= finalGate.BossCost;

        if (hasEmeralds)
        {
            foreach (var emeraldData in Mod.SaveDataHandler.CustomSaveData.Emeralds)
            {
                if (emeraldData.Value) 
                    continue;
                
                Console.WriteLine($"Need {emeraldData.Key} Emerald For Final Boss");
                hasEmeralds = false;
            }
        }

        if (hasLevelCompletions)
        {
            var levelGoals = isCompletingNewLevel ? Mod.AbilityUnlockHandler.GetCompletedLevelsForTeam(Team.Sonic) + 1 : Mod.AbilityUnlockHandler.GetCompletedLevelsForTeam(Team.Sonic);

            if (levelGoals < Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions)
            {
                Console.WriteLine($"Need {Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions} Levels Goals For Final Boss : Only Have {levelGoals}");
                hasLevelCompletions = false;
            }
        }
        
        finalGate.BossLevel.IsUnlocked = hasCharacters && ((needLevelCompletionsAndEmeralds && hasLevelCompletions && hasEmeralds) || (needLevelCompletions && hasLevelCompletions) || (needEmeralds && hasEmeralds) || hasEmblemsForMetal);
        
        Mod.SaveDataHandler.CustomSaveData.GateBossUnlocked[finalGate.Index] = finalGate.BossLevel.IsUnlocked;

        foreach (var gate in GateData.Where(gate => Mod.SaveDataHandler.CustomSaveData.GateBossComplete[gate.Index]))
            gate.Next().IsUnlocked = true;
            
        foreach (var gate in GateData
                     .Where(gate => gate.IsUnlocked && Mod.SaveDataHandler.CustomSaveData.Emblems >= gate.BossCost 
                                                    && gate.BossLevel.LevelId != LevelId.MetalMadness))
        {
            gate.BossLevel.IsUnlocked = true;
            Mod.SaveDataHandler.CustomSaveData.GateBossUnlocked[gate.Index] = true;
        }
        Mod.ArchipelagoHandler!.Save();
        
        foreach (var gate in GateData)
        {
            gate.RefreshUnlockStatus();
            gate.BossLevel.RefreshUnlockStatus();
        }
    }

    public int? FindGateForLevel(LevelId levelId, Team storyId)
    {
        foreach (var gate in GateData.Where(gate => gate.Levels.Any(x => x.LevelId == levelId && x.Story == storyId || gate.BossLevel.LevelId == levelId)))
            return gate.Index;
        return null;
    }
}