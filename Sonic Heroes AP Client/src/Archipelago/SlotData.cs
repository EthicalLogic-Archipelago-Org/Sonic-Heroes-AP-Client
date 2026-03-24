
using System.Drawing;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Exceptions;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.LevelUnlocking;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Archipelago;

public class SlotData
{
    //Removed from SlotData
    //RingLink
    //RingLinkOverlord
    //Deathlink
    //SkipMetalMadness
    //DontLoseBonusKey
    //ModernRingLoss
    
    
    //Moved to Level Select Manager
    //public StoriesAndSanities EnabledStoriesAndSanities;
    //public GoalUnlockConditions GoalUnlockConditions;
    //public FinalBoss FinalBoss;

    public EntireRunUnlockType EntireRunUnlockType;
    
    public int GoalLevelCompletions;
    public int GoalLevelCompletionsPerStory;
    public Rank RequiredRank;

    public int DarksanityCheckSize;
    public int RosesanityCheckSize;
    public int ChaotixsanityRingCheckSize;
    public bool RemoveCasinoParkVIPTableLaserGate;
    public AbilityCharacterUnlockType AbilityCharacterUnlockType;
    

    public SlotData(Dictionary<string, Object> slotDict, string taskName)
    {
        var slotDataStr = slotDict.Aggregate("", (current, x) => current + $"{x.Key}: {x.Value}\n");
        
        LoggingHandler.LogMessage(slotDataStr, taskName, LogLevel.APAction);
        
        var apworldversion = "0.0.0";

        if (slotDict.ContainsKey("ModVersion"))
        {
            apworldversion = slotDict["ModVersion"].ToString();
        }
        
        else if (slotDict.ContainsKey("APWorldVersion"))
        {
            apworldversion = slotDict["APWorldVersion"].ToString();
        }

        try
        {
            if (!Mod.CheckCurrentModVersionWithValue(apworldversion!))
            {
                var versionErrorMsg = $"Your Mod and APWorld versions are incompatible. Your Mod version is: {Mod.ModConfig.ModVersion} and your APWorld version is: {apworldversion}";
                LoggingHandler.LogMessage(versionErrorMsg, taskName, LogLevel.Error);
                throw new ModVersionConflictException(versionErrorMsg, taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }

        
        LoggingHandler.LogMessage($"Slot Data Version Check Passed", taskName, LogLevel.SuperDebug);
        var gateLevelCounts = ((JArray)slotDict["GateLevelCounts"]).ToObject<int[]>();
        var gateEmblemCosts = ((JArray)slotDict["GateEmblemCosts"]).ToObject<int[]>();
        var shuffledLevels = ((JArray)slotDict["ShuffledLevels"]).ToObject<string[]>();
        var shuffledBosses = ((JArray)slotDict["ShuffledBosses"]).ToObject<string[]>();
        var runningLevelCount = 0;
        
        
        for (var gateIndex = 0; gateIndex < gateEmblemCosts.Length; gateIndex++)
        {
            var gateLevelStrings = shuffledLevels.Skip(runningLevelCount).Take(gateLevelCounts[gateIndex]).ToArray();
            var bossLevelString = shuffledBosses[gateIndex];
            Mod.LevelSelectManager.GateData.Add(new GateDatum(
                this,
                gateIndex,
                gateEmblemCosts[gateIndex],
                gateLevelStrings,
                bossLevelString
            ));
            if (gateIndex == 0)
                Mod.LevelSelectManager.GateData[gateIndex].IsUnlocked = true;
            runningLevelCount += gateLevelCounts[gateIndex];
        }
        LoggingHandler.LogMessage($"Slot Data Gate Datum Done", taskName, LogLevel.SuperDebug);
        
        EntireRunUnlockType = (EntireRunUnlockType)(int)(long)slotDict["UnlockType"];
        
        Mod.LevelSelectManager.FinalBoss = (FinalBoss)(int)(long)slotDict["FinalBoss"];
        
        GoalLevelCompletions = (int)(long)slotDict["GoalLevelCompletions"];
        GoalLevelCompletionsPerStory = (int)(long)slotDict["GoalLevelCompletionsPerStory"];
        RequiredRank = (Rank)(int)(long)slotDict["RequiredRank"];

        foreach (var str in ((JArray)slotDict["IncludedLevelsAndSanities"]).ToObject<string[]>().ToList())
        {
            StoriesAndSanities? res = Enum.GetValues<StoriesAndSanities>().Cast<StoriesAndSanities?>().FirstOrDefault(x => str.Replace(" ", "").Contains(x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe, StringComparison.InvariantCultureIgnoreCase));
            if (res == null)
            {
                LoggingHandler.LogMessage($"{str} is not a valid StoriesAndSanities", taskName, LogLevel.Error);
                continue;
            }
            Mod.LevelSelectManager.EnabledStoriesAndSanities |= (StoriesAndSanities)res;
        }
        
        LoggingHandler.LogMessage($"Slot Data Stories and Sanities Done", taskName, LogLevel.SuperDebug);
        
        foreach (var str in ((JArray)slotDict["GoalUnlockConditions"]).ToObject<string[]>().ToList())
        {
            GoalUnlockConditions? res = Enum.GetValues<GoalUnlockConditions>().Cast<GoalUnlockConditions?>().FirstOrDefault(x => str.Replace(" ", "").Contains(x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe, StringComparison.InvariantCultureIgnoreCase));
            if (res == null)
            {
                LoggingHandler.LogMessage($"{str} is not a valid GoalUnlockConditions", taskName, LogLevel.Error);
                continue;
            }
            Mod.LevelSelectManager.GoalUnlockConditions |= (GoalUnlockConditions)res;
        }
        DarksanityCheckSize = (int)(long)slotDict["DarkSanity"];
        RosesanityCheckSize = (int)(long)slotDict["RoseSanity"];
        ChaotixsanityRingCheckSize = (int)(long)slotDict["ChaotixSanity"];
        RemoveCasinoParkVIPTableLaserGate =  (long)slotDict["RemoveCasinoParkVIPTableLaserGate"] == 1;
        AbilityCharacterUnlockType = (AbilityCharacterUnlockType)(int)(long)slotDict["AbilityUnlocks"];

        LoggingHandler.LogMessage($"Slot Data Constructor Done", taskName, LogLevel.SuperDebug);
        ArchipelagoHandler.CheckTags(taskName);

    }
}