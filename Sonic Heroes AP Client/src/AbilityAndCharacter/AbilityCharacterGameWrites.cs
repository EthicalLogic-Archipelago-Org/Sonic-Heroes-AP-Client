
using Reloaded.Memory;
using Reloaded.Memory.Enums;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;

public static class AbilityCharacterGameWrites
{
    private const byte LockedState = 0x27;
    
    /// <summary>
    /// Unlocks or Locks Character.
    /// </summary>
    /// <param name="formationChar">Which formation? (Speed, Power, Flying)</param>
    /// <param name="value">True to unlock character, false to lock</param>
    /// <param name="force">Should the character be force-unlocked? (This breaks capture state)</param>
    public static unsafe void SetCharState(FormationChar formationChar, bool value, bool force)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64B1B0 + (4 * (int)formationChar));
            var ptr = (byte*)(baseAddress + 0xF4);
            var currentState = *ptr;

            if (!value && currentState != LockedState)
                *ptr = LockedState;
        
            if (value && currentState == LockedState && force)
                *ptr = 0x00;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    /// <summary>
    /// Sets the Character's Level
    /// </summary>
    /// <param name="formationChar">Which formation? (Speed, Power, Flying)</param>
    /// <param name="level">(0, 1, 2, 3) Must be In-Game.</param>
    public static unsafe void SetCharLevel(FormationChar formationChar, byte level)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
            var charlevels = (byte*)(baseAddress + 0x208 + (byte)formationChar);
            *charlevels = level;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void SetAmyHammerHover(bool value)
    {
        var bytes = value ? new byte[] { 0x74 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1CEFD4, bytes);
    }
    
    public static void SetHomingAttack(bool value)
    {
        var bytes = value ? new byte[] { 0x85 } : new byte[] { 0x84 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1CEE71, bytes);
    }
    
    public static void SetTornado(bool value)
    {
        var bytes = value ? new byte[] { 0x75 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF093, bytes);
    }
    public static void SetLightDash(bool value)
    {
        var bytes = value ? new byte[] { 0x75 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A67DD, bytes);
    }
    
    public static void SetTriangleJump(bool value)
    {
        var bytes = value ? new byte[] { 0x74, 0x07 } : new byte[] { 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A68AD, bytes);
    }
    
    public static void SetRocketAccel(bool value)
    {
        var bytes = value ? new byte[] { 0x0F, 0x84, 0xBA, 0x01, 0x00, 0x00 } : new byte[] { 0xE9, 0xBB, 0x01, 0x00, 0x00, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A5AD6, bytes);
    }
    
    public static void SetLightAttack(bool value)
    {
        var bytes = value ? new byte[] { 0x75 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A6838, bytes);
    }
    
    public static void SetGlide(bool value)
    {
        var bytes = value ? new byte[] { 0x4F } : new byte[] { 0xCB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AED5D, bytes);
    }

    public static void SetPowerAttack(bool value)
    {
    }
    
    public static void SetFireDunk(bool value)
    {
        //Sonic and Dark
        var bytes = value ? new byte[] { 0x74 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF2D0, bytes);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF2E3, bytes);
        
        //Big and Vector
        bytes = value ?  new byte[] { 0x3F } : new byte[] { 0x04 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1C54F2, bytes);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1CF5ED, bytes);
    }
    
    public static void SetUltimateFireDunk(bool value)
    {
        var bytes = value ? new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 } : new byte[] { 0xE8, 0x0D, 0x7F, 0xFF, 0xFF };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF2FE, bytes);
    }
    
    public static void SetBellyFlop(bool value)
    {
        var bytes = value ? new byte[] { 0x0F, 0x8A, 0x2A, 0x03, 0x00, 0x00 } : new byte[] { 0xEB, 0x2B, 0x03, 0x00, 0x00, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF335, bytes);
    }
    
    public static void SetComboFinisher(bool value)
    {
        var bytes = value ? new byte[] { 0x0F, 0x85, 0xDE, 0x03, 0x00, 0x00 } : new byte[] { 0xE9, 0xDF, 0x03, 0x00, 0x00, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1BFD3B, bytes);
    }
    
    public static void SetFlying(bool value)
    {
        //Console.WriteLine($"SetFlying: {value}");
        //var bytes = value ? new byte[] { 0x34 } : new byte[] { 0x35 };
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1C9C7F, bytes);
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1CA608, bytes);
        var bytes = value ? new byte[] { 0x00, 0x00, 0x34, 0x43 } : new byte[] { 0x00, 0x00, 0x80, 0xBF };
        Memory.Instance.ChangeProtection(Mod.ModuleBase + 0x389FE4, 0x4, MemoryProtection.ReadWriteExecute);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x389FE4, bytes);
        Memory.Instance.ChangeProtection(Mod.ModuleBase + 0x389FE4, 0x4, MemoryProtection.Read);
    }
    
    public static void SetThundershoot(bool value)
    {
        //Air
        var bytes = value ? new byte[] { 0x7E } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF5EE, bytes);
        //Ground
        bytes = value ? new byte[] { 0x0F, 0x84, 0xF4, 0x00, 0x00, 0x00 } : new byte[] { 0xE9, 0xF5, 0x00, 0x00, 0x00, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF56B, bytes);
    }
    
    public static void SetDummyRings(bool value)
    {
        //var bytes = value ? new byte[] { 0x66, 0xC7, 0x85, 0xF4, 0x00, 0x00, 0x00, 0x4B, 0x00 } : new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, };
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1C866B, bytes);
        
        var bytes = value ? new byte[] { 0xE8, 0xD7, 0x7A, 0xFF, 0xFF } : new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF644, bytes);
    }
    
    
    public static void SetFlowerSting(bool value)
    {
        //in stack
        var bytes = value ? new byte[] { 0x74 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A62EA, bytes);
        //independent
        bytes = value ? new byte[] { 0xE8, 0xDB, 0x80, 0xFF, 0xFF } : new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF660, bytes);
    }
    
    public static void SetCheeseCannon(bool value)
    {
        var bytes = value ? new byte[] { 0xE8, 0x99, 0x7A, 0xFF, 0xFF } : new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF652, bytes);
    }
    
    public static void SetInvisibilty(bool value)
    {
    }
    
    public static void SetShuriken(bool value)
    {
    }
    
    /// <summary>
    /// Enables or disables the ability to team blast.
    /// </summary>
    /// <param name="value">True to enable, False to disable.</param>
    public static void SetTeamBlastWrite(bool value)
    {
        var bytes = value ? new byte[] { 0x75 } 
            : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AEB9E, bytes);
    }
}