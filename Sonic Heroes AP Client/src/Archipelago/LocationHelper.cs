using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sanity;
using Sonic_Heroes_AP_Client.Sanity.Rings;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class LocationHelper
{
    private static readonly Dictionary<string, string> SuperSecretLocationNameMapping = new()
    {
        { $"{GetRegularSanityLocationName(Team.Rose, LevelId.CasinoPark, Act.Act1, true, "Bonus Key", 4, "StaticImport")}", $"SUPER SECRET HIDDEN Bonus Key" },
        { $"{GetRegularSanityLocationName(Team.Rose, LevelId.CasinoPark, Act.Act1, false, "Bonus Key", 4, "StaticImport")}", $"SUPER SECRET HIDDEN Act A Bonus Key" },
        { $"{GetRegularSanityLocationName(Team.Rose, LevelId.CasinoPark, Act.Act2, false, "Bonus Key", 4, "StaticImport")}", $"SUPER SECRET HIDDEN Act B Bonus Key" },
    };
    
    private static string GetLocationNamePieceForStageTeam(Team team, LevelId levelId, string taskName)
    {
        string result = $"{SonicHeroesDefinitions.LevelIdToLocationNamePiece[levelId]} {Enum.GetName(team)}";
        return result;
    }

    
    private static string GetLocationNamePieceForAct(Act act, string taskName)
    {
        return act switch
        {
            Act.Act1 => "Act A",
            Act.Act2 => "Act B",
            Act.Act3 => "Act 3 In GetLocationNamePieceForAct",
            _ => "Default Act In GetLocationNamePieceForAct"
        };
    }


    
    private static string GetLevelGoalLocationName(Team team, LevelId levelId, Act act, string taskName)
    {
        return team switch
        {
            Team.SuperHardMode => $"{GetLocationNamePieceForStageTeam(team, levelId, taskName)}",
            _ => $"{GetLocationNamePieceForStageTeam(team, levelId, taskName)} {GetLocationNamePieceForAct(act, taskName)}"
        };
    }

    public static void SendLevelGoalLocation(Team team, LevelId levelId, Act act, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetLevelGoalLocationName(team, levelId, act, taskName), taskName);
    }
    
    public static bool IsLevelGoalLocationSent(Team team, LevelId levelId, Act act, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetLevelGoalLocationName(team, levelId, act, taskName), taskName);
    }

    
    
    private static string GetBossGoalLocationName(Team team, LevelId levelId, string taskName)
    {
        return team switch
        {
            Team.SuperHardMode => $"{GetLocationNamePieceForStageTeam(Team.Sonic, levelId, taskName)}",
            _ => $"{GetLocationNamePieceForStageTeam(team, levelId, taskName)}"
        };
    }

    public static void SendBossGoalLocation(Team team, LevelId levelId, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetBossGoalLocationName(team, levelId, taskName), taskName);
    }
    
    public static bool IsBossGoalLocationSent(Team team, LevelId levelId, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetBossGoalLocationName(team, levelId, taskName), taskName);
    }
    

    
    private static string GetEmeraldLocationName(LevelId levelId, string taskName)
    {
        try
        {
            return $"{SonicHeroesDefinitions.LevelIdToLocationNamePiece[SonicHeroesDefinitions.BonusStageToLevelId[levelId]]} Emerald";
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return "INVALID LOCATION NAME HERE";
    }
    
    public static void SendEmeraldLocation(LevelId levelId, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetEmeraldLocationName(levelId, taskName), taskName);
    }
    
    public static bool IsEmeraldLocationSent(LevelId levelId, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetEmeraldLocationName(levelId, taskName), taskName);
    }
    
    
    
    private static string GetDarkObjSanityLocationName(LevelId levelId, int enemyNumber, string taskName)
    {
        return $"{GetLocationNamePieceForStageTeam(Team.Dark, levelId, taskName)} {GetLocationNamePieceForAct(Act.Act2, taskName)} Enemies Killed: {enemyNumber}";
    }
    
    public static void SendDarkObjSanityLocation(LevelId levelId, int enemyNumber, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetDarkObjSanityLocationName(levelId, enemyNumber, taskName), taskName);
    }
    
    public static bool IsDarkObjSanityLocationSent(LevelId levelId, int enemyNumber, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetDarkObjSanityLocationName(levelId, enemyNumber, taskName), taskName);
    }
    
    
    
    private static string GetRoseObjSanityLocationName(LevelId levelId, int ringNumber, string taskName)
    {
        return $"{GetLocationNamePieceForStageTeam(Team.Rose, levelId, taskName)} {GetLocationNamePieceForAct(Act.Act2, taskName)} Rings Collected: {ringNumber}";
    }
    
    public static void SendRoseObjSanityLocation(LevelId levelId, int ringNumber, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetRoseObjSanityLocationName(levelId, ringNumber, taskName), taskName);
    }
    
    public static bool IsRoseObjSanityLocationSent(LevelId levelId, int ringNumber, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetRoseObjSanityLocationName(levelId, ringNumber, taskName), taskName);
    }
    
    
    
    private static string GetChaotixObjSanityLocationName(LevelId levelId, Act act, int amount, string taskName)
    {
        int maxSanityChecks;
        string flavorText = "";
        try
        {
            switch (act)
            {
                case Act.Act1:
                    maxSanityChecks = LevelTracker.ChaotixsanityData[(int)levelId].Act1Max;
                    if (levelId is LevelId.MysticMansion)
                    {
                        flavorText = "Red ";
                    }
                    break;
                case Act.Act2:
                    maxSanityChecks = LevelTracker.ChaotixsanityData[(int)levelId].Act2Max;
                    if (levelId is LevelId.MysticMansion)
                    {
                        flavorText = "Blue ";
                    }
                    break;
                default:
                    maxSanityChecks = 0;
                    break;
            }

            if (amount < 1 || amount > maxSanityChecks)
            {
                LoggingHandler.LogMessage($"Checking Chaotix ObjSanity Location in {levelId} Act {act} Amount {amount}", taskName, LogLevel.Debug);
                return $"INVALID LOCATION NAME";
            }
            
            return $"{GetLocationNamePieceForStageTeam(Team.Chaotix, levelId, taskName)} {GetLocationNamePieceForAct(act, taskName)} {flavorText}{LevelTracker.ChaotixsanityData[(int)levelId].FlavorText}: {amount}";
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
        return $"INVALID LOCATION NAME";
    }
    
    public static void SendChaotixObjSanityLocation(LevelId levelId, Act act, int amount, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetChaotixObjSanityLocationName(levelId, act, amount, taskName), taskName);
    }
    
    public static bool IsChaotixObjSanityLocationSent(LevelId levelId, Act act, int amount, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetChaotixObjSanityLocationName(levelId, act, amount, taskName), taskName);
    }

    
    
    private static string GetRegularSanityLocationName(Team team, LevelId levelId, Act act, bool oneSet, string flavorText, int number, string taskName)
    {
        return oneSet ? $"{GetLocationNamePieceForStageTeam(team, levelId, taskName)} {flavorText} {number}" : $"{GetLocationNamePieceForStageTeam(team, levelId, taskName)} {GetLocationNamePieceForAct(act, taskName)} {flavorText} {number}";
    }

    
    
    private static string GetKeySanityLocationName(Team team, LevelId levelId, Act act, bool oneSet, int number, string taskName)
    {
        string result = GetRegularSanityLocationName(team, levelId, act, oneSet, "Bonus Key", number, taskName);
        if (SuperSecretLocationNameMapping.TryGetValue(result, out string? value))
        {
            result = value;
        }
        return result;
    }
    
    public static void SendKeySanityLocation(Team team, LevelId levelId, Act act, int number, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetKeySanityLocationName(team: team, levelId: levelId, act: act, oneSet: true, number: number, taskName: taskName), taskName);
        Mod.ArchipelagoHandler.CheckLocation(GetKeySanityLocationName(team: team, levelId: levelId, act: act, oneSet: false, number: number, taskName: taskName), taskName);
    }
    
    public static bool IsKeySanityLocationSent(Team team, LevelId levelId, Act act, bool oneSet, int number, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetKeySanityLocationName(team, levelId, act, oneSet, number, taskName), taskName);
    }


    private static string GetCheckpointSanityLocationName(Team team, LevelId levelId, Act act, bool oneSet, int number, string taskName)
    {
        string result = GetRegularSanityLocationName(team, levelId, act, oneSet, "Checkpoint", number, taskName);
        if (SuperSecretLocationNameMapping.TryGetValue(result, out string? value))
        {
            result = value;
        }
        return result;
    }
    
    public static void SendCheckpointSanityLocation(Team team, LevelId levelId, Act act, int number, string taskName)
    {
        // bool oneSetEnabled = (bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName, oneSet: true)!;
        // bool bothActsEnabled = team is not Team.SuperHardMode && (bool)Mod.LevelSelectManager.IsThisSanityEnabled((Team)team, SanityType.CheckpointSanity, taskName, bothActs: true)!;
        Mod.ArchipelagoHandler.CheckLocation(GetCheckpointSanityLocationName(team: team, levelId: levelId, act: act, oneSet: true, number: number, taskName: taskName), taskName);
        Mod.ArchipelagoHandler.CheckLocation(GetCheckpointSanityLocationName(team: team, levelId: levelId, act: act, oneSet: false, number: number, taskName: taskName), taskName);
    }
    
    public static bool IsCheckpointSanityLocationSent(Team team, LevelId levelId, Act act, bool oneSet, int number, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetCheckpointSanityLocationName(team, levelId, act, oneSet, number, taskName), taskName);
    }
    
    

    private static string GetMetalMadnessFillerLocationName(string taskName)
    {
        return $"Metal Overlord";
    }
    
    public static void SendMetalMadnessFillerLocation(string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetMetalMadnessFillerLocationName(taskName), taskName);
    }
    
    public static bool IsMetalMadnessFillerLocationSent(string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetMetalMadnessFillerLocationName(taskName), taskName);
    }
    
    
    
    private static string GetStageObjSanityGroupLocationName(StageObjDataBase stageObjDataBase, Act act, bool oneSet, string taskName)
    {
        string group = stageObjDataBase is RingsData.RingData ? " Ring Group" : " Group";
        
        string result = oneSet ? $"{GetLocationNamePieceForStageTeam(stageObjDataBase.Team, stageObjDataBase.LevelId, taskName)} {stageObjDataBase.LocName}{group}" : $"{GetLocationNamePieceForStageTeam(stageObjDataBase.Team, stageObjDataBase.LevelId, taskName)} {GetLocationNamePieceForAct(act, taskName)} {stageObjDataBase.LocName}{group}";
        
        if (SuperSecretLocationNameMapping.TryGetValue(result, out string? value))
        {
            result = value;
        }
        return result;
    }
    
    public static void SendStageObjSanityGroupLocation(StageObjDataBase stageObjDataBase, Act act, string taskName)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetStageObjSanityGroupLocationName(stageObjDataBase, act, true, taskName), taskName);
        Mod.ArchipelagoHandler.CheckLocation(GetStageObjSanityGroupLocationName(stageObjDataBase, act, false, taskName), taskName);
    }
    
    public static bool IsStageObjSanityGroupLocationSent(StageObjDataBase stageObjDataBase, Act act, bool oneSet, string taskName)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetStageObjSanityGroupLocationName(stageObjDataBase, act, oneSet, taskName), taskName);
    }
    
    
    
    
    private static string GetStageObjSanityFullLocationName(StageObjDataBase stageObjDataBase, Act act, bool oneSet, string taskName, int ringNum = 0)
    {
        string ringNumStr = stageObjDataBase is RingsData.RingData ? $" Ring {ringNum}" : "";
        string result = oneSet ? $"{GetLocationNamePieceForStageTeam(stageObjDataBase.Team, stageObjDataBase.LevelId, taskName)} {stageObjDataBase.LocName}{ringNumStr}" : $"{GetLocationNamePieceForStageTeam(stageObjDataBase.Team, stageObjDataBase.LevelId, taskName)} {GetLocationNamePieceForAct(act, taskName)} {stageObjDataBase.LocName}{ringNumStr}";
        
        if (SuperSecretLocationNameMapping.TryGetValue(result, out string? value))
        {
            result = value;
        }
        return result;
    }
    
    public static void SendStageObjSanityFullLocation(StageObjDataBase stageObjDataBase, Act act, string taskName, int ringNum = 0)
    {
        Mod.ArchipelagoHandler.CheckLocation(GetStageObjSanityFullLocationName(stageObjDataBase, act, true, taskName, ringNum), taskName);
        Mod.ArchipelagoHandler.CheckLocation(GetStageObjSanityFullLocationName(stageObjDataBase, act, false, taskName, ringNum), taskName);
    }
    
    public static bool IsStageObjSanityFullLocationSent(StageObjDataBase stageObjDataBase, Act act, bool oneSet, string taskName, int ringNum = 0)
    {
        return Mod.ArchipelagoHandler.IsLocationChecked(GetStageObjSanityFullLocationName(stageObjDataBase, act, oneSet, taskName, ringNum), taskName);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}