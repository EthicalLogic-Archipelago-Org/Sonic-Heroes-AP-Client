
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sound;

namespace Sonic_Heroes_AP_Client.Archipelago;

public enum FreezeType
{
    NoFreeze,
    StageFreeze,
    FullFreeze,
}

public static class TrapHandler
{
    public const int StealthTrapDuration = 140; //20 a sec
    public const int FreezeTrapDuration = 8;
    
    public const int NoSwapTrapDuration = 10;
    
    /// <summary>
    /// Duration of Charmy Trap in number of voice lines.
    /// </summary>
    public const int CharmyTrapDuration = 4;

    public static bool StealthTrapRunning = false;
    public static byte PreviousStealth = 0x0;
    public static int RemainingStealthDuration = 0;
    
    public static bool FreezeTrapRunning => PreviousFreeze != FreezeType.NoFreeze;
    public static readonly FreezeType FreezeTrapType = FreezeType.FullFreeze;
    public static FreezeType PreviousFreeze = FreezeType.NoFreeze;
    public static int RemainingFreezeDuration = 0;
    
    
    public static bool CharmyTrapRunning = false;
    public static readonly int[] CharmyLines =
    [
        1446, 485, 1602, 1636, 1971, 485,
        2055, 2079, 2103, 2116, 2259, 2296, 485, 2309, 2350, 2490,
        2710, 2755, 2832, 2844, 2878, 2941, 3169, 485, 3204, 3215,
        3220, 3230, 3287, 3321, 3355, 3373, 3485, 3738, 3762,
        3772, 3791, 3802, 3804, 3810, 3878, 4273, 4282, 4291,
        3398, 4522, 4621, 485
    ];
    
    public static int RemainingCharmyTrap = 0;

    public static bool IsAnyTrapRunning(string taskName)
    {
        return StealthTrapRunning || FreezeTrapRunning || IsNoSwapRunning(taskName) || CharmyTrapRunning;
    }

    //Stealth
    private static unsafe byte GetStealth(string taskName)
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x6777E4);
            return *(byte*)(baseAddr + 0x25);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return 0x0;
    }

    public static void HandleStealthTrap(string taskName)
    {
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in HandleStealthTrap", taskName, LogLevel.Error);
        }
        else
        {
            if (Mod.Configuration.PlaySounds)
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE00D, taskName);
        }
        
        Interlocked.Add(ref RemainingStealthDuration, StealthTrapDuration);
        if (Mod.StealthTrapTask.Status != TaskStatus.Running)
        {
            PreviousStealth = GetStealth(taskName);
            LoggingHandler.LogMessage($"Stealth Trap Task not running, starting now.", taskName, LogLevel.SuperDebug);
            Mod.StealthTrapTask.Start();
            return;
        }
        LoggingHandler.LogMessage($"Stealth Trap Already Running", taskName, LogLevel.SuperDebug);
    }


    public static void DisableStealthTrap(string taskName)
    {
        if (!StealthTrapRunning)
            return;

        Interlocked.Exchange(ref RemainingStealthDuration, 0);
        ItemGameWrites.SetStealth(PreviousStealth, taskName);
        StealthTrapRunning = false;
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in DisableStealthTrap", taskName, LogLevel.Error);
            return;
        }
        if (Mod.Configuration.PlaySounds)
            SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE00E, taskName);
        
    }
    
    //Freeze
    public static void HandleFreezeTrap(string taskName)
    {
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in HandleFreezeTrap", taskName, LogLevel.Error);
        }
        else
        {
            if (Mod.Configuration.PlaySounds) 
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE014, taskName);
        }
        
        if (PreviousFreeze == FreezeTrapType)
        {
            LoggingHandler.LogMessage($"Previous Freeze = Full Freeze (dropping Freeze Trap)", taskName, LogLevel.SuperDebug);
            return;
        }
            
        Interlocked.Add(ref RemainingFreezeDuration, FreezeTrapDuration);
        PreviousFreeze = FreezeTrapType;
        if (Mod.FreezeTrapTask.Status != TaskStatus.Running)
        {
            LoggingHandler.LogMessage($"Freeze Trap Task not running, starting now.", taskName, LogLevel.SuperDebug);
            Mod.FreezeTrapTask.Start();
            return;
        }
        LoggingHandler.LogMessage($"Freeze Trap Task running, adding time.", taskName, LogLevel.SuperDebug);
    }
    
    //NoSwap
    public static void HandleNoSwapTrap(string taskName)
    {
        SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE018, taskName);
        ItemGameWrites.SetNoSwap(NoSwapTrapDuration, taskName);
    }
    
    
    public static unsafe bool IsNoSwapRunning(string taskName)
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x64C268);
            return *(short*)(baseAddr + 0x204) > 0;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return false;
    }
    
    //Charmy Trap
    public static void HandleCharmyTrap(string taskName)
    {
        Interlocked.Add(ref RemainingCharmyTrap, CharmyTrapDuration);
        if (Mod.CharmyTrapTask.Status != TaskStatus.Running)
        {
            LoggingHandler.LogMessage($"Charmy Trap Task not running, starting now.", taskName, LogLevel.SuperDebug);
            Mod.CharmyTrapTask.Start();
            return;
        }
        LoggingHandler.LogMessage($"Charmy Trap Task running, adding time.", taskName, LogLevel.SuperDebug);
    }
}