using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.Enemy;

public static class EnemyData
{
    public readonly struct EnemySanityData (Team team, LevelId levelid, string region, string loc_name, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<EnemySanityData> SonicEnemies = new()
    {
        
    };
    
    
    public static List<EnemySanityData> DarkEnemies = new()
    {
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "Egg Pawn", linkid: 0, x: 0.0f, y: 40.0f, z: -1412.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Left", linkid: 0, x: -331.5993f, y: 329.9996f, z: -6572.702f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Center Left", linkid: 0, x: -294.9051f, y: 329.9996f, z: -6602.96f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Center Right", linkid: 0, x: -294.9051f, y: 329.9996f, z: -6642.96f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "Egg Pawn Right", linkid: 0, x: -334.5375f, y: 329.9996f, z: -6666.054f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Middle", loc_name: "Red Flapper", linkid: 0, x: -518.4742f, y: 469.0f, z: -6665.969f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Egg Pawn Bazooka", linkid: 0, x: -712.5491f, y: 530.0f, z: -6630.392f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Egg Pawn", linkid: 0, x: -735.9069f, y: 609.1093f, z: -6552.866f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Left", linkid: 0, x: -1818.239f, y: 620.0f, z: -6368.518f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Center", linkid: 0, x: -1840.888f, y: 620.0f, z: -6398.068f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "Egg Pawn Right", linkid: 0, x: -1799.18f, y: 620.0f, z: -6423.996f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Middle", loc_name: "Red Flapper Left", linkid: 10, x: -2457.811f, y: 580.0f, z: -6420.685f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Middle", loc_name: "Red Flapper Right", linkid: 10, x: -2457.811f, y: 580.0f, z: -6460.685f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Red Flapper Left", linkid: 0, x: -2670.235f, y: 670.0f, z: -6430.016f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Green Flapper Bazooka", linkid: 0, x: -2710.235f, y: 680.0f, z: -6470.016f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Top", loc_name: "Red Flapper Right", linkid: 0, x: -2670.235f, y: 670.0f, z: -6500.016f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Left", linkid: 0, x: -4570.841f, y: 8.4525f, z: -10741.49f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center Left", linkid: 0, x: -4550.841f, y: 8.4525f, z: -10781.49f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center", linkid: 0, x: -4500.841f, y: 8.4525f, z: -10811.49f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Bazooka Center Right", linkid: 0, x: -4450.841f, y: 8.4525f, z: -10781.49f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", loc_name: "Egg Pawn Right", linkid: 0, x: -4430.841f, y: 8.4525f, z: -10741.49f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Red Flapper Left", linkid: 0, x: -4593.362f, y: 130.99f, z: -11706.12f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Green Flapper Bazooka", linkid: 0, x: -4553.362f, y: 140.99f, z: -11736.12f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Third Ruin Between First and Second Island", loc_name: "Red Flapper Right", linkid: 0, x: -4513.362f, y: 130.99f, z: -11706.12f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "Egg Pawn Bazooka", linkid: 0, x: -4638.64f, y: 2.589f, z: -12470.5f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "Egg Pawn Lance", linkid: 0, x: -4353.55f, y: 5.8472f, z: -12444.79f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Second Island", loc_name: "Green Flapper Bazooka", linkid: 0, x: -4508.843f, y: 90.0731f, z: -13015.63f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Stairs After Second Island", loc_name: "Red Flapper", linkid: 0, x: -4508.843f, y: 160.0731f, z: -13175.63f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Before First Bobsled", loc_name: "Red Flapper", linkid: 0, x: -4508.843f, y: 220.0731f, z: -13295.63f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn First Group Left", linkid: 0, x: -4201.66f, y: 9.0f, z: -15935.95f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn First Group Right", linkid: 0, x: -4190.66f, y: 9.0115f, z: -15935.95f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Second Group Front", linkid: 0, x: -4171.267f, y: 97.491f, z: -16698.38f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Second Group Back", linkid: 0, x: -4142.161f, y: 101.1138f, z: -16745.32f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Front", linkid: 0, x: -2473.575f, y: 30.0f, z: -16177.73f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Middle", linkid: 0, x: -2273.575f, y: 30.0f, z: -16137.73f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Egg Pawn Third Group Back", linkid: 0, x: -2053.575f, y: 30.0f, z: -16149.73f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "Green Flapper Bazooka Left", linkid: 0, x: -800.8923f, y: 175.614f, z: -16190.05f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "Green Flapper Bazooka Right", linkid: 0, x: -800.8923f, y: 175.614f, z: -16110.05f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Egg Pawn Lance Left", linkid: 0, x: 437.5364f, y: 385.0f, z: -16203.21f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Egg Pawn Lance Right", linkid: 0, x: 460.0002f, y: 385.0f, z: -16158.21f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Red Flapper", linkid: 0, x: 532.7141f, y: 509.5f, z: -16140.88f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "Green Flapper Bazooka", linkid: 0, x: 572.7141f, y: 519.5f, z: -16140.88f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance First Group Left", linkid: 0, x: 916.91f, y: 535.0f, z: -16236.91f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance First Group Right", linkid: 0, x: 916.91f, y: 535.0f, z: -16146.91f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Second Group Left", linkid: 0, x: 1049.868f, y: 600.0f, z: -16311.85f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Green Flapper Bazooka Second Group", linkid: 0, x: 1091.18f, y: 575.0f, z: -16261.66f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Second Group Right", linkid: 0, x: 1132.147f, y: 600.0f, z: -16204.82f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Third Group Left", linkid: 0, x: 1118.053f, y: 600.0f, z: -16429.21f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Green Flapper Bazooka Third Group", linkid: 0, x: 1201.271f, y: 592.0f, z: -16499.8f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "Egg Pawn Lance Third Group Right", linkid: 0, x: 1271.852f, y: 600.0f, z: -16412.44f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1", loc_name: "Egg Pawn Lance", linkid: 0, x: 1210.007f, y: -4.9f, z: -18120.01f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 1", loc_name: "Egg Pawn Bazooka", linkid: 0, x: 726.8429f, y: 670.0f, z: -18443.09f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance First Group Left", linkid: 0, x: 666.8429f, y: 670.0f, z: -18743.09f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance First Group Right", linkid: 0, x: 726.8429f, y: 670.0f, z: -18743.09f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Second Group", linkid: 0, x: 746.8165f, y: 720.0f, z: -19081.61f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Red Flapper Third Group", linkid: 0, x: 973.2862f, y: 700.0f, z: -19149.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fourth Group", linkid: 0, x: 1237.105f, y: 670.0f, z: -19174.21f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Left", linkid: 0, x: 1295.168f, y: 670.0f, z: -19498.48f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Center", linkid: 0, x: 1315.168f, y: 670.0f, z: -19448.48f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Top Path 2", loc_name: "Egg Pawn Lance Fifth Group Right", linkid: 0, x: 1335.168f, y: 670.0f, z: -19498.48f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Lance Concrete Shield First Group Left", linkid: 0, x: 1144.919f, y: 2.0131f, z: -18927.16f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Lance Concrete Shield First Group Right", linkid: 0, x: 1204.919f, y: 1.2619f, z: -18927.16f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Bazooka Second Group Left", linkid: 0, x: 1539.577f, y: 5.0f, z: -19612.19f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Second Group Center", linkid: 0, x: 1599.577f, y: 33.0f, z: -19682.19f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "Egg Pawn Bazooka Second Group Right", linkid: 0, x: 1659.577f, y: 1.5491f, z: -19612.19f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Green Flapper Bazooka Left", linkid: 0, x: 981.0848f, y: 480.0f, z: -20029.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Red Flapper Center", linkid: 0, x: 1022.773f, y: 450.0f, z: -20042.44f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "Green Flapper Bazooka Right", linkid: 0, x: 993.4366f, y: 480.0f, z: -20076.54f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Red Flapper Left", linkid: 0, x: 849.9999f, y: 135.99f, z: -23017.75f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Green Flapper Bazooka Center", linkid: 0, x: 899.9999f, y: 145.99f, z: -23087.75f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Special Ruin Before Volcano", loc_name: "Red Flapper Right", linkid: 0, x: 949.9999f, y: 135.99f, z: -23017.75f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Red Flapper Left", linkid: 0, x: 859.9997f, y: 245.0f, z: -23480.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Green Flapper Bazooka Center", linkid: 0, x: 899.9997f, y: 255.0f, z: -23510.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After Big Special Ruin Before Volcano", loc_name: "Red Flapper Right", linkid: 0, x: 934.9997f, y: 245.0f, z: -23480.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Start", loc_name: "Red Flapper Left", linkid: 0, x: 875.7404f, y: 338.0f, z: -23898.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Start", loc_name: "Red Flapper Right", linkid: 0, x: 925.7404f, y: 338.0f, z: -23898.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Left", linkid: 0, x: 871.7404f, y: 300.0f, z: -24128.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Center", linkid: 0, x: 901.7404f, y: 300.0f, z: -24168.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Egg Pawn Lance Right", linkid: 0, x: 931.7404f, y: 300.0f, z: -24128.04f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano Entrance", loc_name: "Egg Pawn Lance Left", linkid: 0, x: 880.007f, y: 200.0f, z: -28345.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano Entrance", loc_name: "Egg Pawn Lance Right", linkid: 0, x: 920.007f, y: 200.0f, z: -28345.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After First Block", loc_name: "Egg Pawn Lance Left", linkid: 0, x: 880.007f, y: 200.0f, z: -28945.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After First Block", loc_name: "Egg Pawn Lance Right", linkid: 0, x: 920.007f, y: 200.0f, z: -28945.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Cave Before Volcano After Second Block", loc_name: "Egg Pawn Lance", linkid: 0, x: 900.007f, y: 200.0f, z: -29385.57f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Left", linkid: 0, x: 839.5432f, y: -299.9f, z: -30711.98f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Center", linkid: 0, x: 899.9273f, y: -299.9f, z: -30682.09f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Front Right", linkid: 0, x: 959.5065f, y: -299.9f, z: -30709.01f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Left", linkid: 0, x: 586.0004f, y: -250.0f, z: -30800.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Center", linkid: 0, x: 900.0004f, y: -250.0f, z: -31110.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Volcano Cannon Room", loc_name: "Egg Pawn Lance Back Right", linkid: 0, x: 1216.0f, y: -250.0f, z: -30800.0f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Left", linkid: 0, x: 843.2516f, y: 924.99f, z: -37366.77f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Center", linkid: 0, x: 881.4348f, y: 932.99f, z: -37313.05f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn First Group Right", linkid: 0, x: 878.1386f, y: 928.99f, z: -37434.5f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Second Group", linkid: 0, x: 429.6094f, y: 601.1299f, z: -38594.59f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Third Group Left", linkid: 0, x: 1068.531f, y: 542.1299f, z: -38622.16f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Egg Pawn Second Group Right", linkid: 0, x: 1128.531f, y: 536.1299f, z: -38602.16f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Loop", loc_name: "Egg Pawn", linkid: 0, x: -1719.5f, y: 350.0f, z: -41906.11f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn First Group Left", linkid: 0, x: -1740.95f, y: 600.0f, z: -42555.11f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn First Group Right", linkid: 0, x: -1698.631f, y: 600.0f, z: -42552.43f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Second Group Left", linkid: 0, x: -1737.727f, y: 600.0f, z: -42686.12f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Second Group Right", linkid: 0, x: -1704.198f, y: 600.0f, z: -42685.07f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Egg Pawn Third Group", linkid: 0, x: -1719.835f, y: 600.0f, z: -42870.92f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Egg Pawn Lance", linkid: 0, x: 2187.556f, y: 1050.0f, z: -43160.73f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Left", linkid: 0, x: 1977.396f, y: 1032.99f, z: -42987.16f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Green Flapper Bazooka Center Left", linkid: 0, x: 1933.149f, y: 1032.99f, z: -42977.47f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Center", linkid: 0, x: 1901.695f, y: 1032.99f, z: -43003.15f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Green Flapper Bazooka Center Right", linkid: 0, x: 1895.821f, y: 1032.99f, z: -43041.02f),
        new EnemySanityData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Whale Island Top", loc_name: "Red Flapper Right", linkid: 0, x: 1923.55f, y: 1032.99f, z: -43076.94f),
    };
    
    public static List<EnemySanityData> RoseEnemies = new()
    {
    };

    public static List<EnemySanityData> ChaotixEnemies = new()
    {
    };

    public static List<EnemySanityData> SuperHardModeEnemies = new()
    {
    };

    public static readonly List<EnemySanityData> AllEnemies = SonicEnemies.Concat(DarkEnemies).Concat(RoseEnemies).Concat(ChaotixEnemies).Concat(SuperHardModeEnemies).ToList();

    
}