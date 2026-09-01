using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.EggFlappers;

public class EggFlappersData
{
    
    public readonly struct EggFlapperData(Team team, LevelId levelid, string region, string loc_name, EggFlapperWeapon weapon, EggFlapperArmor armor, int group, int id_offset_group, int id_offset_full, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly EggFlapperWeapon Weapon = weapon;
        public readonly EggFlapperArmor Armor = armor;
        public readonly int Group = group;
        public readonly int IdOffsetGroup = id_offset_group;
        public readonly int IdOffsetFull = id_offset_full;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<EggFlapperData> SonicEggFlappers = new()
    {
        
    };

    public static List<EggFlapperData> DarkEggFlappers = new()
    {
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Middle", loc_name: "Red Flapper", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -518.4742f, y: 469f, z: -6665.969f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Middle", loc_name: "2 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -1, id_offset_group: 0, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Middle", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 10, x: -2457.811f, y: 580f, z: -6420.685f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Middle", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 10, x: -2457.811f, y: 580f, z: -6460.685f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "3 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -2, id_offset_group: 2, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: -2670.235f, y: 670f, z: -6430.016f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Green Flapper Bazooka", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: -2710.235f, y: 680f, z: -6470.016f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: -2670.235f, y: 670f, z: -6500.016f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "3 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -3, id_offset_group: 5, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 3, id_offset_group: -999999, id_offset_full: 3, linkid: 0, x: -4593.362f, y: 130.99f, z: -11706.12f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Green Flapper Bazooka", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 3, id_offset_group: -999999, id_offset_full: 3, linkid: 0, x: -4553.362f, y: 140.99f, z: -11736.12f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 3, id_offset_group: -999999, id_offset_full: 3, linkid: 0, x: -4513.362f, y: 130.99f, z: -11706.12f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Second Island", loc_name: "Green Flapper Bazooka", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 0, id_offset_group: 8, id_offset_full: 3, linkid: 0, x: -4508.843f, y: 90.0731f, z: -13015.63f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Stairs After Second Island", loc_name: "Red Flapper", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 0, id_offset_group: 8, id_offset_full: 3, linkid: 0, x: -4508.843f, y: 160.0731f, z: -13175.63f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Before First Bobsled", loc_name: "Red Flapper", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 0, id_offset_group: 8, id_offset_full: 3, linkid: 0, x: -4508.843f, y: 220.0731f, z: -13295.63f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "2 x Egg Flappers", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: -4, id_offset_group: 8, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "Green Flapper Bazooka Left", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 4, id_offset_group: -999999, id_offset_full: 4, linkid: 0, x: -800.8923f, y: 175.614f, z: -16190.05f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "Green Flapper Bazooka Right", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 4, id_offset_group: -999999, id_offset_full: 4, linkid: 0, x: -800.8923f, y: 175.614f, z: -16110.05f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "2 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -5, id_offset_group: 10, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Red Flapper", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: 532.7141f, y: 509.5f, z: -16140.88f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Green Flapper Bazooka", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: 572.7141f, y: 519.5f, z: -16140.88f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "2 x Egg Flappers", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: -6, id_offset_group: 12, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Green Flapper Bazooka Second Group", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 6, id_offset_group: -999999, id_offset_full: 6, linkid: 0, x: 1091.18f, y: 575f, z: -16261.66f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Green Flapper Bazooka Third Group", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 6, id_offset_group: -999999, id_offset_full: 6, linkid: 0, x: 1201.271f, y: 592f, z: -16499.8f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Red Flapper Third Group", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 0, id_offset_group: 14, id_offset_full: 6, linkid: 0, x: 973.2862f, y: 700f, z: -19149.04f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "3 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -7, id_offset_group: 14, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Green Flapper Bazooka Left", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 7, id_offset_group: -999999, id_offset_full: 7, linkid: 0, x: 981.0848f, y: 480f, z: -20029.57f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Red Flapper Center", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 7, id_offset_group: -999999, id_offset_full: 7, linkid: 0, x: 1022.773f, y: 450f, z: -20042.44f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Green Flapper Bazooka Right", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 7, id_offset_group: -999999, id_offset_full: 7, linkid: 0, x: 993.4366f, y: 480f, z: -20076.54f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "3 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -8, id_offset_group: 17, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 8, id_offset_group: -999999, id_offset_full: 8, linkid: 0, x: 849.9999f, y: 135.99f, z: -23017.75f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Green Flapper Bazooka Center", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 8, id_offset_group: -999999, id_offset_full: 8, linkid: 0, x: 899.9999f, y: 145.99f, z: -23087.75f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 8, id_offset_group: -999999, id_offset_full: 8, linkid: 0, x: 949.9999f, y: 135.99f, z: -23017.75f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "3 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -9, id_offset_group: 20, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 9, id_offset_group: -999999, id_offset_full: 9, linkid: 0, x: 859.9997f, y: 245f, z: -23480f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Green Flapper Bazooka Center", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 9, id_offset_group: -999999, id_offset_full: 9, linkid: 0, x: 899.9997f, y: 255f, z: -23510f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 9, id_offset_group: -999999, id_offset_full: 9, linkid: 0, x: 934.9997f, y: 245f, z: -23480f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Start", loc_name: "2 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -10, id_offset_group: 23, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Start", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 10, id_offset_group: -999999, id_offset_full: 10, linkid: 0, x: 875.7404f, y: 338f, z: -23898.04f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Start", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 10, id_offset_group: -999999, id_offset_full: 10, linkid: 0, x: 925.7404f, y: 338f, z: -23898.04f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "5 x Egg Flappers", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: -11, id_offset_group: 25, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Left", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 11, id_offset_group: -999999, id_offset_full: 11, linkid: 0, x: 1977.396f, y: 1032.99f, z: -42987.16f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Green Flapper Bazooka Center Left", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 11, id_offset_group: -999999, id_offset_full: 11, linkid: 0, x: 1933.149f, y: 1032.99f, z: -42977.47f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Center", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 11, id_offset_group: -999999, id_offset_full: 11, linkid: 0, x: 1901.695f, y: 1032.99f, z: -43003.15f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Green Flapper Bazooka Center Right", weapon: EggFlapperWeapon.Bazooka, armor: EggFlapperArmor.NoArmor, group: 11, id_offset_group: -999999, id_offset_full: 11, linkid: 0, x: 1895.821f, y: 1032.99f, z: -43041.02f),
        new EggFlapperData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Right", weapon: EggFlapperWeapon.NoWeapon, armor: EggFlapperArmor.NoArmor, group: 11, id_offset_group: -999999, id_offset_full: 11, linkid: 0, x: 1923.55f, y: 1032.99f, z: -43076.94f),
    };
    
    public static List<EggFlapperData> RoseEggFlappers = new()
    {
        
    };

    public static List<EggFlapperData> ChaotixEggFlappers = new()
    {
        
    };

    public static List<EggFlapperData> SuperHardModeEggFlappers = new()
    {
        
    };



    public static readonly List<EggFlapperData> AllEggFlappers = SonicEggFlappers.Concat(DarkEggFlappers).Concat(RoseEggFlappers).Concat(ChaotixEggFlappers).Concat(SuperHardModeEggFlappers).ToList();
    

}