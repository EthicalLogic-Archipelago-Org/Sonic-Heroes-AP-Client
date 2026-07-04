using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.E2000s;

public static class E2000sData
{
    public readonly struct E2000Data(Team team, LevelId levelid, string region, string loc_name, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<E2000Data> SonicE2000s = new()
    {
        
    };
    
    
    public static List<E2000Data> DarkE2000s = new()
    {
        
    };
    
    
    public static List<E2000Data> RoseE2000s = new()
    {
        
    };
    
    
    public static List<E2000Data> ChaotixE2000s = new()
    {
        
    };
    
    
    public static List<E2000Data> SuperHardModeE2000s = new()
    {
        
    };
    
    
    
    public static readonly List<E2000Data> AllE2000s = SonicE2000s.Concat(DarkE2000s).Concat(RoseE2000s).Concat(ChaotixE2000s).Concat(SuperHardModeE2000s).ToList();

    
}