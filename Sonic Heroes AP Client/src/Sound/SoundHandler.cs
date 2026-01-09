
using System.Runtime.InteropServices;

namespace Sonic_Heroes_AP_Client.Sound;

public static class SoundHandler
{
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int PlaySound(int moduleBase, int soundId);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int PlayAFSSound(int moduleBase, int soundId);
}