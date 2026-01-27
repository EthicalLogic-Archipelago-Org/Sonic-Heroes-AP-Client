

using System.Collections.Concurrent;
using Archipelago.MultiClient.Net.Models;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;
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
    private static readonly ConcurrentQueue<Tuple<int, ItemInfo>> receivedItems = new();
    private static readonly ConcurrentQueue<FillerSHItem> cachedInGameItems = new();
    
    
    public static void QueueItem(int index, ItemInfo item)
    {
        receivedItems.Enqueue(Tuple.Create(index, item));
    }

    public static void RunCheckReceivedItemsQueue()
    {
        try
        {
            while (true)
            {
                if (receivedItems.TryDequeue(out var itemTuple))
                    HandleItem(itemTuple.Item1, itemTuple.Item2);
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void HandleItem(int index, ItemInfo item)
    {
        if (index < Mod.SaveDataHandler!.CustomSaveData.LastItemIndex)
            return;
        
        
        Mod.SaveDataHandler!.CustomSaveData!.LastItemIndex++;
        var handled = false;
        
        
        var itemName = item.ItemName;
        var itemId = (FillerSHItem)(item.ItemId - 0x93930000);
        
        //check for items here
        CheckPlayableCharItemName(itemName, ref handled);
        CheckEmeraldItemName(itemName, ref handled);
        CheckEmblemItemName(itemName, ref handled);
        CheckAbilityItemName(itemName, ref handled);
        CheckStageObjItemName(itemName, ref handled);
        
        if (handled && Mod.ArchipelagoHandler.SlotData != null)
            Mod.LevelSelectManager.RecalculateOpenLevels();
        Mod.ArchipelagoHandler!.Save();
        
        if (!GameStateHandler.InGame())
        {
            cachedInGameItems.Enqueue(itemId);
            return;
        }
        HandleInGameItem(itemId);
    }
    
    public static void HandleCachedItems()
    {
        while (cachedInGameItems.Count > 0)
        {
            if (cachedInGameItems.TryDequeue(out var item))
                HandleInGameItem(item);
        }
    }
    
    public static void HandleInGameItem(FillerSHItem itemId)
    {
        switch (itemId)
        {
            case FillerSHItem.ExtraLife:
                GameStateGameWrites.ModifyLives((int)Mod.ModuleBase, 1);
                unsafe
                {
                    try
                    {
                        Mod.SaveDataHandler.SaveData->savedLives++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1034);
                break;
            case FillerSHItem.FiveRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount() + 5);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case FillerSHItem.TenRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount() + 10);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case FillerSHItem.TwentyRings:
                GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount() + 20);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                break;
            case FillerSHItem.Shield:
                GameStateGameWrites.GiveShield((int)Mod.ModuleBase);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1036);
                break;
            case FillerSHItem.SpeedLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Speed);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case FillerSHItem.PowerLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Power);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case FillerSHItem.FlyLevelUp:
                ItemGameWrites.GiveLevelUp(LevelUpType.Flying);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case FillerSHItem.TeamLevelUp: 
                ItemGameWrites.GiveLevelUp(LevelUpType.Speed);
                ItemGameWrites.GiveLevelUp(LevelUpType.Power);
                ItemGameWrites.GiveLevelUp(LevelUpType.Flying);
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE005);
                break;
            case FillerSHItem.TeamBlastFiller:
                try
                {
                    ItemGameWrites.HandleTeamBlastFiller();
                    var team = GameStateHandler.GetCurrentStory();
                    var level = GameStateHandler.GetCurrentLevel();

                    if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue((LevelId)level!, out Region region))
                        break;

                    if (AbilityCharacterManager.CanTeamBlast((Team)team!, region))
                    {
                        GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount() + 10);
                        if (Mod.Configuration!.PlaySounds)
                            SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                        break;
                    }
                    GameStateGameWrites.SetRingCount(GameStateGameWrites.GetRingCount() + 1);
                    if (Mod.Configuration!.PlaySounds)
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1004);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case FillerSHItem.StealthTrap:
                TrapHandler.HandleStealthTrap();
                break;
            case FillerSHItem.FreezeTrap:
                TrapHandler.HandleFreezeTrap();
                break;
            case FillerSHItem.NoSwapTrap:
                TrapHandler.HandleNoSwapTrap();
                break;
            case FillerSHItem.RingTrap:
                GameStateGameWrites.SetRingCount(Math.Max(0, GameStateGameWrites.GetRingCount() - 50));
                if (Mod.Configuration!.PlaySounds)
                    SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1005);
                RingLinkHandler.SendRingPacket(-50);
                break;
            case FillerSHItem.CharmyTrap:
                TrapHandler.HandleCharmyTrap();
                break;
            default:
                break;
        }
    }
    
    public static void CheckPlayableCharItemName(string itemName, ref bool handled)
    {
        try
        {
            var character = Enum.GetValues<PlayableCharacter>().Cast<PlayableCharacter?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString()!}", StringComparison.InvariantCultureIgnoreCase));
            if (character == null) 
                return;
            //match here
            var team = SonicHeroesDefinitions.PlayableCharToTeam[(PlayableCharacter)character];
            var formation = SonicHeroesDefinitions.PlayableCharToFormation[(PlayableCharacter)character];
            var unlocked = AbilityCharacterManager.GetCharUnlock(team, formation);
            if (!Mod.IsDebug)
                unlocked = false;
            AbilityCharacterManager.SetCharUnlock(team, formation, !unlocked);
            Console.WriteLine($"Got Item: {itemName}");
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static unsafe void CheckEmeraldItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            Emerald? emerald = Enum.GetValues<Emerald>().Cast<Emerald?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString()!}ChaosEmerald", StringComparison.InvariantCultureIgnoreCase));
            if (emerald == null) 
                return;
            Mod.SaveDataHandler!.CustomSaveData!.Emeralds[(Emerald)emerald] = true;
            Mod.SaveDataHandler!.RedirectData->Emerald[((int)emerald + 1) * 3] = 1;
            Console.WriteLine($"Got Item: {itemName}");
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static unsafe void CheckEmblemItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            if (!itemName.Contains("Emblem"))
                return;
            Mod.SaveDataHandler!.CustomSaveData!.Emblems++;
            Console.WriteLine($"Got Item: {itemName}");
            if (Mod.Configuration!.PlaySounds)
                SoundHandler.PlaySound((int)Mod.ModuleBase, 0xE016);
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static unsafe void CheckAbilityItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            Team? team;
            Region? region;
            if (itemName.Contains("All Abilities", StringComparison.InvariantCultureIgnoreCase))
            {
                team = CheckTeamItemName(itemName, ref handled);
                region = CheckRegionItemName(itemName, ref handled);
                AbilityCharacterManager.UnlockAbilityItemCallback(null, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
            
            Ability? ability = Enum.GetValues<Ability>().Cast<Ability?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString()!}", StringComparison.InvariantCultureIgnoreCase));
            if (ability == null)
                return;
            team = CheckTeamItemName(itemName, ref handled);
            region = CheckRegionItemName(itemName, ref handled);
            AbilityCharacterManager.UnlockAbilityItemCallback(ability, team, region);
            Console.WriteLine($"Got Item: {itemName}");
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static unsafe void CheckStageObjItemName(string itemName, ref bool handled)
    {
        try
        {
            if (handled)
                return;
            Team? team;
            Region? region;
            if (itemName.Contains("All Stage Objects", StringComparison.InvariantCultureIgnoreCase))
            {
                team = CheckTeamItemName(itemName, ref handled);
                region = CheckRegionItemName(itemName, ref handled);
                StageObjHandler.UnlockStageObjItemCallback(null, team, region);
                Console.WriteLine($"Got Item: {itemName}");
                handled = true;
                return;
            }
            StageObjTypes? stageObj = StageObjData.StageObjsToMessWith.Cast<StageObjTypes?>().FirstOrDefault(x =>itemName.Replace(" ", "").Contains($"{x.ToString()!}", StringComparison.InvariantCultureIgnoreCase));
            if (stageObj == null)
                return;
            team = CheckTeamItemName(itemName, ref handled);
            region = CheckRegionItemName(itemName, ref handled);
            StageObjHandler.UnlockStageObjItemCallback(stageObj, team, region);
            Console.WriteLine($"Got Item: {itemName}");
            handled = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static Team? CheckTeamItemName(string itemName, ref bool handled)
    {
        try
        {
            return Enum.GetValues<Team>().Cast<Team?>().FirstOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString()!}", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }
    
    public static Region? CheckRegionItemName(string itemName, ref bool handled)
    {
        try
        {
            return Enum.GetValues<Region>().Cast<Region?>().LastOrDefault(x =>
                itemName.Replace(" ", "").Contains($"{x.ToString()!}Region", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }
}