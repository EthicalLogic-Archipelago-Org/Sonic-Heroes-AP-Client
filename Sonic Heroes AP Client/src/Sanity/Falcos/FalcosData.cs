using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.Falcos;

public static class FalcosData
{
    public record FalcoData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.Falco);
    
    
    public static List<FalcoData> SonicFalcos = new()
    {
        
    };
    
    
    public static List<FalcoData> DarkFalcos = new()
    {
        
    };
    
    
    public static List<FalcoData> RoseFalcos = new()
    {
        
    };
    
    
    public static List<FalcoData> ChaotixFalcos = new()
    {
        
    };
    
    
    public static List<FalcoData> SuperHardModeFalcos = new()
    {
        
    };
    
    
    
    public static readonly List<FalcoData> AllFalcos = SonicFalcos.Concat(DarkFalcos).Concat(RoseFalcos).Concat(ChaotixFalcos).Concat(SuperHardModeFalcos).ToList();
    
    
}
