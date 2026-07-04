
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.BingoChips;

public static class BingoChipSanityHandler
{
    
    public static unsafe void HandleBingoChip(int esi, string taskName)
    {
        try
        {
            var staticPtr = (int*)*(int*)(esi + 0x2C);
            ObjSpawnData* chip = (ObjSpawnData*) staticPtr;
            
            //var chipNum = *(byte*)*(varPtr + 0x4);
            var chipNum = *(byte*)(chip->PtrVars + 0x4);
            
            LoggingHandler.LogMessage($"Congrats on Getting Chip! VarPtr: 0x{chip->PtrVars:x} ChipNum: {chipNum} LinkID: {chip->LinkId} Static Addr: 0x{(int)staticPtr:x} Dynamic Ptr: 0x{chip->PtrDynamicMem:x}", taskName, LogLevel.Info);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
}