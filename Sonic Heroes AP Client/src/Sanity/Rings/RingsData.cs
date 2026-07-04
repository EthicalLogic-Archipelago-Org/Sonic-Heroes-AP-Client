using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Rings;

public static class RingsData
{
    public readonly struct RingData(Team team, LevelId levelid, string region, string loc_name, int num_rings, RingType ring_type, float length, float radius, int start_id_offset, int id_offset, byte linkid, float x, float y, float z, string rule)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly string Region = region;
        public readonly string LocName = loc_name;
        public readonly int NumRings = num_rings;
        public readonly RingType RingType = ring_type;
        public readonly float Length = length;
        public readonly float Radius = radius;
        public readonly int StartIDOffset = start_id_offset;
        public readonly int ID_offset = id_offset;
        public readonly byte LinkID = linkid;
        public readonly Vector3 SpawnCoords = new(x, y, z);
        public readonly string Rule = rule;
    }
    

    public static readonly List<RingData> SonicRings = new()
    {
        

    };
    
    public static readonly List<RingData> DarkRings = new()
    {
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "4 Rings Before Dash Ramp", num_rings: 4, ring_type: RingType.Line, length: 220f, radius: 0f, start_id_offset: 0, id_offset: 0, linkid: 100, x: -0.0005f, y: 150f, z: 850.0001f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "6 Rings Left", num_rings: 6, ring_type: RingType.Line, length: 240f, radius: 0f, start_id_offset: 4, id_offset: 0, linkid: 0, x: -20.3162f, y: 46f, z: -857.9999f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "6 Rings Center", num_rings: 6, ring_type: RingType.Line, length: 240f, radius: 0f, start_id_offset: 10, id_offset: 0, linkid: 0, x: -0.3162f, y: 46f, z: -857.9999f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "After Beginning Falling Ruins", loc_name: "6 Rings Right", num_rings: 6, ring_type: RingType.Line, length: 240f, radius: 0f, start_id_offset: 16, id_offset: 0, linkid: 0, x: 19.6838f, y: 46f, z: -857.9999f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Upper Path After Beginning Flower Patch", loc_name: "4 Rings Left", num_rings: 4, ring_type: RingType.Line, length: 180f, radius: 0f, start_id_offset: 22, id_offset: 0, linkid: 0, x: -5.9997f, y: 106.0003f, z: -2728f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Upper Path After Beginning Flower Patch", loc_name: "4 Rings Right", num_rings: 4, ring_type: RingType.Line, length: 180f, radius: 0f, start_id_offset: 26, id_offset: 0, linkid: 0, x: 6.0003f, y: 106.0003f, z: -2728f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Loop", loc_name: "4 Rings", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 30, id_offset: 0, linkid: 0, x: 0f, y: 36.1314f, z: -4622f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "2 Rings At Start of Ring Line", num_rings: 2, ring_type: RingType.Normal, length: 65f, radius: 230f, start_id_offset: 34, id_offset: 0, linkid: 0, x: 152.294f, y: 340f, z: -6375.695f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "10 Rings In Ring Line", num_rings: 10, ring_type: RingType.Arch, length: 370f, radius: 230f, start_id_offset: 36, id_offset: 0, linkid: 0, x: 152.294f, y: 337f, z: -6435.695f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Eggmans Robots Bottom", loc_name: "6 Rings Vertical", num_rings: 6, ring_type: RingType.Line, length: 100f, radius: 0f, start_id_offset: 46, id_offset: 0, linkid: 0, x: -465.2492f, y: 373f, z: -6580.921f, rule: "And(Has(Dark Rings), Or(And(SonicHeroesMacroRule[HasAbilityForTeam()], Or(SonicHeroesMacroRule[HasAbilityForTeam()], SonicHeroesMacroRule[HasAbilityForTeam()])), And(SonicHeroesMacroRule[HasAbilityForTeam()], Has(Dark Triple Spring))))"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Staircase Before Corner Cave Bottom", loc_name: "4 Rings", num_rings: 4, ring_type: RingType.Line, length: 90f, radius: 0f, start_id_offset: 52, id_offset: 0, linkid: 0, x: -2205.001f, y: 486f, z: -6425f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "7 Rings Line At Start of Trail", num_rings: 7, ring_type: RingType.Line, length: 280f, radius: 1000f, start_id_offset: 56, id_offset: 0, linkid: 0, x: -4212.262f, y: 188f, z: -6536.168f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "10 Rings Arch in Trail", num_rings: 10, ring_type: RingType.Arch, length: 430f, radius: 270f, start_id_offset: 63, id_offset: 0, linkid: 0, x: -4259.12f, y: 192f, z: -6537.792f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "1 Ring At End of Trail", num_rings: 1, ring_type: RingType.Normal, length: 430f, radius: 270f, start_id_offset: 73, id_offset: 0, linkid: 0, x: -4508.022f, y: 189f, z: -6878.146f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "4 Rings Before Loop", num_rings: 4, ring_type: RingType.Line, length: 180f, radius: 0f, start_id_offset: 74, id_offset: 0, linkid: 0, x: -4509.233f, y: 38f, z: -7311.386f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "6 Rings At Loop Left", num_rings: 6, ring_type: RingType.Arch, length: 330f, radius: 202f, start_id_offset: 78, id_offset: 0, linkid: 0, x: -4543.649f, y: 51f, z: -7781.153f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Corner Cave", loc_name: "6 Rings At Loop Right", num_rings: 6, ring_type: RingType.Arch, length: 330f, radius: 202f, start_id_offset: 84, id_offset: 0, linkid: 0, x: -4453.649f, y: 49f, z: -7781.153f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Ruin Between First and Second Island", loc_name: "3 Rings Left", num_rings: 3, ring_type: RingType.Line, length: 45f, radius: 0f, start_id_offset: 90, id_offset: 0, linkid: 0, x: -4573.362f, y: 100.99f, z: -11526.12f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Ruin Between First and Second Island", loc_name: "3 Rings Center", num_rings: 3, ring_type: RingType.Line, length: 45f, radius: 0f, start_id_offset: 93, id_offset: 0, linkid: 0, x: -4553.362f, y: 100.99f, z: -11526.12f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Ruin Between First and Second Island", loc_name: "3 Rings Right", num_rings: 3, ring_type: RingType.Line, length: 45f, radius: 0f, start_id_offset: 96, id_offset: 0, linkid: 0, x: -4533.362f, y: 100.99f, z: -11526.12f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "5 Rings", num_rings: 5, ring_type: RingType.Arch, length: 130f, radius: 180f, start_id_offset: 99, id_offset: 0, linkid: 0, x: -4273.527f, y: 110f, z: -14666.9f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings Left", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 104, id_offset: 0, linkid: 0, x: -4199.207f, y: 20.3052f, z: -16207.65f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings Center", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 108, id_offset: 0, linkid: 0, x: -4179.207f, y: 20.3052f, z: -16207.65f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings Right", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 112, id_offset: 0, linkid: 0, x: -4159.207f, y: 20.3052f, z: -16207.65f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings At Last Turn", num_rings: 4, ring_type: RingType.Arch, length: 170f, radius: 200f, start_id_offset: 116, id_offset: 0, linkid: 0, x: -3177.891f, y: 38f, z: -16191.55f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings After Last Turn", num_rings: 4, ring_type: RingType.Line, length: 120f, radius: 0f, start_id_offset: 120, id_offset: 0, linkid: 0, x: -2620.324f, y: 36f, z: -16149.16f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Bobsled After Block", loc_name: "4 Rings At Path End", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 124, id_offset: 0, linkid: 0, x: -2133.005f, y: 36f, z: -16149.53f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin After First Bobsled", loc_name: "6 Rings ", num_rings: 6, ring_type: RingType.Line, length: 170f, radius: 0f, start_id_offset: 128, id_offset: 0, linkid: 0, x: -1050.892f, y: 135.614f, z: -16150.05f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "First Cliff Before Big Ruin Beach", loc_name: "4 Rings Vertical", num_rings: 4, ring_type: RingType.Line, length: 80f, radius: 0f, start_id_offset: 134, id_offset: 0, linkid: 0, x: 66.0038f, y: 315f, z: -16160.74f, rule: "And(Has(Dark Rings), Has(Dark Single Spring), Or(SonicHeroesMacroRule[HasAbilityForTeam()], SonicHeroesMacroRule[HasAbilityForTeam()], SonicHeroesMacroRule[HasAbilityForTeam()], SonicHeroesMacroRule[And(HasComboHeight(), SonicHeroesMacroRule[HasAbilityForTeam()])]))"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Cliff Before Big Ruin Beach", loc_name: "4 Rings", num_rings: 4, ring_type: RingType.Line, length: 100f, radius: 0f, start_id_offset: 138, id_offset: 0, linkid: 0, x: 248.9631f, y: 391f, z: -16178.3f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "3 Rings ", num_rings: 3, ring_type: RingType.Line, length: 70f, radius: 0f, start_id_offset: 142, id_offset: 0, linkid: 0, x: 730.0513f, y: 541f, z: -16180.01f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Big Ruin Beach", loc_name: "5 Rings", num_rings: 5, ring_type: RingType.Arch, length: 170f, radius: 250f, start_id_offset: 145, id_offset: 0, linkid: 0, x: 1100.383f, y: 541f, z: -16267.82f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "10 Rings", num_rings: 10, ring_type: RingType.Arch, length: 400f, radius: 300f, start_id_offset: 150, id_offset: 0, linkid: 0, x: 1359.126f, y: 5.1f, z: -18953.15f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach Island 2", loc_name: "5 Rings", num_rings: 5, ring_type: RingType.Line, length: 230f, radius: 0f, start_id_offset: 160, id_offset: 0, linkid: 0, x: 1599.577f, y: 14f, z: -19387.19f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Ruin Beach End Path", loc_name: "4 Rings", num_rings: 4, ring_type: RingType.Line, length: 90f, radius: 0f, start_id_offset: 165, id_offset: 0, linkid: 0, x: 901.421f, y: 426f, z: -20154.21f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path Before Volcano Start", loc_name: "6 Rings ", num_rings: 6, ring_type: RingType.Line, length: 300f, radius: 0f, start_id_offset: 169, id_offset: 0, linkid: 0, x: 900.1624f, y: 59f, z: -21640.25f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "4 Rings Left", num_rings: 4, ring_type: RingType.Line, length: 120f, radius: 0f, start_id_offset: 175, id_offset: 0, linkid: 0, x: 764.6441f, y: 208f, z: -25992.69f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Path After Big Special Ruin Before Volcano Loop", loc_name: "4 Rings Right", num_rings: 4, ring_type: RingType.Line, length: 120f, radius: 0f, start_id_offset: 179, id_offset: 0, linkid: 0, x: 776.6441f, y: 208f, z: -25992.69f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "8 Rings", num_rings: 8, ring_type: RingType.Line, length: 400f, radius: 0f, start_id_offset: 183, id_offset: 0, linkid: 0, x: 880.7844f, y: 1954.13f, z: -35162.68f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "5 Rings After First Turn", num_rings: 5, ring_type: RingType.Line, length: 250f, radius: 0f, start_id_offset: 191, id_offset: 0, linkid: 0, x: -419.6664f, y: 843.1299f, z: -37598.62f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "5 Rings After Spikeball", num_rings: 5, ring_type: RingType.Line, length: 250f, radius: 0f, start_id_offset: 196, id_offset: 0, linkid: 0, x: 558.0216f, y: 596.1299f, z: -38609.15f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Second Bobsled", loc_name: "4 Rings Before Last Turn", num_rings: 4, ring_type: RingType.Line, length: 200f, radius: 0f, start_id_offset: 201, id_offset: 0, linkid: 0, x: 2499f, y: 363.9998f, z: -39218.5f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Loop After Second Bobsled", loc_name: "15 Rings At Loop Center", num_rings: 15, ring_type: RingType.Arch, length: 900f, radius: 610f, start_id_offset: 205, id_offset: 0, linkid: 0, x: -48.5919f, y: 161.4598f, z: -40850.67f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "45 Rings", num_rings: 45, ring_type: RingType.Arch, length: -999f, radius: -999f, start_id_offset: 220, id_offset: -999, linkid: 0, x: -999999f, y: -999999f, z: -999999f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 1", num_rings: 1, ring_type: RingType.Normal, length: 0f, radius: 0f, start_id_offset: 220, id_offset: 1, linkid: 0, x: -253.9886f, y: 1028.187f, z: -43278.38f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 2", num_rings: 1, ring_type: RingType.Normal, length: 0f, radius: 0f, start_id_offset: 221, id_offset: 2, linkid: 0, x: -238.9886f, y: 1103.187f, z: -43274.38f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "12 Rings Vertical", num_rings: 12, ring_type: RingType.Line, length: 900f, radius: 0f, start_id_offset: 222, id_offset: 3, linkid: 0, x: -228.548f, y: 1183.187f, z: -43269.41f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 15", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 234, id_offset: 15, linkid: 0, x: -229.4876f, y: 2155.187f, z: -43199.3f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 16", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 235, id_offset: 16, linkid: 0, x: -231.635f, y: 2221.187f, z: -43196.03f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 17", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 236, id_offset: 17, linkid: 0, x: -242.8471f, y: 2281.187f, z: -43188.27f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 18", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 237, id_offset: 18, linkid: 0, x: -258.9836f, y: 2344.187f, z: -43179.12f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 19", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 238, id_offset: 19, linkid: 0, x: -285.1532f, y: 2413.187f, z: -43166.44f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 20", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 239, id_offset: 20, linkid: 0, x: -327.3402f, y: 2494.187f, z: -43149.6f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 21", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 240, id_offset: 21, linkid: 0, x: -385.2706f, y: 2563.187f, z: -43131.55f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 22", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 241, id_offset: 22, linkid: 0, x: -449.6578f, y: 2612.187f, z: -43104.93f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 23", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 242, id_offset: 23, linkid: 0, x: -526.1863f, y: 2642.187f, z: -43073.35f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 24", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 243, id_offset: 24, linkid: 0, x: -604.0718f, y: 2658.187f, z: -43044.02f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 25", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 244, id_offset: 25, linkid: 0, x: -674.9214f, y: 2655.187f, z: -43015.41f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 26", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 245, id_offset: 26, linkid: 0, x: -750.8811f, y: 2634.187f, z: -42990.12f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 27", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 246, id_offset: 27, linkid: 0, x: -823.7528f, y: 2598.187f, z: -42965.23f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 28", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 247, id_offset: 28, linkid: 0, x: -886.1153f, y: 2546.187f, z: -42940.27f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 29", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 248, id_offset: 29, linkid: 0, x: -924.1053f, y: 2486.187f, z: -42925.95f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 30", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 249, id_offset: 30, linkid: 0, x: -960.4724f, y: 2420.187f, z: -42909.03f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 31", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 250, id_offset: 31, linkid: 0, x: -976.2457f, y: 2354.187f, z: -42899.91f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 32", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 251, id_offset: 32, linkid: 0, x: -994.0707f, y: 2288.187f, z: -42891.59f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 33", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 252, id_offset: 33, linkid: 0, x: -1001.071f, y: 2221.187f, z: -42885.59f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "Ring 34", num_rings: 1, ring_type: RingType.Normal, length: 900f, radius: 410f, start_id_offset: 253, id_offset: 34, linkid: 0, x: -1003.071f, y: 2151.187f, z: -42880.69f, rule: "Has(Dark Rings)"),
        new RingData(team: Team.Dark, levelid: LevelId.SeasideHill, region: "Big Loop", loc_name: "11 Rings Vertical", num_rings: 11, ring_type: RingType.Line, length: 800f, radius: 0f, start_id_offset: 254, id_offset: 35, linkid: 0, x: -1006.071f, y: 2084.187f, z: -42875.59f, rule: "Has(Dark Rings)"),
    };
    
    public static readonly List<RingData> RoseRings = new()
    {
        

    };
    
    public static readonly List<RingData> ChaotixRings = new()
    {
        

    };
    
    public static readonly List<RingData> SuperHardModeRings = new()
    {
        

    };


    public static readonly List<RingData> AllRings = SonicRings.Concat(DarkRings).Concat(RoseRings).Concat(ChaotixRings).Concat(SuperHardModeRings).ToList();


}