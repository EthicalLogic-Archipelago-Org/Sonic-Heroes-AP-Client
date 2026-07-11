using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sanity.EggFlappers;
using Sonic_Heroes_AP_Client.Sanity.EggPawns;
using Sonic_Heroes_AP_Client.Sanity.ObjSanity;
using Sonic_Heroes_AP_Client.StageObj;

namespace Sonic_Heroes_AP_Client.Sanity.Enemy;

public static class EnemySanityHandler
{
    
    private static unsafe void HandleEnemySanityStaticPtr(UIntPtr staticPtr, string taskName)
    {
        try
        {
            // var level = GameStateHandler.GetCurrentLevel(taskName);
            // var team = GameStateHandler.GetCurrentStory(taskName);
            // var act = GameStateHandler.GetCurrentAct(taskName);


            StageObjTypes objType = *(StageObjTypes*)(staticPtr + 0x28);
            
            switch (objType)
            {
                case StageObjTypes.EggFlapper:
                    EggFlappersSanityHandler.HandleEggFlapperKilledStaticPtr(staticPtr, taskName);
                    break;
                case StageObjTypes.EggPawn:
                    EggPawnsSanityHandler.HandleEggPawnKilledStaticPtr(staticPtr, taskName);
                    break;
                
                
                
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    public static unsafe void HandleEnemySanity(UIntPtr dynamicPtr, string taskName)
    {
        try
        {
            var staticPtr = *(int*)(dynamicPtr + 0x2C);
            LoggingHandler.LogMessage($"StaticPtr: 0x{staticPtr:X}", taskName, LogLevel.SuperDebug);
            HandleEnemySanityStaticPtr((UIntPtr)staticPtr, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
}