

using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.MusicShuffle;
using Sonic_Heroes_AP_Client.Sanity.BonusKeys;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.SaveData;

/// <summary>
/// A Data Class defining the Save Data for the Mod.
/// Will be serialized to a JSON and back.
/// </summary>
public class CustomSaveData
{
    
    /// <summary>
    /// The Index of the last received and handled Item.
    /// Starts at 1 as the server starts at 1.
    /// </summary>
    public int LastItemIndex = 1;
    
    /// <summary>
    /// The number of emblem items received.
    /// </summary>
    public int Emblems = 0;

    /// <summary>
    /// A mapping of Emerald to whether the item has been received.
    /// </summary>
    public Dictionary<Emerald, bool> Emeralds = Enum.GetValues<Emerald>().ToDictionary(x => x, x => false);
    
    /// <summary>
    /// An Array of bools saving if that specific boss has been unlocked.
    /// Is always length 8 regardless of how many bosses are actually generated.
    /// </summary>
    public bool[] GateBossUnlocked = new bool[8];

    /// <summary>
    /// An Array of bools saving if that specific boss has been completed.
    /// Is always length 8 regardless of how many bosses are actually generated.
    /// Will not save the completion of the Final Boss
    /// </summary>
    public bool[] GateBossComplete = new bool[8];
    
    /// <summary>
    /// A mapping of Team and LevelId to a List of bools referring to if that Bonus Key has been picked up by the Player. Has to be saved as Players can choose to not have KeySanity enabled.
    /// </summary>
    public Dictionary<Team, Dictionary<LevelId, List<bool>>> BonusKeysPickedUp = Enum.GetValues<Team>().ToDictionary(x => x, x => Enum.GetValues<LevelId>().Where(id => (int)id < 16 && (int)id > 1).ToDictionary(y => y, y =>
    {
        var amount = BonusKeyData.AllKeyPositions.Count(key => key.Team == x && key.LevelId == y);
        if (x is Team.Rose && y is  LevelId.CasinoPark)
            amount--;
        return Enumerable.Repeat(false, amount).ToList();
    }));


    /// <summary>
    /// A mapping of Team to TeamProgSaveData.
    /// Stores the progression unlocks for each Team.
    /// </summary>
    public Dictionary<Team, TeamProgSaveData> UnlockSaveData = Enum.GetValues<Team>().ToDictionary(x => x, x => new TeamProgSaveData());
    
    
    /// <summary>
    /// Mapping of Team and LevelId to a List of bools for if that specific spawn position is unlocked.
    /// </summary>
    public Dictionary<Team, Dictionary<LevelId, List<bool>>> SpawnDataUnlocks = Enum.GetValues<Team>().ToDictionary
    (
        team => team, team => Enum.GetValues<LevelId>().Where(id => ((int)id < 16 && (int)id > 1) || (int)id == 23 || (int)id == 24).ToDictionary
        (
            id => id, 
            id => 
            {
                var amount = CheckpointData.AllCheckpoints.Count(x =>
                    x.Team == team && x.LevelId == id) + 1;

                if (team is Team.Sonic && id is LevelId.GrandMetropolis)
                    amount++;
                
                var output = Enumerable.Repeat(false, amount + 1).ToList();
                
                //default spawn
                output[0] = true;
                return output;
            }
        )
    );
    
    
    
    /// <summary>
    /// A mapping of ADX file name to ADX file name from Music Randomization.
    /// Is not actually used (as the results are stored in Music Rando)
    /// </summary>
    public Dictionary<string, string> MusicRandoMapping =
        MusicShuffleData.HeroesSongs.ToDictionary(x => x.name.Split('\\').Last(), x => x.name.Split('\\').Last());


    public Dictionary<Team, Dictionary<StageObjTypes, bool>> StageObjSpawnSaveData = Enum.GetValues<Team>().ToDictionary(x => x, x => StageObjData.StageObjsToMessWith.ToDictionary(y => y, y => true));
    
}