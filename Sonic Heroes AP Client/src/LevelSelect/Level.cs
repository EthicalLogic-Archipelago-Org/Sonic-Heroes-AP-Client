using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.LevelSelect;

public class Level
{
    public LevelId LevelId;
    public Team? Story; //this needs to be nullable
    public bool IsBoss;
    private bool _isUnlocked;
    
    public bool GetIsUnlocked(string taskName) => _isUnlocked;
    
    public void SetIsUnlocked(bool value, string taskName)
    {
        // LoggingHandler.LogMessage($"Setting Team: {Story} Level: {LevelId} Unlocked to: {value}", taskName, LogLevel.SuperDebug);
        _isUnlocked = value;
        Mod.SaveDataHandler.WriteLevelUnlockToRedirectSaveData(LevelId, IsBoss, Story, value, taskName);
    }
    
    public void RefreshUnlockStatus(string taskName)
    {
        SetIsUnlocked(GetIsUnlocked(taskName), taskName);
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