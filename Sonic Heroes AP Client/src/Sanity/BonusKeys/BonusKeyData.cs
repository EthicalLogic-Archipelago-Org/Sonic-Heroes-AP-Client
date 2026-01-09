
using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.BonusKeys;


public readonly struct KeyPosition (Team team, LevelId levelId, float x, float y, float z, int unused = 0)
{
    public readonly Team Team = team;
    public readonly LevelId LevelId = levelId;
    public readonly Vector3 Pos = new (x, y, z);
}

public static class BonusKeyData
{
    public static readonly List<KeyPosition> AllKeyPositions = new ()
    {
        new (Team.Sonic, LevelId.SeasideHill, -2425.0007f, 532.19995f, -6440.0005f, 1),
        //before 4 Egg Pawns with Cement Blocks blocking path
        new (Team.Sonic, LevelId.SeasideHill, 878.09625f, 112.1f, -18833.014f, 1),
        //beach ruins after drop (left side against cliff)
        new (Team.Sonic, LevelId.SeasideHill, 561.00037f, -297.8f, -30800f, 1),
        //next to cannon in octagon room behind cement block (left)
        
        new(Team.Sonic, LevelId.OceanPalace, 44.854974f, 367f, -12755.146f, 1),
        //above fan after first checkpoint
        new(Team.Sonic, LevelId.OceanPalace, 3100.0288f, 82f, -27150.059f, 1),
        //turtles right side (with jump pad pointing left)
        new(Team.Sonic, LevelId.OceanPalace, 1070.2482f, 1058f, -35358.895f, 1),
        //triangle jump (between walls) before boulder
        
        new(Team.Sonic, LevelId.GrandMetropolis, -135.19885f, -2108.7f, -11917.231f, 1),
        //after first checkpoint next to 2 Flappers
        //Second Key OOB (skipped by tp at blimps with springs)
        //4082.9126 5028.6997 -45936.355
        new(Team.Sonic, LevelId.GrandMetropolis, 8590.294f, -5137.8f, -45958.953f, 1),
        //after blimps with springs turn right to see path (easy to miss)
        
        new(Team.Sonic, LevelId.PowerPlant, 12817f, 4142.2f, -11863f),
        //Behind springs after vertical path (next to triple crusher and pulleys)
        new(Team.Sonic, LevelId.PowerPlant, 16318.082f, 5975.4f, -13293.968f, 9),
        //Top of hallway right side (before 2nd Elevator
        new(Team.Sonic, LevelId.PowerPlant, 20410.777f, 7692f, -12524.706f, 10),
        //Before Ending Section (rising liquid) next to checkpoint (right side)
        
        new(Team.Sonic, LevelId.CasinoPark, -764.0001f, 1362.1f, -2320.0005f, 11),
        //after first pull switch next to glass floor
        new(Team.Sonic, LevelId.CasinoPark, -7350.0337f, -747.9f, -930.0748f, 12),
        //Behind Door that opens after killing many enemies (next to 2nd checkpoint)
        // //1 Gold Cameron 2 Klagens 2 Egg Pawn Shields
        new(Team.Sonic, LevelId.CasinoPark, -6390.0015f, 728f, 2290.0784f, 13),
        //Behind Laser Wall (VIP Table) Requires switch on pinball table before
        
        new(Team.Sonic, LevelId.BingoHighway, 0.04f, -1567.80f, -4942.56f, 14),
        new(Team.Sonic, LevelId.BingoHighway, 500.00f, -5592.80f, -13690.01f, 15),
        new(Team.Sonic, LevelId.BingoHighway, 8278.00f, -14550.80f, -18515.37f, 16),
        
        new(Team.Sonic, LevelId.RailCanyon, 1396.10f, 28168.62f, -24861.00f, 17),
        new(Team.Sonic, LevelId.RailCanyon, -38375.02f, 16302.10f, -21140.10f, 18),
        new(Team.Sonic, LevelId.RailCanyon, -55567.08f, 12692.00f, -20100.07f, 19),
        //-55567.08f, 12762.00f, -20100.07f <- is normally here
        //-55567.08f, 12620.00f, -20100.07f <- is moved to here to not have cage
        
        new(Team.Sonic, LevelId.BulletStation, 48737.21f, 1992.10f, -6192.54f, 20),
        new(Team.Sonic, LevelId.BulletStation, 84000.63f, -393.80f, -8560.90f, 21),
        new(Team.Sonic, LevelId.BulletStation, 114590.08f, 394.10f, -7770.05f, 22),
        
        new(Team.Sonic, LevelId.FrogForest, 0.00f, 1002.00f, -5349.70f, 23),
        new(Team.Sonic, LevelId.FrogForest, -1179.65f,-797.80f, -17170.26f, 24),
        new(Team.Sonic, LevelId.FrogForest, -8239.99f,-2632.70f, -24853.12f, 25),
        
        new(Team.Sonic, LevelId.LostJungle, -191.08f, 107.10f, -1586.23f, 26),
        new(Team.Sonic, LevelId.LostJungle, -1064.99f, 102.10f, -11447.93f, 27),
        new(Team.Sonic, LevelId.LostJungle, -8949.34f, 107.01f, -12604.50f, 1),
        
        new(Team.Sonic, LevelId.HangCastle, 117.00f, -1147.90f, -4195.88f, 28),
        new(Team.Sonic, LevelId.HangCastle, -100.01f, -2347.80f, -7720.01f, 29),
        //new(Team.Sonic, LevelId.HangCastle, 10700.52f, -1595.80f, -13541.10f, 30),
        //moved down
        new(Team.Sonic, LevelId.HangCastle, 10700.52f, -1755f, -13541.10f, 30),
        
        new(Team.Sonic, LevelId.MysticMansion, 1000.08f, 222.10f, -1545.00f, 31),
        new(Team.Sonic, LevelId.MysticMansion, 1325.02f,-4447.90f, -18940.09f, 32),
        //new(Team.Sonic, LevelId.MysticMansion, 15420.056f, -8739.9f, -39680.32f, 999),
        //moved
        new(Team.Sonic, LevelId.MysticMansion, 15420.056f, -8878f, -39730f, 999),
        
        new(Team.Sonic, LevelId.EggFleet, -2726.00f, 607.02f, -4270.00f, 33),
        new(Team.Sonic, LevelId.EggFleet, -7540.00f, 1301.02f, -20140.00f, 34),
        new(Team.Sonic, LevelId.EggFleet, -9498.73f, -3930.48f, -41548.92f, 35),
        
        new(Team.Sonic, LevelId.FinalFortress, 2260.00f, 5402.10f, 38840.04f, 36),
        //new(Team.Sonic, LevelId.FinalFortress, 2250.01f, 5552.00f, 33690.04f, 37),
        //moved
        new(Team.Sonic, LevelId.FinalFortress, 2250.01f, 5400.00f, 33690.04f, 37),
        new(Team.Sonic, LevelId.FinalFortress, 1540.10f, -3896.30f, 15440.05f, 38),
        
        new(Team.Dark, LevelId.SeasideHill, -1000.33f, 482.10f, -6623.32f, 39),
        new(Team.Dark, LevelId.SeasideHill, 1365.05f, 122.10f, -17266.63f, 40),
        new(Team.Dark, LevelId.SeasideHill, 900.00f, -297.80f, -31189.48f, 41),
        
        new(Team.Dark, LevelId.OceanPalace, 749.56f, 517.10f, -9269.01f, 42),
        new(Team.Dark, LevelId.OceanPalace, 1955.00f, 82.10f, -26980.99f, 43),
        new(Team.Dark, LevelId.OceanPalace, 2160.00f, 452.10f, -33446.21f, 44),
        
        new(Team.Dark, LevelId.GrandMetropolis, -133.53f, -2108.70f, -11783.53f, 45),
        new(Team.Dark, LevelId.GrandMetropolis, 3405.47f, -4647.80f, -28467.56f, 46),
        new(Team.Dark, LevelId.GrandMetropolis, 8090.33f, -5055.90f, -45962.78f, 47),
        
        new(Team.Dark, LevelId.PowerPlant, 6435.85f, 2340.00f, -5610.51f, 48),
        new(Team.Dark, LevelId.PowerPlant, 16353.94f, 5975.50f, -13224.65f, 49),
        new(Team.Dark, LevelId.PowerPlant, 20410.32f, 7690.00f, -12265.38f, 50),
        
        new(Team.Dark, LevelId.CasinoPark, -634.00f, 1362.10f, -2230.00f, 51),
        new(Team.Dark, LevelId.CasinoPark, -7350.03f, -747.90f, -930.07f, 52),
        new(Team.Dark, LevelId.CasinoPark, -6390.00f, 728.00f, 2290.08f, 53),
        
        new(Team.Dark, LevelId.BingoHighway, 0.04f, -1567.80f, -4942.56f, 54),
        new(Team.Dark, LevelId.BingoHighway, 500.00f, -5592.80f, -13690.01f, 55),
        new(Team.Dark, LevelId.BingoHighway, 8278.00f, -14550.80f, -18515.37f, 56),
        
        new(Team.Dark, LevelId.RailCanyon, 1386.10f, 28168.62f, -24751.00f, 57),
        new(Team.Dark, LevelId.RailCanyon, -38375.02f, 16302.10f, -21140.10f, 58),
        new(Team.Dark, LevelId.RailCanyon, -55323.55f, 12623.00f, -20100.66f, 59),
        
        new(Team.Dark, LevelId.BulletStation, -1720.00f, 2617.10f, -1830.05f, 60),
        new(Team.Dark, LevelId.BulletStation, 84000.63f, -393.80f, -8560.90f, 61),
        new(Team.Dark, LevelId.BulletStation, 114590.08f, 394.10f, -7770.05f, 62),
        
        new(Team.Dark, LevelId.FrogForest, 0.00f, 1002.00f, -5349.70f, 63),
        new(Team.Dark, LevelId.FrogForest, -909.29f, -747.80f, -17101.28f, 64),
        new(Team.Dark, LevelId.FrogForest, -8239.99f, -2632.70f, -24853.12f, 65),
        
        new(Team.Dark, LevelId.LostJungle, -191.08f, 107.10f, -1586.23f, 66),
        new(Team.Dark, LevelId.LostJungle, -1064.99f, 102.10f, -11447.93f, 67),
        new(Team.Dark, LevelId.LostJungle, -5410.01f, 292.10f, -12140.00f, 68),
        
        new(Team.Dark, LevelId.HangCastle, 283.40f, -1147.80f, -4195.88f, 69),
        new(Team.Dark, LevelId.HangCastle, -100.01f, -2347.80f, -7720.01f, 70),
        new(Team.Dark, LevelId.HangCastle, 10700.52f, -1752.90f, -13541.10f, 71),

        new(Team.Dark, LevelId.MysticMansion, 1000.08f, 222.10f, -1555.00f, 72),
        new(Team.Dark, LevelId.MysticMansion, 1325.02f, -4447.90f, -18940.09f, 73),
        new(Team.Dark, LevelId.MysticMansion, 15420.056f, -8739.9f, -39680.32f, 74),
        
        new(Team.Dark, LevelId.EggFleet, -2726.0f, 607.02f, -4270.00f, 75),
        new(Team.Dark, LevelId.EggFleet, -7540.00f, 1301.02f, -20140.00f, 76),
        new(Team.Dark, LevelId.EggFleet, -9500.01f, -3976.40f, -40660.01f, 77),
        
        new(Team.Dark, LevelId.FinalFortress, 2260.00f, 5402.10f, 38840.04f, 79),
        new(Team.Dark, LevelId.FinalFortress, 2250.01f, 5552.00f, 33690.04f, 78),
        new(Team.Dark, LevelId.FinalFortress, 1540.10f, -3896.30f, 15440.05f, 80),
        
        //new(Team.Rose, LevelId.SeasideHill, -2425f, 532.2f, -6440f, 81),
        //before start of level
        new(Team.Rose, LevelId.SeasideHill, -4510.01f, -2.80f, -12720.03f, 81),
        new(Team.Rose, LevelId.SeasideHill, 1220.40f, 182.20f, -19120.84f, 82),
        
        new(Team.Rose, LevelId.OceanPalace, -481.02f, 317.20f, -17298.80f, 83),
        new(Team.Rose, LevelId.OceanPalace, 2514.33f, 52.00f, -21150.78f, 84),
        new(Team.Rose, LevelId.OceanPalace, 2299.39f, 67.00f, -28581.59f, 85),
        
        new(Team.Rose, LevelId.GrandMetropolis, -135.00f, -1907.80f, -12477.22f, 86),
        new(Team.Rose, LevelId.GrandMetropolis, 1294f, -4598.67f, -25675f, 87),
        new(Team.Rose, LevelId.GrandMetropolis, 3450.15f, -2727.80f, -34111.05f, 87),
        
        new(Team.Rose, LevelId.PowerPlant, 10256.89f, 4340.00f, -8250.38f, 88),
        new(Team.Rose, LevelId.PowerPlant, 12814.33f, 4140.00f, -11862.55f, 89),
        new(Team.Rose, LevelId.PowerPlant, 16304.52f, 5385.00f, -13166.00f, 90),
        
        new(Team.Rose, LevelId.CasinoPark, -530.00f, 1362.10f, -2320.01f, 91),
        new(Team.Rose, LevelId.CasinoPark, -3490.84f, -37.80f, -2320.00f, 92),
        new(Team.Rose, LevelId.CasinoPark, -7350.03f, -747.90f, -920.07f, 93),
        new(Team.Rose, LevelId.CasinoPark, -6390.00f, 728.00f, 2290.08f, 94),
        
        new(Team.Rose, LevelId.BingoHighway, 0.04f, -1567.80f, -4942.56f, 95),
        new(Team.Rose, LevelId.BingoHighway, 500.00f, -5392.80f, -13690.01f, 96),
        new(Team.Rose, LevelId.BingoHighway, 8278.00f, -14550.80f, -18515.37f, 97),
        
        new(Team.Rose, LevelId.RailCanyon, 1396.10f, 28168.62f, -24861.00f, 98),
        new(Team.Rose, LevelId.RailCanyon, -23921.57f, 21202.10f, -28070.71f, 99),
        new(Team.Rose, LevelId.RailCanyon, -37490.06f, 16978.10f, -21161.68f, 100),
        
        new(Team.Rose, LevelId.BulletStation, -1720.00f, 2617.10f, -1830.05f, 101),
        new(Team.Rose, LevelId.BulletStation, 48600.01f, 1990.10f, -5591.00f, 102),
        new(Team.Rose, LevelId.BulletStation, 84000.63f, -393.80f, -8560.90f, 103),
        
        new(Team.Rose, LevelId.FrogForest, 0.00f, 1002.00f, -5369.70f, 104),
        new(Team.Rose, LevelId.FrogForest, -1015.09f, -7.80f, -15320.03f, 105),
        new(Team.Rose, LevelId.FrogForest, -1179.65f, -797.80f, -17170.26f, 106),
        
        new(Team.Rose, LevelId.LostJungle, 254.09f, 432.60f, -5943.21f, 107),
        new(Team.Rose, LevelId.LostJungle, -858.39f, 102.10f, -11372.20f, 108),
        new(Team.Rose, LevelId.LostJungle, -900.00f, 402.10f, -12360.04f, 109),
        
        new(Team.Rose, LevelId.HangCastle, -22.50f, -2347.80f, -6790.01f, 110),
        new(Team.Rose, LevelId.HangCastle, -100.01f, -2347.80f, -7720.01f, 111),
        new(Team.Rose, LevelId.HangCastle, 10700.00f, -1062.98f, -10320.08f, 112),
        
        new(Team.Rose, LevelId.MysticMansion, 0.09f, 2.10f, -105.66f, 113),
        new(Team.Rose, LevelId.MysticMansion, 2000.06f, 262.00f, -3810.03f, 114),
        new(Team.Rose, LevelId.MysticMansion, 1230.02f, -4231.60f, -18939.89f, 115),
        
        new(Team.Rose, LevelId.EggFleet, -7540.00f, 1301.02f, -20220.00f, 116),
        new(Team.Rose, LevelId.EggFleet, -7875.00f, -3110.88f, -29460.01f, 117),
        new(Team.Rose, LevelId.EggFleet, -9369.67f, -3348.50f, -41859.71f, 118),
        
        new(Team.Rose, LevelId.FinalFortress, 2000.38f, 8198.10f, 50457.92f, 119),
        new(Team.Rose, LevelId.FinalFortress, 2250.00f, 5602.10f, 42002.74f, 120),
        new(Team.Rose, LevelId.FinalFortress, 2250.02f, 4502.06f, 35480.06f, 121),
        
        new(Team.Chaotix, LevelId.SeasideHill, -2425.0007f, 532.19995f, -6440.0005f, 122),
        new(Team.Chaotix, LevelId.SeasideHill, 1164.18f, 62.10f, -18921.58f, 123),
        new(Team.Chaotix, LevelId.SeasideHill, 1623.73f, 31.1f, -19727.86f, 123),
        
        new(Team.Chaotix, LevelId.OceanPalace, 44.85f, 367.00f, -12755.15f, 124),
        new(Team.Chaotix, LevelId.OceanPalace, -480.02f, 242.10f, -16540.05f, 125),
        new(Team.Chaotix, LevelId.OceanPalace, 1070.01f, 902.15f, -35043.77f, 126),
        
        new(Team.Chaotix, LevelId.GrandMetropolis, -950.06f, -1297.80f, -9634.01f, 127),
        new(Team.Chaotix, LevelId.GrandMetropolis, 1294.00f, -4598.67f, -25675.00f, 1),
        new(Team.Chaotix, LevelId.GrandMetropolis, 3400.00f, -3357.80f, -34348.00f, 128),
        
        new(Team.Chaotix, LevelId.PowerPlant, 2756.56f, 351.20f, -2587.83f, 129),
        new(Team.Chaotix, LevelId.PowerPlant, 7225.45f, 2580.00f, -4870.29f, 130),
        new(Team.Chaotix, LevelId.PowerPlant, 16383.23f, 5875.70f, -13162.18f, 131),
        
        new(Team.Chaotix, LevelId.CasinoPark, -3620.08f, 202.10f, -2320.02f, 132),
        new(Team.Chaotix, LevelId.CasinoPark, -7350.03f, -747.90f, -930.07f, 133),
        new(Team.Chaotix, LevelId.CasinoPark, -6390.00f, 692.00f, 2210.08f, 134),
        
        new(Team.Chaotix, LevelId.BingoHighway, 0.04f, -1567.80f, -4942.56f, 135),
        new(Team.Chaotix, LevelId.BingoHighway, 500.00f, -5592.80f, -13690.01f, 136),
        new(Team.Chaotix, LevelId.BingoHighway, 8278.00f, -14750.80f, -17815.37f, 137),
        
        new(Team.Chaotix, LevelId.RailCanyon, 1426.00f, 28168.69f, -24800.00f, 138),
        new(Team.Chaotix, LevelId.RailCanyon, -40775.02f, 16302.00f, -21140.10f, 139),
        //new(Team.Chaotix, LevelId.RailCanyon, -55323.55f, 12623.00f, -20100.66f, 140),
        //OOB
        
        new(Team.Chaotix, LevelId.BulletStation, 83080.40f, 922.10f, -8835.65f, 141),
        new(Team.Chaotix, LevelId.BulletStation, 114590.08f, 394.10f, -7770.05f, 143),
        new(Team.Chaotix, LevelId.BulletStation, 99330.05f, 1002.10f, -6642.18f, 142),
        
        new(Team.Chaotix, LevelId.FrogForest, 0.00f, 1042.00f, -5329.70f, 144),
        new(Team.Chaotix, LevelId.FrogForest, -1179.65f, -797.80f, -17170.26f, 145),
        new(Team.Chaotix, LevelId.FrogForest, -1285.60f, -1547.80f, -23978.99f, 146),
        
        new(Team.Chaotix, LevelId.LostJungle, 253.07f, 432.60f, -5942.64f, 147),
        new(Team.Chaotix, LevelId.LostJungle, -1009.04f, 102.10f, -11653.21f, 148),
        new(Team.Chaotix, LevelId.LostJungle, -11020.96f, 302.10f, -12720.72f, 149),
        
        new(Team.Chaotix, LevelId.HangCastle, -50.00f, -2347.80f, -6780.08f, 150),
        new(Team.Chaotix, LevelId.HangCastle, -100.01f, -2347.80f, -7720.01f, 151),
        new(Team.Chaotix, LevelId.HangCastle, 10700.52f, -1595.80f, -13541.10f, 152),

        new(Team.Chaotix, LevelId.MysticMansion, 1000.08f, 263.30f, -1526.00f, 153),
        new(Team.Chaotix, LevelId.MysticMansion, 1325.02f, -4447.90f, -18940.09f, 154),
        new(Team.Chaotix, LevelId.MysticMansion, 5148.02f, -3517.80f, -23899.04f, 155),

        new(Team.Chaotix, LevelId.EggFleet, -2726.00f, 607.02f, -4270.00f, 156),
        new(Team.Chaotix, LevelId.EggFleet, -8615.00f, 3084.02f, -18730.00f, 157),
        new(Team.Chaotix, LevelId.EggFleet, -9330.30f, -3562.38f, -41668.80f, 158),

        new(Team.Chaotix, LevelId.FinalFortress, 2251.01f, 6002.10f, 44260.36f, 159),
        new(Team.Chaotix, LevelId.FinalFortress, 2250.00f, 4502.04f, 35480.00f, 160),
        new(Team.Chaotix, LevelId.FinalFortress, 2200.01f, -3470.80f, 16850.07f, 161),
    };
}