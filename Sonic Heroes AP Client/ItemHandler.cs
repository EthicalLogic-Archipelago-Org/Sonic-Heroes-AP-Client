using System.Text.RegularExpressions;
using Archipelago.MultiClient.Net.Models;

namespace Sonic_Heroes_AP_Client;

public enum SHItem
{
    Emblem,
    GreenChaosEmerald,
    BlueChaosEmerald,
    YellowChaosEmerald,
    WhiteChaosEmerald,
    CyanChaosEmerald,
    PurpleChaosEmerald,
    RedChaosEmerald,
    PlayableSonic,
    PlayableTails,
    PlayableKnuckles,
    PlayableShadow,
    PlayableRouge,
    PlayableOmega,
    PlayableAmy,
    PlayableCream,
    PlayableBig,
    PlayableEspio,
    PlayableCharmy,
    PlayableVector,
    PlayableSuperHardSonic,
    PlayableSuperHardTails,
    PlayableSuperHardKnuckles,
    
    AnyTeamHomingAttackAllRegions = 0x200,
    AnyTeamTornadoAllRegions,
    AnyTeamRocketAccelAllRegions,
    AnyTeamLightDashAllRegions,
    AnyTeamTriangleJumpAllRegions,
    AnyTeamLightAttackAllRegions,
    AnyTeamAmyHammerHoverAllRegions,
    AnyTeamInvisibilityAllRegions,
    AnyTeamShurikenAllRegions,
    AnyTeamThundershootAllRegions,
    AnyTeamFlightAllRegions,
    AnyTeamDummyRingsAllRegions,
    AnyTeamCheeseCannonAllRegions,
    AnyTeamFlowerStingAllRegions,
    AnyTeamPowerAttackAllRegions,
    AnyTeamComboFinisherAllRegions,
    AnyTeamGlideAllRegions,
    AnyTeamFireDunkAllRegions,
    AnyTeamBellyFlopAllRegions,
    AnyTeamAllAbilitiesAllRegions,
    
    AnyTeamHomingAttackOceanRegion = 0x220,
    AnyTeamTornadoOceanRegion,
    AnyTeamRocketAccelOceanRegion,
    AnyTeamLightDashOceanRegion,
    AnyTeamTriangleJumpOceanRegion,
    AnyTeamLightAttackOceanRegion,
    AnyTeamAmyHammerHoverOceanRegion,
    AnyTeamInvisibilityOceanRegion,
    AnyTeamShurikenOceanRegion,
    AnyTeamThundershootOceanRegion,
    AnyTeamFlightOceanRegion,
    AnyTeamDummyRingsOceanRegion,
    AnyTeamCheeseCannonOceanRegion,
    AnyTeamFlowerStingOceanRegion,
    AnyTeamPowerAttackOceanRegion,
    AnyTeamComboFinisherOceanRegion,
    AnyTeamGlideOceanRegion,
    AnyTeamFireDunkOceanRegion,
    AnyTeamBellyFlopOceanRegion,
    
    AnyTeamHomingAttackHotPlantRegion = 0x240,
    AnyTeamTornadoHotPlantRegion,
    AnyTeamRocketAccelHotPlantRegion,
    AnyTeamLightDashHotPlantRegion,
    AnyTeamTriangleJumpHotPlantRegion,
    AnyTeamLightAttackHotPlantRegion,
    AnyTeamAmyHammerHoverHotPlantRegion,
    AnyTeamInvisibilityHotPlantRegion,
    AnyTeamShurikenHotPlantRegion,
    AnyTeamThundershootHotPlantRegion,
    AnyTeamFlightHotPlantRegion,
    AnyTeamDummyRingsHotPlantRegion,
    AnyTeamCheeseCannonHotPlantRegion,
    AnyTeamFlowerStingHotPlantRegion,
    AnyTeamPowerAttackHotPlantRegion,
    AnyTeamComboFinisherHotPlantRegion,
    AnyTeamGlideHotPlantRegion,
    AnyTeamFireDunkHotPlantRegion,
    AnyTeamBellyFlopHotPlantRegion,
    
    AnyTeamHomingAttackCasinoRegion = 0x260,
    AnyTeamTornadoCasinoRegion,
    AnyTeamRocketAccelCasinoRegion,
    AnyTeamLightDashCasinoRegion,
    AnyTeamTriangleJumpCasinoRegion,
    AnyTeamLightAttackCasinoRegion,
    AnyTeamAmyHammerHoverCasinoRegion,
    AnyTeamInvisibilityCasinoRegion,
    AnyTeamShurikenCasinoRegion,
    AnyTeamThundershootCasinoRegion,
    AnyTeamFlightCasinoRegion,
    AnyTeamDummyRingsCasinoRegion,
    AnyTeamCheeseCannonCasinoRegion,
    AnyTeamFlowerStingCasinoRegion,
    AnyTeamPowerAttackCasinoRegion,
    AnyTeamComboFinisherCasinoRegion,
    AnyTeamGlideCasinoRegion,
    AnyTeamFireDunkCasinoRegion,
    AnyTeamBellyFlopCasinoRegion,
    
    AnyTeamHomingAttackTrainRegion = 0x280,
    AnyTeamTornadoTrainRegion,
    AnyTeamRocketAccelTrainRegion,
    AnyTeamLightDashTrainRegion,
    AnyTeamTriangleJumpTrainRegion,
    AnyTeamLightAttackTrainRegion,
    AnyTeamAmyHammerHoverTrainRegion,
    AnyTeamInvisibilityTrainRegion,
    AnyTeamShurikenTrainRegion,
    AnyTeamThundershootTrainRegion,
    AnyTeamFlightTrainRegion,
    AnyTeamDummyRingsTrainRegion,
    AnyTeamCheeseCannonTrainRegion,
    AnyTeamFlowerStingTrainRegion,
    AnyTeamPowerAttackTrainRegion,
    AnyTeamComboFinisherTrainRegion,
    AnyTeamGlideTrainRegion,
    AnyTeamFireDunkTrainRegion,
    AnyTeamBellyFlopTrainRegion,
    
    AnyTeamHomingAttackBigPlantRegion = 0x2A0,
    AnyTeamTornadoBigPlantRegion,
    AnyTeamRocketAccelBigPlantRegion,
    AnyTeamLightDashBigPlantRegion,
    AnyTeamTriangleJumpBigPlantRegion,
    AnyTeamLightAttackBigPlantRegion,
    AnyTeamAmyHammerHoverBigPlantRegion,
    AnyTeamInvisibilityBigPlantRegion,
    AnyTeamShurikenBigPlantRegion,
    AnyTeamThundershootBigPlantRegion,
    AnyTeamFlightBigPlantRegion,
    AnyTeamDummyRingsBigPlantRegion,
    AnyTeamCheeseCannonBigPlantRegion,
    AnyTeamFlowerStingBigPlantRegion,
    AnyTeamPowerAttackBigPlantRegion,
    AnyTeamComboFinisherBigPlantRegion,
    AnyTeamGlideBigPlantRegion,
    AnyTeamFireDunkBigPlantRegion,
    AnyTeamBellyFlopBigPlantRegion,
    
    AnyTeamHomingAttackGhostRegion = 0x2C0,
    AnyTeamTornadoGhostRegion,
    AnyTeamRocketAccelGhostRegion,
    AnyTeamLightDashGhostRegion,
    AnyTeamTriangleJumpGhostRegion,
    AnyTeamLightAttackGhostRegion,
    AnyTeamAmyHammerHoverGhostRegion,
    AnyTeamInvisibilityGhostRegion,
    AnyTeamShurikenGhostRegion,
    AnyTeamThundershootGhostRegion,
    AnyTeamFlightGhostRegion,
    AnyTeamDummyRingsGhostRegion,
    AnyTeamCheeseCannonGhostRegion,
    AnyTeamFlowerStingGhostRegion,
    AnyTeamPowerAttackGhostRegion,
    AnyTeamComboFinisherGhostRegion,
    AnyTeamGlideGhostRegion,
    AnyTeamFireDunkGhostRegion,
    AnyTeamBellyFlopGhostRegion,
    
    AnyTeamHomingAttackSkyRegion = 0x2E0,
    AnyTeamTornadoSkyRegion,
    AnyTeamRocketAccelSkyRegion,
    AnyTeamLightDashSkyRegion,
    AnyTeamTriangleJumpSkyRegion,
    AnyTeamLightAttackSkyRegion,
    AnyTeamAmyHammerHoverSkyRegion,
    AnyTeamInvisibilitySkyRegion,
    AnyTeamShurikenSkyRegion,
    AnyTeamThundershootSkyRegion,
    AnyTeamFlightSkyRegion,
    AnyTeamDummyRingsSkyRegion,
    AnyTeamCheeseCannonSkyRegion,
    AnyTeamFlowerStingSkyRegion,
    AnyTeamPowerAttackSkyRegion,
    AnyTeamComboFinisherSkyRegion,
    AnyTeamGlideSkyRegion,
    AnyTeamFireDunkSkyRegion,
    AnyTeamBellyFlopSkyRegion,
    
    SonicHomingAttackAllRegions = 0x400,
    SonicTornadoAllRegions,
    SonicRocketAccelAllRegions,
    SonicLightDashAllRegions,
    SonicTriangleJumpAllRegions,
    SonicLightAttackAllRegions,
    SonicAmyHammerHoverAllRegions,
    SonicInvisibilityAllRegions,
    SonicShurikenAllRegions,
    SonicThundershootAllRegions,
    SonicFlightAllRegions,
    SonicDummyRingsAllRegions,
    SonicCheeseCannonAllRegions,
    SonicFlowerStingAllRegions,
    SonicPowerAttackAllRegions,
    SonicComboFinisherAllRegions,
    SonicGlideAllRegions,
    SonicFireDunkAllRegions,
    SonicBellyFlopAllRegions,
    
    SonicHomingAttackOceanRegion = 0x420,
    SonicTornadoOceanRegion,
    SonicRocketAccelOceanRegion,
    SonicLightDashOceanRegion,
    SonicTriangleJumpOceanRegion,
    SonicLightAttackOceanRegion,
    SonicAmyHammerHoverOceanRegion,
    SonicInvisibilityOceanRegion,
    SonicShurikenOceanRegion,
    SonicThundershootOceanRegion,
    SonicFlightOceanRegion,
    SonicDummyRingsOceanRegion,
    SonicCheeseCannonOceanRegion,
    SonicFlowerStingOceanRegion,
    SonicPowerAttackOceanRegion,
    SonicComboFinisherOceanRegion,
    SonicGlideOceanRegion,
    SonicFireDunkOceanRegion,
    SonicBellyFlopOceanRegion,
    
    SonicHomingAttackHotPlantRegion = 0x440,
    SonicTornadoHotPlantRegion,
    SonicRocketAccelHotPlantRegion,
    SonicLightDashHotPlantRegion,
    SonicTriangleJumpHotPlantRegion,
    SonicLightAttackHotPlantRegion,
    SonicAmyHammerHoverHotPlantRegion,
    SonicInvisibilityHotPlantRegion,
    SonicShurikenHotPlantRegion,
    SonicThundershootHotPlantRegion,
    SonicFlightHotPlantRegion,
    SonicDummyRingsHotPlantRegion,
    SonicCheeseCannonHotPlantRegion,
    SonicFlowerStingHotPlantRegion,
    SonicPowerAttackHotPlantRegion,
    SonicComboFinisherHotPlantRegion,
    SonicGlideHotPlantRegion,
    SonicFireDunkHotPlantRegion,
    SonicBellyFlopHotPlantRegion,
    
    SonicHomingAttackCasinoRegion = 0x460,
    SonicTornadoCasinoRegion,
    SonicRocketAccelCasinoRegion,
    SonicLightDashCasinoRegion,
    SonicTriangleJumpCasinoRegion,
    SonicLightAttackCasinoRegion,
    SonicAmyHammerHoverCasinoRegion,
    SonicInvisibilityCasinoRegion,
    SonicShurikenCasinoRegion,
    SonicThundershootCasinoRegion,
    SonicFlightCasinoRegion,
    SonicDummyRingsCasinoRegion,
    SonicCheeseCannonCasinoRegion,
    SonicFlowerStingCasinoRegion,
    SonicPowerAttackCasinoRegion,
    SonicComboFinisherCasinoRegion,
    SonicGlideCasinoRegion,
    SonicFireDunkCasinoRegion,
    SonicBellyFlopCasinoRegion,
    
    SonicHomingAttackTrainRegion = 0x480,
    SonicTornadoTrainRegion,
    SonicRocketAccelTrainRegion,
    SonicLightDashTrainRegion,
    SonicTriangleJumpTrainRegion,
    SonicLightAttackTrainRegion,
    SonicAmyHammerHoverTrainRegion,
    SonicInvisibilityTrainRegion,
    SonicShurikenTrainRegion,
    SonicThundershootTrainRegion,
    SonicFlightTrainRegion,
    SonicDummyRingsTrainRegion,
    SonicCheeseCannonTrainRegion,
    SonicFlowerStingTrainRegion,
    SonicPowerAttackTrainRegion,
    SonicComboFinisherTrainRegion,
    SonicGlideTrainRegion,
    SonicFireDunkTrainRegion,
    SonicBellyFlopTrainRegion,
    
    SonicHomingAttackBigPlantRegion = 0x4A0,
    SonicTornadoBigPlantRegion,
    SonicRocketAccelBigPlantRegion,
    SonicLightDashBigPlantRegion,
    SonicTriangleJumpBigPlantRegion,
    SonicLightAttackBigPlantRegion,
    SonicAmyHammerHoverBigPlantRegion,
    SonicInvisibilityBigPlantRegion,
    SonicShurikenBigPlantRegion,
    SonicThundershootBigPlantRegion,
    SonicFlightBigPlantRegion,
    SonicDummyRingsBigPlantRegion,
    SonicCheeseCannonBigPlantRegion,
    SonicFlowerStingBigPlantRegion,
    SonicPowerAttackBigPlantRegion,
    SonicComboFinisherBigPlantRegion,
    SonicGlideBigPlantRegion,
    SonicFireDunkBigPlantRegion,
    SonicBellyFlopBigPlantRegion,
    
    SonicHomingAttackGhostRegion = 0x4C0,
    SonicTornadoGhostRegion,
    SonicRocketAccelGhostRegion,
    SonicLightDashGhostRegion,
    SonicTriangleJumpGhostRegion,
    SonicLightAttackGhostRegion,
    SonicAmyHammerHoverGhostRegion,
    SonicInvisibilityGhostRegion,
    SonicShurikenGhostRegion,
    SonicThundershootGhostRegion,
    SonicFlightGhostRegion,
    SonicDummyRingsGhostRegion,
    SonicCheeseCannonGhostRegion,
    SonicFlowerStingGhostRegion,
    SonicPowerAttackGhostRegion,
    SonicComboFinisherGhostRegion,
    SonicGlideGhostRegion,
    SonicFireDunkGhostRegion,
    SonicBellyFlopGhostRegion,
    
    SonicHomingAttackSkyRegion = 0x4E0,
    SonicTornadoSkyRegion,
    SonicRocketAccelSkyRegion,
    SonicLightDashSkyRegion,
    SonicTriangleJumpSkyRegion,
    SonicLightAttackSkyRegion,
    SonicAmyHammerHoverSkyRegion,
    SonicInvisibilitySkyRegion,
    SonicShurikenSkyRegion,
    SonicThundershootSkyRegion,
    SonicFlightSkyRegion,
    SonicDummyRingsSkyRegion,
    SonicCheeseCannonSkyRegion,
    SonicFlowerStingSkyRegion,
    SonicPowerAttackSkyRegion,
    SonicComboFinisherSkyRegion,
    SonicGlideSkyRegion,
    SonicFireDunkSkyRegion,
    SonicBellyFlopSkyRegion,
    
    
    
    //Currently Not Using these
    //ProgressiveLevelUpSonic = 0x1F0,
    //ProgressiveLevelUpTails,
    //ProgressiveLevelUpKnuckles,
    
    
    
    ExtraLife = 0x8000,
    FiveRings,
    TenRings,
    TwentyRings,
    Shield,
    Invincibility,
    SpeedLevelUp,
    FlyLevelUp,
    PowerLevelUp,
    TeamLevelUp,
    TeamBlastFiller,
    
    StealthTrap = 0x8100,
    FreezeTrap,
    NoSwapTrap,
    RingTrap,
    CharmyTrap,
}



public class ItemHandler
{
    private readonly Queue<SHItem> cachedItems;
    
    //private static Dictionary<string, Enum> ItemStrEnumMatches = new Dictionary<string, Enum>();
    
    private static Dictionary<string, Team> TeamStrToEnum = new();
    private static Dictionary<string, Region> RegionStrToEnum = new();
    private static Dictionary<string, Ability> AbilityStrToEnum = new();
    private static Dictionary<string, PlayableCharacter> PlayableCharStrToEnum = new();
    private static Dictionary<string, Emerald> EmeraldStrToEnum = new();
    
    
    public ItemHandler()
    {
        cachedItems = new Queue<SHItem>();    
        Enum.GetValues<Team>().ToList().ForEach(t => TeamStrToEnum[t.ToString()] = t);
        Enum.GetValues<Region>().ToList().ForEach(t => RegionStrToEnum[t.ToString()] = t);
        Enum.GetValues<Ability>().ToList().ForEach(t => AbilityStrToEnum[t.ToString()] = t);
        Enum.GetValues<PlayableCharacter>().ToList().ForEach(t => PlayableCharStrToEnum[t.ToString()] = t);
        Enum.GetValues<Emerald>().ToList().ForEach(t => EmeraldStrToEnum[t.ToString()] = t);
    }
    
    //public unsafe void HandleItem(int index, NetworkItem item)
    public void HandleItem(int index, ItemInfo item)
    {

        //Console.WriteLine($"HandleSHItem {index}, {Mod.SaveDataHandler!.CustomSaveData!.LastItemIndex} :: {(SHItem)(item.Item - 0x93930000)} : 0x{item.Item:X}");
        
        //use this print below
        //Console.WriteLine($"HandleSHItem {index}, {Mod.SaveDataHandler!.CustomSaveData!.LastItemIndex} :: {item.ItemName} : 0x{item.ItemId:X}");
        
        
        if (index < Mod.SaveDataHandler!.CustomSaveData.LastItemIndex)
            return;

        //Console.WriteLine($"{index} : {Mod.SaveDataHandler.CustomData->LastItemIndex}");
        Mod.SaveDataHandler!.CustomSaveData!.LastItemIndex++;
        
        //Console.WriteLine($"Item received: {item.ItemName}");
        
        var handled = false;
        //var itemName = (SHItem)(item.Item - 0x93930000);
        //var itemId = (SHItem)(item.Item - 0x93930000);
        
        var itemName = item.ItemName;
        var itemId = (SHItem)(item.ItemId - 0x93930000);
        bool unlocked = false;
        
        CheckPlayableCharItemName(itemName, ref handled);
        CheckEmeraldItemName(itemName, ref handled);
        CheckEmblemItemName(itemName, ref handled);
        CheckAbilityItemName(itemName, ref handled);
        CheckStageObjItemName(itemName, ref handled);
            
        //Emblem
        //Green Chaos Emerald
        
        //Playable Sonic
        //Playable SuperHard Sonic
            
        //Homing Attack
        //Homing Attack Ocean Region
        //Sonic Homing Attack
        //Sonic Homing Attack Ocean Region
        
        //Super Hard Homing Attack Ocean Region
        
        
        /*
        switch (itemId)
        {
            case SHItem.Emblem:
                Mod.SaveDataHandler!.CustomSaveData.Emblems++;
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE016);
                break;
            case SHItem.GreenChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Green] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[3] = 1;
                break;
            case SHItem.BlueChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Blue] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[6] = 1;
                break;
            case SHItem.YellowChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Yellow] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[9] = 1;
                break;
            case SHItem.WhiteChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.White] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[12] = 1;
                break;
            case SHItem.CyanChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Cyan] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[15] = 1;
                break;
            case SHItem.PurpleChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Purple] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[18] = 1;
                break;
            case SHItem.RedChaosEmerald:
                Mod.SaveDataHandler!.CustomSaveData.Emeralds[Emerald.Red] = true;
                Mod.SaveDataHandler!.RedirectData->Emerald[21] = 1;
                break;
            case SHItem.PlayableSonic:
                unlocked = Mod.AbilityUnlockHandler!.GetCharUnlock(Team.Sonic, FormationChar.Speed);
                Mod.AbilityUnlockHandler!.SetCharUnlock(Team.Sonic, FormationChar.Speed, !unlocked);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.PlayableTails:
                unlocked = Mod.AbilityUnlockHandler!.GetCharUnlock(Team.Sonic, FormationChar.Flying);
                Mod.AbilityUnlockHandler!.SetCharUnlock(Team.Sonic, FormationChar.Flying, !unlocked);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.PlayableKnuckles:
                unlocked = Mod.AbilityUnlockHandler!.GetCharUnlock(Team.Sonic, FormationChar.Power);
                Mod.AbilityUnlockHandler!.SetCharUnlock(Team.Sonic, FormationChar.Power, !unlocked);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            
            
            
            
            
            
            case SHItem.SonicHomingAttackAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopAllRegions:
                Mod.AbilityUnlockHandler.UnlockAbilityForAllRegions(Team.Sonic, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopOceanRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ocean, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopHotPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.HotPlant, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopCasinoRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Casino, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopTrainRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Train, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopBigPlantRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.BigPlant, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilityGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopGhostRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Ghost, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicHomingAttackSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.HomingAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTornadoSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Tornado);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicRocketAccelSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.RocketAccel);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightDashSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.LightDash);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicTriangleJumpSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.TriangleJump);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicLightAttackSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.LightAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicAmyHammerHoverSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.AmyHammerHover);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicInvisibilitySkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Invisibility);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicShurikenSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Shuriken);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicThundershootSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Thundershoot);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlightSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Flight);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicDummyRingsSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.DummyRings);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicCheeseCannonSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.CheeseCannon);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFlowerStingSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.FlowerSting);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicPowerAttackSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.PowerAttack);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicComboFinisherSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.ComboFinisher);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicGlideSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.Glide);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicFireDunkSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.FireDunk);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            case SHItem.SonicBellyFlopSkyRegion:
                Mod.AbilityUnlockHandler.UnlockAbilityForRegion(Team.Sonic, Region.Sky, Ability.BellyFlop);
                Console.WriteLine($"Got Item: {itemName}");
                break;
            
            
            default:
                handled = false;
                break;
        }
        */

        if (handled && Mod.ArchipelagoHandler.SlotData != null)
            Mod.ArchipelagoHandler.SlotData.RecalculateOpenLevels();
        Mod.ArchipelagoHandler!.Save();
        
        if (!Mod.GameHandler.InGame())
        {
            cachedItems.Enqueue(itemId);
            return;
        }
        
        HandleInGameItem(itemId);
    }


    public void HandleInGameItem(SHItem itemId)
    {
        switch (itemId)
        {
            case SHItem.ExtraLife:
                GameHandler.ModifyLives((int)Mod.ModuleBase, 1);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1034);
                break;
            case SHItem.FiveRings:
                Mod.GameHandler?.SetRingCount(Mod.GameHandler.GetRingCount() + 5);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case SHItem.TenRings:
                Mod.GameHandler?.SetRingCount(Mod.GameHandler.GetRingCount() + 10);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case SHItem.TwentyRings:
                Mod.GameHandler?.SetRingCount(Mod.GameHandler.GetRingCount() + 20);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case SHItem.Shield:
                GameHandler.GiveShield((int)Mod.ModuleBase);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1036);
                break;
            case SHItem.SpeedLevelUp:
                //GameHandler.GiveLevelUp(LevelUpType.Speed);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case SHItem.PowerLevelUp:
                //GameHandler.GiveLevelUp(LevelUpType.Power);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case SHItem.FlyLevelUp:
                //GameHandler.GiveLevelUp(LevelUpType.Flying);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case SHItem.TeamLevelUp: 
                //GameHandler.GiveLevelUp(LevelUpType.Speed);
                //GameHandler.GiveLevelUp(LevelUpType.Power);
                //GameHandler.GiveLevelUp(LevelUpType.Flying);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case SHItem.TeamBlastFiller:
                try
                {
                    GameHandler.HandleTeamBlastFiller();
                    var team = Mod.GameHandler.GetCurrentStory();
                    var level = Mod.GameHandler.GetCurrentLevel();
                    var act = Mod.GameHandler.GetCurrentAct();

                    if (team is Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && act is Act.Act2 or Act.Act3)
                        team = Team.SuperHard;
                    

                    if (!GameHandler.LevelIdToRegion.TryGetValue(level, out Region region))
                        break;

                    if (!Mod.AbilityUnlockHandler.CanTeamBlast(team, region, false))
                    {
                        Mod.GameHandler?.SetRingCount(Mod.GameHandler.GetRingCount() + 10);
                        if (Mod.Configuration!.PlaySounds)
                            SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                        break;
                    }
                    Mod.GameHandler?.SetRingCount(Mod.GameHandler.GetRingCount() + 1);
                    if (Mod.Configuration!.PlaySounds)
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1004);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case SHItem.StealthTrap:
                Mod.TrapHandler?.HandleStealthTrap();
                break;
            case SHItem.FreezeTrap:
                Mod.TrapHandler?.HandleFreezeTrap();
                break;
            case SHItem.NoSwapTrap:
                Mod.TrapHandler?.HandleNoSwapTrap();
                break;
            case SHItem.RingTrap:
                Mod.GameHandler!.SetRingCount(Math.Max(0, Mod.GameHandler.GetRingCount() - 50));
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1005);
                Mod.ArchipelagoHandler.SendRing(-50);
                break;
            case SHItem.CharmyTrap:
                Mod.TrapHandler?.HandleCharmyTrap();
                break;
            default:
                break;
        }
    }

    public void HandleCachedItems()
    {
        while (cachedItems.Count > 0)
        {
            var item = cachedItems.Dequeue();
            HandleInGameItem(item);
        }
    }


    public void CheckPlayableCharItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            foreach (var pair in PlayableCharStrToEnum)
            {
                if (itemName.ToLower().Replace(" ", "").Contains(pair.Key.ToLower()))
                {
                    //match here
                    var team = GameHandler.PlayableCharToTeam[pair.Value];
                    var formation = GameHandler.PlayableCharToFormation[pair.Value];
                    var unlocked = Mod.AbilityUnlockHandler!.GetCharUnlock(team, formation);
                    if (!Mod.IsDebug)
                        unlocked = false;
                    Mod.AbilityUnlockHandler!.SetCharUnlock(team, formation, !unlocked);
                    Console.WriteLine($"Got Item: {itemName}");
                    handled = true;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public unsafe void CheckEmeraldItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            foreach (var pair in EmeraldStrToEnum)
            {
                if (itemName.ToLower().Replace(" ", "").Contains($"{pair.Key}ChaosEmerald".ToLower()))
                {
                    Mod.SaveDataHandler!.CustomSaveData.Emeralds[pair.Value] = true;
                    Mod.SaveDataHandler!.RedirectData->Emerald[((int)pair.Value + 1) * 3] = 1;
                    Console.WriteLine($"Got Item: {itemName}");
                    handled = true;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    public void CheckEmblemItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            if (!itemName.Contains("Emblem")) 
                return;
            Mod.SaveDataHandler!.CustomSaveData!.Emblems++;
            Console.WriteLine($"Got Item: {itemName}");
            if (Mod.Configuration!.PlaySounds)
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE016);
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void CheckAbilityItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;

            if (itemName.ToLower().Contains("all abilities"))
            {
                var team = CheckTeamItemName(itemName, ref handled);
                var region = CheckRegionItemName(itemName, ref handled);
                Mod.AbilityUnlockHandler!.UnlockAbilityItemCallback(null, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
            
            foreach (var pair in AbilityStrToEnum)
            {
                if (!itemName.ToLower().Replace(" ", "").Contains(pair.Key.ToLower())) 
                    continue;
                var team = CheckTeamItemName(itemName, ref handled);
                var region = CheckRegionItemName(itemName, ref handled);
                Mod.AbilityUnlockHandler!.UnlockAbilityItemCallback(pair.Value, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void CheckStageObjItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            
            if (itemName.ToLower().Contains("all stage objects"))
            {
                var team = CheckTeamItemName(itemName, ref handled);
                var region = CheckRegionItemName(itemName, ref handled);
                Mod.StageObjHandler!.UnlockStageObjItemCallback(null, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
            
            foreach (var stageObj in StageObjHandler.StageObjsToMessWith)
            {
                if (!itemName.ToLower().Replace(" ", "").Contains(stageObj.ToString().ToLower())) 
                    continue;
                var team = CheckTeamItemName(itemName, ref handled);
                var region = CheckRegionItemName(itemName, ref handled);
                Mod.StageObjHandler!.UnlockStageObjItemCallback(stageObj, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public Team? CheckTeamItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return null;
            foreach (var pair in TeamStrToEnum)
            {
                if (itemName.ToLower().Replace(" ", "").Contains(pair.Key.ToLower()))
                {
                    return pair.Value;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }
    
    public Region? CheckRegionItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return null;
            foreach (var pair in RegionStrToEnum)
            {
                if (itemName.ToLower().Replace(" ", "").Contains($"{pair.Key}Region".ToLower()))
                {
                    return pair.Value;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }
}