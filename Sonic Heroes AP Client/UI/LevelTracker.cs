using System.Drawing;
using System.Numerics;
using DearImguiSharp;
using Reloaded.Imgui.Hook;

namespace Sonic_Heroes_AP_Client;

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
    public readonly Dictionary<int, int> LevelMapping = new()
    {
        {0, (int)LevelId.SeasideHill},
        {1, (int)LevelId.OceanPalace},
        {2, (int)LevelId.EggHawk},
        {3, (int)LevelId.GrandMetropolis},
        {4, (int)LevelId.PowerPlant},
        {5, (int)LevelId.TeamFight1},
        {6, (int)LevelId.CasinoPark},
        {7, (int)LevelId.BingoHighway},
        {8, (int)LevelId.RobotCarnival},
        {9, (int)LevelId.RailCanyon},
        {10, (int)LevelId.BulletStation},
        {11, (int)LevelId.EggAlbatross},
        {12, (int)LevelId.FrogForest},
        {13, (int)LevelId.LostJungle},
        {14, (int)LevelId.TeamFight2},
        {15, (int)LevelId.HangCastle},
        {16, (int)LevelId.MysticMansion},
        {17, (int)LevelId.RobotStorm},
        {18, (int)LevelId.EggFleet},
        {19, (int)LevelId.FinalFortress},
        {20, (int)LevelId.EggEmperor},
        {21, (int)LevelId.MetalMadness}
    };
    
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
            var menuScreenIndex = *(int*)(Mod.ModuleBase + 0x4D69A4);
            var inGamePaused = *(byte*)(Mod.ModuleBase + 0x4D6708);
            if (Mod.ArchipelagoHandler?.SlotData == null || Mod.SanityHandler == null)
                return;
            if (menuScreenIndex != 6)
                if (inGamePaused != 0x01)
                    return;

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
            var textSize = new ImVec2.__Internal();


            IntPtr levelSelectPtr = 0x0;
            var levelIndex = 0;
            var storyIndex = 0;


            if (inGamePaused != 0x01)
            {
                levelSelectPtr = *(IntPtr*)(Mod.ModuleBase + 0x6777B4);
                levelIndex = *(int*)(levelSelectPtr + 0x194);
                if ((levelIndex is < 0 or > 21))
                {
                    ImGui.End();
                    ImGui.__Internal.PopStyleColor(1);
                    return;
                }
                
                levelIndex = LevelMapping[levelIndex];
                storyIndex = *(int*)(levelSelectPtr + 0x194 + 0x8C);
            }
            var templevelIndex = levelIndex;

            if (inGamePaused == 0x01)
            {
                levelIndex = (int)Mod.GameHandler!.GetCurrentLevel();
                storyIndex = (int)Mod.GameHandler.GetCurrentStory();

                if (levelIndex == (int)LevelId.MetalOverlord)
                    templevelIndex = (int)LevelId.MetalMadness;

                else
                    templevelIndex = levelIndex;

            }

            var gate = Mod.ArchipelagoHandler.SlotData.FindGateForLevel((LevelId)templevelIndex, (Team)storyIndex);
            var gateStr = gate == null ? "?" : gate.ToString();
            ImGui.SetWindowFontScale(uiScale + 0.5f);
            ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), $"{(LevelId)levelIndex} - Gate {gateStr}", null, false,
                -1.0f);
            ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
            ImGui.Text($"{(LevelId)levelIndex} - Gate {gateStr}");
            ImGui.SetWindowFontScale(uiScale + 0.3f);

            switch (levelIndex)
            {
                case < 2 or > 24:
                    ImGui.End();
                    ImGui.__Internal.PopStyleColor(1);
                    return;
                case < 16:
                {
                    var act1CompleteId = 0xA0 + storyIndex * 42 + (levelIndex - 2) * 2 + 0;
                    var act2CompleteId = 0xA0 + storyIndex * 42 + (levelIndex - 2) * 2 + 1;
                    var isAct1Complete = Mod.ArchipelagoHandler.IsLocationChecked(act1CompleteId);
                    var isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);

                    var cursorPos = new ImVec2();
                    ImGui.GetCursorScreenPos(cursorPos);

                    if (Mod.ArchipelagoHandler.SlotData.StoriesActive[(Team)storyIndex] is MissionsActive.Act1
                        or MissionsActive.Both)
                    {
                        ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), "Act 1", null, false, -1.0f);
                        ImGui.SetCursorPosX(_col1Centre - textSize.x / 2);
                        ImGui.Text("Act 1");
                        DrawCircle(_drawList, _windowPosX + _col1Centre - textSize.x / 2 - 4 * _circRadius,
                            cursorPos.Y + 2 * _circRadius, _circRadius, isAct1Complete);
                    }

                    if (Mod.ArchipelagoHandler.SlotData.StoriesActive[(Team)storyIndex] is MissionsActive.Act2
                        or MissionsActive.Both)
                    {
                        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[(Team)storyIndex] is MissionsActive.Act1
                            or MissionsActive.Both)
                            ImGui.__Internal.SameLine(0, 0);

                        //Super Hard Mode Sonic Act 2 here
                        if (storyIndex == (int)Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2)
                        {
                            ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), "SuperHard (Act 2)", null, false, -1.0f);
                            ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                            ImGui.Text("SuperHard (Act 2)");

                            act2CompleteId = GameHandler.SuperHardModeId + (levelIndex - 2);
                            isAct2Complete = Mod.ArchipelagoHandler.IsLocationChecked(act2CompleteId);

                        }
                        else
                        {
                            ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), "Act 2", null, false, -1.0f);
                            ImGui.SetCursorPosX(_col2Centre - textSize.x / 2);
                            ImGui.Text("Act 2");

                        }

                        DrawCircle(_drawList, _windowPosX + _col2Centre + textSize.x / 2 + 4 * _circRadius,
                            cursorPos.Y + 2 * _circRadius, _circRadius, isAct2Complete);
                    }

                    if (levelIndex % 2 == 1 && levelIndex < 16)
                    {
                        var emeraldCompleteId = 0x148 + ((int)levelIndex - 2) / 2;
                        var emeraldStageComplete = Mod.ArchipelagoHandler.IsLocationChecked(emeraldCompleteId);
                        ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), "Emerald Stage", null, false, -1.0f);
                        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
                        ImGui.Text("Emerald Stage");
                        //var cursorPos = new ImVec2();
                        ImGui.GetCursorScreenPos(cursorPos);
                        DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius,
                            emeraldStageComplete);
                        ImGui.NewLine();
                    }

                    ImGui.GetCursorScreenPos(cursorPos);

                    switch (storyIndex)
                    {
                        case (int)Team.Sonic:
                            HandleSonicLayout((LevelId)levelIndex);
                            break;
                        case (int)Team.Dark:
                            HandleDarkLayout((LevelId)levelIndex);
                            break;
                        case (int)Team.Rose:
                            HandleRoseLayout((LevelId)levelIndex);
                            break;
                        case (int)Team.Chaotix:
                            HandleChaotixLayout((LevelId)levelIndex);
                            break;
                    }

                    break;
                }
                default:
                    HandleBossLayout((Team)storyIndex, (LevelId)levelIndex);
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

    private unsafe void HandleSonicLayout(LevelId level)
    {
        try
        {
            HandleKeySanity(Team.Sonic, level);
            HandleCheckpointSanity(Team.Sonic, level);
            if (!GameHandler.LevelIdToRegion.TryGetValue(level, out Region region))
                return;
            HandleSpawnPos(Team.Sonic, level);
            HandleCharDisplayForTeam(Team.Sonic);
            HandleAbilityDisplayForRegion(Team.Sonic, region);
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
            HandleKeySanity(Team.Dark, level);
            HandleCheckpointSanity(Team.Dark, level);
            if (!Mod.ArchipelagoHandler.SlotData.IsDarksanityActive)
                return;
            if (Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Dark] is MissionsActive.None or MissionsActive.Act1)
                return;
            var sanityLevelOffset = 0x150 + ((int)level - 2) * 100;
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
        HandleKeySanity(Team.Rose, level);
        HandleCheckpointSanity(Team.Rose, level);
        if (!Mod.ArchipelagoHandler.SlotData.IsRosesanityActive)
            return;
        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Rose] is MissionsActive.None or MissionsActive.Act1)
            return;
        var sanityLevelOffset = 0x6C8 + ((int)level - 2) * 200;
        var sanityMax = 200 / Mod.ArchipelagoHandler.SlotData.RosesanityCheckSize;
        var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(sanityLevelOffset, sanityLevelOffset + 200);
        HandleSanityLayout("Rings", sanityChecked, sanityMax, _windowWidth / 2);
    }
    
    private unsafe void HandleChaotixLayout(LevelId level)
    {
        HandleKeySanity(Team.Chaotix, level);
        HandleCheckpointSanity(Team.Chaotix, level);
        var chaotixData = _chaotixsanityData[(int)level];
        if (!Mod.ArchipelagoHandler.SlotData.IsChaotixsanityActive)
            return;
        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Chaotix] is MissionsActive.Act1 or MissionsActive.Both)
        {
            var sanityMax = chaotixData.Act1Max;
            var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(chaotixData.Act1Offset, chaotixData.Act1Offset + chaotixData.Act1Max);
            if (level == LevelId.CasinoPark)
                sanityMax /= Mod.ArchipelagoHandler.SlotData.ChaotixsanityRingCheckSize;
            HandleSanityLayout(chaotixData.Type, sanityChecked, sanityMax, _col1Centre - 0.05f * _windowWidth);
        }
        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Chaotix] is MissionsActive.Act2 or MissionsActive.Both)
        {
            if (Mod.ArchipelagoHandler.SlotData.StoriesActive[Team.Chaotix] is MissionsActive.Act1 or MissionsActive.Both)
                ImGui.__Internal.SameLine(0, 0);
            var sanityMax = chaotixData.Act2Max;
            var sanityChecked = Mod.ArchipelagoHandler.CountLocationsCheckedInRange(chaotixData.Act2Offset, chaotixData.Act2Offset + chaotixData.Act2Max);
            if (level == LevelId.CasinoPark)
                sanityMax /= Mod.ArchipelagoHandler.SlotData.ChaotixsanityRingCheckSize;
            HandleSanityLayout(chaotixData.Type, sanityChecked, sanityMax, _col2Centre + 0.05f * _windowWidth);
        }
    }

    private unsafe void HandleKeySanity(Team team, LevelId level)
    {
        //KeySanity
        if (Mod.ArchipelagoHandler!.SlotData.KeySanityDict[team] == 0)
            return;

        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.None)
            return;

        if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && team is Team.Sonic &&
            Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2)
            return;
        
        var act1StartId = KeySanityPositions.Act1StartId;
        var act2StartId = KeySanityPositions.Act2StartId;
        var noActStartId = KeySanityPositions.NoActStartId;
        
        var keysinlevel = from key in KeySanityPositions.AllKeyPositions
            where key.Team == team && key.LevelId == level
            select key;

        List<KeyPosition> keylist = keysinlevel.ToList();

        if (keylist.Count == 0)
            return;
        
        //Super Secret Hidden Bonus Key
        if (keylist.Count == 4)
            keylist.RemoveAt(3);
        
        var bonusKeyOffset = KeySanityPositions.AllKeyPositions.IndexOf(keylist[0]); 
        /*
        var bonusKeyOffset = 300;
        foreach (var keydata in keylist)
        {
            if (KeySanityPositions.AllKeyPositions.IndexOf(keydata) < bonusKeyOffset)
                bonusKeyOffset = KeySanityPositions.AllKeyPositions.IndexOf(keydata);
        }
        */
        
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        
        var textSize = new ImVec2.__Internal();
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), "Bonus Keys", null, false, -1.0f);
            
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);

        ImGui.Text("Bonus Keys");


        if ((Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 2 &&
            Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act1 or MissionsActive.Both)
            || (Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 1 && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && team is Team.Sonic) || (Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 1 && Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act1 or MissionsActive.Both))
        {
            //draw left circles
            var windowCentre = _outerWidth - _windowWidth / 2;

            var textWidth = textSize.x / 2;

            var textStart = windowCentre - textWidth;

            for (int i = 0; i < keylist.Count; i++)
            {
                var bonusKeyChecked = Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 1 
                    ? Mod.ArchipelagoHandler.IsLocationChecked(noActStartId + bonusKeyOffset + i) 
                    : Mod.ArchipelagoHandler.IsLocationChecked(act1StartId + bonusKeyOffset + i);
                
                DrawCircle(_drawList, textStart - 4 * _circRadius * (keylist.Count + 1) + 4 * _circRadius * i, cursorPos.Y + 2 * _circRadius,
                    _circRadius, bonusKeyChecked);
            }
        }

        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2 or MissionsActive.Both)
        {
            if (Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && team is Team.Sonic)
                return;
            //draw right side
            var windowCentre = _outerWidth - _windowWidth / 2;

            var textWidth = textSize.x / 2;

            var textEnd = windowCentre + textWidth;

            for (int i = 0; i < keylist.Count; i++)
            {
                var bonusKeyChecked = Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 1 
                    ? Mod.ArchipelagoHandler.IsLocationChecked(noActStartId + bonusKeyOffset + i) 
                    : Mod.ArchipelagoHandler.IsLocationChecked(act2StartId + bonusKeyOffset + i);
                DrawCircle(_drawList, textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius,
                    _circRadius, bonusKeyChecked);
            }

        }
    }
    
    
    
    
    private unsafe void HandleCheckpointSanity(Team team, LevelId level)
    {
        //Checkpoint Sanity
        if (Mod.ArchipelagoHandler!.SlotData.CheckpointSanityDict[team] == 0)
            return;

        if (Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.None)
            return;
        
        var act1StartId = CheckPointPriorities.Act1StartId;
        var act2StartId = CheckPointPriorities.Act2StartId;
        var noActStartId = CheckPointPriorities.NoActStartId;
        
        //Handle Normal First
        var checkpointsinlevel = from checkpoint in CheckPointPriorities.AllCheckpoints
            where checkpoint.Team == team && checkpoint.LevelId == level && checkpoint.SuperHard == false
            select checkpoint;
        List<CheckPointPriority> checkpointlist = checkpointsinlevel.ToList();

        if (checkpointlist.Count == 0)
            return;
        
        //SuperHardHere
        List<CheckPointPriority> superhardcheckpoints = new List<CheckPointPriority>();
        if (team is Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 &&
            Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2 or MissionsActive.Both)
        {
            var superhardcplist = from checkpoint in CheckPointPriorities.AllCheckpoints
                where checkpoint.Team == team && checkpoint.LevelId == level && checkpoint.SuperHard == true
                select checkpoint;
            superhardcheckpoints =  superhardcplist.ToList();
        }
        
        
        var checkpointOffset = CheckPointPriorities.AllCheckpoints.IndexOf(checkpointlist[0]); 
        
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        
        var textSize = new ImVec2.__Internal();
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), "Checkpoints", null, false, -1.0f);
            
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);

        ImGui.Text("Checkpoints");
        
        
        
        
        if (Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] == 1 &&              //only 1 set (or only normal for Sonic)
            ((team is Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2) //normal with SonicSuperHard
                || Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act1 or MissionsActive.Both) //Act 1 enabled
            ||
            (Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] == 3 && Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act1 or MissionsActive.Both)) //Both Sets enabled with Act 1 Active
        {
            //draw left circles
            var windowCentre = _outerWidth - _windowWidth / 2;

            var textWidth = textSize.x / 2;

            var textStart = windowCentre - textWidth;

            for (int i = 0; i < checkpointlist.Count; i++)
            {
                var checkpointChecked = Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] == 1 
                    ? Mod.ArchipelagoHandler.IsLocationChecked(noActStartId + checkpointOffset + i) 
                    : Mod.ArchipelagoHandler.IsLocationChecked(act1StartId + checkpointOffset + i);
                
                DrawCircle(_drawList, textStart - 4 * _circRadius * (checkpointlist.Count + 1) + 4 * _circRadius * i, cursorPos.Y + 2 * _circRadius,
                    _circRadius, checkpointChecked);
            }
            
        }
        
        if (
            (team is Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2 && Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] > 1 && Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2 or MissionsActive.Both) //Sonic w/ SuperHard w/ SuperHard Checkpoints w/ Act 2 on
            ||
            (Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] == 3 && Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2 or MissionsActive.Both) //both sets with Act 2 enabled
            ||
            (Mod.ArchipelagoHandler.SlotData.CheckpointSanityDict[team] == 1 && Mod.ArchipelagoHandler.SlotData.StoriesActive[team] is MissionsActive.Act2 or MissionsActive.Both) //one set only with only Act 2 (already know it isnt super hard)
            ) 
        {
            //draw right circles
            var windowCentre = _outerWidth - _windowWidth / 2;

            var textWidth = textSize.x / 2;

            var textEnd = windowCentre + textWidth;

            if (team is Team.Sonic && Mod.ArchipelagoHandler.SlotData.SuperHardModeSonicAct2)
            {
                //SuperHard
                checkpointOffset = CheckPointPriorities.AllCheckpoints.IndexOf(superhardcheckpoints[0]); 
                for (int i = 0; i < superhardcheckpoints.Count; i++)
                {
                    var checkpointChecked = Mod.ArchipelagoHandler.IsLocationChecked(act2StartId + checkpointOffset + i);
                    DrawCircle(_drawList, textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius,
                        _circRadius, checkpointChecked);
                }
            }

            else
            {
                //not SuperHard
                for (int i = 0; i < checkpointlist.Count; i++)
                {
                    var checkpointChecked = Mod.ArchipelagoHandler.SlotData.KeySanityDict[team] == 1 
                        ? Mod.ArchipelagoHandler.IsLocationChecked(noActStartId + checkpointOffset + i) 
                        : Mod.ArchipelagoHandler.IsLocationChecked(act2StartId + checkpointOffset + i);
                    DrawCircle(_drawList, textEnd + 4 * _circRadius * (i + 2), cursorPos.Y + 2 * _circRadius,
                        _circRadius, checkpointChecked);
                }
            }
        }

    }
    
    
    private unsafe void HandleSanityLayout(string sanityText, int sanityChecked, int sanityMax, float posX)
    {
        var textSize = new ImVec2.__Internal();
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"{sanityText} {sanityChecked}/{sanityMax}", null, false, -1.0f);
        ImGui.SetCursorPosX(posX - textSize.x / 2);
        ImGui.Text($"{sanityText} {sanityChecked}/{sanityMax}");
    }

    private unsafe void HandleBossLayout(Team story, LevelId level)
    {
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        
        var textSize = new ImVec2.__Internal();
        
        var bossCompleteId = 0xA0 + (int)story * 42 + 28 + ((int)level - 16) * 2;
        //Console.WriteLine(bossCompleteId.ToString("X"));

        //Handling for Final Boss Location ID
        //MetalOverlord LevelID (24) can not be here (check case switch earlier)
        if (level is LevelId.MetalMadness or LevelId.MetalOverlord)
        {
            bossCompleteId = 0x230E;
        }
        
        var isBossComplete = Mod.ArchipelagoHandler.IsLocationChecked(bossCompleteId);
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), "Boss", null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text("Boss");
        
        ImGui.GetCursorScreenPos(cursorPos);
        DrawCircle(_drawList, _outerWidth - _windowWidth / 2, cursorPos.Y + _circRadius, _circRadius, isBossComplete); 
        
        ImGui.NewLine();
        
        if (level is LevelId.MetalMadness or LevelId.MetalOverlord)
        {
            HandleFinalBossUI();
        }
        
    }


    private unsafe void HandleSpawnPos(Team team, LevelId level)
    {
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        
        var textSize = new ImVec2.__Internal();

        string text = "Spawn Position:";
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);
        
        
        text = Mod.LevelSpawnData!.GetLevelSelectUiText(team, level);
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);
    }

    private unsafe void HandleCharDisplayForTeam(Team team)
    {
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        var textSize = new ImVec2.__Internal();
        string text = "";

        text = $"Characters Unlocked:{Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForCharUnlocks(team)}";
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        //ImGui.TextColored(GetImVec4FromVec4(GetVec4FromColor(Color.Aqua)), text);
        ImGui.Text(text);
    }
    
    private unsafe void HandleAbilityDisplayForRegion(Team team, Region region)
    {
        var cursorPos = new ImVec2();
        ImGui.GetCursorScreenPos(cursorPos);
        var textSize = new ImVec2.__Internal();
        string text = "";
        
        List<Ability> teamAbilities = Mod.AbilityUnlockHandler!.GetAbilitiesForTeam(team);

        foreach (var ability in teamAbilities)
        {
            text = ability.ToString();
            Color textColor = Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][ability]
                ? Color.Green
                : Color.Red;

            if (ability is Ability.Flight && textColor == Color.Green &&
                !Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.Thundershoot])
            {
                textColor = Color.Orange;
            }

            if (ability is Ability.TriangleJump && textColor == Color.Green &&
                !Mod.SaveDataHandler.CustomSaveData.UnlockSaveData[team].AbilityUnlocks[region][Ability.HomingAttack])
            {
                textColor = Color.Orange;
            }
            
            ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
            ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
            ImGui.TextColored(GetImVec4FromVec4(GetVec4FromColor(textColor)) , text);
        }
        /*
        text = Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForAbilityUnlocks(team, region, FormationChar.Speed);
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);

        text = Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForAbilityUnlocks(team, region, FormationChar.Flying);
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);

        text = Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForAbilityUnlocks(team, region, FormationChar.Power);
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);
        */
    }


    private unsafe void HandleFinalBossUI()
    {
        try
        {
            var cursorPos = new ImVec2();
            ImGui.GetCursorScreenPos(cursorPos);
            var textSize = new ImVec2.__Internal();
            string text = "";

            text = $"Characters Unlocked: {Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForFinalBossCharUnlocks()}";
            ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
            ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
            ImGui.Text(text);



            var isMission2 = 0;
            foreach (var pair in Mod.ArchipelagoHandler.SlotData.StoriesActive.Where(pair => pair.Value > MissionsActive.None))
            {
                var levelCompletions = Mod.AbilityUnlockHandler.GetCompletedLevelsForTeam(pair.Key);
                var levelsNeeded = Mod.ArchipelagoHandler.SlotData.GoalLevelCompletions;
                
                text = $"{pair.Key.ToString()} Level Completions: {levelCompletions} / {levelsNeeded}";
                ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
                ImGui.Text(text);
            }

            if (Mod.ArchipelagoHandler.SlotData.GoalUnlockCondition is GoalUnlockCondition.LevelCompletions) 
                return;
            
            foreach (var emerald in Enum.GetValues<Emerald>())
            {
                text = $"{emerald.ToString()} Chaos Emerald";
                Color textColor = Mod.SaveDataHandler.CustomSaveData.Emeralds[emerald]
                    ? Color.Green
                    : Color.Red;
            
                ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
                ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
                ImGui.TextColored(GetImVec4FromVec4(GetVec4FromColor(textColor)) , text);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        /*

        text = $"Progressive Abilities Unlocked: {Mod.AbilityUnlockHandler!.GetLevelSelectUIStringForFinalBossAbilityUnlocks()}";
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), text, null, false, -1.0f);
        ImGui.SetCursorPosX(_windowWidth / 2 - textSize.x / 2);
        ImGui.Text(text);
        
        */
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