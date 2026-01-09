
using Sonic_Heroes_AP_Client.Definitions;
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
    public const int StealthTrapDuration = 10;
    public const int FreezeTrapDuration = 10;
    
    public const int NoSwapTrapDuration = 10;
    
    /// <summary>
    /// Duration of Charmy Trap in number of voice lines.
    /// </summary>
    public const int CharmyTrapDuration = 4;

    public static bool StealthTrapRunning = false;
    private static byte previousStealth = 0x0;
    private static int remainingStealthDuration = 0;

    public static bool NoSwapTrapRunning => IsNoSwapRunning();
    
    public static bool FreezeTrapRunning => previousFreeze != FreezeType.NoFreeze;
    private static FreezeType previousFreeze = FreezeType.NoFreeze;
    
    
    public static bool CharmyTrapRunning = false;
    private static readonly int[] _charmyLines = 
    {
        1446, 485, 1602, 1636, 1971, 485,
        2055, 2079, 2103, 2116, 2259, 2296, 485, 2309, 2350, 2490,
        2710, 2755, 2832, 2844, 2878, 2941, 3169, 485, 3204, 3215,
        3220, 3230, 3287, 3321, 3355, 3373, 3485, 3738, 3762,
        3772, 3791, 3802, 3804, 3810, 3878, 4273, 4282, 4291,
        3398, 4522, 4621, 485
    };
    private static readonly Random _random = new();
    private static int remainingCharmyTrap = 0;

    public static bool IsAnyTrapRunning()
    {
        return CharmyTrapRunning || FreezeTrapRunning || NoSwapTrapRunning || CharmyTrapRunning;
    }

    //Stealth
    private static unsafe byte GetStealth()
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x6777E4);
            return *(byte*)(baseAddr + 0x25);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0x0;
    }

    public static void HandleStealthTrap()
    {
        try
        {
            var oldValue = Interlocked.Exchange(ref remainingStealthDuration, StealthTrapDuration);
            if (StealthTrapRunning)
                return;
            StealthTrapRunning = true;
            previousStealth = GetStealth();
            var t = new Thread(() =>
            {
                ItemGameWrites.SetStealth(1);
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE00D);
                while (Interlocked.CompareExchange(ref remainingStealthDuration, 0, 0) > 0) {
                    Thread.Sleep(1000);
                    Interlocked.Decrement(ref remainingStealthDuration);
                }
                ItemGameWrites.SetStealth(previousStealth);
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE00E);
                StealthTrapRunning = false;
            });
            t.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    //Freeze
    public static void HandleFreezeTrap()
    {
        StartFreeze(FreezeType.FullFreeze, FreezeTrapDuration);
    }

    private static unsafe void StartFreeze(FreezeType freezeType, int duration)
    {
        try
        {
            if (previousFreeze == freezeType)
                return;
            previousFreeze = freezeType;
            SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE014);
            ItemGameWrites.SetFreeze(freezeType);
            var timer = new System.Timers.Timer(duration * 1000);
            timer.Elapsed += (sender, e) =>
            {
                ItemGameWrites.SetFreeze(FreezeType.NoFreeze);
                previousFreeze = FreezeType.NoFreeze;
                timer.Stop();
                timer.Dispose();
            };
            timer.AutoReset = false;
            timer.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    //NoSwap
    public static void HandleNoSwapTrap()
    {
        try
        {
            SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE018);
            ItemGameWrites.SetNoSwap(NoSwapTrapDuration);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private static unsafe bool IsNoSwapRunning()
    {
        try
        {
            var baseAddr = *(int*)(Mod.ModuleBase + 0x64C268);
            return *(short*)(baseAddr + 0x204) > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
    
    //Charmy Trap
    public static void HandleCharmyTrap()
    {
        try
        {
            Interlocked.Add(ref remainingCharmyTrap, CharmyTrapDuration);
            if (CharmyTrapRunning)
                return;
            CharmyTrapRunning = true;
            var t = new Thread(() =>
            {
                while (Interlocked.CompareExchange(ref remainingCharmyTrap, 0, 0) > 0) 
                {
                    SoundHandler.PlayAFSSound((int)Mod.ModuleBase, _charmyLines[_random.Next(_charmyLines.Length)]);
                    Thread.Sleep(_random.Next(5000, 15000));
                    Interlocked.Decrement(ref remainingCharmyTrap);
                }
                CharmyTrapRunning = false;
            });
            t.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
}