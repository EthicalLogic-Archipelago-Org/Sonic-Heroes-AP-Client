using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.TripleSprings;

public static class TripleSpringsData
{
    public record TripleSpringData(float Power, int NoControlTime, ItemReward ItemReward, float Scale, Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : StageObjDataBase(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.TripleSpring);
    
    
    public static List<TripleSpringData> SonicTripleSprings = new()
    {
        
    };

    public static List<TripleSpringData> DarkTripleSprings = new()
    {
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "After Beginning Falling Ruins After Start Of Level", LocName: "After Beginning Falling Ruins After Start Of Level 5 Rings Triple Spring", Power: 2f, NoControlTime: 30, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -0.0004f, y: 40f, z: -1432f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Lower Path After Beginning Flower Patch After of Start Of Level", LocName: "Lower Path After Beginning Flower Patch After of Start Of Level 5 Rings Triple Spring", Power: 3.4999986f, NoControlTime: 70, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -0.0004f, y: 160f, z: -1932f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Bottom Before Checkpoint 1", LocName: "Eggmans Robots Bottom Before Checkpoint 1 5 Rings Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -415.4742f, y: 330f, z: -6660.969f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Top Cliff Route With Hermit Crab Before Checkpoint 1", LocName: "Top Cliff Route With Hermit Crab Before Checkpoint 1 5 Rings Triple Spring", Power: 1f, NoControlTime: 0, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -1697.694f, y: 520f, z: -6389.736f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Ruin Between First and Second Island After Checkpoint 1", LocName: "First Ruin Between First and Second Island After Checkpoint 1 2 x Triple Springs", Power: -999999f, NoControlTime: -999999, ItemReward: ItemReward.NoneItemBox, Scale: -999999f, Group: -1, IdOffsetGroup: 0, IdOffsetFull: -999999, LinkID: 0, SpawnCoords: new Vector3(x: -999999f, y: -999999f, z: -999999f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Ruin Between First and Second Island After Checkpoint 1", LocName: "First Ruin Between First and Second Island After Checkpoint 1 Left 5 Rings Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -4585.006f, y: 73.5f, z: -11394.73f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Ruin Between First and Second Island After Checkpoint 1", LocName: "First Ruin Between First and Second Island After Checkpoint 1 Right 5 Rings Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 1, IdOffsetGroup: -999999, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -4525.006f, y: 73.5f, z: -11394.73f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Second Island After Checkpoint 1", LocName: "Second Island After Checkpoint 1 First 5 Rings Triple Spring", Power: 2f, NoControlTime: 10, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 2, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -4534.092f, y: 21.7605f, z: -12103.58f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Second Island After Checkpoint 1", LocName: "Second Island After Checkpoint 1 Second 5 Rings Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 2, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -4508.843f, y: -4.9f, z: -12752.63f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Ruin After First Bobsled After Checkpoint 1", LocName: "Ruin After First Bobsled After Checkpoint 1 5 Rings Triple Spring", Power: 3.5000005f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 2, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -1577.504f, y: 29.99f, z: -16150.56f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Path Before Big Loop After Checkpoint 4", LocName: "Path Before Big Loop After Checkpoint 4 5 Rings Triple Spring", Power: 5f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 2, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: -1720.006f, y: 350f, z: -42013.09f)),
        new TripleSpringData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Big Loop After Checkpoint 4", LocName: "Big Loop After Checkpoint 4 5 Rings Triple Spring", Power: 10f, NoControlTime: 40, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 2, IdOffsetFull: 1, LinkID: 0, SpawnCoords: new Vector3(x: 2052.116f, y: 10f, z: -43083.47f)),
    };

    public static List<TripleSpringData> RoseTripleSprings = new()
    {
        new TripleSpringData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "First Cliff Before Big Ruin Beach At Checkpoint 1", LocName: "First Cliff Before Big Ruin Beach At Checkpoint 1 Triple Spring", Power: 3.6999998f, NoControlTime: 20, ItemReward: ItemReward.Rings10, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 83.1179f, y: 185f, z: -16175.29f)),
        new TripleSpringData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Path After Blockage Before Big Ruin Beach After Checkpoint 1", LocName: "Path After Blockage Before Big Ruin Beach After Checkpoint 1 Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings10, Scale: -0.2f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1210.293f, y: 535f, z: -16647.35f)),
        new TripleSpringData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Island 1 After Checkpoint 1", LocName: "Big Ruin Beach Island 1 After Checkpoint 1 Triple Spring", Power: 1154.348f, NoControlTime: 20, ItemReward: ItemReward.Rings10, Scale: 5.0232f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1154.348f, y: 5.0232f, z: -17808.47f)),
        new TripleSpringData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Higher Big Special Ruin Before Volcano After Checkpoint 2", LocName: "Higher Big Special Ruin Before Volcano After Checkpoint 2 Triple Spring", Power: 899.9999f, NoControlTime: 0, ItemReward: ItemReward.Rings5, Scale: 199.99f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 899.9999f, y: 199.99f, z: -23668.75f)),
    };

    public static List<TripleSpringData> ChaotixTripleSprings = new()
    {
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Middle Before Checkpoint 1", LocName: "Eggmans Robots Middle Before Checkpoint 1 5 Rings Triple Spring", Power: 1f, NoControlTime: 10, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -600.855f, y: 430f, z: -6662.792f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Top Cliff Route With Hermit Crab Before Checkpoint 1", LocName: "Top Cliff Route With Hermit Crab Before Checkpoint 1 5 Rings Triple Spring", Power: 1f, NoControlTime: 0, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -1697.694f, y: 520f, z: -6389.736f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Staircase Before Corner Cave Middle After Checkpoint 1", LocName: "Staircase Before Corner Cave Middle After Checkpoint 1 10 Rings Triple Spring", Power: 0.9000001f, NoControlTime: 20, ItemReward: ItemReward.Rings10, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -2495.969f, y: 530f, z: -6440.261f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Third Ruin Between First and Second Island After Checkpoint 1", LocName: "Third Ruin Between First and Second Island After Checkpoint 1 5 Rings Triple Spring", Power: 3f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4553f, y: 114.99f, z: -11740.03f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Second Island After Checkpoint 1", LocName: "Second Island After Checkpoint 1 5 Rings Triple Spring", Power: 2f, NoControlTime: 10, ItemReward: ItemReward.Rings5, Scale: 0.2f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4534.092f, y: 21.7605f, z: -12102.58f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Bobsled Side Island After Checkpoint 2", LocName: "First Bobsled Side Island After Checkpoint 2 5 Rings Triple Spring", Power: 3f, NoControlTime: 30, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4862.527f, y: 39.8f, z: -16104.56f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Bobsled Path After Cannon After Checkpoint 2", LocName: "First Bobsled Path After Cannon After Checkpoint 2 5 Rings Triple Spring", Power: 2f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -1765.067f, y: 30f, z: -16150.05f)),
        new TripleSpringData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Cliff Before Big Ruin Beach At Checkpoint 3", LocName: "First Cliff Before Big Ruin Beach At Checkpoint 3 5 Rings Triple Spring", Power: 3.6999998f, NoControlTime: 20, ItemReward: ItemReward.Rings5, Scale: 0f, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 80.1179f, y: 185f, z: -16175.29f)),
    };

    public static List<TripleSpringData> SuperHardModeTripleSprings = new()
    {
        
    };
    
    
    
    
    public static readonly List<TripleSpringData> AllTripleSprings = SonicTripleSprings.Concat(DarkTripleSprings).Concat(RoseTripleSprings).Concat(ChaotixTripleSprings).Concat(SuperHardModeTripleSprings).ToList();
    
    
    
}