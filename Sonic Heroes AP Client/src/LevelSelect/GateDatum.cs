using Sonic_Heroes_AP_Client.Archipelago;

namespace Sonic_Heroes_AP_Client.LevelUnlocking;

public class GateDatum
{
    private readonly SlotData _slotData;
    public int Index { get; private set; }
    public int BossCost;
    public List<Level> Levels;
    public Level BossLevel;
    private bool _isUnlocked;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set
        {
            _isUnlocked = value;
            foreach (var level in Levels)
                level.IsUnlocked = value;
        }
    }

    public GateDatum Next()
    {
        return Index != Mod.LevelSelectManager.GateData.Count - 1 ? Mod.LevelSelectManager.GateData[Index + 1] : this;
    }

    public GateDatum Previous()
    {
        return Index != 0 ? Mod.LevelSelectManager.GateData[Index - 1] : this;
    }
    
    public void RefreshUnlockStatus()
    {
        IsUnlocked = _isUnlocked;
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