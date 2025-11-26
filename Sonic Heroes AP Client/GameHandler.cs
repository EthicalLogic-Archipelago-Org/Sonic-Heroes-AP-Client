using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;

namespace Sonic_Heroes_AP_Client;

public enum LevelUpType
{
    Speed,
    Flying,
    Power
}

public enum Act
{
    Act1,
    Act2,
    Act3
}

public struct Stage
{
    private readonly LevelId _level;
    private readonly Team _story;
    private readonly Act _act;

    public override bool Equals(object? obj)
    {
        if (obj is not Stage stage)
            return false;
        return (_level == stage._level
                && _story == stage._story
                && _act == stage._act);
    }

    public Stage(LevelId level, Team story, Act act)
    { 
        _level = level;
        _story = story;
        _act = act;
    }
}

public class GameHandler
{
    public static int SuperHardModeId = 0x2300; //154F

    public static readonly Dictionary<Region, List<LevelId>> RegionToLevelId = new()
    {
        {
            Region.Ocean, new List<LevelId>()
            {
                LevelId.SeasideHill,
                LevelId.OceanPalace,
            }
        },
        {
            Region.HotPlant, new List<LevelId>()
            {
                LevelId.GrandMetropolis,
                LevelId.PowerPlant,
            }
        },
        {
            Region.Casino, new List<LevelId>()
            {
                LevelId.CasinoPark,
                LevelId.BingoHighway,
            }
        },
        {
            Region.Train, new List<LevelId>()
            {
                LevelId.RailCanyon,
                LevelId.BulletStation,
                LevelId.ChaotixRailCanyon,
            }
        },
        {
            Region.BigPlant, new List<LevelId>()
            {
                LevelId.FrogForest,
                LevelId.LostJungle,
            }
        },
        {
            Region.Ghost, new List<LevelId>()
            {
                LevelId.HangCastle,
                LevelId.MysticMansion,
            }
        },
        {
            Region.Sky, new List<LevelId>()
            {
                LevelId.EggFleet,
                LevelId.FinalFortress,
            }
        },
        {
            Region.SpecialStage, new List<LevelId>()
            {
                LevelId.BonusStage1,
                LevelId.BonusStage2,
                LevelId.BonusStage3,
                LevelId.BonusStage4,
                LevelId.BonusStage5,
                LevelId.BonusStage6,
                LevelId.BonusStage7,
                LevelId.EmeraldStage1,
                LevelId.EmeraldStage2,
                LevelId.EmeraldStage3,
                LevelId.EmeraldStage4,
                LevelId.EmeraldStage5,
                LevelId.EmeraldStage6,
                LevelId.EmeraldStage7,
            }
        },
        {
            Region.Boss, new List<LevelId>()
            {
                LevelId.EggHawk,
                LevelId.TeamFight1,
                LevelId.RobotCarnival,
                LevelId.EggAlbatross,
                LevelId.TeamFight2,
                LevelId.RobotStorm,
                LevelId.EggEmperor
            }
        },
        {
            Region.FinalBoss, new List<LevelId>()
            {
                LevelId.MetalMadness,
                LevelId.MetalOverlord,
            }
        },
    };

    public static Dictionary<LevelId, Region> LevelIdToRegion =
        RegionToLevelId.SelectMany(x =>
                x.Value.Select(s => new { Key = s, Value = x.Key }))
            .ToDictionary(y => y.Key, y => y.Value);


    public static Dictionary<Team, Dictionary<FormationChar, string>> CharacterNames =
        new()
        {
            {
                Team.Sonic, new()
                {
                    {
                        FormationChar.Speed,
                        "Sonic"
                    },
                    {
                        FormationChar.Flying,
                        "Tails"
                    },
                    {
                        FormationChar.Power,
                        "Knuckles"
                    }
                }
            },
            {
                Team.Dark, new()
                {
                    {
                        FormationChar.Speed,
                        "Shadow"
                    },
                    {
                        FormationChar.Flying,
                        "Rouge"
                    },
                    {
                        FormationChar.Power,
                        "Omega"
                    }
                }
            },
            {
                Team.Rose, new()
                {
                    {
                        FormationChar.Speed,
                        "Amy"
                    },
                    {
                        FormationChar.Flying,
                        "Cream"
                    },
                    {
                        FormationChar.Power,
                        "Big"
                    }
                }
            },
            {
                Team.Chaotix, new()
                {
                    {
                        FormationChar.Speed,
                        "Espio"
                    },
                    {
                        FormationChar.Flying,
                        "Charmy"
                    },
                    {
                        FormationChar.Power,
                        "Vector"
                    }
                }
            },
            {
                Team.SuperHard, new()
                {
                    {
                        FormationChar.Speed,
                        "SuperHard Sonic"
                    },
                    {
                        FormationChar.Flying,
                        "SuperHard Tails"
                    },
                    {
                        FormationChar.Power,
                        "SuperHard Knuckles"
                    }
                }
            },
        };


    public static Dictionary<LevelId, LevelId> BonusStageForLevel = new()
    {
        { LevelId.SeasideHill, LevelId.BonusStage1 },
        { LevelId.OceanPalace, LevelId.EmeraldStage1 },
        { LevelId.GrandMetropolis, LevelId.BonusStage2 },
        { LevelId.PowerPlant, LevelId.EmeraldStage2 },
        { LevelId.CasinoPark, LevelId.BonusStage3 },
        { LevelId.BingoHighway, LevelId.EmeraldStage3 },
        { LevelId.RailCanyon, LevelId.BonusStage4 },
        { LevelId.BulletStation, LevelId.EmeraldStage4 },
        { LevelId.FrogForest, LevelId.BonusStage5 },
        { LevelId.LostJungle, LevelId.EmeraldStage5 },
        { LevelId.HangCastle, LevelId.BonusStage6 },
        { LevelId.MysticMansion, LevelId.EmeraldStage6 },
        { LevelId.EggFleet, LevelId.BonusStage7 },
        { LevelId.FinalFortress, LevelId.EmeraldStage7 },
        
        { LevelId.ChaotixRailCanyon, LevelId.BonusStage4},
    };


public static Dictionary<PlayableCharacter, Team> PlayableCharToTeam = new Dictionary<PlayableCharacter, Team>()
    {
        { PlayableCharacter.PlayableSonic, Team.Sonic },
        { PlayableCharacter.PlayableTails, Team.Sonic },
        { PlayableCharacter.PlayableKnuckles, Team.Sonic },
        { PlayableCharacter.PlayableShadow, Team.Dark },
        { PlayableCharacter.PlayableRouge, Team.Dark },
        { PlayableCharacter.PlayableOmega, Team.Dark },
        { PlayableCharacter.PlayableAmy, Team.Rose },
        { PlayableCharacter.PlayableCream, Team.Rose },
        { PlayableCharacter.PlayableBig, Team.Rose },
        { PlayableCharacter.PlayableEspio, Team.Chaotix },
        { PlayableCharacter.PlayableCharmy, Team.Chaotix },
        { PlayableCharacter.PlayableVector, Team.Chaotix },
        { PlayableCharacter.PlayableSuperHardSonic, Team.SuperHard },
        { PlayableCharacter.PlayableSuperHardTails, Team.SuperHard },
        { PlayableCharacter.PlayableSuperHardKnuckles, Team.SuperHard },
    };
    
    public static Dictionary<PlayableCharacter, FormationChar> PlayableCharToFormation = new ()
    {
        { PlayableCharacter.PlayableSonic, FormationChar.Speed },
        { PlayableCharacter.PlayableTails, FormationChar.Flying },
        { PlayableCharacter.PlayableKnuckles, FormationChar.Power },
        { PlayableCharacter.PlayableShadow, FormationChar.Speed },
        { PlayableCharacter.PlayableRouge, FormationChar.Flying },
        { PlayableCharacter.PlayableOmega, FormationChar.Power },
        { PlayableCharacter.PlayableAmy, FormationChar.Speed },
        { PlayableCharacter.PlayableCream, FormationChar.Flying },
        { PlayableCharacter.PlayableBig, FormationChar.Power },
        { PlayableCharacter.PlayableEspio, FormationChar.Speed },
        { PlayableCharacter.PlayableCharmy, FormationChar.Flying },
        { PlayableCharacter.PlayableVector, FormationChar.Power },
        { PlayableCharacter.PlayableSuperHardSonic, FormationChar.Speed },
        { PlayableCharacter.PlayableSuperHardTails, FormationChar.Flying },
        { PlayableCharacter.PlayableSuperHardKnuckles, FormationChar.Power },
    };
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ModifyLives(int moduleBase, int amount);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int RestartLevel(int moduleBase);
    
    [DllImport("SHAP-NativeCaller.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GiveShield(int moduleBase);
    
    /*
    public static int GiveShield(int moduleBase)
    {
        return 0;
    }
    
    public static int ModifyLives(int moduleBase, int amount)
    {
        return 0;
    }
    
    */
    

    public static void SetRingLoss(bool modern)
    {
        var bytes = modern ? new byte[] { 0xB9, 0x14, 0x0, 0x0, 0x0, 0x90, 0x90 } 
            : new byte[] { 0x8B, 0xC, 0x85, 0xC, 0xD7, 0x9D, 0x0 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A446D, bytes);
    }

    public static unsafe void GiveLevelUp(LevelUpType type)
    {

       var baseAddress = *(int*)((int)Mod.ModuleBase + 0x64C268);
       var ptr = (byte*)(baseAddress + 0x208 + (int)type);
       var currentValue = *ptr;
       if (currentValue > 2)
           return;
       *ptr = (byte)(currentValue + 1);
    }

    public static unsafe void HandleTeamBlastFiller()
    {
        var baseAddress = (float*)((int)Mod.ModuleBase + 0x5DD72C);
        *baseAddress += 100;
    }
    
    public static void SetDontLoseBonusKey(bool value)
    {
        var writeValue1 = value ? new byte[] { 0x90, 0x90, 0x90, 0x90 } : new byte[] { 0xC6, 0x40, 0x26, 0x1 };
        var writeValue2 = value ? new byte[] { 0x90, 0x90, 0x90, 0x90 } : new byte[] { 0xC6, 0x46, 0x26, 0x1 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B4E, writeValue1);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A43D3, writeValue2);
    }
    
    public static void SetCheckPointPriorityWrite(bool value)
    {
        if (!value) 
            return;
        var bytes = new byte[] { 0x90, 0x90 };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x23996, bytes);
    }

    public static void SetTeamBlastWrite(bool value)
    {
        var bytes = value ? new byte[] { 0x75 } 
            : new byte[] { 0xEB };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AEB9E, bytes);
    }

    public static void SetLevelSelectAllLevelsAvailableWrite(bool value)
    {
        var bytes = value ? new byte[] { 0x90, 0x90 } : new byte[] { 0x74, 0x1D };
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B3BE, bytes);
    }
    

    public void ModifyInstructions()
    {
        // Makes all menu options display visually
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x504A3, new byte[] { 0x90, 0x90 });
        // Removes emblem update
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x22F344, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x24255B, new byte[] { 0x90 });
    }

    public void Kill()
    {
        try
        {
            if (!InGame()) 
                return;
            ModifyLives((int)Mod.ModuleBase, -1);
            Console.WriteLine(RestartLevel((int)Mod.ModuleBase));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public unsafe bool InGame(bool canBePaused = false)
    {
        try
        {
            var isPaused = *(int*)(Mod.ModuleBase + 0x4D66F0) == 6 && *(int*)((int)Mod.ModuleBase + 0x64C268) != 0;
            return (*(int*)(Mod.ModuleBase + 0x4D66F0) == 5  && *(int*)((int)Mod.ModuleBase + 0x64C268) != 0) || (isPaused && canBePaused);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }
    
    public unsafe LevelId GetCurrentLevel() 
    {
        var level = *(int*)(Mod.ModuleBase + 0x4D6720);
        if (level == 36)
            level = 8;
        return (LevelId)level;
    }
    
    public unsafe Team GetCurrentStory() 
    {

        return (Team)(*(int*)(Mod.ModuleBase + 0x4D6920));
    }

    public Act GetCurrentAct()
    {
        unsafe
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777E4);
            return (Act)(*(byte*)(baseAddress + 0x28));
        }
    }
    
    public void SetRingCount(int amount)
    {
        unsafe
        {
            *(int*)(Mod.ModuleBase + 0x5DD70C) = amount;
        }
    }
    
    public int GetRingCount()
    {
        unsafe
        {
            return *(int*)(Mod.ModuleBase + 0x5DD70C);
        }
    }
    
    public void RedirectSaveData(IntPtr redirectAddress)
    {
        //Console.WriteLine($"addr: {redirectAddress}");
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BAD7, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0xB)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BAF4, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x23)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB11, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x33)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB2E, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x43)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB63, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x383)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4BB4A, 
            BitConverter.GetBytes((int)(redirectAddress + 0x4C + 0x4D3)));
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B823, 
            BitConverter.GetBytes((int)(redirectAddress + 0x624)));
    }

    public static void SetSkipMadness(bool value)
    {
        unsafe
        {
            var addr = (char*)(Mod.ModuleBase + 0x343898 + (0x15 * 4));
            var levelId = value ? (int)LevelId.MetalOverlord : (int)LevelId.MetalMadness;
            Memory.Instance.SafeWrite((UIntPtr)addr, new byte[] {(byte)levelId});
        }
    }

    private static List<IAsmHook> _asmHooks;
    private static IReverseWrapper<CompleteLevel> _reverseWrapOnCompleteLevel;
    private static IReverseWrapper<GoLevelSelect> _reverseWrapOnGoLevelSelect;
    private static IReverseWrapper<SetRings> _reverseWrapOnSetRings;
    private static IReverseWrapper<Die> _reverseWrapOnDie;
    private static IReverseWrapper<IncrementCount> _reverseWrapOnIncrementCount;
    private static IReverseWrapper<IncrementEnemyCount> _reverseWrapOnMoveEnemyCount;
    private static IReverseWrapper<IncrementBSCapsuleCount> _reverseWrapOnIncrementBSCapsuleCount;
    private static IReverseWrapper<IncrementGoldBeetleCount> _reverseWrapOnIncrementGoldBeetleCount;
    private static IReverseWrapper<AssignRings> _reverseWrapOnCheckRings;
    private static IReverseWrapper<CompleteEmeraldStage> _reverseWrapOnCompleteEmeraldStage;
    private static IReverseWrapper<SetStateInGame> _reverseWrapOnSetStateInGame;
    private static IReverseWrapper<StartCompleteStage> _reverseWrapOnStartCompleteStage;
    private static IReverseWrapper<GetBonusKey> _reverseWrapOnGetBonusKey;
    private static IReverseWrapper<GetCheckPoint> _reverseWrapOnGetCheckPoint;
    private static IReverseWrapper<SetObjStateSpawned> _reverseWrapOnObjSetStateSpawned;
    private static IReverseWrapper<SetAct> _reverseWrapOnSetAct;
    private static IReverseWrapper<GoSelectActFromSelectLevel> _reverseWrapOnGoSelectActFromSelectLevel;
    private static IReverseWrapper<GoSelectLevelFromSelectAct> _reverseWrapOnGoSelectLevelFromSelectAct;
    private static IReverseWrapper<GoToGameFromLevelSelect> _reverseWrapOnGoToGameFromLevelSelect;
    private static IReverseWrapper<GoCharUncaptureState> _reverseWrapOnGoCharUncaptureState;
    private static IReverseWrapper<GoPlayerChangeModeWait> _reverseWrapOnGoPlayerChangeModeWait;
    private static IReverseWrapper<AddLevel> _reverseWrapOnAddLevel;
    private static IReverseWrapper<InitSetGenerator> _reverseWrapOnInitSetGenerator;
    private static IReverseWrapper<SetTeamInitialPosition> _reverseWrapOnSetTeamInitialPosition;
    private static IReverseWrapper<GetBingoChip> _reverseWrapOnGetBingoChip;
    private static IReverseWrapper<BGMSetFileName> _reverseWrapOnBGMSetFileName;
    private static IReverseWrapper<BGMGetDVDRootPath> _reverseWrapOnBGMGetDVDRootPath;
    
    
    
    
    public void SetupHooks(IReloadedHooks hooks)
    {
        _asmHooks = new List<IAsmHook>();
        string[] goMenuHook = {
            "use32",
            "mov dword[esi+0x438],0x0",
            "mov dword[esi+0x43C],0x1",
            "mov dword[esi+0x440],0x0",
            "mov dword[esi+0x444],0x0"
        };
        _asmHooks.Add(hooks.CreateAsmHook(goMenuHook, (int)(Mod.ModuleBase + 0x50436), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] completeLevelHook = 
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            "push ebx",
            "push edx",
            "push ecx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCompleteLevel, out _reverseWrapOnCompleteLevel)}",
            "pop ecx",
            "pop edx",
            "pop ebx",
            "pop esi",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(completeLevelHook, (int)(Mod.ModuleBase + 0x22EEC0), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] goLevelSelectHook = 
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoLevelSelect, out _reverseWrapOnGoLevelSelect)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(goLevelSelectHook, (int)(Mod.ModuleBase + 0x4F440), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] setRings = 
        {
            "use32",
            "pushad",
            "pushfd",
            "push edx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnSetRings, out _reverseWrapOnSetRings)}",
            "pop edx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(setRings, (int)(Mod.ModuleBase + 0x23AA0), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] die = 
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnDie, out _reverseWrapOnDie)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(die, (int)(Mod.ModuleBase + 0x452B), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] incrementCount =
        {
            "use32",
            "pushad",
            "pushfd",
            "push ecx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnIncrementCount, out _reverseWrapOnIncrementCount)}",
            "pop ecx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(incrementCount, (int)(Mod.ModuleBase + 0x1B4901), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] moveEnemyCount =
        {
            "use32",
            "pushad",
            "pushfd",
            "push ebx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnMoveEnemyCount, out _reverseWrapOnMoveEnemyCount)}",
            "pop ebx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(moveEnemyCount, (int)(Mod.ModuleBase + 0x1DDFD7), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] incrementBSCapsuleCount =
        {
            "use32",
            "pushad",
            "pushfd",
            "push eax",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnIncrementBSCapsuleCount, out _reverseWrapOnIncrementBSCapsuleCount)}",
            "pop eax",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(incrementBSCapsuleCount, (int)(Mod.ModuleBase + 0xD4B76), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] incrementGoldBeetleCount =
        {
            "use32",
            "pushad",
            "pushfd",
            "push edx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnIncrementGoldBeetleCount, out _reverseWrapOnIncrementGoldBeetleCount)}",
            "pop edx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(incrementGoldBeetleCount, (int)(Mod.ModuleBase + 0x1FA390), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] checkRings =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCheckRings, out _reverseWrapOnCheckRings)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(checkRings, (int)(Mod.ModuleBase + 0x1A9DB2), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] completeEmeraldStage =
        {
            "use32",
            "pushad",
            "pushfd",
            "push eax",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCompleteEmeraldStage, out _reverseWrapOnCompleteEmeraldStage)}",
            "pop eax",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(completeEmeraldStage, (int)(Mod.ModuleBase + 0x22F498), AsmHookBehaviour.DoNotExecuteOriginal).Activate());
        
        string[] startCompleteStage =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnStartCompleteStage, out _reverseWrapOnStartCompleteStage)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(startCompleteStage, (int)(Mod.ModuleBase + 0x4454), AsmHookBehaviour.ExecuteFirst).Activate());
        
        string[] setStateInGame =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnSetStateInGame, out _reverseWrapOnSetStateInGame)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(setStateInGame, (int)(Mod.ModuleBase + 0x2774), AsmHookBehaviour.ExecuteAfter).Activate());
        _asmHooks.Add(hooks.CreateAsmHook(setStateInGame, (int)(Mod.ModuleBase + 0x41FC), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] getBonusKey =
        {
            "use32",
            "pushad",
            "pushfd",
            "mov edx,ebp",
            "push edx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetBonusKey, out _reverseWrapOnGetBonusKey)}",
            "pop edx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(getBonusKey, (int)(Mod.ModuleBase + 0x7B325), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] getCheckPoint = 
        {
            "use32",
            "pushad",
            "pushfd",
            "mov edx,eax",
            "push edx",
            "push ecx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetCheckPoint, out _reverseWrapOnGetCheckPoint)}",
            "pop ecx",
            "pop edx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(getCheckPoint, (int)(Mod.ModuleBase + 0x23990), AsmHookBehaviour.DoNotExecuteOriginal).Activate());
        
        string[] setAct =
        {
            "use32",
            "pushad",
            "pushfd",
            "mov eax,esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnSetAct, out _reverseWrapOnSetAct)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(setAct, (int)(Mod.ModuleBase + 0x4B659), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] ObjSetStateSpawned =
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnObjSetStateSpawned, out _reverseWrapOnObjSetStateSpawned)}",
            "pop esi",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(ObjSetStateSpawned, (int)(Mod.ModuleBase + 0x3D9E9), AsmHookBehaviour.ExecuteFirst).Activate());

        string[] SelectActFromSelectLevel =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoSelectActFromSelectLevel, out _reverseWrapOnGoSelectActFromSelectLevel)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(SelectActFromSelectLevel, (int)(Mod.ModuleBase + 0x4B3D4), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] SelectLevelFromSelectAct =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoSelectLevelFromSelectAct, out _reverseWrapOnGoSelectLevelFromSelectAct)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(SelectLevelFromSelectAct, (int)(Mod.ModuleBase + 0x4B541), AsmHookBehaviour.ExecuteAfter).Activate());
        
        string[] GoToGameFromLevelSelect =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoToGameFromLevelSelect, out _reverseWrapOnGoToGameFromLevelSelect)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(GoToGameFromLevelSelect, (int)(Mod.ModuleBase + 0x4B6D3), AsmHookBehaviour.ExecuteAfter).Activate());
        

        string[] GoCharUncaptureState =
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoCharUncaptureState, out _reverseWrapOnGoCharUncaptureState)}",
            "pop esi",
            "popfd",
            "popad"
        };
        
        _asmHooks.Add(hooks.CreateAsmHook(GoCharUncaptureState, (int)(Mod.ModuleBase + 0x1AFFF9), AsmHookBehaviour.ExecuteAfter).Activate());

        
        string[] GoPlayerChangeModeWait =
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGoPlayerChangeModeWait, out _reverseWrapOnGoPlayerChangeModeWait)}",
            "pop esi",
            "popfd",
            "popad"
        };
        
        _asmHooks.Add(hooks.CreateAsmHook(GoPlayerChangeModeWait, (int)(Mod.ModuleBase + 0x1A4555), AsmHookBehaviour.ExecuteAfter).Activate());
        
        
        string[] AddLevel =
        {
            "use32",
            "pushad",
            "pushfd",
            "mov edx,ebp",
            "push edx",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnAddLevel, out _reverseWrapOnAddLevel)}",
            "pop edx",
            "popfd",
            "popad"
        };
        
        _asmHooks.Add(hooks.CreateAsmHook(AddLevel, (int)(Mod.ModuleBase + 0x1B4C81), AsmHookBehaviour.ExecuteAfter).Activate());
        
        
        
        string[] InitSetGenerator =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnInitSetGenerator, out _reverseWrapOnInitSetGenerator)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(InitSetGenerator, (int)(Mod.ModuleBase + 0x3C987), AsmHookBehaviour.ExecuteAfter).Activate());
        
        
        
        string[] SetTeamInitialPosition =
        {
            "use32",
            "pushad",
            "pushfd",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnSetTeamInitialPosition, out _reverseWrapOnSetTeamInitialPosition)}",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(SetTeamInitialPosition, (int)(Mod.ModuleBase + 0x1ABE2D), AsmHookBehaviour.ExecuteFirst).Activate());
        
        
        
        string[] GetBingoChip =
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetBingoChip, out _reverseWrapOnGetBingoChip)}",
            "pop esi",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(GetBingoChip, (int)(Mod.ModuleBase + 0xC5D73), AsmHookBehaviour.ExecuteFirst).Activate());
        
        
        string[] BGMSetFileName =
        {
            "use32",
            "pushad",
            "pushfd",
            "mov ecx,eax",
            "push ecx",
            "push edx",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnBGMSetFileName, out _reverseWrapOnBGMSetFileName)}",
            "pop esi",
            "pop edx",
            "pop ecx",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(BGMSetFileName, (int)(Mod.ModuleBase + 0x3F3AE), AsmHookBehaviour.ExecuteFirst).Activate());
        
        
        string[] BGMGetDVDRootPath =
        {
            "use32",
            "pushad",
            "pushfd",
            "push esi",
            $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnBGMGetDVDRootPath, out _reverseWrapOnBGMGetDVDRootPath)}",
            "pop esi",
            "popfd",
            "popad"
        };
        _asmHooks.Add(hooks.CreateAsmHook(BGMGetDVDRootPath, (int)(Mod.ModuleBase + 0x22B9EB), AsmHookBehaviour.ExecuteAfter).Activate());
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int BGMGetDVDRootPath(int esi);

    private static unsafe int OnBGMGetDVDRootPath(int esi)
    {
        try
        {
            //Console.WriteLine($"OnBGMGetDVDRootPath(esi): 0x{esi:x}");
            if (!Mod.Configuration!.MusicShuffleOptions.MusicShuffle)
                return 0;
            //Console.WriteLine($"OnBGMGetDVDRootPath(esi): Check Passed");
            Mod.MusicShuffleHandler.HandleBGMFilePathHook(esi);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }




    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx, FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int BGMSetFileName(int ecx, int edx, int esi);
    private static unsafe int OnBGMSetFileName(int ecx, int edx, int esi)
    {
        return 0;
        //Console.WriteLine($"OnBGMSetFileName: ECX (EAX): 0x{ecx:x} EDX: 0x{edx:x} ESI: 0x{esi:x}");
        if (!Mod.Configuration!.MusicShuffleOptions.MusicShuffle)
            return 0;
        var length = ecx - esi;
        List<byte> originalName = [];
        for (int i = 0; i < length - 1; i++)
            originalName.Add(*(byte*)(edx + esi + i));

        var originalNameString = Encoding.ASCII.GetString(originalName.ToArray());
        var success = MusicShuffleHandler.Map.TryGetValue(originalNameString, out var newName);
        newName += '\0';
        if (!success) 
            return 0;
        var newNameBytes = Encoding.ASCII.GetBytes(newName.ToArray());
        for (var i = 0; i < newName.Length; i++)
            *(byte*)(edx + esi + i) = newNameBytes[i];
        
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBingoChip(int esi);
    private static int OnGetBingoChip(int esi)
    {
        //Console.WriteLine($"GetBingoChip: 0x{esi:x}");
        Mod.SanityHandler!.HandleBingoChip(esi);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetTeamInitialPosition();
    private static int OnSetTeamInitialPosition()
    {
        //Console.WriteLine($"SetTeamInitialPosition()");
        //Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
        //Mod.AbilityUnlockHandler!.PollUpdates();
        return 0;
    }
    
    
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int InitSetGenerator();
    private static int OnInitSetGenerator()
    {
        StageObjHandler.HandleInitSetGenerator();
        Mod.AbilityUnlockHandler!.PollUpdates();
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AddLevel(int edx);
    private static int OnAddLevel(int edx)
    {
        //ecx team pointer
        //edx is formation char
        //Console.WriteLine($"Adding level edx (ebp) is 0x{edx:x}");

        Team team = Mod.GameHandler!.GetCurrentStory();
        LevelId level = Mod.GameHandler!.GetCurrentLevel();
        Act act = Mod.GameHandler.GetCurrentAct();
        
        //check for SuperHard
        if (team is Team.Sonic && act is Act.Act2 or Act.Act3 &&
            Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
        {
            team = Team.SuperHard;
        }
        
        //handle region
        if (!LevelIdToRegion.ContainsKey(level))
        {
            Console.WriteLine($"Add Level Function run in level {level} that is not in LevelIdToRegion");
            return 0;
        }
        Region region = LevelIdToRegion[level];
        Mod.AbilityUnlockHandler!.HandleLevelUp(team, region, (FormationChar)edx);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoPlayerChangeModeWait(int esi);
    private static int OnGoPlayerChangeModeWait(int esi)
    {
        Mod.AbilityUnlockHandler!.PollUpdates();
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoCharUncaptureState(int esi);
    private static int OnGoCharUncaptureState(int esi)
    {
        Mod.AbilityUnlockHandler!.PollUpdates();
        return 0;
    }
    
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectActFromSelectLevel();
    private static int OnGoSelectActFromSelectLevel()
    {
        //Console.WriteLine("GoSelectActFromSelectLevel");
        Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
        Mod.LevelSpawnData!.PrintUnlockedSpawnData();
        Mod.LevelSpawnHandler!.ShouldCheckForInput = true;
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectLevelFromSelectAct();
    private static int OnGoSelectLevelFromSelectAct()
    {
        //Console.WriteLine("GoSelectLevelFromSelectAct");
        Mod.LevelSpawnHandler!.ShouldCheckForInput = false;
        Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoToGameFromLevelSelect();
    private static int OnGoToGameFromLevelSelect()
    {
        //Console.WriteLine($"GoToGameFromLevelSelect. Spawn Index: {Mod.LevelSpawnHandler!.SpawnPosIndex}");
        Mod.LevelSpawnHandler!.ShouldCheckForInput = false;
        Mod.LevelSpawnHandler.ChangeSpawnPos();
        //Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
        return 0;
    }
    
    
    
    
    public void SetCurrentAct(Act act)
    {
        unsafe
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777E4);
            *(byte*)(baseAddress + 0x28) = (byte)act;
        }
    }

    public unsafe void SetBonusKey(bool value)
    {
        try
        {
            if (!InGame(true))
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
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetAct();
    private static int OnSetAct()
    {
        //return 0;
        unsafe
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777B4);
            var team = *(int*)(baseAddress + 0x220);
            //var level = (LevelId)Mod.UserInterface!.LevelTracker.LevelMapping[*(int*)(baseAddress + 0x194)];
            
            var levelSelectIndex = *(int*)(baseAddress + 0x194);
            var level = (LevelId)Mod.UserInterface.LevelTracker.LevelMapping[levelSelectIndex];
            //CURRENT LEVEL IS NOT VALID HERE
            //STAGE OBJS ARE NOT LOADED IN MEMORY YET


            if (Mod.LevelSpawnData!.GetAllSpawnDataForLevel((Team)team, level).Last().Bonusstage)
            {
                if (Mod.LevelSpawnData!.GetLevelSelectUiText((Team)team, level) == "Bonus Stage")
                {
                    var addr = (UIntPtr)((int)Mod.ModuleBase + 0x343898 + 4 * levelSelectIndex);
                    Memory.Instance.SafeWrite(addr, [(byte)BonusStageForLevel[level]]);
                    return 0;
                }
                else
                {
                    var addr = (UIntPtr)((int)Mod.ModuleBase + 0x343898 + 4 * levelSelectIndex);
                    Memory.Instance.SafeWrite(addr, [(byte)level]);
                }
            }
            
            if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 &&
                (Team)team == Team.Sonic &&
                Mod.GameHandler.GetCurrentAct() == Act.Act2)
                Mod.GameHandler.SetCurrentAct(Act.Act3);
            return 0;
        }
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetObjStateSpawned(int esi);
    private static int OnObjSetStateSpawned(int esi)
    {
        //StageObjHandler.OnObjSetStateSpawned(esi);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBonusKey(int edx);
    private static int OnGetBonusKey(int edx)
    {
        Mod.SanityHandler!.HandleKeySanity(edx);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetCheckPoint(int priority, int pointer);
    private static int OnGetCheckPoint(int ecx, int edx)
    {
        Mod.SanityHandler!.HandleCheckPointSanity(ecx, edx);
        //Mod.GameHandler.Kill();
        return 0;
    }
    
    [UnmanagedFunctionPointer(CallingConvention.FastCall)]
    private delegate void LifeSetFunc(int ecx, int edx);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void RestartFunc();

    [Function(new[] {
        FunctionAttribute.Register.ecx, 
        FunctionAttribute.Register.edx, 
        FunctionAttribute.Register.ebx, 
        FunctionAttribute.Register.esi 
    },FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteLevel(int isMission2, int levelIndex, int rank, int story);
    private static int OnCompleteLevel(int ecx, int edx, int ebx, int esi)
    {
        try
        {
            var isMission2 = ecx; 
            var levelIndex = edx;
            var story = (Team)esi;
            var rank = (Rank)ebx;
            var apHandler = Mod.ArchipelagoHandler!;
            var slotData = apHandler.SlotData;

            if (levelIndex == 36)
                levelIndex = 8;
            
            //Console.WriteLine($"OnCompleteLevel Here. IsAct2: {isMission2},  LevelIndex: {levelIndex}, Rank: {rank}, Story: {story}");
            
            
            if (levelIndex > 25)
                return 0;
            
            //SeaGate is 25
            //maybe do special handling here
            
            if ((LevelId)levelIndex == LevelId.MetalOverlord) 
            {
                apHandler.CheckLocation(0x230E);
                Logger.Log("Victory!");
                apHandler.Release();
                return 1;
            }
            
            if (rank <= slotData.RequiredRank) 
            {
                Logger.Log("Did not reach the required rank.");
                Console.WriteLine($"Did not reach the required rank. {rank} is not the required {slotData.RequiredRank}");
                return 0;
            }
            
            if (Mod.ArchipelagoHandler.SlotData.StoriesActive[story] is MissionsActive.None)
                return 0;
            if (levelIndex < 16 && Mod.ArchipelagoHandler.SlotData.StoriesActive[story] is not MissionsActive.Both)
            {
                if (Mod.ArchipelagoHandler.SlotData.StoriesActive[story] is MissionsActive.Act1 && isMission2 == 1)
                    return 0;
                if (Mod.ArchipelagoHandler.SlotData.StoriesActive[story] is MissionsActive.Act2 && isMission2 == 0)
                    return 0;
            }
            //Console.WriteLine($"Story: {(int)story} Level: {levelIndex} Rank: {(int)rank} IsMission2: {isMission2}");

            var locationId = 0xA0 + (int)story * 42 + (levelIndex - 2) * 2 + isMission2;

            if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && story == Team.Sonic && isMission2 == 1)
            {
                //hardcoded SuperHard ID here
                story = Team.SuperHard;
                locationId = SuperHardModeId + (levelIndex - 2);
                //Console.WriteLine($"OnCompleteLevel Here. IsAct2: {isMission2},  LevelIndex: {levelIndex}, Rank: {rank}, Story: {story}");

            }
            
            
            if (levelIndex is >= 16 and < 25)
            {
                for (var gateIndex = 0; gateIndex < slotData.GateData.Count - 1; gateIndex++)
                {
                    if (slotData.GateData[gateIndex].BossLevel.LevelId == (LevelId)levelIndex)
                    {
                        slotData.GateData[gateIndex + 1].IsUnlocked = true;
                        slotData.RecalculateOpenLevels();
                        unsafe
                        {
                            Mod.SaveDataHandler!.CustomSaveData.GateBossComplete[gateIndex] = true;
                        }
                    }
                    Mod.ArchipelagoHandler?.Save();
                    locationId = 0xA0 + (levelIndex - 2) * 2;
                    foreach (var team in slotData.StoriesActive.Where(team => team.Value != MissionsActive.None))
                        apHandler.CheckLocation(locationId + 42 * (int)team.Key);
                }
                return 1;
            }

            //Console.WriteLine($"Checking Mission Completion Location Here: Id = {(0x93930000 + locationId):X}");
            //Mod.SaveDataHandler.CustomSaveData.LevelsGoaled[story][(LevelId)levelIndex] = true;
            Mod.LevelSpawnData.BonusStageUnlockCallback(story, (LevelId)levelIndex, goal: true);
            slotData.RecalculateOpenLevels(!apHandler.IsLocationChecked(locationId));
            apHandler.CheckLocation(locationId);
            /*
            var t = new Thread(start: () =>
            {
                Thread.Sleep(1000);
                //Console.WriteLine($"Delayed Recalc Open Levels Here");
                slotData.RecalculateOpenLevels();
            });
            t.Start();
            */
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return 1;
    }
    

    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoLevelSelect();
    private static int OnGoLevelSelect()
    {
        Mod.ArchipelagoHandler!.SlotData.RecalculateOpenLevels();
        return 1;
    }
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetStateInGame();
    private static int OnSetStateInGame()
    {
        
        Mod.ItemHandler!.HandleCachedItems();
        Mod.AbilityUnlockHandler!.PollUpdates();
        if (Mod.GameHandler.GetCurrentAct() == Act.Act3)
        {
            Mod.GameHandler.SetCurrentAct(Act.Act2);
            Mod.GameHandler.SetBonusKey(true);
        }
        
        return 1;
    }

    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetRings(int amount);
    private static int OnSetRings(int amount)
    {
        if (!Mod.ArchipelagoHandler!.SlotData.RingLink) 
            return 0;
        if (Mod.ArchipelagoHandler.SlotData.RingLinkOverlord 
            || Mod.GameHandler!.GetCurrentLevel() != LevelId.MetalOverlord)
            Mod.ArchipelagoHandler.SendRing(amount);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AssignRings();
    private static int OnCheckRings()
    {
        unsafe
        {
            var newCount = *(int*)(Mod.ModuleBase + 0x5DD70C);
            Mod.SanityHandler!.CheckRingSanity(newCount);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int StartCompleteStage();
    private static int OnStartCompleteStage()
    {
        Mod.SanityHandler!.CheckEnemyCount(100);
        Mod.SanityHandler.CheckRingSanity(500);
        //if (Mod.GameHandler.GetCurrentAct() == Act.Act3)
        //    Mod.GameHandler.SetBonusKey(true);
        Mod.GameHandler.SetBonusKey(false);
        return 0;
    }

    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int Die();

    public static bool SomeoneElseDied;
    private static int OnDie()
    {
        //Console.WriteLine($"OnDie Here:: SomeoneElseDied: {SomeoneElseDied}");
        if (SomeoneElseDied)
        {
            SomeoneElseDied = false;
            return 1;
        }
        if (Mod.ArchipelagoHandler!.SlotData.DeathLink)
            Mod.ArchipelagoHandler.SendDeath();
        return 0;
    }

    private static List<Stage> stealthStages =  new List<Stage>
    {
        new (LevelId.OceanPalace, Team.Chaotix, Act.Act2),
        new (LevelId.FrogForest, Team.Chaotix, Act.Act1),
        new (LevelId.FrogForest, Team.Chaotix, Act.Act2),
        new (LevelId.EggFleet, Team.Chaotix, Act.Act1),
        new (LevelId.EggFleet, Team.Chaotix, Act.Act2),
        new (LevelId.HangCastle, Team.Chaotix, Act.Act2)
    };

    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementCount(int newCount);

    private static int OnIncrementCount(int newCount)
    {
        Mod.SanityHandler!.HandleCountIncreased(newCount);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ebx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementEnemyCount(int newCount);

    private static int OnMoveEnemyCount(int newCount)
    {
        Mod.SanityHandler!.CheckEnemyCount(newCount);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementGoldBeetleCount(int newCount);

    private static int OnIncrementGoldBeetleCount(int newCount)
    {
        Mod.SanityHandler!.HandleGoldBeetleCountIncreased(newCount);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementBSCapsuleCount(int ptr);
    private static int OnIncrementBSCapsuleCount(int ptr)
    {
        unsafe
        {
            var newCount = *(int*)(ptr + 0x23C);
            Mod.SanityHandler!.HandleBSCapsuleCountIncreased(newCount);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteEmeraldStage(int emeraldAddressOffset);
    private static int OnCompleteEmeraldStage(int emeraldAddressOffset)
    {
        var locationId = 0x148 + (emeraldAddressOffset - 21) / 3;
        Mod.ArchipelagoHandler!.CheckLocation(locationId);
        return 0;
    }
}