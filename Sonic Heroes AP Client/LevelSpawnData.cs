
using System.Numerics;

namespace Sonic_Heroes_AP_Client;

public class LevelSpawnEntry(float x, float y, float z, ushort pitch = 0x0080, SpawnMode mode = SpawnMode.Normal, ushort runningtime = 0, bool secret = false, bool isdefault = false, bool bonusstage = false, ushort paddingShort = 0x0000)
{
    public Vector3 Pos = new Vector3(x, y, z);
    public ushort Pitch = pitch;
    public SpawnMode Mode = mode;
    public ushort RunningTime = runningtime;
    public bool Secret = secret;
    public bool Bonusstage = bonusstage;
    public bool IsDefault = isdefault;
    public ushort PaddingShort = paddingShort;

    public override string ToString()
    {
        return $"Pos: {this.Pos}, Pitch: {this.Pitch}, Mode: {this.Mode},  RunningTime: {this.RunningTime}, Secret: {this.Secret}, BonusStage: {this.Bonusstage}, IsDefault: {this.IsDefault}, PaddingShort: {this.PaddingShort}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not LevelSpawnEntry entry)
            return false;

        return Vector3.Distance(this.Pos, entry.Pos) < 100;
    }
}

public class LevelSpawnData
{
    public Dictionary<Team, Dictionary<LevelId, List<LevelSpawnEntry>>> AllSpawnData = new ()
    {
        {
            Team.Sonic,
            new ()
            {
                {
                    LevelId.SeasideHill, 
                    [
                        new LevelSpawnEntry(0, 20, 2960, mode: SpawnMode.Running, runningtime: 370, isdefault: true),
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
                        //new LevelSpawnEntry(0, 480, 855, mode:SpawnMode.Running, runningtime:400, isdefault:true),
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
                        new LevelSpawnEntry(-2024.099f, -1099.9f, -23137.62f),
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
                        //this spawn pos is super annoying please help
                        new LevelSpawnEntry(500, 4000, 2800, isdefault:true),
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
                        new LevelSpawnEntry(0, 20, 0, isdefault:true),
                    ]
                },
                {
                    LevelId.MetalOverlord, 
                    [
                        new LevelSpawnEntry(-999.20f, 20, -1025.00f, isdefault:true)
                    ]
                },
                {
                    LevelId.SeaGate, 
                    [
                        new LevelSpawnEntry(-999.20f, 20, -1025.00f, isdefault:true)
                    ]
                }
            }
        },
    };

    public bool ShouldIncludeSecret(Team team, LevelId level)
    {
        return false;
    }

    public void BonusStageUnlockCallback(Team team, LevelId level, int keynum = 0, bool goal = false)
    {
        try
        {
            if (keynum == 0 && !goal)
                return;

            if (keynum > 0)
            {
                Mod.SaveDataHandler!.CustomSaveData!.BonusKeysPickedUp[team][level][keynum - 1] = true;
                if (Mod.AbilityUnlockHandler.GetIfLevelGoaled(team, level))
                {
                    //unlocking bonus stage spawn here
                    Console.WriteLine($"Unlocking Bonus Stage Spawn for {team} {level}");
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
            }

            if (goal)
            {
                //Mod.SaveDataHandler!.CustomSaveData!.LevelsGoaled[team][level] =  true;
                
                int keys = 0;

                foreach (var key in Mod.SaveDataHandler.CustomSaveData.BonusKeysPickedUp[team][level])
                {
                    if (key)
                        keys++;
                }

                if (keys > 0)
                {
                    //unlocking bonus stage spawn here
                    Console.WriteLine($"Unlocking Bonus Stage Spawn for {team} {level}");
                    Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level]
                        [Mod.SaveDataHandler.CustomSaveData.SpawnDataUnlocks[team][level].Count - 1] = true;
                }
                
            }
            
            Mod.ArchipelagoHandler!.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public List<LevelSpawnEntry> GetAllSpawnDataForLevel(Team team, LevelId level)
    {
        //Console.WriteLine($"Get All SpawnData for Team {team} Level {level}.");
        
        if (!AllSpawnData.ContainsKey(team))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data.");
            return new () { };
        }
        if (!AllSpawnData[team].ContainsKey(level))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
            return new () { };
        }
        
        return AllSpawnData[team][level];
    }
    

    public List<LevelSpawnEntry> GetUnlockedSpawnData(Team team, LevelId level)
    {
        //Console.WriteLine($"Get Unlocked SpawnData for Team {team} Level {level}.");
        
        if (!AllSpawnData.ContainsKey(team))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data.");
            return [];
        }
        if (!AllSpawnData[team].ContainsKey(level))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
            return [];
        }

        if (ShouldIncludeSecret(team, level))
        {
            return AllSpawnData[team][level].Where((x, index) => Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] ).ToList();
        }
        
        return AllSpawnData[team][level].Where((x, index) => !x.Secret && Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] ).ToList();
    }

    public void UnlockSpawnData(Team team, LevelId level, int index, bool secret = false)
    {
        if (!AllSpawnData.ContainsKey(team))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data.");
            return;
        }
        if (!AllSpawnData[team].ContainsKey(level))
        {
            //Console.WriteLine($"Team {team} does not have any spawn data for Level {level}.");
            return;
        }
        
        Mod.SaveDataHandler!.CustomSaveData!.SpawnDataUnlocks[team][level][index] = true;
        
        var entry = AllSpawnData[team][level][index];
        //Console.WriteLine($"Unlocked spawn data for Team {team} and Level {level}. Pos is {entry.Pos}, Index in List is {AllSpawnData[team][level].IndexOf(entry)}, Index is {index}");
        Mod.ArchipelagoHandler!.Save();
    }


    public unsafe void HandleInput(bool up)
    {
        var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
        var levelIndex = *(int*)(levelSelectPtr + 0x194);
        if (levelIndex is < 0 or > 21)
            return;
        
        var level = (LevelId)Mod.UserInterface!.LevelTracker.LevelMapping[levelIndex];
        var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
        
        var actPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
        var actIndex = *(int*)(actPtr + 0x2BC);
        
        var team = (Team)storyIndex;
        
        //Console.WriteLine($"HandleInput Here: Team {team} Level {level}");
        
        var entries = GetUnlockedSpawnData(team, level);
        var allentries = GetAllSpawnDataForLevel(team, level);

        if (entries.Count < 2)
        {
            Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
            return;
        }

        if (!up)
        {
            int unlockedindex = entries.IndexOf(allentries[Mod.LevelSpawnHandler!.SpawnPosIndex]);
            
            if  (unlockedindex <= 0)
                unlockedindex = entries.Count;
            
            unlockedindex--;
            //Console.WriteLine($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}");
            Mod.LevelSpawnHandler.SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
        }

        else
        {
            int unlockedindex = entries.IndexOf(allentries[Mod.LevelSpawnHandler!.SpawnPosIndex]);
            
            if  (unlockedindex >= entries.Count - 1)
                unlockedindex = -1;
            
            unlockedindex++;
            //Console.WriteLine($"Spawn pos Index is: {Mod.LevelSpawnHandler.SpawnPosIndex}, Unlocked index is: {unlockedindex}");
            Mod.LevelSpawnHandler.SpawnPosIndex = allentries.IndexOf(entries[unlockedindex]);
        }
    }

    public unsafe void PrintUnlockedSpawnData()
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            if (levelIndex is < 0 or > 21)
            {
                //Console.WriteLine($"Level {levelIndex} is out of range.");
            }
            
            var level = (LevelId)Mod.UserInterface!.LevelTracker.LevelMapping[levelIndex];
            var storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
            
            var actPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(actPtr + 0x2BC);
            
            var team = (Team)storyIndex;
            
            var entries = GetUnlockedSpawnData(team, level);
            
            var allentries = GetAllSpawnDataForLevel(team, level);
            
            //Console.WriteLine($"Unlocked spawn data for Team {team} and Level {level}. ");
            foreach (var entry in entries)
            {
                //Console.WriteLine($"{entry}, ");
            }
            
            //Console.WriteLine($"All Entries: {allentries.Count}");
            foreach (var entry in allentries)
            {
                //Console.WriteLine($"{entry}, ");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    

    public string GetLevelSelectUiText(Team team, LevelId level)
    {
        try
        {
            if (GetAllSpawnDataForLevel(team, level).Count <= Mod.LevelSpawnHandler!.SpawnPosIndex)
                Mod.LevelSpawnHandler.SpawnPosIndex = 0;
            
            var unlockedSpawnEntries = GetUnlockedSpawnData(team, level);
            
            //Console.WriteLine($"GetLevelSelectUIText Team {team} and Level {level} :: {unlockedSpawnEntries.Count}");

            if (unlockedSpawnEntries.Count <= 1) 
                return "Start of Level";
            
            
            if (Mod.LevelSpawnHandler!.SpawnPosIndex == 0)
            {
                return "Start of Level";
            }

            if (GetAllSpawnDataForLevel(team, level)[Mod.LevelSpawnHandler!.SpawnPosIndex].Bonusstage)
                return "Bonus Stage";
                    
            var result = $"Checkpoint: {Mod.LevelSpawnHandler!.SpawnPosIndex}";

            if (GetAllSpawnDataForLevel(team, level)[Mod.LevelSpawnHandler!.SpawnPosIndex].Secret)
            {
                result += $" SECRET!";
                if (!Mod.IsDebug)
                    return "Start of Level";
            }
                        
            return result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return "Start of Level";
        
    }
}