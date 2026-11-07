using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;


namespace Sonic_Heroes_AP_Client.AbilityAndCharacter;

public static class TeamRosterRandoGameWrites
{
    private static readonly UIntPtr TeamRosterRandoJumpTableAddr = Mod.ModuleBase + 0x1AAEB0;
    
    /// <summary>
    /// Switch Case Jump Table Values for each character. Note that Super Hard Mode and Super Sonic use the regular versions. Example for Sonic is: [0x7C, 0xAC, 0x5A, 0x00]
    /// </summary>
    private static readonly Dictionary<Character, ushort> TeamRosterRandoJumpTableValues = new()
    {
        {Character.Sonic, 0xAC7C},
        {Character.Knuckles, 0xAC91},
        {Character.Tails, 0xACA6},
        {Character.Shadow, 0xACBB},
        {Character.Omega, 0xACCD},
        {Character.Rouge, 0xACE2},
        {Character.Amy, 0xACF7},
        {Character.Big, 0xAD09},
        {Character.Cream, 0xAD1B},
        {Character.Espio, 0xAD2D},
        {Character.Vector, 0xAD55},
        {Character.Charmy, 0xAD7D},
    };



    public static unsafe void TeamRosterRandoInitConnect(string taskName)
    {
        TeamRosterRandoLightDashGameWrites(taskName);
        TeamRosterRandoLightAttackGameWrites(taskName);
        TeamRosterRandoWarpFlowerGameWrites(taskName);
    }
    
    
    public static void TeamRosterRandoLightDashGameWrites(string taskName)
    {
        // moveset restriction stuff
        byte[] speedCharCanLightDashAndLightAttackBytes = [0x63, 0xF0, 0x5A, 0x00];
        byte[] speedCharCanLightDashButNotLightAttackBytes = [0x7A, 0xF0, 0x5A, 0x00];
        byte[] speedCharCanNotLightDashOrLightAttackBytes = [0x87, 0xF0, 0x5A, 0x00];
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A6FC, speedCharCanLightDashAndLightAttackBytes); //Sonic
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF708, speedCharCanLightDashAndLightAttackBytes); //Shadow
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF714, speedCharCanLightDashAndLightAttackBytes); //Amy
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF720, speedCharCanLightDashAndLightAttackBytes); //Espio
        
        
        
    }
    
    public static void TeamRosterRandoLightAttackGameWrites(string taskName)
    {
        // moveset restriction stuff handled by Light Dash Writes
        
        // Light Attack needs 3 things:
        // moveset restriction stuff
        // Jump at Base + 1A6838 (make JUMP and control Jump amount to enable / disable)
        // timer at Team Ptr + 0x1FE (signed 2 bytes) (Sonic TB gives 600)
        // Sonic TB adds 600 timer at Base + 1B1D27
        // will prob need hook to add time to other teams 

        
    }



    public static void TeamRosterRandoWarpFlowerGameWrites(string taskName)
    {
        // moveset restriction stuff
        byte[] flyingCharCanOpenWarpFlowerBytes = [0xAE, 0xF4, 0x5A, 0x00];
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF704, flyingCharCanOpenWarpFlowerBytes); //Tails
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF710, flyingCharCanOpenWarpFlowerBytes); //Rouge
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1AF71C, flyingCharCanOpenWarpFlowerBytes); //Cream
    
        // 0x471E6F
        // Jump after checking if Charmy (Warp Flower Sting)
        // 0x5A62F3
        // Jump after checking if Charmy (Flower Sting Ground Attack)
    
        // 0xEB is JMP
        // 0x75 is JNE
        // enable / disable handled by Flower Sting Function
        byte[] bytes = [0xEB]; // default is { 0x75 }
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x71E6F, bytes);
        Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1A62F3, bytes);
    }
    
    
    
    
    
    public static unsafe void HandleTeamRosterRandoCallback(string taskName)
    {
        try
        {
            if (!Mod.Configuration.TeamRosterRando)
            {
                LoggingHandler.LogMessage($"Team Roster Rando is disabled, checking original bytes now", taskName, LogLevel.Debug);
                foreach (var character in Enum.GetValues<Character>())
                {
                    int addr = (int)TeamRosterRandoJumpTableAddr + 0x4 * (int)character;
                    ushort correctValue = TeamRosterRandoJumpTableValues[character];
                    ushort currentValue = *(ushort*)addr;
                    if (currentValue != correctValue)
                    {
                        LoggingHandler.LogMessage($"Writing Bytes: 0x{correctValue:X} to addr: 0x{addr:X}", taskName, LogLevel.SuperDebug);
                        Memory.Instance.SafeWrite((UIntPtr)addr, BitConverter.GetBytes(correctValue));
                    }
                    
                }
                LoggingHandler.LogMessage($"Team Roster Rando is disabled, leaving HandleTeamRosterRando", taskName,
                    LogLevel.SuperDebug);
                return;
            }

            if (Mod.Configuration.TeamRosterRandoForceRandom && !AbilityCharacterManager.IsTeamRosterRandomized)
            {
                LoggingHandler.LogMessage($"Shuffling Team Roster Rando now", taskName, LogLevel.SuperDebug);
                Character[] tempArray = Enum.GetValues<Character>();
                Random.Shared.Shuffle(tempArray);
                List<Character> tempList = tempArray.ToList();
                foreach (var pair in AbilityCharacterManager.TeamRosterRando)
                {
                    AbilityCharacterManager.TeamRosterRando[pair.Key] = tempList.First();
                    tempList.RemoveAt(0);
                }
                AbilityCharacterManager.IsTeamRosterRandomized = true;
            }


            if (!Mod.Configuration.TeamRosterRandoForceRandom)
            {
                // make sure mapping is correct
                AbilityCharacterManager.TeamRosterRando[Character.Sonic] = Mod.Configuration.TeamSonicSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Knuckles] = Mod.Configuration.TeamSonicPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Tails] = Mod.Configuration.TeamSonicFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Shadow] = Mod.Configuration.TeamDarkSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Omega] = Mod.Configuration.TeamDarkPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Rouge] = Mod.Configuration.TeamDarkFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Amy] = Mod.Configuration.TeamRoseSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Big] = Mod.Configuration.TeamRosePowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Cream] = Mod.Configuration.TeamRoseFlyingCharacterRando;
                
                AbilityCharacterManager.TeamRosterRando[Character.Espio] = Mod.Configuration.TeamChaotixSpeedCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Vector] = Mod.Configuration.TeamChaotixPowerCharacterRando;
                AbilityCharacterManager.TeamRosterRando[Character.Charmy] = Mod.Configuration.TeamChaotixFlyingCharacterRando;
            }
            
            
            
            
            LoggingHandler.LogMessage($"Handling Team Roster Rando now", taskName, LogLevel.SuperDebug);
            
            foreach (var pair in AbilityCharacterManager.TeamRosterRando)
            {
                int addr = (int)TeamRosterRandoJumpTableAddr + 0x4 * (int)pair.Key;
                ushort correctValue = TeamRosterRandoJumpTableValues[pair.Value];
                LoggingHandler.LogMessage($"Writing Bytes: 0x{correctValue:X} to addr: 0x{addr:X}", taskName, LogLevel.SuperDebug);
                Memory.Instance.SafeWrite((UIntPtr)addr, BitConverter.GetBytes(correctValue));
            }
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
    
    
    
    
    
    
    
    
    
}