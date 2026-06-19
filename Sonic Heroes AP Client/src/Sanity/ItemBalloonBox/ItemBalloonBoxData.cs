using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBalloonBox;

public static class ItemBalloonBoxData
{
    public readonly struct ItemBalloonBoxesData(Team team, LevelId levelid, string region, string loc_name, ItemReward item_reward, StageObjTypes stage_obj_type, byte linkid, float x, float y, float z)
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
    
    
    public static List<ItemBalloonBoxesData> SonicItemBalloonBoxes = new()
    {
    };

    public static List<ItemBalloonBoxesData> DarkItemBalloonBoxes = new()
    {
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 44.23f, y: 287.58f, z: -5107.67f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 81.62f, y: 323.58f, z: -5099.388f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "Right 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 126.62f, y: 365.58f, z: -5092.609f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Hermit Crab Cliff Below Eggmans Robots", loc_name: "Extra life Box", item_reward: ItemReward.ExtraLife, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 248.877f, y: 202.6395f, z: -6337.48f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", loc_name: "Shield Box In Rock", item_reward: ItemReward.Shield, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: -775.99f, y: 530f, z: -6660.06f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Top Cliff Route With Hermit Crab", loc_name: "10 Rings Box in Iron Container", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: -1769.18f, y: 619.9f, z: -6393.996f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -2072.6f, y: 613f, z: -6420.188f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Before Corner Cave", loc_name: "Speed Up Balloon", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -2830.04f, y: 749.9459f, z: -6470.037f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "Loop Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: -4370.176f, y: 482.0524f, z: -7693.988f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island Top Pillar Right", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4410.34f, y: 394.0565f, z: -11321.93f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Left 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4605.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Center 5 Rings Balloon in Rock", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4555.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Ruin Between First and Second Island", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4505.006f, y: 93.5f, z: -11289.73f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4522.08f, y: 164.76f, z: -12214.5f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Island High Item Balloon", loc_name: "Fly Level Up Balloon", item_reward: ItemReward.LevelUpFly, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4418.694f, y: 358.02f, z: -12342.48f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -4208.084f, y: 49.3547f, z: -15612.81f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "Second 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -3522.268f, y: 155f, z: -16722.85f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "10 Rings Box in Rock", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 589.9285f, y: 386f, z: -16148.27f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Lower Cliff Before Big Ruin Beach", loc_name: "Invincibility Box in Rock", item_reward: ItemReward.Invincibility, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 454.9761f, y: 244.7602f, z: -16049.88f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Left Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1160.001f, y: 569f, z: -17139.8f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Ruin With Dash Ramp", loc_name: "Right Power Level Up Balloon in Rock", item_reward: ItemReward.LevelUpPower, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1260.001f, y: 569f, z: -17139.8f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 High Item Balloon", loc_name: "Extra Life Balloon", item_reward: ItemReward.ExtraLife, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1210.085f, y: 585.1104f, z: -18040.01f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 1 Mid Item Balloon", loc_name: "Speed Level Up Balloon", item_reward: ItemReward.LevelUpSpeed, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1210.085f, y: 385.1104f, z: -18040.01f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path Item Balloon", loc_name: "10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1151.265f, y: 575.2516f, z: -20020.56f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 756.006f, y: 400.8734f, z: -26670.38f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Center 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 804.0079f, y: 463.8734f, z: -26670.47f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "Right 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: 856.6043f, y: 541.8734f, z: -26661.36f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "First 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -993.6437f, y: 822.1299f, z: -37714.48f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Second 10 Rings Balloon", item_reward: ItemReward.Rings10, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -882.0215f, y: 747.1299f, z: -38624.08f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Third 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: -134.5972f, y: 676.99f, z: -38590.56f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fourth 5 Rings Balloon", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 2409.016f, y: 468.1299f, z: -38679.7f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Fifth Speed Up Balloon to Left", item_reward: ItemReward.SpeedShoes, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40825.48f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "Sixth 5 Rings Balloon to Right", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBalloon, linkid: 0, x: 1757.533f, y: 347.9998f, z: -40875.48f),
        new ItemBalloonBoxesData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Loop After Second Bobsled", loc_name: "Left 5 Rings Box", item_reward: ItemReward.Rings5, stage_obj_type: StageObjTypes.ItemBox, linkid: 0, x: -540.6484f, y: 442.7799f, z: -40279.72f),
    };

    public static List<ItemBalloonBoxesData> RoseItemBalloonBoxes = new()
    {
    };

    public static List<ItemBalloonBoxesData> ChaotixItemBalloonBoxes = new()
    {
    };

    public static List<ItemBalloonBoxesData> SuperHardModeItemBalloonBoxes = new()
    {
    };
    
    
    public static readonly List<ItemBalloonBoxesData> AllItemBalloonBoxes = SonicItemBalloonBoxes.Concat(DarkItemBalloonBoxes).Concat(RoseItemBalloonBoxes).Concat(ChaotixItemBalloonBoxes).Concat(SuperHardModeItemBalloonBoxes).ToList();
    
    
}