
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.LevelUnlocking;

public class Level
{
    public LevelId LevelId;
    public Team? Story; //this needs to be nullable
    public bool IsBoss;
    private bool _isUnlocked;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set
        {
            _isUnlocked = value;
            Mod.SaveDataHandler.WriteLevelUnlockToRedirectSaveData(LevelId, IsBoss, Story, value);
        }
    }
    
    public void RefreshUnlockStatus()
    {
        IsUnlocked = _isUnlocked; 
    }
    
    public Level(string levelCode)
    {
        var storyId = levelCode[0].ToString().ToLower();
        Story = storyId switch
        {
            "s" => Team.Sonic,
            "d" => Team.Dark,
            "r" => Team.Rose,
            "c" => Team.Chaotix,
            "b" => null,
            _ => Team.Sonic
        };
        LevelId = (LevelId)int.Parse(levelCode[1..]);
        IsBoss = Story == null;
    }
}