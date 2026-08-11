using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.StageObj;

public static class StageObjSpawnDataFactory
{
    public static unsafe StageObjSpawnData? CreateSpawnData(UIntPtr spawnDataPtr)
    {
        //Marshal.OffsetOf<ObjSpawnData>("RenderDistance");
        StageObjTypes type = (StageObjTypes)(*(ushort*)(spawnDataPtr + 0x28));

        switch (type)
        {
            case StageObjTypes.None:
                return null;
            
            case StageObjTypes.SingleSpring:
                return new SingleSpringSpawnData(spawnDataPtr);
            case StageObjTypes.TripleSpring:
                return new TripleSpringSpawnData(spawnDataPtr);
            case StageObjTypes.Rings:
                return new RingsSpawnData(spawnDataPtr);
            case StageObjTypes.HintRing:
                return new HintRingSpawnData(spawnDataPtr);
            case StageObjTypes.RegularSwitch:
                return new RegularSwitchSpawnData(spawnDataPtr);
            case StageObjTypes.PushAndPullSwitch:
                return new PushPullSwitchSpawnData(spawnDataPtr);
            case StageObjTypes.TargetSwitch:
                return new TargetSwitchSpawnData(spawnDataPtr);
            case StageObjTypes.DashPanel:
                return new DashPanelSpawnData(spawnDataPtr);
            case StageObjTypes.DashRing:
                return new DashRingSpawnData(spawnDataPtr);
            case StageObjTypes.RainbowHoops:
                return new RainbowHoopsSpawnData(spawnDataPtr);
            case StageObjTypes.Checkpoint:
                return new CheckpointSpawnData(spawnDataPtr);
            
            
            // case StageObjTypes.Alligator:
            //     return new StageObjSpawnData(spawnDataPtr);
            default:
                return new StageObjSpawnData(spawnDataPtr);
        }
    }
}