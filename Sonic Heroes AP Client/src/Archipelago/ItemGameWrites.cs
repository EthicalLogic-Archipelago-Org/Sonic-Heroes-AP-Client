
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class ItemGameWrites
{
    
    /// <summary>
    /// Gives a Level to the specified Character.
    /// MUST be in-game.
    /// </summary>
    /// <param name="type">LevelUpType to give level up to</param>
    public static unsafe void GiveLevelUp(LevelUpType type)
    {
        var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
        var ptr = (byte*)(baseAddress + 0x208 + (int)type);
        var currentValue = *ptr;
        if (currentValue > 2)
            return;
        *ptr = (byte)(currentValue + 1);
    }
    
    /// <summary>
    /// Fills the Team Blast Gauge.
    /// If not in-game, then bar is reset to 0 when entering level (undoing this)
    /// </summary>
    public static unsafe void HandleTeamBlastFiller()
    {
        var baseAddress = (float*)((int)Mod.ModuleBase + 0x5DD72C);
        *baseAddress += 100;
    }



    /// <summary>
    /// Sets the Stealth byte.
    /// Must be In-Game.
    /// </summary>
    /// <param name="value">0 for none, 1 for stealth, 2 for frog stealth</param>
    public static unsafe void SetStealth(byte value)
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x6777E4);
            *(byte*)(baseAddr + 0x25) = value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static unsafe void SetFreeze(FreezeType freezeType)
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x6777E4);
            *(byte*)(baseAddr + 0x1F) = (byte)freezeType;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    public static unsafe void SetNoSwap(int seconds)
    {
        try
        {
            var duration = seconds * 60;
            var baseAddr = *(int*)(Mod.ModuleBase + 0x64C268);
            *(short*)(baseAddr + 0x204) = (short)duration;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}