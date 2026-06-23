

using System.Numerics;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj;



public static class StageObjHandler
{
    public static void ForceUnlockAllStageObjs(Team? team, Region? region, string taskName)
    {
        UnlockStageObjItemCallback(null, team, region, taskName, true);
        //Mod.SaveDataHandler!.CustomSaveData!.StageObjSpawnSaveData[(Team)team][obj] = true;
    }

    public static unsafe void SpawnOrUnSpawnBobsled(Team team, LevelId levelId, bool forceDespawn, int index, string taskName)
    {
        try
        {
            if (!StageObjData.BobsledInitialYCoords.TryGetValue(team, out var levelDict))
            {
                return;
            }

            if (!levelDict.TryGetValue(levelId, out var bobsledYCoords))
            {
                return;
            }
            
            var bobsledPtr = Mod.ModuleBase + 0x5CE618 + (UIntPtr)(4 * index);
            LoggingHandler.LogMessage($"Bobsled Ptr Here: 0x{(int)bobsledPtr:X} and is value: 0x{*(int*)bobsledPtr:X}", taskName, LogLevel.Debug);
            var bobsledStartAddr = *(int*)bobsledPtr;
            LoggingHandler.LogMessage($"Bobsled Start Addr Here: 0x{bobsledStartAddr:X}", taskName, LogLevel.Debug);
            var bobsledYCoordPtr = (float*)(bobsledStartAddr + 0x9C);
            LoggingHandler.LogMessage($"Bobsled Y Coord Here: 0x{(UIntPtr)bobsledYCoordPtr:X} and value is: {*bobsledYCoordPtr}", taskName, LogLevel.Debug);
            float oldYCoord = *bobsledYCoordPtr;
            bool shouldSpawn = !forceDespawn && Mod.SaveDataHandler.CustomSaveData.BobsledUnlocks[team];
            float newYCoord = shouldSpawn ? bobsledYCoords[index] : bobsledYCoords[index] - 1000f;
            *bobsledYCoordPtr = newYCoord;
            string msg = shouldSpawn ? "Spawning" : "Despawning";
            LoggingHandler.LogMessage($"{msg} Bobsled at 0x{(int)bobsledYCoordPtr:X} :: Old Y: {oldYCoord} New Y: {newYCoord}", taskName, LogLevel.Debug);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName,  LogLevel.Error);
        }
    }


    public static void CheckBobsledOnEnterLevel(string taskName)
    {
        //InitSetGen is too early moving to Set State In Game
        //Set State In Game is still too early moving to delayed task
        try
        {
            Team team = (Team)GameStateHandler.GetCurrentStory(taskName);
            LevelId levelId = (LevelId)GameStateHandler.GetCurrentLevel(taskName);

            if (team is Team.Dark && levelId is LevelId.SeasideHill)
            {
                SpawnOrUnSpawnBobsled(Team.Dark, levelId, true, 0, taskName);
                if (!Mod.SaveDataHandler.CustomSaveData.BobsledUnlocks[Team.Dark])
                {
                    SpawnOrUnSpawnBobsled(Team.Dark, levelId,false, 1, taskName);
                }
                
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    public static void HandleBobsledItemToSpawn(Team team, LevelId levelId, string taskName)
    {
        if (GameStateHandler.GetCurrentStory(taskName) != team)
            return;

        if (team is Team.Dark && levelId is LevelId.SeasideHill)
        {
            SpawnOrUnSpawnBobsled(team, levelId, false, 1, taskName);
        }
        
    }

    public static void UnlockBobsledItemCallback(Team? team, string taskName)
    {
        try
        {
            if (team is null)
            {
                foreach (var t in Enum.GetValues<Team>())
                {
                    UnlockBobsledItemCallback(t, taskName);
                }
            }
            else
            {
                Mod.SaveDataHandler.CustomSaveData.BobsledUnlocks[(Team)team] = !Mod.IsDebug || !Mod.SaveDataHandler.CustomSaveData.BobsledUnlocks[(Team)team];

                if (!GameStateHandler.InGame(taskName, true))
                    return;
                LevelId level = (LevelId)GameStateHandler.GetCurrentLevel(taskName);
                HandleBobsledItemToSpawn((Team)team, level, taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void UnlockStageObjItemCallback(StageObjTypes? stageObjTypes, Team? team, Region? region, string taskName, bool forceunlock = false)
    {
        if (stageObjTypes is null)
        {
            foreach (var s in StageObjData.StageObjsToMessWith)
            {
                UnlockStageObjItemCallback(s, team, region, taskName, forceunlock);
            }
        }
        else if (team is null)
        {
            foreach (var t in Enum.GetValues<Team>())
            {
                UnlockStageObjItemCallback(stageObjTypes, t, region, taskName, forceunlock);
            }
        }
        else if (region is null)
        {
            UnlockStageObjForTeamRegion((StageObjTypes)stageObjTypes, (Team)team, Region.SpecialStage, taskName, forceunlock);
            // foreach (var r in Enum.GetValues<Region>())
            // {
            //     UnlockStageObjItemCallback(stageObjTypes, team, r, taskName, forceunlock);
            // }
        }
        else
        {
            UnlockStageObjForTeamRegion((StageObjTypes)stageObjTypes, (Team)team, (Region)region, taskName, forceunlock);
        }
    }

    public static void UnlockStageObjForTeamRegion(StageObjTypes stageObjTypes, Team team, Region region,
        string taskName, bool forceunlock = false)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data is Null in UnlockStageObjForTeamRegion", taskName, LogLevel.Error);
            return;
        }
        
        var currState = IsStageObjUnlockedForTeamRegion(stageObjTypes, team, region, taskName);
        if (forceunlock || !Mod.IsDebug)
            currState = false;
        LoggingHandler.LogMessage($"StageObjItemReceived. Obj: {stageObjTypes} Team: {team} Region: {region} currState: {currState} newState: {!currState} forceunlock: {forceunlock}", taskName, LogLevel.SuperDebug);
        Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[team][stageObjTypes] = !currState;
        StageObjHandleChangingUnlockStatusSingle(stageObjTypes, team, region, taskName);
    }

    public static bool IsStageObjUnlockedForTeamRegion(StageObjTypes stageObjTypes, Team team, Region region, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data null in IsStageObjUnlocked", taskName, LogLevel.Error);
            return true;
        }
        return Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[team][stageObjTypes];
    }


    public static void StageObjHandleChangingUnlockStatusSingle(StageObjTypes stageObjTypes, Team team, Region? region, string taskName)
    {
        try
        {
            foreach (var spawnData in GetInLevelObjsOfType(stageObjTypes, taskName))
            {
                spawnData.SpawnOrDespawnObj(IsStageObjUnlockedForTeamRegion(stageObjTypes, team, region ?? Region.SpecialStage, taskName), taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void HandleObjSpawningWhenReceivingCharItem(Team team, FormationChar formationChar, bool unlock, string taskName)
    {
        if (!GameStateHandler.InGame(taskName,true))
            return;
        
        Team? teamInGame = GameStateHandler.GetCurrentStory(taskName);
        LevelId? level = GameStateHandler.GetCurrentLevel(taskName);
        Act? act = GameStateHandler.GetCurrentAct(taskName);

        if (teamInGame == null || level == null || act == null)
            return;
        if (team != teamInGame)
            return;
        
        try
        {
            if (formationChar is FormationChar.Speed && team is Team.Sonic)
            {
                //Final Fortress Sonic Respawn Self Destruct Switches When Getting Sonic
                if (level is LevelId.FinalFortress)
                {
                    //UnSpawn Self Destruct Switches if no Sonic
                    var stringValue = unlock ? "Respawning" : "Despawning";
                    var selfDestructSwitchItem = IsStageObjUnlockedForTeamRegion(StageObjTypes.SelfDestructSwitch, team, Region.Sky, taskName);

                    foreach (var selfDestructSwitch in GetInLevelObjsOfType(StageObjTypes.SelfDestructSwitch, taskName))
                    {
                        LoggingHandler.LogMessage($"Final Fortress Sonic {stringValue} SelfDestruct Switch at Address 0x{selfDestructSwitch.GetPtrToSpawnData(taskName):X}", taskName, LogLevel.Debug);
                        selfDestructSwitch.SpawnOrDespawnObj(unlock && selfDestructSwitchItem, taskName);
                    }
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }


    public static void HandleInitSetGenerator(string taskName)
    {
        //Obj Table is loaded into memory already
        //look into making changes to Objs like coords and the like
        BackupStageObjTable(taskName);

        Team? team = GameStateHandler.GetCurrentStory(taskName);
        LevelId? level = GameStateHandler.GetCurrentLevel(taskName);
        Act? act = GameStateHandler.GetCurrentAct(taskName);

        if (team == null || level == null || act == null)
        {
            LoggingHandler.LogMessage($"Null in HandleInitSetGenerator. team: {team} level: {level} act: {act}", taskName, LogLevel.Debug);
            return;
        }
        
        HandleStageObjs((Team)team, (LevelId)level, (Act)act, taskName);
    }


    public static void BackupStageObjTable(string taskName)
    {
        try
        {
            StageObjData.BackupStageObjSpawnData.Clear();
            
            UIntPtr currentObjPtr = StageObjData.StartOfStageObjTable;
            var numObjs = 0;

            while (true)
            {
                //find obj type (without mapping struct)
                //if good init obj (pass to factory) (factory should case switch type and init obj)
                //save copy of obj data to List/dict/w.e
                //change data in game mem if needed (unspawn/change coords/etc)

                StageObjSpawnData? spawnData = StageObjSpawnDataFactory.CreateSpawnData(currentObjPtr);

                if (spawnData == null)
                {
                    LoggingHandler.LogMessage($"Exiting Backup Stage Data. There are {numObjs} Stage Objs", taskName, LogLevel.Debug);
                    break;
                }

                if (numObjs >= 1000000)
                {
                    LoggingHandler.LogMessage("Exiting Backup Stage Data as there are somehow 1000000 stage objs. Please report this.", taskName, LogLevel.Error);
                    break;
                }
                numObjs++;
                currentObjPtr += 0x40;

                if (!StageObjData.BackupStageObjSpawnData.ContainsKey(spawnData.Type))
                {
                    StageObjData.BackupStageObjSpawnData[spawnData.Type] = [];
                }

                switch (spawnData.Type)
                {
                    case StageObjTypes.SingleSpring:
                        var singleSpring = (SingleSpringSpawnData)spawnData;
                        StageObjData.BackupStageObjSpawnData[spawnData.Type].Add(singleSpring);
                        break;

                    default:
                        StageObjData.BackupStageObjSpawnData[spawnData.Type].Add(spawnData);
                        break;
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    

    public static List<StageObjSpawnData> GetInLevelObjsOfType(StageObjTypes stageObjType, string taskName)
    {
        try
        {
            return StageObjData.BackupStageObjSpawnData.TryGetValue(stageObjType, out var objList) ?  objList : [];
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return [];
    }
    
    
    public static void HandleStageObjs(Team team, LevelId level, Act act, string taskName)
    {
        SpawnObjsBasedOnSaveDataForTeam(team, taskName);
        switch (team)
        {
            case Team.Sonic:
                HandleSonicStageObjs(level, act, taskName);
                break;
            case Team.Dark:
                HandleDarkStageObjs(level, act, taskName);
                break;
            case Team.Rose:
                HandleRoseStageObjs(level, act, taskName);
                break;
            case Team.Chaotix:
                HandleChaotixStageObjs(level, act, taskName);
                break;
            case Team.SuperHardMode:
                HandleSuperHardStageObjs(level, act, taskName);
                break;
            default:
                break;
        }
    }

    public static void SpawnObjsBasedOnSaveDataForTeam(Team team, string taskName)
    {
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom SaveData is null in SpawnObjsBasedOnSaveDataForTeam", taskName, LogLevel.Error);
            return;
        }
            
        foreach (var pair in Mod.SaveDataHandler.CustomSaveData.StageObjSpawnSaveData[team])
        {
            foreach (var objData in GetInLevelObjsOfType(pair.Key, taskName))
            {
                objData.SpawnOrDespawnObj(pair.Value, taskName);
            }
        }
    }

    public static void MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes stageObjType, Vector3 spawnPos,
        Vector3 offset, string taskName, string customDebugMsg = "")
    {
        try
        {
            foreach (var objSpawnData in GetInLevelObjsOfType(stageObjType, taskName))
            {
                if (Vector3.Distance(objSpawnData.GetOriginalSpawnPosition(taskName), spawnPos) < StageObjData.DistanceForMatchingStageObj)
                {
                    if (customDebugMsg != "")
                    {
                        LoggingHandler.LogMessage(customDebugMsg, taskName, LogLevel.Debug);
                    }
                    objSpawnData.SetSpawnPosition(spawnPos + offset, taskName);
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    //TODO handle Stage Obj Stuff for Level Gates
    public static void HandleSonicStageObjs(LevelId level, Act act, string taskName)
    {
        try
        {
            foreach (var cageObj in GetInLevelObjsOfType(StageObjTypes.CageBox, taskName))
            {
                cageObj.SpawnOrDespawnObj(false, taskName);
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

                    var despawnLaser = Mod.ArchipelagoHandler.SlotData.RemoveCasinoParkVIPTableLaserGate;
                    var spawnStr = !despawnLaser ? "Spawning" : "Despawning";
                    foreach (var objSpawnData in GetInLevelObjsOfType(StageObjTypes.LaserFence, taskName))
                    {
                        objSpawnData.SpawnOrDespawnObj(!despawnLaser, taskName);
                        LoggingHandler.LogMessage($"{spawnStr} Casino Park Sonic VIP Table Laser Gate", taskName, LogLevel.Debug);
                    }
                    break;
                }
                
                case LevelId.RailCanyon:
                {
                    //Rail Canyon Sonic
                    //A9151C
                    //change to 12620
                    //-55567.08f, 12762.00f, -20100.07f <- is normally here
                    //-55567.08f, 12620.00f, -20100.07f <- is moved to here to not have cage (and to be on ground)
                    Vector3 originalPos = new Vector3(-55567.08f, 12762.00f, -20100.07f);
                    Vector3 posOffset = new Vector3(0f, -142f, 0f);
                    MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes.BonusKey, originalPos, posOffset, taskName, $"Rail Canyon Sonic Bonus Key 3 moving down");
                    
                    break;
                }

                case LevelId.FrogForest:
                {
                    //Move Cage with BonusKey 1 Down in case it spawns in level (so it doesnt block getting Key)
                    //0, 1000, -5349.7f <- is normally here
                    //0, 800, -5349.7f <- is moved to here to not block Key 1
                    Vector3 originalPos = new Vector3(0f, 1000f, -5349.7f);
                    Vector3 posOffset = new Vector3(0f, -200f, 0f);
                    MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes.CageBox, originalPos, posOffset, taskName, $"Frog Forest Sonic Cage for Key 1 moving down");
                    break;
                }

                case LevelId.HangCastle:
                {
                    //10700.52f, -1595.80f, -13541.10f <- is normally here
                    //10700.52f, -1755f, -13541.10f <- is moved to here to not have cage
                    Vector3 originalPos = new Vector3(10700.52f, -1595.80f, -13541.10f);
                    Vector3 posOffset = new Vector3(0f, -159.2f, 0f);
                    MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes.BonusKey, originalPos, posOffset, taskName, $"Hang Castle Sonic Bonus Key 3 moving down");
                    break;
                }

                case LevelId.MysticMansion:
                {
                    //Mystic Mansion Sonic
                    //A8A8D8 (coords)
                    //15420.056f, -8739.9f, -39680.32f
                    //change to 15420.056f, -8878f, -39730f
                    Vector3 originalPos = new Vector3(15420.056f, -8739.9f, -39680.32f);
                    Vector3 posOffset = new Vector3(0f, -138.1f, -49.68f);
                    MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes.BonusKey, originalPos, posOffset, taskName, $"Mystic Mansion Sonic Bonus Key 3 moving down");
                    break;
                }
                
                case LevelId.FinalFortress:
                {
                    //Final Fortress Sonic
                    //A8945C
                    //change to 5400 (y)
                    //2250.01f, 5552.00f, 33690.04f <- is normally here
                    //2250.01f, 5400.00f, 33690.04f <- is moved to here to not have cage
                    Vector3 originalPos = new Vector3(2250.01f, 5552.00f, 33690.04f);
                    Vector3 posOffset = new Vector3(0f, -152f, 0f);
                    MoveSpawnPosOfMatchingStageObjsByOffset(StageObjTypes.BonusKey, originalPos, posOffset, taskName, $"Final Fortress Sonic Bonus Key 2 moving down");
                    
                    
                    //UnSpawn Self Destruct Switches if no Sonic
                    var hasSpeedChar = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[Team.Sonic].CharsUnlocked[FormationChar.Speed];
                   
                    var selfDestructSwitchItem = IsStageObjUnlockedForTeamRegion(StageObjTypes.SelfDestructSwitch, Team.Sonic, Region.Sky, taskName);
                    var stringValue = hasSpeedChar && selfDestructSwitchItem ? "Respawning" : "Despawning";
                    
                    foreach (var selfDestructSwitch in GetInLevelObjsOfType(StageObjTypes.SelfDestructSwitch, taskName))
                    {
                        LoggingHandler.LogMessage($"Final Fortress Sonic {stringValue} SelfDestruct Switch at Address 0x{selfDestructSwitch.GetPtrToSpawnData(taskName):X}", taskName, LogLevel.Debug);
                        selfDestructSwitch.SpawnOrDespawnObj(hasSpeedChar && selfDestructSwitchItem, taskName);
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
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static void HandleDarkStageObjs(LevelId level, Act act, string taskName)
    {
        try
        {
            foreach (var cageObj in GetInLevelObjsOfType(StageObjTypes.CageBox, taskName))
            {
                cageObj.SpawnOrDespawnObj(false, taskName);
            }

            switch (level)
            {
                
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static void HandleRoseStageObjs(LevelId level, Act act, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static void HandleChaotixStageObjs(LevelId level, Act act, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
    public static void HandleSuperHardStageObjs(LevelId level, Act act, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
    }
    
}