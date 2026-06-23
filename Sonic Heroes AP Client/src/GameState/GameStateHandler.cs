
using System.Numerics;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.GameState;

public static class GameStateHandler
{
    
    /// <summary>
    /// Gets the current Level that the player is in (must be in-game)
    /// </summary>
    /// <returns>LevelId or null if something went wrong</returns>
    public static unsafe LevelId? GetCurrentLevel(string taskName) 
    {
        try
        {
            var level = *(int*)(Mod.ModuleBase + 0x4D6720);
            if (level == 36)
                level = 8;
            return (LevelId)level;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return null;
    }
    
    /// <summary>
    /// Gets the current Team that the player is (must be in-game)
    /// Will return SuperHard if in Super Hard Mode
    /// </summary>
    /// <returns>Team or null if something went wrong</returns>
    public static unsafe Team? GetCurrentStory(string taskName) 
    {
        try
        {
            var team = (Team)(*(int*)(Mod.ModuleBase + 0x4D6920));
            var act = GetCurrentAct(taskName);

            if (team is Team.Sonic && act is Act.Act2 or Act.Act3 && (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode, taskName)!)
                team = Team.SuperHardMode;
        
            return team;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        
        return null;
    }

    /// <summary>
    /// Gets the current Act that the player is in (must be in-game)
    /// </summary>
    /// <returns>Act or null if something went wrong</returns>
    public static unsafe Act? GetCurrentAct(string taskName)
    {
        try
        {
            var baseAddress = *(int*)((int)Mod.ModuleBase + 0x6777E4);
            return (Act)(*(byte*)(baseAddress + 0x28));
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"Error getting current act:\n{e}", taskName, LogLevel.Error);
        }

        return null;
    }


    public static unsafe Vector3? GetCurrentLeaderPos(string taskName)
    {
        try
        {
            if (!InGame(taskName, true))
                return null;
            var leaderStructAddr = *(int*)(Mod.ModuleBase + 0x5CE820);
            var leaderPos = *(Vector3*)(leaderStructAddr + 0xE8);
            //LoggingHandler.LogMessage($"Leader Pos: [{leaderPos.X}, {leaderPos.Y}, {leaderPos.Z}]", taskName, LogLevel.Debug);
            return leaderPos;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return null;
    }
    
    
    
    /// <summary>
    /// Returns true if the player is currently in-game.
    /// </summary>
    /// /// <param name="taskName">Name of Task/Thread running this</param>
    /// <param name="canBePaused">True if being paused is allowed, False if being in-game and not paused is required</param>
    /// <returns>Bool</returns>
    public static unsafe bool InGame(string taskName, bool canBePaused = false)
    {
        try
        {
            return (*(int*)(Mod.ModuleBase + 0x4D66F0) == 5  && *(int*)((int)Mod.ModuleBase + 0x64C268) != 0) || (IsInGameAndPaused(taskName) && canBePaused);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return false;
    }

    public static unsafe bool IsInGameAndPaused(string taskName)
    {
        try
        {
            return *(int*)(Mod.ModuleBase + 0x4D66F0) == 6 && *(int*)((int)Mod.ModuleBase + 0x64C268) != 0;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return false;
    }


    public static unsafe bool IsInLevelSelect(string taskName)
    {
        try
        {
            var menuScreenIndex = *(int*)(Mod.ModuleBase + 0x4D69A4);
            return menuScreenIndex == 6;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return false;
    }
    
}