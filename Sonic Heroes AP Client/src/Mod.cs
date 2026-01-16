

using System.Diagnostics;
using System.Drawing;
using Heroes.Controller.Hook.Interfaces;
using Reloaded.Hooks.Definitions;
using Reloaded.Imgui.Hook;
using Reloaded.Mod.Interfaces;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Configuration;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Hooks;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Sonic_Heroes_AP_Client.SaveData;
using Sonic_Heroes_AP_Client.Template;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client;

/// <summary>
/// The Main Mod Class.
/// Created by the Mod Loader (Reloaded)
/// </summary>
public class Mod: ModBase
{
    /// <summary>
    /// Flag for debug prints and testing.
    /// Should NOT be enabled in prod.
    /// </summary>
    public static bool IsDebug = true;
    
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
    
    
    /// <summary>
    /// Pointer to the Start of the Spawn Data.
    /// Should be ModuleBase + 0x3C2FC8 in all cases
    /// </summary>
    public static uint SpawnDataStartAddr;


    public static DXHook? DxHook;
    public static bool DXHookInitialized = false;
    
    
    //vars for mod classes here:
    public static ArchipelagoHandler ArchipelagoHandler;
    public static Controller.Controller? Controller;
    public static FunctionHooks FunctionHooks;
    public static SaveDataHandler SaveDataHandler;
    public static LevelSelectManager LevelSelectManager;
    public static UserInterface UserInterface;
    


    /// <summary>
    /// Constructor for Mod class.
    /// </summary>
    /// <param name="context">Information passed in to the mod.</param>
    public Mod(ModContext context)
    {
        try
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            Logger = context.Logger;
            //Logger.OnWriteLine += (sender, tuple) => { };
            _owner = context.Owner;
            ModConfig = context.ModConfig;
            Configuration = context.Configuration;
            _controllerHook = _modLoader.GetController<IControllerHook>();
            
            SDK.Init(_hooks);
            ModuleBase = (UIntPtr)Process.GetCurrentProcess().MainModule!.BaseAddress;
            SpawnDataStartAddr = (uint)(ModuleBase + 0x3C2FC8);
            
            Controller = new Controller.Controller(_controllerHook, 0);
            
            ArchipelagoHandler = new ArchipelagoHandler(Configuration.Server, Configuration.Port, Configuration.Slot, Configuration.Password);
            context.Configuration.ConfigurationUpdated += OnModConfigChange;
            
            //Save Data After 
            SaveDataHandler = new SaveDataHandler();
            
            //LevelSelect must be before SlotData
            LevelSelectManager = new LevelSelectManager();

            //UI can be last
            UserInterface = new UserInterface();
            
            FunctionHooks = new FunctionHooks();

            //connection stuff here
            var t = new Thread(start: () =>
            {
                while (true)
                {
                    if (!ArchipelagoHandler.IsConnecting && !ArchipelagoHandler.IsConnected)
                    {
                        ArchipelagoHandler.CreateSession();
                        ArchipelagoHandler.InitConnect();
                    }
                    Thread.Sleep(2500);
                }
            });
            t.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void InitOnConnect()
    {
        try
        {
            LevelSelectGameWrites.ModifyInstructions();
            CheckpointGameWrites.SetCheckPointPriorityWrite(true);
            GameStateGameWrites.SetRingLoss(Configuration.RingLoss);
            if (_hooks != null)
            {
                FunctionHooks.SetUpFunctionHooks(_hooks);
                GameStateGameWrites.RemoveRingCapOnScatteredRingSpawn(true);
            } 
            //Logger.WriteLine($"[{ModConfig.ModId}] Initialized", Color.Blue);
            LevelSelectManager.InitConnect();
            LevelSpawnUnlockHandler.InitConnect();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void OnModConfigChange(IUpdatableConfigurable x)
    {
        try
        {
            if (ArchipelagoHandler.Seed == null)
                return;
            //Console.WriteLine($"Mod Config Changed. Seed is: {Seed}");
            MusicShuffleHandler.Shuffle(int.Parse(ArchipelagoHandler.Seed[..9]));
            
            //Console.WriteLine($"Mod Config Changed. Deathlink is now: {Mod.Configuration.TagOptions.DeathLink}");
            ArchipelagoHandler.CheckTags();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        Configuration = configuration;
        Logger.WriteLine($"[{ModConfig.ModId}] Config Updated: Applying");
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