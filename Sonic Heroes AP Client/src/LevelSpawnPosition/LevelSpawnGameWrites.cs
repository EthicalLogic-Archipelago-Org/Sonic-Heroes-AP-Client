

using System.Numerics;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.LevelSpawnPosition;

public static class LevelSpawnGameWrites
{
    public static void ChangeSpawnLevelForOnSetAct(Team team, int levelSelectIndex, string taskName)
    {
        var addr = (UIntPtr)((int)Mod.ModuleBase + 0x343898 + 4 * levelSelectIndex);
        var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelSelectIndex];

        if (level is LevelId.MetalMadness)
        {
            Memory.Instance.SafeWrite(addr, [(byte)SonicHeroesDefinitions.FinalBossToLevelId[Mod.LevelSelectManager.FinalBoss]]);
            return;
        }
        
        if (LevelSpawnUnlockHandler.GetLevelSelectUiText(team, level, taskName) == "Bonus Stage")
        {
            Memory.Instance.SafeWrite(addr, [(byte)SonicHeroesDefinitions.LevelToBonusStage[level]]);
        }
        else
        {
            Memory.Instance.SafeWrite(addr, [(byte)level]);
        }
        
    }
    
    public static unsafe void ChangeSpawnPos(Team team, LevelId level, LevelSpawnEntry input, string taskName)
    {
        try
        {
            LoggingHandler.LogMessage($"Running ChangeSpawnPos: Team {team} Level {level}  X {input.Pos.X} Y {input.Pos.Y} Z {input.Pos.Z} Pitch {input.Pitch} SpawnMode {input.Mode} runningTime {input.RunningTime}", taskName, LogLevel.Debug);
            LevelSpawnUnlockHandler.TeamSpawnData* data = (LevelSpawnUnlockHandler.TeamSpawnData*) new IntPtr(GetSpawnDataPtr(team, level, taskName));
            if ((int)data < (int)Mod.ModuleBase)
            {
                LoggingHandler.LogMessage($"GetSpawnDataPtr returned a null ptr.", taskName, LogLevel.Error);
                return;
            }
            //Memory.Instance.SafeWrite(&data->XSpawnPos, BitConverter.GetBytes(x));
            data->XSpawnPos = input.Pos.X;
            data->YSpawnPos = input.Pos.Y;
            data->ZSpawnPos = input.Pos.Z;
            data->Mode = input.Mode;
            data->RunningTime = input.RunningTime;
            data->PaddingShort = input.PaddingShort;
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }

    public static unsafe LevelSpawnUnlockHandler.TeamSpawnData* GetSpawnDataPtr(Team team, LevelId level, string taskName)
    {
        try
        {
            LoggingHandler.LogMessage($"Running GetSpawnPos: Team {team} Level {level}", taskName, LogLevel.Debug);
            if (team is Team.SuperHardMode)
                team = Team.Sonic;
        
            if (team == Team.Chaotix && level == LevelId.RailCanyon)
                level = LevelId.ChaotixRailCanyon;

            if (!SonicHeroesDefinitions.LevelToSpawnDataIndex.ContainsKey(level))
            {
                LoggingHandler.LogMessage($"Level {level} does not save Spawn Pos. Defaulting to Sea Gate.", taskName, LogLevel.Error);
                level = LevelId.SeaGate;
            }
            int leveloffset = SonicHeroesDefinitions.LevelToSpawnDataIndex[level];

            var ptr = LevelSpawnData.SpawnDataStartAddr + leveloffset * 0x90 + 4 + (int)team * 0x1C;
            LevelSpawnUnlockHandler.TeamSpawnData* data = (LevelSpawnUnlockHandler.TeamSpawnData*)ptr;
            return data;

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return (LevelSpawnUnlockHandler.TeamSpawnData*)0;
    }
}