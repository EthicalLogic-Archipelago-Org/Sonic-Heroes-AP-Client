using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.HintRings;

public static class HintRingsData
{
    public record HintRingData(ushort VoiceLineID, Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords) : StageObjDataBase(Team: Team, LevelId: LevelId, Region: Region, LocName: LocName, Group: Group, IdOffsetGroup: IdOffsetGroup, IdOffsetFull: IdOffsetFull, LinkID: LinkID, SpawnCoords: SpawnCoords, StageObjType: StageObjTypes.HintRing);

    public static readonly List<HintRingData> SonicHintRings = new()
    {
        
    };

    public static readonly List<HintRingData> DarkHintRings = new()
    {
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Beginning Flower Patch After Start Of Level", LocName: "Beginning Flower Patch After Start Of Level Hint Ring", VoiceLineID: 27, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 24.45f, y: 190f, z: -1632.55f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Bottom Before Checkpoint 1", LocName: "Eggmans Robots Bottom Before Checkpoint 1 Hint Ring", VoiceLineID: 30, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -177.01f, y: 330f, z: -6675.51f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Top Before Checkpoint 1", LocName: "Eggmans Robots Top Before Checkpoint 1 Hint Ring", VoiceLineID: 414, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -705.99f, y: 530f, z: -6669.76f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Staircase Before Corner Cave Bottom At Checkpoint 1", LocName: "Staircase Before Corner Cave Bottom At Checkpoint 1 Hint Ring", VoiceLineID: 121, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 10, SpawnCoords: new Vector3(x: -2309.88f, y: 480f, z: -6401.89f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Ruin Before Corner Cave After Checkpoint 1", LocName: "Ruin Before Corner Cave After Checkpoint 1 Hint Ring", VoiceLineID: 64, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -2831.04f, y: 690f, z: -6470.78f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "First Island After Checkpoint 1", LocName: "First Island After Checkpoint 1 Hint Ring", VoiceLineID: 78, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4505.685f, y: 21.5f, z: -10896.22f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Ruin Between Big Ruin Beach Islands After Checkpoint 2", LocName: "Ruin Between Big Ruin Beach Islands After Checkpoint 2 Hint Ring", VoiceLineID: 24, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1231.38f, y: 51.09f, z: -18541.68f)),
        new HintRingData(Team: Team.Dark, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Island 2 Before Checkpoint 3", LocName: "Big Ruin Beach Island 2 Before Checkpoint 3 Hint Ring", VoiceLineID: 2, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1599.577f, y: 33f, z: -19697.19f)),
    };

    public static readonly List<HintRingData> RoseHintRings = new()
    {
        
    };

    public static readonly List<HintRingData> ChaotixHintRings = new()
    {
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Bottom Before Checkpoint 1", LocName: "Eggmans Robots Bottom Before Checkpoint 1 Hint Ring", VoiceLineID: 91, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -430.127f, y: 330f, z: -6601.082f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Top Before Checkpoint 1", LocName: "Eggmans Robots Top Before Checkpoint 1 Hint Ring", VoiceLineID: 414, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -772.8177f, y: 530f, z: -6660.057f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Eggmans Robots Top After Block Before Checkpoint 1", LocName: "Eggmans Robots Top After Block Before Checkpoint 1 Hint Ring", VoiceLineID: 84, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -1088.87f, y: 480f, z: -6662.69f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Staircase Before Corner Cave Bottom At Checkpoint 1", LocName: "Staircase Before Corner Cave Bottom At Checkpoint 1 Hint Ring", VoiceLineID: 77, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -2165.655f, y: 480f, z: -6447.778f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Island After Checkpoint 1", LocName: "First Island After Checkpoint 1 First Hint Ring", VoiceLineID: 24, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4506.383f, y: 6.5013f, z: -10433.47f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "First Island After Checkpoint 1", LocName: "First Island After Checkpoint 1 Second Hint Ring", VoiceLineID: 78, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4484.391f, y: 8.5f, z: -10867.28f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Second Island After Checkpoint 1", LocName: "Second Island After Checkpoint 1 Hint Ring", VoiceLineID: 417, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4558.368f, y: 100f, z: -12153f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Middle Ruins After Second Island After Checkpoint 1", LocName: "Middle Ruins After Second Island After Checkpoint 1 Hint Ring", VoiceLineID: 121, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4465.796f, y: 40.8432f, z: -12881.11f)),
        new HintRingData(Team: Team.Chaotix, LevelId: LevelId.SeasideHill, Region: "Lower Cliff Before Big Ruin Beach After Checkpoint 3", LocName: "Lower Cliff Before Big Ruin Beach After Checkpoint 3 Hint Ring", VoiceLineID: 7, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1250.417f, y: 343.1801f, z: -16835.14f)),
    };

    public static readonly List<HintRingData> SuperHardModeHintRings = new()
    {
        
    };

    public static List<HintRingData> AllHintRings = SonicHintRings.Concat(DarkHintRings).Concat(RoseHintRings).Concat(ChaotixHintRings).Concat(SuperHardModeHintRings).ToList();




}