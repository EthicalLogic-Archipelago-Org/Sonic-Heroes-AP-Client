using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.ItemBoxes;

public static class ItemBoxesData
{
    public record ItemBoxData(ItemReward ItemReward, Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : StageObjDataBase(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.ItemBox);
    
    
    public static List<ItemBoxData> SonicItemBoxes = new()
    {
        
    };

    public static List<ItemBoxData> DarkItemBoxes = new()
    {
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Loop Before Checkpoint 1", LocName: "First Loop Before Checkpoint 1 3 x Item Boxes", ItemReward: ItemReward.NoneItemBox, Group: -1, IdOffsetGroup: 0, IdOffsetFull: -999999, LinkID: 0, SpawnCoords: new Vector3(x: -999999f, y: -999999f, z: -999999f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Loop Before Checkpoint 1", LocName: "First Loop Before Checkpoint 1 Left 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 44.23f, y: 287.58f, z: -5107.67f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Loop Before Checkpoint 1", LocName: "First Loop Before Checkpoint 1 Center 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 81.62f, y: 323.58f, z: -5099.388f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Loop Before Checkpoint 1", LocName: "First Loop Before Checkpoint 1 Right 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 126.62f, y: 365.58f, z: -5092.609f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Hermit Crab Cliff Below Eggmans Robots Before Checkpoint 1", LocName: "Hermit Crab Cliff Below Eggmans Robots Before Checkpoint 1 Extra life Box", ItemReward: ItemReward.ExtraLife, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 248.877f, y: 202.6395f, z: -6337.48f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Top Before Checkpoint 1", LocName: "Eggmans Robots Top Before Checkpoint 1 Shield Box In Rock", ItemReward: ItemReward.Shield, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -775.99f, y: 530f, z: -6660.06f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Top Cliff Route With Hermit Crab Before Checkpoint 1", LocName: "Top Cliff Route With Hermit Crab Before Checkpoint 1 10 Rings Box in Iron Container", ItemReward: ItemReward.Rings10, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -1769.18f, y: 619.9f, z: -6393.996f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Corner Cave After Checkpoint 1", LocName: "Corner Cave After Checkpoint 1 Loop Center 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -4370.176f, y: 482.0524f, z: -7693.988f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Second Cliff Before Big Ruin Beach After Checkpoint 2", LocName: "Second Cliff Before Big Ruin Beach After Checkpoint 2 10 Rings Box in Rock", ItemReward: ItemReward.Rings10, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 589.9285f, y: 386f, z: -16148.27f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Lower Cliff Before Big Ruin Beach After Checkpoint 2", LocName: "Lower Cliff Before Big Ruin Beach After Checkpoint 2 Invincibility Box in Rock", ItemReward: ItemReward.Invincibility, Group: 0, IdOffsetGroup: 3, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 454.9761f, y: 244.7602f, z: -16049.88f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3", LocName: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3 3 x Item Boxes", ItemReward: ItemReward.NoneItemBox, Group: -2, IdOffsetGroup: 3, IdOffsetFull: -999999, LinkID: 0, SpawnCoords: new Vector3(x: -999999f, y: -999999f, z: -999999f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3", LocName: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3 Left 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 2, IdOffsetGroup: -999999, IdOffsetFull: 2, LinkID: 0, SpawnCoords: new Vector3(x: 756.006f, y: 400.8734f, z: -26670.38f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3", LocName: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3 Center 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 2, IdOffsetGroup: -999999, IdOffsetFull: 2, LinkID: 0, SpawnCoords: new Vector3(x: 804.0079f, y: 463.8734f, z: -26670.47f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3", LocName: "Path After Big Special Ruin Before Volcano Loop After Checkpoint 3 Right 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 2, IdOffsetGroup: -999999, IdOffsetFull: 2, LinkID: 0, SpawnCoords: new Vector3(x: 856.6043f, y: 541.8734f, z: -26661.36f)),
        new ItemBoxData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Loop After Second Bobsled After Checkpoint 4", LocName: "Loop After Second Bobsled After Checkpoint 4 Left 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 0, IdOffsetGroup: 6, IdOffsetFull: 2, LinkID: 0, SpawnCoords: new Vector3(x: -540.6484f, y: 442.7799f, z: -40279.72f)),
    };

    public static List<ItemBoxData> RoseItemBoxes = new()
    {
        
    };

    public static List<ItemBoxData> ChaotixItemBoxes = new()
    {
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Loop at Start of Level", LocName: "First Loop at Start of Level 3 x Item Boxes", ItemReward: ItemReward.NoneItemBox, Group: -1, IdOffsetGroup: 0, IdOffsetFull: -999999, LinkID: 0, SpawnCoords: new Vector3(x: -999999f, y: -999999f, z: -999999f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Loop at Start of Level", LocName: "First Loop at Start of Level Left 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 52.2303f, y: 287.5814f, z: -5107.672f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Loop at Start of Level", LocName: "First Loop at Start of Level Center 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 87.6204f, y: 323.5814f, z: -5099.388f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Loop at Start of Level", LocName: "First Loop at Start of Level Right 5 Rings Box", ItemReward: ItemReward.Rings5, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 122.6231f, y: 365.5814f, z: -5092.609f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Hermit Crab Cliff Below Eggmans Robots Before Checkpoint 1", LocName: "Hermit Crab Cliff Below Eggmans Robots Before Checkpoint 1 Extra Life", ItemReward: ItemReward.ExtraLife, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 248.8773f, y: 202.6395f, z: -6337.483f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Small Cliff Above Eggmans Robots Before Checkpoint 1", LocName: "Small Cliff Above Eggmans Robots Before Checkpoint 1 10 Rings", ItemReward: ItemReward.Rings10, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -735.9069f, y: 614.0084f, z: -6552.866f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Staircase Before Corner Cave Bottom Item Box At Checkpoint 1", LocName: "Staircase Before Corner Cave Bottom Item Box At Checkpoint 1 Rings 5", ItemReward: ItemReward.Rings5, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -2338f, y: 505f, z: -6447.5f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Corner Cave After Checkpoint 1", LocName: "Corner Cave After Checkpoint 1 5 Rings ", ItemReward: ItemReward.Rings5, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4370.176f, y: 482.0524f, z: -7693.988f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Second Ruin on First Island After Checkpoint 1", LocName: "Second Ruin on First Island After Checkpoint 1 10 Rings", ItemReward: ItemReward.Rings10, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4596.383f, y: 129.9144f, z: -10683.47f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Middle Ruins After Second Island After Checkpoint 1", LocName: "Middle Ruins After Second Island After Checkpoint 1 5 Rings ", ItemReward: ItemReward.Rings5, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4508.843f, y: 120.0731f, z: -13067.63f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Top Path Ruins With Item Box", LocName: "Big Ruin Beach Top Path Ruins With Item Box Invincibility", ItemReward: ItemReward.Invincibility, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 749.5585f, y: 725f, z: -17244.15f)),
        new ItemBoxData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Speed Path Ruins After Checkpoint 3", LocName: "Big Ruin Beach Speed Path Ruins After Checkpoint 3 Extra Life", ItemReward: ItemReward.ExtraLife, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1832.168f, y: 196.2407f, z: -18759.79f)),
    };

    public static List<ItemBoxData> SuperHardModeItemBoxes = new()
    {
        
    };
    
    
    public static readonly List<ItemBoxData> AllItemBoxes = SonicItemBoxes.Concat(DarkItemBoxes).Concat(RoseItemBoxes).Concat(ChaotixItemBoxes).Concat(SuperHardModeItemBoxes).ToList();
    
}