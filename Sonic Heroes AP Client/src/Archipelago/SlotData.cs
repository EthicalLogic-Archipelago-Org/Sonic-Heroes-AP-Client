
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.LevelUnlocking;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;
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
    

    public SlotData(Dictionary<string, Object> slotDict)
    {
        try
        {
            foreach (var x in slotDict) 
                Console.WriteLine($"{x.Key} {x.Value}");
            
            var apworldversion = "0.0.0";

            if (slotDict.ContainsKey("ModVersion"))
            {
                apworldversion = slotDict["ModVersion"].ToString();
            }
            
            else if (slotDict.ContainsKey("APWorldVersion"))
            {
                apworldversion = slotDict["APWorldVersion"].ToString();
            }
            
            var slotVersion = apworldversion.Split(".");
            var modVersion = Mod.ModConfig.ModVersion.Split(".");
            
            if (modVersion[0] != slotVersion[0] || modVersion[1] != slotVersion[1])
            {
                while (true)
                {
                    Console.WriteLine($"Your Mod and APWorld versions are incompatible. Your Mod version is: {Mod.ModConfig.ModVersion} and your APWorld version is: {apworldversion}");
                    LoggerWindow.Log($"Your Mod and APWorld versions are incompatible. Your Mod version is: {Mod.ModConfig.ModVersion} and your APWorld version is: {apworldversion}");
                    Thread.Sleep(3000);
                }
            }
            
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
            
            EntireRunUnlockType = (EntireRunUnlockType)(int)(long)slotDict["UnlockType"];
            
            if (EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
            {
                //Unlock All Characters and Abilities
                foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!))
                {
                    AbilityCharacterManager.SetCharUnlock(team, FormationChar.Speed, true);
                    AbilityCharacterManager.SetCharUnlock(team, FormationChar.Power, true);
                    AbilityCharacterManager.SetCharUnlock(team, FormationChar.Flying, true);
                    foreach (var region in Enum.GetValues<Region>())
                    {
                        AbilityCharacterManager.HandleAbilityUnlockCheck(team, region, true);
                    }
                }
            }
            
            
            Mod.LevelSelectManager.FinalBoss = (FinalBoss)(int)(long)slotDict["FinalBoss"];
            
            GoalLevelCompletions = (int)(long)slotDict["GoalLevelCompletions"];
            GoalLevelCompletionsPerStory = (int)(long)slotDict["GoalLevelCompletionsPerStory"];
            RequiredRank = (Rank)(int)(long)slotDict["RequiredRank"];


            foreach (var str in ((JArray)slotDict["IncludedLevelsAndSanities"]).ToObject<string[]>().ToList())
            {
                StoriesAndSanities? res = Enum.GetValues<StoriesAndSanities>().Cast<StoriesAndSanities?>().FirstOrDefault(x => str.Replace(" ", "").Contains(x.ToString()!, StringComparison.InvariantCultureIgnoreCase));
                if (res == null)
                {
                    Console.WriteLine($"{str} is not a valid StoriesAndSanities");
                    continue;
                }
                Mod.LevelSelectManager.EnabledStoriesAndSanities |= (StoriesAndSanities)res;
            }
            
            foreach (var str in ((JArray)slotDict["GoalUnlockConditions"]).ToObject<string[]>().ToList())
            {
                GoalUnlockConditions? res = Enum.GetValues<GoalUnlockConditions>().Cast<GoalUnlockConditions?>().FirstOrDefault(x => str.Replace(" ", "").Contains(x.ToString()!, StringComparison.InvariantCultureIgnoreCase));
                if (res == null)
                {
                    Console.WriteLine($"{str} is not a valid GoalUnlockConditions");
                    continue;
                }
                Mod.LevelSelectManager.GoalUnlockConditions |= (GoalUnlockConditions)res;
            }
            DarksanityCheckSize = (int)(long)slotDict["DarkSanity"];
            RosesanityCheckSize = (int)(long)slotDict["RoseSanity"];
            ChaotixsanityRingCheckSize = (int)(long)slotDict["ChaotixSanity"];
            RemoveCasinoParkVIPTableLaserGate =  (long)slotDict["RemoveCasinoParkVIPTableLaserGate"] == 1;
            AbilityCharacterUnlockType = (AbilityCharacterUnlockType)(int)(long)slotDict["AbilityUnlocks"];

            Mod.ArchipelagoHandler.CheckTags();
        }
        catch (Exception e)
        {
            while (true)
            {
                Console.WriteLine(e);
                Thread.Sleep(3500);
            }
        }
    }
}