using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity;

public record StageObjDataBase(Team Team, LevelId LevelId, string Region, string LocName, int Group, int IdOffsetGroup, int IdOffsetFull, byte LinkID, Vector3 SpawnCoords, StageObjTypes StageObjType);