
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;

namespace Sonic_Heroes_AP_Client.Sanity.Checkpoints;

public static class CheckpointGameWrites
{
    public static void SetCheckPointPriorityWrite(bool value)
    {
        if (!value) 
            return;
        var bytes = new byte[] { 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x23996, bytes);
    }
}