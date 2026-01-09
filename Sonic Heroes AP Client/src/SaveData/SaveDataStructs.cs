
using System.Runtime.InteropServices;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.SaveData;


/// <summary>
/// How the Game saves the time for level completions
/// </summary>
public struct TimeData 
{
    public byte Minutes;
    public byte Seconds;
    public byte Milliseconds;
};

/// <summary>
/// 
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct LongLevelGameSaveData 
{
    [FieldOffset(0x0)]
    public short Rings;
    [FieldOffset(0x4)]
    public int Score;
    [FieldOffset(0x8)]
    public TimeData Time;
    [FieldOffset(0xB)]
    public Rank Rank;
};

/// <summary>
/// 
/// </summary>
public struct ShortLevelGameSaveData 
{
    public TimeData Time;
    public Rank Rank;
};

public struct SonicLevelData 
{
    public LongLevelGameSaveData Mission1;
    public LongLevelGameSaveData Mission2;
};

public struct DarkLevelData 
{
    public LongLevelGameSaveData Mission1;
    public ShortLevelGameSaveData Mission2;
};

public struct RoseLevelData 
{
    public LongLevelGameSaveData Mission1;
    public ShortLevelGameSaveData Mission2;
};

public struct ChaotixLevelData 
{
    public LongLevelGameSaveData Mission1;
    public LongLevelGameSaveData Mission2;
};

public struct LevelSaveData 
{
    public SonicLevelData Sonic;
    public DarkLevelData Dark;
    public RoseLevelData Rose;
    public ChaotixLevelData Chaotix;
};

public struct BossData 
{
    public ShortLevelGameSaveData SonicBoss;
    public ShortLevelGameSaveData DarkBoss;
    public ShortLevelGameSaveData RoseBoss;
    public ShortLevelGameSaveData ChaotixBoss;
};


[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct SaveData
{
    private fixed byte padding1[0x22];
    public byte EmblemCount;
    public fixed byte padding2[0x19];
    public int savedLives;
    public fixed byte padding99[0xC];
    private fixed byte levelsBuffer[14 * 0x50];
    private fixed byte bossesBuffer[7 * 0x10];
    public ShortLevelGameSaveData MetalMadness;
    private fixed byte padding3[0x14C];
    public fixed int Emerald[25];
    
    public LevelSaveData* Levels
    {
        get
        {
            fixed (byte* ptr = levelsBuffer)
                return (LevelSaveData*)ptr;
        }
    }

    public BossData* Bosses
    {
        get
        {
            fixed (byte* ptr = bossesBuffer)
                return (BossData*)ptr;
        }
    }
}