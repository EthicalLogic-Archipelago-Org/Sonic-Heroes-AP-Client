using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.AbilityAndCharacter;

public static class AbilityCharacterGameWrites
{
    private const byte LockedState = 0x27;

    public const float LockedFlightMeterValue = 999f;
    
    
    private static readonly UIntPtr TeamRosterRandoJumpTableAddr = Mod.ModuleBase + 0x1AAEB0;
    
    private static readonly Dictionary<Character, int> TeamRosterRandoJumpTableOffsets = new()
    {
        {Character.Sonic, 0},
        {Character.Knuckles, 0x4},
        {Character.Tails, 0x8},
        {Character.Shadow, 0xC},
        {Character.Omega, 0x10},
        {Character.Rouge, 0x14},
        {Character.Amy, 0x18},
        {Character.Big, 0x1C},
        {Character.Cream, 0x20},
        {Character.Espio, 0x24},
        {Character.Vector, 0x28},
        {Character.Charmy, 0x2C},
    };

    /// <summary>
    /// Switch Case Jump Table Values for each character. Note that Super Hard Mode and Super Sonic use the regular versions. Example for Sonic is: [0x7C, 0xAC, 0x5A, 0x00]
    /// </summary>
    private static readonly Dictionary<Character, List<byte>> TeamRosterRandoJumpTableValuessss = new()
    {
        {Character.Sonic, [0x7C, 0xAC]},
        {Character.Knuckles, [0x91, 0xAC]},
        {Character.Tails, [0xA6, 0xAC]},
        {Character.Shadow, [0xBB, 0xAC]},
        {Character.Omega, [0xCD, 0xAC]},
        {Character.Rouge, [0xE2, 0xAC]},
        {Character.Amy, [0xF7, 0xAC]},
        {Character.Big, [0x09, 0xAD]},
        {Character.Cream, [0x1B, 0xAD]},
        {Character.Espio, [0x2D, 0xAD]},
        {Character.Vector, [0x55, 0xAD]},
        {Character.Charmy, [0x7D, 0xAD]},
    };
    
    /// <summary>
    /// Switch Case Jump Table Values for each character. Note that Super Hard Mode and Super Sonic use the regular versions. Example for Sonic is: [0x7C, 0xAC, 0x5A, 0x00]
    /// </summary>
    private static readonly Dictionary<Character, ushort> TeamRosterRandoJumpTableValues = new()
    {
        {Character.Sonic, 0xAC7C},
        {Character.Knuckles, 0xAC91},
        {Character.Tails, 0xACA6},
        {Character.Shadow, 0xACBB},
        {Character.Omega, 0xACCD},
        {Character.Rouge, 0xACE2},
        {Character.Amy, 0xACF7},
        {Character.Big, 0xAD09},
        {Character.Cream, 0xAD1B},
        {Character.Espio, 0xAD2D},
        {Character.Vector, 0xAD55},
        {Character.Charmy, 0xAD7D},
    };
    
    
    
    
    
    
    /// <summary>
    /// Unlocks or Locks Character.
    /// </summary>
    /// <param name="formationChar">Which formation? (Speed, Power, Flying)</param>
    /// <param name="value">True to unlock character, false to lock</param>
    /// <param name="force">Should the character be force-unlocked? (This breaks capture state)</param>
    /// <param name="taskName">Name of the task running this</param>
    public static unsafe void SetCharState(FormationChar formationChar, bool value, bool force, string taskName)
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
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    
    public static unsafe void HandleTeamRosterRando(string taskName)
    {
        try
        {
            if (!Mod.Configuration.TeamRosterRando)
            {
                LoggingHandler.LogMessage($"Team Roster Rando is disabled, checking original bytes now", taskName, LogLevel.Debug);
                foreach (var character in Enum.GetValues<Character>())
                {
                    int addr = (int)TeamRosterRandoJumpTableAddr + TeamRosterRandoJumpTableOffsets[character];
                    ushort correctValue = TeamRosterRandoJumpTableValues[character];
                    ushort currentValue = *(ushort*)addr;
                    if (currentValue != correctValue)
                    {
                        LoggingHandler.LogMessage($"Writing Bytes: 0x{correctValue:X} to addr: 0x{addr:X}", taskName, LogLevel.SuperDebug);
                        Memory.Instance.SafeWrite((UIntPtr)addr, BitConverter.GetBytes(correctValue));
                    }
                    
                }
                LoggingHandler.LogMessage($"Team Roster Rando is disabled, leaving HandleTeamRosterRando", taskName,
                    LogLevel.SuperDebug);
                return;
            }

            if (Mod.Configuration.TeamRosterRandoForceRandom && !AbilityCharacterManager.IsTeamRosterRandomized)
            {
                LoggingHandler.LogMessage($"Shuffling Team Roster Rando now", taskName, LogLevel.SuperDebug);
                Character[] tempArray = Enum.GetValues<Character>();
                Random.Shared.Shuffle(tempArray);
                List<Character> tempList = tempArray.ToList();
                foreach (var pair in AbilityCharacterManager.TeamRosterRando)
                {
                    AbilityCharacterManager.TeamRosterRando[pair.Key] = tempList.First();
                    tempList.RemoveAt(0);
                }
                AbilityCharacterManager.IsTeamRosterRandomized = true;
            }


            if (!Mod.Configuration.TeamRosterRandoForceRandom)
            {
                // make sure mapping is correct
                AbilityCharacterManager.TeamRosterRando[Character.Sonic] = Mod.Configuration.TeamSonicSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Knuckles] = Mod.Configuration.TeamSonicPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Tails] = Mod.Configuration.TeamSonicFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Shadow] = Mod.Configuration.TeamDarkSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Omega] = Mod.Configuration.TeamDarkPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Rouge] = Mod.Configuration.TeamDarkFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Amy] = Mod.Configuration.TeamRoseSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Big] = Mod.Configuration.TeamRosePowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Cream] = Mod.Configuration.TeamRoseFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Espio] = Mod.Configuration.TeamChaotixSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Vector] = Mod.Configuration.TeamChaotixPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Charmy] = Mod.Configuration.TeamChaotixFlyingCharacterRando;
            }
            
            
            
            
            LoggingHandler.LogMessage($"Handling Team Roster Rando now", taskName, LogLevel.SuperDebug);
            
            foreach (var pair in AbilityCharacterManager.TeamRosterRando)
            {
                int addr = (int)TeamRosterRandoJumpTableAddr + TeamRosterRandoJumpTableOffsets[pair.Key];
                ushort correctValue = TeamRosterRandoJumpTableValues[pair.Value];
                LoggingHandler.LogMessage($"Writing Bytes: 0x{correctValue:X} to addr: 0x{addr:X}", taskName, LogLevel.SuperDebug);
                Memory.Instance.SafeWrite((UIntPtr)addr, BitConverter.GetBytes(correctValue));
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    
    
    
    /// <summary>
    /// Sets the Character's Level
    /// </summary>
    /// <param name="formationChar">Which formation? (Speed, Power, Flying)</param>
    /// <param name="level">(0, 1, 2, 3) Must be In-Game.</param>
    /// <param name="taskName">Name of the task running this</param>
    public static unsafe void SetCharLevel(FormationChar formationChar, byte level, string taskName)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
            var charlevels = (byte*)(baseAddress + 0x208 + (byte)formationChar);
            *charlevels = level;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static void SetJumpAbility(bool value)
    {
        var bytes = value ? new byte[] { 0x74 } : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x191590, bytes);
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
        //function hook used for team blast meter
        
        var bytes = value ? new byte[] { 0x57, 0xE8, 0x96, 0x17, 0x00, 0x00 } : new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1BEB74, bytes);
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
        var bytes = value ? new byte[] { 0x0F, 0x8A, 0x2A, 0x03, 0x00, 0x00 } : new byte[] { 0xEB, 0x0B, 0x90, 0x90, 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF335, bytes);
    }
    
    public static void SetComboFinisher(bool value)
    {
        var bytes = value ? new byte[] { 0x0F, 0x85, 0xDE, 0x03, 0x00, 0x00 } : new byte[] { 0xE9, 0xDF, 0x03, 0x00, 0x00, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1BFD3B, bytes);
    }
    
    public static void SetFlying(bool value, string taskName)
    {
        //LoggingHandler.LogMessage($"SetFlying: {value}", taskName, LogLevel.SuperDebug);
        //var bytes = value ? new byte[] { 0x34 } : new byte[] { 0x35 };
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1C9C7F, bytes);
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1CA608, bytes);
        //var bytes = value ? new byte[] { 0x00, 0x00, 0x34, 0x43 } : new byte[] { 0x00, 0x00, 0x80, 0xBF };
        //Memory.Instance.ChangeProtection(Mod.ModuleBase + 0x389FE4, 0x4, MemoryProtection.ReadWriteExecute);
        //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x389FE4, bytes);
        //Memory.Instance.ChangeProtection(Mod.ModuleBase + 0x389FE4, 0x4, MemoryProtection.Read);
        
        //Handled through Function Hook now
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