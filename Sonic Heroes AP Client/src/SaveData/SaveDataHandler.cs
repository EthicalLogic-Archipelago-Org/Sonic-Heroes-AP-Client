
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;

namespace Sonic_Heroes_AP_Client.SaveData;


public class SaveDataHandler
{
    /// <summary>
    /// The Game's regular Save Data.
    /// The Game will be redirected to Redirect Data
    /// </summary>
    public unsafe SaveData* SaveData;
    
    /// <summary>
    /// Save Data that is stored in Unmanaged Memory of the Game.
    /// The Game is redirected to this Save Data
    /// </summary>
    public unsafe SaveData* RedirectData;
    
    /// <summary>
    /// Contains the Mod Save Data that is then serialized to a JSON and back.
    /// </summary>
    public CustomSaveData? CustomSaveData;
    
    /// <summary>
    /// This is needed to Pin the emblem count so GC doesnt delete it (from unmanaged memory).
    /// </summary>
    private int[] redirectEmblemCount = new int[1];
    
    
    public unsafe bool LoadSaveData(string seed, string slot)
    {
        var filePath = $"./Saves/{seed}{slot}.json";
        if (!Directory.Exists("./Saves"))
            Directory.CreateDirectory("./Saves");
        if (File.Exists(filePath))
        {
            try
            {
                var data = JsonConvert.DeserializeObject<CustomSaveData>(File.ReadAllText(filePath));
                CustomSaveData = data ?? throw new Exception("AHHHHHHHHHHHHH");
                //Logger.Log("Save loaded successfully!");
                redirectEmblemCount[0] = CustomSaveData.Emblems;
            }
            catch (Exception ex)
            {
                //Logger.Log($"Error: Unable to read save. {ex.Message}");
                return false;
            }
        }
        else
        {
            //Logger.Log("Creating a new save file.");
            CustomSaveData = new CustomSaveData();
            SaveGame(seed, slot);
            //Logger.Log("Save file created");
        }

        try
        {
            SaveData = (SaveData*)(Mod.ModuleBase + 0x6A4228);
            RedirectData = (SaveData*)Marshal.AllocHGlobal(sizeof(SaveData)).ToPointer();
            var redirectAddress = (IntPtr)RedirectData;
            var empty = new SaveData();
            Marshal.StructureToPtr(empty, (IntPtr)RedirectData, false);
            SaveDataGameWrites.RedirectSaveData(redirectAddress);
            SaveData->EmblemCount = (byte)CustomSaveData.Emblems;
            RedirectData->Emerald[3] = CustomSaveData.Emeralds[Emerald.Green] ? 1 : 0;
            RedirectData->Emerald[6] = CustomSaveData.Emeralds[Emerald.Blue] ? 1 : 0;
            RedirectData->Emerald[9] = CustomSaveData.Emeralds[Emerald.Yellow] ? 1 : 0;
            RedirectData->Emerald[12] = CustomSaveData.Emeralds[Emerald.White] ? 1 : 0;
            RedirectData->Emerald[15] = CustomSaveData.Emeralds[Emerald.Cyan] ? 1 : 0;
            RedirectData->Emerald[18] = CustomSaveData.Emeralds[Emerald.Purple] ? 1 : 0;
            RedirectData->Emerald[21] = CustomSaveData.Emeralds[Emerald.Red] ? 1 : 0;

            var handle = GCHandle.Alloc(redirectEmblemCount, GCHandleType.Pinned);
        
            Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B6E3, new byte[] { 0xA1 });
            Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B6E4, BitConverter.GetBytes((int)handle.AddrOfPinnedObject()));
            Memory.Instance.SafeWrite(Mod.ModuleBase + 0x4B6E8, new byte[] { 0x90, 0x90 });
        
            //GameHandler.Change99LivesCap(true);
            //GameHandler.Change999RingsCap(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return true;
    }
    
    
    /// <summary>
    /// Lock Object for the save JSON.
    /// Will prevent writing to the Save JSON twice at the same time.
    /// </summary>
    private static object Lock = new object();
    
    public void SaveGame(string seed, string slot)
    {
        try
        {
            var filePath = $"./Saves/{seed}{slot}.json";
        
            Console.WriteLine("Saved Here");
            var json = JsonConvert.SerializeObject(CustomSaveData, Formatting.Indented);

            lock (Lock)
            {
                File.WriteAllText(filePath, json);
            }
            redirectEmblemCount[0] = CustomSaveData!.Emblems;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public unsafe void WriteLevelUnlockToRedirectSaveData(LevelId level, bool isBoss, Team? story, bool value)
    {
        if (Mod.ArchipelagoHandler.SlotData == null)
            return;

        var rank = value ? Rank.ERank : Rank.NoRank;
        
        if (level == LevelId.MetalMadness)
        {
            //Logger.Log($"Setting boss: {level} to {rank}");
            RedirectData->MetalMadness.Rank = rank;
            return;
        }
        if (isBoss)
        { 
            //Logger.Log($"Setting boss: {level} to {rank}");
            if (Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.SonicActA) || Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.SonicActB) || Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.SuperHardMode))
                RedirectData->Bosses[(int)level - 16].SonicBoss.Rank = rank;
            if (Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.DarkActA) || Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.DarkActB))
                RedirectData->Bosses[(int)level - 16].DarkBoss.Rank = rank;
            if (Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.RoseActA) || Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.RoseActB))
                RedirectData->Bosses[(int)level - 16].RoseBoss.Rank = rank;
            if (Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.ChaotixActA) || Mod.LevelSelectManager.EnabledStoriesAndSanities.HasFlag(StoriesAndSanities.ChaotixActB))
                RedirectData->Bosses[(int)level - 16].ChaotixBoss.Rank = rank;
            return;
        }
        //Logger.Log($"Setting {story}'s {level} to {rank}");
        if (story == Team.Sonic)
            RedirectData->Levels[(int)level - 2].Sonic.Mission1.Rank = rank;
        if (story == Team.Dark)
            RedirectData->Levels[(int)level - 2].Dark.Mission1.Rank = rank;
        if (story == Team.Rose)
            RedirectData->Levels[(int)level - 2].Rose.Mission1.Rank = rank;
        if (story == Team.Chaotix)
            RedirectData->Levels[(int)level - 2].Chaotix.Mission1.Rank = rank;
    }
    
}