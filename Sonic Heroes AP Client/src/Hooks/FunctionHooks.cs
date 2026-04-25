

using System.Drawing;
using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.Sanity;
using Sonic_Heroes_AP_Client.Sanity.BingoChip;
using Sonic_Heroes_AP_Client.Sanity.BonusKeys;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Sonic_Heroes_AP_Client.StageObj;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Hooks;

public static class FunctionHooks
{
    private const string TaskName = "GameThread";
    
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
    private static IReverseWrapper<ScatteredRingConstructor> _reverseWrapOnScatteredRingConstructor;
    private static IReverseWrapper<PickUpRing> _reverseWrapOnPickUpRing;
    private static IReverseWrapper<EnemyDestroyMyself> _reverseWrapOnEnemyDestroyMyself;
    private static IReverseWrapper<E2000Killed> _reverseWrapOnE2000Killed;
    private static IReverseWrapper<EggHammerKilled> _reverseWrapOnEggHammerKilled;
    private static IReverseWrapper<PowerAttackGiveSFAMeter> _reverseWrapOnPowerAttackGiveSFAMeter;
    private static IReverseWrapper<ItemBoxPickUp> _reverseWrapOnItemBoxPickUp;
    private static IReverseWrapper<ItemBaloonPickUp> _reverseWrapOnItemBaloonPickUp;
    private static IReverseWrapper<HintRingActivated> _reverseWrapOnHintRingActivated;
    
    
    
    public static void SetUpFunctionHooks(IReloadedHooks hooks)
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
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnTObjResultConstructStart, out _reverseWrapOnTObjResultConstructStart)}",
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
            
            
            string[] ScatteredRingConstructor =
            {
                "use32",
                "pushad",
                "pushfd",
                "push esp",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnScatteredRingConstructor, out _reverseWrapOnScatteredRingConstructor)}",
                "pop esp",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(ScatteredRingConstructor, (int)(Mod.ModuleBase + 0x825F7), AsmHookBehaviour.ExecuteFirst).Activate());
            
            string[] PickUpRing =
            {
                "use32",
                "pushad",
                "pushfd",
                "mov ecx, esi",
                "add ecx, 0xCA",
                "movsx ecx, byte [ecx]",
                "push ecx",
                "push esi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnPickUpRing, out _reverseWrapOnPickUpRing)}",
                "pop esi",
                "pop ecx",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(PickUpRing, (int)(Mod.ModuleBase + 0x83366), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
            string[] EnemyDestroyMyself =
            {
                "use32",
                "pushad",
                "pushfd",
                "push esi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnEnemyDestroyMyself, out _reverseWrapOnEnemyDestroyMyself)}",
                "pop esi",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(EnemyDestroyMyself, (int)(Mod.ModuleBase + 0x1E4D22), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
            string[] E2000Killed =
            {
                "use32",
                "pushad",
                "pushfd",
                "push esi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnE2000Killed, out _reverseWrapOnE2000Killed)}",
                "pop esi",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(E2000Killed, (int)(Mod.ModuleBase + 0x1F2630), AsmHookBehaviour.ExecuteFirst).Activate());
            
            string[] EggHammerKilled =
            {
                "use32",
                "pushad",
                "pushfd",
                "push esi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnEggHammerKilled, out _reverseWrapOnEggHammerKilled)}",
                "pop esi",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(EggHammerKilled, (int)(Mod.ModuleBase + 0x206430), AsmHookBehaviour.ExecuteFirst).Activate());
            
            string[] PowerAttackGiveSFAMeter =
            {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnPowerAttackGiveSFAMeter, out _reverseWrapOnPowerAttackGiveSFAMeter)}",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(PowerAttackGiveSFAMeter, (int)(Mod.ModuleBase + 0x1AF426), AsmHookBehaviour.ExecuteAfter).Activate());
            
            string[] ItemBoxPickUp =
            {
                "use32",
                "pushad",
                "pushfd",
                "push edi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnItemBoxPickUp, out _reverseWrapOnItemBoxPickUp)}",
                "pop edi",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(ItemBoxPickUp, (int)(Mod.ModuleBase + 0x7969A), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
            
            string[] ItemBaloonPickUp =
            {
                "use32",
                "pushad",
                "pushfd",
                "mov edx,ebp",
                "push edx",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnItemBaloonPickUp, out _reverseWrapOnItemBaloonPickUp)}",
                "pop edx",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(ItemBaloonPickUp, (int)(Mod.ModuleBase + 0x7852A), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
            string[] HintRingActivated =
            {
                "use32",
                "pushad",
                "pushfd",
                "push edi",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnHintRingActivated, out _reverseWrapOnHintRingActivated)}",
                "pop edi",
                "popfd",
                "popad"
            };
            _asmHooks.Add(hooks.CreateAsmHook(HintRingActivated, (int)(Mod.ModuleBase + 0x76345), AsmHookBehaviour.ExecuteFirst).Activate());
            
            
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
    }
    
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int HintRingActivated(int hintRingPtr);
    private static unsafe int OnHintRingActivated(int edi)
    {
        LoggingHandler.LogMessage($"HintRingActivated Start edi: 0x{edi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //var staticPtr = *(int*)(edi + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"HintRingActivated Start", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ItemBaloonPickUp(int itemBaloonPtr);
    private static unsafe int OnItemBaloonPickUp(int edx)
    {
        LoggingHandler.LogMessage($"ItemBaloonPickUp Start edx: 0x{edx:X}", TaskName, LogLevel.SuperDebug);
        //Balloon has 1 L because that is how the game stores it
        try
        {
            //var staticPtr = *(int*)(edx + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"ItemBaloonPickUp End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ItemBoxPickUp(int itemBoxPtr);
    private static unsafe int OnItemBoxPickUp(int edi)
    {
        LoggingHandler.LogMessage($"ItemBoxPickUp Start edi: 0x{edi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //var staticPtr = *(int*)(edi + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"ItemBoxPickUp End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int PowerAttackGiveSFAMeter();
    private static unsafe int OnPowerAttackGiveSFAMeter()
    {
        LoggingHandler.LogMessage($"PowerAttackGiveSFAMeter Start", TaskName, LogLevel.SuperDebug);
        try
        {
            if (!GameStateHandler.InGame(TaskName))
            {
                LoggingHandler.LogMessage($"PowerAttackGiveSFAMeter End (Not InGame)", TaskName, LogLevel.SuperDebug);
                return 0;
            }
                

            var team = (Team)GameStateHandler.GetCurrentStory(TaskName)!;
            var level = (LevelId)GameStateHandler.GetCurrentLevel(TaskName)!;

            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var region))
            {
                LoggingHandler.LogMessage($"PowerAttackGiveSFAMeter End (Level not In Region)", TaskName, LogLevel.SuperDebug);
                return 0;
            }
                

            if (Mod.SaveDataHandler.CustomSaveData!.UnlockSaveData[team].AbilityUnlocks[region][Ability.PowerAttack])
            {
                LoggingHandler.LogMessage($"PowerAttackGiveSFAMeter End (Have Power Attack)", TaskName, LogLevel.SuperDebug);
                //do nothing if have power attack
                return 0;
            }
                

            var teamBlastPtr = (float*)(Mod.ModuleBase + 0x5DD72C);

            (*teamBlastPtr) -= 1.0f;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"PowerAttackGiveSFAMeter End (Dont Have Power Attack)", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int EggHammerKilled(int enemyPtr);
    private static unsafe int OnEggHammerKilled(int esi)
    {
        LoggingHandler.LogMessage($"EggHammerKilled Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //var staticPtr = *(int*)(esi + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"EggHammerKilled Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int E2000Killed(int enemyPtr);
    private static unsafe int OnE2000Killed(int esi)
    {
        LoggingHandler.LogMessage($"E2000Killed Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //var staticPtr = *(int*)(esi + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"E2000Killed End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int EnemyDestroyMyself(int enemyPtr);
    private static unsafe int OnEnemyDestroyMyself(int esi)
    {
        LoggingHandler.LogMessage($"EnemyDestroyMyself Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //var staticPtr = *(int*)(esi + 0x2C);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"EnemyDestroyMyself End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.esi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int PickUpRing(int ringIndex, int ringSubstancePtr);
    private static unsafe int OnPickUpRing(int ecx, int esi)
    {
        LoggingHandler.LogMessage($"PickUpRing Start ecx: {ecx} esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            var heapPtr = *(int*)(esi + 0xD8);
            //LoggingHandler.LogMessage($"HeapPtr: 0x{heapPtr:x}", TaskName, LogLevel.SuperDebug);
            
            var ringGroupType = *(byte*)(heapPtr + 0x29);
            //LoggingHandler.LogMessage($"RingGroupType: {ringGroupType}", TaskName, LogLevel.SuperDebug);
            
            var numRingsTotal = *(byte*)(heapPtr + 0x28);
            //LoggingHandler.LogMessage($"NumRingsTotal: {numRingsTotal}", TaskName, LogLevel.SuperDebug);

            if (ringGroupType == 4)
            {
                LoggingHandler.LogMessage($"PickUpRing End (Scattered Ring Group)", TaskName, LogLevel.SuperDebug);
                return 1;
            }
            
            //var linkedListStartPtr = *(int*)(heapPtr + 0x4C);
            //esi points to current entry
            //list is list of TObjRingSubstance
            //LoggingHandler.LogMessage($"LinkedListStartPtr: 0x{linkedListStartPtr:x}", TaskName, LogLevel.SuperDebug);
            
            var staticPtr = *(int*)(heapPtr + 0x54);
            //LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:x}", TaskName, LogLevel.SuperDebug);
            
            var staticRingCount =  *(byte*)(*(int*)(staticPtr + 0x2C) + 0x2);
            //LoggingHandler.LogMessage($"StaticRingCount: {staticRingCount}", TaskName, LogLevel.SuperDebug);
            
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"PickUpRing End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esp }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ScatteredRingConstructor(int stackPtr);
    private static unsafe int OnScatteredRingConstructor(int esp)
    {
        LoggingHandler.LogMessage($"ScatteredRingConstructor Start: esp: 0x{esp:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            //0x24 is stack offset from pushing registers (to preserve them)
            if (!GameStateHandler.InGame(TaskName))
            {
                LoggingHandler.LogMessage($"ScatteredRingConstructor End (not InGame)", TaskName, LogLevel.SuperDebug);
                return 1;
            }
                
            //Mod.Logger.WriteLine($"OnScatteredRing Hook ESP: 0x{esp:x}", Color.LightGreen);
            
            var ringSpawnCount = *(int*)(esp + 0x3C + 0x24);
            LoggingHandler.LogMessage($"RingSpawnCount: {ringSpawnCount}, ScatteredRingsCap: {Mod.Configuration.ScatteredRingsCap}", TaskName, LogLevel.Debug);
            if (ringSpawnCount > Mod.Configuration.ScatteredRingsCap)
            {
                Memory.Instance.SafeWrite((UIntPtr)(esp + 0x3C + 0x24), BitConverter.GetBytes(Mod.Configuration.ScatteredRingsCap));
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"ScatteredRingConstructor End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ChangeModeToFlying(int flyingCharObjPtr);
    private static int OnChangeModeToFlying(int eax)
    {
        LoggingHandler.LogMessage($"ChangeModeToFlying Start: eax: 0x{eax:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            if (!GameStateHandler.InGame(TaskName))
            {
                LoggingHandler.LogMessage($"ChangeModeToFlying End", TaskName, LogLevel.SuperDebug);
                return 1;
            }

            var team = GameStateHandler.GetCurrentStory(TaskName);
            var level = GameStateHandler.GetCurrentLevel(TaskName);

            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue((LevelId)level!, out var region))
            {
                LoggingHandler.LogMessage($"ChangeModeToFlying End", TaskName, LogLevel.SuperDebug);
                return 1;
            }
                

            var canFly = AbilityCharacterManager.CanFly((Team)team!, region, TaskName);
            
            if (!canFly)
                Memory.Instance.SafeWrite((UIntPtr)(eax + 0x994), BitConverter.GetBytes(AbilityCharacterGameWrites.LockedFlightMeterValue));
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"ChangeModeToFlying End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    
        
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int ChangeActInLevelSelect();
    private static unsafe int OnChangeActInLevelSelect()
    {
        LoggingHandler.LogMessage($"ChangeActInLevelSelect Start", TaskName, LogLevel.SuperDebug);
        try
        {
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var actIndex = *(int*)(levelSelectPtr + 0x2BC);
            Mod.LevelSelectManager.ActSelectedInLevelSelect = (Act)actIndex;
            
            LevelSpawnUnlockHandler.SelectActFromLevelSelectCallback(TaskName);
            LoggingHandler.LogMessage($"Setting Act Selected in Level Select to {Mod.LevelSelectManager.ActSelectedInLevelSelect}", TaskName, LogLevel.Debug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"ChangeActInLevelSelect End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetActInLevelSelectToZero();
    private static int OnSetActInLevelSelectToZero()
    {
        LoggingHandler.LogMessage($"SetActInLevelSelectToZero Start", TaskName, LogLevel.SuperDebug);
        Mod.LevelSelectManager.ActSelectedInLevelSelect = Act.Act1;
        LevelSpawnUnlockHandler.SelectActFromLevelSelectCallback(TaskName);
        LoggingHandler.LogMessage($"Setting Act Selected in Level Select to {Mod.LevelSelectManager.ActSelectedInLevelSelect}", TaskName, LogLevel.Debug);
        LoggingHandler.LogMessage($"SetActInLevelSelectToZero End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    [Function(new[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx, FunctionAttribute.Register.ebx, FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteLevel(int isMission2, int levelIndex, int rank, int story);
    public static int OnCompleteLevel(int ecx, int edx, int ebx, int esi)
    {
        LoggingHandler.LogMessage($"CompleteLevel Start", TaskName, LogLevel.SuperDebug);
        try
        {
            var isMission2 = ecx; 
            var levelIndex = edx;
            var team = (Team)esi;
            var rank = (Rank)ebx;
            var apHandler = Mod.ArchipelagoHandler;
            var slotData = apHandler.SlotData;

            //Chaotix Rail Canyon
            if (levelIndex == (int)LevelId.ChaotixRailCanyon)
                levelIndex = (int)LevelId.RailCanyon;

            if (!Enum.IsDefined(typeof(LevelId), levelIndex))
            {
                LoggingHandler.LogMessage($"OnCompleteLevel End LevelIndex: {levelIndex} is not Defined in Enum", TaskName, LogLevel.Error);
                return 1;
            }
            
            LoggingHandler.LogMessage($"OnCompleteLevel Here. IsAct2: {isMission2},  LevelIndex: {(LevelId)levelIndex}, Rank: {rank}, Team: {team}", TaskName, LogLevel.APAction);

            if (levelIndex > 24)
            {
                LoggingHandler.LogMessage($"CompleteLevel End LevelIndex: {levelIndex} is > MetalOverlord", TaskName, LogLevel.Error);
                return 0;
            }
                
            
            //SeaGate is 25
            //will never actually run this function

            if ((LevelId)levelIndex == LevelId.MetalOverlord)
            {
                if (Mod.LevelSelectManager.FinalBoss is FinalBoss.MetalMadness or FinalBoss.MetalOverlord) 
                {
                    apHandler.CheckLocation(SonicHeroesDefinitions.MetalMadnessId);
                    LoggingHandler.LogMessage($"Victory", TaskName, LogLevel.APAction);
                    apHandler.Release();
                }
                LoggingHandler.LogMessage($"CompleteLevel End (MetalOverlord)", TaskName, LogLevel.SuperDebug);
                return 1;
            }
            
            
            if (rank <= slotData.RequiredRank) 
            {
                LoggingHandler.LogMessage($"Did not reach the required rank. {rank} is not the required {slotData.RequiredRank}", TaskName, LogLevel.APAction);
                //LoggingHandler.LogMessage($"CompleteLevel End (Required Rank)", TaskName, LogLevel.SuperDebug);
                return 0;
            }
            
            var locationId = 0xA0 + (int)team * 42 + (levelIndex - 2) * 2 + isMission2;

            if (team is Team.Sonic && isMission2 == 1 &&
                (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, TaskName)!)
            {
                team = Team.SuperHardMode;
                locationId = SonicHeroesDefinitions.SuperHardModeId + (levelIndex - 2);
            }
            else
            {
                //check team enabled (not including Sonic Bosses)
                if ((team is not Team.Sonic || levelIndex <= (int)LevelId.FinalFortress) 
                    && !(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, TaskName)!)
                    return 0;
                
                //check team enabled on Sonic Bosses (as SuperHard is an option)
                if (team is Team.Sonic && levelIndex >= (int)LevelId.EggHawk &&
                    !(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, TaskName)! &&
                    !(bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, TaskName)!)
                    return 0;
                
                //check if Act is enabled (if not Boss)
                if (levelIndex < (int)LevelId.EggHawk && !Mod.LevelSelectManager.IsThisTeamActEnabled(team, (Act)isMission2, TaskName))
                    return 0;
            }
            
            if (levelIndex is >= (int)LevelId.EggHawk and < (int)LevelId.MetalMadness)
            {
                for (var gateIndex = 0; gateIndex < Mod.LevelSelectManager.GateData.Count - 1; gateIndex++)
                {
                    if (Mod.LevelSelectManager.GateData[gateIndex].BossLevel.LevelId == (LevelId)levelIndex)
                    {
                        Mod.LevelSelectManager.GateData[gateIndex + 1].SetIsUnlocked(true, TaskName);
                        //if (!Mod.LevelSelectManager.IsThisBossCompletedYet((LevelId)levelIndex))
                        Mod.LevelSelectManager.RecalculateOpenLevels(TaskName);
                    }
                    Mod.ArchipelagoHandler?.Save(TaskName);
                    locationId = 0xA0 + (levelIndex - 2) * 2;

                    foreach (var tempTeam in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x, TaskName)!))
                    {
                        if (tempTeam == Team.SuperHardMode)
                            apHandler.CheckLocation(locationId + 42 * (int)Team.Sonic);
                        else
                            apHandler.CheckLocation(locationId + 42 * (int)tempTeam);
                    }
                        
                }
                LoggingHandler.LogMessage($"CompleteLevel End (Boss Level)", TaskName, LogLevel.SuperDebug);
                return 1;
            }
            
            LoggingHandler.LogMessage($"Checking Mission Completion Location Here: Id = 0x{(SonicHeroesDefinitions.AllIdsStartOffset + locationId):X}", TaskName, LogLevel.Debug);
            //Mod.SaveDataHandler.CustomSaveData.LevelsGoaled[story][(LevelId)levelIndex] = true;
            LevelSpawnUnlockHandler.BonusStageUnlockCallback(team, (LevelId)levelIndex, TaskName, goal: true);

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
            Mod.LevelSelectManager.RecalculateOpenLevels(TaskName, team, isLevelNotCompletedYet);
            apHandler.CheckLocation(locationId);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"CompleteLevel End (Regular Level)", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoLevelSelect();
    private static int OnGoLevelSelect()
    {
        LoggingHandler.LogMessage($"GoLevelSelect Start", TaskName, LogLevel.SuperDebug);
        Mod.LevelSelectManager.RecalculateOpenLevels(TaskName);
        LoggingHandler.LogMessage($"GoLevelSelect End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetRings(int amount);
    private static int OnSetRings(int amount)
    {
        LoggingHandler.LogMessage($"SetRings Start Amount: {amount}", TaskName, LogLevel.SuperDebug);
        if (!RingLinkHandler.IsRingLinkEnabled(TaskName))
        {
            LoggingHandler.LogMessage($"SetRings End (no Ringlink)", TaskName, LogLevel.SuperDebug);
            return 0;
        }
        if (RingLinkHandler.IsRingLinkOverlord(TaskName) || GameStateHandler.GetCurrentLevel(TaskName) != LevelId.MetalOverlord)
            RingLinkHandler.SendRingPacket(amount, TaskName);
        LoggingHandler.LogMessage($"SetRings End (with RingLink)", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int Die();
    private static int OnDie()
    {
        LoggingHandler.LogMessage($"Die Start", TaskName, LogLevel.SuperDebug);
        TrapHandler.DisableStealthTrap(TaskName);
        if (DeathLinkHandler.SomeoneElseDied)
        {
            DeathLinkHandler.SomeoneElseDied = false;
            LoggingHandler.LogMessage($"Die End (No DeathLink)", TaskName, LogLevel.SuperDebug);
            return 0;
        }
        if (DeathLinkHandler.IsDeathLinkEnabled(TaskName))
            DeathLinkHandler.SendDeath(TaskName);
        LoggingHandler.LogMessage($"Die End (with DeathLink", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementCount(int newCount);
    private static int OnIncrementCount(int ecx)
    {
        LoggingHandler.LogMessage($"IncrementCount Start ecx: {ecx}", TaskName, LogLevel.SuperDebug);
        ObjSanityHandler.HandleCountIncreased(ecx, TaskName);
        LoggingHandler.LogMessage($"IncrementCount End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ebx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementEnemyCount(int newCount);
    private static int OnMoveEnemyCount(int ebx)
    {
        LoggingHandler.LogMessage($"IncrementEnemyCount Start NewCount: {ebx}", TaskName, LogLevel.SuperDebug);
        ObjSanityHandler.CheckEnemyCount(ebx, TaskName);
        LoggingHandler.LogMessage($"IncrementEnemyCount End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementBSCapsuleCount(int ptr);
    private static unsafe int OnIncrementBSCapsuleCount(int eax)
    {
        LoggingHandler.LogMessage($"IncrementBSCapsuleCount Start eax: 0x{eax:X}", TaskName, LogLevel.SuperDebug);
        try
        {
            var newCount = *(int*)(eax + 0x23C);
            ObjSanityHandler.HandleBSCapsuleCountIncreased(newCount, TaskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"IncrementBSCapsuleCount End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int IncrementGoldBeetleCount(int newCount);
    private static int OnIncrementGoldBeetleCount(int edx)
    {
        LoggingHandler.LogMessage($"IncrementGoldBeetleCount Start edx: {edx}", TaskName, LogLevel.SuperDebug);
        ObjSanityHandler.HandleGoldBeetleCountIncreased(edx, TaskName);
        LoggingHandler.LogMessage($"IncrementGoldBeetleCount End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AssignRings();
    private static unsafe int OnCheckRings()
    {
        LoggingHandler.LogMessage($"AssignRings Start", TaskName, LogLevel.SuperDebug);
        try
        {
            var newCount = *(int*)(Mod.ModuleBase + 0x5DD70C);
            ObjSanityHandler.CheckRingSanity(newCount, TaskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", TaskName, LogLevel.Error);
        }
        LoggingHandler.LogMessage($"AssignRings End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int CompleteEmeraldStage(int emeraldAddressOffset);
    private static int OnCompleteEmeraldStage(int eax)
    {
        LoggingHandler.LogMessage($"CompleteEmeraldStage Start: eax: {eax}", TaskName, LogLevel.SuperDebug);
        var locationId = SonicHeroesDefinitions.EmeraldStartId + (eax - 21) / 3;
        Mod.ArchipelagoHandler.CheckLocation(locationId);
        LoggingHandler.LogMessage($"CompleteEmeraldStage End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int StartCompleteStage();
    private static int OnStartCompleteStage()
    {
        LoggingHandler.LogMessage($"StartCompleteStage Start", TaskName, LogLevel.SuperDebug);
        ObjSanityHandler.CheckEnemyCount(100, TaskName);
        ObjSanityHandler.CheckRingSanity(500, TaskName);
        GameStateGameWrites.SetBonusKey(false, TaskName);
        LoggingHandler.LogMessage($"StartCompleteStage End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetStateInGame();
    private static int OnSetStateInGame()
    {
        LoggingHandler.LogMessage($"SetStateInGame Start", TaskName, LogLevel.SuperDebug);
        ItemHandler.HandleCachedItems(TaskName);
        AbilityCharacterManager.PollUpdates(TaskName);

        if (GameStateHandler.GetCurrentAct(TaskName) != Act.Act3)
        {
            LoggingHandler.LogMessage($"SetStateInGame End", TaskName, LogLevel.SuperDebug);
            return 1;
        }
            
        LoggingHandler.LogMessage($"Setting Current Act from Act3 (SuperHard) to Act 2", TaskName, LogLevel.Debug);
        GameStateGameWrites.SetCurrentAct(Act.Act2, TaskName);
        //GameStateGameWrites.SetBonusKey(true);
        LoggingHandler.LogMessage($"SetStateInGame End", TaskName, LogLevel.SuperDebug);
        return 1;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBonusKey(int pointer);
    private static int OnGetBonusKey(int edx)
    {
        LoggingHandler.LogMessage($"GetBonusKey Start edx: 0x{edx:X}", TaskName, LogLevel.SuperDebug);
        KeySanityHandler.HandleKeySanity(edx, TaskName);
        LoggingHandler.LogMessage($"GetBonusKey End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx}, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetCheckPoint(int priority, int pointer);
    private static int OnGetCheckPoint(int ecx, int edx)
    {
        LoggingHandler.LogMessage($"GetCheckPoint Start ecx: {ecx} edx: 0x{edx:X}", TaskName, LogLevel.SuperDebug);
        CheckpointSanityHandler.HandleCheckPointSanity(ecx, edx, TaskName);
        LoggingHandler.LogMessage($"GetCheckPoint End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetAct();
    private static int OnSetAct()
    {
        //CURRENT LEVEL IS NOT VALID HERE
        //STAGE OBJS ARE NOT LOADED IN MEMORY YET
        LoggingHandler.LogMessage($"SetAct Start", TaskName, LogLevel.SuperDebug);
        LevelSpawnUnlockHandler.SpawnPosCallbackChangeLevel(TaskName);
        LoggingHandler.LogMessage($"SetAct End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetObjStateSpawned(int esi);
    private static int OnObjSetStateSpawned(int esi)
    {
        //StageObjHandler.OnObjSetStateSpawned(esi);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectActFromSelectLevel();
    private static int OnGoSelectActFromSelectLevel()
    {
        LoggingHandler.LogMessage($"GoSelectActFromSelectLevel Start", TaskName, LogLevel.SuperDebug);
        LevelSpawnUnlockHandler.SelectActFromLevelSelectCallback(TaskName);
        LoggingHandler.LogMessage($"GoSelectActFromSelectLevel End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoSelectLevelFromSelectAct();
    private static int OnGoSelectLevelFromSelectAct()
    {
        LoggingHandler.LogMessage($"GoSelectLevelFromSelectAct Start", TaskName, LogLevel.SuperDebug);
        LevelSpawnUnlockHandler.ShouldCheckForInput = false;
        LevelSpawnUnlockHandler.SpawnPosIndex = 0;
        LoggingHandler.LogMessage($"GoSelectLevelFromSelectAct End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoToGameFromLevelSelect();
    private static int OnGoToGameFromLevelSelect()
    {
        LoggingHandler.LogMessage($"GoToGameFromLevelSelect Start", TaskName, LogLevel.SuperDebug);
        //LoggingHandler.LogMessage($"GoToGameFromLevelSelect. Spawn Index: {Mod.LevelSpawnHandler!.SpawnPosIndex}", TaskName, LogLevel.Debug);
        LevelSpawnUnlockHandler.ShouldCheckForInput = false;
        LevelSpawnUnlockHandler.GoToGameSpawnPosCallback(TaskName);
        LoggingHandler.LogMessage($"GoToGameFromLevelSelect End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoCharUncaptureState(int esi);
    private static int OnGoCharUncaptureState(int esi)
    {
        LoggingHandler.LogMessage($"OnGoCharUncaptureState Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        AbilityCharacterManager.PollUpdates(TaskName);
        LoggingHandler.LogMessage($"OnGoCharUncaptureState End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GoPlayerChangeModeWait(int charPtr);
    private static int OnGoPlayerChangeModeWait(int esi)
    {
        LoggingHandler.LogMessage($"OnGoPlayerChangeModeWait Start esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        AbilityCharacterManager.PollUpdates(TaskName);
        LoggingHandler.LogMessage($"OnGoPlayerChangeModeWait End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.edx }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int AddLevel(int formationChar);
    private static int OnAddLevel(int edx)
    {
        //ecx team pointer
        //edx is formation char
        LoggingHandler.LogMessage($"AddLevel Start: edx (ebp) is 0x{edx:x}", TaskName, LogLevel.SuperDebug);

        var team = GameStateHandler.GetCurrentStory(TaskName);
        var level = GameStateHandler.GetCurrentLevel(TaskName);
        //var act = GameStateHandler.GetCurrentAct(TaskName);

        if (team == null || level == null)
        {
            LoggingHandler.LogMessage($"OnAddLevel Team: {team} or Level: {level} is null", TaskName, LogLevel.Error);
            return 0;
        }
    
        //handle region
        if (!SonicHeroesDefinitions.LevelIdToRegion.ContainsKey((LevelId)level))
        {
            LoggingHandler.LogMessage($"Add Level Function run in level {level} that is not in LevelIdToRegion", TaskName, LogLevel.SuperDebug);
            return 0;
        }
        var region = SonicHeroesDefinitions.LevelIdToRegion[(LevelId)level];
        

        if (!Enum.IsDefined(typeof(FormationChar), edx))
        {
            LoggingHandler.LogMessage($"Formation Character: {edx} does not exist", TaskName, LogLevel.Error);
            return 0;
        }
        
        AbilityCharacterManager.HandleLevelUp((Team)team, region, (FormationChar)edx, TaskName);
        LoggingHandler.LogMessage($"AddLevel End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int InitSetGenerator();
    private static int OnInitSetGenerator()
    {
        LoggingHandler.LogMessage($"OnInitSetGenerator Start", TaskName, LogLevel.SuperDebug);
        TrapHandler.ExpectedStealthForLevel = TrapHandler.GetStealth(TaskName);
        LoggingHandler.LogMessage($"Setting ExpectedStealth to 0x{TrapHandler.ExpectedStealthForLevel:X}", TaskName, LogLevel.SuperDebug);
        StageObjHandler.HandleInitSetGenerator(TaskName);
        AbilityCharacterManager.PollUpdates(TaskName);
        LoggingHandler.LogMessage($"OnInitSetGenerator Finished", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int SetTeamInitialPosition();
    private static int OnSetTeamInitialPosition()
    {
        return 0;
    }
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int GetBingoChip(int esi);
    private static int OnGetBingoChip(int esi)
    {
        LoggingHandler.LogMessage($"GetBingoChip: esi: 0x{esi:X}", TaskName, LogLevel.SuperDebug);
        BingoChipSanityHandler.HandleBingoChip(esi, TaskName);
        LoggingHandler.LogMessage($"GetBingoChip Finished", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.edx, FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int BGMSetFileName(int ecx, int edx, int esi);
    private static int OnBGMSetFileName(int ecx, int edx, int esi)
    {
        return 0;
        //LoggingHandler.LogMessage($"OnBGMSetFileName: ECX (EAX): 0x{ecx:x} EDX: 0x{edx:x} ESI: 0x{esi:x}", TaskName, LogLevel.Debug);

    }
    
    
    [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.esi }, 
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int BGMGetDVDRootPath(int esi);

    private static int OnBGMGetDVDRootPath(int esi)
    {
        LoggingHandler.LogMessage($"OnBGMGetDVDRootPath Start esi: 0x{esi:x}", TaskName, LogLevel.SuperDebug);
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null on BGMGetDVDRootPath Hook", TaskName, LogLevel.Error);
            return 0;
        }
        if (!Mod.Configuration.MusicShuffle)
            return 0;
        //LoggingHandler.LogMessage($"OnBGMGetDVDRootPath(esi): Check Passed", TaskName, LogLevel.Debug);
        MusicShuffleHandler.HandleBGMFilePathHook(esi, TaskName);
        LoggingHandler.LogMessage($"OnBGMGetDVDRootPath Finished", TaskName, LogLevel.SuperDebug);
        return 0;
    }
    
    
    
    [Function(new FunctionAttribute.Register[] { },
        FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
    public delegate int TObjResultConstructStart();
    private static int OnTObjResultConstructStart()
    {
        LoggingHandler.LogMessage($"OnTObjResultConstructStart Start", TaskName, LogLevel.SuperDebug);
        LevelId? level = GameStateHandler.GetCurrentLevel(TaskName);

        if (level == null)
        {
            LoggingHandler.LogMessage($"Null Level in OnTObjResultConstructStart", TaskName, LogLevel.Error);
            return 0;
        }
        
        LoggingHandler.LogMessage($"OnTObjResultConstructStart with Level: {level}", TaskName, LogLevel.SuperDebug);
        if (level == LevelId.SeaGate)
        {
            if (Mod.LevelSelectManager.FinalBoss is FinalBoss.SeaGate)
            {
                Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.MetalMadnessId);
                Mod.ArchipelagoHandler.Release();
            }
        }
        LoggingHandler.LogMessage($"OnTObjResultConstructStart End", TaskName, LogLevel.SuperDebug);
        return 0;
    }
}