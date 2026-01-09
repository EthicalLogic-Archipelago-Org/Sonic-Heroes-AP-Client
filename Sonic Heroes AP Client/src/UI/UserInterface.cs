using System.Runtime.InteropServices;
using Reloaded.Imgui.Hook;
using Reloaded.Imgui.Hook.Direct3D11;
using Reloaded.Imgui.Hook.Implementations;

namespace Sonic_Heroes_AP_Client.UI;

public class UserInterface

{
    public LoggerWindow LoggerWindow;
    public LevelTracker LevelTracker;
    public TrapTracker TrapTracker;
    
    public UserInterface()
    {
        Task.Run(CreateGui);
    }

    private async void CreateGui()
    {
        LoggerWindow = new LoggerWindow();
        LevelTracker = new LevelTracker();
        TrapTracker = new TrapTracker();
        try
        {
            await ImguiHook.Create(Render, new ImguiHookOptions()
            {
                Implementations = new List<IImguiHook>
                {
                    new ImguiHookDx9(),
                    new ImguiHookDx11()
                }
            }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Disabling overlay, did you add d3d8.dll to the game directory?");
        }
    }
    
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out LoggerWindow.RECT lpRect);
    
    private unsafe void Render()
    {
        if (!GetWindowRect(ImguiHook.WndProcHook.WindowHandle, out var rect))
            return;
        var width = rect.Right - rect.Left;
        var height = rect.Bottom - rect.Top;
        const int baseWidth = 1920;
        const int baseHeight = 1080;
        var widthScale = (float)width / baseWidth;
        var heightScale = (float)height / baseHeight;
        var uiScale = widthScale < heightScale ? widthScale : heightScale;
        LoggerWindow.Draw(width, height, uiScale);
        LevelTracker.Draw(width, height, uiScale);
        TrapTracker.Draw(width, height, uiScale);
    }
}