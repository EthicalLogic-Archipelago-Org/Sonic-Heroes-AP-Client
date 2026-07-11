using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj;


public enum RingType : byte
{
    Normal = 0,
    Line,
    Circle,
    Arch,
    WarpToPlayerIfAtSpawn,
    Scattered = WarpToPlayerIfAtSpawn,
}


public enum RegularSwitchType : byte
{
    Alternate,
    Touch,
    Once,
    Interlock,
}


public enum RegularSwitchSound : byte
{
    Pi,
    Pipori,
}


public enum PushPullSwitchType : byte
{
    Push,
    Pull,
}


public enum RainbowHoopsType
{
    Speed,  //horizontal forward
    FlyA,   //vertical upward
    FlyB,   //vertical forward
    PowerS, //3 spread even height
    PowerL, //3 spread uneven height
}


public enum ItemReward: byte
{
    NoneItemBox = 0,
    Rings5,
    Rings10,
    Rings20,
    Shield,
    ExtraLife,
    SpeedShoes,
    TeamBlastRefill,
    Invincibility,
    LevelUpSpeed,
    LevelUpFly,
    LevelUpPower,
    RefillFlightGauge,
    None3Spring = 255,
}


public static class StageObjSpawnDataClasses
{
    public static string GetToStringForStageObjStruct<T>(ref T input, bool shouldIncludeStructType = false) where T : struct
    {
        //T tempStruct = *input;
        string _result = shouldIncludeStructType ? $"{typeof(T).Name}: {{" : $"{{";

        foreach (var field in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(input)}, ";
        }
        _result = _result.TrimEnd();
        _result = _result.TrimEnd(',');
        _result += $"}}";
        return _result;
    }
}


public unsafe class StageObjSpawnData
{
    protected readonly ObjSpawnData* SpawnDataPtr; //<- 0x40 bytes
    public readonly StageObjTypes Type;
    private ObjSpawnData _backupData;

    public StageObjSpawnData(UIntPtr objPtr)
    {
        SpawnDataPtr = (ObjSpawnData*)objPtr;
        Type = (StageObjTypes)SpawnDataPtr->ObjId;
        _backupData = *SpawnDataPtr;
    }
    public void ResetData(string taskName)
    {
        *SpawnDataPtr = _backupData;
    }

    public byte GetBackupRenderDistance(string taskName)
    {
        return _backupData.RenderDistance;
    }

    public UIntPtr GetPtrToSpawnData(string taskName)
    {
        return (UIntPtr)SpawnDataPtr;
    }

    public Vector3 GetSpawnPosition(string taskName)
    {
        return new Vector3(SpawnDataPtr->XSpawnPos, SpawnDataPtr->YSpawnPos, SpawnDataPtr->ZSpawnPos);
    }
    
    public Vector3 GetOriginalSpawnPosition(string taskName)
    {
        return new Vector3(_backupData.XSpawnPos, _backupData.YSpawnPos, _backupData.ZSpawnPos);
    }

    public bool IsAtPosition(Vector3 pos, string taskName)
    {
        return Vector3.Distance(pos, GetOriginalSpawnPosition(taskName)) < StageObjData.DistanceForMatchingStageObj;
    }

    public void SpawnOrDespawnObj(bool spawn, string taskName)
    {
        byte renderDistance = spawn ? _backupData.RenderDistance : StageObjData.DespawnObjRenderDistance;
        SpawnDataPtr->RenderDistance = renderDistance;
    }

    public void SetSpawnPosition(Vector3 position, string taskName, bool shouldChangeBackup = false)
    {
        SpawnDataPtr->XSpawnPos = position.X;
        SpawnDataPtr->YSpawnPos = position.Y;
        SpawnDataPtr->ZSpawnPos = position.Z;
        
        LoggingHandler.LogMessage($"Moving Spawn Pos of obj: {Type}. Old Pos: ({_backupData.XSpawnPos}, {_backupData.YSpawnPos}, {_backupData.ZSpawnPos}) New Pos: ({position.X}, {position.Y}, {position.Z}) ChangeBackup: {shouldChangeBackup}", taskName, LogLevel.Debug);

        if (shouldChangeBackup)
        {
            _backupData.XSpawnPos = position.X;
            _backupData.YSpawnPos = position.Y;
            _backupData.ZSpawnPos = position.Z;
        }
    }

    public void ChangeValueAtDynamicMem<T>(T value, int offset, string taskName)
    {
        if (!SonicHeroesDefinitions.IsValidPtr(SpawnDataPtr->PtrDynamicMem)) 
            return;
        if (value is not (int or float or ushort or byte))
        {
            LoggingHandler.LogMessage($"Invalid type for value in ChangeFloatAtDynamicMem: {value.GetType()}", taskName, LogLevel.Error);
            return;
        }
        
        try
        {
            var dynamicMemValuePtr = SpawnDataPtr->PtrDynamicMem + (UIntPtr)offset;
            *(T*)dynamicMemValuePtr = value;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public override string ToString()
    {
        try
        {
            ObjSpawnData temp = *SpawnDataPtr;
            string _result = $"{this.GetType().Name}: Ptr: 0x{(UIntPtr)SpawnDataPtr:X} {StageObjSpawnDataClasses.GetToStringForStageObjStruct(input: ref temp, shouldIncludeStructType: true)}";
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "ERROR IN STAGEOBJSPAWNDATA TOSTRING";
    }
}


public unsafe class StageObjSpawnDataWithExtraVars : StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PlaceholderStruct
    {
        private fixed byte _padding[0x20];
        public int SomeID;
    }
        
    private readonly PlaceholderStruct* _extraVarsPtr;
    private readonly PlaceholderStruct _backupExtraVars;
        
    public StageObjSpawnDataWithExtraVars(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (PlaceholderStruct*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
        
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }
    
    public int SomeID
    {
        get => _extraVarsPtr->SomeID;
        set => _extraVarsPtr->SomeID = value;
    }
        
    public void ResetSomeID(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting {this.GetType().Name} SomeID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->SomeID} :: New: {_backupExtraVars.SomeID}", taskName, LogLevel.Debug);
        _extraVarsPtr->SomeID = _backupExtraVars.SomeID;
    }

    public void PrintResetLogMsg(string taskName)
    {
        
    }

    public override string ToString()
    {
        string _result = $"{base.ToString()} ExtraVars: Ptr: 0x{(UIntPtr)this._extraVarsPtr:X} {StageObjSpawnDataClasses.GetToStringForStageObjStruct(input: ref *this._extraVarsPtr, shouldIncludeStructType: true)}";
        return _result;
    }
}


public unsafe class SingleSpringSpawnData: StageObjSpawnDataWithExtraVars
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SingleSpringExtraVars
    {
        public float Power;
        public ushort NoControlTime;
        private fixed byte _padding[0x1A];
        public int SomeID;
    }

    private readonly SingleSpringExtraVars* _extraVarsPtr;
    private SingleSpringExtraVars _backupExtraVars;

    public SingleSpringSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (SingleSpringExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }

    public float Power => _extraVarsPtr->Power;

    public void SetPower(float value, string taskName)
    {
        _extraVarsPtr->Power = value;
        ChangeValueAtDynamicMem(value, 0xC8, taskName);
    }

    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Single Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        SetPower(_backupExtraVars.Power, taskName);
    }
        
    public ushort NoControlTime => _extraVarsPtr->NoControlTime;

    public void SetNoControlTime(ushort value, string taskName)
    {
        _extraVarsPtr->NoControlTime = value;
        ChangeValueAtDynamicMem(value, 0x9C, taskName);
    }
        
    public void ResetNoControlTime(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Single Spring NoControlTime: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->NoControlTime} :: New: {_backupExtraVars.NoControlTime}", taskName, LogLevel.Debug);
        SetNoControlTime(_backupExtraVars.NoControlTime, taskName);
    }
}

public unsafe class TripleSpringSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct TripleSpringExtraVars
    {
        public float Power;
        public float Scale;
        public ushort NoControlTime;
        public ItemReward ItemReward;
        private fixed byte _padding[0x15];
        public int SomeID;
    }

    private readonly TripleSpringExtraVars* _extraVarsPtr;
    private readonly TripleSpringExtraVars _backupExtraVars;

    public TripleSpringSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (TripleSpringExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
    
    
    public float Power => _extraVarsPtr->Power;

    public void SetPower(float value, string taskName)
    {
        _extraVarsPtr->Power = value;
        ChangeValueAtDynamicMem(value, 0xC8, taskName);
    }

    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        SetPower(_backupExtraVars.Power, taskName);
    }
    
    public float Scale => _extraVarsPtr->Scale;

    public void SetScale(float value, string taskName)
    {
        _extraVarsPtr->Power = value;
        //have to add 1 as scale defaults to 0 in static but defaults to 1 in dynamic
        ChangeValueAtDynamicMem(value + 1, 0xCC, taskName);
    }
    
    public void ResetScale(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Scale: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Scale} :: New: {_backupExtraVars.Scale}", taskName, LogLevel.Debug);
        SetScale(_backupExtraVars.Scale, taskName);
    }
    
    public float NoControlTime => _extraVarsPtr->NoControlTime;

    public void SetNoControlTime(ushort value, string taskName)
    {
        _extraVarsPtr->NoControlTime = value;
        ChangeValueAtDynamicMem(value, 0x9E, taskName);
    }
    
    public void ResetNoControlTime(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring No Control Time: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->NoControlTime} :: New: {_backupExtraVars.NoControlTime}", taskName, LogLevel.Debug);
        SetNoControlTime(_backupExtraVars.NoControlTime, taskName);
    }
    
    public ItemReward ItemReward => _extraVarsPtr->ItemReward;

    public void SetItemReward(ItemReward value, string taskName)
    {
        _extraVarsPtr->ItemReward = value;
        ChangeValueAtDynamicMem((byte)value, 0x9D, taskName);
    }
    
    public void ResetItemReward(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Item Reward: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->ItemReward} :: New: {_backupExtraVars.ItemReward}", taskName, LogLevel.Debug);
        SetItemReward(_backupExtraVars.ItemReward, taskName);
    }
}

public unsafe class RingsSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RingsExtraVars
    {
        public RingType RingType;
        private byte _paddingByte;
        public ushort NumberOfRings;
        public float Length;
        public float Radius;
        private int UnknownInt;
        private fixed byte _padding[0x10];
        public int SomeID;
    }

    private readonly RingsExtraVars* _extraVarsPtr;
    private readonly RingsExtraVars _backupExtraVars;

    public RingsSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (RingsExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
    
    //dynamic - 0x50
    
    //NOT POSSIBLE TO EDIT SPAWNED IN RINGS WITHOUT DESPAWNING AND RESPAWNING
    public RingType RingType => _extraVarsPtr->RingType;
    public ushort NumberOfRings => _extraVarsPtr->NumberOfRings;
    public float Length => _extraVarsPtr->Length;
    public float Radius => _extraVarsPtr->Radius;
    
}

public unsafe class HintRingSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct HintRingExtraVars
    {
        public ushort VoiceLineID;
        public bool DeleteByLinkOff;
        private fixed byte _padding[0x14];
        public int SomeID;
    }

    private readonly HintRingExtraVars* _extraVarsPtr;
    private readonly HintRingExtraVars _backupExtraVars;

    public HintRingSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (HintRingExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
    
    
    public float VoiceLineID => _extraVarsPtr->VoiceLineID;

    public void SetVoiceLineID(ushort value, string taskName)
    {
        _extraVarsPtr->VoiceLineID = value;
        ChangeValueAtDynamicMem(value, 0x18, taskName);
    }
    
    public void ResetVoiceLineID(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Hint Ring VoiceLine ID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->VoiceLineID} :: New: {_backupExtraVars.VoiceLineID}", taskName, LogLevel.Debug);
        SetVoiceLineID(_backupExtraVars.VoiceLineID, taskName);
    }
    
    public bool DeleteByLinkOff => _extraVarsPtr->DeleteByLinkOff;

    public void SetDeleteByLinkOff(bool value, string taskName)
    {
        _extraVarsPtr->DeleteByLinkOff = value;
        ChangeValueAtDynamicMem(value, 0x18, taskName);
    }
    
    public void ResetDeleteByLinkOff(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Hint Ring DeleteByLinkOff ID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->DeleteByLinkOff} :: New: {_backupExtraVars.DeleteByLinkOff}", taskName, LogLevel.Debug);
        SetDeleteByLinkOff(_backupExtraVars.DeleteByLinkOff, taskName);
    }
}


//Not Done Yet
public unsafe class RegularSwitchSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RegularSwitchExtraVars
    {
        public RegularSwitchType SwitchType;
        public byte IsHidden;
        public byte LinkIDForHidden;
        public RegularSwitchSound Sound;
        private fixed byte _padding[0x1C];
        public int SomeID;
    }

    private readonly RegularSwitchExtraVars* _extraVarsPtr;
    private readonly RegularSwitchExtraVars _backupExtraVars;

    public RegularSwitchSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (RegularSwitchExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class PushPullSwitchSpawnData: StageObjSpawnData
{
    
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PushPullSwitchExtraVars
    {
        public PushPullSwitchType PushPullSwitchType;
        private fixed byte _padding[0x1F];
        public int SomeID;
    }

    private readonly PushPullSwitchExtraVars* _extraVarsPtr;
    private readonly PushPullSwitchExtraVars _backupExtraVars;

    public PushPullSwitchSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (PushPullSwitchExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class TargetSwitchSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct TargetSwitchExtraVars
    {
        public ItemReward Reward;
        public byte AppearMode;
        public byte LinkID;
        private fixed byte _padding[0x1D];
        public int SomeID;
    }

    private readonly TargetSwitchExtraVars* _extraVarsPtr;
    private readonly TargetSwitchExtraVars _backupExtraVars;

    public TargetSwitchSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (TargetSwitchExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class DashPanelSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DashPanelExtraVars
    {
        public float Speed;
        public ushort NoControlTime;
        private fixed byte _padding[0x1A];
        public int SomeID;
    }
    private readonly DashPanelExtraVars* _extraVarsPtr;
    private readonly DashPanelExtraVars _backupExtraVars;

    public DashPanelSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (DashPanelExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class DashRingSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DashRingExtraVars
    {
        public float Speed;
        public ushort NoControlTime;
        private fixed byte _padding[0x1A];
        public int SomeID;
    }
    private readonly DashRingExtraVars* _extraVarsPtr;
    private readonly DashRingExtraVars _backupExtraVars;

    public DashRingSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (DashRingExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class RainbowHoopsSpawnData: StageObjSpawnData
{
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RainbowHoopsExtraVars
    {
        public RainbowHoopsType RainbowHoopsType;
        private byte _paddingByte;
        public ushort NoControlTime;
        public float Speed;
        public float Offset;
        private fixed byte _padding[0x14];
        public int SomeID;
    }
    private readonly RainbowHoopsExtraVars* _extraVarsPtr;
    private readonly RainbowHoopsExtraVars _backupExtraVars;

    public RainbowHoopsSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (RainbowHoopsExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}

public unsafe class CheckpointSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct CheckpointExtraVars
    {
        public byte Priority;
        private fixed byte _padding[0x1F];
        public int SomeID;
    }
    private readonly CheckpointExtraVars* _extraVarsPtr;
    private readonly CheckpointExtraVars _backupExtraVars;

    public CheckpointSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (CheckpointExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
    
    public byte Priority
    { 
        get => _extraVarsPtr->Priority;
        set => _extraVarsPtr->Priority = value; 
    }
    
    public void ResetPriority(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Checkpoint Priority ID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Priority} :: New: {_backupExtraVars.Priority}", taskName, LogLevel.Debug);
        _extraVarsPtr->Priority = _backupExtraVars.Priority;
    }
}

public unsafe class DashRampSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DashRampExtraVars
    {
        private float SpeedHorizontal;
        private float SpeedVertical;
        private ushort NoControlTime;
        private fixed byte _padding[0x16];
        public int SomeID;
    }
    private readonly DashRampExtraVars* _extraVarsPtr;
    private readonly DashRampExtraVars _backupExtraVars;

    public DashRampSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (DashRampExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }
}




























