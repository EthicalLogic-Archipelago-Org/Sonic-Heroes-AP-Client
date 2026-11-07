using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBalloons;

public static class ItemBalloonsData
{
    public readonly struct ItemBalloonData(Team team, LevelId levelid, string region, string loc_name, ItemReward item_reward, StageObjTypes stage_obj_type, int group, int id_offset_group, int id_offset_full, byte linkid, float x, float y, float z)
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
    
    
    public static List<ItemBalloonData> SonicItemBalloons = new()
    {
        
    };

    public static List<ItemBalloonData> DarkItemBalloons = new()
    {
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -2072.6f, y: 613f, z: -6420.188f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Before Corner Cave", loc_name: "Speed Up Balloon", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -2830.04f, y: 749.9459f, z: -6470.037f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island Top Pillar Right", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4410.34f, y: 394.0565f, z: -11321.93f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "3 x Balloons in Rocks", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBalloon, group: -1, id_offset_group: 0, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Left 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: -4605.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Center 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: -4555.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, group: 1, id_offset_group: -999999, id_offset_full: 1, linkid: 0, x: -4505.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: -4522.08f, y: 164.76f, z: -12214.5f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island High Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 3, id_offset_full: 1, linkid: 0, x: -4418.694f, y: 358.02f, z: -12342.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "2 x Balloons", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBalloon, group: -2, id_offset_group: 3, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: -4208.084f, y: 49.3547f, z: -15612.81f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Second 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 2, id_offset_group: -999999, id_offset_full: 2, linkid: 0, x: -3522.268f, y: 155f, z: -16722.85f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "2 x Balloons in Rocks", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBalloon, group: -3, id_offset_group: 5, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Left Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, group: 3, id_offset_group: -999999, id_offset_full: 3, linkid: 0, x: 1160.001f, y: 569f, z: -17139.8f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, group: 3, id_offset_group: -999999, id_offset_full: 3, linkid: 0, x: 1260.001f, y: 569f, z: -17139.8f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 Mid Item Balloon", loc_name: "2 x Balloons", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBalloon, group: -4, id_offset_group: 7, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 High Item Balloon", loc_name: "Extra Life Balloon", item_reward: ItemReward.ExtraLife, stage_obj_type: StageObjTypes.ItemBalloon, group: 4, id_offset_group: -999999, id_offset_full: 4, linkid: 0, x: 1210.085f, y: 585.1104f, z: -18040.01f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 Mid Item Balloon", loc_name: "Speed Level Up Balloon", item_reward: ItemReward.LevelUpSpeed, stage_obj_type: StageObjTypes.ItemBalloon, group: 4, id_offset_group: -999999, id_offset_full: 4, linkid: 0, x: 1210.085f, y: 385.1104f, z: -18040.01f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path Item Balloon", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 9, id_offset_full: 4, linkid: 0, x: 1151.265f, y: 575.2516f, z: -20020.56f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "6 x Balloons", item_reward: ItemReward.NoneItemBox, stage_obj_type: StageObjTypes.ItemBalloon, group: -5, id_offset_group: 9, id_offset_full: -999999, linkid: 0, x: -999999f, y: -999999f, z: -999999f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: -993.6437f, y: 822.1299f, z: -37714.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Second 10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: -882.0215f, y: 747.1299f, z: -38624.08f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Third 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: -134.5972f, y: 676.99f, z: -38590.56f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fourth 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: 2409.016f, y: 468.1299f, z: -38679.7f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fifth Speed Up Balloon to Left", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40825.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Sixth 5 Rings Balloon to Right", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 5, id_offset_group: -999999, id_offset_full: 5, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40875.48f),
    };

    public static List<ItemBalloonData> RoseItemBalloons = new()
    {
        
    };

    public static List<ItemBalloonData> ChaotixItemBalloons = new()
    {
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom At Checkpoint 1 Item Balloon", loc_name: "Shield", item_reward: ItemReward.Shield, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -2072.601f, y: 658f, z: -6415.188f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Third Ruin Item Balloon Between First and Second Island After Checkpoint 1", loc_name: "Rings 10", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4553f, y: 194.99f, z: -11640.03f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Second Island After Checkpoint 1 Left High Item Balloon", loc_name: "Level Up Fly", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4693.275f, y: 270.9428f, z: -11998.18f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Second Island After Checkpoint 1 Center High Item Balloon", loc_name: "Level Up Power", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4527.988f, y: 250f, z: -12151.61f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Second Island After Checkpoint 1 Right High Item Balloon", loc_name: "Level Up Speed", item_reward: ItemReward.LevelUpSpeed, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4393.275f, y: 270.9428f, z: -11998.18f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Item Balloon After Cannon on First Bobsled After Checkpoint 2", loc_name: "Speed Shoes", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4187.683f, y: 304.6126f, z: -16376.27f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Item Balloon A After Large Ruin At Big Ruin Beach After Checkpoint 3", loc_name: "5 Rings ", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1283.97f, y: 256.358f, z: -17073.94f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Item Balloon B After Large Ruin At Big Ruin Beach After Checkpoint 3", loc_name: "5 Rings ", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1317f, y: 313.358f, z: -16999.91f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Item Balloon C After Large Ruin At Big Ruin Beach After Checkpoint 3", loc_name: "5 Rings ", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1265.346f, y: 304.358f, z: -16930.78f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2 Item Balloon After Checkpoint 3", loc_name: "20 Rings", item_reward: ItemReward.Rings20, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1602.533f, y: 55f, z: -19823.16f),
        new ItemBalloonData(team: Team.Chaotix, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path Before Checkpoint 4 Item Balloon", loc_name: "10 Rings", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1056.102f, y: 566.4025f, z: -20037.56f),
    };

    public static List<ItemBalloonData> SuperHardModeItemBalloons = new()
    {
        
    };
    
    
    public static readonly List<ItemBalloonData> AllItemBalloons = SonicItemBalloons.Concat(DarkItemBalloons).Concat(RoseItemBalloons).Concat(ChaotixItemBalloons).Concat(SuperHardModeItemBalloons).ToList();
    
}