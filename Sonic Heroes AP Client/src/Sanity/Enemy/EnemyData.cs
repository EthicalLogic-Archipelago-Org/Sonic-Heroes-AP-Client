using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Sanity.Camerons;
using Sonic_Heroes_AP_Client.Sanity.E2000s;
using Sonic_Heroes_AP_Client.Sanity.EggBishops;
using Sonic_Heroes_AP_Client.Sanity.EggFlappers;
using Sonic_Heroes_AP_Client.Sanity.EggHammers;
using Sonic_Heroes_AP_Client.Sanity.EggPawns;
using Sonic_Heroes_AP_Client.Sanity.Falcos;
using Sonic_Heroes_AP_Client.Sanity.Klagens;
using Sonic_Heroes_AP_Client.Sanity.RhinoLiners;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Enemy;

public static class EnemyData
{
    public readonly struct BaseEnemyData(Team team, LevelId levelid, StageObjTypes stageObjTypes, Vector3 spawnCoords, int enemyIndex)
    {
        public readonly Team Team = team;
        public readonly LevelId LevelId = levelid;
        public readonly StageObjTypes StageObjType = stageObjTypes;
        public readonly Vector3 SpawnCoords = spawnCoords;
        public readonly int EnemyIndex = enemyIndex;
    }
    

    public static List<BaseEnemyData> AllEnemies = 
        EggFlappersData.AllEggFlappers.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.EggFlapper, x.SpawnCoords, index))
        .Concat(EggPawnsData.AllEggPawns.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.EggPawn, x.SpawnCoords, index)))
        .Concat(KlagensData.AllKlagens.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.Klagen, x.SpawnCoords, index)))
        .Concat(FalcosData.AllFalcos.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.Falco, x.SpawnCoords, index)))
        .Concat(EggHammersData.AllEggHammers.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.EggHammer, x.SpawnCoords, index)))
        .Concat(CameronsData.AllCamerons.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.Cameron, x.SpawnCoords, index)))
        .Concat(RhinoLinersData.AllRhinoLiners.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.RhinoLiner, x.SpawnCoords, index)))
        .Concat(EggBishopsData.AllEggBishops.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.EggBishop, x.SpawnCoords, index)))
        .Concat(E2000sData.AllE2000s.Where(enemy => enemy.IdOffsetFull != StageObjData.IdOffsetInvalid).Select((x, index) => new BaseEnemyData(x.Team, x.LevelId, StageObjTypes.E2000, x.SpawnCoords, index))).ToList();
}