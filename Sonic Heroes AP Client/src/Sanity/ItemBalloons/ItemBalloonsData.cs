using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBalloons;

public static class ItemBalloonsData
{
    public readonly struct ItemBalloonData(Team team, LevelId levelid, string region, string loc_name, ItemReward item_reward, StageObjTypes stage_obj_type, byte linkid, float x, float y, float z)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly ItemReward Reward = item_reward;
        public readonly StageObjTypes ObjType = stage_obj_type;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new(x, y, z);
    }
    
    
    public static List<ItemBalloonData> SonicItemBalloons = new()
    {
        
    };

    public static List<ItemBalloonData> DarkItemBalloons = new()
    {
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -2072.6f, y: 613f, z: -6420.188f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Before Corner Cave", loc_name: "Speed Up Balloon", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -2830.04f, y: 749.9459f, z: -6470.037f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island Top Pillar Right", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4410.34f, y: 394.0565f, z: -11321.93f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Left 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4605.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Center 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4555.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4505.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4522.08f, y: 164.76f, z: -12214.5f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island High Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4418.694f, y: 358.02f, z: -12342.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4208.084f, y: 49.3547f, z: -15612.81f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Second 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -3522.268f, y: 155f, z: -16722.85f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Left Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1160.001f, y: 569f, z: -17139.8f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1260.001f, y: 569f, z: -17139.8f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 High Item Balloon", loc_name: "Extra Life Balloon", item_reward: ItemReward.ExtraLife, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1210.085f, y: 585.1104f, z: -18040.01f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 Mid Item Balloon", loc_name: "Speed Level Up Balloon", item_reward: ItemReward.LevelUpSpeed, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1210.085f, y: 385.1104f, z: -18040.01f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path Item Balloon", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1151.265f, y: 575.2516f, z: -20020.56f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -993.6437f, y: 822.1299f, z: -37714.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Second 10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -882.0215f, y: 747.1299f, z: -38624.08f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Third 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -134.5972f, y: 676.99f, z: -38590.56f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fourth 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 2409.016f, y: 468.1299f, z: -38679.7f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fifth Speed Up Balloon to Left", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40825.48f),
        new ItemBalloonData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Sixth 5 Rings Balloon to Right", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40875.48f),
    };

    public static List<ItemBalloonData> RoseItemBalloons = new()
    {
        
    };

    public static List<ItemBalloonData> ChaotixItemBalloons = new()
    {
        
    };

    public static List<ItemBalloonData> SuperHardModeItemBalloons = new()
    {
        
    };
    
    
    public static readonly List<ItemBalloonData> AllItemBalloons = SonicItemBalloons.Concat(DarkItemBalloons).Concat(RoseItemBalloons).Concat(ChaotixItemBalloons).Concat(SuperHardModeItemBalloons).ToList();
    
}