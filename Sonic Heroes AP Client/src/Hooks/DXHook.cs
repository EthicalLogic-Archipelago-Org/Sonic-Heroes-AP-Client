
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions;
using Reloaded.Imgui.Hook;
using Reloaded.Imgui.Hook.Direct3D9;
using Reloaded.Imgui.Hook.Direct3D9.Definitions;
using Reloaded.Imgui.Hook.Misc;
using SharpDX;
using SharpDX.Direct3D9;

namespace Sonic_Heroes_AP_Client.Hooks;

public class DXHook
{
    private static Device? _device;
    public static List<Texture> Textures = new();
    private static IHook<DX9Hook.CreateDevice> _createDeviceHook;

    public DXHook(IReloadedHooks hooks)
    {
        SDK.Init(hooks);
        var createDevicePtr = (long)DX9Hook.DeviceVTable[(int)IDirect3D9.CreateDevice].FunctionPointer;
        _createDeviceHook = hooks.CreateHook<DX9Hook.CreateDevice>(typeof(DXHook), 
            nameof(OnCreateDevice), createDevicePtr).Activate();
    }
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe int OnCreateDevice(IntPtr device, uint adapter, DeviceType deviceType, 
        IntPtr focusWindow, CreateFlags behaviorFlags, 
        BlittablePtr<PresentParameters> presentationParameters, 
        BlittablePtrPtr<int> returnedDeviceInterface)
    {
        var result = _createDeviceHook.OriginalFunction.Value.Invoke(
            device, adapter, deviceType, focusWindow, 
            behaviorFlags, presentationParameters, returnedDeviceInterface
        );
        
        Textures.Clear();
        
        Console.WriteLine($"[DXHook] New D3D9 Device Created! Pointer: {device}");
        
        _device = CppObject.FromPointer<Device>(device);
        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("Sonic_Heroes_AP_Client.Resource.so2_emy.png");
        
        var testTexture = new Texture(_device, 64, 64, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);
        using (var surface = testTexture.GetSurfaceLevel(0))
        {
            var rect = surface.LockRectangle(LockFlags.None);
            var pixelData = (uint*)rect.DataPointer; // Get pointer to the pixel data

            for (var y = 0; y < 64; y++)
            {
                for (var x = 0; x < 64; x++)
                {
                    pixelData[y * (rect.Pitch / 4) + x] = 0xFFFF0000; // Solid Red (ARGB format)
                }
            }
            surface.UnlockRectangle();
        }
        Textures.Add(testTexture);
        
        //Textures.Add(Texture.FromStream(_device, stream));
        return (int)result;
    }
    
}