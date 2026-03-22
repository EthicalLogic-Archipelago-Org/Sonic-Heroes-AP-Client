
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sound;

namespace Sonic_Heroes_AP_Client.Tasks;

public static class TrapTask
{
    private static readonly Random _random = new();
    
    public static void StealthTrapTask()
    {
        const string taskName = "StealthTrapTask";
        ItemGameWrites.SetStealth(1, taskName);
        while (Interlocked.CompareExchange(ref TrapHandler.RemainingStealthDuration, 0, 0) > 1) //check for > 0 in disable Stealth Trap so this must be 1
        {
            Thread.Sleep(50);
            Interlocked.Decrement(ref TrapHandler.RemainingStealthDuration);
        }
        LoggingHandler.LogMessage($"Stealth Trap Task finishing.", taskName, LogLevel.SuperDebug);
        TrapHandler.DisableStealthTrap(taskName);
    }
    
    
    public static void FreezeTrapTask()
    {
        const string taskName = "FreezeTrapTask";
        
        ItemGameWrites.SetFreeze(TrapHandler.FreezeTrapType, taskName);
        while (Interlocked.CompareExchange(ref TrapHandler.RemainingFreezeDuration, 0, 0) > 0) 
        {
            Thread.Sleep(1000);
            Interlocked.Decrement(ref TrapHandler.RemainingFreezeDuration);
        }
        LoggingHandler.LogMessage($"Freeze Trap Task finishing.", taskName, LogLevel.SuperDebug);
        ItemGameWrites.SetFreeze(FreezeType.NoFreeze, taskName);
        TrapHandler.PreviousFreeze = FreezeType.NoFreeze;
    }

    
    public static void CharmyTrapTask()
    {
        const string taskName = "CharmyTrapTask";
        TrapHandler.CharmyTrapRunning = true;
        while (Interlocked.CompareExchange(ref TrapHandler.RemainingCharmyTrap, 0, 0) > 0) 
        {
            SoundHandler.PlayAFSSound((int)Mod.ModuleBase, TrapHandler.CharmyLines[_random.Next(TrapHandler.CharmyLines.Length)], taskName);
            Thread.Sleep(_random.Next(5000, 15000));
            Interlocked.Decrement(ref TrapHandler.RemainingCharmyTrap);
        }
        LoggingHandler.LogMessage($"Charmy Trap Task finishing.", taskName, LogLevel.SuperDebug);
        TrapHandler.CharmyTrapRunning = false;
        // ReSharper disable once FunctionNeverReturns
    }
}