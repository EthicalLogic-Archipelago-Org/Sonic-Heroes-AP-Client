

using System.Diagnostics;
using System.Drawing;
using Heroes.Controller.Hook.Interfaces;
using Reloaded.Hooks.Definitions;
using Reloaded.Imgui.Hook;
using Reloaded.Mod.Interfaces;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Hooks;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Sonic_Heroes_AP_Client.SaveData;
using Sonic_Heroes_AP_Client.Tasks;
using Sonic_Heroes_AP_Client.Template;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client;

/// <summary>
/// The Main Mod Class.
/// Created by the Mod Loader (Reloaded)
/// </summary>
public class Mod: ModBase
{
    #if DEBUG
    /// <summary>
    /// Flag for debug prints and testing.
    /// Should NOT be enabled in prod.
    /// </summary>
    public const bool IsDebug = true;
    #else
    /// <summary>
    /// Flag for debug prints and testing.
    /// Should NOT be enabled in prod.
    /// </summary>
    public const bool IsDebug = false;
    #endif
    
    
    private readonly IModLoader _modLoader;
    
    /// <summary>
    /// Weak Reference for the Controller Hook.
    /// Is Used for checking for controller inputs.
    /// </summary>
    private static WeakReference<IControllerHook> _controllerHook;
    
    /// <summary>
    /// Used for hooking game functions.
    /// </summary>
    private static IReloadedHooks? _hooks;
    
    public static ILogger Logger;
    private readonly IMod _owner;
    public static IModConfig ModConfig;
    
    /// <summary>
    /// The Mod Configuration with the chosen options.
    /// </summary>
    public static Config? Configuration { get; private set; }


    /// <summary>
    /// Pointer to the ModuleBase of the Game Application.
    /// Should be 0x400000 in all cases.
    /// </summary>
    public static UIntPtr ModuleBase = 0x400000;


    public static DXHook? DxHook;
    public static bool DXHookInitialized = false;
    
    
    //vars for mod classes here:
    public static ArchipelagoHandler ArchipelagoHandler;
    public static Controller.Controller? Controller;
    //public static FunctionHooks FunctionHooks;
    public static SaveDataHandler SaveDataHandler;
    public static LevelSelectManager LevelSelectManager;
    public static UserInterface UserInterface;
    
    
    //tasks here
    //main task/thread exists as well (obv)
    
    public static Task ConnectionTask = new (Tasks.ConnectionTask.APConnectionTask);
    public static Task CheckReceivedItemsTask = new (ReceivedItemsTask.CheckReceivedItemsTask);
    public static Task CheckedLocationsTask = new (Tasks.CheckedLocationsTask.CheckedLocationsAPTask);
    public static Task StealthTrapTask = new (TrapTask.StealthTrapTask);
    public static Task FreezeTrapTask = new (TrapTask.FreezeTrapTask);
    public static Task CharmyTrapTask = new (TrapTask.CharmyTrapTask);
    //public static Task PositionMappingTask = new (Tasks.ConnectionTask.APConnectionTask);
    
    //Imgui (Level, Trap, logger windows)
    //mod config changed
    //mod configuration updated
    //mod logger writeline
    //controller OnInput
    //ErrorReceived
    //OnMessageReceived
    //OnSocketClosed
    //PacketReceived
    //ItemReceived
    
    


    /// <summary>
    /// Constructor for Mod class.
    /// </summary>
    /// <param name="context">Information passed in to the mod.</param>
    public Mod(ModContext context)
    {
        const string taskName = "Main Task";
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        Logger = context.Logger;
        Logger.OnWriteLine += LoggingHandler.OnModLoggerWriteLine;
        _owner = context.Owner;
        ModConfig = context.ModConfig;
        Configuration = context.Configuration;
        
        _controllerHook = _modLoader.GetController<IControllerHook>();
        
        SDK.Init(_hooks);
        ModuleBase = (UIntPtr)Process.GetCurrentProcess().MainModule!.BaseAddress;
        LevelSpawnData.SpawnDataStartAddr = (uint)(ModuleBase + 0x3C2FC8);
        
        Controller = new Controller.Controller(_controllerHook, 0);
        
        
        ArchipelagoHandler = new ArchipelagoHandler(Configuration.Server, Configuration.Port, Configuration.Slot, Configuration.Password);
        context.Configuration.ConfigurationUpdated += OnModConfigChange;
        
        //Save Data After 
        SaveDataHandler = new SaveDataHandler();
        
        //LevelSelect must be before SlotData
        LevelSelectManager = new LevelSelectManager();

        //UI can be last
        UserInterface = new UserInterface();
        
        //FunctionHooks = new FunctionHooks();

        CheckInvalidConfigValues(taskName);
        //connection stuff here
        ConnectionTask.Start();
    }
    
    public static void InitOnConnect(string taskName)
    {
        LoggingHandler.LogMessage($"InitOnConnect Start", taskName, LogLevel.SuperDebug);
        if (_hooks != null)
        {
            LoggingHandler.LogMessage($"InitOnConnect Before Function Hooks", taskName, LogLevel.SuperDebug);
            FunctionHooks.SetUpFunctionHooks(_hooks);
            LoggingHandler.LogMessage($"InitOnConnect After Function Hooks", taskName, LogLevel.SuperDebug);
            GameStateGameWrites.RemoveRingCapOnScatteredRingSpawn(true);
            LoggingHandler.LogMessage($"InitOnConnect After Remove Ring Cap", taskName, LogLevel.SuperDebug);
        } 
        //GameStateGameWrites.Change999RingsCap(true);
        LoggingHandler.LogMessage($"InitOnConnect Before AbilityCharacter", taskName, LogLevel.SuperDebug);
        AbilityCharacterManager.InitConnect(taskName);
        LoggingHandler.LogMessage($"InitOnConnect Before LevelSelectManager", taskName, LogLevel.SuperDebug);
        LevelSelectManager.InitConnect(taskName);
        LoggingHandler.LogMessage($"InitOnConnect Before LevelSpawnUnlockHandler", taskName, LogLevel.SuperDebug);
        LevelSpawnUnlockHandler.InitConnect(taskName);
        LoggingHandler.LogMessage($"InitOnConnect Before LevelSelectGameWrites", taskName, LogLevel.SuperDebug);
        LevelSelectGameWrites.ModifyInstructions(taskName);
        LoggingHandler.LogMessage($"InitOnConnect Before CheckpointGameWrites", taskName, LogLevel.SuperDebug);
        CheckpointGameWrites.SetCheckPointPriorityWrite(true);
        LoggingHandler.LogMessage($"InitOnConnect Before GameStateGameWrites", taskName, LogLevel.SuperDebug);
        if (Configuration == null)
            return;
        GameStateGameWrites.SetRingLoss(Configuration.RingLoss);
        LoggingHandler.LogMessage($"InitOnConnect End", taskName, LogLevel.SuperDebug);
    }

    public static bool CheckCurrentModVersionWithValue(string version)
    {
        var modVersion = ModConfig.ModVersion.Split(".");
        var otherVersion = version.Split(".");
        return modVersion[0] == otherVersion[0] && modVersion[1] == otherVersion[1];
    }

    public static void CheckDebugStatusChange(string taskName)
    {
        LevelSelectManager.LevelSelectAllLevelsAvailableWrite = IsDebug;
    }
    
    public void OnModConfigChange(IUpdatableConfigurable x)
    {
        const string taskName = "ModConfigChange";
        if (ArchipelagoHandler.Seed == null)
        {
            LoggingHandler.LogMessage($"Seed is Null in OnModConfigChange", taskName, LogLevel.Error);
            return;
        }
            
        if (Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in OnModConfigChange", taskName, LogLevel.Error);
            return;
        }
            
        
        CheckInvalidConfigValues(taskName);
        MusicShuffleHandler.Shuffle(int.Parse(ArchipelagoHandler.Seed[..9]), taskName);
        
        ArchipelagoHandler.CheckTags(taskName);
        GameStateGameWrites.SetRingLoss(Configuration.RingLoss);
    }

    public void CheckInvalidConfigValues(string taskName)
    {
        if (Configuration == null)
            return;
        CheckDebugStatusChange(taskName);
        if (Configuration.ScatteredRingsCap > Configuration.RingLoss && Configuration.RingLoss != 19)
        {
            LoggingHandler.LogMessage($"Scattered Rings Cap: {Configuration.ScatteredRingsCap} is above Ring Loss: {Configuration.RingLoss}", taskName, LogLevel.Error);
            Configuration.ScatteredRingsCap = Configuration.RingLoss;
        }
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        Configuration = configuration;
        LoggingHandler.LogMessage($"[{ModConfig.ModId}] Config Updated: Applying", "ModConfigurationUpdated", LogLevel.APAction);
    }
    #endregion

    #region For Exports, Serialization etc.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod()
    {
    }
#pragma warning restore CS8618

    #endregion
}