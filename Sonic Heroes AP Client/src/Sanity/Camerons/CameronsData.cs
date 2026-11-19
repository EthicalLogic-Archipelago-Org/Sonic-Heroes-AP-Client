using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.Camerons;

public static class CameronsData
{
    public record CameronData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.Cameron);
    
    
    public static readonly List<CameronData> SonicCamerons = new()
    {
        
    };
    
    
    public static readonly List<CameronData> DarkCamerons = new()
    {
        
    };
    
    
    public static readonly List<CameronData> RoseCamerons = new()
    {
        
    };
    
    
    public static readonly List<CameronData> ChaotixCamerons = new()
    {
        
    };
    
    
    public static readonly List<CameronData> SuperHardModeCamerons = new()
    {
        
    };
    
    
    public static readonly List<CameronData> AllCamerons = SonicCamerons.Concat(DarkCamerons).Concat(RoseCamerons).Concat(ChaotixCamerons).Concat(SuperHardModeCamerons).ToList();

    
}