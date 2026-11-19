using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.RhinoLiners;

public static class RhinoLinersData
{
    public record RhinoLinerData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.RhinoLiner);
    
    
    public static List<RhinoLinerData> SonicRhinoLiners = new()
    {
        
    };
    
    
    public static List<RhinoLinerData> DarkRhinoLiners = new()
    {
        
    };
    
    
    public static List<RhinoLinerData> RoseRhinoLiners = new()
    {
        
    };
    
    
    public static List<RhinoLinerData> ChaotixRhinoLiners = new()
    {
        
    };
    
    
    public static List<RhinoLinerData> SuperHardModeRhinoLiners = new()
    {
        
    };
    
    
    
    public static readonly List<RhinoLinerData> AllRhinoLiners = SonicRhinoLiners.Concat(DarkRhinoLiners).Concat(RoseRhinoLiners).Concat(ChaotixRhinoLiners).Concat(SuperHardModeRhinoLiners).ToList();

    
    
}