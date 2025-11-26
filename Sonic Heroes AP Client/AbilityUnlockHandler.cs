

using System.Windows.Forms;

namespace Sonic_Heroes_AP_Client;

public class AbilityUnlockHandler
{

    public Dictionary<Team, Dictionary<FormationChar, List<Ability>>> AbilityListForTeamAndChar = new()
    {
        {
            Team.Sonic, new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        Ability.LightDash,
                        Ability.TriangleJump,
                        Ability.LightAttack,
                        //Ability.Invisibility,
                        //Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        Ability.DummyRings,
                        //Ability.CheeseCannon,
                        //Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                    }
                },
            }
        },
    };
    
    
    public Dictionary<Team, Dictionary<FormationChar, bool>> ShouldOverrideState = Enum.GetValues<Team>().ToDictionary(x => x, x => Enum.GetValues<FormationChar>().ToDictionary(y => y, y => false));
    
    /*
    public readonly Dictionary<Team, Dictionary<FormationChar, bool>> ShouldOverrideState = new ()
    {
        {
            Team.Sonic, new Dictionary<FormationChar, bool>()
            {
                { FormationChar.Speed, false },
                { FormationChar.Flying, false },
                { FormationChar.Power, false },
            }
        },
        {
            Team.Dark, new Dictionary<FormationChar, bool>()
            {
                { FormationChar.Speed, false },
                { FormationChar.Flying, false },
                { FormationChar.Power, false },
            }
        },
        {
            Team.Rose, new Dictionary<FormationChar, bool>()
            {
                { FormationChar.Speed, false },
                { FormationChar.Flying, false },
                { FormationChar.Power, false },
            }
        },
        {
            Team.Chaotix, new Dictionary<FormationChar, bool>()
            {
                { FormationChar.Speed, false },
                { FormationChar.Flying, false },
                { FormationChar.Power, false },
            }
        },
        {
            Team.SuperHardMode, new Dictionary<FormationChar, bool>()
            {
                { FormationChar.Speed, false },
                { FormationChar.Flying, false },
                { FormationChar.Power, false },
            }
        },
    };
    */

    
    public List<Ability> GetAbilitiesForTeam(Team team)
    {
        List<Ability> result = [];
        foreach (var formationChar in Enum.GetValues<FormationChar>())
        {
            foreach (Ability ability in AbilityListForTeamAndChar[team][formationChar])
            {
                result.Add(ability);
            }
        }
        return result;
    }

    public List<Ability> GetAbilitiesForTeamAndChar(Team team, FormationChar formationChar)
    {
        List<Ability> result = [];
        foreach (Ability ability in AbilityListForTeamAndChar[team][formationChar])
        {
            result.Add(ability);
        }
        return result;
    }

    public bool CanTeamBlast(Team team, Region region, bool force)
    {
        if (force)
            return true;
        
        bool hasChars = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed] && Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying] && Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];
        
        var hasAbilities = HasAllAbilitiesForRegion(team, region);
        return hasChars && hasAbilities;
    }


    public void HandleAbilityUnlockCheck(Team team, Region region)
    {
        AbilityHandler.SetHomingAttack(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.HomingAttack]);
        AbilityHandler.SetTornado(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Tornado]);
        AbilityHandler.SetRocketAccel(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.RocketAccel]);
        AbilityHandler.SetLightDash(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.LightDash]);
        AbilityHandler.SetTriangleJump(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.TriangleJump]);
        AbilityHandler.SetLightAttack(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.LightAttack]);
        AbilityHandler.SetAmyHammerHover(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.AmyHammerHover]);
        AbilityHandler.SetInvisibilty(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Invisibility]);
        AbilityHandler.SetShuriken(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Shuriken]);
        AbilityHandler.SetThundershoot(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot]);
        AbilityHandler.SetFlying(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Flight] && Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot]);
        AbilityHandler.SetDummyRings(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.DummyRings]);
        AbilityHandler.SetCheeseCannon(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.CheeseCannon]);
        AbilityHandler.SetFlowerSting(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.FlowerSting]);
        AbilityHandler.SetPowerAttack(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.PowerAttack]);
        AbilityHandler.SetComboFinisher(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.ComboFinisher]);
        AbilityHandler.SetGlide(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Glide]);
        AbilityHandler.SetFireDunk(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.FireDunk]);
        AbilityHandler.SetUltimateFireDunk(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.UltimateFireDunk]);
        AbilityHandler.SetBellyFlop(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.BellyFlop]);
    }
    
    
    public void HandleAbilityUnlockAll(Team team, Region region)
    {
        AbilityHandler.SetHomingAttack(true);
        AbilityHandler.SetTornado(true);
        AbilityHandler.SetRocketAccel(true);
        AbilityHandler.SetLightDash(true);
        AbilityHandler.SetTriangleJump(true);
        AbilityHandler.SetLightAttack(true);
        AbilityHandler.SetAmyHammerHover(true);
        AbilityHandler.SetInvisibilty(true);
        AbilityHandler.SetShuriken(true);
        AbilityHandler.SetThundershoot(true);
        AbilityHandler.SetFlying(true);
        AbilityHandler.SetDummyRings(true);
        AbilityHandler.SetCheeseCannon(true);
        AbilityHandler.SetFlowerSting(true);
        AbilityHandler.SetPowerAttack(true);
        AbilityHandler.SetComboFinisher(true);
        AbilityHandler.SetGlide(true);
        AbilityHandler.SetFireDunk(true);
        AbilityHandler.SetUltimateFireDunk(true);
        AbilityHandler.SetBellyFlop(true);
    }
    

    public void UnlockAbilityForAllRegions(Team team, Ability ability)
    {
        UnlockAbilityForRegion(team, Region.Ocean, ability);
        UnlockAbilityForRegion(team, Region.HotPlant, ability);
        UnlockAbilityForRegion(team, Region.Casino, ability);
        UnlockAbilityForRegion(team, Region.Train, ability);
        UnlockAbilityForRegion(team, Region.BigPlant, ability);
        UnlockAbilityForRegion(team, Region.Ghost, ability);
        UnlockAbilityForRegion(team, Region.Sky, ability);
        //UnlockAbilityForRegion(team, Region.SpecialStage, ability);
        //UnlockAbilityForRegion(team, Region.Boss, ability);
        //UnlockAbilityForRegion(team, Region.FinalBoss, ability);
    }

    public void UnlockAbilityForRegion(Team team, Region region, Ability ability)
    {
        
        Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[(Team)team!].AbilityUnlocks[region][ability] = !Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region][ability];
        PollUpdates();
    }

    public void UnlockAbilityItemCallback(Ability? ability, Team? team, Region? region)
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
    
    

    public void SetCharUnlock(Team team, FormationChar formationChar, bool unlock)
    {
        Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[formationChar] = unlock;
        Console.WriteLine($"Unlocking Team {team} Character {formationChar} with {unlock}");
        ShouldOverrideState[team][formationChar] = true;
        PollUpdates();
        StageObjHandler.HandleObjSpawningWhenReceivingCharItem(team, formationChar, unlock);
    }
    
    
    public bool GetCharUnlock(Team team, FormationChar formationChar)
    {
        return Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[formationChar];
    }

    public bool GetIfLevelGoaled(Team team, LevelId level)
    {
        var locationId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0; //the + 0 is the Act

        if (team is not Team.SuperHard)
            return Mod.ArchipelagoHandler.IsLocationChecked(locationId) ||
                   Mod.ArchipelagoHandler.IsLocationChecked(locationId + 1);
        
        locationId = GameHandler.SuperHardModeId + ((int)level - 2);
        return Mod.ArchipelagoHandler.IsLocationChecked(locationId);

    }
    
    
    public int GetCompletedLevelsForTeam(Team team)
    {
        var levelCompletions = 0;
        try
        {
            foreach (var level in Enum.GetValues<LevelId>().Where(x => x is >= LevelId.SeasideHill and <= LevelId.FinalFortress))
            {
                var locationId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0; //the + 0 is the Act

                if (team is Team.SuperHard)
                {
                    locationId = GameHandler.SuperHardModeId + ((int)level - 2);
                    levelCompletions += Mod.ArchipelagoHandler.IsLocationChecked(locationId) ? 1 : 0;
                    continue;
                }
                
                levelCompletions += Mod.ArchipelagoHandler.IsLocationChecked(locationId) || Mod.ArchipelagoHandler.IsLocationChecked(locationId + 1) ? 1 : 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return levelCompletions;
    }

    public bool HasAllCharsForTeam(Team team)
    {
        return GetCharUnlock(team, FormationChar.Speed) && GetCharUnlock(team, FormationChar.Flying) && GetCharUnlock(team, FormationChar.Power);
    }
    
    public bool HasAllAbilitiesForRegion(Team team, Region region)
    {
        var abilitiesNeeded = 0;
        var abilitiesHave = 0;
        List<Ability> abilities = GetAbilitiesForTeam(team);

        foreach (var ability in abilities)
        {
            abilitiesNeeded += 1;
            abilitiesHave += Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability] ? 1 : 0;
        }
        
        var hasAbilities = abilitiesHave >= abilitiesNeeded;
        return hasAbilities;
    }

    public bool HasAllAbilitiesandCharsandLevelUpsForTeam(Team team)
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

    


    /*
    public string GetLevelSelectUIStringForAbilityUnlocks(Team team, Region region, FormationChar formationChar)
    {
        int currentChecks = 0;
        int maxChecks = 0;

        switch (formationChar)
        {
            case FormationChar.Speed:
                currentChecks = (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].SpeedLevel;
                maxChecks = Enum.GetValues(typeof(SpeedAbility)).Cast<int>().Max();
                break;
            case FormationChar.Flying:
                currentChecks = (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].FlyingLevel;
                maxChecks = Enum.GetValues(typeof(FlyingAbility)).Cast<int>().Max();
                break;
            case FormationChar.Power:
                currentChecks = (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].PowerLevel;
                maxChecks = Enum.GetValues(typeof(PowerAbility)).Cast<int>().Max();
                break;
        }

        if (Mod.ArchipelagoHandler!.SlotData.AbilityUnlocks is AbilityUnlockType.AllRegionsSeparate)
        {
            return $"Progressive {GameHandler.CharacterNames[team][formationChar]} {region} Region: {currentChecks} / {maxChecks}";

        }
        
        return $"Progressive {GameHandler.CharacterNames[team][formationChar]}: {currentChecks} / {maxChecks}";
    }
    
    */

    public string GetLevelSelectUIStringForCharUnlocks(Team team)
    {
        string result = "";

        if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed])
        {
            result += $" {GameHandler.CharacterNames[team][FormationChar.Speed]}";
        }
        if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying])
        {
            result += $" {GameHandler.CharacterNames[team][FormationChar.Flying]}";
        }
        if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power])
        {
            result += $" {GameHandler.CharacterNames[team][FormationChar.Power]}";
        }
        
        return result;
    }
    
    public string GetLevelSelectUIStringForFinalBossCharUnlocks()
    {
        int charsUnlocked = 0;
        int totalCharsNeeded = 0;

        if (Mod.ArchipelagoHandler!.SlotData.StoriesActive[Team.Sonic] > 0)
        {
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].CharsUnlocked[FormationChar.Speed])
                charsUnlocked++;
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].CharsUnlocked[FormationChar.Flying])
                charsUnlocked++;
            if (Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].CharsUnlocked[FormationChar.Power])
                charsUnlocked++;
            
            totalCharsNeeded += 3;
        }
        
        return $"{charsUnlocked} / {totalCharsNeeded}";
    }

    public string GetLevelSelectUIStringForFinalBossLevelCompletionsPerTeam(Team team)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return $"Error On {team.ToString()} Level Completions";
    }


    /*
    public string GetLevelSelectUIStringForFinalBossAbilityUnlocks()
    {
        int abilitiesUnlocked = 0;
        int totalAbiltiesNeeded = 0;

        if (Mod.ArchipelagoHandler.SlotData.AbilityUnlocks is AbilityUnlockType.AllRegionsSeparate)
        {

            foreach (var team in Mod.ArchipelagoHandler.SlotData.StoriesActive.Keys)
            {
                if (Mod.ArchipelagoHandler.SlotData.StoriesActive[team] <= 0) 
                    continue;
                foreach (var region in Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks.Keys.Where(region => region <= Region.Sky))
                {
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].SpeedLevel;
                
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].FlyingLevel;
                
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region].PowerLevel;
                
                    totalAbiltiesNeeded += 9; //3 speed 3 flying 3 power
                }

            }

            //Handle SuperHard Here
            if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 &&
                Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Sonic] is MissionsActive.Act2 or MissionsActive.Both)
            {
                foreach (var region in Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks.Keys.Where(region => region <= Region.Sky))
                {
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[region].SpeedLevel;
                
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[region].FlyingLevel;
                
                    abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[region].PowerLevel;
                
                    totalAbiltiesNeeded += 9;
                }
            }
        }


        else
        {
            
            foreach (var team in Mod.ArchipelagoHandler.SlotData.StoriesActive.Keys)
            {
                if (Mod.ArchipelagoHandler.SlotData.StoriesActive[team] <= 0) 
                    continue;

                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].AbilityUnlocks[Region.Ocean].SpeedLevel;
                
                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].AbilityUnlocks[Region.Ocean].FlyingLevel;
                
                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].AbilityUnlocks[Region.Ocean].PowerLevel;
                
                totalAbiltiesNeeded += 9;
                

            }
            //Handle SuperHard Here
            if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 &&
                Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Sonic] is MissionsActive.Act2 or MissionsActive.Both)
            {
                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[Region.Ocean].SpeedLevel;
                
                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[Region.Ocean].FlyingLevel;
                
                abilitiesUnlocked += (int)Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.SuperHardMode].AbilityUnlocks[Region.Ocean].PowerLevel;
                
                totalAbiltiesNeeded += 9;
            }
        }
        
        return $"{abilitiesUnlocked} / {totalAbiltiesNeeded}";
    }
    */



    public int GetLevelUpForChar(Team team, Region region, FormationChar formationChar)
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
    
    
    public unsafe void HandleLevelUp(Team team, Region region, FormationChar formationChar)
    {
        if (!Mod.GameHandler!.InGame())
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
    
    

    public void PollUpdates()
    {
        if (!Mod.GameHandler!.InGame())
            return;

        if (!ArchipelagoHandler.IsConnected)
        {
            //Console.WriteLine($"Not Connected in PollUpdates. Aborting");
            return;
        }
        
        Team team = Mod.GameHandler.GetCurrentStory();
        Act act = Mod.GameHandler.GetCurrentAct();
        LevelId levelId = Mod.GameHandler.GetCurrentLevel();
        
        //Console.WriteLine($"Running Poll Updates");

        if (!GameHandler.LevelIdToRegion.ContainsKey(levelId))
        {
            Console.WriteLine($"LevelId {levelId} does not exist in Region Mapping");
            return;
        }
        
        Region region = GameHandler.LevelIdToRegion[levelId];
        
        

        if (team is Team.Sonic && act is Act.Act2 or Act.Act3
                               && Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
        {
            team = Team.SuperHard;
            //Console.WriteLine($"Team is Super Hard");
        }
        //Console.WriteLine($"Poll Updates is Updating Game Here");


        bool speedChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Speed];
        bool flyingChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying];
        bool powerChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[team].CharsUnlocked[FormationChar.Power];

        bool forceTeamBlastEnable = false;
        

        if (team is not Team.Sonic || region > Region.Sky)
        {
            //UnlockAll();
            speedChar = true;
            flyingChar = true;
            powerChar = true;

            AbilityHandler.SetCharLevel(FormationChar.Speed, 3);
            AbilityHandler.SetCharLevel(FormationChar.Flying, 3);
            AbilityHandler.SetCharLevel(FormationChar.Power, 3);
            
            ShouldOverrideState[team][FormationChar.Speed] = true;
            ShouldOverrideState[team][FormationChar.Flying] = true;
            ShouldOverrideState[team][FormationChar.Power] = true;
            
            forceTeamBlastEnable = true;
            
            HandleAbilityUnlockAll(team, region);
        }
        else
        {
            AbilityHandler.SetCharLevel(FormationChar.Speed, (byte)GetLevelUpForChar(team, region, FormationChar.Speed));
            AbilityHandler.SetCharLevel(FormationChar.Flying, (byte)GetLevelUpForChar(team, region, FormationChar.Flying));
            AbilityHandler.SetCharLevel(FormationChar.Power, (byte)GetLevelUpForChar(team, region, FormationChar.Power));
            
            HandleAbilityUnlockCheck(team, region);
        }
        
        AbilityHandler.SetCharState(FormationChar.Speed, speedChar, ShouldOverrideState[team][FormationChar.Speed]);
        AbilityHandler.SetCharState(FormationChar.Flying, flyingChar, ShouldOverrideState[team][FormationChar.Flying]);
        AbilityHandler.SetCharState(FormationChar.Power, powerChar, ShouldOverrideState[team][FormationChar.Power]);
        

        if (CanTeamBlast(team, region, forceTeamBlastEnable))
        {
            //Console.WriteLine($"Team Blast is allowed");
            Mod.ArchipelagoHandler!.SlotData.TeamBlastWrite = true;
        }
        else
        {
            //Console.WriteLine($"Team Blast is not allowed");
            Mod.ArchipelagoHandler!.SlotData.TeamBlastWrite = false;
        }
        
        ShouldOverrideState[team][FormationChar.Speed] = false;
        ShouldOverrideState[team][FormationChar.Flying] = false;
        ShouldOverrideState[team][FormationChar.Power] = false;
    }
}
