using System.Numerics;
using Sonic_Heroes_AP_Client.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.StageObj.DashPanel;

public static class DashPanelHandler
{

    public static void HandleSonicDashPanelAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {

        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void HandleDarkDashPanelAfterBackup(LevelId level, Act act, string taskName)
    {
        try
        {
            HandleDashPanelSpecialCases(Team.Dark, level, act, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static void HandleDashPanelSpecialCases(Team team, LevelId level, Act act, string taskName)
    {
        try
        {
            switch (team)
            {
                case Team.Dark when level is LevelId.SeasideHill:
                    //Dash Panels to Unspawn if cant bypass easily
                    List<Vector3> dashPanelsToUnSpawn =
                    [
                        //before first loop
                        new Vector3(-27.0046f, 30f, -4897f),
                        new Vector3(1.9955f, 30f, -4911f),
                        new Vector3(30.9955f, 30f, -4897f),
                        //after first loop
                        new Vector3(105f, 30f, -5345f),
                        new Vector3(130f, 30f, -5370f),
                        new Vector3(156f, 30.1880f, -5345f),
                        //after first loop (at incline before eggmans robots)
                        new Vector3(102.0002f, 32.3710f, -5930.8240f),    //(prob not needed) <- should keep this one
                        new Vector3(129.0002f, 34.2878f, -5955.8240f),
                        new Vector3(154.0002f, 32.3710f, -5930.8240f),
                        //before loop after corner cave
                        new Vector3(-4533f, 30f, -7536f),
                        new Vector3(-4508f, 30f, -7551f),
                        new Vector3(-4483f, 30f, -7536f),
                        //after loop after corner cave
                        new Vector3(-4548.4070f, 30f, -8035.0040f),
                        new Vector3(-4509.4070f, 30f, -8035.0040f),
                        new Vector3(-4472f, 30.0896f, -8035.0040f),
                    ];
                    
                    foreach (StageObjSpawnData spawnData in StageObjHandler.GetInLevelObjsOfType(StageObjTypes.DashPanel, taskName))
                    {
                        DashPanelSpawnData dashPanel = spawnData as DashPanelSpawnData;
                        foreach (Vector3 spawnPos in dashPanelsToUnSpawn)
                        {
                            if (dashPanel.IsAtPosition(spawnPos, taskName))
                            {
                                dashPanel.SpawnOrDespawnObj(AbilityCharacterManager.CanBypassDashPanelInBadSpot(team, SonicHeroesDefinitions.LevelIdToRegion[level], taskName), taskName);
                            }
                        }
                    }
                    break;
                
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    
    
    
    
}