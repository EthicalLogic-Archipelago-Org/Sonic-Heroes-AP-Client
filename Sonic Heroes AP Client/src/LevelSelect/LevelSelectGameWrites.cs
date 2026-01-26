

using Reloaded.Memory;
using Reloaded.Memory.Interfaces;

namespace Sonic_Heroes_AP_Client.LevelSelect;

/// <summary>
/// Handles Game Memory Writes for Level Select
/// </summary>
public static class LevelSelectGameWrites
{
    
    /// <summary>
    /// Allows access to Level Select without having a beaten Level.
    /// Also removes the Level Select Emblem count from updating (it is updated by changing the count in Redirect Save Data instead)
    /// </summary>
    public static void ModifyInstructions()
    {
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