

using System.Numerics;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;

namespace Sonic_Heroes_AP_Client.StageObj;



public static class StageObjHandler
{
    public static void ForceUnlockAllStageObjs(Team? team, Region? region)
    {
        try
        {
            foreach (var obj in StageObjData.StageObjsToMessWith)
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
    
    public static void UnlockStageObjItemCallback(StageObjTypes? stageObjTypes, Team? team, Region? region, bool forceunlock = false)
    {
        try
        {
            if (stageObjTypes is null)
            {
                foreach (var s in StageObjData.StageObjsToMessWith)
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
                foreach (var r in Enum.GetValues<Region>())
                {
                    UnlockStageObjItemCallback(stageObjTypes, team, r);
                }
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
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static unsafe void StageObjsPollUpdates(StageObjTypes stageObjTypes, Team team, Region region, bool unlock)
    {
        try
        {
            if (!GameStateHandler.InGame())
                return;

            if (!ArchipelagoHandler.IsConnected)
                return;
            
            Team? teamInGame = GameStateHandler.GetCurrentStory();
            Act? act = GameStateHandler.GetCurrentAct();
            LevelId? levelId = GameStateHandler.GetCurrentLevel();
            
            if (teamInGame == null || levelId == null || act == null)
                return;
            if (team != teamInGame)
                return;

            if (!SonicHeroesDefinitions.LevelIdToRegion.ContainsKey((LevelId)levelId))
            {
                Console.WriteLine($"LevelId {levelId} does not exist in Region Mapping");
                return;
            }
            Region regionInGame = SonicHeroesDefinitions.LevelIdToRegion[(LevelId)levelId];

            //TODO If Region becomes important on StageObjs, handle checking here
            //if (region != regionInGame)
                //return;
            
            
            var foundAddrs = GetAddrsOfObjInTable([stageObjTypes]);
            
            var renderDistance = unlock ? (byte)0x20 : (byte)0x00;
            
            if (stageObjTypes is StageObjTypes.SelfDestructSwitch)
                //check for speed char here
                if (!Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[(Team)teamInGame].CharsUnlocked[FormationChar.Speed])
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
    
    
    public static unsafe void HandleObjSpawningWhenReceivingCharItem(Team team, FormationChar formationChar, bool unlock)
    {
        try
        {
            if (!GameStateHandler.InGame(true))
                return;
            
            Team? teamInGame = GameStateHandler.GetCurrentStory();
            LevelId? level = GameStateHandler.GetCurrentLevel();
            Act? act = GameStateHandler.GetCurrentAct();

            if (teamInGame == null || level == null || act == null)
                return;
            if (team != teamInGame)
                return;
            
            var foundAddrs = GetAddrsOfObjInTable(StageObjData.StageObjsWithSpecialHandling);
            
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static unsafe Dictionary<StageObjTypes, List<IntPtr>> GetAddrsOfObjInTable(List<StageObjTypes> objTypesList)
    {
        try
        {
            //each OBJ is 0x40 bytes
            IntPtr currentObjPtr = StageObjData.StartOfStageObjTable;

            Dictionary<StageObjTypes, List<IntPtr>> foundAddrs = objTypesList.ToDictionary(x => x, x => new List<IntPtr>());


            var numObjs = 0;

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
        try
        {
            //Obj Table is loaded into memory already
            //look into making changes to Objs like coords and the like

            Team? team = GameStateHandler.GetCurrentStory();
            LevelId? level = GameStateHandler.GetCurrentLevel();
            Act? act = GameStateHandler.GetCurrentAct();

            if (team == null || level == null || act == null)
                return;
            
            HandleStageObjs((Team)team, (LevelId)level, (Act)act);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
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
            case Team.SuperHardMode:
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
            var objsToFind = new List<StageObjTypes>(StageObjData.StageObjsToMessWith);
            
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

                    var renderDistance =
                        Mod.ArchipelagoHandler.SlotData.RemoveCasinoParkVIPTableLaserGate ? (byte)0x00 : (byte)0xA;
                    Console.WriteLine($"Setting Casino Park Sonic VIP Table Laser Gate to 0x{renderDistance:X}");
                    foreach (var laserAddr in foundAddrs[StageObjTypes.LaserFence])
                    {
                        var tempObj = (ObjSpawnData*) new IntPtr(laserAddr);
                        tempObj->RenderDistance = renderDistance;
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
        try
        {
            var objsToFind = new List<StageObjTypes>(StageObjData.StageObjsToMessWith);
            
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

            foreach (var pair in Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[Team.Dark])
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
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