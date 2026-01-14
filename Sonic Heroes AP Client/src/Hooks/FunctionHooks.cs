

using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.Sanity;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Sanity.BingoChip;
using Sonic_Heroes_AP_Client.Sanity.BonusKeys;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Sonic_Heroes_AP_Client.StageObj;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Hooks;

public class FunctionHooks
{
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
    private static IReverseWrapper<TObjResultConstructStart> _reverseWrapOnTObjResultConstructStart;
    private static IReverseWrapper<SetActInLevelSelectToZero> _reverseWrapOnGoSetActInLevelSelectToZero;
    private static IReverseWrapper<ChangeActInLevelSelect> _reverseWrapOnChangeActInLevelSelect;
    private static IReverseWrapper<ChangeModeToFlying> _reverseWrapOnChangeModeToFlying;
    
    
    
    public void SetUpFunctionHooks(IReloadedHooks hooks)
    {
        try
        {
            _asmHooks = [];
        
            string[] goMenuHook =
            [
                "use32",
                "mov dword[esi+0x438],0x0",
                "mov dword[esi+0x43C],0x1",
                "mov dword[esi+0x440],0x0",
                "mov dword[esi+0x444],0x0"
            ];
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
            
            
            string[] TObjResultConstructStart =
            {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnTObjResultConstructStart, out _reverseWrapOnSetTeamInitialPosition)}",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(TObjResultConstructStart, (int)(Mod.ModuleBase + 0x36580), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
            string[] SetActInLevelSelectToZero =
            {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnSetActInLevelSelectToZero, out _reverseWrapOnGoSetActInLevelSelectToZero)}",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(SetActInLevelSelectToZero, (int)(Mod.ModuleBase + 0x4B1F1), AsmHookBehaviour.ExecuteAfter).Activate());
            
            
            string[] ChangeActInLevelSelect =
            {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnChangeActInLevelSelect, out _reverseWrapOnChangeActInLevelSelect)}",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(ChangeActInLevelSelect, (int)(Mod.ModuleBase + 0x4B46D), AsmHookBehaviour.ExecuteAfter).Activate());
         
            
            
            
            string[] ChangeModeToFlying =
            {
                "use32",
                "pushad",
                "pushfd",
                "push eax",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnChangeModeToFlying, out _reverseWrapOnChangeModeToFlying)}",
                "pop eax",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(ChangeModeToFlying, (int)(Mod.ModuleBase + 0x1C9C81), AsmHookBehaviour.ExecuteAfter).Activate());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ChangeModeToFlying(int flyingCharObjPtr);
    private static int OnChangeModeToFlying(int eax)
    {
        try
        {
            //Console.WriteLine($"ChangeModeToFlying 0x{eax:x}");
            if (!GameStateHandler.InGame(true))
                return 1;

            var team = GameStateHandler.GetCurrentStory();
            var level = GameStateHandler.GetCurrentLevel();
            
            
            //Console.WriteLine($"Team: {team}, Level: {level}");

            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue((LevelId)level!, out var region))
                return 1;

            var canFly = AbilityCharacterManager.CanFly((Team)team!, region);
            
            if (!canFly)
                Memory.Instance.SafeWrite((UIntPtr)(eax + 0x994), BitConverter.GetBytes(AbilityCharacterGameWrites.LockedFlightMeterValue));
            //Console.WriteLine($"Region: {region} CanFly: {canFly}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }

    
    
    
    
    
        
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ChangeActInLevelSelect();
    private static unsafe int OnChangeActInLevelSelect()
    {
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(levelSelectPtr + 0x2BC);
            Mod.LevelSelectManager.ActSelectedInLevelSelect = (Act)actIndex;
            LevelSpawnUnlockHandler.SpawnPosIndex = 0;
            //Console.WriteLine($"Setting Act Selected in Level Select to {Mod.LevelSelectManager.ActSelectedInLevelSelect}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetActInLevelSelectToZero();
    private static int OnSetActInLevelSelectToZero()
    {
        try
        {
            Mod.LevelSelectManager.ActSelectedInLevelSelect = Act.Act1;
            //Console.WriteLine($"Setting Act Selected in Level Select to {Mod.LevelSelectManager.ActSelectedInLevelSelect}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 1;
    }
    
    [Function(new[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx, FunctionAttribute.Register.ebx, FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteLevel(int isMission2, int levelIndex, int rank, int story);
    public static int OnCompleteLevel(int ecx, int edx, int ebx, int esi)
    {
        try
        {
            var isMission2 = ecx; 
            var levelIndex = edx;
            var team = (Team)esi;
            var rank = (Rank)ebx;
            var apHandler = Mod.ArchipelagoHandler!;
            var slotData = apHandler.SlotData;

            //Chaotix Rail Canyon
            if (levelIndex == 36)
                levelIndex = 8;
            
            //Console.WriteLine($"OnCompleteLevel Here. IsAct2: {isMission2},  LevelIndex: {levelIndex}, Rank: {rank}, Story: {story}");
            
            
            if (levelIndex > 25)
                return 0;
            
            //SeaGate is 25
            //maybe do special handling here
            
            if ((LevelId)levelIndex == LevelId.SeaGate && Mod.LevelSelectManager.FinalBoss is FinalBoss.SeaGate) 
            {
                apHandler.CheckLocation(0x230E);
                LoggerWindow.Log("Victory!");
                apHandler.Release();
                return 1;
            }
            
            if ((LevelId)levelIndex == LevelId.MetalOverlord && Mod.LevelSelectManager.FinalBoss is FinalBoss.MetalMadness or FinalBoss.MetalOverlord) 
            {
                apHandler.CheckLocation(0x230E);
                LoggerWindow.Log("Victory!");
                apHandler.Release();
                return 1;
            }
            
            if (rank <= slotData.RequiredRank) 
            {
                LoggerWindow.Log("Did not reach the required rank.");
                Console.WriteLine($"Did not reach the required rank. {rank} is not the required {slotData.RequiredRank}");
                return 0;
            }
            
            var locationId = 0xA0 + (int)team * 42 + (levelIndex - 2) * 2 + isMission2;

            if (team is Team.Sonic && isMission2 == 1 &&
                (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode)!)
            {
                team = Team.SuperHardMode;
                locationId = SonicHeroesDefinitions.SuperHardModeId + (levelIndex - 2);
            }
            else
            {
                if (!(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team)!)
                    return 0;
                
                if (levelIndex < (int)LevelId.EggHawk && !Mod.LevelSelectManager.IsThisTeamActEnabled(team, (Act)isMission2))
                    return 0;
            }
            
            if (levelIndex is >= (int)LevelId.EggHawk and <= (int)LevelId.SeaGate)
            {
                for (var gateIndex = 0; gateIndex < Mod.LevelSelectManager.GateData.Count - 1; gateIndex++)
                {
                    if (Mod.LevelSelectManager.GateData[gateIndex].BossLevel.LevelId == (LevelId)levelIndex)
                    {
                        Mod.LevelSelectManager.GateData[gateIndex + 1].IsUnlocked = true;
                        Mod.LevelSelectManager.RecalculateOpenLevels();
                        unsafe
                        {
                            Mod.SaveDataHandler!.CustomSaveData.GateBossComplete[gateIndex] = true;
                        }
                    }
                    Mod.ArchipelagoHandler?.Save();
                    locationId = 0xA0 + (levelIndex - 2) * 2;

                    foreach (var tempTeam in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!))
                    {
                        if (tempTeam == Team.SuperHardMode)
                            apHandler.CheckLocation(locationId + 42 * (int)Team.Sonic);
                        else
                            apHandler.CheckLocation(locationId + 42 * (int)tempTeam);
                    }
                        
                }
                return 1;
            }

            //Console.WriteLine($"Checking Mission Completion Location Here: Id = {(0x93930000 + locationId):X}");
            //Mod.SaveDataHandler.CustomSaveData.LevelsGoaled[story][(LevelId)levelIndex] = true;
            LevelSpawnUnlockHandler.BonusStageUnlockCallback(team, (LevelId)levelIndex, goal: true);

            var isLevelNotCompletedYet = false;

            if (team is not Team.SuperHardMode)
            {
                isLevelNotCompletedYet = isMission2 > 0
                    ? !apHandler.IsLocationChecked(locationId) && !apHandler.IsLocationChecked(locationId - 1) && !apHandler.IsLocationChecked(locationId) && !apHandler.IsLocationChecked(locationId)
                    : !apHandler.IsLocationChecked(locationId) && !apHandler.IsLocationChecked(locationId + 1) && !apHandler.IsLocationChecked(locationId) && !apHandler.IsLocationChecked(locationId);
            }
            else
            {
                isLevelNotCompletedYet = !apHandler.IsLocationChecked(locationId);
            }
            Mod.LevelSelectManager.RecalculateOpenLevels(team, isLevelNotCompletedYet);
            apHandler.CheckLocation(locationId);
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
        try
        {
            Mod.LevelSelectManager.RecalculateOpenLevels();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetRings(int amount);
    private static int OnSetRings(int amount)
    {
        try
        {
            if (!RingLinkHandler.IsRingLinkEnabled()) 
                return 0;
            if (RingLinkHandler.IsRingLinkOverlord() || GameStateHandler.GetCurrentLevel() != LevelId.MetalOverlord)
                RingLinkHandler.SendRingPacket(amount);
            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int Die();
    private static int OnDie()
    {
        try
        {
            if (DeathLinkHandler.SomeoneElseDied)
            {
                DeathLinkHandler.SomeoneElseDied = false;
                return 0;
            }
            if (DeathLinkHandler.IsDeathLinkEnabled())
                DeathLinkHandler.SendDeath();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementCount(int newCount);
    private static int OnIncrementCount(int newCount)
    {
        try
        {
            ObjSanityHandler.HandleCountIncreased(newCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ebx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementEnemyCount(int newCount);
    private static int OnMoveEnemyCount(int newCount)
    {
        try
        {
            ObjSanityHandler.CheckEnemyCount(newCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementBSCapsuleCount(int ptr);
    private static unsafe int OnIncrementBSCapsuleCount(int ptr)
    {
        try
        {
            var newCount = *(int*)(ptr + 0x23C);
            ObjSanityHandler.HandleBSCapsuleCountIncreased(newCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementGoldBeetleCount(int newCount);
    private static int OnIncrementGoldBeetleCount(int newCount)
    {
        try
        {
            ObjSanityHandler.HandleGoldBeetleCountIncreased(newCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AssignRings();
    private static unsafe int OnCheckRings()
    {
        try
        {
            var newCount = *(int*)(Mod.ModuleBase + 0x5DD70C);
            ObjSanityHandler.CheckRingSanity(newCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteEmeraldStage(int emeraldAddressOffset);
    private static int OnCompleteEmeraldStage(int emeraldAddressOffset)
    {
        try
        {
            var locationId = SonicHeroesDefinitions.EmeraldStartId + (emeraldAddressOffset - 21) / 3;
            Mod.ArchipelagoHandler!.CheckLocation(locationId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int StartCompleteStage();
    private static int OnStartCompleteStage()
    {
        try
        {
            ObjSanityHandler.CheckEnemyCount(100);
            ObjSanityHandler.CheckRingSanity(500);
            GameStateGameWrites.SetBonusKey(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetStateInGame();
    private static int OnSetStateInGame()
    {
        try
        {
            ItemHandler.HandleCachedItems();
            AbilityCharacterManager.PollUpdates();
        
            if (GameStateHandler.GetCurrentAct() != Act.Act3) 
                return 1;
            GameStateGameWrites.SetCurrentAct(Act.Act2);
            //GameStateGameWrites.SetBonusKey(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBonusKey(int edx);
    private static int OnGetBonusKey(int edx)
    {
        try
        {
            KeySanityHandler.HandleKeySanity(edx);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetCheckPoint(int priority, int pointer);
    private static int OnGetCheckPoint(int ecx, int edx)
    {
        try
        {
            CheckpointSanityHandler.HandleCheckPointSanity(ecx, edx);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        //Mod.LevelSpawnHandler.SpawnTeamHere(2);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetAct();
    private static int OnSetAct()
    {
        try
        {
            //CURRENT LEVEL IS NOT VALID HERE
            //STAGE OBJS ARE NOT LOADED IN MEMORY YET
            LevelSpawnUnlockHandler.OnSetActSpawnPosCallback();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetObjStateSpawned(int esi);
    private static int OnObjSetStateSpawned(int esi)
    {
        try
        {
            //StageObjHandler.OnObjSetStateSpawned(esi);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectActFromSelectLevel();
    private static int OnGoSelectActFromSelectLevel()
    {
        try
        {
            //Console.WriteLine("GoSelectActFromSelectLevel");
            LevelSpawnUnlockHandler.SpawnPosIndex = 0;
            //Mod.LevelSpawnData!.PrintUnlockedSpawnData();
            LevelSpawnUnlockHandler.ShouldCheckForInput = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectLevelFromSelectAct();
    private static int OnGoSelectLevelFromSelectAct()
    {
        try
        {
            //Console.WriteLine("GoSelectLevelFromSelectAct");
            LevelSpawnUnlockHandler.ShouldCheckForInput = false;
            LevelSpawnUnlockHandler.SpawnPosIndex = 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoToGameFromLevelSelect();
    private static int OnGoToGameFromLevelSelect()
    {
        try
        {
            //Console.WriteLine($"GoToGameFromLevelSelect. Spawn Index: {Mod.LevelSpawnHandler!.SpawnPosIndex}");
            LevelSpawnUnlockHandler.ShouldCheckForInput = false;
            LevelSpawnUnlockHandler.GoToGameSpawnPosCallback();
            //Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoCharUncaptureState(int esi);
    private static int OnGoCharUncaptureState(int esi)
    {
        try
        {
            AbilityCharacterManager.PollUpdates();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoPlayerChangeModeWait(int esi);
    private static int OnGoPlayerChangeModeWait(int esi)
    {
        try
        {
            AbilityCharacterManager.PollUpdates();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AddLevel(int edx);
    private static int OnAddLevel(int edx)
    {
        try
        {
            //ecx team pointer
            //edx is formation char
            //Console.WriteLine($"Adding level edx (ebp) is 0x{edx:x}");

            var team = GameStateHandler.GetCurrentStory();
            var level = GameStateHandler.GetCurrentLevel();
            var act = GameStateHandler.GetCurrentAct();
        
            //handle region
            if (!SonicHeroesDefinitions.LevelIdToRegion.ContainsKey((LevelId)level!))
            {
                Console.WriteLine($"Add Level Function run in level {level} that is not in LevelIdToRegion");
                return 0;
            }
            var region = SonicHeroesDefinitions.LevelIdToRegion[(LevelId)level];
            AbilityCharacterManager.HandleLevelUp((Team)team!, region, (FormationChar)edx);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int InitSetGenerator();
    private static int OnInitSetGenerator()
    {
        try
        {
            StageObjHandler.HandleInitSetGenerator();
            AbilityCharacterManager.PollUpdates();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetTeamInitialPosition();
    private static int OnSetTeamInitialPosition()
    {
        try
        {
            //Console.WriteLine($"SetTeamInitialPosition()");
            //Mod.LevelSpawnHandler!.SpawnPosIndex = 0;
            //Mod.AbilityUnlockHandler!.PollUpdates();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBingoChip(int esi);
    private static int OnGetBingoChip(int esi)
    {
        try
        {
            //Console.WriteLine($"GetBingoChip: 0x{esi:x}");
            BingoChipSanityHandler.HandleBingoChip(esi);
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
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
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
    public delegate int BGMGetDVDRootPath(int esi);

    private static int OnBGMGetDVDRootPath(int esi)
    {
        try
        {
            //Console.WriteLine($"OnBGMGetDVDRootPath(esi): 0x{esi:x}");
            if (!Mod.Configuration!.MusicShuffleOptions.MusicShuffle)
                return 0;
            //Console.WriteLine($"OnBGMGetDVDRootPath(esi): Check Passed");
            MusicShuffleHandler.HandleBGMFilePathHook(esi);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
    
    
    
    //TObjResultConstructStart
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int TObjResultConstructStart();
    private static int OnTObjResultConstructStart()
    {
        try
        {
            if (GameStateHandler.GetCurrentLevel() == LevelId.SeaGate)
            {
                Console.WriteLine($"I think that you just finished Sea Gate.");
                if (Mod.LevelSelectManager.FinalBoss is FinalBoss.SeaGate)
                    Mod.ArchipelagoHandler.Release();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return 0;
    }
}