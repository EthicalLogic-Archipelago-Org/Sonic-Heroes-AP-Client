
using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.Checkpoints;

public readonly struct CheckPointPriority (Team team, LevelId levelid, int priority, float x = 0.0f, float y = 0.0f, float z = 0.0f)
{
    public readonly Team Team = team;
    public readonly LevelId LevelId = levelid;
    public readonly int Priority = priority;
    //public readonly bool SuperHard = superhard;
    public readonly Vector3 SpawnCoords = new (x, y, z);

}

public static class CheckpointData
{
    public static List<CheckPointPriority> SonicCheckpoints = new()
    {
        new(Team.Sonic, LevelId.SeasideHill, 0),
        //1 110 -4215
        new(Team.Sonic, LevelId.SeasideHill, 1),
        //-4509.383 180 -6922.28
        new(Team.Sonic, LevelId.SeasideHill, 2),
        //-130.082 154.9283 -16150.09
        new(Team.Sonic, LevelId.SeasideHill, 4),
        //900 600 -20605
        new(Team.Sonic, LevelId.SeasideHill, 5),
        //880.9 2000 -33754.08
        
        
        new(Team.Sonic, LevelId.OceanPalace, 0),
        //750 810 -10302
        new(Team.Sonic, LevelId.OceanPalace, 1),
        //2398.8208 75 -24800.24
        new(Team.Sonic, LevelId.OceanPalace, 3),
        //2100.043 50 -31690.03
        new(Team.Sonic, LevelId.OceanPalace, 10),
        //800 1515 -36010
        
        
        new(Team.Sonic, LevelId.GrandMetropolis, 0),
        //-134.99962 -1299.9 -10105
        new(Team.Sonic, LevelId.GrandMetropolis, 1),
        //-335.00836 -4699.9 -20207
        new(Team.Sonic, LevelId.GrandMetropolis, 2, 3405.0002f, -4649.9f, -28195),
        //3405.0002 -4649.9 -28195
        new(Team.Sonic, LevelId.GrandMetropolis, 2, 3400.0005f, -3359.9001f, -33430.008f),
        //3400.0005 -3359.9001 -33430.008
        //new(Team.Sonic, LevelId.GrandMetropolis, 3),
        //Out of Bounds (blimp springs tp past it)
        //2649.5144 -2369.9001 -41030.516
        
        
        
        new(Team.Sonic, LevelId.PowerPlant, 1, 5772.445f, 2320, -3946.1865f),
        //5772.445 2320 -3946.1865
        new(Team.Sonic, LevelId.PowerPlant, 1, 12816.072f, 3690, -11003.363f),
        //12816.072 3690 -11003.363
        new(Team.Sonic, LevelId.PowerPlant, 2),
        //16507.467, 5745, -12811.182
        new(Team.Sonic, LevelId.PowerPlant, 3),
        //20412.184 7690 -12387.666
        
        
        new(Team.Sonic, LevelId.CasinoPark, 1),
        //-3190.092 -39.9 -2320
        new(Team.Sonic, LevelId.CasinoPark, 2),
        //-7250.528 -649.9 -550.4929
        new(Team.Sonic, LevelId.CasinoPark, 3),
        //-6480 160 1360.049
        
        
        new(Team.Sonic, LevelId.BingoHighway, 1),
        //680 -2259.9 -6170.08
        new(Team.Sonic, LevelId.BingoHighway, 2),
        //680 -2689.9 -7029.155
        new(Team.Sonic, LevelId.BingoHighway, 3),
        //500 -5394.9 -13793.82
        new(Team.Sonic, LevelId.BingoHighway, 5),
        //8280.059 -14352.9 -18490.49
        
        
        new(Team.Sonic, LevelId.RailCanyon, 1),
        //885.1667 28167.7 -24285.14
        new(Team.Sonic, LevelId.RailCanyon, 2),
        //-6778.1060 25195.37 -26802.91
        //new(Team.Sonic, LevelId.RailCanyon, 3),
        //mutual with 4
        //-17050.62 24400 -25440.06
        new(Team.Sonic, LevelId.RailCanyon, 4),
        //mutual with 3
        //-17560.62 24201 -25440.06
        new(Team.Sonic, LevelId.RailCanyon, 5),
        //-35870.75 17371 -21000.85
        new(Team.Sonic, LevelId.RailCanyon, 6, -39004.93f, 16494.9f, -20625.45f),
        //Duplicate Priority
        //-39004.93 16494.9 -20625.45
        new(Team.Sonic, LevelId.RailCanyon, 6, -52560.83f, 13367.79f, -20100.75f),
        //Duplicate Priority
        //-52560.83 13367.79 -20100.75
        
        
        //new(Team.Sonic, LevelId.BulletStation, 1),
        //before start of level
        //-1720.75 2625.04 -2540.591
        new(Team.Sonic, LevelId.BulletStation, 1),
        //-150.017 2030 8022.626
        new(Team.Sonic, LevelId.BulletStation, 5),
        //83079.46 910 -8556.479
        new(Team.Sonic, LevelId.BulletStation, 6),
        //115500.4 194 -7139.779
        new(Team.Sonic, LevelId.BulletStation, 10),
        //99600.2 1000 -6942.058
        
        
        new(Team.Sonic, LevelId.FrogForest, 2),
        //0.076 1040 -5410.125
        new(Team.Sonic, LevelId.FrogForest, 4),
        //-1045.003 -0.0007 -14740.66
        new(Team.Sonic, LevelId.FrogForest, 5),
        //Mutually exclusive with 6
        //-1993.793 -699.9 -23210.49
        //new(Team.Sonic, LevelId.FrogForest, 6),
        //mutually exclusive with 5
        //-2024.099 -1099.9 -23137.62
        
        
        new(Team.Sonic, LevelId.LostJungle, 1),
        //-150.009 225 -1760.8079
        new(Team.Sonic, LevelId.LostJungle, 2),
        //-1.4890196 1180 -6201.9697
        new(Team.Sonic, LevelId.LostJungle, 3),
        //-1110.0771 250 -11997.056
        new(Team.Sonic, LevelId.LostJungle, 4),
        //-6260.0044 100 -11785.068
        new(Team.Sonic, LevelId.LostJungle, 5),
        //-12480.084 1880 -13100.067
        
        
        new(Team.Sonic, LevelId.HangCastle, 1),
        //60.000786 -2349.9001 -6780.0757
        new(Team.Sonic, LevelId.HangCastle, 2),
        //260.06445 -1969.9 -8740.041
        new(Team.Sonic, LevelId.HangCastle, 4),
        //-700.0119 -1879.9 -13060
        
        
        new(Team.Sonic, LevelId.MysticMansion, 1),
        //1000.06903 420 -1650.007
        new(Team.Sonic, LevelId.MysticMansion, 2),
        //1230.0764 -4449.9 -18710.809
        new(Team.Sonic, LevelId.MysticMansion, 4),
        //6560.0044 -3679.9001 -21970.074
        new(Team.Sonic, LevelId.MysticMansion, 5),
        //15420 -9090 -39680.023
        
        
        new(Team.Sonic, LevelId.EggFleet, 0),
        //-2849 800 -4360
        new(Team.Sonic, LevelId.EggFleet, 2, -6000, 2471, -8285),
        //has a second one with 2
        //-6000 2471 -8285
        new(Team.Sonic, LevelId.EggFleet, 2, -7750, 1365, -20610),
        //after other one (next to Key 2) intended to get both
        //-7750 1365 -20610
        new(Team.Sonic, LevelId.EggFleet, 3),
        //-7714 -3062.9 -29300
        new(Team.Sonic, LevelId.EggFleet, 4),
        //-9500 -4213.4 -38170
        
        
        new(Team.Sonic, LevelId.FinalFortress, 1),
        //2250.01 6270 44010.02
        new(Team.Sonic, LevelId.FinalFortress, 2),
        //2250 5400 33620.06
        new(Team.Sonic, LevelId.FinalFortress, 3),
        //2350 -6439.90 10930.05
    };
    
    
    public static List<CheckPointPriority> SuperHardCheckpoints = new()
    {
        new(Team.SuperHardMode, LevelId.SeasideHill, 0),
        //-2155 480 -6425
        new(Team.SuperHardMode, LevelId.SeasideHill, 1),
        //-112.1187 156.1652 -16150
        new(Team.SuperHardMode, LevelId.SeasideHill, 4),
        //900 600 -20595
        new(Team.SuperHardMode, LevelId.SeasideHill, 10),
        //880.9 2000 -33750.5
        
        
        new(Team.SuperHardMode, LevelId.OceanPalace, 0),
        //750 810 -10302
        new(Team.SuperHardMode, LevelId.OceanPalace, 1),
        //-480.06 175 -18730
        new(Team.SuperHardMode, LevelId.OceanPalace, 2),
        //2398.8208 75 -24800.24
        new(Team.SuperHardMode, LevelId.OceanPalace, 3),
        //2100 50 -31690.03
        new(Team.SuperHardMode, LevelId.OceanPalace, 5),
        //1070 1050 -34416.93
        
        
        new(Team.SuperHardMode, LevelId.GrandMetropolis, 0),
        //-135 -1299.9 -10105
        new(Team.SuperHardMode, LevelId.GrandMetropolis, 1),
        //-335 -4699.9 -20207
        new(Team.SuperHardMode, LevelId.GrandMetropolis, 2),
        //3400 -3359.9 -33430
        new(Team.SuperHardMode, LevelId.GrandMetropolis, 3),
        //2649.51 -2369.9 -40160.516
        
        
        new(Team.SuperHardMode, LevelId.PowerPlant, 1),
        //5772.577 2320 -3946.2330
        new(Team.SuperHardMode, LevelId.PowerPlant, 2),
        //13466.96 4690 -12727.36
        new(Team.SuperHardMode, LevelId.PowerPlant, 3),
        //14504, 5790, -13923.48
        new(Team.SuperHardMode, LevelId.PowerPlant, 4),
        //20412.18 7690 -12387.67
        
        
        new(Team.SuperHardMode, LevelId.CasinoPark, 1),
        //-3190.092 -39.9 -2320
        new(Team.SuperHardMode, LevelId.CasinoPark, 2),
        //-7250.528 -649.9 -550.4929
        new(Team.SuperHardMode, LevelId.CasinoPark, 3),
        //-6480 160 1360.049
        
        
        new(Team.SuperHardMode, LevelId.BingoHighway, 1),
        //680 -2259.9 -6170.08
        new(Team.SuperHardMode, LevelId.BingoHighway, 2),
        //680 -2689.9 -7029.155
        new(Team.SuperHardMode, LevelId.BingoHighway, 3),
        //500 -5394.9 -13793.82
        new(Team.SuperHardMode, LevelId.BingoHighway, 5),
        //8280.059 -14352.9 -18490.49
        
        
        new(Team.SuperHardMode, LevelId.RailCanyon, 1),
        //885.1667 28167.7 -24285.14
        new(Team.SuperHardMode, LevelId.RailCanyon, 2),
        //-6738.1060 25195.37 -26802.91
        //new(Team.Sonic, LevelId.RailCanyon, 3, true),
        //mutual with 4
        //-17050.62 24400 -25440.06
        new(Team.SuperHardMode, LevelId.RailCanyon, 4),
        //mutual with 3
        //-17560.62 24201 -25440.06
        new(Team.SuperHardMode, LevelId.RailCanyon, 5),
        //-35870.75 17371 -21000.85
        new(Team.SuperHardMode, LevelId.RailCanyon, 6, -39004.93f, 16494.9f, -20625.45f),
        //Duplicate Priority
        //-39004.93 16494.9 -20625.45
        new(Team.SuperHardMode, LevelId.RailCanyon, 6, -52560.83f, 13367.79f, -20100.75f),
        //Duplicate Priority
        //-52560.83 13367.79 -20100.75
        
        
        //new(Team.Sonic, LevelId.BulletStation, 1, true),
        //before start of level
        //-1720.75 2625.04 -2540.59
        new(Team.SuperHardMode, LevelId.BulletStation, 3),
        //49830.38 1910 -6180.549
        new(Team.SuperHardMode, LevelId.BulletStation, 5),
        //83079.46 910 -8556.479
        new(Team.SuperHardMode, LevelId.BulletStation, 6),
        //115500.4 194 -7139.779
        new(Team.SuperHardMode, LevelId.BulletStation, 10),
        //99600.2 1000 -6942.058
        
        
        new(Team.SuperHardMode, LevelId.FrogForest, 2),
        //0.076 1040 -5410.125
        new(Team.SuperHardMode, LevelId.FrogForest, 4),
        //-1045.003 -0.0007 -14740.66
        new(Team.SuperHardMode, LevelId.FrogForest, 6),
        //-2024.099 -1099.9 -23137.62
        
        
        new(Team.SuperHardMode, LevelId.LostJungle, 1),
        //-476.093 225 -1829.312
        new(Team.SuperHardMode, LevelId.LostJungle, 2),
        //-1.4890196 1180 -6201.9697
        new(Team.SuperHardMode, LevelId.LostJungle, 3),
        //-1110.0771 250 -11997.056
        new(Team.SuperHardMode, LevelId.LostJungle, 4),
        //-6260.0044 100 -11785.068
        new(Team.SuperHardMode, LevelId.LostJungle, 5),
        //-12480.084 1880 -13100.067
        
        
        new(Team.SuperHardMode, LevelId.HangCastle, 1),
        //60.000786 -2349.9001 -6780.0757
        new(Team.SuperHardMode, LevelId.HangCastle, 2),
        //260.06445 -1969.9 -8740.041
        new(Team.SuperHardMode, LevelId.HangCastle, 4),
        //-700.0119 -1879.9 -13060
        
        
        new(Team.SuperHardMode, LevelId.MysticMansion, 1),
        //1000.06903 420 -1650.007
        new(Team.SuperHardMode, LevelId.MysticMansion, 2),
        //1230.0764 -4449.9 -18710.809
        new(Team.SuperHardMode, LevelId.MysticMansion, 4),
        //6340.0044 -3979.9001 -21970.074
        new(Team.SuperHardMode, LevelId.MysticMansion, 5),
        //15420 -9090 -39680.023
        //new(Team.Sonic, LevelId.MysticMansion, 9, true),
        //not possible to get (holdover from Chaotix)
        //2830.0042 -3319.9001 -18998.008
        new(Team.SuperHardMode, LevelId.MysticMansion, 11),
        //4930 -3319.9001 -24800.012
        
        
        new(Team.SuperHardMode, LevelId.EggFleet, 0),
        //-4930.0050 600 -6519.2650
        new(Team.SuperHardMode, LevelId.EggFleet, 1),
        //-6000 2471 -8395
        new(Team.SuperHardMode, LevelId.EggFleet, 2),
        //-7750 1365 -20610
        new(Team.SuperHardMode, LevelId.EggFleet, 3),
        //-7714 -3062.9 -29300
        new(Team.SuperHardMode, LevelId.EggFleet, 4),
        //-9500 -4213.4 -38470
        
        
        new(Team.SuperHardMode, LevelId.FinalFortress, 1),
        //2250.01 6270 44010.02
        new(Team.SuperHardMode, LevelId.FinalFortress, 2),
        //2250 5400 33620.06
        new(Team.SuperHardMode, LevelId.FinalFortress, 3),
        //2350 -6439.90 10930.05
    };
    
    
    public static List<CheckPointPriority> DarkCheckpoints = new()
    {
        new(Team.Dark, LevelId.SeasideHill, 0),
        //-2155 480 -6425
        new(Team.Dark, LevelId.SeasideHill, 1),
        //-112.1187 156.1652 -16150
        new(Team.Dark, LevelId.SeasideHill, 4),
        //900 600 -20595
        new(Team.Dark, LevelId.SeasideHill, 5),
        //880.9 2000 -33750.5
        
        
        new(Team.Dark, LevelId.OceanPalace, 0),
        //750 810 -10302
        new(Team.Dark, LevelId.OceanPalace, 1),
        //-480.06 175 -18730
        new(Team.Dark, LevelId.OceanPalace, 2),
        //2398.8208 75 -24800.24
        new(Team.Dark, LevelId.OceanPalace, 3),
        //2100 50 -31690.03
        new(Team.Dark, LevelId.OceanPalace, 5),
        //800 515 -36010.03
        
        
        new(Team.Dark, LevelId.GrandMetropolis, 0),
        //-135 -1299.9 -10105
        new(Team.Dark, LevelId.GrandMetropolis, 1),
        //-335 -4699.9 -20207
        new(Team.Dark, LevelId.GrandMetropolis, 2),
        //3400 -3359.9 -33430
        new(Team.Dark, LevelId.GrandMetropolis, 3),
        //2649.514 -2369.9 -41060.52
        
        
        new(Team.Dark, LevelId.PowerPlant, 1),
        //5775.577 2320 -3946.233
        new(Team.Dark, LevelId.PowerPlant, 2),
        //13466.96 4640 -12727.36
        new(Team.Dark, LevelId.PowerPlant, 3),
        //14504 5790 -13923.48
        new(Team.Dark, LevelId.PowerPlant, 4),
        //20412.18 7690 -12387.67
        
        
        new(Team.Dark, LevelId.CasinoPark, 1),
        //-3190.092 -39.9 -2320
        new(Team.Dark, LevelId.CasinoPark, 2),
        //-7250.528 -649.9 -550.4929
        new(Team.Dark, LevelId.CasinoPark, 3),
        //-6480 160 1360.049
        
        
        new(Team.Dark, LevelId.BingoHighway, 1),
        //680 -2259.9 -6170.08
        new(Team.Dark, LevelId.BingoHighway, 2),
        //680 -2689.9 -7029.155
        new(Team.Dark, LevelId.BingoHighway, 3),
        //500 -5394.9 -13793.82
        new(Team.Dark, LevelId.BingoHighway, 5),
        //8280.059 -14352.9 -18490.49
        
        
        new(Team.Dark, LevelId.RailCanyon, 1),
        //885.1667 28167.7 -24285.14
        new(Team.Dark, LevelId.RailCanyon, 2),
        //-6945.02 25412.4 -26730.76
        new(Team.Dark, LevelId.RailCanyon, 4),
        //-17560.62 24201 -25440.06
        new(Team.Dark, LevelId.RailCanyon, 5),
        //-35870.75 17371 -21000.85
        new(Team.Dark, LevelId.RailCanyon, 6, -39004.93f, 16494.9f, -20625.45f),
        //Duplicate Priority
        //-39004.93 16494.9 -20625.45
        new(Team.Dark, LevelId.RailCanyon, 6, -52560.83f, 13367.79f, -20100.75f),
        //Duplicate Priority
        //-52560.83 13367.79 -20100.75
        
        
        new(Team.Dark, LevelId.BulletStation, 1),
        //-1720.75 2625.04 -2540.59
        new(Team.Dark, LevelId.BulletStation, 3),
        //49830.38 1910 -6180.549
        new(Team.Dark, LevelId.BulletStation, 5),
        //83079.46 910 -8556.479
        new(Team.Dark, LevelId.BulletStation, 6),
        //115500.4 194 -7139.779
        new(Team.Dark, LevelId.BulletStation, 10),
        //99600.2 1000 -6942.058
        
        new(Team.Dark, LevelId.FrogForest, 2),
        //0.076 1040 -5410.125
        new(Team.Dark, LevelId.FrogForest, 4),
        //-1045.003 -0.0007 -14740.66
        new(Team.Dark, LevelId.FrogForest, 5),
        //Mutually exclusive with 6
        //-1993.793 -699.9 -23210.49
        //new(Team.Dark, LevelId.FrogForest, 6),
        //mutually exclusive with 5
        //-2024.099 -1099.9 -23137.62
        
        
        new(Team.Dark, LevelId.LostJungle, 1),
        //-476.093 225 -1829.312
        new(Team.Dark, LevelId.LostJungle, 2),
        //-1.4890196 1180 -6201.9697
        new(Team.Dark, LevelId.LostJungle, 3),
        //-1110.0771 250 -11997.056
        new(Team.Dark, LevelId.LostJungle, 4),
        //-6260.0044 100 -11785.068
        new(Team.Dark, LevelId.LostJungle, 5),
        //-12480.084 1880 -13100.067
        
        
        new(Team.Dark, LevelId.HangCastle, 1),
        //60.000786 -2349.9001 -6780.0757
        new(Team.Dark, LevelId.HangCastle, 2),
        //260.06445 -1969.9 -8740.041
        new(Team.Dark, LevelId.HangCastle, 4),
        //-700.0119 -1879.9 -13060
        
        
        new(Team.Dark, LevelId.MysticMansion, 1),
        //1000.06903 420 -1650.007
        new(Team.Dark, LevelId.MysticMansion, 2),
        //1230.0764 -4449.9 -18710.809
        new(Team.Dark, LevelId.MysticMansion, 4),
        //6340.0044 -3979.9001 -21970.074
        new(Team.Dark, LevelId.MysticMansion, 5),
        //15420 -9090 -39680.023
        
        
        new(Team.Dark, LevelId.EggFleet, 0),
        //-4930.0050 600 -6519.2650
        new(Team.Dark, LevelId.EggFleet, 1),
        //-6000 2471 -8395
        new(Team.Dark, LevelId.EggFleet, 2),
        //-7750 1365 -20610
        new(Team.Dark, LevelId.EggFleet, 3),
        //-7714 -3062.9 -29300
        new(Team.Dark, LevelId.EggFleet, 4),
        //-9500 -4213.4 -38470
        
        
        new(Team.Dark, LevelId.FinalFortress, 1),
        //2250.01 6270 44010.02
        new(Team.Dark, LevelId.FinalFortress, 2),
        //2250 5400 33620.06
        new(Team.Dark, LevelId.FinalFortress, 3),
        //2350 -6439.90 10930.05
    };
    
    
    public static List<CheckPointPriority> RoseCheckpoints = new()
    {
        //new(Team.Rose, LevelId.SeasideHill, 0),
        //before start
        //0 110 -4215
        //new(Team.Rose, LevelId.SeasideHill, 1),
        //before start
        //-4509.3830 180 -6922.28
        new(Team.Rose, LevelId.SeasideHill, 1),
        //-150.036 154.07 -16150.02
        new(Team.Rose, LevelId.SeasideHill, 4),
        //900 600 -20515
        //new(Team.Rose, LevelId.SeasideHill, 5),
        //after goal
        //880.9 2000 -33754.08
        
        
        new(Team.Rose, LevelId.OceanPalace, 0),
        //750 810 -10302
        new(Team.Rose, LevelId.OceanPalace, 1),
        //2398.8208 75 -24800.24
        //new(Team.Rose, LevelId.OceanPalace, 3),
        //after goal
        //2100.043 50 -31690.03
        //new(Team.Rose, LevelId.OceanPalace, 10),
        //after goal
        //800 1515 -36010.03
        
        new(Team.Rose, LevelId.GrandMetropolis, 0),
        //-135 -1299.9 -10105
        new(Team.Rose, LevelId.GrandMetropolis, 1),
        //-335 -4699.9 -20207
        new(Team.Rose, LevelId.GrandMetropolis, 2),
        //3400 -3359.9 -33430
        //new(Team.Rose, LevelId.GrandMetropolis, 3),
        //past goal
        //2649.514 -2369.9 -41030.52
        
        
        new(Team.Rose, LevelId.PowerPlant, 0),
        //12818.32 3690 -11001.39
        new(Team.Rose, LevelId.PowerPlant, 1),
        //14438.32 5790 -13932.72
        
        
        new(Team.Rose, LevelId.CasinoPark, 1),
        //-3190.092 -39.9 -2320
        //new(Team.Rose, LevelId.CasinoPark, 3),
        //After Goal
        //-6480 160 1360.049
        
        
        new(Team.Rose, LevelId.BingoHighway, 1),
        //680 -2259.9 -6170.08
        new(Team.Rose, LevelId.BingoHighway, 2),
        //680 -2689.9 -7029.155
        new(Team.Rose, LevelId.BingoHighway, 3),
        //500 -5394.9 -13793.82
        
        
        new(Team.Rose, LevelId.RailCanyon, 1),
        //885.1667 28167.7 -24285.14
        new(Team.Rose, LevelId.RailCanyon, 2),
        //-6778.106 25195.37 -26802.91
        new(Team.Rose, LevelId.RailCanyon, 4),
        //-17560.62 24201 -25440.057
        new(Team.Rose, LevelId.RailCanyon, 5),
        //-35870.75 17371 -21000.85
        //new(Team.Rose, LevelId.RailCanyon, 6),
        //After Goal
        //-52560.83 13367.79 -20100.75
        
        
        new(Team.Rose, LevelId.BulletStation, 1),
        //-1720.75 2625.04 -2540.59
        new(Team.Rose, LevelId.BulletStation, 3),
        //49830.38 1910 -6180.549
        //new(Team.Rose, LevelId.BulletStation, 5),
        //after Goal
        //83079.46 910 -8556.479
        //new(Team.Rose, LevelId.BulletStation, 6),
        //after Goal
        //115500.4 194 -7139.779
        //new(Team.Rose, LevelId.BulletStation, 10),
        //after Goal
        //99600.2 1000 -6942.058
        
        
        new(Team.Rose, LevelId.FrogForest, 2),
        //0.076 1040 -5410.125
        new(Team.Rose, LevelId.FrogForest, 4),
        //-1045.003 -0.0007 -14740.66
        //new(Team.Rose, LevelId.FrogForest, 5),
        //after Goal
        //-1993.793 -699.9 -23210.49
        //new(Team.Rose, LevelId.FrogForest, 6),
        //after Goal
        //-2024.099 -1099.9 -23137.62
        
        
        //new(Team.Rose, LevelId.LostJungle, 1),
        //before start of level
        //-150.009 225 -1760.8079
        new(Team.Rose, LevelId.LostJungle, 2),
        //-1.4890196 1180 -6201.9697
        new(Team.Rose, LevelId.LostJungle, 3),
        //-1110.0771 250 -11997.056
        //new(Team.Rose, LevelId.LostJungle, 4),
        //after Goal
        //-6260.0044 100 -11785.068
        //new(Team.Rose, LevelId.LostJungle, 5),
        //after Goal
        //-12480.084 1880 -13100.067
        
        
        new(Team.Rose, LevelId.HangCastle, 1),
        //60.000786 -2349.9001 -7210.0760
        new(Team.Rose, LevelId.HangCastle, 2),
        //260.06445 -1969.9 -8740.041
        //new(Team.Rose, LevelId.HangCastle, 4),
        //after goal
        //-700.0119 -1879.9 -13060
        
        
        
        new(Team.Rose, LevelId.MysticMansion, 1),
        //1000.06903 420 -1650.007
        new(Team.Rose, LevelId.MysticMansion, 2),
        //1230.0764 -4449.9 -18710.809
        //new(Team.Rose, LevelId.MysticMansion, 4),
        //after goal
        //6560 -3679.9 -21970.07
        //new(Team.Rose, LevelId.MysticMansion, 5),
        //after goal
        //15420 -9090 -39680.02
        
        
        new(Team.Rose, LevelId.EggFleet, 0),
        //-7947.5 -26.7 -18410
        new(Team.Rose, LevelId.EggFleet, 1),
        //-7750 1565.44 -20755
        new(Team.Rose, LevelId.EggFleet, 2),
        //-9500 -4213.40 -38320
        
        
        new(Team.Rose, LevelId.FinalFortress, 1),
        //2250.01 6270 44010.02
        new(Team.Rose, LevelId.FinalFortress, 2),
        //2250 5400 33620.06
        //new(Team.Rose, LevelId.FinalFortress, 3),
        //after goal ring
        //2350 -6439.90 10930.05
    };
    
    
    public static List<CheckPointPriority> ChaotixCheckpoints = new()
    {
        new(Team.Chaotix, LevelId.SeasideHill, 0),
        //-2243.161 480 -6425
        new(Team.Chaotix, LevelId.SeasideHill, 2),
        //-4511.019 400 -13565.43
        new(Team.Chaotix, LevelId.SeasideHill, 3),
        //-140.7085 154.1362 -16150.95
        new(Team.Chaotix, LevelId.SeasideHill, 4),
        //900 600 -20605
        
        
        new(Team.Chaotix, LevelId.OceanPalace, 0),
        //750 810 -10302
        //new(Team.Chaotix, LevelId.OceanPalace, 1),
        //coords swapped with 2
        //flower warps past
        //2398.8208 75 -24800.24
        new(Team.Chaotix, LevelId.OceanPalace, 2),
        //coords swapped with 1
        //-480.06 175 -18730
        //new(Team.Chaotix, LevelId.OceanPalace, 3),
        //flower warps past
        //2100 50 -31690.03
        
        
        new(Team.Chaotix, LevelId.GrandMetropolis, 0),
        //-135 -1299.9 -10105
        new(Team.Chaotix, LevelId.GrandMetropolis, 1),
        //-335 -4699.9 -20207
        new(Team.Chaotix, LevelId.GrandMetropolis, 2),
        //3405 -4649.9 -28195
        new(Team.Chaotix, LevelId.GrandMetropolis, 3),
        //3400 -3359.9 -33430
        
        
        new(Team.Chaotix, LevelId.PowerPlant, 0),
        //5775.8590 2320 -3946.674
        new(Team.Chaotix, LevelId.PowerPlant, 1),
        //12816.42 3690 -11001.45
        new(Team.Chaotix, LevelId.PowerPlant, 2),
        //16416.04, 5625, -12972.35
        
        
        new(Team.Chaotix, LevelId.CasinoPark, 1),
        //-3190.092 -39.9 -2320
        new(Team.Chaotix, LevelId.CasinoPark, 2),
        //-7350.401 -750 -785.127
        new(Team.Chaotix, LevelId.CasinoPark, 3),
        //-6480 160 1360.049
        
        
        new(Team.Chaotix, LevelId.BingoHighway, 1),
        //680 -2259.9 -6170.08
        new(Team.Chaotix, LevelId.BingoHighway, 3),
        //500 -5394.9 -13793.82
        
        
        new(Team.Chaotix, LevelId.RailCanyon, 1),
        //885.1667 28167.7 -24285.14
        new(Team.Chaotix, LevelId.RailCanyon, 2),
        //-6945.02 25412.4 -26730.76
        new(Team.Chaotix, LevelId.RailCanyon, 4),
        //-16270.621 24201 -25440.057
        new(Team.Chaotix, LevelId.RailCanyon, 5),
        //-35870.75 17371 -21000.85
        new(Team.Chaotix, LevelId.RailCanyon, 6),
        //-40500.07 16300 -20820.064
        //new(Team.Chaotix, LevelId.RailCanyon, 6, false, -52560.83f, 13367.79f, -20100.75f),
        //Does Not Exist
        //-52560.83 13367.79 -20100.75
        
        
        //new(Team.Chaotix, LevelId.BulletStation, 1),
        //before start of level
        //-1720.75 2625.04 -2540.59
        //new(Team.Chaotix, LevelId.BulletStation, 3),
        //before start of level
        //49830.38 1910 -6180.549
        new(Team.Chaotix, LevelId.BulletStation, 5),
        //83079.46 910 -8556.479
        new(Team.Chaotix, LevelId.BulletStation, 6),
        //79720.79 0 -12440.07
        new(Team.Chaotix, LevelId.BulletStation, 7),
        //115500.4 194 -7139.779
        new(Team.Chaotix, LevelId.BulletStation, 10),
        //100550 619.8502 -6960
        
        
        new(Team.Chaotix, LevelId.FrogForest, 2),
        //0.076 1040 -5410.125
        new(Team.Chaotix, LevelId.FrogForest, 4),
        //-1045.003 -0.0007 -14740.66
        new(Team.Chaotix, LevelId.FrogForest, 6),
        //-766.8029 -1199.9 -23996.85
        
        
        //new(Team.Chaotix, LevelId.LostJungle, 1),
        //OOB
        //-150.009 225 -1760.8079
        new(Team.Chaotix, LevelId.LostJungle, 2),
        //-1.4890196 1180 -6201.9697
        new(Team.Chaotix, LevelId.LostJungle, 3),
        //-1110.0771 250 -11997.056
        //new(Team.Chaotix, LevelId.LostJungle, 4),
        //OOB
        //-6260.0044 100 -11785.068
        
        
        new(Team.Chaotix, LevelId.HangCastle, 2),
        //260.06445 -1969.9 -8740.041
        new(Team.Chaotix, LevelId.HangCastle, 4),
        //-700.0119 -1879.9 -13060
        
        new(Team.Chaotix, LevelId.MysticMansion, 1),
        //1000.06903 420 -1650.007
        new(Team.Chaotix, LevelId.MysticMansion, 2),
        //1230.0764 -4449.9 -18710.809
        //new(Team.Chaotix, LevelId.MysticMansion, 4),
        //OOB
        //6560.0044 -3679.9001 -21970.074
        new(Team.Chaotix, LevelId.MysticMansion, 9),
        //2830.0042 -3319.9001 -18998.008
        new(Team.Chaotix, LevelId.MysticMansion, 11),
        //4930 -3319.9001 -24800.012
        
        new(Team.Chaotix, LevelId.EggFleet, 0),
        //0 -99.40 -3550
        new(Team.Chaotix, LevelId.EggFleet, 1),
        //-2849.0000 800.0000 -4340.0000
        new(Team.Chaotix, LevelId.EggFleet, 2),
        //-4930.0000 600.0000 -6460.0000
        new(Team.Chaotix, LevelId.EggFleet, 3),
        //-8870.0020 -8870.0020 -19693.0000
        
        new(Team.Chaotix, LevelId.FinalFortress, 1),
        //2250.01 6270 44050.02
        new(Team.Chaotix, LevelId.FinalFortress, 2),
        //2250 5400 33620.06
        new(Team.Chaotix, LevelId.FinalFortress, 3),
        //2350 -6439.90 10930.05
    };
    
    
    /// <summary>
    /// List of all Checkpoints In order of Sonic, Dark, Rose, Chaotix, SuperHardMode.
    /// Can use the index to determine Location ID.
    /// </summary>
    public static readonly List<CheckPointPriority>
        AllCheckpoints = SonicCheckpoints
            .Concat(DarkCheckpoints).Concat(RoseCheckpoints).Concat(ChaotixCheckpoints).Concat(SuperHardCheckpoints).ToList();


}


/*
   Sonic
   GM
   1 OOB
   
   RC
   1 mutally exclusive
   
   BS
   1 before start
   
   Frog
   1 mutally exclusive
   
   
   
   
   
   SuperHard
   
   RC
   1 mutally exclusive
   
   BS
   1 before start
   
   MM
   1 OOB
   
   
   
   Dark
   
   Frog
   1 mutally exclusive
   
   
   
   Rose
   
   SH
   2 before start
   1 after goal
   
   OP
   2 after goal
   
   GM
   2 after goal
   
   CP
   1 after goal
   
   RC
   1 after goal
   
   BS
   3 after goal
   
   Frog
   2 after goal
   
   LJ
   2 after goal
   
   MM
   2 after goal
   
   EF
   1 at start position (lol)
   
   Final
   1 after goal
   
   
   Chaotix
   
   OP
   2 skipped by warp flower (OOB)
   
   RC
   1 does not exist
   
   BS
   2 before start of level
   */
