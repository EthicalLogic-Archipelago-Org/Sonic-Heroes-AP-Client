using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.LevelSelect;

public struct Stage
{
    private readonly LevelId _level;
    private readonly Team _story;
    private readonly Act _act;

    public override bool Equals(object? obj)
    {
        if (obj is not Stage stage)
            return false;
        return (_level == stage._level
                && _story == stage._story
                && _act == stage._act);
    }

    public Stage(LevelId level, Team story, Act act)
    { 
        _level = level;
        _story = story;
        _act = act;
    }
}