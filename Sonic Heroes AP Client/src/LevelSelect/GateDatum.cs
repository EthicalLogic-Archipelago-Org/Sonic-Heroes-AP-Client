using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.LevelSelect;

public class GateDatum
{
    private readonly SlotData _slotData;
    public int Index { get; private set; }
    public int BossCost;
    public List<Level> Levels;
    public Level BossLevel;
    private bool _isUnlocked;
    
    public bool GetIsUnlocked(string taskName) => _isUnlocked;

    public void SetIsUnlocked(bool value, string taskName)
    {
        // LoggingHandler.LogMessage($"Setting Gate Datum Index: {Index} Unlocked to: {value} Actual Index: {Mod.LevelSelectManager.GateData.IndexOf(this)}", taskName, LogLevel.SuperDebug);
        _isUnlocked = value;
        foreach (var level in Levels)
            level.SetIsUnlocked(value, taskName);
    }

    public GateDatum Next()
    {
        return Index != Mod.LevelSelectManager.GateData.Count - 1 ? Mod.LevelSelectManager.GateData[Index + 1] : this;
    }

    public GateDatum Previous()
    {
        return Index != 0 ? Mod.LevelSelectManager.GateData[Index - 1] : this;
    }
    
    public void RefreshUnlockStatus(string taskName)
    {
        SetIsUnlocked(GetIsUnlocked(taskName), taskName);
    }

    public GateDatum(SlotData slotData, int index, int bossCost, string[] levelIndices, string bossLevel)
    {
        _slotData = slotData;
        Index = index;
        BossCost = bossCost;
        Levels = new List<Level>();
        foreach(var levelIndex in levelIndices)
            Levels.Add(new Level(levelIndex));
        BossLevel = new Level(bossLevel);
    }
}