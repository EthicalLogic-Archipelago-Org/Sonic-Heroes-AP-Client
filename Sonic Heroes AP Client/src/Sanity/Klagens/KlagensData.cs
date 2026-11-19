using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.Klagens;

public static class KlagensData
{
    public record KlagenData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.Klagen);
    
    
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