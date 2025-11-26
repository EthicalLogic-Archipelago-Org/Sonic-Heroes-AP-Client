using System.Reflection.Emit;

namespace Sonic_Heroes_AP_Client;

public class CustomSaveData
{
    public int LastItemIndex = 1;
    public int Emblems = 0;
    
    public Dictionary<Emerald, bool> Emeralds = new ()
    {
      [Emerald.Green] = false,
      [Emerald.Blue] = false,
      [Emerald.Yellow] = false,
      [Emerald.White] = false,
      [Emerald.Cyan] = false,
      [Emerald.Purple] = false,
      [Emerald.Red] = false,
    };

    public bool[] GateBossUnlocked = new bool[8];

    public bool[] GateBossComplete = new bool[7];
    
    //public Dictionary<Team, Dictionary<LevelId, bool>> LevelsGoaled = Enum.GetValues<Team>().ToDictionary(x => x, x => Enum.GetValues<LevelId>().Where(id => (int)id < 16 && (int)id > 1).ToDictionary(y => y, y => false));
    
    public Dictionary<Team, Dictionary<LevelId, List<bool>>> BonusKeysPickedUp = Enum.GetValues<Team>().ToDictionary(x => x, x => Enum.GetValues<LevelId>().Where(id => (int)id < 16 && (int)id > 1).ToDictionary(y => y, y => Enumerable.Repeat(false, 3).ToList()));
    

    public Dictionary<Team, TeamProgSaveData>  UnlockSaveData = new()
    {
        [Team.Sonic] =  new TeamProgSaveData(),
        [Team.Dark] =  new TeamProgSaveData(),
        [Team.Rose] =  new TeamProgSaveData(),
        [Team.Chaotix] =  new TeamProgSaveData(),
        [Team.SuperHard] =  new TeamProgSaveData(),
    };

    public Dictionary<Team, Dictionary<LevelId, List<bool>>> SpawnDataUnlocks = new()
    {
        
        [Team.Sonic] = Enum.GetValues<LevelId>()
            .Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24)
            .ToDictionary(
                id => id,
                id =>
                {
                    var amount = CheckPointPriorities.AllCheckpoints.Count(x =>
                        x.Team == Team.Sonic && x.LevelId == id && !x.SuperHard) + 1;
                    if (id is LevelId.GrandMetropolis)
                        amount++;
                    return new[] { true }
                        .Concat(Enumerable.Repeat(false, amount))
                        .ToList();
                }),
        [Team.Dark] = Enum.GetValues<LevelId>()
            .Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24)
            .ToDictionary(
                id => id,
                id =>
                {
                    return new[] { true }
                        .Concat(Enumerable.Repeat(false,
                            CheckPointPriorities.AllCheckpoints.Count(x => x.Team == Team.Dark && x.LevelId == id + 1)))
                        .ToList();
                }),
        [Team.Rose] = Enum.GetValues<LevelId>()
            .Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24)
            .ToDictionary(
                id => id,
                id =>
                {
                    return new[] { true }
                        .Concat(Enumerable.Repeat(false,
                            CheckPointPriorities.AllCheckpoints.Count(x => x.Team == Team.Rose && x.LevelId == id + 1)))
                        .ToList();
                }),
        [Team.Chaotix] = Enum.GetValues<LevelId>()
            .Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24)
            .ToDictionary(
                id => id,
                id =>
                {
                    return new[] { true }
                        .Concat(Enumerable.Repeat(false,
                            CheckPointPriorities.AllCheckpoints.Count(x => x.Team == Team.Chaotix && x.LevelId == id + 1)))
                        .ToList();
                }),
        [Team.SuperHard] = Enum.GetValues<LevelId>()
            .Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24)
            .ToDictionary(
                id => id,
                id =>
                {
                    return new[] { true }
                        .Concat(Enumerable.Repeat(false,
                            CheckPointPriorities.AllCheckpoints.Count(x => x.Team == Team.Sonic && x.LevelId == id && x.SuperHard) + 1))
                        .ToList();
                }),
    };
    
    public Dictionary<string, string> MusicRandoMapping =
        MusicShuffleData.HeroesSongs.ToDictionary(x => x.name.Split('\\').Last(), x => x.name.Split('\\').Last());


    public Dictionary<Team, Dictionary<StageObjTypes, bool>> StageObjSpawnSaveData = Enum.GetValues<Team>().ToDictionary(x => x, x => StageObjHandler.StageObjsToMessWith.ToDictionary(y => y, y => true));
}

public class TeamProgSaveData
{
    public Dictionary<FormationChar, bool> CharsUnlocked = new()
    {
        [FormationChar.Speed] = false,
        [FormationChar.Flying] = false,
        [FormationChar.Power] = false,
    };
    
    /*
    public Dictionary<FormationChar, int> CharLevelUps = new()
    {
        [FormationChar.Speed] = 0,
        [FormationChar.Flying] = 0,
        [FormationChar.Power] = 0,
    };
    */

    public Dictionary<Region, Dictionary<Ability, bool>> AbilityUnlocks = new ()
    {
        //[Region.Ocean] =  new AbilityUnlockSaveData(),
        //[Region.HotPlant] =  new AbilityUnlockSaveData(),
        //[Region.Casino] =  new AbilityUnlockSaveData(),
        //[Region.Train] =  new AbilityUnlockSaveData(),
        //[Region.BigPlant] =  new AbilityUnlockSaveData(),
        //[Region.Ghost] =  new AbilityUnlockSaveData(),
        //[Region.Sky] =  new AbilityUnlockSaveData(),
        //[Region.Boss] =  new AbilityUnlockSaveData(),
        //[Region.FinalBoss] =   new AbilityUnlockSaveData(),
        
        [Region.Ocean] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.HotPlant] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.Casino] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.Train] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.BigPlant] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.Ghost] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.Sky] = Enum.GetValues<Ability>().ToDictionary(x => x, x => false),
        [Region.SpecialStage] = Enum.GetValues<Ability>().ToDictionary(x => x, x => true),
        [Region.Boss] = Enum.GetValues<Ability>().ToDictionary(x => x, x => true),
        [Region.FinalBoss] = Enum.GetValues<Ability>().ToDictionary(x => x, x => true),
    };
}