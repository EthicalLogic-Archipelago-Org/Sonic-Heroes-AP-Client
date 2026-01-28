
using Sonic_Heroes_AP_Client.Definitions;
namespace Sonic_Heroes_AP_Client.LevelSpawnPosition;

public static class LevelSpawnData
{
    public static readonly Dictionary<Team, Dictionary<LevelId, List<LevelSpawnEntry>>> AllSpawnData = new ()
    {
        {
            Team.Sonic,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        //new LevelSpawnEntry(0, 20, 2960, mode: SpawnMode.Running, runningtime: 0x0172, isdefault: true),
                        new LevelSpawnEntry(0, 20, 2960, mode: SpawnMode.Running, runningtime: 0x00C8, isdefault: true),
                        new LevelSpawnEntry(1, 110, -4215),
                        new LevelSpawnEntry(-4509.383f, 180, -6922.28f),
                        new LevelSpawnEntry(-130.082f, 154.9283f, -16150.09f),
                        new LevelSpawnEntry(900, 600, -20605),
                        new LevelSpawnEntry(880.9f, 2000, -33754.08f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.OceanPalace, 
                    [
                        //new LevelSpawnEntry(230, 1300, 2630, isdefault:true),
                        new LevelSpawnEntry(200, 1300, 0, isdefault:true),
                        new LevelSpawnEntry(750, 810, -10302),
                        new LevelSpawnEntry(2398.8208f, 75, -24800.24f),
                        new LevelSpawnEntry(2100.043f, 50, -31690.03f),
                        new LevelSpawnEntry(800, 1515, -36010),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.GrandMetropolis, 
                    [
                        //new LevelSpawnEntry(0, 480, 855, mode:SpawnMode.Running, runningtime:0x190, isdefault:true),
                        new LevelSpawnEntry(0, -200, -2650, isdefault:true),
                        new LevelSpawnEntry(-134.99962f, -1299.9f, -10105),
                        new LevelSpawnEntry(-335.00836f, -4699.9f, -20207),
                        new LevelSpawnEntry(3405.0002f, -4649.9f, -28195),
                        new LevelSpawnEntry(3400.0005f, -3359.9001f, -33430.008f),
                        new LevelSpawnEntry(2649.5144f, -2369.9001f, -41030.516f, secret:true), //secret OOB
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.PowerPlant, 
                    [
                        new LevelSpawnEntry(0, 640, 100, isdefault:true),
                        new LevelSpawnEntry(5772.445f, 2320, -3946.1865f),
                        new LevelSpawnEntry(12816.072f, 3690, -11003.363f),
                        new LevelSpawnEntry(16507.467f, 5745, -12811.182f),
                        new LevelSpawnEntry(20412.184f, 7690, -12387.666f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.CasinoPark, 
                    [
                        new LevelSpawnEntry(0, 100, 0, isdefault:true),
                        new LevelSpawnEntry(-3190.092f, -39.9f, -2320),
                        new LevelSpawnEntry(-7250.528f, -649.9f, -550.4929f),
                        new LevelSpawnEntry(-6480, 160, 1360.049f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BingoHighway, 
                    [
                        new LevelSpawnEntry(0, 233, 400, isdefault:true),
                        new LevelSpawnEntry(680, -2259.9f, -6170.08f),
                        new LevelSpawnEntry(680, -2689.9f, -7029.155f),
                        new LevelSpawnEntry(500, -5394.9f, -13793.82f),
                        new LevelSpawnEntry(8280.059f, -14352.9f, -18490.49f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.RailCanyon, 
                    [
                        new LevelSpawnEntry(895, 32380, -16755, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(885.1667f, 28167.7f, -24285.14f),
                        new LevelSpawnEntry(-6778.1060f, 25195.37f, -26802.91f),
                        new LevelSpawnEntry(-17050.62f, 24400f, -25440.06f),
                        new LevelSpawnEntry(-35870.75f, 17371f, -21000.85f),
                        new LevelSpawnEntry(-39004.93f, 16494.9f, -20625.45f),
                        new LevelSpawnEntry(-52560.83f, 13367.79f, -20100.75f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BulletStation, 
                    [
                        new LevelSpawnEntry(50000, 3366.20f, -390, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(-150.017f, 2030f, 8022.626f),
                        new LevelSpawnEntry(83079.46f, 910, -8556.479f),
                        new LevelSpawnEntry(115500.4f, 194, -7139.779f),
                        new LevelSpawnEntry(99600.2f, 1000, -6942.058f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FrogForest, 
                    [
                        new LevelSpawnEntry(0, 3866, 70.3f, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(0.076f, 1040, -5410.125f),
                        new LevelSpawnEntry(-1045.003f, -0.0007f, -14740.66f),
                        new LevelSpawnEntry(-1993.793f, -699.9f, -23210.49f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.LostJungle, 
                    [
                        new LevelSpawnEntry(10, 400, 5, isdefault:true, paddingShort: 0xFFFF),
                        new LevelSpawnEntry(-150, 225, -1760.8079f),
                        new LevelSpawnEntry(-1.4890196f, 1180, -6201.9697f),
                        new LevelSpawnEntry(-1110.0771f, 250, -11997.056f),
                        new LevelSpawnEntry(-6260.0044f, 100, -11785.068f),
                        new LevelSpawnEntry(-12480.084f, 1880, -13100.067f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.HangCastle, 
                    [
                        //new LevelSpawnEntry(-168, -40, -930, isdefault:true, pitch:0x0014, mode: SpawnMode.Running, runningtime: 0x012C),
                        new LevelSpawnEntry(200, -800, -2050, isdefault:true),
                        new LevelSpawnEntry(60.000786f, -2349.9001f, -6780.0757f),
                        new LevelSpawnEntry(260.06445f, -1969.9f, -8740.041f),
                        new LevelSpawnEntry(-700.0119f, -1879.9f, -13060),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MysticMansion, 
                    [
                        //new LevelSpawnEntry(0, 0, 1000, isdefault:true, mode:SpawnMode.Running, runningtime: 0x0082),
                        new LevelSpawnEntry(1, 10, 450f, isdefault:true),
                        new LevelSpawnEntry(1000.06903f, 420, -1650.007f),
                        new LevelSpawnEntry(1230.0764f, -4449.9f, -18710.809f),
                        new LevelSpawnEntry(6560.0044f, -3679.9001f, -21970.074f),
                        new LevelSpawnEntry(15420, -9090, -39680.023f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.EggFleet, 
                    [
                        //new LevelSpawnEntry(500, 4200, 5250, isdefault:true, mode:SpawnMode.Running, runningtime: 0x01F4),
                        //new LevelSpawnEntry(500, 4200, 5250, isdefault:true),
                        //new LevelSpawnEntry(500, 4260, 3900, isdefault:true),
                        //new LevelSpawnEntry(500, 2900, 1430, isdefault:true, mode:SpawnMode.Rail),
                        //new LevelSpawnEntry(500, 4000, 2800, isdefault:true),
                        //this spawn pos is super annoying please help
                        new LevelSpawnEntry(595, 4260, 4464, isdefault:true),
                        new LevelSpawnEntry(-2849, 800, -4360),
                        new LevelSpawnEntry(-6000, 2471, -8285),
                        new LevelSpawnEntry(-7750, 1365, -20610),
                        new LevelSpawnEntry(-7714, -3062.9f, -29300),
                        new LevelSpawnEntry(-9500, -4213.4f, -38170),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FinalFortress, 
                    [
                        //new LevelSpawnEntry(500, 10800, 57420, isdefault:true, mode:SpawnMode.Running),
                        //new LevelSpawnEntry(2000, 8220, 49710, isdefault:true),
                        new LevelSpawnEntry(2000, 8220, 50450, isdefault:true),
                        new LevelSpawnEntry(2250.01f, 6270, 44010.02f),
                        new LevelSpawnEntry(2250, 5400, 33620.06f),
                        new LevelSpawnEntry(2350, -6439.90f, 10930.05f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MetalMadness, 
                    [
                        new LevelSpawnEntry(-6, 70, -316, isdefault:true),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(0f, 0f, 0f, isdefault:true, pitch: 0x0000)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(0f, 130, 0f, isdefault:true)
                    ]
                },
            }
        },
        {
            Team.Dark,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        new LevelSpawnEntry(0, 20, 2960, mode: SpawnMode.Running, runningtime: 0x0172, isdefault: true),
                        new LevelSpawnEntry(-2155, 480, -6425),
                        new LevelSpawnEntry(-112.1187f, 156.1652f, -16150),
                        new LevelSpawnEntry(900, 600, -20595),
                        new LevelSpawnEntry(880.9f, 2000, -33750.5f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.OceanPalace, 
                    [
                        new LevelSpawnEntry(230, 1300, 2630, isdefault:true),
                        //new LevelSpawnEntry(200, 1300, 0, isdefault:true), <- after dash ramp at start
                        new LevelSpawnEntry(750, 810, -10302),
                        new LevelSpawnEntry(-480.06f, 175, -18730),
                        new LevelSpawnEntry(2398.8208f, 75, -24800.24f),
                        new LevelSpawnEntry(2100.043f, 50, -31690.03f),
                        new LevelSpawnEntry(800, 1515, -36010),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.GrandMetropolis, 
                    [
                        new LevelSpawnEntry(0, 480, 855, mode:SpawnMode.Running, runningtime:0x190, isdefault:true),
                        //new LevelSpawnEntry(0, -200, -2650, isdefault:true),
                        new LevelSpawnEntry(-134.99962f, -1299.9f, -10105),
                        new LevelSpawnEntry(-335.00836f, -4699.9f, -20207),
                        new LevelSpawnEntry(3400.0005f, -3359.9001f, -33430.008f),
                        new LevelSpawnEntry(2649.5144f, -2369.9001f, -41060.516f, secret:true),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.PowerPlant, 
                    [
                        new LevelSpawnEntry(0, 640, 100, isdefault:true),
                        new LevelSpawnEntry(5772.445f, 2320, -3946.1865f),
                        new LevelSpawnEntry(13466.96f, 4640, -12727.36f),
                        new LevelSpawnEntry(14504f, 5790, -13923.48f),
                        new LevelSpawnEntry(20412.184f, 7690, -12387.666f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.CasinoPark, 
                    [
                        new LevelSpawnEntry(0, 100, 0, isdefault:true),
                        new LevelSpawnEntry(-3190.092f, -39.9f, -2320),
                        new LevelSpawnEntry(-7250.528f, -649.9f, -550.4929f),
                        new LevelSpawnEntry(-6480, 160, 1360.049f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BingoHighway, 
                    [
                        new LevelSpawnEntry(0, 233, 400, isdefault:true),
                        new LevelSpawnEntry(680, -2259.9f, -6170.08f),
                        new LevelSpawnEntry(680, -2689.9f, -7029.155f),
                        new LevelSpawnEntry(500, -5394.9f, -13793.82f),
                        new LevelSpawnEntry(8280.059f, -14352.9f, -18490.49f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.RailCanyon, 
                    [
                        new LevelSpawnEntry(104, 43493, 2060, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(885.1667f, 28167.7f, -24285.14f),
                        new LevelSpawnEntry(-6945.02f, 25412.4f, -26730.76f),
                        new LevelSpawnEntry(-17560.62f, 24201, -25440.06f),
                        new LevelSpawnEntry(-35870.75f, 17371f, -21000.85f),
                        new LevelSpawnEntry(-39004.93f, 16494.9f, -20625.45f),
                        new LevelSpawnEntry(-52560.83f, 13367.79f, -20100.75f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BulletStation, 
                    [
                        new LevelSpawnEntry(-772, 2571.5f, 1122, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(-150.017f, 2030f, 8022.626f),
                        new LevelSpawnEntry(83079.46f, 910, -8556.479f),
                        new LevelSpawnEntry(115500.4f, 194, -7139.779f),
                        new LevelSpawnEntry(99600.2f, 1000, -6942.058f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FrogForest, 
                    [
                        new LevelSpawnEntry(0, 3866, 70.3f, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(0.076f, 1040, -5410.125f),
                        new LevelSpawnEntry(-1045.003f, -0.0007f, -14740.66f),
                        new LevelSpawnEntry(-1993.793f, -699.9f, -23210.49f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.LostJungle, 
                    [
                        new LevelSpawnEntry(10, 400, 5, isdefault:true, paddingShort: 0xFFFF),
                        new LevelSpawnEntry(-476.093f, 225, -1829.312f),
                        new LevelSpawnEntry(-1.4890196f, 1180, -6201.9697f),
                        new LevelSpawnEntry(-1110.0771f, 250, -11997.056f),
                        new LevelSpawnEntry(-6260.0044f, 100, -11785.068f),
                        new LevelSpawnEntry(-12480.084f, 1880, -13100.067f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.HangCastle, 
                    [
                        new LevelSpawnEntry(-168, -40, -930, isdefault:true, pitch:0x1400, mode: SpawnMode.Running, runningtime: 0x012C),
                        //new LevelSpawnEntry(200, -800, -2050, isdefault:true),
                        new LevelSpawnEntry(60.000786f, -2349.9001f, -6780.0757f),
                        new LevelSpawnEntry(260.06445f, -1969.9f, -8740.041f),
                        new LevelSpawnEntry(-700.0119f, -1879.9f, -13060),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MysticMansion, 
                    [
                        new LevelSpawnEntry(0, 0, 1000, isdefault:true, mode:SpawnMode.Running, runningtime: 0x0082),
                        //new LevelSpawnEntry(1, 10, 450f, isdefault:true),
                        new LevelSpawnEntry(1000.06903f, 420, -1650.007f),
                        new LevelSpawnEntry(1230.0764f, -4449.9f, -18710.809f),
                        new LevelSpawnEntry(6340.0044f, -3979.9001f, -21970.074f),
                        new LevelSpawnEntry(15420, -9090, -39680.023f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.EggFleet, 
                    [
                        new LevelSpawnEntry(500, 4200, 5250, isdefault:true, mode:SpawnMode.Running, runningtime: 0x01F4),
                        //new LevelSpawnEntry(500, 4200, 5250, isdefault:true),
                        //new LevelSpawnEntry(500, 4260, 3900, isdefault:true),
                        //new LevelSpawnEntry(500, 2900, 1430, isdefault:true, mode:SpawnMode.Rail),
                        //this spawn pos is super annoying please help
                        //new LevelSpawnEntry(500, 4000, 2800, isdefault:true),
                        new LevelSpawnEntry(-4930.0050f, 600, -6519.2650f),
                        new LevelSpawnEntry(-6000, 2471, -8395),
                        new LevelSpawnEntry(-7750, 1365, -20610),
                        new LevelSpawnEntry(-7714, -3062.9f, -29300),
                        new LevelSpawnEntry(-9500, -4213.4f, -38470),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FinalFortress, 
                    [
                        new LevelSpawnEntry(500, 10800, 57420, isdefault:true),
                        //new LevelSpawnEntry(2000, 8220, 49710, isdefault:true),
                        //new LevelSpawnEntry(2000, 8220, 50450, isdefault:true),
                        new LevelSpawnEntry(2250.01f, 6270, 44010.02f),
                        new LevelSpawnEntry(2250, 5400, 33620.06f),
                        new LevelSpawnEntry(2350, -6439.90f, 10930.05f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MetalMadness, 
                    [
                        new LevelSpawnEntry(-6, 70, -316, isdefault:true),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(0f, 0f, 0f, isdefault:true, pitch: 0x0000)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(0f, 130, 0f, isdefault:true)
                    ]
                },
            }
        },
        {
            Team.Rose,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        new LevelSpawnEntry(-4470, 45, -10250, isdefault:true, mode: SpawnMode.Running, runningtime: 0x0064),
                        new LevelSpawnEntry(-150.036f, 154.07f, -16150),
                        new LevelSpawnEntry(900, 600, -20515),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.OceanPalace, 
                    [
                        new LevelSpawnEntry(740, 1005, -5270, isdefault:true),
                        new LevelSpawnEntry(750, 810, -10302),
                        new LevelSpawnEntry(2398.8208f, 75, -24800.24f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.GrandMetropolis, 
                    [
                        //running time is longer than Sonic kekw 0x190
                        new LevelSpawnEntry(0, 480, 855, mode:SpawnMode.Running, runningtime:0x1CC, isdefault:true),
                        //new LevelSpawnEntry(0, -200, -2650, isdefault:true), <- after dash ramp at start (in case)
                        new LevelSpawnEntry(-134.99962f, -1299.9f, -10105),
                        new LevelSpawnEntry(-335.00836f, -4699.9f, -20207),
                        new LevelSpawnEntry(3400.0005f, -3359.9001f, -33430.008f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.PowerPlant, 
                    [
                        new LevelSpawnEntry(10190, 3000, -8470, isdefault:true),
                        new LevelSpawnEntry(12818.32f, 3690, -11001.39f),
                        new LevelSpawnEntry(14438.32f, 5790, -13923.72f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.CasinoPark, 
                    [
                        new LevelSpawnEntry(80, 1250, -1450, isdefault:true),
                        new LevelSpawnEntry(-3190.092f, -39.9f, -2320),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BingoHighway, 
                    [
                        new LevelSpawnEntry(0, 233, 400, isdefault:true),
                        new LevelSpawnEntry(680, -2259.9f, -6170.08f),
                        new LevelSpawnEntry(680, -2689.9f, -7029.155f),
                        new LevelSpawnEntry(500, -5394.9f, -13793.82f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.RailCanyon, 
                    [
                        new LevelSpawnEntry(895, 32380, -16755, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(885.1667f, 28167.7f, -24285.14f),
                        new LevelSpawnEntry(-6778.106f, 25195.37f, -26802.91f),
                        new LevelSpawnEntry(-17560.62f, 24201, -25440.06f),
                        new LevelSpawnEntry(-35870.75f, 17371f, -21000.85f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BulletStation, 
                    [
                        new LevelSpawnEntry(-772, 2571.5f, 1122, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(-1720.75f, 2625.04f, -2540.59f),
                        new LevelSpawnEntry(49830.38f, 1910, -6180.549f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FrogForest, 
                    [
                        new LevelSpawnEntry(0, 3866, 70.3f, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(0.076f, 1040, -5410.125f),
                        new LevelSpawnEntry(-1045.003f, -0.0007f, -14740.66f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.LostJungle, 
                    [
                        new LevelSpawnEntry(-417.3f, 239.8f, -4065.2f, isdefault:true, paddingShort: 0xFFFF),
                        new LevelSpawnEntry(-1.4890196f, 1180, -6201.9697f),
                        new LevelSpawnEntry(-1110.0771f, 250, -11997.056f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.HangCastle, 
                    [
                        new LevelSpawnEntry(200, -740, -4120, isdefault:true),
                        new LevelSpawnEntry(60.000786f, -2349.9001f, -7210.076f),
                        new LevelSpawnEntry(260.06445f, -1969.9f, -8740.041f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MysticMansion, 
                    [
                        //longer running time than Sonic kekw 0x0082
                        new LevelSpawnEntry(0, 0, 1000, isdefault:true, mode:SpawnMode.Running, runningtime: 0x00BE),
                        new LevelSpawnEntry(1000.06903f, 420, -1650.007f),
                        new LevelSpawnEntry(1230.0764f, -4449.9f, -18710.809f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.EggFleet, 
                    [
                        new LevelSpawnEntry(500, 4200, 5250, isdefault:true, mode:SpawnMode.Running, runningtime: 0x01F4),
                        //this spawn pos is super annoying please help
                        new LevelSpawnEntry(-7947.5f, -26.7f, -18410),
                        new LevelSpawnEntry(-7750, 1565.44f, -20755),
                        new LevelSpawnEntry(-9500, -4213.4f, -38320),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FinalFortress, 
                    [
                        new LevelSpawnEntry(2000, 8496, 50440, isdefault:true, mode:SpawnMode.Running, runningtime: 0x0032),
                        new LevelSpawnEntry(2250.01f, 6270, 44010.02f),
                        new LevelSpawnEntry(2250, 5400, 33620.06f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MetalMadness, 
                    [
                        new LevelSpawnEntry(375, 70, 60, isdefault:true, pitch:0x0000),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(0f, 0f, 0f, isdefault:true, pitch: 0x0000)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(0f, 130, 0f, isdefault:true)
                    ]
                },
            }
        },
        {
            Team.Chaotix,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        new LevelSpawnEntry(0, 250, -4530, isdefault: true),
                        new LevelSpawnEntry(-2243.161f, 480, -6425),
                        new LevelSpawnEntry(-4511.019f, 400, -13565.43f),
                        new LevelSpawnEntry(-140.7085f, 154.1362f, -16150.95f),
                        new LevelSpawnEntry(900, 600, -20605f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.OceanPalace, 
                    [
                        new LevelSpawnEntry(750, 1050, -7050, isdefault:true),
                        new LevelSpawnEntry(750, 810, -10302),
                        new LevelSpawnEntry(480.06f, 175, -18730),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.GrandMetropolis, 
                    [
                        new LevelSpawnEntry(0, -500, -3700, isdefault:true),
                        new LevelSpawnEntry(-134.99962f, -1299.9f, -10105),
                        new LevelSpawnEntry(-335.00836f, -4699.9f, -20207),
                        new LevelSpawnEntry(3405, -4649.9f, -28195),
                        new LevelSpawnEntry(3400.0005f, -3359.9001f, -33430.008f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.PowerPlant, 
                    [
                        new LevelSpawnEntry(2870, 365, -2060, isdefault:true),
                        new LevelSpawnEntry(5772.445f, 2320, -3946.1865f),
                        new LevelSpawnEntry(12816.42f, 3690, -11001.45f),
                        new LevelSpawnEntry(16416.04f, 5625, -12972.35f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.CasinoPark, 
                    [
                        new LevelSpawnEntry(-1500, -730, -2140, isdefault:true),
                        new LevelSpawnEntry(-3190.092f, -39.9f, -2320),
                        new LevelSpawnEntry(-7350.401f, -750, -785.127f),
                        new LevelSpawnEntry(-6480, 160, 1360.049f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BingoHighway, 
                    [
                        new LevelSpawnEntry(0, -2000, -3250, isdefault:true),
                        new LevelSpawnEntry(680, -2259.9f, -6170.08f),
                        new LevelSpawnEntry(500, -5394.9f, -13793.82f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.RailCanyon, 
                    [
                        new LevelSpawnEntry(104, 43493, 2060, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(885.1667f, 28167.7f, -24285.14f),
                        new LevelSpawnEntry(-6945.02f, 25412.4f, -26730.76f),
                        new LevelSpawnEntry(-16270.621f, 24201, -25440.06f),
                        new LevelSpawnEntry(-35870.75f, 17371f, -21000.85f),
                        new LevelSpawnEntry(-40500.07f, 16300, -20820.064f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BulletStation, 
                    [
                        new LevelSpawnEntry(84000, 2576, -2325.7f, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(83079.46f, 910, -8556.479f),
                        new LevelSpawnEntry(79720.79f, 0, -12440.07f),
                        new LevelSpawnEntry(115500.4f, 194, -7139.779f),
                        new LevelSpawnEntry(100550, 619.8502f, -6960),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FrogForest, 
                    [
                        new LevelSpawnEntry(24.5f, 3100, -1170, isdefault:true),
                        new LevelSpawnEntry(0.076f, 1040, -5410.125f),
                        new LevelSpawnEntry(-1045.003f, -0.0007f, -14740.66f),
                        new LevelSpawnEntry(-766.8029f, -1199.9f, -23996.85f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.LostJungle, 
                    [
                        new LevelSpawnEntry(-417.3f, 239.8f, -4065.2f, isdefault:true, paddingShort: 0xFFFF),
                        new LevelSpawnEntry(-1.4890196f, 1180, -6201.9697f),
                        new LevelSpawnEntry(-1110.0771f, 250, -11997.056f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.HangCastle, 
                    [
                        new LevelSpawnEntry(200, -740, -4120, isdefault:true),
                        new LevelSpawnEntry(260.06445f, -1969.9f, -8740.041f),
                        new LevelSpawnEntry(-700.0119f, -1879.9f, -13060),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MysticMansion, 
                    [
                        new LevelSpawnEntry(0, 0, 1000, isdefault:true, mode:SpawnMode.Running, runningtime: 0x0082),
                        //new LevelSpawnEntry(1, 10, 450f, isdefault:true),
                        new LevelSpawnEntry(1000.06903f, 420, -1650.007f),
                        new LevelSpawnEntry(1230.0764f, -4449.9f, -18710.809f),
                        new LevelSpawnEntry(2830.0042f, -3319.9001f, -18998.008f),
                        new LevelSpawnEntry(4930, -3319.9001f, -24800.012f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.EggFleet, 
                    [
                        new LevelSpawnEntry(500, 2363, 790, isdefault:true),
                        new LevelSpawnEntry(0, -99.40f, -3550),
                        new LevelSpawnEntry(-2849, 800, -4340),
                        new LevelSpawnEntry(-4930, 600, -6460),
                        new LevelSpawnEntry(-8870.002f, -8870.002f, -19693),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FinalFortress, 
                    [
                        new LevelSpawnEntry(2000, 8496, 50440, isdefault:true, mode: SpawnMode.Running, runningtime: 0x0032),
                        new LevelSpawnEntry(2250.01f, 6270, 44050.02f),
                        new LevelSpawnEntry(2250, 5400, 33620.06f),
                        new LevelSpawnEntry(2350, -6439.90f, 10930.05f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MetalMadness, 
                    [
                        new LevelSpawnEntry(-340, 70, 60, isdefault:true),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(0f, 0f, 0f, isdefault:true, pitch: 0x0000)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(0f, 130, 0f, isdefault:true)
                    ]
                },
            }
        },
        {
            Team.SuperHardMode,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        new LevelSpawnEntry(0, 20, 2960, mode: SpawnMode.Running, runningtime: 0x0172, isdefault: true),
                        new LevelSpawnEntry(-2155, 480, -6425),
                        new LevelSpawnEntry(-112.1187f, 156.1652f, -16150),
                        new LevelSpawnEntry(900, 600, -20595),
                        new LevelSpawnEntry(880.9f, 2000, -33750.5f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.OceanPalace, 
                    [
                        new LevelSpawnEntry(230, 1300, 2630, isdefault:true),
                        new LevelSpawnEntry(750, 810, -10302),
                        new LevelSpawnEntry(-480.06f, 175, -18730),
                        new LevelSpawnEntry(2398.8208f, 75, -24800.24f),
                        new LevelSpawnEntry(2100.043f, 50, -31690.03f),
                        new LevelSpawnEntry(1070, 1050, -34416.93f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.GrandMetropolis, 
                    [
                        new LevelSpawnEntry(0, 480, 855, mode:SpawnMode.Running, runningtime:0x190, isdefault:true),
                        new LevelSpawnEntry(-134.99962f, -1299.9f, -10105),
                        new LevelSpawnEntry(-335.00836f, -4699.9f, -20207),
                        new LevelSpawnEntry(3400.0005f, -3359.9001f, -33430.008f),
                        new LevelSpawnEntry(2649.5144f, -2369.9001f, -41060.516f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.PowerPlant, 
                    [
                        new LevelSpawnEntry(0, 640, 100, isdefault:true),
                        new LevelSpawnEntry(5772.445f, 2320, -3946.1865f),
                        new LevelSpawnEntry(13466.96f, 4640, -12727.36f),
                        new LevelSpawnEntry(14504f, 5790, -13923.48f),
                        new LevelSpawnEntry(20412.184f, 7690, -12387.666f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.CasinoPark, 
                    [
                        new LevelSpawnEntry(0, 100, 0, isdefault:true),
                        new LevelSpawnEntry(-3190.092f, -39.9f, -2320),
                        new LevelSpawnEntry(-7250.528f, -649.9f, -550.4929f),
                        new LevelSpawnEntry(-6480, 160, 1360.049f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BingoHighway, 
                    [
                        new LevelSpawnEntry(0, 233, 400, isdefault:true),
                        new LevelSpawnEntry(680, -2259.9f, -6170.08f),
                        new LevelSpawnEntry(680, -2689.9f, -7029.155f),
                        new LevelSpawnEntry(500, -5394.9f, -13793.82f),
                        new LevelSpawnEntry(8280.059f, -14352.9f, -18490.49f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.RailCanyon, 
                    [
                        new LevelSpawnEntry(895, 32380, -16755, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(885.1667f, 28167.7f, -24285.14f),
                        new LevelSpawnEntry(-6738.1060f, 25195.37f, -26802.91f),
                        new LevelSpawnEntry(-17050.62f, 24400, -25440.06f),
                        new LevelSpawnEntry(-35870.75f, 17371f, -21000.85f),
                        new LevelSpawnEntry(-39004.93f, 16494.9f, -20625.45f),
                        new LevelSpawnEntry(-52560.83f, 13367.79f, -20100.75f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.BulletStation, 
                    [
                        new LevelSpawnEntry(50000, 3366.2f, -390, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(49830.38f, 1910, -6180.549f),
                        new LevelSpawnEntry(83079.46f, 910, -8556.479f),
                        new LevelSpawnEntry(115500.4f, 194, -7139.779f),
                        new LevelSpawnEntry(99600.2f, 1000, -6942.058f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FrogForest, 
                    [
                        new LevelSpawnEntry(0, 3866, 70.3f, isdefault:true, mode:SpawnMode.Rail),
                        new LevelSpawnEntry(0.076f, 1040, -5410.125f),
                        new LevelSpawnEntry(-1045.003f, -0.0007f, -14740.66f),
                        new LevelSpawnEntry(-2024.099f, -1099.9f, -23137.62f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.LostJungle, 
                    [
                        new LevelSpawnEntry(10, 400, 5, isdefault:true, paddingShort: 0xFFFF),
                        new LevelSpawnEntry(-476.093f, 225, -1829.312f),
                        new LevelSpawnEntry(-1.4890196f, 1180, -6201.9697f),
                        new LevelSpawnEntry(-1110.0771f, 250, -11997.056f),
                        new LevelSpawnEntry(-6260.0044f, 100, -11785.068f),
                        new LevelSpawnEntry(-12480.084f, 1880, -13100.067f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.HangCastle, 
                    [
                        new LevelSpawnEntry(-168, -40, -930, isdefault:true, pitch:0x1400, mode: SpawnMode.Running, runningtime: 0x012C),
                        //new LevelSpawnEntry(200, -800, -2050, isdefault:true),
                        new LevelSpawnEntry(60.000786f, -2349.9001f, -6780.0757f),
                        new LevelSpawnEntry(260.06445f, -1969.9f, -8740.041f),
                        new LevelSpawnEntry(-700.0119f, -1879.9f, -13060),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MysticMansion, 
                    [
                        new LevelSpawnEntry(0, 0, 1000, isdefault:true, mode:SpawnMode.Running, runningtime: 0x0082),
                        //new LevelSpawnEntry(1, 10, 450f, isdefault:true),
                        new LevelSpawnEntry(1000.06903f, 420, -1650.007f),
                        new LevelSpawnEntry(1230.0764f, -4449.9f, -18710.809f),
                        new LevelSpawnEntry(6340.0044f, -3979.9001f, -21970.074f),
                        new LevelSpawnEntry(15420, -9090, -39680.023f),
                        new LevelSpawnEntry(4930, -3319.9001f, -24800.012f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.EggFleet, 
                    [
                        new LevelSpawnEntry(500, 4200, 5250, isdefault:true, mode:SpawnMode.Running, runningtime: 0x01F4),
                        new LevelSpawnEntry(-4930.0050f, 600, -6519.2650f),
                        new LevelSpawnEntry(-6000, 2471, -8395),
                        new LevelSpawnEntry(-7750, 1365, -20610),
                        new LevelSpawnEntry(-7714, -3062.9f, -29300),
                        new LevelSpawnEntry(-9500, -4213.4f, -38470),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.FinalFortress, 
                    [
                        new LevelSpawnEntry(500, 10800, 57420, isdefault:true),
                        new LevelSpawnEntry(2250.01f, 6270, 44010.02f),
                        new LevelSpawnEntry(2250, 5400, 33620.06f),
                        new LevelSpawnEntry(2350, -6439.90f, 10930.05f),
                        new LevelSpawnEntry(-9999, -9999, -9999, bonusstage: true),
                    ]
                },
                {
                    LevelId.MetalMadness, 
                    [
                        new LevelSpawnEntry(-6, 70, -316, isdefault:true),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(0f, 0f, 0f, isdefault:true, pitch: 0x0000)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(0f, 130, 0f, isdefault:true)
                    ]
                },
            }
        },
    };
}