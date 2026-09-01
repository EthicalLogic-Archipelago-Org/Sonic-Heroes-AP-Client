using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBoxes;

public static class ItemBoxesData
{
    public readonly struct ItemBoxData(Team team, LevelId levelid, string region, string loc_name, ItemReward item_reward, StageObjTypes stage_obj_type, int group, int id_offset_group, int id_offset_full, byte linkid, float x, float y, float z)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly ItemReward Reward = item_reward;
        public readonly StageObjTypes ObjType = stage_obj_type;
        public readonly int Group = group;
        public readonly int IdOffsetGroup = id_offset_group;
        public readonly int IdOffsetFull = id_offset_full;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new(x, y, z);
    }
    
    
    
    public static List<ItemBoxData> SonicItemBoxes = new()
    {
        
    };

    public static List<ItemBoxData> DarkItemBoxes = new()
    {
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "3 x Item Boxes", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBox, group: -1, id_offset_group: 0, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: 44.23f, y: 287.58f, z: -5107.67f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: 81.62f, y: 323.58f, z: -5099.388f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Right 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: 126.62f, y: 365.58f, z: -5092.609f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Hermit Crab Cliff Below Eggmans Robots", loc_name: "Extra life Box", item_reward: ItemReward.ExtraLife, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: 248.877f, y: 202.6395f, z: -6337.48f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Shield Box In Rock", item_reward: ItemReward.Shield, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: -775.99f, y: 530f, z: -6660.06f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "10 Rings Box in Iron Container", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: -1769.18f, y: 619.9f, z: -6393.996f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "Loop Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: -4370.176f, y: 482.0524f, z: -7693.988f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "10 Rings Box in Rock", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: 589.9285f, y: 386f, z: -16148.27f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Lower Cliff Before Big Ruin Beach", loc_name: "Invincibility Box in Rock", item_reward: ItemReward.Invincibility, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: 454.9761f, y: 244.7602f, z: -16049.88f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "3 x Item Boxes", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBox, group: -2, id_offset_group: 3, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: 756.006f, y: 400.8734f, z: -26670.38f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: 804.0079f, y: 463.8734f, z: -26670.47f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Right 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: 856.6043f, y: 541.8734f, z: -26661.36f),
        new ItemBoxData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Loop After Second Bobsled", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, group: 0, id_offset_group: 6, id_offset_full: 2, linkid: 0, x: -540.6484f, y: 442.7799f, z: -40279.72f),
    };

    public static List<ItemBoxData> RoseItemBoxes = new()
    {
        
    };

    public static List<ItemBoxData> ChaotixItemBoxes = new()
    {
        
    };

    public static List<ItemBoxData> SuperHardModeItemBoxes = new()
    {
        
    };
    
    
    public static readonly List<ItemBoxData> AllItemBoxes = SonicItemBoxes.Concat(DarkItemBoxes).Concat(RoseItemBoxes).Concat(ChaotixItemBoxes).Concat(SuperHardModeItemBoxes).ToList();
    
}