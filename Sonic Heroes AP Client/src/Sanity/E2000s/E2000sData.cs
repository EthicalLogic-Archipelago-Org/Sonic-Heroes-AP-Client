using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.E2000s;

public static class E2000sData
{
    public record E2000Data(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.E2000);
    
    
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