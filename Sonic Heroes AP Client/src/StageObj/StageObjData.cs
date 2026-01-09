
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.StageObj;

public struct ObjSpawnData
{
    public float XSpawnPos;
    public float YSpawnPos;
    public float ZSpawnPos;
    public float XSpawnRot;
    public float YSpawnRot;
    public float ZSpawnRot;
    public byte State;
    public byte Team;
    public byte AnotherState;
    public byte PaddingByte;
    public int PaddingInt;
    public long PaddingLong;
    public ushort ObjId;
    //public byte ObjId; //instead of ushort
    //public byte ObjList; //instead of ushort
    public byte LinkId;
    public byte RenderDistance;
    public int PtrVars;
    public int PaddingInt2;
    public int PtrPrevObj;
    public int PtrNextObj;
    public int PtrDynamicMem;
}


public static class StageObjData
{
    public const IntPtr StartOfStageObjTable = 0xA825D8; 
    //Mod.ModuleBase + 0x6825D8
    
    public static readonly List<StageObjTypes> StageObjsToMessWith =
    [
        StageObjTypes.SingleSpring,
        StageObjTypes.TripleSpring,
        StageObjTypes.Rings,
        //StageObjTypes.HintRing,
        StageObjTypes.RegularSwitch,
        StageObjTypes.PushAndPullSwitch,
        StageObjTypes.TargetSwitch,
        StageObjTypes.DashPanel,
        StageObjTypes.DashRing,
        StageObjTypes.RainbowHoops,
        //StageObjTypes.Checkpoint,
        StageObjTypes.DashRamp,
        StageObjTypes.Cannon,
        StageObjTypes.RegularWeight,
        StageObjTypes.BreakableWeight,
        //StageObjTypes.ItemBox,
        //StageObjTypes.ItemBalloon,
        //StageObjTypes.GoalRing,
        StageObjTypes.Pulley,
        //StageObjTypes.WoodContainer,
        //StageObjTypes.IronContainer,
        //StageObjTypes.UnbreakableContainer,
        StageObjTypes.LostChao,
        StageObjTypes.Propeller,
        StageObjTypes.Pole,
        StageObjTypes.Gong,
        StageObjTypes.Fan,
        StageObjTypes.WarpFlower,
        StageObjTypes.BonusKey,
        StageObjTypes.TeleportTrigger,
        
        //StageObjTypes.CementBlockOnRails,
        //StageObjTypes.CementSlidingBlock,
        //StageObjTypes.CementBlock,
        StageObjTypes.MovingRuinPlatform,
        StageObjTypes.HermitCrab,
        StageObjTypes.SmallStonePlatform,
        StageObjTypes.CrumblingStonePillar,
        
        StageObjTypes.EnergyRoadSection,
        StageObjTypes.FallingDrawbridge,
        StageObjTypes.TiltingBridge,
        StageObjTypes.BlimpPlatform,
        StageObjTypes.EnergyRoadSpeedEffect,
        StageObjTypes.EnergyRoadUpwardSection,
        StageObjTypes.EnergyColumn,
        StageObjTypes.Elevator,
        StageObjTypes.LavaPlatform,
        StageObjTypes.LiquidLava,
        StageObjTypes.EnergyRoadUpwardEffect,
        
        StageObjTypes.SmallBumper,
        StageObjTypes.GreenFloatingBumper,
        StageObjTypes.PinballFlipper,
        StageObjTypes.SmallTriangleBumper,
        StageObjTypes.StarGlassPanel,
        StageObjTypes.StarGlassAirPanel,
        StageObjTypes.LargeTriangleBumper,
        //StageObjTypes.BreakableGlassFloor,
        StageObjTypes.FloatingDice,
        StageObjTypes.TripleSlots,
        StageObjTypes.SingleSlots,
        StageObjTypes.BingoChart,
        StageObjTypes.BingoChip,
        StageObjTypes.DashArrow,
        StageObjTypes.PotatoChip,
        
        StageObjTypes.SwitchableRail,
        StageObjTypes.RailSwitch,
        StageObjTypes.SwitchableArrow,
        StageObjTypes.RailBooster,
        StageObjTypes.RailCrossingRoadblock,
        StageObjTypes.Capsule,
        StageObjTypes.RailPlatform,
        StageObjTypes.TrainTrain,
        StageObjTypes.EngineCore,    
        StageObjTypes.BigGunInterior,    
        //StageObjTypes.Barrel,    
        //StageObjTypes.CanyonBridge,    
        //StageObjTypes.TrainTop,    
        
        StageObjTypes.GreenFrog,
        StageObjTypes.SmallGreenRainPlatform,
        StageObjTypes.SmallBouncyMushroom,
        StageObjTypes.TallVerticalVine,
        StageObjTypes.TallTreeWithPlatforms,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnIt,
        StageObjTypes.LargeYellowPlatform,
        StageObjTypes.BouncyFruit,
        StageObjTypes.BigBouncyMushroom,
        StageObjTypes.SwingingVine,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnIt2,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnIt3,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnItETC,
        StageObjTypes.BlackFrog,
        StageObjTypes.BouncyFallingFruit,
        
        StageObjTypes.TeleporterSwitch,
        StageObjTypes.CastleFloatingPlatform,
        StageObjTypes.FlameTorch,
        //StageObjTypes.PumpkinGhost,
        StageObjTypes.MansionFloatingPlatform,
        StageObjTypes.CastleKey,
        
        
        StageObjTypes.RectangularFloatingPlatform,
        StageObjTypes.SquareFloatingPlatform,
        StageObjTypes.FallingPlatform,
        StageObjTypes.SelfDestructSwitch,
        StageObjTypes.EggmanCellKey,
        
        //StageObjTypes.EggFlapper,
        //StageObjTypes.EggPawn,
        //StageObjTypes.Klagen,
        //StageObjTypes.Falco,
        //StageObjTypes.EggHammer,
        //StageObjTypes.Cameron,
        //StageObjTypes.RhinoLiner,
        //StageObjTypes.EggBishop,
        //StageObjTypes.E2000,
        
        StageObjTypes.SpecialStageOrbs,
        StageObjTypes.AppearEmerald,
        StageObjTypes.SpecialStageSpring,
        StageObjTypes.SpecialStageDashPanel,
        StageObjTypes.SpecialStageDashRing,
    ];


    public static List<StageObjTypes> StageObjsWithSpecialHandling =
    [
        StageObjTypes.SelfDestructSwitch,
    ];
    
}