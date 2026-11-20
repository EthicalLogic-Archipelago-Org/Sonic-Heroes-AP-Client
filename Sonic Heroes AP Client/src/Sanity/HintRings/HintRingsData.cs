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
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Start First Ruin Platforms", LocName: "Start First Ruin Platforms Hint Ring", VoiceLineID: 414, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4463.506f, y: 60f, z: -11090.07f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Second Island After Start", LocName: "Second Island After Start Hint Ring", VoiceLineID: 84, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4509.112f, y: 10.3056f, z: -12044.62f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Second Island Ruin Platform Staircase After Start", LocName: "Second Island Ruin Platform Staircase After Start Hint Ring", VoiceLineID: 27, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -4478.577f, y: 85.8432f, z: -13003.46f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Higher Ruin After Bobsled Area Before Checkpoint 1", LocName: "Higher Ruin After Bobsled Area Before Checkpoint 1 Hint Ring", VoiceLineID: 76, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: -1481.238f, y: 42.99f, z: -16121.25f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "First Cliff Before Big Ruin Beach At Checkpoint 1", LocName: "First Cliff Before Big Ruin Beach At Checkpoint 1 Hint Ring", VoiceLineID: 68, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 19.6489f, y: 185f, z: -16195.09f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Second Cliff Before Big Ruin Beach After Checkpoint 1", LocName: "Second Cliff Before Big Ruin Beach After Checkpoint 1 Hint Ring", VoiceLineID: 121, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 278.9631f, y: 385f, z: -16154.3f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Path After Blockage Before Big Ruin Beach After Checkpoint 1", LocName: "Path After Blockage Before Big Ruin Beach After Checkpoint 1 Hint Ring", VoiceLineID: 30, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1197.523f, y: 535f, z: -16337.14f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Start Hint Ring After Checkpoint 1", LocName: "Big Ruin Beach Start Hint Ring After Checkpoint 1 Hint Ring", VoiceLineID: 39, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1262.403f, y: 400.1801f, z: -16857.68f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Start Lower Ruin Platform After Checkpoint 1", LocName: "Big Ruin Beach Start Lower Ruin Platform After Checkpoint 1 Hint Ring", VoiceLineID: 46, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1313.886f, y: 380.1901f, z: -17028.06f)),
        new HintRingData(Team: Team.Rose, LevelId: LevelId.SeasideHill, Region: "Big Ruin Beach Island 2 Ruins After Checkpoint 1", LocName: "Big Ruin Beach Island 2 Ruins After Checkpoint 1 Hint Ring", VoiceLineID: 78, Group: 0, IdOffsetGroup: 0, IdOffsetFull: 0, LinkID: 0, SpawnCoords: new Vector3(x: 1670.206f, y: 39.8996f, z: -19541.21f)),
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