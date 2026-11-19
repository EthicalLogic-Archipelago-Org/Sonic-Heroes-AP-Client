using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Enemy;

namespace Sonic_Heroes_AP_Client.Sanity.EggHammers;

public static class EggHammersData
{
    public record EggHammerData(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : EnemyData.BaseEnemyData(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.EggHammer);
    
    
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