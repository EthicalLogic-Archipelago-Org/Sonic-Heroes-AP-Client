using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.EggPawns;

public static class EggPawnsData
{
    public readonly struct EggPawnData(Team team, LevelId levelid, string region, string loc_name, EggPawnWeapon weapon, EggPawnShield shield, EggPawnType special_type, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly EggPawnWeapon Weapon = weapon;
        public readonly EggPawnShield Shield = shield;
        public readonly EggPawnType SpecialType = special_type;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<EggPawnData> SonicEggPawns = new()
    {
        
    };

    public static List<EggPawnData> DarkEggPawns = new()
    {
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "Egg Pawn", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 0f, y: 40f, z: -1412f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -331.5993f, y: 329.9996f, z: -6572.702f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Center Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -294.9051f, y: 329.9996f, z: -6602.96f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Center Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -294.9051f, y: 329.9996f, z: -6642.96f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -334.5375f, y: 329.9996f, z: -6666.054f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Egg Pawn Bazooka", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -712.5491f, y: 530f, z: -6630.392f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Egg Pawn", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -735.9069f, y: 609.1093f, z: -6552.866f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1818.239f, y: 620f, z: -6368.518f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Center", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1840.888f, y: 620f, z: -6398.068f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1799.18f, y: 620f, z: -6423.996f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4570.841f, y: 8.4525f, z: -10741.49f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center Left", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4550.841f, y: 8.4525f, z: -10781.49f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4500.841f, y: 8.4525f, z: -10811.49f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center Right", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4450.841f, y: 8.4525f, z: -10781.49f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4430.841f, y: 8.4525f, z: -10741.49f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "Egg Pawn Bazooka", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4638.64f, y: 2.589f, z: -12470.5f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "Egg Pawn Lance", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4353.55f, y: 5.8472f, z: -12444.79f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn First Group Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4201.66f, y: 9f, z: -15935.95f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn First Group Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4190.66f, y: 9.0115f, z: -15935.95f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Second Group Front", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4171.267f, y: 97.491f, z: -16698.38f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Second Group Back", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -4142.161f, y: 101.1138f, z: -16745.32f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Front", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -2473.575f, y: 30f, z: -16177.73f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Middle", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -2273.575f, y: 30f, z: -16137.73f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Back", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -2053.575f, y: 30f, z: -16149.73f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Egg Pawn Lance Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 437.5364f, y: 385f, z: -16203.21f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Egg Pawn Lance Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 460.0002f, y: 385f, z: -16158.21f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance First Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 916.91f, y: 535f, z: -16236.91f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance First Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 916.91f, y: 535f, z: -16146.91f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Second Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1049.868f, y: 600f, z: -16311.85f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Second Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1132.147f, y: 600f, z: -16204.82f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Third Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1118.053f, y: 600f, z: -16429.21f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Third Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1271.852f, y: 600f, z: -16412.44f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1", loc_name: "Egg Pawn Lance", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1210.007f, y: -4.9f, z: -18120.01f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 1", loc_name: "Egg Pawn Bazooka", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 726.8429f, y: 670f, z: -18443.09f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance First Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 666.8429f, y: 670f, z: -18743.09f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance First Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 726.8429f, y: 670f, z: -18743.09f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Second Group", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 746.8165f, y: 720f, z: -19081.61f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fourth Group", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1237.105f, y: 670f, z: -19174.21f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1295.168f, y: 670f, z: -19498.48f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Center", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1315.168f, y: 670f, z: -19448.48f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1335.168f, y: 670f, z: -19498.48f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Lance Concrete Shield First Group Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.ConcreteShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1144.919f, y: 2.0131f, z: -18927.16f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Lance Concrete Shield First Group Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.ConcreteShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1204.919f, y: 1.2619f, z: -18927.16f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Bazooka Second Group Left", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1539.577f, y: 5f, z: -19612.19f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Second Group Center", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1599.577f, y: 33f, z: -19682.19f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Bazooka Second Group Right", weapon: EggPawnWeapon.Bazooka, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1659.577f, y: 1.5491f, z: -19612.19f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 871.7404f, y: 300f, z: -24128.04f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Center", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 901.7404f, y: 300f, z: -24168.04f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 931.7404f, y: 300f, z: -24128.04f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano Entrance", loc_name: "Egg Pawn Lance Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 880.007f, y: 200f, z: -28345.57f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano Entrance", loc_name: "Egg Pawn Lance Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 920.007f, y: 200f, z: -28345.57f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After First Block", loc_name: "Egg Pawn Lance Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 880.007f, y: 200f, z: -28945.57f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After First Block", loc_name: "Egg Pawn Lance Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 920.007f, y: 200f, z: -28945.57f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After Second Block", loc_name: "Egg Pawn Lance", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 900.007f, y: 200f, z: -29385.57f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 839.5432f, y: -299.9f, z: -30711.98f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Center", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 899.9273f, y: -299.9f, z: -30682.09f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 959.5065f, y: -299.9f, z: -30709.01f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Left", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 586.0004f, y: -250f, z: -30800f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Center", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 900.0004f, y: -250f, z: -31110f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Right", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1216f, y: -250f, z: -30800f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 843.2516f, y: 924.99f, z: -37366.77f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Center", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 881.4348f, y: 932.99f, z: -37313.05f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 878.1386f, y: 928.99f, z: -37434.5f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Second Group", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 429.6094f, y: 601.1299f, z: -38594.59f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Third Group Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1068.531f, y: 542.1299f, z: -38622.16f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Second Group Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 1128.531f, y: 536.1299f, z: -38602.16f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Loop", loc_name: "Egg Pawn", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1719.5f, y: 350f, z: -41906.11f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn First Group Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1740.95f, y: 600f, z: -42555.11f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn First Group Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1698.631f, y: 600f, z: -42552.43f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Second Group Left", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1737.727f, y: 600f, z: -42686.12f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Second Group Right", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1704.198f, y: 600f, z: -42685.07f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Third Group", weapon: EggPawnWeapon.NoWeapon, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: -1719.835f, y: 600f, z: -42870.92f),
        new EggPawnData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Egg Pawn Lance", weapon: EggPawnWeapon.Lance, shield: EggPawnShield.NoShield, special_type: EggPawnType.RegularPawn, linkid: 0, x: 2187.556f, y: 1050f, z: -43160.73f),
    };

    public static List<EggPawnData> RoseEggPawns = new()
    {
        
    };

    public static List<EggPawnData> ChaotixEggPawns = new()
    {
        
    };

    public static List<EggPawnData> SuperHardModeEggPawns = new()
    {
        
    };
    
    public static readonly List<EggPawnData> AllEggPawns = SonicEggPawns.Concat(DarkEggPawns).Concat(RoseEggPawns).Concat(ChaotixEggPawns).Concat(SuperHardModeEggPawns).ToList();
    
    
}