using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.Klagens;

public static class KlagensData
{
    public readonly struct KlagenData(Team team, LevelId levelid, string region, string loc_name, byte linkid, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }
    
    
    public static List<KlagenData> SonicKlagens = new()
    {
        
    };
    
    
    public static List<KlagenData> DarkKlagens = new()
    {
        
    };
    
    
    public static List<KlagenData> RoseKlagens = new()
    {
        
    };
    
    
    public static List<KlagenData> ChaotixKlagens = new()
    {
        
    };
    
    
    public static List<KlagenData> SuperHardModeKlagens = new()
    {
        
    };
    
    
    
    public static readonly List<KlagenData> AllKlagens = SonicKlagens.Concat(DarkKlagens).Concat(RoseKlagens).Concat(ChaotixKlagens).Concat(SuperHardModeKlagens).ToList();
    
    
    
    
}