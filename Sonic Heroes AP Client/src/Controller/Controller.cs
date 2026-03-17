

using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Definitions;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Sonic_Heroes_AP_Client.LevelSpawnPosition;

namespace Sonic_Heroes_AP_Client.Controller;


public class Controller
{
    private WeakReference<IControllerHook> _controllerHook;
    private readonly int _port;
    private DateTime _timesinceLastAnalogStickUp;
    private DateTime _timesinceLastAnalogStickDown;
    
    public Controller(WeakReference<IControllerHook> controllerHook, int port)
    {
        this._controllerHook = controllerHook;
        this._port = port;
        this._timesinceLastAnalogStickUp = DateTime.Now;
        this._timesinceLastAnalogStickDown = DateTime.Now;
        IControllerHook target;
        if (!this._controllerHook.TryGetTarget(out target))
            return;
        target.OnInput += new OnInputEvent(this.OnInput);
    }


    private void OnInput(IExtendedHeroesController inputs, int port)
    {
        const string taskName = "ControllerOnInput";
        if (port != this._port)
        {
            return;
        }

        if (!LevelSpawnUnlockHandler.ShouldCheckForInput)
        {
            return;
        }

        if (inputs.LeftStickY < -0.5 && (DateTime.Now - this._timesinceLastAnalogStickUp).TotalSeconds > 0.5)
        {
            //"Left Stick Up: {inputs.LeftStickY}"
            this._timesinceLastAnalogStickUp = DateTime.Now;
            
            LevelSpawnUnlockHandler.HandleInput(true, taskName);
        }

        if (inputs.LeftStickY > 0.5 && (DateTime.Now - this._timesinceLastAnalogStickDown).TotalSeconds > 0.5)
        {
            //"Left Stick Down: {inputs.LeftStickY}"
            this._timesinceLastAnalogStickDown = DateTime.Now;
            
            LevelSpawnUnlockHandler.HandleInput(false, taskName);
        }
        

        if ((inputs.OneFramePressButtonFlag & ButtonFlags.DpadUp) != 0)
        {
            //"Dpad Up"
            LevelSpawnUnlockHandler.HandleInput(true, taskName);
        }

        if ((inputs.OneFramePressButtonFlag & ButtonFlags.DpadDown) != 0)
        {
            //"Dpad Down"
            LevelSpawnUnlockHandler.HandleInput(false, taskName);
        }
        
    }
}