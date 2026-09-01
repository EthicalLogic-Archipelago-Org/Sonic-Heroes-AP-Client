using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.EggHammers;

public static class EggHammersData
{
    public readonly struct EggHammerData(Team team, LevelId levelid, string region, string loc_name, int group, int id_offset_group, int id_offset_full, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly int Group = group;
        public readonly int IdOffsetGroup = id_offset_group;
        public readonly int IdOffsetFull = id_offset_full;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<EggHammerData> SonicEggHammers = new()
    {
        
    };
    
    
    public static List<EggHammerData> DarkEggHammers = new()
    {
        
    };
    
    
    public static List<EggHammerData> RoseEggHammers = new()
    {
        
    };
    
    
    public static List<EggHammerData> ChaotixEggHammers = new()
    {
        
    };
    
    
    public static List<EggHammerData> SuperHardModeEggHammers = new()
    {
        
    };
    
    
    
    public static readonly List<EggHammerData> AllEggHammers = SonicEggHammers.Concat(DarkEggHammers).Concat(RoseEggHammers).Concat(ChaotixEggHammers).Concat(SuperHardModeEggHammers).ToList();

    
    
}