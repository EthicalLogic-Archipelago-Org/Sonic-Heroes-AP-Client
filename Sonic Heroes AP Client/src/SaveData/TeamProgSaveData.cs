using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.SaveData;

/// <summary>
/// A Data Class defining the Save Data for the Progression for an individual Team.
/// Is part of CustomSaveData and will be serialized to a JSON and back.
/// </summary>
public class TeamProgSaveData
{
    public Dictionary<FormationChar, bool> CharsUnlocked = Enum.GetValues<FormationChar>().ToDictionary(x => x, _ => false);
    
    public Dictionary<Region, Dictionary<Ability, bool>> AbilityUnlocks = 
        Enum.GetValues<Region>().ToDictionary(region => region, region => Enum.GetValues<Ability>().ToDictionary(ability => ability, _ => region > Region.Sky));
}