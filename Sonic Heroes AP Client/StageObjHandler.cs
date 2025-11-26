using System.Numerics;
using System.Windows.Forms;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;

namespace Sonic_Heroes_AP_Client;

public class StageObjHandler
{
    
    public const IntPtr StartOfStageObjTable = 0xA825D8; 
    //Mod.ModuleBase + 0x6825D8
    
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


    public void ForceUnlockAllStageObjs(Team? team, Region? region)
    {
        try
        {
            foreach (var obj in StageObjsToMessWith)
            {
                UnlockStageObjItemCallback(obj, team, region, true);
                //Mod.SaveDataHandler!.CustomSaveData!.StageObjSpawnSaveData[(Team)team][obj] = true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    
    public void UnlockStageObjItemCallback(StageObjTypes? stageObjTypes, Team? team, Region? region, bool forceunlock = false)
    {
        try
        {
            if (stageObjTypes is null)
            {
                foreach (var s in StageObjsToMessWith)
                {
                    UnlockStageObjItemCallback(s, team, region, forceunlock);
                }
            }
            else if (team is null)
            {
                foreach (var t in Enum.GetValues<Team>())
                {
                    UnlockStageObjItemCallback(stageObjTypes, t, region, forceunlock);
                }
            }
        
            else if (region is null)
            {
                //TODO make handling for Regions such as Boss and Final Boss
                /*
                foreach (var r in Enum.GetValues<Region>())
                {
                    UnlockStageObjItemCallback(stageObjTypes, team, r);
                }
                */
                UnlockStageObjItemCallback(stageObjTypes, team, Region.Ocean, forceunlock);
            }
            else
            {
                var currState = Mod.SaveDataHandler!.CustomSaveData!.StageObjSpawnSaveData[(Team)team][(StageObjTypes)stageObjTypes];
                if (forceunlock)
                    currState = false;
                Console.WriteLine($"StageObjItemReceived. Obj: {(StageObjTypes)stageObjTypes} Team: {(Team)team} Region: {(Region)region} currState: {currState} newState: {!currState} forceunlock: {forceunlock}");
                if (!Mod.IsDebug)
                    currState = false;
                Mod.SaveDataHandler!.CustomSaveData!.StageObjSpawnSaveData[(Team)team][(StageObjTypes)stageObjTypes] = !currState;
                StageObjsPollUpdates((StageObjTypes)stageObjTypes, (Team)team, (Region)region, !currState);
                
                //Mod.GameHandler.SetBonusKey(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public unsafe void StageObjsPollUpdates(StageObjTypes stageObjTypes, Team team, Region region, bool unlock)
    {
        try
        {
            if (!Mod.GameHandler!.InGame())
                return;

            if (!ArchipelagoHandler.IsConnected)
                return;
            
            Team teamInGame = Mod.GameHandler.GetCurrentStory();
            Act act = Mod.GameHandler.GetCurrentAct();
            LevelId levelId = Mod.GameHandler.GetCurrentLevel();
        
            //Console.WriteLine($"Running Poll Updates");

            if (!GameHandler.LevelIdToRegion.ContainsKey(levelId))
            {
                Console.WriteLine($"LevelId {levelId} does not exist in Region Mapping");
                return;
            }
        
            Region regionInGame = GameHandler.LevelIdToRegion[levelId];
            
            if (teamInGame is Team.Sonic && act is Act.Act2 or Act.Act3
                                         && Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
            {
                teamInGame = Team.SuperHard;
                //Console.WriteLine($"Team is Super Hard");
            }

            if (team != teamInGame)
                return;

            //TODO If Region becomes important on StageObjs, handle checking here
            //if (region != regionInGame)
                //return;
            
            
            var foundAddrs = GetAddrsOfObjInTable([stageObjTypes]);
            
            
            var renderDistance = unlock ? (byte)0x20 : (byte)0x00;
            
            if (stageObjTypes is StageObjTypes.SelfDestructSwitch)
                //check for speed char here
                if (!Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[teamInGame].CharsUnlocked[FormationChar.Speed])
                    renderDistance = 0x00;

            foreach (var addr in foundAddrs[stageObjTypes])
            {
                var tempObj = (ObjSpawnData*) new IntPtr(addr);
                tempObj->RenderDistance = renderDistance;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static unsafe void OnObjSetStateSpawned(int esi)
    {
        
        ObjSpawnData* spawnData = (ObjSpawnData*) new IntPtr(esi);

        if ((StageObjTypes)spawnData->ObjId is StageObjTypes.TripleSpring)
        {
            IncreaseRenderDistanceByFloat(spawnData, 5.0f);
        }
    }

    public static unsafe void IncreaseRenderDistanceByFloat(ObjSpawnData* _spawnData, float _distanceMultiplier)
    {
        var temp = _spawnData->RenderDistance;
        //ObjSpawnData* spawnData = (ObjSpawnData*) new IntPtr(esi);
        _spawnData->RenderDistance = byte.Max((byte)Math.Min((int)(_spawnData->RenderDistance * _distanceMultiplier), 255), 0x00);
        //Console.WriteLine($"IncreaseRenderDistanceByFloat: OldRenderDistance = 0x{temp:x} ::  " +
                          //$"NewRenderDistance = 0x{_spawnData->RenderDistance:x}");
    }


    public static unsafe void HandleObjSpawningWhenReceivingCharItem(Team team, FormationChar formationChar, bool unlock)
    {
        if (!Mod.GameHandler!.InGame(true))
            return;
        
        Team teamInGame = Mod.GameHandler.GetCurrentStory();
        LevelId level = Mod.GameHandler.GetCurrentLevel();
        Act act = Mod.GameHandler.GetCurrentAct();

        
        if (team is Team.Sonic && act is Act.Act2 or Act.Act3 && Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
        {
            team = Team.SuperHard;
        }
        
        if (teamInGame is Team.Sonic && act is Act.Act2 or Act.Act3 && Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
        {
            teamInGame = Team.SuperHard;
        }
        
        if (team != teamInGame)
            return;
        
        
        var objsToFind = new List<StageObjTypes>()
        {
            StageObjTypes.SelfDestructSwitch,
        };
        
        
        var foundAddrs = GetAddrsOfObjInTable(objsToFind);

        if (formationChar is FormationChar.Speed && team is Team.Sonic)
        {
            //Final Fortress Sonic Respawn Self Destruct Switches When Getting Sonic
            if (level is LevelId.FinalFortress)
            {
                //UnSpawn Self Destruct Switches if no Sonic
                var stringValue = unlock ? "Respawning" : "Despawning";
                var selfDestructSwitchItem = Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[team][StageObjTypes.SelfDestructSwitch];
                var renderValue = unlock && selfDestructSwitchItem ? (byte)0x0F : (byte)0x00;
                foreach (var switchAddr in foundAddrs[StageObjTypes.SelfDestructSwitch])
                {
                    Console.WriteLine($"Final Fortress Sonic {stringValue} SelfDestruct Switch at Address 0x{switchAddr:x}");
                    var tempObj = (ObjSpawnData*) new IntPtr(switchAddr);
                    tempObj->RenderDistance = renderValue;
                }
            }
        }
    }

    public static unsafe Dictionary<StageObjTypes, List<IntPtr>> GetAddrsOfObjInTable(List<StageObjTypes> objTypesList)
    {
        try
        {
            //each OBJ is 0x40 bytes
            IntPtr currentObjPtr = StartOfStageObjTable;

            Dictionary<StageObjTypes, List<IntPtr>> foundAddrs = objTypesList.ToDictionary(x => x, x => new List<IntPtr>());


            int numObjs = 0;

            while (true)
            {
                ObjSpawnData* tempObj = (ObjSpawnData*) new IntPtr(currentObjPtr);

                if (!Enum.IsDefined(typeof(StageObjTypes), tempObj->ObjId))
                {
                    Console.WriteLine($"This OBJID does not exist in table: 0x{tempObj->ObjId:x}. The Addr is: 0x{currentObjPtr:x}");
                    break;
                }

                if ((StageObjTypes)tempObj->ObjId is StageObjTypes.None)
                {
                    Console.WriteLine($"Table should end here: 0x{currentObjPtr:x}");
                    break;
                }
                
                numObjs++;
                if (objTypesList.Contains((StageObjTypes)tempObj->ObjId))
                {
                    foundAddrs[(StageObjTypes)tempObj->ObjId].Add(currentObjPtr);
                    //Console.WriteLine($"I found a {(StageObjTypes)tempObj->ObjId} with Addr: 0x{currentObjPtr:x}");
                }
                currentObjPtr += 0x40;
            }
            
            Console.WriteLine($"Found {numObjs} StageObjects");
            
            return foundAddrs;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }


    public static unsafe void HandleInitSetGenerator()
    {
        //Obj Table is loaded into memory already
        //look into making changes to Objs like coords and the like

        Team team = Mod.GameHandler.GetCurrentStory();
        LevelId level = Mod.GameHandler.GetCurrentLevel();
        Act act = Mod.GameHandler.GetCurrentAct();

        if (team is Team.Sonic && act is Act.Act2 or Act.Act3 && Mod.ArchipelagoHandler!.SlotData.SuperHardModeSonicAct2)
        {
            team = Team.SuperHard;
        }
        
        //Console.WriteLine($"Running HandleInitSetGenerator: Team: {team} Level: {level} Act: {act}");
        
        HandleStageObjs(team, level, act);
        
        /*
        //Casino Park Sonic
        //A8B5C3 
        //change to 0
        if (team is Team.Sonic && level == LevelId.CasinoPark && Mod.ArchipelagoHandler.SlotData.RemoveCasinoParkVIPTableLaserGate)
        {
            Console.WriteLine($"Removing Casino Park Sonic VIP Table Laser Gate");
            var renderDistance = (byte*)(Mod.ModuleBase + 0x68B5C3);
            *renderDistance = 0x00;
        }
        
        
        //Rail Canyon Sonic
        //A9151C
        //change to 12620
        if (team is Team.Sonic && level == LevelId.RailCanyon)
        {
            Console.WriteLine($"Rail Canyon Sonic Bonus Key 3 moving down");
            var bonusKey3YCoord = (float*)(Mod.ModuleBase + 0x69151C);
            *bonusKey3YCoord = 12620f;
        }
        
        //Hang Castle Sonic
        //A9125C
        //change to -1755
        if (team is Team.Sonic && level == LevelId.HangCastle)
        {
            Console.WriteLine($"Hang Castle Sonic Bonus Key 3 moving down");
            var bonusKey3YCoord = (float*)(Mod.ModuleBase + 0x69125C);
            *bonusKey3YCoord = -1755f;
        }
        
        //Mystic Mansion Sonic
        //A8A8D8 (coords)
        //change to 15420 -8878 -39730
        if (team is Team.Sonic && level == LevelId.MysticMansion)
        {
            Console.WriteLine($"Mystic Mansion Sonic Bonus Key 3 moving down");
            var bonusKey3XCoord = (float*)(Mod.ModuleBase + 0x68A8D8);
            var bonusKey3YCoord = (float*)(Mod.ModuleBase + 0x68A8D8 + 4);
            var bonusKey3ZCoord = (float*)(Mod.ModuleBase + 0x68A8D8 + 8);
            *bonusKey3XCoord = 15420f;
            *bonusKey3YCoord = -8878f;
            *bonusKey3ZCoord = -39730f;
        }
        
        
        //Final Fortress Sonic
        //A8945C
        //change to 5400 (y)
        if (team is Team.Sonic && level == LevelId.FinalFortress)
        {
            Console.WriteLine($"Final Fortress Sonic Bonus Key 2 moving down");
            var bonusKey2YCoord = (float*)(Mod.ModuleBase + 0x68945C);
            *bonusKey2YCoord = 5400;
            
            
            //UnSpawn Self Destruct Switches if no Sonic
            var spawnSwitches = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic]
                .CharsUnlocked[FormationChar.Speed];
            var stringValue = spawnSwitches ? "Spawning" : "Despawning";
            var renderValue = spawnSwitches ? (byte)0x0F : (byte)0x00;
            
            //Switch 1 A85CC3
            Console.WriteLine($"Final Fortress Sonic {stringValue} SelfDestruct Switch 1");
            var renderDistance1 = (byte*)(Mod.ModuleBase + 0x685CC3);
            *renderDistance1 = renderValue;
            
            //Switch 2 A86403
            Console.WriteLine($"Final Fortress Sonic {stringValue} SelfDestruct Switch 2");
            var renderDistance2 = (byte*)(Mod.ModuleBase + 0x686403);
            *renderDistance2 = renderValue;
            
            //Switch 3 A86443
            Console.WriteLine($"Final Fortress Sonic {stringValue} SelfDestruct Switch 3");
            var renderDistance3 = (byte*)(Mod.ModuleBase + 0x686443);
            *renderDistance3 = renderValue;
            
        }
        */
        
    }


    public static void HandleStageObjs(Team team, LevelId level, Act act)
    {
        switch (team)
        {
            case Team.Sonic:
                HandleSonicStageObjs(level, act);
                break;
            case Team.Dark:
                HandleDarkStageObjs(level, act);
                break;
            case Team.Rose:
                HandleRoseStageObjs(level, act);
                break;
            case Team.Chaotix:
                HandleChaotixStageObjs(level, act);
                break;
            case Team.SuperHard:
                HandleSuperHardStageObjs(level, act);
                break;
            default:
                break;
        }
    }
    

    public static unsafe void HandleSonicStageObjs(LevelId level, Act act)
    {
        try
        {
            var objsToFind = new List<StageObjTypes>(StageObjsToMessWith);
            
            //if (!objsToFind.Contains(StageObjTypes.LaserFence))
            objsToFind.Add(StageObjTypes.LaserFence);
            
            //if (!objsToFind.Contains(StageObjTypes.CageBox))
            objsToFind.Add(StageObjTypes.CageBox);
            
            
            var foundAddrs = GetAddrsOfObjInTable(objsToFind);

            foreach (var cageAddr in foundAddrs[StageObjTypes.CageBox])
            {
                var tempObj = (ObjSpawnData*) new IntPtr(cageAddr);
                tempObj->RenderDistance = 0x00;
            }

            foreach (var pair in Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[Team.Sonic])
            {
                if (!pair.Value)
                {
                    foreach (var objAddr in foundAddrs[pair.Key])
                    {
                        var tempObj = (ObjSpawnData*) new IntPtr(objAddr);
                        tempObj->RenderDistance = 0x00;
                    }
                }
            }
            
            
            switch (level)
            {
                case LevelId.CasinoPark:
                {
                    //Casino Park Sonic
                    //A8B5C3 (addr)
                    //change to 0
                    //-6480.093 430 1695.467    (coord)
                    //There is only 1 laser gate in Casino Park Sonic
                    Console.WriteLine($"Removing Casino Park Sonic VIP Table Laser Gate");
                    foreach (var laserAddr in foundAddrs[StageObjTypes.LaserFence])
                    {
                        var tempObj = (ObjSpawnData*) new IntPtr(laserAddr);
                        tempObj->RenderDistance = 0x00;
                    }
                    break;
                }
                case LevelId.RailCanyon:
                {
                    //Rail Canyon Sonic
                    //A9151C
                    //change to 12620
                    Console.WriteLine($"Rail Canyon Sonic Bonus Key 3 moving down");
                    foreach (var keyAddr in foundAddrs[StageObjTypes.BonusKey])
                    {
                        //-55567.08f, 12762.00f, -20100.07f <- is normally here
                        //-55567.08f, 12620.00f, -20100.07f <- is moved to here to not have cage
                        var tempObj = (ObjSpawnData*) new IntPtr(keyAddr);
                        Vector3 oldPos = new Vector3(tempObj->XSpawnPos,  tempObj->YSpawnPos, tempObj->ZSpawnPos);
                        Vector3 key3Pos = new Vector3(-55567.08f, 12762.00f, -20100.07f);
                        if (Vector3.Distance(oldPos, key3Pos) < 175)
                        {
                            tempObj->YSpawnPos = 12620f;
                        }
                    }
                    break;
                }

                case LevelId.FrogForest:
                {
                    //Move Cage with BonusKey 1 Down in case it spawns in level (so it doesnt block getting Key)
                    //0, 1000, -5349.7f <- is normally here
                    //0, 800, -5349.7f <- is moved to here to not block Key 1
                    foreach (var cageAddr in foundAddrs[StageObjTypes.CageBox])
                    {
                        var tempObj = (ObjSpawnData*) new IntPtr(cageAddr);
                        tempObj->RenderDistance = 0x00;
                        Vector3 oldPos = new Vector3(tempObj->XSpawnPos,  tempObj->YSpawnPos, tempObj->ZSpawnPos);
                        Vector3 cage1Pos = new Vector3(0, 1000, -5349.7f);
                        if (Vector3.Distance(oldPos, cage1Pos) < 175)
                        {
                            tempObj->YSpawnPos = 800f;
                        }
                    }
                    break;
                }

                case LevelId.HangCastle:
                {
                    Console.WriteLine($"Hang Castle Sonic Bonus Key 3 moving down");
                    foreach (var keyAddr in foundAddrs[StageObjTypes.BonusKey])
                    {
                        //10700.52f, -1595.80f, -13541.10f <- is normally here
                        //10700.52f, -1755f, -13541.10f <- is moved to here to not have cage
                        var tempObj = (ObjSpawnData*) new IntPtr(keyAddr);
                        Vector3 oldPos = new Vector3(tempObj->XSpawnPos,  tempObj->YSpawnPos, tempObj->ZSpawnPos);
                        Vector3 key3Pos = new Vector3(10700.52f, -1595.80f, -13541.10f);
                        if (Vector3.Distance(oldPos, key3Pos) < 175)
                        {
                            tempObj->YSpawnPos = -1755f;
                        }
                    }
                    break;
                }

                case LevelId.MysticMansion:
                {
                    //Mystic Mansion Sonic
                    //A8A8D8 (coords)
                    //15420.056f, -8739.9f, -39680.32f
                    //change to 15420.056f, -8878f, -39730f
                    Console.WriteLine($"Mystic Mansion Sonic Bonus Key 3 moving down");
                    foreach (var keyAddr in foundAddrs[StageObjTypes.BonusKey])
                    {
                        //15420.056f, -8739.9f, -39680.32f <- is normally here
                        //15420.056f, -8878f, -39730f <- is moved to here to not have cage
                        var tempObj = (ObjSpawnData*) new IntPtr(keyAddr);
                        Vector3 oldPos = new Vector3(tempObj->XSpawnPos,  tempObj->YSpawnPos, tempObj->ZSpawnPos);
                        Vector3 key3Pos = new Vector3(15420.056f, -8739.9f, -39680.32f);
                        if (Vector3.Distance(oldPos, key3Pos) < 175)
                        {
                            tempObj->YSpawnPos = -8878f;
                            tempObj->ZSpawnPos = -39730f;
                        }
                    }
                    break;
                }
                case LevelId.FinalFortress:
                {
                    //Final Fortress Sonic
                    //A8945C
                    //change to 5400 (y)
                    
                    Console.WriteLine($"Final Fortress Sonic Bonus Key 2 moving down");
                    foreach (var keyAddr in foundAddrs[StageObjTypes.BonusKey])
                    {
                        //2250.01f, 5552.00f, 33690.04f <- is normally here
                        //2250.01f, 5400.00f, 33690.04f <- is moved to here to not have cage
                        var tempObj = (ObjSpawnData*) new IntPtr(keyAddr);
                        Vector3 oldPos = new Vector3(tempObj->XSpawnPos,  tempObj->YSpawnPos, tempObj->ZSpawnPos);
                        Vector3 key3Pos = new Vector3(2250.01f, 5552.00f, 33690.04f);
                        if (Vector3.Distance(oldPos, key3Pos) < 175)
                        {
                            tempObj->YSpawnPos = 5400f;
                        }
                    }
                    
                    
                    //UnSpawn Self Destruct Switches if no Sonic
                    var hasSpeedChar = Mod.SaveDataHandler!.CustomSaveData!.UnlockSaveData[Team.Sonic].CharsUnlocked[FormationChar.Speed];
                    var selfDestructSwitchItem = Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[Team.Sonic][StageObjTypes.SelfDestructSwitch];
                    var stringValue = hasSpeedChar && selfDestructSwitchItem ? "Spawning" : "Despawning";
                    var renderValue = hasSpeedChar && selfDestructSwitchItem ? (byte)0x0F : (byte)0x00;
                    
                    foreach (var switchAddr in foundAddrs[StageObjTypes.SelfDestructSwitch])
                    {
                        Console.WriteLine($"Final Fortress Sonic {stringValue} SelfDestruct Switch at Address 0x{switchAddr:x}");
                        var tempObj = (ObjSpawnData*) new IntPtr(switchAddr);
                        tempObj->RenderDistance = renderValue;
                    }
                    
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static unsafe void HandleDarkStageObjs(LevelId level, Act act)
    {
        
    }
    public static unsafe void HandleRoseStageObjs(LevelId level, Act act)
    {
        
    }
    public static unsafe void HandleChaotixStageObjs(LevelId level, Act act)
    {
        
    }
    public static unsafe void HandleSuperHardStageObjs(LevelId level, Act act)
    {
        
    }
}