using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.HintRings;

public static class HintRingsData
{
    public readonly struct HintRingData (Team team, LevelId levelid, string region, ushort voicelineid, int group, int id_offset_group, int id_offset_full, byte linkid, float x, float y, float z)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly ushort VoiceLineID = voicelineid;
        public readonly int Group = group;
        public readonly int IdOffsetGroup = id_offset_group;
        public readonly int IdOffsetFull = id_offset_full;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new (x, y, z);
    }


    public static List<HintRingData> SonicHintRings = new()
    {
        

    };
    public static List<HintRingData> DarkHintRings = new()
    {
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Beginning Flower Patch", voicelineid: 27, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 24.45f, y: 190f, z: -1632.55f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", voicelineid: 30, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -177.01f, y: 330f, z: -6675.51f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Top", voicelineid: 414, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -705.99f, y: 530f, z: -6669.76f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom", voicelineid: 121, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 10, x: -2309.88f, y: 480f, z: -6401.89f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Before Corner Cave", voicelineid: 64, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -2831.04f, y: 690f, z: -6470.78f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Island", voicelineid: 78, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: -4490.37f, y: 21.5f, z: -10896.22f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Ruin Between Big Ruin Beach Islands", voicelineid: 24, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1231.38f, y: 51.09f, z: -18541.68f),
        new HintRingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", voicelineid: 2, group: 0, id_offset_group: 0, id_offset_full: 0, linkid: 0, x: 1599.577f, y: 33f, z: -19697.19f),
    };
    public static List<HintRingData> RoseHintRings = new()
    {

    };
    public static List<HintRingData> ChaotixHintRings = new()
    {

    };
    public static List<HintRingData> SuperHardModeHintRings = new()
    {

    };

    public static List<HintRingData> AllHintRings = SonicHintRings.Concat(DarkHintRings).Concat(RoseHintRings).Concat(ChaotixHintRings).Concat(SuperHardModeHintRings).ToList();




}