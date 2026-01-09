

using System.Numerics;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.LevelSpawnPosition;

public static class LevelSpawnGameWrites
{
    public static void ChangeSpawnLevelForOnSetAct(Team team, int levelSelectIndex)
    {
        try
        {
            var addr = (UIntPtr)((int)Mod.ModuleBase + 0x343898 + 4 * levelSelectIndex);
            var level = (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelSelectIndex];

            if (level is LevelId.MetalMadness)
            {
                Memory.Instance.SafeWrite(addr, [(byte)SonicHeroesDefinitions.FinalBossToLevelId[Mod.LevelSelectManager.FinalBoss]]);
                return;
            }
            
            if (LevelSpawnUnlockHandler.GetLevelSelectUiText(team, level) == "Bonus Stage")
            {
                Memory.Instance.SafeWrite(addr, [(byte)SonicHeroesDefinitions.LevelToBonusStage[level]]);
            }
            else
            {
                Memory.Instance.SafeWrite(addr, [(byte)level]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static unsafe void ChangeSpawnPos(Team team, LevelId level, LevelSpawnEntry input)
    {
        try
        {
            //Console.WriteLine($"Running ChangeSpawnPos: Team {team} Level {level}  X {x} Y {y} Z {z} Pitch {pitch} SpawnMode {mode} runningTime {runningTime}");
            LevelSpawnUnlockHandler.TeamSpawnData* data = (LevelSpawnUnlockHandler.TeamSpawnData*) new IntPtr(GetSpawnDataPtr(team, level));

            if ((int)data == 0)
            {
                Console.WriteLine($"GetSpawnDataPtr returned a null ptr.");
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
            Console.WriteLine(e);
        }
    }

    public static unsafe LevelSpawnUnlockHandler.TeamSpawnData* GetSpawnDataPtr(Team team, LevelId level)
    {
        try
        {
            //Console.WriteLine($"Running GetSpawnPos: Team {team} Level {level}");
            if (team is Team.SuperHardMode)
                team = Team.Sonic;
        
            if (team == Team.Chaotix && level == LevelId.RailCanyon)
                level = LevelId.ChaotixRailCanyon;

            if (!SonicHeroesDefinitions.LevelToSpawnDataIndex.ContainsKey(level))
            {
                //Console.WriteLine($"Level {level} does not save Spawn Pos. Defaulting to Sea Gate.");
                level = LevelId.SeaGate;
            }
            int leveloffset = SonicHeroesDefinitions.LevelToSpawnDataIndex[level];

            var ptr = Mod.SpawnDataStartAddr + leveloffset * 0x90 + 4 + (int)team * 0x1C;
            LevelSpawnUnlockHandler.TeamSpawnData* data = (LevelSpawnUnlockHandler.TeamSpawnData*)ptr;
            //Console.WriteLine($"Spawn Pos Ptr Here: 0x{ptr:x}");

            return data;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return (LevelSpawnUnlockHandler.TeamSpawnData*)0;
    }
}