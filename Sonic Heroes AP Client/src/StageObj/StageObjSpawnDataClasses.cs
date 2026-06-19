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
    private readonly ObjSpawnData _backupData;

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

    public void SpawnOrDespawnObj(bool spawn, string taskName)
    {
        byte renderDistance = spawn ? _backupData.RenderDistance : StageObjData.DespawnObjRenderDistance;
        SpawnDataPtr->RenderDistance = renderDistance;
    }

    public void SetSpawnPosition(Vector3 position, string taskName)
    {
        SpawnDataPtr->XSpawnPos = position.X;
        SpawnDataPtr->YSpawnPos = position.Y;
        SpawnDataPtr->ZSpawnPos = position.Z;
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
    private readonly SingleSpringExtraVars _backupExtraVars;

    public SingleSpringSpawnData(UIntPtr objPtr) : base(objPtr)
    {
        _extraVarsPtr = (SingleSpringExtraVars*)SpawnDataPtr->PtrVars;
        _backupExtraVars = *_extraVarsPtr;
    }

    public float Power
    { 
        get => _extraVarsPtr->Power;
        set => _extraVarsPtr->Power = value;
    }
        
    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Single Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        _extraVarsPtr->Power = _backupExtraVars.Power;
    }
        
    public ushort NoControlTime
    {
        get => _extraVarsPtr->NoControlTime;
        set => _extraVarsPtr->NoControlTime = value;
    }
        
    public void ResetNoControlTime(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Single Spring NoControlTime: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->NoControlTime} :: New: {_backupExtraVars.NoControlTime}", taskName, LogLevel.Debug);
        _extraVarsPtr->NoControlTime = _backupExtraVars.NoControlTime;
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
    
    
    public float Power
    { 
        get => _extraVarsPtr->Power;
        set => _extraVarsPtr->Power = value; 
    }
    
    
    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        _extraVarsPtr->Power = _backupExtraVars.Power;
    }
    
    
    public float Scale
    {
        get => _extraVarsPtr->Scale;
        set => _extraVarsPtr->Scale = value;
    }
    
    public void ResetScale(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Scale: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Scale} :: New: {_backupExtraVars.Scale}", taskName, LogLevel.Debug);
        _extraVarsPtr->Scale = _backupExtraVars.Scale;
    }
    
    public ushort NoControlTime
    {
        get => _extraVarsPtr->NoControlTime;
        set => _extraVarsPtr->NoControlTime = value;
    }
    
    public void ResetNoControlTime(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring No Control Time: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->NoControlTime} :: New: {_backupExtraVars.NoControlTime}", taskName, LogLevel.Debug);
        _extraVarsPtr->NoControlTime = _backupExtraVars.NoControlTime;
    }
    
    public ItemReward ItemReward
    {
        get => _extraVarsPtr->ItemReward;
        set => _extraVarsPtr->ItemReward = value;
    }
    
    public void ResetItemReward(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Item Reward: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->ItemReward} :: New: {_backupExtraVars.ItemReward}", taskName, LogLevel.Debug);
        _extraVarsPtr->ItemReward = _backupExtraVars.ItemReward;
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
    
    public RingType RingType
    { 
        get => _extraVarsPtr->RingType;
        set => _extraVarsPtr->RingType = value; 
    }
    
    public void ResetRingType(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Rings Ring Type: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->RingType} :: New: {_backupExtraVars.RingType}", taskName, LogLevel.Debug);
        _extraVarsPtr->RingType = _backupExtraVars.RingType;
    }
    
    public ushort NumberOfRings
    { 
        get => _extraVarsPtr->NumberOfRings;
        set => _extraVarsPtr->NumberOfRings = value; 
    }
    
    public void ResetNumberOfRings(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Rings Number Of Rings: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->NumberOfRings} :: New: {_backupExtraVars.NumberOfRings}", taskName, LogLevel.Debug);
        _extraVarsPtr->NumberOfRings = _backupExtraVars.NumberOfRings;
    }
    
    public float Length
    { 
        get => _extraVarsPtr->Length;
        set => _extraVarsPtr->Length = value; 
    }
    
    public void ResetLength(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Rings Length: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Length} :: New: {_backupExtraVars.Length}", taskName, LogLevel.Debug);
        _extraVarsPtr->Length = _backupExtraVars.Length;
    }
    
    public float Radius
    { 
        get => _extraVarsPtr->Radius;
        set => _extraVarsPtr->Radius = value; 
    }
    
    public void ResetRadius(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Rings Radius: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Radius} :: New: {_backupExtraVars.Radius}", taskName, LogLevel.Debug);
        _extraVarsPtr->Radius = _backupExtraVars.Radius;
    }
    
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
    
    public ushort VoiceLineID
    { 
        get => _extraVarsPtr->VoiceLineID;
        set => _extraVarsPtr->VoiceLineID = value; 
    }
    
    public void ResetVoiceLineID(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Hint Ring VoiceLine ID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->VoiceLineID} :: New: {_backupExtraVars.VoiceLineID}", taskName, LogLevel.Debug);
        _extraVarsPtr->VoiceLineID = _backupExtraVars.VoiceLineID;
    }
    
    public bool DeleteByLinkOff
    { 
        get => _extraVarsPtr->DeleteByLinkOff;
        set => _extraVarsPtr->DeleteByLinkOff = value; 
    }
    
    public void ResetDeleteByLinkOff(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Hint Ring DeleteByLinkOff ID: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->DeleteByLinkOff} :: New: {_backupExtraVars.DeleteByLinkOff}", taskName, LogLevel.Debug);
        _extraVarsPtr->DeleteByLinkOff = _backupExtraVars.DeleteByLinkOff;
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




























