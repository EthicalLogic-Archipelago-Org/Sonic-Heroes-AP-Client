using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.AbilityAndCharacter;

public static class AbilityCharacterManager
{
    public static Dictionary<Team, Dictionary<FormationChar, bool>> ShouldOverrideState = Enum.GetValues<Team>().ToDictionary(x => x, _ => Enum.GetValues<FormationChar>().ToDictionary(y => y, _ => false));


    public static void InitConnect(string taskName)
    {
        try
        {
            //Unlock Jump and PowerAttack for enabled teams (and all abilities for regions > sky)
             foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x, taskName)!))
             {
                 //UnlockAbilityForAllRegions(team, Ability.Jump, taskName);            //moved to APWorld Precollected
                 //UnlockAbilityForAllRegions(team, Ability.PowerAttack,  taskName);    //moved to APWorld Precollected
                 foreach (var region in Enum.GetValues<Region>().Where(reg => reg > Region.Sky))
                 {
                     UnlockAllAbilitiesForRegion(team, region,  taskName);
                 }
             }
     
             //Unlock speed char for all other teams (to avoid death loop)
             foreach (var team in Enum.GetValues<Team>().Where(x => x is not Team.Sonic))
             {
                 SetCharUnlock(team, FormationChar.Speed, true, taskName);
             }
             
             
             if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
             {
                 //Unlock All Characters and Abilities
                 foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x, taskName)!))
                 {
                     SetCharUnlock(team, FormationChar.Speed, true, taskName);
                     SetCharUnlock(team, FormationChar.Power, true, taskName);
                     SetCharUnlock(team, FormationChar.Flying, true, taskName);
                     UnlockAllAbilitiesForAllRegionsForTeam(team, taskName);
                     HandleAbilityUnlockCheck(team, Region.Ocean, taskName, true);
                 }
             }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    
    public static List<Ability> GetAbilitiesForTeam(Team team, string taskName, bool shouldIncludeJump = false)
    {
        List<Ability> result = [];
        try
        {
            foreach (var formationChar in Enum.GetValues<FormationChar>())
            {
                result.AddRange(GetAbilitiesForTeamAndChar(team, formationChar, taskName, shouldIncludeJump));
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return result;
    }

    
    public static List<Ability> GetAbilitiesForTeamAndChar(Team team, FormationChar formationChar, string taskName, bool shouldIncludeJump = false)
    {
        List<Ability> result = [];
        result.AddRange(AbilityCharacterDefinitions.AbilityListForTeamAndChar[team][formationChar]);
        if (!shouldIncludeJump)
            result.Remove(Ability.Jump);
        
        return result;
    }

    
    public static bool CanTeamBlast(Team team, Region region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in CanTeamBlast", taskName, LogLevel.Error);
            return false;
        }
        
        var hasChars = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];
        
        var hasAbilities = HasAllAbilitiesForRegion(team, region, taskName);
        return hasChars && hasAbilities;
    }

    public static bool CanFly(Team team, Region region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in CanFly", taskName, LogLevel.Error);
            return false;
        }

        return Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Flight] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot];
    }
    
    
    public static void UnlockAllAbilitiesForAllRegionsForTeam(Team team, string taskName)
    {
        foreach (var region in Enum.GetValues<Region>())
        {
            UnlockAllAbilitiesForRegion(team, region, taskName);
        }
    }

    public static void UnlockAllAbilitiesForRegion(Team team, Region region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in UnlockAllAbilitiesForRegion", taskName, LogLevel.Error);
            return;
        }
        foreach (var pair in Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region])
        {
            UnlockAbilityForRegion(team, region, pair.Key, taskName);
        }
    }
    
    public static void UnlockAbilityForAllRegions(Team team, Ability ability, string taskName)
    {
        foreach (var region in Enum.GetValues<Region>())
        {
            UnlockAbilityForRegion(team, region, ability, taskName);
        }
    }
    
    
    public static void UnlockAbilityForRegion(Team team, Region region, Ability ability, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in UnlockAllAbilitiesForRegion", taskName, LogLevel.Error);
            return;
        }
        
        Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability] = true;
        PollUpdates(taskName);
    }
    
    
    /// <summary>
    /// Sets Player's Ability to Perform Abilities based on Mod Save Data.
    /// Called from Poll Updates
    /// </summary>
    /// <param name="team">Team</param>
    /// <param name="region">Region</param>
    /// <param name="taskName">Name of the task that is running this</param>
    /// <param name="forceunlock">set to true to force unlock ability</param>
    public static void HandleAbilityUnlockCheck(Team team, Region region, string taskName, bool forceunlock = false)
    {
        LoggingHandler.LogMessage($"HandleAbilityUnlockCheck Team: {team} Region: {region} ForceUnlock: {forceunlock}", taskName, LogLevel.SuperDebug);
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in HandleAbilityUnlockCheck", taskName, LogLevel.Error);
            return;
        }
        AbilityCharacterGameWrites.SetJumpAbility(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Jump]);
        AbilityCharacterGameWrites.SetHomingAttack(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.HomingAttack]);
        AbilityCharacterGameWrites.SetTornado(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Tornado]);
        AbilityCharacterGameWrites.SetRocketAccel(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.RocketAccel]);
        AbilityCharacterGameWrites.SetLightDash(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.LightDash]);
        AbilityCharacterGameWrites.SetTriangleJump(forceunlock || (Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.TriangleJump] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.HomingAttack]));
        AbilityCharacterGameWrites.SetLightAttack(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.LightAttack]);
        AbilityCharacterGameWrites.SetAmyHammerHover(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.AmyHammerHover]);
        AbilityCharacterGameWrites.SetInvisibilty(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Invisibility]);
        AbilityCharacterGameWrites.SetShuriken(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Shuriken]);
        AbilityCharacterGameWrites.SetThundershoot(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot]);
        //AbilityCharacterGameWrites.SetFlying(forceunlock || (Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Flight] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot]));
        AbilityCharacterGameWrites.SetDummyRings(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.DummyRings]);
        AbilityCharacterGameWrites.SetCheeseCannon(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.CheeseCannon]);
        AbilityCharacterGameWrites.SetFlowerSting(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.FlowerSting]);
        AbilityCharacterGameWrites.SetPowerAttack(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.PowerAttack]);
        AbilityCharacterGameWrites.SetComboFinisher(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.ComboFinisher]);
        AbilityCharacterGameWrites.SetGlide(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Glide]);
        AbilityCharacterGameWrites.SetFireDunk(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.FireDunk]);
        //AbilityCharacterGameWrites.SetUltimateFireDunk(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.UltimateFireDunk]);
        AbilityCharacterGameWrites.SetBellyFlop(forceunlock || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.BellyFlop]);
    }
    
    
    public static void UnlockAbilityItemCallback(Ability? ability, Team? team, Region? region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in UnlockAbilityItemCallback", taskName, LogLevel.Error);
            return;
        }
        
        if (ability is null)
        {
            foreach (var a in Enum.GetValues<Ability>())
            {
                UnlockAbilityItemCallback(a, team, region, taskName);
            }
        }
        else if (team is null)
        {
            foreach (var t in Enum.GetValues<Team>())
            {
                UnlockAbilityItemCallback(ability, t, region, taskName);
            }
        }
    
        else if (region is null)
        {
            foreach (var r in Enum.GetValues<Region>())
            {
                UnlockAbilityItemCallback(ability, team, r, taskName);
            }
        }
        else
        {
            Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[(Team)team].AbilityUnlocks[(Region)region][(Ability)ability] = !Mod.IsDebug || !Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[(Team)team].AbilityUnlocks[(Region)region][(Ability)ability];
            PollUpdates(taskName);
        }
    }
    
    
    public static void SetCharUnlock(Team team, FormationChar formationChar, bool unlock, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data is Null in SetCharUnlock", taskName, LogLevel.Error);
            return;
        }
        
        Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[formationChar] = unlock;
        LoggingHandler.LogMessage($"Unlocking Team {team} Character {formationChar} with {unlock}", taskName, LogLevel.SuperDebug);
        ShouldOverrideState[team][formationChar] = true;
        PollUpdates(taskName);
        StageObjHandler.HandleObjSpawningWhenReceivingCharItem(team, formationChar, unlock, taskName);
    }
    
    
    public static bool GetCharUnlock(Team team, FormationChar formationChar, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in GetCharUnlock", taskName, LogLevel.Error);
            return false;
        }
        
        return Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates || Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[formationChar];
    }
    
    
    public static bool HasAllCharsForTeam(Team team, string taskName)
    {
        return GetCharUnlock(team, FormationChar.Speed, taskName) && GetCharUnlock(team, FormationChar.Flying, taskName) && GetCharUnlock(team, FormationChar.Power, taskName);
    }
    
    
    public static bool HasAllAbilitiesForRegion(Team team, Region region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in HasAllAbilitiesForRegion", taskName, LogLevel.Error);
            return false;
        }
        var abilitiesNeeded = 0;
        var abilitiesHave = 0;
        List<Ability> abilities = GetAbilitiesForTeam(team, taskName, true);

        foreach (var ability in abilities)
        {
            abilitiesNeeded += 1;
            abilitiesHave += Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability] ? 1 : 0;
        }
        
        var hasAbilities = abilitiesHave >= abilitiesNeeded;
        return hasAbilities;
    }

    
    public static bool HasAllAbilitiesandCharsandLevelUpsForTeam(Team team, string  taskName)
    {
        if (!HasAllCharsForTeam(team,  taskName))
        {
            //Final Boss Requires All Characters
            return false;
        }

        foreach (var reg in Enum.GetValues<Region>().Where(reg => reg <= Region.Sky))
        {
            if (!HasAllAbilitiesForRegion(team, reg, taskName))
            {
                //Final Boss Requires All Abilities for Team {team} and Region {reg}
                return false;
            }
        }
        return true;
    }
    

    public static string GetLevelSelectUIStringForCharUnlocksForTeam(Team team, string taskName)
    {
        var result = "";
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in GetLevelSelectUIStringForCharUnlocksForTeam", taskName, LogLevel.Error);
            return "";
        }
        
        if (Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed])
        {
            result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Speed]}";
        }
        if (Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Power])
        {
            result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Power]}";
        }
        if (Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying])
        {
            result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Flying]}";
        }
        return result;
            
    }


    public static string GetLevelSelectUIStringForCharUnlocksForSonicSuperHard()
    {
        var result = "";
        return result;
    }

    public static string GetLevelSelectUIStringForCharUnlocksForFinalBoss()
    {
        var result = "";
        return result;
    }
    
    
    public static Dictionary<Team, List<int>> GetLevelSelectUIFinalBossCharUnlocks(string taskName)
    {
        var result = new Dictionary<Team, List<int>>();

        var charsUnlocked = 0;
        var totalCharsNeeded = 0;
        
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            //LoggingHandler.LogMessage($"Custom Save Data Null in GetLevelSelectUIFinalBossCharUnlocks", taskName, LogLevel.Error);
            return result;
        }
    
        foreach (Team team in Enum.GetValues<Team>())
        {
            if (!(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, taskName)!) 
                continue;
            totalCharsNeeded += 3;
            
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed])
                charsUnlocked++;
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power])
                charsUnlocked++;
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying])
                charsUnlocked++;

            result[team] = [charsUnlocked, totalCharsNeeded];
        }

        return result;
    }
    
    
    public static int GetLevelUpForChar(Team team, Region region, FormationChar formationChar, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in GetLevelUpForChar", taskName, LogLevel.Error);
        }
        List<Ability> abilities = GetAbilitiesForTeamAndChar(team, formationChar,  taskName);
        var abilitiesNeeded = abilities.Count;
        var abilitiesHave = abilities.Count(ability => Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability]);

        if (abilitiesHave >= abilitiesNeeded)
            return 3;

        if (abilitiesHave >= abilitiesNeeded / 2.0)
            return 2;

        return abilitiesHave >= 1 ? 1 : 0;
    }
    
    
    public static unsafe void HandleLevelUp(Team team, Region region, FormationChar formationChar, string taskName)
    {
        if (!GameStateHandler.InGame(taskName))
            return;

        if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
            return;
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
            var charlevels = (byte*)(baseAddress + 0x208 + (byte)formationChar);
            switch (formationChar)
            {
                case FormationChar.Speed:
                {
                    var speedMax = GetLevelUpForChar(team, region, FormationChar.Speed, taskName);

                    if (*charlevels > speedMax)
                    {
                        LoggingHandler.LogMessage($"Level Up for Character {formationChar} is over max allowed value of {speedMax}", taskName, LogLevel.SuperDebug);
                        *charlevels = (byte)speedMax;
                    }
                    break;
                }
                case FormationChar.Flying:
                {
                    var flyingMax = GetLevelUpForChar(team, region, FormationChar.Flying, taskName);

                    if (*charlevels > flyingMax)
                    {
                        LoggingHandler.LogMessage($"Level Up for Character {formationChar} is over max allowed value of {flyingMax}", taskName, LogLevel.SuperDebug);
                        *charlevels = (byte)flyingMax;
                    }

                    break;
                }
                case FormationChar.Power:
                {
                    var powerMax = GetLevelUpForChar(team, region, FormationChar.Power, taskName);

                    if (*charlevels > powerMax)
                    {
                        LoggingHandler.LogMessage($"Level Up for Character {formationChar} is over max allowed value of {powerMax}", taskName, LogLevel.SuperDebug);
                        *charlevels = (byte)powerMax;
                    }
                    break;
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
       
    }
    
    public static void PollUpdates(string taskName)
    {
        
        if (!GameStateHandler.InGame(taskName))
            return;

        if (!ArchipelagoHandler.IsConnected)
        {
            return;
        }

        if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
            return;

        Team? tempTeam = GameStateHandler.GetCurrentStory(taskName);
        Act? tempAct = GameStateHandler.GetCurrentAct(taskName);
        LevelId? tempLevel = GameStateHandler.GetCurrentLevel(taskName);

        if (tempTeam == null || tempAct == null || tempLevel == null)
        {
            LoggingHandler.LogMessage($"Team: {tempTeam} Act: {tempAct} Level: {tempLevel}. One is null in PollUpdates().", taskName, LogLevel.Error);
            return;
        }
        
        Team team = (Team)tempTeam;
        Act act = (Act)tempAct;
        LevelId levelId = (LevelId)tempLevel;

        if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(levelId, out Region region))
        {
            LoggingHandler.LogMessage($"LevelId {levelId} does not exist in Region Mapping", taskName, LogLevel.SuperDebug);
            return;
        }
        
        LoggingHandler.LogMessage($"Poll Updates is Updating Game Here", taskName, LogLevel.SuperDebug);
        
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data Null in PollUpdates", taskName, LogLevel.Error);
            return;
        }

        bool speedChar = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed];
        bool flyingChar = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying];
        bool powerChar = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];

        bool forceTeamBlastEnable = false;
        
        AbilityCharacterGameWrites.SetCharLevel(FormationChar.Speed, (byte)GetLevelUpForChar(team, region, FormationChar.Speed, taskName), taskName);
        AbilityCharacterGameWrites.SetCharLevel(FormationChar.Flying, (byte)GetLevelUpForChar(team, region, FormationChar.Flying, taskName), taskName);
        AbilityCharacterGameWrites.SetCharLevel(FormationChar.Power, (byte)GetLevelUpForChar(team, region, FormationChar.Power, taskName), taskName);
        
        HandleAbilityUnlockCheck(team, region, taskName);
        
        
        AbilityCharacterGameWrites.SetCharState(FormationChar.Speed, speedChar, ShouldOverrideState[team][FormationChar.Speed], taskName);
        AbilityCharacterGameWrites.SetCharState(FormationChar.Flying, flyingChar, ShouldOverrideState[team][FormationChar.Flying],  taskName);
        AbilityCharacterGameWrites.SetCharState(FormationChar.Power, powerChar, ShouldOverrideState[team][FormationChar.Power], taskName);
        

        if (forceTeamBlastEnable || CanTeamBlast(team, region, taskName))
        {
            //Team Blast is allowed
            AbilityCharacterGameWrites.SetTeamBlastWrite(true);
        }
        else
        {
            //Team Blast is not allowed
            AbilityCharacterGameWrites.SetTeamBlastWrite(false);
        }
        
        ShouldOverrideState[team][FormationChar.Speed] = false;
        ShouldOverrideState[team][FormationChar.Flying] = false;
        ShouldOverrideState[team][FormationChar.Power] = false;
        
        LoggingHandler.LogMessage($"Poll Updates Done", taskName, LogLevel.SuperDebug);
    }
}