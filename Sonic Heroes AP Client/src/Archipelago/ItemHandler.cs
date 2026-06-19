

using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Archipelago.MultiClient.Net.Models;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sound;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Archipelago;

/// <summary>
/// Enum of the Id's of Filler Items (Filler Only as Entire Item List is too large)
/// </summary>
public enum FillerSHItem
{
    ExtraLife = 0x8000,
    FiveRings,
    TenRings,
    TwentyRings,
    Shield,
    Invincibility,
    SpeedLevelUp,
    PowerLevelUp,
    FlyLevelUp,
    TeamLevelUp,
    TeamBlastFiller,
    
    StealthTrap = 0x8100,
    FreezeTrap,
    NoSwapTrap,
    RingTrap,
    CharmyTrap,
}

public static class ItemHandler
{
    public static readonly ConcurrentQueue<Tuple<int, ItemInfo>> ReceivedItems = new();
    private static readonly ConcurrentQueue<FillerSHItem> CachedInGameItems = new();
    
    
    public static void QueueItem(int index, ItemInfo item)
    {
        ReceivedItems.Enqueue(Tuple.Create(index, item));
    }
    
    public static void HandleItem(int index, ItemInfo item, string taskName)
    {
        try
        {
            if (Mod.SaveDataHandler.CustomSaveData == null)
            {
                LoggingHandler.LogMessage($"Custom Save Data is Null in Handle Item", taskName, LogLevel.Error);
                return;
            }
            
            if (index < Mod.SaveDataHandler.CustomSaveData.LastItemIndex)
            {
                LoggingHandler.LogMessage($"Item #{index}: {item.ItemName} dropped due to index being lower than Save Data index: {Mod.SaveDataHandler.CustomSaveData.LastItemIndex}", taskName, LogLevel.SuperDebug);
                return;
            }
            
            LoggingHandler.LogMessage($"Handle Item Start", taskName, LogLevel.SuperDebug);
                    
            
            var handled = false;
            
            var itemName = item.ItemName;
            
            LoggingHandler.LogMessage($"Handle Item Start : Name: {itemName} Index: {Mod.SaveDataHandler.CustomSaveData.LastItemIndex}", taskName, LogLevel.SuperDebug);
            Mod.SaveDataHandler.CustomSaveData.LastItemIndex++;
            
            

            if (itemName == null)
            {
                LoggingHandler.LogMessage($"ItemName is null for some reason", taskName, LogLevel.Error);
                return;
            }
            
            //check for items here
            LoggingHandler.LogMessage($"Handle Item Before Playable Char: {handled}", taskName, LogLevel.SuperDebug);
            CheckPlayableCharItemName(itemName, ref handled, taskName);
            LoggingHandler.LogMessage($"Handle Item Before Emerald: {handled}", taskName, LogLevel.SuperDebug);
            CheckEmeraldItemName(itemName, ref handled, taskName);
            LoggingHandler.LogMessage($"Handle Item Before Emblem: {handled}", taskName, LogLevel.SuperDebug);
            CheckEmblemItemName(itemName, ref handled, taskName);
            LoggingHandler.LogMessage($"Handle Item Before Ability: {handled}", taskName, LogLevel.SuperDebug);
            CheckAbilityItemName(itemName, ref handled, taskName);
            LoggingHandler.LogMessage($"Handle Item Before Spawn Position: {handled}", taskName, LogLevel.SuperDebug);
            CheckSpawnPositionItemName(itemName, ref handled, taskName);
            LoggingHandler.LogMessage($"Handle Item Before StageObj: {handled}", taskName, LogLevel.SuperDebug);
            CheckStageObjItemName(itemName, ref handled, taskName);
            
            
            if (handled)
            {
                LoggingHandler.LogMessage($"Item Handled in HandleItem", taskName, LogLevel.SuperDebug);
                Mod.LevelSelectManager.RecalculateOpenLevels(taskName: taskName);
                Mod.ArchipelagoHandler.Save(taskName);
                return;
            }

            if (item.ItemId - SonicHeroesDefinitions.AllIdsStartOffset < (long)FillerSHItem.ExtraLife)
            {
                LoggingHandler.LogMessage($"Item not handled but ID is not in Filler items. \nHOW DID WE GET HERE? Item: {itemName}", taskName, LogLevel.Error);
                return;
            }
                
            
            //have filler item here
            if (!GameStateHandler.InGame(taskName))
            {
                LoggingHandler.LogMessage($"Enqueuing Item: {itemName} to CachedInGameItems", taskName, LogLevel.SuperDebug);
                CachedInGameItems.Enqueue((FillerSHItem)(item.ItemId - SonicHeroesDefinitions.AllIdsStartOffset));
                return;
            }
            
            LoggingHandler.LogMessage($"Handling InGame Item Directly {itemName}", taskName, LogLevel.SuperDebug);
            HandleInGameItem((FillerSHItem)(item.ItemId - SonicHeroesDefinitions.AllIdsStartOffset), taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static void HandleCachedItems(string taskName)
    {
        while (!CachedInGameItems.IsEmpty)
        {
            if (CachedInGameItems.TryDequeue(out var item))
                HandleInGameItem(item, taskName);
        }
    }
    
    public static unsafe void HandleInGameItem(FillerSHItem itemId, string taskName)
    {
        switch (itemId)
        {
            case FillerSHItem.ExtraLife:
                LoggingHandler.LogMessage($"Handling Extra Life: {itemId}", taskName, LogLevel.SuperDebug);
                GameStateGameWrites.ModifyLives((int)Mod.ModuleBase, 1, taskName);
                try
                {
                    Mod.SaveDataHandler.SaveData->savedLives++;
                }
                catch (Exception e)
                {
                    LoggingHandler.LogMessage($"Error Handling Extra Life: {e}", taskName, LogLevel.Error);
                }
                
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1034, taskName);
                break;
            case FillerSHItem.FiveRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount(taskName) + 5, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033, taskName);
                break;
            case FillerSHItem.TenRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount(taskName) + 10, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033, taskName);
                break;
            case FillerSHItem.TwentyRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount(taskName) + 20, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033, taskName);
                break;
            case FillerSHItem.Shield:
                GameStateGameWrites.GiveShield((int)Mod.ModuleBase, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1036, taskName);
                break;
            case FillerSHItem.Invincibility:
                //not implemented yet
                break;
            case FillerSHItem.SpeedLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Speed, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005, taskName);
                break;
            case FillerSHItem.PowerLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Power, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005, taskName);
                break;
            case FillerSHItem.FlyLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Flying, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005, taskName);
                break;
            case FillerSHItem.TeamLevelUp: 
                ItemGameWrites.GiveLevelUp(LevelUpType.Speed, taskName);
                ItemGameWrites.GiveLevelUp(LevelUpType.Power, taskName);
                ItemGameWrites.GiveLevelUp(LevelUpType.Flying, taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005, taskName);
                break;
            case FillerSHItem.TeamBlastFiller:
                try
                {
                    ItemGameWrites.HandleTeamBlastFiller(taskName);
                    var team = GameStateHandler.GetCurrentStory(taskName);
                    var level = GameStateHandler.GetCurrentLevel(taskName);

                    if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue((LevelId)level, out Region region))
                        break;

                    if (!AbilityCharacterManager.CanTeamBlast((Team)team, region, taskName))
                    {
                        GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount(taskName) + 10, taskName);
                        if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                            SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033, taskName);
                        break;
                    }
                    GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount(taskName) + 1, taskName);
                    if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1004, taskName);
                }
                catch (Exception e)
                {
                    LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
                }
                break;
            case FillerSHItem.StealthTrap:
                TrapHandler.HandleStealthTrap(taskName);
                break;
            case FillerSHItem.FreezeTrap:
                TrapHandler.HandleFreezeTrap(taskName);
                break;
            case FillerSHItem.NoSwapTrap:
                TrapHandler.HandleNoSwapTrap(taskName);
                break;
            case FillerSHItem.RingTrap:
                GameStateGameWrites.SetRingCount(Math.Max(0, GameStateGameWrites.GetRingCount(taskName) - 50), taskName);
                if (Mod.Configuration != null && Mod.Configuration.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1005, taskName);
                RingLinkHandler.SendRingPacket(-50, taskName);
                break;
            case FillerSHItem.CharmyTrap:
                TrapHandler.HandleCharmyTrap(taskName);
                break;
            default:
                break;
        }
    }
    
    public static void CheckPlayableCharItemName(string itemName, ref bool handled, string taskName)
    {
        var character = Enum.GetValues<PlayableCharacter>().Cast<PlayableCharacter?>().FirstOrDefault(x =>
            itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}", StringComparison.InvariantCultureIgnoreCase));
        if (character == null) 
            return;
        //match here
        LoggingHandler.LogMessage($"Got Playable Char Item: {itemName}", taskName, LogLevel.APAction);
        var team = SonicHeroesDefinitions.PlayableCharToTeam[(PlayableCharacter)character];
        var formation = SonicHeroesDefinitions.PlayableCharToFormation[(PlayableCharacter)character];
        var unlocked = AbilityCharacterManager.GetCharUnlock(team, formation, taskName);
        if (!Mod.IsDebug)
            unlocked = false;
        AbilityCharacterManager.SetCharUnlock(team, formation, !unlocked, taskName);
        handled = true;
    }

    public static unsafe void CheckEmeraldItemName(string itemName, ref bool handled, string taskName)
    {
        if (handled)
            return;
        if (Mod.SaveDataHandler.CustomSaveData == null)
        {
            LoggingHandler.LogMessage($"Custom Save Data is Null in Check Emerald Item", taskName, LogLevel.Error);
            return;
        }
        Emerald? emerald = Enum.GetValues<Emerald>().Cast<Emerald?>().FirstOrDefault(x =>
            itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}ChaosEmerald", StringComparison.InvariantCultureIgnoreCase));
        if (emerald == null) 
            return;
        LoggingHandler.LogMessage($"Got Emerald: {itemName}", taskName, LogLevel.APAction);
        Mod.SaveDataHandler.CustomSaveData.Emeralds[(Emerald)emerald] = true;
        Mod.SaveDataHandler.RedirectData->Emerald[((int)emerald + 1) * 3] = 1;
        handled = true;
    }

    public static void CheckEmblemItemName(string itemName, ref bool handled, string taskName)
    {
        try
        {
            if (handled)
                return;
            if (!itemName.Contains("Emblem"))
                return;
            if (Mod.SaveDataHandler.CustomSaveData == null)
            {
                LoggingHandler.LogMessage($"Custom Save Data is Null in Check Emblem Item", taskName, LogLevel.Error);
                return;
            }
            if (Mod.Configuration == null)
            {
                LoggingHandler.LogMessage($"Mod Configuration is Null in Handle Item", taskName, LogLevel.Error);
                return;
            }
            LoggingHandler.LogMessage($"Got Emblem: {itemName}", taskName, LogLevel.APAction);
            Mod.SaveDataHandler.CustomSaveData.Emblems++;
            if (Mod.Configuration.PlaySounds)
            {
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE016, taskName);
            }
                
            handled = true;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.APAction);
        }
    }

    public static void CheckAbilityItemName(string itemName, ref bool handled, string taskName)
    {
        if (handled)
            return;
        Team? team;
        Region? region;
        if (itemName.Contains("All Abilities", StringComparison.InvariantCultureIgnoreCase))
        {
            team = CheckTeamItemName(itemName, taskName);
            region = CheckRegionItemName(itemName, taskName);
            AbilityCharacterManager.UnlockAbilityItemCallback(null, team, region, taskName);
            LoggingHandler.LogMessage($"Got Item: {itemName}", taskName, LogLevel.APAction);
            handled = true;
            return;
        }
        
        Ability? ability = Enum.GetValues<Ability>().Cast<Ability?>().LastOrDefault(x =>
            itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}", StringComparison.InvariantCultureIgnoreCase));
        if (ability == null)
            return;
        team = CheckTeamItemName(itemName, taskName);
        region = CheckRegionItemName(itemName, taskName);
        AbilityCharacterManager.UnlockAbilityItemCallback(ability, team, region, taskName);
        LoggingHandler.LogMessage($"Got Item: {itemName}", taskName, LogLevel.APAction);
        handled = true;
    }

    public static void CheckStageObjItemName(string itemName, ref bool handled, string taskName)
    {
        if (handled)
            return;
        Team? team;
        Region? region;
        if (itemName.Contains("All Stage Objects", StringComparison.InvariantCultureIgnoreCase))
        {
            LoggingHandler.LogMessage($"Got Item: {itemName}", taskName, LogLevel.APAction);
            team = CheckTeamItemName(itemName, taskName);
            region = CheckRegionItemName(itemName, taskName);
            StageObjHandler.UnlockStageObjItemCallback(null, team, region, taskName);
            handled = true;
            return;
        }
        StageObjTypes? stageObj = StageObjData.StageObjsToMessWith.Cast<StageObjTypes?>().LastOrDefault(x =>itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}", StringComparison.InvariantCultureIgnoreCase));
        if (stageObj == null)
            return;
        LoggingHandler.LogMessage($"Got Item: {itemName}", taskName, LogLevel.APAction);
        team = CheckTeamItemName(itemName, taskName);
        region = CheckRegionItemName(itemName, taskName);
        StageObjHandler.UnlockStageObjItemCallback(stageObj, team, region, taskName);
        handled = true;
    }
    
    public static void CheckSpawnPositionItemName(string itemName, ref bool handled, string taskName)
    {
        try
        {
            if (handled)
                return;
            Team? team;
            LevelId? levelId;
            
            if (itemName.Contains("Spawn Position", StringComparison.InvariantCultureIgnoreCase))
            {
                team = CheckTeamItemName(itemName, taskName);
                levelId = CheckLevelIdItemName(itemName, taskName);
                if (team == null || levelId == null)
                    return;
                
                if (itemName.Contains("Start of Level", StringComparison.InvariantCultureIgnoreCase))
                {
                    LoggingHandler.LogMessage($"Unlocking Start of Level Spawn for: {levelId} {team}", taskName, LogLevel.Debug);
                    LevelSpawnUnlockHandler.UnlockSpecificSpawnData((Team)team, (LevelId)levelId, 0, taskName);
                    LoggingHandler.LogMessage($"Got Item: {itemName}", taskName, LogLevel.APAction);
                    handled = true;
                    return;
                }

                if (itemName.Contains("Checkpoint", StringComparison.InvariantCultureIgnoreCase))
                {
                    var checkpointNumber = int.Parse(Regex.Matches(itemName, @"-?\d+").Last().Value);
                    LoggingHandler.LogMessage($"Unlocking Checkpoint {checkpointNumber} Spawn for: {levelId} {team}", taskName, LogLevel.Debug);
                    LevelSpawnUnlockHandler.UnlockSpecificSpawnData((Team)team, (LevelId)levelId, checkpointNumber, taskName);
                    handled = true;
                    return;
                }
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    public static Team? CheckTeamItemName(string itemName, string taskName)
    {
        try
        {
            return Enum.GetValues<Team>().Cast<Team?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return null;
    }
    
    public static LevelId? CheckLevelIdItemName(string itemName, string taskName)
    {
        try
        {
            return Enum.GetValues<LevelId>().Cast<LevelId?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return null;
    }
    
    public static Region? CheckRegionItemName(string itemName, string taskName)
    {
        try
        {
            return Enum.GetValues<Region>().Cast<Region?>().LastOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString() ?? SonicHeroesDefinitions.PleaseDontContainMe}Region", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return null;
    }
}