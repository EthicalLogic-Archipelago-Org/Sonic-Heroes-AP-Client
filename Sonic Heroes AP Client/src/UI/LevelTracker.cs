

using System.Drawing;
using System.Numerics;
using DearImguiSharp;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.LevelSelect;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;
using Sonic_Heroes_AP_Client.LevelUnlocking;
using Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;
using Sonic_Heroes_AP_Client.Sanity.BonusKeys;
using Sonic_Heroes_AP_Client.Sanity.Checkpoints;
using Region = Sonic_Heroes_AP_Client.Definitions.Region;

namespace Sonic_Heroes_AP_Client.UI;

internal class ChaotixSanityData
{
    public LevelId Level;
    public string Type;
    public int Act1Max;
    public int Act2Max;
    public long Act1Offset;
    public long Act2Offset;

    public ChaotixSanityData(LevelId level, string type, int act1Max, int act2Max, long act1Offset, long act2Offset)
    {
        Level = level;
        Type = type;
        Act1Max = act1Max;
        Act2Max = act2Max;
        Act1Offset = act1Offset;
        Act2Offset = act2Offset;
    }
}

public class LevelTracker
{
    private Dictionary<int, ChaotixSanityData> _chaotixsanityData = new()
    {
        {
            (int)LevelId.SeasideHill, 
            new ChaotixSanityData(LevelId.SeasideHill, "Crabs", 10, 20, 0x11B8, 0x11C2)
        },
        {
            (int)LevelId.OceanPalace, 
            new ChaotixSanityData(LevelId.OceanPalace, "N/A", 0, 0, 0, 0)
        },
        {
            (int)LevelId.GrandMetropolis, 
            new ChaotixSanityData(LevelId.GrandMetropolis, "Enemies", 85, 85, 0x11D6, 0x122B)
        },
        {
            (int)LevelId.PowerPlant, 
            new ChaotixSanityData(LevelId.PowerPlant, "Turtles", 3, 5, 0x1280, 0x1283)
        },
        {
            (int)LevelId.CasinoPark, 
            new ChaotixSanityData(LevelId.CasinoPark, "Rings", 200, 500, 0x1288, 0x1350)
        },
        {
            (int)LevelId.BingoHighway, 
            new ChaotixSanityData(LevelId.BingoHighway, "Chips", 10, 20, 0x1544, 0x154E)
        },
        {
            (int)LevelId.RailCanyon, 
            new ChaotixSanityData(LevelId.RailCanyon, "N/A", 0, 0, 0, 0)
        },
        {
            (int)LevelId.BulletStation, 
            new ChaotixSanityData(LevelId.BulletStation, "Capsules", 30, 50, 0x1562, 0x1580)
        },
        {
            (int)LevelId.FrogForest, 
            new ChaotixSanityData(LevelId.FrogForest, "N/A", 0, 0, 0, 0)
        },
        {
            (int)LevelId.LostJungle, 
            new ChaotixSanityData(LevelId.LostJungle, "Chao", 10, 20, 0x15B2, 0x15BC)
        },
        {
            (int)LevelId.HangCastle, 
            new ChaotixSanityData(LevelId.HangCastle, "Keys", 10, 10, 0x15d0, 0x15da)
        },
        {
            (int)LevelId.MysticMansion, 
            new ChaotixSanityData(LevelId.MysticMansion, "Torches", 60, 46, 0x15e4, 0x1620)
        },
        {
            (int)LevelId.EggFleet, 
            new ChaotixSanityData(LevelId.EggFleet, "N/A", 0, 0, 0, 0)
        },
        {
            (int)LevelId.FinalFortress, 
            new ChaotixSanityData(LevelId.FinalFortress, "Keys", 5, 10, 0x164e, 0x1653)
        },
    };
    
    private IntPtr _drawList;
    private float _col1Centre;
    private float _col2Centre;
    private float _circRadius;
    private float _outerWidth;
    private float _outerHeight;
    private float _uiScale;
    private float _windowWidth;
    private float _windowHeight;
    private float _windowPosX;
    private float _windowPosY;
    
    public unsafe void Draw(float outerWidth, float outerHeight, float uiScale)
    {
        try
        {
            if (Mod.ArchipelagoHandler?.SlotData == null)
                return;

            if (!(GameStateHandler.IsInLevelSelect() || GameStateHandler.IsInGameAndPaused()))
                return;

            SetUpTrackerWindow(outerWidth, outerHeight, uiScale);
            
            //get level and team
            var team = GetTeamForLevelTracker();
            var levelId = GetLevelForLevelTracker();

            //draw based on level and team
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            
            HandleRegionAndGateDisplayForLevel(team, levelId);
            ImGui.SetWindowFontScale(_uiScale + 0.3f);
            
            switch (levelId)
            {
                case LevelId.MetalMadness or LevelId.MetalOverlord or LevelId.SeaGate:
                    HandleFinalBossLayout();
                    break;
                case >= LevelId.EggHawk and < LevelId.MetalMadness:
                    HandleBossLayout(team, levelId);
                    break;
                case >= LevelId.SeasideHill and <= LevelId.FinalFortress:
                    HandleRegularLevelLayout(team, levelId);
                    break;
                case >= LevelId.BonusStage1 and <= LevelId.BonusStage7:
                case >= LevelId.EmeraldStage1 and <= LevelId.EmeraldStage7:
                    HandleBonusEmeraldStageLayout(levelId);
                    break;
                default:
                    Console.WriteLine($"{levelId} in Level Tracker Draw(). HOW DID WE GET HERE???");
                    break;
            }
            
            ImGui.End();
            ImGui.__Internal.PopStyleColor(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private unsafe void SetUpTrackerWindow(float outerWidth, float outerHeight, float uiScale)
    {
        try
        {
            _outerHeight = outerHeight;
            _outerWidth = outerWidth;
            _uiScale = uiScale;
            _windowWidth = 0.35f * _outerWidth;
            _windowHeight = 0.55f * _outerHeight;
            _windowPosX = _outerWidth - _windowWidth;
            _windowPosY = 0.0f;
            var trackerPos = new ImVec2.__Internal { x = _windowPosX, y = _windowPosY };
            var trackerSize = new ImVec2.__Internal { x = _windowWidth, y = _windowHeight };
            var trackerPivot = new ImVec2.__Internal { x = 0, y = 0 };
            ImGui.__Internal.SetNextWindowPos(trackerPos, (int)ImGuiCond.Always, trackerPivot);
            ImGui.__Internal.SetNextWindowSize(trackerSize, (int)ImGuiCond.Always);
            ImGui.__Internal.PushStyleColorU32((int)ImGuiCol.WindowBg, 0xC0000000);
            ImGui.__Internal.Begin("Tracker", null,
                (int)(ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize));
            ImGui.SetWindowFontScale(uiScale + 0.3f);

            _drawList = ImGui.__Internal.GetWindowDrawList();
            _col1Centre = _windowWidth / 3;
            _col2Centre = 2 * _col1Centre;
            _circRadius = 5 * uiScale;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    

    /// <summary>
    /// WILL NOT RETURN SUPERHARD IF IN LVL SELECT
    /// </summary>
    /// <returns></returns>
    private unsafe Team GetTeamForLevelTracker()
    { 
        try
        {
            if (GameStateHandler.InGame(true)) 
                return (Team)GameStateHandler.GetCurrentStory()!;
            
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var storyIndex = *(int*)(levelSelectPtr + 0x220);
            return (Team)storyIndex;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return Team.Sonic;
    }
    
    private unsafe LevelId GetLevelForLevelTracker()
    { 
        try
        {
            if (GameStateHandler.InGame(true)) 
                return (LevelId)GameStateHandler.GetCurrentLevel()!;
            
            var levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
            var levelIndex = *(int*)(levelSelectPtr + 0x194);
            return (LevelId)SonicHeroesDefinitions.LevelTrackerUILevelMapping[levelIndex];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return LevelId.SeaGate;
    }

    private unsafe void WriteCenteredText(string text, Color color = default, bool largerText = false)
    {
        try
        {
            if (largerText)
                ImGui.SetWindowFontScale(_uiScale + 0.5f);
            else
                ImGui.SetWindowFontScale(_uiScale + 0.3f);
            var textsize = new ImVec2.__Internal();
            ImGui.__Internal.CalcTextSize((IntPtr)(&textsize), text, null,false,-1.0f);
            ImGui.SetCursorPosX(_windowWidth / 2 - textsize.x / 2);
            if (color == Color.Empty)
            {
                ImGui.Text(text);
            }
            else
            {
                ImGui.TextColored(GetImVec4FromVec4(GetVec4FromColor(color)), text);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private void HandleFinalBossLayout()
    {
        try
        {
            var bossCompleteId = SonicHeroesDefinitions.MetalMadnessId;
            var isBossComplete = Mod.ArchipelagoHandler.IsLocationChecked(bossCompleteId);
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius, isBossComplete);
            ImGui.NewLine();
            var text = $"";
            
            //If individual teams info needed:
            if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.AbilityCharacterUnlocks ||
                Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.LevelCompletionsPerStory))
            {
                foreach (var team in Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!))
                {
                    text = $"Team {team} Requirements:";
                    WriteCenteredText(text, Color.Empty);
                    
                    //characters are required if they are items
                    if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.AbilityCharacterUnlocks)
                    {
                            text = $"Characters Unlocked:{AbilityCharacterManager.GetLevelSelectUIStringForCharUnlocksForTeam(team)}";
                            WriteCenteredText(text, Color.Empty);
                    }

                    //level completions per story
                    if (Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.LevelCompletionsPerStory))
                    {
                        text = $"Level Completions: {Mod.LevelSelectManager.GetCompletedLevelsForTeam(team)} / {Mod.ArchipelagoHandler.SlotData.GoalLevelCompletionsPerStory}";
                        WriteCenteredText(text, Color.Empty);
                    }
                }
            }
            
            //Level Completions (All Teams)
            if (Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.LevelCompletionsAllTeams))
            {
                var levelcompletions = Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!).Sum(team => Mod.LevelSelectManager.GetCompletedLevelsForTeam(team));
                var levelcompletionsneeded = Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions;
                
                text = $"Total Level Completions For All Teams: {levelcompletions} / {levelcompletionsneeded}";
                WriteCenteredText(text, Color.Empty);
            }
            
            //emblems
            if (Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.Emblems))
            {
                var gateIndex = Mod.LevelSelectManager.FindGateForLevel(LevelId.MetalMadness, Team.Sonic);
                if (gateIndex != null)
                {
                    text = $"Emblems: {Mod.SaveDataHandler.CustomSaveData!.Emblems} / {Mod.LevelSelectManager.GateData[(int)gateIndex].BossCost}";
                    WriteCenteredText(text, Color.Empty);
                }
            }
            
            //emeralds
            if (Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.Emeralds))
            {
                foreach (var emerald in Enum.GetValues<Emerald>())
                {
                    text = $"{emerald.ToString()} Chaos Emerald";
                    Color textColor = Mod.SaveDataHandler.CustomSaveData.Emeralds[emerald]
                        ? Color.Green
                        : Color.Red;
                    WriteCenteredText(text, textColor);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private void HandleBonusEmeraldStageLayout(LevelId level)
    {
        try
        {
            //no checks for bonus stages yet (only emerald)
            if (level < LevelId.EmeraldStage1)
                return;
            var emeraldLocationID = SonicHeroesDefinitions.EmeraldStartId + level - LevelId.EmeraldStage1;
            var emeraldCompleted = Mod.ArchipelagoHandler.IsLocationChecked(emeraldLocationID);
            
            WriteCenteredText($"{(Emerald)(level - LevelId.EmeraldStage1)} Chaos Emerald Location");
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius, emeraldCompleted); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
    
    private void HandleBossLayout(Team team, LevelId level)
    {
        try
        {
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            if (gate == null)
                return;
            
            var bossCompleteId = 0xA0 + (int)team * 42 + 28 + ((int)level - 16) * 2;
            var isBossComplete = Mod.ArchipelagoHandler.IsLocationChecked(bossCompleteId);
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius, isBossComplete);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void HandleRegularLevelLayout(Team team, LevelId level)
    {
        try
        {
            switch (team)
            {
                case Team.Sonic or Team.SuperHardMode:
                    HandleSonicOrSuperHardLayout(level);
                    break;
                case Team.Dark:
                    HandleDarkLayout(level);
                    break;
                case Team.Rose:
                    HandleRoseLayout(level);
                    break;
                case Team.Chaotix:
                    HandleChaotixLayout(level);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
    
    
    private void HandleEmeraldStageForRegularLevelLayout(Team team, LevelId level)
    {
        try
        {
            var text = $"{(Emerald)(level - LevelId.EmeraldStage1)} Chaos Emerald Location";
            var cursorPos = new ImVec2();
            var emeraldLocationID = SonicHeroesDefinitions.EmeraldStartId + level - LevelId.EmeraldStage1;
            var emeraldStageComplete = Mod.ArchipelagoHandler.IsLocationChecked(emeraldLocationID);
            WriteCenteredText(text, Color.Empty);
            ImGui.GetCursorScreenPos(cursorPos);
            DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius, emeraldStageComplete);
            ImGui.NewLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private unsafe void HandleSonicOrSuperHardLayout(LevelId level)
    {
        try
        {
            const Team team = Team.Sonic;
            var text = $"";
            var textSize = new ImVec2.__Internal();
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            var superHardGate = Mod.LevelSelectManager.FindGateForLevel(level, Team.SuperHardMode);
            if (gate == null && superHardGate == null)
                return;
            
            var act1CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0;
            var act2CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 1;
            var isAct1Complete = Mod.ArchipelagoHandler.IsLocationChecked(act1CompleteId);
            var isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);
            

            var shouldRightSideBeDrawn = Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2) || (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode)!;
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                text = "Act 1";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col1Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col1Centre - textSize.x / 2 - 4 * _circRadius, cursorPos.Y + 2 * _circRadius, _circRadius, isAct1Complete);
            }
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1) && shouldRightSideBeDrawn)
                ImGui.__Internal.SameLine(0, 0);
            
            if (shouldRightSideBeDrawn)
            {
                if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
                {
                    //Sonic
                    text = "Act 2";
                }
                else
                {
                    //SuperHard
                    text = "SuperHard Mode (Act 2)";
                    act2CompleteId = SonicHeroesDefinitions.SuperHardModeId + ((int)level - 2);
                    isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);
                }
                
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col2Centre + textSize.x / 2 + 4 * _circRadius, cursorPos.Y + 2 * _circRadius, _circRadius, isAct2Complete);
                
            }
            if ((int)level % 2 == 1 && level is < LevelId.EggHawk and > LevelId.Unk2)
                //emerald stage here
                HandleEmeraldStageForRegularLevelLayout(team, SonicHeroesDefinitions.LevelToBonusStage[level]);
            
            HandleKeySanityLayout(team, level);
            HandleCheckpointSanityLayout(team, level);
            HandleSpawnPos(team, level);
            HandleCharDisplayForTeam(team);
            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var region))
                return;
            HandleAbilityDisplayForRegion(team, region);
            
            //ObjSanity
            //there is none
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private unsafe void HandleDarkLayout(LevelId level)
    {
        try
        {
            const Team team = Team.Dark;
            var text = $"{level}";
            var textSize = new ImVec2.__Internal();
            var cursorPos = new ImVec2();
            
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            if (gate == null)
                return;
            
            var act1CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0;
            var act2CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 1;
            var isAct1Complete = Mod.ArchipelagoHandler.IsLocationChecked(act1CompleteId);
            var isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);
            ImGui.GetCursorScreenPos(cursorPos);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                text = "Act 1";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col1Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col1Centre - textSize.x / 2 - 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct1Complete);
            }
            
            if ((bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, true)!)
                ImGui.__Internal.SameLine(0, 0);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
            {
                text = "Act 2";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col2Centre + textSize.x / 2 + 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct2Complete);
            }
            if ((int)level % 2 == 1 && level is < LevelId.EggHawk and > LevelId.Unk2)
                //emerald stage here
                HandleEmeraldStageForRegularLevelLayout(team, SonicHeroesDefinitions.LevelToBonusStage[level]);
            
            
            HandleKeySanityLayout(team, level);
            HandleCheckpointSanityLayout(team, level);
            HandleSpawnPos(team, level);
            HandleCharDisplayForTeam(team);
            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var region))
                return;
            HandleAbilityDisplayForRegion(team, region);
            
            //ObjSanity
            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.Dark, SanityType.ObjSanity)!)
                return;
            if (!Mod.LevelSelectManager.IsThisTeamActEnabled(Team.Dark, Act.Act2))
                return;
            var sanityLevelOffset = SonicHeroesDefinitions.DarkObjSanityStartId + ((int)level - 2) * 100;
            var sanityMax = 100 / Mod.ArchipelagoHandler.SlotData.DarksanityCheckSize;
            var sanityChecked =
                Mod.ArchipelagoHandler.CountLocationsCheckedInRange(sanityLevelOffset, sanityLevelOffset + 100);
            HandleSanityLayout("Enemies", sanityChecked, sanityMax, _windowWidth / 2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private unsafe void HandleRoseLayout(LevelId level)
    {
        try
        {
            const Team team = Team.Rose;
            var textSize = new ImVec2.__Internal();
            var cursorPos = new ImVec2();
            var text = $"";
            
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            if (gate == null)
                return;
            
            var act1CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0;
            var act2CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 1;
            var isAct1Complete = Mod.ArchipelagoHandler.IsLocationChecked(act1CompleteId);
            var isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);
            ImGui.GetCursorScreenPos(cursorPos);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                text = "Act 1";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col1Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col1Centre - textSize.x / 2 - 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct1Complete);
            }
            
            if ((bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, true)!)
                ImGui.__Internal.SameLine(0, 0);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
            {
                text = "Act 2";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col2Centre + textSize.x / 2 + 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct2Complete);
            }
            if ((int)level % 2 == 1 && level is < LevelId.EggHawk and > LevelId.Unk2)
                //emerald stage here
                HandleEmeraldStageForRegularLevelLayout(team, SonicHeroesDefinitions.LevelToBonusStage[level]);
            
            HandleKeySanityLayout(team, level);
            HandleCheckpointSanityLayout(team, level);
            HandleSpawnPos(team, level);
            HandleCharDisplayForTeam(team);
            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var region))
                return;
            HandleAbilityDisplayForRegion(team, region);
            
            //ObjSanity
            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ObjSanity)!)
                return;
            if (!Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
                return;
            var sanityLevelOffset = SonicHeroesDefinitions.RoseObjSanityStartId + ((int)level - 2) * 200;
            var sanityMax = 200 / Mod.ArchipelagoHandler.SlotData.RosesanityCheckSize;
            var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(sanityLevelOffset, sanityLevelOffset + 200);
            HandleSanityLayout("Rings", sanityChecked, sanityMax, _windowWidth / 2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        
    }
    
    private unsafe void HandleChaotixLayout(LevelId level)
    {
        try
        {
            const Team team = Team.Chaotix;
            var text = $"";
            var textSize = new ImVec2.__Internal();
            var cursorPos = new ImVec2();
            
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            if (gate == null)
                return;
            
            var act1CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 0;
            var act2CompleteId = 0xA0 + (int)team * 42 + ((int)level - 2) * 2 + 1;
            var isAct1Complete = Mod.ArchipelagoHandler.IsLocationChecked(act1CompleteId);
            var isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);
            ImGui.GetCursorScreenPos(cursorPos);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                text = "Act 1";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col1Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col1Centre - textSize.x / 2 - 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct1Complete);
            }
            
            if ((bool)Mod.LevelSelectManager.IsThisTeamEnabled(team, true)!)
                ImGui.__Internal.SameLine(0, 0);
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
            {
                text = "Act 2";
                ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                ImGui.Text(text);
                DrawCircle(_drawList, _windowPosX + _col2Centre + textSize.x / 2 + 4 * _circRadius,
                    cursorPos.Y + 2 * _circRadius, _circRadius, isAct2Complete);
            }
            if ((int)level % 2 == 1 && level is < LevelId.EggHawk and > LevelId.Unk2)
                //emerald stage here
                HandleEmeraldStageForRegularLevelLayout(team, SonicHeroesDefinitions.LevelToBonusStage[level]);
            
            HandleKeySanityLayout(team, level);
            HandleCheckpointSanityLayout(team, level);
            //spawn pos not done yet
            //HandleSpawnPos(team, level);
            HandleCharDisplayForTeam(team);
            if (!SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var region))
                return;
            HandleAbilityDisplayForRegion(team, region);
            
            //ObjSanity
            var chaotixData = _chaotixsanityData[(int)level];
            if (!(bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.ObjSanity)!)
                return;

            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                var sanityMax = chaotixData.Act1Max;
                var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(chaotixData.Act1Offset, chaotixData.Act1Offset + chaotixData.Act1Max);
                if (level == LevelId.CasinoPark)
                    sanityMax /= Mod.ArchipelagoHandler.SlotData.ChaotixsanityRingCheckSize;
                HandleSanityLayout(chaotixData.Type, sanityChecked, sanityMax, _col1Centre - 0.05f * _windowWidth);
            }
            
            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1) && Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
                ImGui.__Internal.SameLine(0, 0);

            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
            {
                var sanityMax = chaotixData.Act2Max;
                var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(chaotixData.Act2Offset, chaotixData.Act2Offset + chaotixData.Act2Max);
                if (level == LevelId.CasinoPark)
                    sanityMax /= Mod.ArchipelagoHandler.SlotData.ChaotixsanityRingCheckSize;
                HandleSanityLayout(chaotixData.Type, sanityChecked, sanityMax, _col2Centre + 0.05f * _windowWidth);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void HandleRegionAndGateDisplayForLevel(Team team, LevelId level)
    {
        try
        {
            var textSize = new ImVec2.__Internal();
            //var text = $"";
            
            var emblemCost = 0;
            
            var gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
            //var gateStr = gate == null ? "null" : gate.ToString();

            var levelStr = $"{level}";
            var regionStr = $"";
            var firstTextLine = $"";
            
            var gateStr = $"";
            var emblemStr = $"";
            var previousBossStr = $"";
            var unlockRequirementsStr = $"";
            var secondTextLine = $"";
            
            //Seaside Hill - Ocean Region
            //Gate 0 - 0 Emblems and Boss To Unlock
            
            if (SonicHeroesDefinitions.LevelIdToRegion.TryGetValue(level, out var reg))
            {
                if (reg <= Region.Sky)
                    regionStr = $"{reg} Region";
            }
            
            switch (level)
            {
                case LevelId.MetalMadness or LevelId.MetalOverlord or LevelId.SeaGate:
                    levelStr = $"Final Boss: {SonicHeroesDefinitions.FinalBossToLevelId[Mod.LevelSelectManager.FinalBoss]}";
                    
                    gate = Mod.LevelSelectManager.FindGateForLevel(LevelId.MetalMadness, team);
                    
                    if (Mod.LevelSelectManager.GoalUnlockConditions.HasFlag(GoalUnlockConditions.Emblems))
                        emblemCost = Mod.LevelSelectManager.GateData[(int)gate!].BossCost;
                    else if (gate > 0)
                        emblemCost = Mod.LevelSelectManager.GateData[(int)gate - 1].BossCost;
                    
                    if (gate > 0)
                        previousBossStr += $"{Mod.LevelSelectManager.GateData[(int)gate - 1].BossLevel.LevelId}";
                    break;
                
                case >= LevelId.EggHawk and <= LevelId.EggEmperor:
                    //boss here
                    levelStr = $"Boss: {level}";
                    gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
                    if (gate == null)
                        break;
                    
                    emblemCost = Mod.LevelSelectManager.GateData[(int)gate!].BossCost;
                    if (gate > 0)
                        previousBossStr += $"{Mod.LevelSelectManager.GateData[(int)gate - 1].BossLevel.LevelId}";
                    break;
                
                case >= LevelId.BonusStage1 and <= LevelId.BonusStage7:
                case >= LevelId.EmeraldStage1 and <= LevelId.EmeraldStage7:
                    var regularLevel = SonicHeroesDefinitions.BonusStageToLevelId[level];
                    var minGateNum = Enum.GetValues<Team>().Where(x => (bool)Mod.LevelSelectManager.IsThisTeamEnabled(x)!).Select(t => Mod.LevelSelectManager.FindGateForLevel(regularLevel, t)).OfType<int>().Select(gatenum => gatenum).Prepend(SonicHeroesDefinitions.GateLimit).Min();
                    emblemCost = SonicHeroesDefinitions.EmblemCostLimit;
                    gate = minGateNum;
                    switch (minGateNum)
                    {
                        case SonicHeroesDefinitions.GateLimit:
                            //cry
                            gate = null;
                            break;
                        case > 0:
                            emblemCost = Mod.LevelSelectManager.GateData[minGateNum - 1].BossCost;
                            previousBossStr = $"{Mod.LevelSelectManager.GateData[minGateNum - 1].BossLevel.LevelId}";
                            break;
                        case 0:
                            emblemCost = 0;
                            break;
                    }
                    break;
                
                case >= LevelId.SeasideHill and <= LevelId.FinalFortress:
                    //regular level here
                    gate = Mod.LevelSelectManager.FindGateForLevel(level, team);
                    if (gate == null)
                        break;
                    
                    if (gate > 0)
                    {
                        emblemCost = Mod.LevelSelectManager.GateData[(int)gate - 1].BossCost;
                        previousBossStr += $"{Mod.LevelSelectManager.GateData[(int)gate - 1].BossLevel.LevelId}";
                    }
                    break;
                
                default:
                    //HOW DID WE GET HERE
                    break;
            }
            
            gateStr = gate == null ? $"Not In Rando" : $"Gate {gate}";
            
            if (emblemCost > 0)
                emblemStr = $"{emblemCost} Emblems";
            
            unlockRequirementsStr = gate switch
            {
                null => $"",
                0 when emblemCost == 0 => $"Unlocked From Start",
                0 => $"{emblemStr} Required",
                _ => emblemCost == 0
                    ? $"{previousBossStr} Required"
                    : $"{emblemStr} and {previousBossStr} Required"
            };

            firstTextLine = string.IsNullOrEmpty(regionStr) ? $"{levelStr}" : $"{levelStr} - {regionStr}";
            WriteCenteredText(firstTextLine, Color.Empty, true);
            secondTextLine = string.IsNullOrEmpty(unlockRequirementsStr) ? $"{gateStr}" : $"{gateStr} - {unlockRequirementsStr}";
            WriteCenteredText(secondTextLine, Color.Empty, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private unsafe void HandleKeySanityLayout(Team team, LevelId level)
    {
        try
        {
            //Super Hard does not have keys
            if (!(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team)!)
                return;
            if (!((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.KeySanity)! || (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.KeySanity, true)!))
                return;
            
            var text = $"Bonus Keys";
            var textSize = new ImVec2.__Internal();
            
            List<KeyPosition> keylist = BonusKeyData.AllKeyPositions.Where(key => key.Team == team && key.LevelId == level).ToList();
            if (keylist.Count == 0)
                return;
            //Super Secret Hidden Bonus Key
            if (keylist.Count == 4)
                keylist.RemoveAt(3);
            var bonusKeyOffset = BonusKeyData.AllKeyPositions.IndexOf(keylist[0]); 
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            WriteCenteredText(text, Color.Empty);
            
            ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
            var windowCentre = _outerWidth - _windowWidth / 2;
            var textWidth = textSize.x / 2;
            var textStart = _windowWidth / 2 - textWidth;
            var textEnd = _windowWidth / 2 + textWidth;

            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1))
            {
                //draw left circles
                for (var i = 0; i < keylist.Count; i++)
                {
                    var bonusKeyChecked = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.KeySanity)! 
                        //1 set
                        ? Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.BonusKeyNoActStartId + bonusKeyOffset + i) 
                        //both sets
                        : Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.BonusKeyAct1StartId + bonusKeyOffset + i);
            
                    DrawCircle(_drawList,_windowPosX + textStart - 4 * _circRadius * (keylist.Count + 1) + 4 * _circRadius * i, cursorPos.Y + 2 * _circRadius,
                        _circRadius, bonusKeyChecked);
                }
            }

            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
            {
                //draw right circles
                for (var i = 0; i < keylist.Count; i++)
                {
                    var bonusKeyChecked = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.KeySanity)! 
                        //1 set
                        ? Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.BonusKeyNoActStartId + bonusKeyOffset + i) 
                        //both sets
                        : Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.BonusKeyAct2StartId + bonusKeyOffset + i);
            
                    DrawCircle(_drawList,_windowPosX + textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius, _circRadius, bonusKeyChecked);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private unsafe void HandleCheckpointSanityLayout(Team team, LevelId level)
    {
        try
        {
            if (team is Team.SuperHardMode)
                team = Team.Sonic;

            if (team is Team.Sonic)
            {
                if (!((bool)Mod.LevelSelectManager.IsThisTeamEnabled(team)! || (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode)!))
                    return;
                
                if (!((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity)! ||
                      (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity, true)! ||
                      (bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.SuperHardMode, SanityType.CheckpointSanity)!))
                    return;
            }
            else
            {
                if (!(bool)Mod.LevelSelectManager.IsThisTeamEnabled(team)!)
                    return;
                if (!((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity)! || (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity, true)!))
                    return;
            }
            
            var text = $"Checkpoints";
            var textSize = new ImVec2.__Internal();
            
            var checkpointlist = CheckpointData.AllCheckpoints.Where(checkpoint => checkpoint.Team == team && checkpoint.LevelId == level).ToList();
            var superhardcheckpointlist = CheckpointData.AllCheckpoints.Where(checkpoint => checkpoint.Team is Team.SuperHardMode && checkpoint.LevelId == level).ToList();
            var checkpointOffset = CheckpointData.AllCheckpoints.IndexOf(checkpointlist[0]); 
            var superhardcheckpointOffset = CheckpointData.AllCheckpoints.IndexOf(superhardcheckpointlist[0]);
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            WriteCenteredText(text, Color.Empty);
            
            ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
            var windowCentre = _outerWidth - _windowWidth / 2;
            var textWidth = textSize.x / 2;
            var textStart = _windowWidth / 2 - textWidth;
            var textEnd = _windowWidth / 2 + textWidth;
            
            //if not Sonic, then draw if Act 1 enabled (already checked sanity enabled)
            //if Sonic, then draw if Sonic Sanity on and Act 1 enabled
            if (team is not Team.Sonic && Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1) || (team is Team.Sonic && ((bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity)! || (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity, true)!) && Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act1)))
            {
                //draw left circles
                for (var i = 0; i < checkpointlist.Count; i++)
                {
                    var checkpointChecked = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity)! 
                        //1 set
                        ? Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.CheckpointNoActStartId + checkpointOffset + i) 
                        //both sets
                        : Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.CheckpointAct1StartId + checkpointOffset + i);
            
                    DrawCircle(_drawList,_windowPosX + textStart - 4 * _circRadius * (checkpointlist.Count + 1) + 4 * _circRadius * i, cursorPos.Y + 2 * _circRadius, _circRadius, checkpointChecked);
                }
            }

            if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2) || (team is Team.Sonic && (bool)Mod.LevelSelectManager.IsThisTeamEnabled(Team.SuperHardMode)! && (bool)Mod.LevelSelectManager.IsThisSanityEnabled(Team.SuperHardMode, SanityType.CheckpointSanity)!))
            {
                if (Mod.LevelSelectManager.IsThisTeamActEnabled(team, Act.Act2))
                {
                    //Sonic Here
                    for (var i = 0; i < checkpointlist.Count; i++)
                    {
                        var checkpointChecked = (bool)Mod.LevelSelectManager.IsThisSanityEnabled(team, SanityType.CheckpointSanity)! 
                            //1 set
                            ? Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.CheckpointNoActStartId + checkpointOffset + i) 
                            //both sets
                            : Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.CheckpointAct2StartId + checkpointOffset + i);
                        DrawCircle(_drawList, _windowPosX + textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius, _circRadius, checkpointChecked);
                    }
                }
                else
                {
                    for (var i = 0; i < superhardcheckpointlist.Count; i++)
                    {
                        //SuperHard here
                        var checkpointChecked = Mod.ArchipelagoHandler.IsLocationChecked(SonicHeroesDefinitions.CheckpointAct2StartId + superhardcheckpointOffset + i);
                        DrawCircle(_drawList, _windowPosX + textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius,
                            _circRadius, checkpointChecked);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    private unsafe void HandleSanityLayout(string sanityText, int sanityChecked, int sanityMax, float posX)
    {
        try
        {
            var textSize = new ImVec2.__Internal();
            ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"{sanityText} {sanityChecked}/{sanityMax}", null, false, -1.0f);
            ImGui.SetCursorPosX(posX - textSize.x / 2);
            ImGui.Text($"{sanityText} {sanityChecked}/{sanityMax}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    private void HandleSpawnPos(Team team, LevelId level)
    {
        try
        {
            var text = $"Spawn Position: - {LevelSpawnUnlockHandler.GetLevelSelectUiText(team, level)}";
            WriteCenteredText(text, Color.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    private void HandleCharDisplayForTeam(Team team)
    {
        try
        {
            if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
                return;
            
            var text = $"Characters Unlocked:{AbilityCharacterManager.GetLevelSelectUIStringForCharUnlocksForTeam(team)}";
            WriteCenteredText(text, Color.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    private void HandleAbilityDisplayForRegion(Team team, Region region)
    {
        try
        {
            if (Mod.ArchipelagoHandler.SlotData.EntireRunUnlockType is EntireRunUnlockType.LegacyLevelGates)
                return;
            var text = "";
            List<Ability> teamAbilities = AbilityCharacterManager.GetAbilitiesForTeam(team);
            foreach (var ability in teamAbilities)
            {
                text = ability.ToString();
                Color textColor =
                    Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability]
                        ? Color.Green
                        : Color.Red;

                if (ability is Ability.Flight && textColor == Color.Green &&
                    !Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][
                        Ability.Thundershoot])
                {
                    textColor = Color.Orange;
                }

                if (ability is Ability.TriangleJump && textColor == Color.Green &&
                    !Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][
                        Ability.HomingAttack])
                {
                    textColor = Color.Orange;
                }

                if (ability is Ability.RocketAccel && textColor == Color.Green &&
                    !(Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Power] ||
                      Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].CharsUnlocked[FormationChar.Flying]))
                {
                    //need second character for rocket accel
                    textColor = Color.Orange;
                }
                WriteCenteredText(text, textColor);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    private void DrawCircle(IntPtr drawList, float x, float y, float radius, bool complete)
    {
        var center = new ImVec2.__Internal { x = x, y = y };
        if (complete)
            ImGui.__Internal.ImDrawListAddCircleFilled(drawList, center, radius, 0xffffffff, 16);
        else
            ImGui.__Internal.ImDrawListAddCircle(drawList, center, radius, 0xffffffff, 16, radius * 0.25f);
    }
    
    
    private ImVec4 GetImVec4FromVec4(Vector4 color)
    {
        ImVec4 result = new ImVec4();
        result.W = color.W;
        result.X = color.X;
        result.Y = color.Y;
        result.Z = color.Z;
        return result;
    }

    private Vector4 GetVec4FromColor(Color color)
    {
        return new Vector4(color.R, color.G, color.B, color.A);
    }
    
}