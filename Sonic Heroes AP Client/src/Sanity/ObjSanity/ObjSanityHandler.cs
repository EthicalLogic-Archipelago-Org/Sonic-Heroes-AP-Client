
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.ObjSanity;

public static class ObjSanityHandler
{
    public static int[] DarkChecksCompleted = new int[14];
    public static int[] RoseChecksCompleted = new int[14];
    public static int[] ChaotixChecksCompleted = new int[28];
    

    public static unsafe void HandleObjSanityOnEnterLevel(string taskName)
    {
        try
        {
            var team = GameStateHandler.GetCurrentStory(taskName);

            if (team == null)
            {
                LoggingHandler.LogMessage($"Team is null in HandleObjSanityOnEnterLevel", taskName, LogLevel.Error);
                return;
            }

            switch (team)
            {
                case Team.Dark:
                    HandleDarkObjSanityOnEnterLevel(taskName);
                    break;
                
                case Team.Rose:
                    //Handle Rose
                    break;
                
                case Team.Chaotix: 
                    //Handle Chaotix
                    break;
                
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    

    private static unsafe void SetTObjTeamObjectiveCount(int newCount, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    private static unsafe int GetTEnemyScoreManagerEnemyKilledCount(string taskName)
    {
        try
        {
            UIntPtr TEnemyScoreManagerPtr = Mod.ModuleBase + 0x678C60;
            UIntPtr enemyKilledCountPtr = (UIntPtr)(*(int*)TEnemyScoreManagerPtr + 0x50);
            LoggingHandler.LogMessage($"Getting TEnemyScoreManagerEnemyKilledCount:: {*(int*)enemyKilledCountPtr} Address is 0x{enemyKilledCountPtr:X}", taskName, LogLevel.Debug);
            return *(int*)enemyKilledCountPtr;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return 0;
    }
    
    
    private static unsafe void SetTEnemyScoreManagerEnemyKilledCount(int newCount, string taskName)
    {
        try
        {
            UIntPtr TEnemyScoreManagerPtr = Mod.ModuleBase + 0x678C60;
            UIntPtr enemyKilledCountPtr = (UIntPtr)(*(int*)TEnemyScoreManagerPtr + 0x50);
            LoggingHandler.LogMessage($"Setting TEnemyScoreManagerEnemyKilledCount to {newCount} Address is 0x{enemyKilledCountPtr:X}", taskName, LogLevel.Debug);
            *(int*)enemyKilledCountPtr = newCount;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    private static void HandleDarkObjSanityOnEnterLevel(string taskName)
    {
        try
        {
            var levelId = GameStateHandler.GetCurrentLevel(taskName);
            var act  = GameStateHandler.GetCurrentAct(taskName);

            if (levelId == null)
            {
                LoggingHandler.LogMessage($"LevelId null in HandleDarkObjSanityOnEnterLevel", taskName, LogLevel.Error);
                return;
            }
            
            if (levelId is < LevelId.SeasideHill or > LevelId.FinalFortress)
            {
                LoggingHandler.LogMessage($"LevelId not Regular Stage in HandleDarkObjSanityOnEnterLevel :: LevelId: {levelId}", taskName, LogLevel.Debug);
                return;
            }

            if (act is not Act.Act2)
            {
                LoggingHandler.LogMessage($"Act not Act 2 in HandleDarkObjSanityOnEnterLevel", taskName, LogLevel.Debug);
                return;
            }

            int currentAmount = Mod.SaveDataHandler.CustomSaveData.DarkObjSanityEnemyKills[(LevelId)levelId].Count(x => x);
            currentAmount = Math.Min(currentAmount, 90);
            SetTEnemyScoreManagerEnemyKilledCount(currentAmount, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    private static unsafe void HandleRoseObjSanityOnEnterLevel(int newCount, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }



    public static void HandleEnemyKilledObjSanity(EnemyData.BaseEnemyData enemyData, Act act, string taskName)
    {
        try
        {
            if (enemyData.Team is not Team.Dark && enemyData.Team is not Team.Chaotix)
            {
                return;
            }
            if (enemyData.Team is Team.Dark && !(act is Act.Act2 && Mod.LevelSelectManager.IsThisTeamActEnabled(enemyData.Team, act, taskName) && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(enemyData.Team, SanityType.ObjSanity, taskName)))
            {
                LoggingHandler.LogMessage($"Team Dark but no ObjSanity in HandleEnemyKilledObjSanity", taskName,  LogLevel.Debug);
                return;
            }
            if (enemyData.Team is Team.Chaotix && !(Mod.LevelSelectManager.IsThisTeamActEnabled(enemyData.Team, act, taskName) && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(enemyData.Team, SanityType.ObjSanity, taskName)))
            {
                LoggingHandler.LogMessage($"Team Chaotix but no ObjSanity in HandleEnemyKilledObjSanity", taskName,  LogLevel.Debug); 
                return;
            }

            switch (enemyData.Team)
            {
                case Team.Dark:
                    HandleDarkEnemyKilledObjSanity(enemyData, taskName);
                    break;
                case Team.Chaotix:
                    break;
                default:
                    return;
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }


    public static void HandleDarkEnemyKilledObjSanity(EnemyData.BaseEnemyData enemySanityData, string taskName)
    {
        try
        {
            var allEnemiesInLevel = EnemyData.AllEnemies.Where(data => data.Team == enemySanityData.Team && data.LevelId == enemySanityData.LevelId).ToList();

            var enemyIndex = allEnemiesInLevel.IndexOf(enemySanityData);

            if (enemyIndex < 0)
            {
                LoggingHandler.LogMessage($"Enemy was not found in EnemySanity Data for HandleEnemyKilledObjSanity", taskName, LogLevel.Debug); 
                return;
            }

            if (!Mod.SaveDataHandler.CustomSaveData.DarkObjSanityEnemyKills[enemySanityData.LevelId][enemyIndex])
            {
                LoggingHandler.LogMessage($"Team Dark Level: {enemySanityData.LevelId} Enemy #{enemyIndex + 1} has not been killed yet.", taskName, LogLevel.Debug);

                Mod.SaveDataHandler.CustomSaveData.DarkObjSanityEnemyKills[enemySanityData.LevelId][enemyIndex] = true;
                Mod.ArchipelagoHandler.Save(taskName);

            }
            else
            {
                var enemyCounter = GetTEnemyScoreManagerEnemyKilledCount(taskName);
                LoggingHandler.LogMessage($"Team Dark Level: {enemySanityData.LevelId} Enemy #{enemyIndex + 1} has been killed already.", taskName, LogLevel.Debug);
                SetTEnemyScoreManagerEnemyKilledCount(enemyCounter - 1, taskName);
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    public static void CheckRingSanity(int newCount, string taskName)
    {
        try
        {
            if (!GameStateHandler.InGame(taskName))
                return;
            var storyId = GameStateHandler.GetCurrentStory(taskName);

            if (storyId != Team.Rose && storyId != Team.Chaotix || !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)storyId, SanityType.ObjSanity, taskName)!)
                return;
        
            var levelId = GameStateHandler.GetCurrentLevel(taskName);
            if (!Enum.IsDefined(typeof(LevelId), levelId) || (int)levelId > 15)
                return;
            var act = GameStateHandler.GetCurrentAct(taskName);
            if (storyId == Team.Rose && act != Act.Act2
                || storyId != Team.Rose && (storyId != Team.Chaotix || levelId != LevelId.CasinoPark))
                return;

            var maxRingCheck = storyId == Team.Rose || act == Act.Act1 ? 200 : 500;
            if (newCount > maxRingCheck)
                newCount = maxRingCheck;
            int previousCount;
            if (storyId == Team.Rose)
            {
                previousCount = RoseChecksCompleted[(int)levelId - 2];
                if (previousCount >= newCount)
                    return;
                RoseChecksCompleted[(int)levelId - 2] = newCount;
            }
            else
            {
                previousCount = ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act!)];
                if (previousCount >= newCount)
                    return;
                ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act)] = newCount;
            }
            var checkSize = storyId == Team.Rose
                ? Mod.ArchipelagoHandler.SlotData.RosesanityCheckSize
                : Mod.ArchipelagoHandler.SlotData.ChaotixsanityRingCheckSize;
            var levelOffset = ((int)levelId - 2) * 200;
            if (storyId == Team.Chaotix && levelId == LevelId.CasinoPark)
                levelOffset = act == Act.Act1 ? 0xBC0 : 0xC88;
            for (var i = previousCount + 1; i <= newCount; i++)
                if (i % checkSize == 0)
                    Mod.ArchipelagoHandler.CheckLocation(SonicHeroesDefinitions.RoseObjSanityStartId - 1 + levelOffset + i);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    
    public static void HandleCountIncreased(int newCount, string taskName)
    {
        try
        {
            if (!GameStateHandler.InGame(taskName))
                return;
            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity, taskName)!)
                return;
            var storyId = GameStateHandler.GetCurrentStory(taskName);
            if (storyId != Team.Chaotix)
                return;
            var levelId = GameStateHandler.GetCurrentLevel(taskName);
            var act = GameStateHandler.GetCurrentAct(taskName);
            switch (levelId)
            {
                case LevelId.SeasideHill:
                    HandleChaotixsanity(act == Act.Act1 ? 0x11B7 : 0x11C1, 
                        act == Act.Act1 ? 10 : 20 , newCount, (LevelId)levelId!, (Act)act!);
                    break;
                case LevelId.BingoHighway:
                    HandleChaotixsanity(act == Act.Act1 ? 0x1543 : 0x154D, 
                        act == Act.Act1 ? 10 : 20 , newCount, (LevelId)levelId!, (Act)act!);
                    break;
                case LevelId.LostJungle:
                    HandleChaotixsanity(act == Act.Act1 ? 0x15B1 : 0x15BB, 
                        act == Act.Act1 ? 10 : 20, newCount, (LevelId)levelId!, (Act)act!);
                    break;
                case LevelId.HangCastle:
                    HandleChaotixsanity(act == Act.Act1 ? 0x15CF : 0x15D9, 
                        10, newCount, (LevelId)levelId!, (Act)act!);
                    break;
                case LevelId.MysticMansion:
                    HandleChaotixsanity(act == Act.Act1 ? 0x15E3 : 0x161F, 
                        act == Act.Act1 ? 60 : 46, newCount, (LevelId)levelId!, (Act)act!);
                    break;
                case LevelId.FinalFortress:
                    HandleChaotixsanity(act == Act.Act1 ? 0x164D : 0x1652, 
                        act == Act.Act1 ? 5 : 10, newCount, (LevelId)levelId!, (Act)act!);
                    break;
                default:
                    return;
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    
    public static void HandleChaotixsanity(int levelOffset, int maxCount, int newCount, LevelId levelId, Act act)
    {
        if (ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act)] < newCount)
            ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act)] = newCount;
        else
            return;
        if (newCount > maxCount)
            return;
        //$"Check Index {newCount}"
        //(levelOffset + newCount).ToString("X")
        Mod.ArchipelagoHandler.CheckLocation(levelOffset + newCount);
    }

    
    public static void CheckEnemyCount(int newCount, string taskName)
    {
        try
        {
            if (!GameStateHandler.InGame(taskName))
                return;
            var storyId = GameStateHandler.GetCurrentStory(taskName);
            if (storyId != Team.Dark && storyId != Team.Chaotix || !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)storyId, SanityType.ObjSanity, taskName)!)
                return;
            var levelId = GameStateHandler.GetCurrentLevel(taskName);
            if (!Enum.IsDefined(typeof(LevelId), levelId!) || (int)levelId > 15)
                return;
            var act = GameStateHandler.GetCurrentAct(taskName);
            if ((storyId != Team.Dark && storyId != Team.Chaotix) ||
                (storyId == Team.Dark && act != Act.Act2) ||
                (storyId == Team.Chaotix && levelId != LevelId.GrandMetropolis))
                return;
            var maxEnemyCheck = storyId == Team.Dark ? 100 : 85;
            if (newCount > maxEnemyCheck)
                newCount = maxEnemyCheck;

            int previousCount;
            if (storyId == Team.Dark)
            {
                previousCount = DarkChecksCompleted[(int)levelId - 2];
                if (previousCount >= newCount)
                    return;
                DarkChecksCompleted[(int)levelId - 2] = newCount;
            }
            else
            {
                previousCount = ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act)];
                if (previousCount >= newCount)
                    return;
                ChaotixChecksCompleted[((int)levelId - 2) + (14 * (int)act)] = newCount;
            }

            var checkSize = storyId == Team.Dark ? Mod.ArchipelagoHandler.SlotData.DarksanityCheckSize : 1;
            var levelOffset = ((int)levelId - 2) * 100;
            if (storyId == Team.Chaotix && levelId == LevelId.GrandMetropolis)
                levelOffset = act == Act.Act1 ? 0x1086 : 0x10DB;

            // Loop through all enemy counts that were skipped (or reached in succession)
            for (var i = previousCount + 1; i <= newCount; i++)
                if (i % checkSize == 0)
                    Mod.ArchipelagoHandler.CheckLocation(0x14F + levelOffset + i);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    
    public static void HandleBSCapsuleCountIncreased(int newCount, string taskName)
    {
        if (!GameStateHandler.InGame(taskName))
            return;
        if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity, taskName)!)
            return;
        var storyId = GameStateHandler.GetCurrentStory(taskName);
        if (storyId != Team.Chaotix)
            return;
        var levelId = GameStateHandler.GetCurrentLevel(taskName);
        var act = GameStateHandler.GetCurrentAct(taskName);
        if (levelId != LevelId.BulletStation)
            return; 
        HandleChaotixsanity(act == Act.Act1 ? 0x1561 : 0x157F, 
            act == Act.Act1 ? 30 : 50 , newCount, (LevelId)levelId, (Act)act!);
    }

    
    public static void HandleGoldBeetleCountIncreased(int newCount, string taskName)
    {
        if (!GameStateHandler.InGame(taskName))
            return;
        if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity, taskName)!)
            return;
        var storyId = GameStateHandler.GetCurrentStory(taskName);
        if (storyId != Team.Chaotix)
            return;
        var levelId = GameStateHandler.GetCurrentLevel(taskName);
        var act = GameStateHandler.GetCurrentAct(taskName);
        if (levelId != LevelId.PowerPlant)
            return; 
        HandleChaotixsanity(act == Act.Act1 ? 0x127F : 0x1282, 
            act == Act.Act1 ? 3 : 5, newCount, (LevelId)levelId, (Act)act!);
    }
    
}