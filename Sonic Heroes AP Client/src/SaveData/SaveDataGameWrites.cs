

using Reloaded.Memory;
using Reloaded.Memory.Interfaces;

namespace Sonic_Heroes_AP_Client.SaveData;



public static class SaveDataGameWrites
{
    public static void RedirectSaveData(IntPtr redirectAddress)
    {
        //Console.WriteLine($"addr: {redirectAddress}");
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BAD7, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0xB)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BAF4, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x23)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB11, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x33)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB2E, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x43)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB63, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x383)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB4A, BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x4D3)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B823, BitConverter.GetBytes((int)(redirectAddress + 0x624)));
    }
}