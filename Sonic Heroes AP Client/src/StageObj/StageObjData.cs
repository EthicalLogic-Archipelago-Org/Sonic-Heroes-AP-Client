
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.ApplicationServices;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
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
    
    public string ToString(string taskName)
    {
        try
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            ObjSpawnData objSpawnData = this;

            Dictionary<string, object> values = new Dictionary<string, object>();
            Array.ForEach(fields, (field) => values.Add(field.Name, field.GetValue(objSpawnData)));
            Array.ForEach(properties, (property) =>
            {
                if (property.CanRead)
                    values.Add(property.Name, property.GetValue(objSpawnData, null));
            });

            return string.Join(", ", values);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return "";
    }
}



public static class StageObjData
{
    public const UIntPtr StartOfStageObjTable = 0xA825D8; 
    //Mod.ModuleBase + 0x6825D8

    public const byte DespawnObjRenderDistance = 0x00;

    public const float DistanceForMatchingStageObj = 10f;
    
    /// <summary>
    /// Dict of StageObjType to Dict of address to saved copy of ObjSpawnData
    /// </summary>
    //public static Dictionary<StageObjTypes, Dictionary<UIntPtr, ObjSpawnData>> SavedStageObjSpawnData = new();
    
    public static readonly List<StageObjTypes> StageObjsToMessWith =
    [
        StageObjTypes.SingleSpring,
        StageObjTypes.TripleSpring,
        StageObjTypes.Rings,
        StageObjTypes.HintRing,
        StageObjTypes.RegularSwitch,
        StageObjTypes.PushAndPullSwitch,
        StageObjTypes.TargetSwitch,
        StageObjTypes.DashPanel,
        StageObjTypes.DashRing,
        StageObjTypes.RainbowHoops,
        StageObjTypes.Checkpoint,
        StageObjTypes.DashRamp,
        StageObjTypes.Cannon,
        StageObjTypes.RegularWeight,
        StageObjTypes.BreakableWeight,
        //StageObjTypes.SpikeBall,
        StageObjTypes.LaserFence,
        StageObjTypes.ItemBox,
        StageObjTypes.ItemBalloon,
        StageObjTypes.GoalRing,
        StageObjTypes.Pulley,
        //StageObjTypes.WoodContainer,
        //StageObjTypes.IronContainer,
        //StageObjTypes.UnbreakableContainer,
        StageObjTypes.LostChao,
        StageObjTypes.CageBox,
        //StageObjTypes.FormationSign,
        //StageObjTypes.FormationChangeGate,
        StageObjTypes.Propeller,
        StageObjTypes.Pole,
        StageObjTypes.Gong,
        StageObjTypes.Fan,
        //StageObjTypes.Case,
        StageObjTypes.WarpFlower,
        //StageObjTypes.InvisibleCollisionObject,
        //StageObjTypes.TriggerTalking,
        //StageObjTypes.TriggerLight,
        //StageObjTypes.TriggerRhinoLiner,
        //StageObjTypes.TriggerDisableInput,
        //StageObjTypes.TriggerEggHawk,
        //StageObjTypes.TriggerFalco,
        //StageObjTypes.TriggerHurt,
        //StageObjTypes.TriggerKlagen,
        //StageObjTypes.BobsledJumpCollisionObject,
        StageObjTypes.BonusKey,
        StageObjTypes.TeleportTrigger,
        StageObjTypes.SECollisionObject,
        StageObjTypes.NoOttoOttoCollisionObject,
        
        //SeasideHill
        //StageObjTypes.CementBlockOnRails,
        //StageObjTypes.CementSlidingBlock,
        //StageObjTypes.CementBlock,
        StageObjTypes.MovingRuinPlatform,
        StageObjTypes.TriggerRuins,
        //StageObjTypes.SeasideHillSun,
        StageObjTypes.HermitCrab,
        //StageObjTypes.SeasideHillFlowerPatch,
        //StageObjTypes.SeasideHillFlag,
        //StageObjTypes.SeasideHillWhale,
        //StageObjTypes.SeasideHillSeagulls,
        //StageObjTypes.SeasideHillLargeBird,
        //StageObjTypes.SeasideHillWhaleCollisionObject,
        //StageObjTypes.SeasideHillWaterfallLarge,
        //StageObjTypes.SeasideHillTidesWave,
        StageObjTypes.SmallStonePlatform,
        //StageObjTypes.SeasideHillWaterfallSmall,
        //StageObjTypes.SeasideHillParticleEffect,

        //OceanPalace
        StageObjTypes.CrumblingStonePillar,
        //StageObjTypes.FallingStoneStructure,
        //StageObjTypes.OceanPalaceBreakableDoor,
        //StageObjTypes.OceanPalaceBreakableBlock,
        //StageObjTypes.Kaos,
        //StageObjTypes.ScrollRingObject,
        StageObjTypes.MovingItemBalloon,
        //StageObjTypes.OceanPalaceQuakeCollisionObject,
        //StageObjTypes.OceanPalaceTriggerEventActivate,
        //StageObjTypes.TriggerKaos,
        //StageObjTypes.TriggerMovingLand,
        //StageObjTypes.TurtleFeet,
        //StageObjTypes.TurtleWave,
        //StageObjTypes.OceanPalaceFlowingWater,
        //StageObjTypes.OceanPalaceGreenPlant,
        //StageObjTypes.OceanPalacePole,

        //GrandMetropolis
        StageObjTypes.EnergyRoadSection,
        //StageObjTypes.GrandMetropolisRoadCap,
        //StageObjTypes.GrandMetropolisDoor,
        StageObjTypes.FallingDrawbridge,
        StageObjTypes.TiltingBridge,
        //StageObjTypes.GrandMetropolisFlyingCar,
        StageObjTypes.BlimpPlatform,
        StageObjTypes.EnergyRoadSpeedEffect,
        //StageObjTypes.GrandMetropolisBalloonDesign,
        //StageObjTypes.GrandMetropolisPlaneTrigger,
        //StageObjTypes.GrandMetropolisTrain,
        //StageObjTypes.GrandMetropolisPipeDesign,
        //StageObjTypes.GrandMetropolisEnergyPiston,
        //StageObjTypes.GrandMetropolisFlashingDoorLights,
        //StageObjTypes.HEXAecoSignboard,

        //Power Plant
        StageObjTypes.EnergyRoadUpwardSection,
        StageObjTypes.EnergyColumn,
        StageObjTypes.Elevator,
        StageObjTypes.LavaPlatform,
        //StageObjTypes.PowerPlantLavaCap,
        //StageObjTypes.PowerPlantFireball,
        //StageObjTypes.PowerPlantColumnCap,
        StageObjTypes.PowerPlantShutter,
        StageObjTypes.LiquidLava,
        //StageObjTypes.PowerPlantElevatorCap,
        StageObjTypes.PowerPlantCollisionGlassBallObject,
        StageObjTypes.EnergyRoadUpwardEffect,
        //StageObjTypes.PowerPlantElevatorSupportColumn,
        StageObjTypes.PowerPlantGlassBall,
        //StageObjTypes.EnergyWallBackground,
        //StageObjTypes.PowerPlantCrane,
        //StageObjTypes.PowerPlantSatellite,
        //StageObjTypes.HEXAecoWallLight,
        //StageObjTypes.PowerPlantFloorLight,
        StageObjTypes.LavaShutter,

        //Casino Park
        StageObjTypes.SmallBumper,
        StageObjTypes.GreenFloatingBumper,
        StageObjTypes.PinballFlipper,
        StageObjTypes.SmallTriangleBumper,
        StageObjTypes.StarGlassPanel,
        StageObjTypes.StarGlassAirPanel,
        StageObjTypes.LargeTriangleBumper,
        //StageObjTypes.CasinoParkXSign,
        //StageObjTypes.LargeCasinoDoor,
        //StageObjTypes.BreakableGlassFloor,
        StageObjTypes.FloatingDice,
        StageObjTypes.TripleSlots,
        StageObjTypes.SingleSlots,
        StageObjTypes.BingoChart,
        StageObjTypes.BingoChip, 
        StageObjTypes.DashArrow,
        StageObjTypes.PotatoChip,
        //StageObjTypes.CasinoParkLightArrowSign,
        //StageObjTypes.CasinoParkLargeFloatingArrow,
        //StageObjTypes.CasinoParkLargeFloatingLetter,
        //StageObjTypes.UnusedFireworks,
        //StageObjTypes.GiantDiceDeco,
        //StageObjTypes.GiantSlotDeco,
        //StageObjTypes.GiantRouletteDeco,
        //StageObjTypes.GiantCasinoChipDeco,
        //StageObjTypes.CasinoParkSkybox,

        //BingoHighway
        //StageObjTypes.BingoHighwayBingoChartMaybeNotUsed,
        //StageObjTypes.BingoHighwayBingoNumberMaybeNotUsed,

        //RailCanyon
        StageObjTypes.SwitchableRail,
        StageObjTypes.RailSwitch,
        StageObjTypes.SwitchableArrow,
        StageObjTypes.RailBooster,
        StageObjTypes.RailCrossingRoadblock,
        StageObjTypes.Capsule,
        //StageObjTypes.StationDoor,
        //StageObjTypes.FloorGrate,
        StageObjTypes.RailPlatform,
        //StageObjTypes.DestructableRail,
        StageObjTypes.TrainTrain,
        //StageObjTypes.Tunnel,
        StageObjTypes.EngineCore,
        StageObjTypes.BigGunInterior,
        //StageObjTypes.BigCannonGunTopDeco,
        //StageObjTypes.TriggerTrainMaybeAmbience,
        //StageObjTypes.ExplosionEffect,
        //StageObjTypes.EggmanBase,
        //StageObjTypes.RailCanyonBobsledCollisionObject,
        //StageObjTypes.RailCanyonFan,
        //StageObjTypes.RailBush,
        //StageObjTypes.RailBarbedWireFence,
        //StageObjTypes.RailChangeRail,
        //StageObjTypes.RailBulletRack,
        //StageObjTypes.RailWaterSupply,
        //StageObjTypes.RailMechTypeABC,
        //StageObjTypes.RailCapEN,
        //StageObjTypes.RailCapEX,
        //StageObjTypes.RailWideCapBlue,
        //StageObjTypes.RailWideCapRed,
        //StageObjTypes.RailPollExclamationMark,
        //StageObjTypes.RailPollArrowLeft,
        //StageObjTypes.RailPollArrowRight,
        //StageObjTypes.RailTie,
        //StageObjTypes.RailCanyonPropeller,
        //StageObjTypes.Piston,
        StageObjTypes.Barrel,
        //StageObjTypes.RailCanyonPulley,
        //StageObjTypes.EggHorn,
        //StageObjTypes.TrainAppearOnOff,
        //StageObjTypes.CanyonBridge,
        //StageObjTypes.AutoDoor,
        //StageObjTypes.TrainTop,

        //BulletStation
        //StageObjTypes.BulletStationFanDeco,
        //StageObjTypes.MountainCannon,
        //StageObjTypes.BulletStationTorchDeco,
        //StageObjTypes.Wheel,
        //StageObjTypes.WallCanyon,

        //FrogForest
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
        //StageObjTypes.MossyBall,
        //StageObjTypes.StopRain,
        //StageObjTypes.Alligator,
        //StageObjTypes.RainFruitMI,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnIt2,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnIt3,
        //StageObjTypes.IvyThatGrowsAsYouGrindOnItETC,
        //StageObjTypes.RainCollisionObject,
        //StageObjTypes.Butterflies,
        //StageObjTypes.PinkFlower,
        //StageObjTypes.SmallMushroomDeco,
        //StageObjTypes.MediumPlant,
        //StageObjTypes.SmallPlantRedLeaves,
        //StageObjTypes.SmallPlant,
        //StageObjTypes.Bush,
        //StageObjTypes.YellowPlant,
        //StageObjTypes.GreenMushroom,
        //StageObjTypes.Pond,
        //StageObjTypes.Palmtree,
        //StageObjTypes.LargeLeaf,
        //StageObjTypes.WaterPlants,
        //StageObjTypes.WigglingMushroom,
        //StageObjTypes.HangingYellowFruit,
        //StageObjTypes.TreeLeaf,
        //StageObjTypes.MossPatchOnGround,
        //StageObjTypes.LargeGreenThing,
        //StageObjTypes.LargePlant,
        //StageObjTypes.SwampWater,
        //StageObjTypes.Powder,
        //StageObjTypes.FloatingTrunk,
        //StageObjTypes.Rain,

        //LostJungle
        StageObjTypes.BlackFrog,
        StageObjTypes.BouncyFallingFruit,
        //StageObjTypes.LostJungleRain,
        //StageObjTypes.LostJunglePond,
        //StageObjTypes.LostJungleSwampWater,

        //HangCastle
        StageObjTypes.TeleporterSwitch,
        //StageObjTypes.CastleDoor,
        //StageObjTypes.CastleCrackedWall,
        StageObjTypes.CastleFloatingPlatform,
        StageObjTypes.FlameTorch,
        StageObjTypes.PumpkinGhost,
        StageObjTypes.MansionFloatingPlatform,
        //StageObjTypes.MansionCrackedWall,
        //StageObjTypes.MansionDoor,
        StageObjTypes.CastleKey,
        //StageObjTypes.HangCastleBobsledDummyObject,
        //StageObjTypes.TriggerDoor,
        //StageObjTypes.TriggerMusic,
        //StageObjTypes.GlowEffect,
        //StageObjTypes.CelestialSphere,
        //StageObjTypes.CastleThunderLightning,
        //StageObjTypes.CastleTriggerThunderLightning,
        //StageObjTypes.SmokeScreen,
        //StageObjTypes.Skeleton,
        //StageObjTypes.TriggerSkeleton,
        //StageObjTypes.SpinningSkeletonHands,
        //StageObjTypes.CastleCurtain,
        //StageObjTypes.GlowingSpiderSigns,
        //StageObjTypes.CastleTree,
        //StageObjTypes.SpikedPlant,
        //StageObjTypes.CastleSmallPlant,
        //StageObjTypes.SwingingAxe,

        //MysticMansion
        //StageObjTypes.MysticMansionPumpkinGhost,
        //StageObjTypes.MysticMansionSkeleton,
        //StageObjTypes.MysticMansionDoor,
        //StageObjTypes.MysticMansionFlameTorchDeco,

        //EggFleet
        StageObjTypes.NormalCannon,
        StageObjTypes.LargeCannon,
        StageObjTypes.HorizontalCannon,
        StageObjTypes.MovingCannon,
        StageObjTypes.RectangularFloatingPlatform,
        //StageObjTypes.EggFleetDoor,
        StageObjTypes.SquareFloatingPlatform,
        //StageObjTypes.EggFleetRoadblock,
        //StageObjTypes.ConveyorBelt,
        //StageObjTypes.BigMovShip,
        StageObjTypes.AnotherCannon,
        StageObjTypes.KanKyoHakai,
        //StageObjTypes.BigFan,
        //StageObjTypes.MissilePod,
        //StageObjTypes.Screw,
        //StageObjTypes.EggFleetDesignPipe,
        //StageObjTypes.EggFleetUFO,
        //StageObjTypes.Blinklight,
        //StageObjTypes.Antenna,
        //StageObjTypes.SenkanFar1,
        //StageObjTypes.SenkanFar2,
        //StageObjTypes.SenkanFar3,
        //StageObjTypes.SenkanFar4,
        //StageObjTypes.SenkanFar5,
        //StageObjTypes.SenkanFar6,
        //StageObjTypes.SenkanFar7,
        //StageObjTypes.SenkanFar8,
        //StageObjTypes.SenkanMiddle1,
        //StageObjTypes.SenkanMiddle2,
        //StageObjTypes.EggFleetRailCapFront,
        //StageObjTypes.EggFleetRailCapBack,
        //StageObjTypes.EggFleetRailArrow1,
        //StageObjTypes.EggFleetRailArrow3,
        //StageObjTypes.SenkanFarMoveTopLeft,
        //StageObjTypes.SenkanFarMoveTopRight,
        //StageObjTypes.SenkanFarMoveSideLeft,
        //StageObjTypes.SenkanFarMoveSideRight,
        //StageObjTypes.Cloud1,
        //StageObjTypes.Cloud2,
        //StageObjTypes.SenkanFarMoveBig,

        //FinalFortress
        StageObjTypes.FallingPlatform,
        StageObjTypes.HigherCannon,
        //StageObjTypes.LaserBeam,
        //StageObjTypes.TriggerLaserBeam,
        //StageObjTypes.LaserBeamLightSign,
        StageObjTypes.SelfDestructSwitch,
        //StageObjTypes.FinalFortressBreakableBlock,
        StageObjTypes.EggmanCellKey,
        //StageObjTypes.Thunder,
        //StageObjTypes.Thunder2,
        //StageObjTypes.ThunderParticle,
        //StageObjTypes.LaserLight,
        //StageObjTypes.RailEndSign,
        //StageObjTypes.RedLight,
        //StageObjTypes.RoadSideA,
        //StageObjTypes.RoadLight,
        //StageObjTypes.FinalFortressUFO,
        //StageObjTypes.RedRingLight,
        //StageObjTypes.WallNeon,
        //StageObjTypes.WallLightSide,
        //StageObjTypes.WallLightFront,
        //StageObjTypes.WallNeonLeft,
        //StageObjTypes.WallNeonRight,
        //StageObjTypes.FinalFortressGoalNeonFloor,
        //StageObjTypes.NeonFloor,
        //StageObjTypes.RoadsideB,
        //StageObjTypes.NeonFloorB,
        //StageObjTypes.TowerNeonA,
        //StageObjTypes.TowerNeonB,
        //StageObjTypes.FinalFortressSearchLight,
        //StageObjTypes.FinalFortressEggMansBase,
        //StageObjTypes.CrushedRoof,
        //StageObjTypes.DecoWallSide,

        //Enemies
        StageObjTypes.EggFlapper,
        StageObjTypes.EggPawn,
        StageObjTypes.Klagen,
        StageObjTypes.Falco,
        StageObjTypes.EggHammer,
        StageObjTypes.Cameron,
        StageObjTypes.RhinoLiner,
        StageObjTypes.EggBishop,
        StageObjTypes.E2000,
        //StageObjTypes.EggMobileObj,
        //StageObjTypes.MetalSonic1,
        //StageObjTypes.MetalSonic2,
        //StageObjTypes.MetalMadnessObj,
        //StageObjTypes.MetalOverlordObj,
        StageObjTypes.SpecialStageOrbs,
        //StageObjTypes.SpecialStageBossAppear,
        //StageObjTypes.SpecialStageBossEnd,
        //StageObjTypes.SpecialStageBossAppearPos,
        StageObjTypes.AppearEmerald,
        //StageObjTypes.SkyBobsleigh,
        //StageObjTypes.SkyBobsledEnd,
        //StageObjTypes.PutParticle,
        //StageObjTypes.ParticleTest,
        //StageObjTypes.SpecialStageEnd,
        StageObjTypes.SpecialStageSpring,
        StageObjTypes.SpecialStageDashPanel,
        StageObjTypes.SpecialStageDashRing,
        //StageObjTypes.SpecialStageFormationGate,

        //EggEmperor
        //StageObjTypes.EggEmperorCollisionCC,
        //StageObjTypes.EggEmperorCollisionCP,
        //StageObjTypes.EggEmperorKingPawn,

        //EggHawk
        //StageObjTypes.EggHawkRoadDecoBlock,
        //StageObjTypes.EggHawkWhaleStatue,
        //StageObjTypes.EggHawkTower,
        //StageObjTypes.EggTreatStar,

        //EggAlbatross
        //StageObjTypes.TriggerEggAlbatross,

        //MetalMadness
        //StageObjTypes.TriggerMetalMadness,

        //MetalOverlord
        //StageObjTypes.TriggerMetalOverlord,

        //MultiplayerBobsled
        //StageObjTypes.UnbrokenBobCountObject,
        //StageObjTypes.SeasideBobsledCourseBobsledDummyObject,
        //StageObjTypes.CasinoCourseChipObject,
        //StageObjTypes.CasinoCourseDiceObject,
        //StageObjTypes.CasinoCourseRouletteObject,
        //StageObjTypes.CasinoCourseSlotObject,
        //StageObjTypes.UnknownBobsledObject,

        //Unknown
        //StageObjTypes.CustomObjectTest,
        //StageObjTypes.SystemObject1,
        //StageObjTypes.SystemObject2,
        //StageObjTypes.SystemObject3,
        //StageObjTypes.SampleObject1,
        //StageObjTypes.SampleObject2,
        
    ];
    
    public static readonly List<StageObjTypes> SeasideHillStageObjsToNotSpawn =
    [
        StageObjTypes.SingleSpring,
        StageObjTypes.TripleSpring,
        StageObjTypes.Rings,
        StageObjTypes.HintRing,
        StageObjTypes.RegularSwitch,
        StageObjTypes.PushAndPullSwitch,
        StageObjTypes.TargetSwitch,
        StageObjTypes.DashPanel,
        StageObjTypes.DashRing,
        StageObjTypes.RainbowHoops,
        StageObjTypes.Checkpoint,
        StageObjTypes.DashRamp,
        StageObjTypes.Cannon,
        StageObjTypes.ItemBox,
        StageObjTypes.ItemBalloon,
        StageObjTypes.GoalRing,
        StageObjTypes.Pulley,
        StageObjTypes.LostChao,
        StageObjTypes.CageBox,
        StageObjTypes.Propeller,
        StageObjTypes.Pole,
        StageObjTypes.Gong,
        StageObjTypes.Fan,
        StageObjTypes.WarpFlower,
        StageObjTypes.BonusKey,
        
        StageObjTypes.MovingRuinPlatform,
        StageObjTypes.TriggerRuins,
        StageObjTypes.HermitCrab,

        //Enemies
        StageObjTypes.EggFlapper,
        StageObjTypes.EggPawn,
        StageObjTypes.Klagen,
        StageObjTypes.Falco,
        StageObjTypes.EggHammer,
        StageObjTypes.Cameron,
        StageObjTypes.RhinoLiner,
        StageObjTypes.EggBishop,
        StageObjTypes.E2000,
    ];
    
    public static Dictionary<StageObjTypes, List<StageObjSpawnData>> BackupStageObjSpawnData = new();


    public static List<StageObjTypes> StageObjsWithSpecialHandlingWhenReceivingCharItem =
    [
        StageObjTypes.SelfDestructSwitch,
    ];


    public static readonly Dictionary<Team, Dictionary<LevelId, List<float>>> BobsledInitialYCoords = new()
    {
        { 
            Team.Sonic, new()
            {
                {
                    LevelId.SeasideHill, [402.00f, 2002.00f]
                },
            }
        },
        { 
            Team.Dark, new()
            {
                {
                    LevelId.SeasideHill, [402.00f, 2002.00f]
                },
            }
        },
    };

}