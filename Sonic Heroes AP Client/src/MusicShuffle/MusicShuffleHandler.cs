

using System.Runtime.InteropServices;
using System.Text;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.MusicShuffle;

/// <summary>
/// Handler for Music Rando/Shuffle.
/// Must be initialized after Configuration
/// </summary>
public static class MusicShuffleHandler
{
    /// <summary>
    /// A Mapping of Heroes (Vanilla) ADX file names to the randomized ADX File Name.
    /// </summary>
    public static Dictionary<string, string> Map = new (StringComparer.OrdinalIgnoreCase);

    
    public static unsafe void HandleBGMFilePathHook(int filePathAddr)
    {
        try
        {
            byte[] filePath = new byte[256];
            Marshal.Copy(filePathAddr, filePath, 0, filePath.Length);
            
            var oldFileFullPath = Encoding.ASCII.GetString(filePath, 0, filePath.Length).Trim('\0');
            
            //Console.WriteLine($"HandleBGMFilePathHook oldFileFullPath: {oldFileFullPath}");
            
            var success = Map.TryGetValue(oldFileFullPath, out var newName);
            if (!success)
                return;
            //Console.WriteLine($"HandleBGMFilePathHook Success: newName: {newName}");
            newName += '\0';
            var newNameBytes = Encoding.ASCII.GetBytes(newName.ToArray());
            for (var i = 0; i < newName.Length; i++)
                *(byte*)(filePathAddr + i) = newNameBytes[i];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    

    public static void Shuffle(int seed)
    {
        if (!Mod.Configuration!.HeroesMusicShuffleOptions.MusicShuffleHeroes 
            && !Mod.Configuration.SA2MusicShuffleOptions.MusicShuffleSA2 
            && !Mod.Configuration.SADXMusicShuffleOptions.MusicShuffleSADX
            && !Mod.Configuration.CustomMusicShuffleOptions.MusicShuffleCustom)
            return;
        
        Map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var allSongs = new List<(string name, MusicType type)>();
        var heroesSongs = MusicShuffleData.HeroesSongs;
        
        if (Mod.Configuration!.HeroesMusicShuffleOptions.MusicShuffleHeroes)
            allSongs.AddRange(heroesSongs);
        if (Mod.Configuration.SADXMusicShuffleOptions.MusicShuffleSADX)
            allSongs.AddRange(MusicShuffleData.SADXSongs);
        if (Mod.Configuration.SA2MusicShuffleOptions.MusicShuffleSA2)
            allSongs.AddRange(MusicShuffleData.SA2Songs);
        
        try
        {
            if (Mod.Configuration.CustomMusicShuffleOptions.MusicShuffleCustom)
            {
                if (Directory.Exists(MusicShuffleData.CustomFolder))
                {
                    allSongs.AddRange(
                        from type in Enum.GetValues(typeof(MusicType)).Cast<MusicType>()
                        where Directory.Exists(
                            Path.Combine(MusicShuffleData.CustomFolder, type.ToString()))
                        from file in Directory.GetFiles(Path.Combine(MusicShuffleData.CustomFolder,
                            type.ToString()))
                        select (file, type));
                }
            }
        
        
            //change types here
            
            var heroesSongsMerged = new List<(string name, MusicType type)>();

            foreach (var hero in heroesSongs)
            {
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleBossMusic && hero.type is MusicType.BossMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleMenuMusic && hero.type is MusicType.MenuMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleShortMusic && hero.type is MusicType.ShortMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleLongJingle && hero.type is MusicType.LongJingle)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Jingle));
                    continue;
                }
                heroesSongsMerged.Add((hero.name, hero.type));
                
            }
            
            var allSongsMerged = new List<(string name, MusicType type)>();

            foreach (var hero in allSongs)
            {
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleBossMusic && hero.type is MusicType.BossMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleMenuMusic && hero.type is MusicType.MenuMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleShortMusic && hero.type is MusicType.ShortMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleLongJingle && hero.type is MusicType.LongJingle)
                {
                    allSongsMerged.Add((hero.name, MusicType.Jingle));
                    continue;
                }
                allSongsMerged.Add((hero.name, hero.type));
            }
            
            

            var heroesGroups = heroesSongsMerged.GroupBy(s => s.type);
            var allGroups = allSongsMerged.GroupBy(s => s.type).ToDictionary(g => g.Key, g => g.Select(x => x.name).ToArray());
            var random = new Random(seed);
            foreach (var group in heroesGroups)
            {
                var songs = group.Select(x => x.name).ToList();
                var type = group.Key;
                var shuffled = allGroups[type].OrderBy(_ => random.Next()).ToList();
                
                if (songs.Count > shuffled.Count)
                {
                    for (var i = 0; i < shuffled.Count; i++)
                    {
                        Map[songs[i]] = shuffled[i];
                        Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = shuffled[i].Split('\\').Last();
                        //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = shuffled[i];
                    }

                    for (var i = shuffled.Count; i < songs.Count; i++)
                    {
                        Map[songs[i]] = songs[i];
                        Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = songs[i].Split('\\').Last();
                        //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = songs[i];
                    }
                    continue;
                }

                for (var i = 0; i < songs.Count; i++)
                {
                    Map[songs[i]] = shuffled[i];  
                    Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = shuffled[i].Split('\\').Last();
                    //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = shuffled[i];
                }
                    
            }
            
            
            //Handle Mystic Mansion Here
            //var tempStr = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12.adx")];
            //Console.WriteLine($"Mystic Mansion Should Now Be: {tempStr}");
            Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12A.adx")] = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12.adx")];
            Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping["SNG_STG12A.adx"] = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12A.adx")].Split('\\').Last();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}