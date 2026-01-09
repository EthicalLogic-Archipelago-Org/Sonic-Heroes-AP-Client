
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.BingoChip;

public static class BingoChipSanityHandler
{
    
    public static unsafe void HandleBingoChip(int esi)
    {
        try
        {
            Console.WriteLine($"Handling Bingo Chip Here");

            var staticPtr = (int*)*(int*)(esi + 0x2C);
            ObjSpawnData* chip = (ObjSpawnData*) staticPtr;
            
            //var chipNum = *(byte*)*(varPtr + 0x4);
            var chipNum = *(byte*)(chip->PtrVars + 0x4);
        
            Console.WriteLine($"Congrats on Getting Chip! VarPtr: 0x{chip->PtrVars:x} ChipNum: {chipNum} LinkID: {chip->LinkId} Static Addr: 0x{(int)staticPtr:x} Dynamic Ptr: 0x{chip->PtrDynamicMem:x}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}