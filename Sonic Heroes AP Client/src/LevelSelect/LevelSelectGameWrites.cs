

using Reloaded.Memory;
using Reloaded.Memory.Interfaces;

namespace Sonic_Heroes_AP_Client.LevelSelect;

/// <summary>
/// Handles Game Memory Writes for Level Select
/// </summary>
public static class LevelSelectGameWrites
{
    public static readonly UIntPtr StoryModeSwitchCaseJumpAddr = Mod.ModuleBase + 0x4FDC0;
    public static readonly UIntPtr LevelSelectSwitchCaseJumpAddr = Mod.ModuleBase + 0x4FDC4;
    public static readonly UIntPtr SuperHardModeStoryModeSwitchCaseJumpAddr = Mod.ModuleBase + 0x4FDC8;
    public static readonly UIntPtr TutorialStoryModeSwitchCaseJumpAddr = Mod.ModuleBase + 0x4FDCC;
    
    public static readonly byte[] StoryModeSwitchCaseJumpInstruction = BitConverter.GetBytes(0x0044FD60);
    public static readonly byte[] LevelSelectSwitchCaseJumpInstruction = BitConverter.GetBytes(0x0044FD6E);
    public static readonly byte[] SuperHardModeSwitchCaseJumpInstruction = BitConverter.GetBytes(0x0044FD7C);
    public static readonly byte[] TutorialSwitchCaseJumpInstruction = BitConverter.GetBytes(0x0044FD8A);



    public static void ReplaceStorySuperHardAndTutorialWithLevelSelect()
    {
        try
        {
            Memory.Instance.SafeWrite(StoryModeSwitchCaseJumpAddr, LevelSelectSwitchCaseJumpInstruction);
            Memory.Instance.SafeWrite(SuperHardModeStoryModeSwitchCaseJumpAddr, LevelSelectSwitchCaseJumpInstruction);
            Memory.Instance.SafeWrite(TutorialStoryModeSwitchCaseJumpAddr, LevelSelectSwitchCaseJumpInstruction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    
    /// <summary>
    /// Allows access to Level Select without having a beaten Level.
    /// Also removes the Level Select Emblem count from updating (it is updated by changing the count in Redirect Save Data instead)
    /// </summary>
    public static void ModifyInstructions()
    {
        ReplaceStorySuperHardAndTutorialWithLevelSelect();
        
        // Makes all menu options display visually
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x504A3, new byte[] { 0x90, 0x90 });
        // Removes emblem update
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x22F344, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x24255B, new byte[] { 0x90 });
    }
    
    /// <summary>
    /// Allows for all levels in Level Select to be entered regardless of unlock status
    /// </summary>
    /// <param name="value">True to enable, False to disable.</param>
    public static void SetLevelSelectAllLevelsAvailableWrite(bool value)
    {
        var bytes = value ? new byte[] { 0x90, 0x90 } : new byte[] { 0x74, 0x1D };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B3BE, bytes);
    }
}