using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj;

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
        ObjSpawnData temp = *SpawnDataPtr;
        string _result = $"StageObjSpawnData: SpawnData: Ptr: 0x{(UIntPtr)SpawnDataPtr:X} {{";

        foreach (var field in typeof(ObjSpawnData).GetFields(BindingFlags.Instance | 
                                                             BindingFlags.Public |
                                                             BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(temp)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}} Type: {(StageObjTypes)temp.ObjId} _backupData: {{";
        
        foreach (var field in typeof(ObjSpawnData).GetFields(BindingFlags.Instance | 
                                                             BindingFlags.Public |
                                                             BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(_backupData)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}}";
        
        return _result;
    }
}

public unsafe class SingleSpringSpawnData: StageObjSpawnData
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }

    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Single Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        _extraVarsPtr->Power = _backupExtraVars.Power;
    }
    
    public float Power
    { 
        get => _extraVarsPtr->Power;
        set => _extraVarsPtr->Power = value; 
    }
    
    public ushort NoControlTime
    {
        get => _extraVarsPtr->NoControlTime;
        set => _extraVarsPtr->NoControlTime = value;
    }

    public override string ToString()
    {
        SingleSpringExtraVars temp = *_extraVarsPtr;
        string _result = $"{base.ToString().Replace("StageObjSpawnData", "SingleSpringSpawnData")} ExtraVars: Ptr: 0x{(UIntPtr)_extraVarsPtr:X} {{";

        foreach (var field in typeof(SingleSpringExtraVars).GetFields(BindingFlags.Instance | 
                                                                      BindingFlags.Public |
                                                                      BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(temp)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}} _backupExtraVars: {{";
        
        foreach (var field in typeof(SingleSpringExtraVars).GetFields(BindingFlags.Instance | 
                                                                      BindingFlags.Public |
                                                                      BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(_backupExtraVars)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}}";
        return _result;
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
        public ItemReward Reward;
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }

    public void ResetPower(string taskName)
    {
        LoggingHandler.LogMessage($"Resetting Triple Spring Power: 0x{(UIntPtr)SpawnDataPtr:X} :: Old: {_extraVarsPtr->Power} :: New: {_backupExtraVars.Power}", taskName, LogLevel.Debug);
        _extraVarsPtr->Power = _backupExtraVars.Power;
    }
    
    public float Power
    { 
        get => _extraVarsPtr->Power;
        set => _extraVarsPtr->Power = value; 
    }

    public float Scale
    {
        get => _extraVarsPtr->Scale;
        set => _extraVarsPtr->Scale = value;
    }
    
    public ushort NoControlTime
    {
        get => _extraVarsPtr->NoControlTime;
        set => _extraVarsPtr->NoControlTime = value;
    }

    public override string ToString()
    {
        TripleSpringExtraVars temp = *_extraVarsPtr;
        string _result = $"{base.ToString().Replace("StageObjSpawnData", "TripleSpringSpawnData")} ExtraVars: Ptr: 0x{(UIntPtr)_extraVarsPtr:X} {{";

        foreach (var field in typeof(TripleSpringExtraVars).GetFields(BindingFlags.Instance | 
                                                                      BindingFlags.Public |
                                                                      BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(temp)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}} _backupExtraVars: {{";
        
        foreach (var field in typeof(TripleSpringExtraVars).GetFields(BindingFlags.Instance | 
                                                                      BindingFlags.Public |
                                                                      BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(_backupExtraVars)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}}";
        return _result;
    }
}

public unsafe class RingsSpawnData: StageObjSpawnData
{
    public enum RingType : byte
    {
        Normal = 0,
        Line,
        Circle,
        Arch,
        WarpToPlayerIfAtSpawn,
        Scattered = WarpToPlayerIfAtSpawn,
    }
    
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }

    public override string ToString()
    {
        RingsExtraVars temp = *_extraVarsPtr;
        string _result = $"{base.ToString().Replace("StageObjSpawnData", "RingsSpawnData")} ExtraVars: Ptr: 0x{(UIntPtr)_extraVarsPtr:X} {{";

        foreach (var field in typeof(RingsExtraVars).GetFields(BindingFlags.Instance | 
                                                               BindingFlags.Public |
                                                               BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(temp)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}} _backupExtraVars: {{";
        
        foreach (var field in typeof(RingsExtraVars).GetFields(BindingFlags.Instance | 
                                                               BindingFlags.Public |
                                                               BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(_backupExtraVars)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}}";
        return _result;
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }

    public override string ToString()
    {
        HintRingExtraVars temp = *_extraVarsPtr;
        string _result = $"{base.ToString().Replace("StageObjSpawnData", $"{this.GetType().Name}")} ExtraVars: Ptr: 0x{(UIntPtr)_extraVarsPtr:X} {{";

        foreach (var field in typeof(HintRingExtraVars).GetFields(BindingFlags.Instance | 
                                                                  BindingFlags.Public |
                                                                  BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(temp)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}} _backupExtraVars: {{";
        
        foreach (var field in typeof(HintRingExtraVars).GetFields(BindingFlags.Instance | 
                                                                  BindingFlags.Public |
                                                                  BindingFlags.NonPublic))
        {
            _result += $"{field.Name}: {field.GetValue(_backupExtraVars)}, ";
        }
        _result.TrimEnd();
        _result.TrimEnd(',');
        _result += $"}}";
        return _result;
    }
}

public unsafe class RegularSwitchSpawnData: StageObjSpawnData
{
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }
}

public unsafe class PushPullSwitchSpawnData: StageObjSpawnData
{
    public enum PushPullSwitchType : byte
    {
        Push,
        Pull,
    }
    
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }
}

public unsafe class RainbowHoopsSpawnData: StageObjSpawnData
{
    public enum RainbowHoopsType
    {
        Speed,  //horizontal forward
        FlyA,   //vertical upward
        FlyB,   //vertical forward
        PowerS, //3 spread even height
        PowerL, //3 spread uneven height
    }
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
    
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }
}

public unsafe class CheckpointSpawnData: StageObjSpawnData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct CheckpointExtraVars
    {
        private byte Priority;
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
    public new void ResetData(string taskName)
    {
        base.ResetData(taskName);
        *_extraVarsPtr = _backupExtraVars;
    }
}


