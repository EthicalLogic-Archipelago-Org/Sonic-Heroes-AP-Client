

using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;

namespace Sonic_Heroes_AP_Client.Sanity;

public static class ObjSanityHandler
{
    public static int[] DarkChecksCompleted = new int[14];
    public static int[] RoseChecksCompleted = new int[14];
    public static int[] ChaotixChecksCompleted = new int[28];
    
    
    public static void CheckRingSanity(int newCount)
    {
        try
        {
            if (!GameStateHandler.InGame())
                return;
            var storyId = GameStateHandler.GetCurrentStory();

            if (storyId != Team.Rose && storyId != Team.Chaotix || !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)storyId, SanityType.ObjSanity)!)
                return;
        
            var levelId = GameStateHandler.GetCurrentLevel();
            if (!Enum.IsDefined(typeof(LevelId), levelId) || (int)levelId > 15)
                return;
            var act = GameStateHandler.GetCurrentAct();
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
            Console.WriteLine(e);
        }
    }

    
    public static void HandleCountIncreased(int newCount)
    {
        try
        {
            if (!GameStateHandler.InGame())
                return;
            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity)!)
                return;
            var storyId = GameStateHandler.GetCurrentStory();
            if (storyId != Team.Chaotix)
                return;
            var levelId = GameStateHandler.GetCurrentLevel();
            var act = GameStateHandler.GetCurrentAct();
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
            Console.WriteLine(e);
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
        //Console.WriteLine($"Check Index {newCount}");
        //Console.WriteLine((levelOffset + newCount).ToString("X"));
        Mod.ArchipelagoHandler.CheckLocation(levelOffset + newCount);
    }

    
    public static void CheckEnemyCount(int newCount)
    {
        try
        {
            if (!GameStateHandler.InGame())
                return;
            var storyId = GameStateHandler.GetCurrentStory();
            if (storyId != Team.Dark && storyId != Team.Chaotix || !(bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)storyId, SanityType.ObjSanity)!)
                return;
            var levelId = GameStateHandler.GetCurrentLevel();
            if (!Enum.IsDefined(typeof(LevelId), levelId!) || (int)levelId > 15)
                return;
            var act = GameStateHandler.GetCurrentAct();
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

            var checkSize = storyId == Team.Dark
                ? Mod.ArchipelagoHandler.SlotData.DarksanityCheckSize
                : 1;
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
            Console.WriteLine(e);
        }
    }

    
    public static void HandleBSCapsuleCountIncreased(int newCount)
    {
        if (!GameStateHandler.InGame())
            return;
        if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity)!)
            return;
        var storyId = GameStateHandler.GetCurrentStory();
        if (storyId != Team.Chaotix)
            return;
        var levelId = GameStateHandler.GetCurrentLevel();
        var act = GameStateHandler.GetCurrentAct();
        if (levelId != LevelId.BulletStation)
            return; 
        HandleChaotixsanity(act == Act.Act1 ? 0x1561 : 0x157F, 
            act == Act.Act1 ? 30 : 50 , newCount, (LevelId)levelId, (Act)act!);
    }

    
    public static void HandleGoldBeetleCountIncreased(int newCount)
    {
        if (!GameStateHandler.InGame())
            return;
        if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Chaotix, SanityType.ObjSanity)!)
            return;
        var storyId = GameStateHandler.GetCurrentStory();
        if (storyId != Team.Chaotix)
            return;
        var levelId = GameStateHandler.GetCurrentLevel();
        var act = GameStateHandler.GetCurrentAct();
        if (levelId != LevelId.PowerPlant)
            return; 
        HandleChaotixsanity(act == Act.Act1 ? 0x127F : 0x1282, 
            act == Act.Act1 ? 3 : 5, newCount, (LevelId)levelId, (Act)act!);
    }
    
}