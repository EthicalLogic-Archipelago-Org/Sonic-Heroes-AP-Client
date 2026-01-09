using DearImguiSharp;
using Sonic_Heroes_AP_Client.Archipelago;
using Sonic_Heroes_AP_Client.GameState;

namespace Sonic_Heroes_AP_Client.UI;

public class TrapTracker
{
    private IntPtr _drawList;
    private float _col1Centre;
    private float _col2Centre;
    private float _circRadius;
    private float _uiScale;
    private float _windowWidth;
    private float _windowHeight;
    private float _windowPosX;
    private float _windowPosY;
    
    private float _animationProgress = 0f; // 0 = offscreen, 1 = fully visible
    private const float _animationSpeed = 0.05f; // Tune this as needed
    private float _targetWindowPosX;
    private float _hiddenWindowPosX;
    
    public unsafe void Draw(float outerWidth, float outerHeight, float uiScale)
    {
        if (!GameStateHandler.InGame(true))
            return;
        
        _animationProgress = TrapHandler.IsAnyTrapRunning() ? Math.Min(1f, _animationProgress + _animationSpeed) : Math.Max(0f, _animationProgress - _animationSpeed);
        
        if (_animationProgress <= 0f)
            return;
        
        _uiScale = uiScale;
        _windowWidth = 0.08f * outerWidth;
        _windowHeight = 0.125f * outerHeight;
        
        _windowPosX = outerWidth - (_windowWidth + 0.05f * outerWidth) * _animationProgress;
        
        _windowPosY = outerHeight - (_windowHeight + 0.45f * outerHeight);
        var trackerPos = new ImVec2.__Internal { x = _windowPosX, y = _windowPosY };
        var trackerSize = new ImVec2.__Internal { x = _windowWidth, y = _windowHeight };
        var trackerPivot = new ImVec2.__Internal { x = 0, y = 0 };
        ImGui.__Internal.SetNextWindowPos(trackerPos, (int)ImGuiCond.Always, trackerPivot);
        ImGui.__Internal.SetNextWindowSize(trackerSize, (int)ImGuiCond.Always);
        ImGui.__Internal.PushStyleColorU32((int)ImGuiCol.WindowBg, 0xC0000000);
        ImGui.__Internal.Begin("Tracker", null,
            (int)(ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar));
        ImGui.SetWindowFontScale(uiScale * 1.75f);
        
        var textSize = new ImVec2.__Internal();
        
        _drawList = ImGui.__Internal.GetWindowDrawList();
        _circRadius = 8 * uiScale;
        
        var disabledColor = 0x80FFFFFF;
        var enabledColor = 0xFF8080FF;

        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"STEALTH", null, false, -1.0f);
        ImGui.SetCursorPosX(_windowPosX + _windowWidth / 2 - textSize.x / 2);
        ImGui.SetCursorPosY(_windowPosY + _windowHeight / 5 * 1);
        var pos = new ImVec2.__Internal { x = ImGui.__Internal.GetCursorPosX(), y = ImGui.__Internal.GetCursorPosY() - textSize.y / 2 };
        ImGui.__Internal.ImDrawListAddTextVec2(
            ImGui.__Internal.GetWindowDrawList(), 
            pos, 
            TrapHandler.StealthTrapRunning ? enabledColor : disabledColor, 
            "STEALTH", null);
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"FREEZE", null, false, -1.0f);
        ImGui.SetCursorPosX(_windowPosX + _windowWidth / 2 - textSize.x / 2);
        ImGui.SetCursorPosY(_windowPosY + _windowHeight / 5 * 2);
        pos = new ImVec2.__Internal { x = ImGui.__Internal.GetCursorPosX(), y = ImGui.__Internal.GetCursorPosY() - textSize.y / 2 };
        ImGui.__Internal.ImDrawListAddTextVec2(
            ImGui.__Internal.GetWindowDrawList(), 
            pos, 
            TrapHandler.FreezeTrapRunning ? enabledColor : disabledColor, 
            "FREEZE", null);
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"NO-SWAP", null, false, -1.0f);
        ImGui.SetCursorPosX(_windowPosX + _windowWidth / 2 - textSize.x / 2);
        ImGui.SetCursorPosY(_windowPosY + _windowHeight / 5 * 3);
        pos = new ImVec2.__Internal { x = ImGui.__Internal.GetCursorPosX(), y = ImGui.__Internal.GetCursorPosY() - textSize.y / 2 };
        ImGui.__Internal.ImDrawListAddTextVec2(
            ImGui.__Internal.GetWindowDrawList(), 
            pos, 
            TrapHandler.NoSwapTrapRunning ? enabledColor : disabledColor, 
            "NO-SWAP", null);
        
        ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), $"CHARMY", null, false, -1.0f);
        ImGui.SetCursorPosX(_windowPosX + _windowWidth / 2 - textSize.x / 2);
        ImGui.SetCursorPosY(_windowPosY + _windowHeight / 5 * 4);
        pos = new ImVec2.__Internal { x = ImGui.__Internal.GetCursorPosX(), y = ImGui.__Internal.GetCursorPosY() - textSize.y / 2 };
        ImGui.__Internal.ImDrawListAddTextVec2(
            ImGui.__Internal.GetWindowDrawList(), 
            pos, 
            TrapHandler.CharmyTrapRunning ? enabledColor : disabledColor, 
            "CHARMY", null);

        
        ImGui.End();
        ImGui.__Internal.PopStyleColor(1);
    }
}