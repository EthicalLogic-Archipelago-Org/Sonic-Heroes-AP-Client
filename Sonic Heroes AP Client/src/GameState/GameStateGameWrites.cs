

using System.Runtime.InteropServices;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.GameState;

public static class GameStateGameWrites
{
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ModifyLives(int moduleBase, int amount);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int RestartLevel(int moduleBase);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GiveShield(int moduleBase);
    
    public static void SetRingCount(int amount)
    {
        unsafe
        {
            *(int*)(Mod.ModuleBase + 0x5DD70C) = amount;
        }
    }
    
    
    public static int GetRingCount()
    {
        unsafe
        {
            return *(int*)(Mod.ModuleBase + 0x5DD70C);
        }
    }


    /// <summary>
    /// Writes over the Game Assembly to change the functionality of spawning scattering rings when getting hit.
    /// </summary>
    /// <param name="value">Should the cap (20) be disabled?</param>
    public static void RemoveRingCapOnScatteredRingSpawn(bool value)
    {
        var bytes = value ? Enumerable.Repeat((byte)0x90, 5).ToArray() : [0xBB, 0x14, 0x00, 0x00, 0x00];
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x82769, bytes);
    }
    


    /// <summary>
    /// Writes over the Game Assembly to change the functionality of losing rings when getting hit.
    /// </summary>
    /// <param name="ringsLost">Number of rings to loss when getting hit. If negative, restore vanilla behavior (all rings)</param>
    public static void SetRingLoss(int ringsLost)
    {
        byte[] classicRingLoss = [0x8B, 0xC, 0x85, 0xC, 0xD7, 0x9D, 0x0];
        
        byte[] startByte = [0xB9];
        var numBytes = BitConverter.GetBytes(ringsLost);
        byte[] endBytes = [0x90, 0x90];
        if (ringsLost < 20)
        {
            Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A446D, classicRingLoss);
            return;
        }
        var result = startByte.Concat(numBytes).Concat(endBytes).ToArray();
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A446D, result);
        //var bytes = modern ? new byte[] { 0xB9, 0x14, 0x0, 0x0, 0x0, 0x90, 0x90 } : new byte[] { 0x8B, 0xC, 0x85, 0xC, 0xD7, 0x9D, 0x0 };
    }
    
    
    /// <summary>
    /// Changes how the Bonus Key is lost on getting hit or dying.
    /// </summary>
    /// <param name="value">True to keep bonus key on getting hit and dying, False to restore vanilla behavior.</param>
    public static void SetDontLoseBonusKey(bool value)
    {
        var writeValue1 = value ? new byte[] { 0x90, 0x90, 0x90, 0x90 } : new byte[] { 0xC6, 0x40, 0x26, 0x1 };
        var writeValue2 = value ? new byte[] { 0x90, 0x90, 0x90, 0x90 } : new byte[] { 0xC6, 0x46, 0x26, 0x1 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B4E, writeValue1);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A43D3, writeValue2);
    }
    
    
    public static unsafe void SetBonusKey(bool value)
    {
        try
        {
            if (!GameStateHandler.InGame(true))
                return;
            
            //Bonus Key Byte
            var baseAddress = *(uint*)(Mod.ModuleBase + 0x6777E4);
            *(byte*)(baseAddress + 0x26) = value ? (byte)0 : (byte)1;

            //Visual Bonus Key Byte Here (yellow key on UI)
            baseAddress = *(uint*)(Mod.ModuleBase + 0x5DD4E4);
            *(byte*)(baseAddress + 0x48) = value ? (byte)1 : (byte)0;

            if (!value)
                return;

            baseAddress = *(uint*)(Mod.ModuleBase + 0x67776C);
            var uiStackAddress = Mod.ModuleBase + 0x5DD540;
            uint uiStackData;

            do
            {
                uiStackAddress += 4;
                if (uiStackAddress >= Mod.ModuleBase + 0x67776C + 0x40)
                    return;
                uiStackData = *(uint*)(uiStackAddress);
            } while (uiStackData != 0);

            *(uint*)uiStackAddress = baseAddress;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static unsafe void SetCurrentAct(Act act)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777E4);
            *(byte*)(baseAddress + 0x28) = (byte)act;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    /// <summary>
    /// Kills the player.
    /// </summary>
    public static void Kill()
    {
        try
        {
            if (!GameStateHandler.InGame()) 
                return;
            ModifyLives((int)Mod.ModuleBase, -1);
            Console.WriteLine(RestartLevel((int)Mod.ModuleBase));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    
    /// <summary>
    /// Removes the cap of 99 lives in game.
    /// </summary>
    /// <param name="value">Set to True to remove cap. False to keep cap</param>
    public static void Change99LivesCap(bool value)
    {
        byte jmpByte = value ? (byte)0xEB : (byte)0x7E;
        
        //TODO handle metal madness/overlord
        
        //save file copy when entering level
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x15E3, [jmpByte]);
        //in game count
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x23B76, [jmpByte]);
        //in game count
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x23B90, [jmpByte]);
        
        //UI
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x20D38, [jmpByte]);
        
        
        //0x4015E0      init stage
        //0x40460A      restart stage
        //0x404624      restart stage
        //0x420D35      UI?
        //0x423B73      add lives
        //0x423B8D      add lives
        //0x423EEF      add min (caps mins at 100) (not lives related)
        //0x44E6E5      no idea (prob not lives)
        //0x60A497      metal Sonic Create Team
    }
    
    /// <summary>
    /// Removes the cap of 999 Rings when in level.
    /// </summary>
    /// <param name="value">True removes cap, False keeps it.</param>
    public static void Change999RingsCap(bool value)
    {
        byte jmpByte = value ? (byte)0xEB : (byte)0x7E;
        
        //in game count
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x23B1F, [jmpByte]);
        //UI
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1FE2A, [jmpByte]);
    }
}