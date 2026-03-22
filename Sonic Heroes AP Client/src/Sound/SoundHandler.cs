
using System.Runtime.InteropServices;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Sound;

public static class SoundHandler
{
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int PlaySound(int moduleBase, int soundId);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int PlayAFSSound(int moduleBase, int soundId);
    

    public static void PlaySound(int moduleBase, int soundId, string taskName)
    {
        try
        {
            PlaySound(moduleBase, soundId);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"Error Playing Sound: 0x{soundId:x}\n{e}", taskName, LogLevel.Error);
        }
    }

    public static void PlayAFSSound(int moduleBase, int soundId, string taskName)
    {
        LoggingHandler.LogMessage($"Playing Sound Bank: 0x{soundId:x}", taskName, LogLevel.SuperDebug);
        try
        {
            PlayAFSSound(moduleBase, soundId);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"Error Playing Sound Bank: 0x{soundId:x}\n{e}", taskName, LogLevel.Error);
        }
    }
    
    
}