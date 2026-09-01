using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.TripleSprings;

public static class TripleSpringsData
{
    public readonly struct TripleSpringData(Team team, LevelId levelid, string region, string loc_name, float power, int no_control_time, ItemReward item_reward, float scale, StageObjTypes stage_obj_type, int group, int id_offset_group, int id_offset_full, byte linkid, float x, float y, float z)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly float Power = power;
        public readonly int NoControlTime = no_control_time;
        public readonly ItemReward Reward = item_reward;
        public readonly float Scale = scale;
        public readonly StageObjTypes ObjType = stage_obj_type;
        public readonly int Group = group;
        public readonly int IdOffsetGroup = id_offset_group;
        public readonly int IdOffsetFull = id_offset_full;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new(x, y, z);
    }
    
    
    
    
    public static List<TripleSpringData> SonicTripleSprings = new()
    {
        
    };

    public static List<TripleSpringData> DarkTripleSprings = new()
    {
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "5 Rings Triple Spring", power: 2f, no_control_time: 30, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -0.0004f, y: 40f, z: -1432f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Lower Path After Beginning Flower Patch", loc_name: "5 Rings Triple Spring", power: 3.4999986f, no_control_time: 70, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -0.0004f, y: 160f, z: -1932f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "5 Rings Triple Spring", power: 2f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -415.4742f, y: 330f, z: -6660.969f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "5 Rings Triple Spring", power: 1f, no_control_time: 0, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -1697.694f, y: 520f, z: -6389.736f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "2 x Triple Springs", power: -999999f, no_control_time: -999999, item_reward: ItemReward.NoneItemBox, scale: -999999f, stage_obj_type: StageObjTypes.TripleSpring, group: -1, id_offset_group: 0, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Left 5 Rings Triple Spring", power: 2f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: -4585.006f, y: 73.5f, z: -11394.73f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Right 5 Rings Triple Spring", power: 2f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: -4525.006f, y: 73.5f, z: -11394.73f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "First 5 Rings Triple Spring", power: 2f, no_control_time: 10, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 2, id_offset_full: 1, linkid: 0, x: -4534.092f, y: 21.7605f, z: -12103.58f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "Second 5 Rings Triple Spring", power: 2f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 2, id_offset_full: 1, linkid: 0, x: -4508.843f, y: -4.9f, z: -12752.63f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin After First Bobsled", loc_name: "5 Rings Triple Spring", power: 3.5000005f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 2, id_offset_full: 1, linkid: 0, x: -1577.504f, y: 29.99f, z: -16150.56f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Loop", loc_name: "5 Rings Triple Spring", power: 5f, no_control_time: 20, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 2, id_offset_full: 1, linkid: 0, x: -1720.006f, y: 350f, z: -42013.09f),
        new TripleSpringData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "5 Rings Triple Spring", power: 10f, no_control_time: 40, item_reward: ItemReward.Rings5, scale: 0f, stage_obj_type: StageObjTypes.TripleSpring, group: 0, id_offset_group: 2, id_offset_full: 1, linkid: 0, x: 2052.116f, y: 10f, z: -43083.47f),
    };

    public static List<TripleSpringData> RoseTripleSprings = new()
    {
        
    };

    public static List<TripleSpringData> ChaotixTripleSprings = new()
    {
        
    };

    public static List<TripleSpringData> SuperHardModeTripleSprings = new()
    {
        
    };
    
    
    
    
    public static readonly List<TripleSpringData> AllTripleSprings = SonicTripleSprings.Concat(DarkTripleSprings).Concat(RoseTripleSprings).Concat(ChaotixTripleSprings).Concat(SuperHardModeTripleSprings).ToList();
    
    
    
}