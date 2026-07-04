using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.EggBishops;

public static class EggBishopsData
{
    public readonly struct EggBishopData(Team team, LevelId levelid, string region, string loc_name, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<EggBishopData> SonicEggBishops = new()
    {
        
    };
    
    
    public static List<EggBishopData> DarkEggBishops = new()
    {
        
    };
    
    
    public static List<EggBishopData> RoseEggBishops = new()
    {
        
    };
    
    
    public static List<EggBishopData> ChaotixEggBishops = new()
    {
        
    };
    
    
    public static List<EggBishopData> SuperHardModeEggBishops = new()
    {
        
    };
    
    
    
    public static readonly List<EggBishopData> AllEggBishops = SonicEggBishops.Concat(DarkEggBishops).Concat(RoseEggBishops).Concat(ChaotixEggBishops).Concat(SuperHardModeEggBishops).ToList();

    
}