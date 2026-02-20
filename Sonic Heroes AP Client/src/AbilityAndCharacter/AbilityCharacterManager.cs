using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.AbilityAndCharacter;

public static class AbilityCharacterManager
{
    public static Dictionary<Team, Dictionary<FormationChar, bool>> ShouldOverrideState = Enum.GetValues<Team>().ToDictionary(x => x, _ => Enum.GetValues<FormationChar>().ToDictionary(y => y, _ => false));


    public static void InitConnect()
    {
        try
        {
            //Unlock Jump for enabled teams (and all abilities for regions > sky)
            foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!))
            {
                UnlockAbilityForAllRegions(team, Ability.Jump);
                UnlockAbilityForAllRegions(team, Ability.PowerAttack);
                foreach (var region in Enum.GetValues<Region>().Where(reg => reg > Region.Sky))
                {
                    UnlockAllAbilitiesForRegion(team, region);
                }
            }

            //Unlock speed char for all other teams (to avoid death loop)
            foreach (var team in Enum.GetValues<Team>().Where(x => x is not Team.Sonic))
            {
                SetCharUnlock(team, FormationChar.Speed, true);
            }
            
            
            if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
            {
                //Unlock All Characters and Abilities
                foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!))
                {
                    SetCharUnlock(team, FormationChar.Speed, true);
                    SetCharUnlock(team, FormationChar.Power, true);
                    SetCharUnlock(team, FormationChar.Flying, true);
                    UnlockAllAbilitiesForAllRegionsForTeam(team);
                    HandleAbilityUnlockCheck(team, Region.Ocean, true);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static List<Ability> GetAbilitiesForTeam(Team team, bool shouldIncludeJump = false)
    {
        List<Ability> result = [];
        try
        {
            foreach (var formationChar in Enum.GetValues<FormationChar>())
            {
                result.AddRange(GetAbilitiesForTeamAndChar(team, formationChar, shouldIncludeJump));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }

    
    public static List<Ability> GetAbilitiesForTeamAndChar(Team team, FormationChar formationChar, bool shouldIncludeJump = false)
    {
        List<Ability> result = [];
        result.AddRange(AbilityCharacterDefinitions.AbilityListForTeamAndChar[team][formationChar]);
        if (!shouldIncludeJump)
            result.Remove(Ability.Jump);
        
        return result;
    }

    
    public static bool CanTeamBlast(Team team, Region region)
    {
        var hasChars = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed] && Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying] && Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];
        
        var hasAbilities = HasAllAbilitiesForRegion(team, region);
        return hasChars && hasAbilities;
    }

    public static bool CanFly(Team team, Region region)
    {
        try
        {
            return Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Flight] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
    
    
    
    public static void UnlockAllAbilitiesForAllRegionsForTeam(Team team)
    {
        try
        {
            foreach (var region in Enum.GetValues<Region>())
            {
                UnlockAllAbilitiesForRegion(team, region);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void UnlockAllAbilitiesForRegion(Team team, Region region)
    {
        try
        {
            foreach (var pair in Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region])
            {
                UnlockAbilityForRegion(team, region, pair.Key);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void UnlockAbilityForAllRegions(Team team, Ability ability)
    {
        try
        {
            foreach (var region in Enum.GetValues<Region>())
            {
                UnlockAbilityForRegion(team, region, ability);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static void UnlockAbilityForRegion(Team team, Region region, Ability ability)
    {
        try
        {
            Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region][ability] = true;
            PollUpdates();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    /// <summary>
    /// Sets Player's Ability to Perform Abilities based on Mod Save Data.
    /// Called from Poll Updates
    /// </summary>
    /// <param name="team">Team</param>
    /// <param name="region">Region</param>
    /// <param name="forceunlock">set to true to force unlock ability</param>
    public static void HandleAbilityUnlockCheck(Team team, Region region, bool forceunlock = false)
    {
        try
        {
            //Console.WriteLine($"HandleAbilityUnlockCheck Team: {team} Region: {region} ForceUnlock: {forceunlock}");
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void UnlockAbilityItemCallback(Ability? ability, Team? team, Region? region)
    {
        try
        {
            if (ability is null)
            {
                foreach (var a in Enum.GetValues<Ability>())
                {
                    UnlockAbilityItemCallback(a, team, region);
                }
            }
            else if (team is null)
            {
                foreach (var t in Enum.GetValues<Team>())
                {
                    UnlockAbilityItemCallback(ability, t, region);
                }
            }
        
            else if (region is null)
            {
                foreach (var r in Enum.GetValues<Region>())
                {
                    UnlockAbilityItemCallback(ability, team, r);
                }
            }
            else
            {
                Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[(Team)team].AbilityUnlocks[(Region)region][(Ability)ability] = !Mod.IsDebug || !Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[(Team)team].AbilityUnlocks[(Region)region][(Ability)ability];
                PollUpdates();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static void SetCharUnlock(Team team, FormationChar formationChar, bool unlock)
    {
        Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[formationChar] = unlock;
        Console.WriteLine($"Unlocking Team {team} Character {formationChar} with {unlock}");
        ShouldOverrideState[team][formationChar] = true;
        PollUpdates();
        StageObjHandler.HandleObjSpawningWhenReceivingCharItem(team, formationChar, unlock);
    }
    
    
    public static bool GetCharUnlock(Team team, FormationChar formationChar)
    {
        return Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates || Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[formationChar];
    }
    
    
    public static bool HasAllCharsForTeam(Team team)
    {
        return GetCharUnlock(team, FormationChar.Speed) && GetCharUnlock(team, FormationChar.Flying) && GetCharUnlock(team, FormationChar.Power);
    }
    
    
    public static bool HasAllAbilitiesForRegion(Team team, Region region)
    {
        var abilitiesNeeded = 0;
        var abilitiesHave = 0;
        List<Ability> abilities = GetAbilitiesForTeam(team, true);

        foreach (var ability in abilities)
        {
            abilitiesNeeded += 1;
            abilitiesHave += Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability] ? 1 : 0;
        }
        
        var hasAbilities = abilitiesHave >= abilitiesNeeded;
        return hasAbilities;
    }

    
    public static bool HasAllAbilitiesandCharsandLevelUpsForTeam(Team team)
    {
        if (!HasAllCharsForTeam(team))
        {
            //Console.WriteLine($"Final Boss Requires All Characters");
            return false;
        }

        foreach (var reg in Enum.GetValues<Region>().Where(reg => reg <= Region.Sky))
        {
            if (!HasAllAbilitiesForRegion(team, reg))
            {
                //Console.WriteLine($"Final Boss Requires All Abilities for Team {team} and Region {reg}");
                return false;
            }
        }
        return true;
    }
    

    public static string GetLevelSelectUIStringForCharUnlocksForTeam(Team team)
    {
        var result = "";
        try
        {
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed])
            {
                result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Speed]}";
            }
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power])
            {
                result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Power]}";
            }
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying])
            {
                result += $" {SonicHeroesDefinitions.CharacterNames[team][FormationChar.Flying]}";
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }


    public static string GetLevelSelectUIStringForCharUnlocksForSonicSuperHard()
    {
        var result = "";
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }

    public static string GetLevelSelectUIStringForCharUnlocksForFinalBoss()
    {
        var result = "";
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }
    
    
    public static Dictionary<Team, List<int>> GetLevelSelectUIFinalBossCharUnlocks()
    {
        var result = new Dictionary<Team, List<int>>();
        try
        {
            var charsUnlocked = 0;
            var totalCharsNeeded = 0;
        
            foreach (Team team in Enum.GetValues<Team>())
            {
                if (!(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team)!) 
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return result;
    }
    
    
    public static int GetLevelUpForChar(Team team, Region region, FormationChar formationChar)
    {
        List<Ability> abilities = GetAbilitiesForTeamAndChar(team, formationChar);
        var abilitiesNeeded = abilities.Count;
        var abilitiesHave = abilities.Count(ability => Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability]);

        if (abilitiesHave >= abilitiesNeeded)
            return 3;

        if (abilitiesHave >= abilitiesNeeded / 2.0)
            return 2;

        return abilitiesHave >= 1 ? 1 : 0;
    }
    
    
    public static unsafe void HandleLevelUp(Team team, Region region, FormationChar formationChar)
    {
        if (!GameStateHandler.InGame())
            return;

        if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
            return;
        
        var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
        var charlevels = (byte*)(baseAddress + 0x208 + (byte)formationChar);
        
        switch (formationChar)
        {
            case FormationChar.Speed:
            {
                var speedMax = GetLevelUpForChar(team, region, FormationChar.Speed);

                if (*charlevels > speedMax)
                {
                    Console.WriteLine($"Level Up for Character {formationChar} is over max allowed value of {speedMax}");
                    *charlevels = (byte)speedMax;
                }

                break;
            }
            case FormationChar.Flying:
            {
                var flyingMax = GetLevelUpForChar(team, region, FormationChar.Flying);

                if (*charlevels > flyingMax)
                {
                    Console.WriteLine($"Level Up for Character {formationChar} is over max allowed value of {flyingMax}");
                    *charlevels = (byte)flyingMax;
                }

                break;
            }
            case FormationChar.Power:
            {
                var powerMax = GetLevelUpForChar(team, region, FormationChar.Power);

                if (*charlevels > powerMax)
                {
                    Console.WriteLine($"Level Up for Character {formationChar} is over max allowed value of {powerMax}");
                    *charlevels = (byte)powerMax;
                }
                break;
            }
        }
    }
    
    public static void PollUpdates()
    {
        try
        {
            if (!GameStateHandler.InGame())
                return;

            if (!ArchipelagoHandler.IsConnected)
            {
                //Console.WriteLine($"Not Connected in PollUpdates. Aborting");
                return;
            }

            if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
                return;

            Team? tempTeam = GameStateHandler.GetCurrentStory();
            Act? tempAct = GameStateHandler.GetCurrentAct();
            LevelId? tempLevel = GameStateHandler.GetCurrentLevel();

            if (tempTeam == null || tempAct == null || tempLevel == null)
            {
                Console.WriteLine($"Team: {tempTeam} Act: {tempAct} Level: {tempLevel}. One is null in PollUpdates().");
                return;
            }
            
            Team team = (Team)tempTeam;
            Act act = (Act)tempAct;
            LevelId levelId = (LevelId)tempLevel;
            
            //Console.WriteLine($"Running Poll Updates");

            if (!SonicHeroesDefinitions.LevelIdToRegion.ContainsKey(levelId))
            {
                Console.WriteLine($"LevelId {levelId} does not exist in Region Mapping");
                return;
            }
            
            Region region = SonicHeroesDefinitions.LevelIdToRegion[levelId];
            
            //Console.WriteLine($"Poll Updates is Updating Game Here");


            bool speedChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed];
            bool flyingChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying];
            bool powerChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];

            bool forceTeamBlastEnable = false;
            
            AbilityCharacterGameWrites.SetCharLevel(FormationChar.Speed, (byte)GetLevelUpForChar(team, region, FormationChar.Speed));
            AbilityCharacterGameWrites.SetCharLevel(FormationChar.Flying, (byte)GetLevelUpForChar(team, region, FormationChar.Flying));
            AbilityCharacterGameWrites.SetCharLevel(FormationChar.Power, (byte)GetLevelUpForChar(team, region, FormationChar.Power));
            
            HandleAbilityUnlockCheck(team, region);
            
            
            AbilityCharacterGameWrites.SetCharState(FormationChar.Speed, speedChar, ShouldOverrideState[team][FormationChar.Speed]);
            AbilityCharacterGameWrites.SetCharState(FormationChar.Flying, flyingChar, ShouldOverrideState[team][FormationChar.Flying]);
            AbilityCharacterGameWrites.SetCharState(FormationChar.Power, powerChar, ShouldOverrideState[team][FormationChar.Power]);
            

            if (forceTeamBlastEnable || CanTeamBlast(team, region))
            {
                //Console.WriteLine($"Team Blast is allowed");
                AbilityCharacterGameWrites.SetTeamBlastWrite(true);
            }
            else
            {
                //Console.WriteLine($"Team Blast is not allowed");
                AbilityCharacterGameWrites.SetTeamBlastWrite(false);
            }
            
            ShouldOverrideState[team][FormationChar.Speed] = false;
            ShouldOverrideState[team][FormationChar.Flying] = false;
            ShouldOverrideState[team][FormationChar.Power] = false;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}