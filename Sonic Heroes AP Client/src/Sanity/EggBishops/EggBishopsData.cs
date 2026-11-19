using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.EggBishops;

public static class EggBishopsData
{
    public record EggBishopData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.EggBishop);
    
    
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