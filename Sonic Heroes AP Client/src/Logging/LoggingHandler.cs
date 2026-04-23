
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Reloaded.Imgui.Hook;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Logging;

public readonly record struct LogStackTraceInfo(string NameSpace, string ClassName, string MethodName, int LineNumber);



public static class LoggingHandler
{
    private static bool _imguiHookInitializedWarning = false;
    
    public static void OnModLoggerWriteLine(object? sender, (string text, Color color) e)
    {
        //do something here
        //"OnModLoggerWriteLine: {sender}"
    }
    
    
    /*
    public static void LogMessage(Type type, string message, string methodName, LogLevel level = LogLevel.Debug, string task = "MainTask")
    {
        var source = $"{type.FullName ?? "Null"}.{methodName}";
        LogEntry log = new (source: source, message: message, logLevel: level, taskSource: task);
        log.LogMessage();
    }
    */

    public static void LogMessage(string message, string taskName, LogLevel level, int stackFrameOverride = -1)
    {
        
        if (Mod.Configuration != null && Mod.Configuration.LowestLogAllowed > level)
        {
            //do not log this
            return;
        }
        
        LogStackTraceInfo info = GetStackTraceInformationFromCallingFunction(stackFrameOverride);
        var source = $"{info.NameSpace}: {info.ClassName}.{info.MethodName} ln:{info.LineNumber}";
        LogEntry log = new (source: source, message: message, logLevel: level, taskSource: taskName);
        log.LogMessage();

        if (!ImguiHook.Initialized && !_imguiHookInitializedWarning)
        {
            //make this warning one time
            source = $"Sonic_Heroes_AP_Client.Logging: LoggingHandler.LogMessage ln: 37";
            message = $"ImGuiHook Not Initialized (did you add d3d8.dll to game directory?";
            level = LogLevel.Error;
            log = new (source: source, message: message, logLevel: level, taskSource: taskName);
            log.LogMessage();
            _imguiHookInitializedWarning = true;
            return;
        }
        
        if (level >= LogLevel.Info)
        {
            LoggerWindow.Log(message, taskName);
        }
        
    }
    
    public static LogStackTraceInfo GetStackTraceInformationFromCallingFunction(int stackFrameOverride)
    {
        //var name = new StackTrace(true).GetFrame(2)?.GetMethod()?.GetMethodFullName();
        var stack = new StackTrace(true);
        
        var stackFrameIndex = stackFrameOverride >= 0 && stackFrameOverride < stack.FrameCount ? stackFrameOverride : 2; //2 because the third func call (this one, LogMessage, then actual function)
        var frame = stack.GetFrame(stackFrameIndex); 
        
        var methodBase = frame?.GetMethod();
        var className = methodBase?.DeclaringType?.Name ?? "NullClass";
        var namespaceName = methodBase?.DeclaringType?.Namespace ?? "NullNamespace";
        var lineNumber = frame?.GetFileLineNumber() ?? -1;
        var methodName = methodBase?.Name ?? "NullMethod";
        return new LogStackTraceInfo(namespaceName, className, methodName, lineNumber);
    }

    /*
    public static string GetMethodFullName(this MethodBase method) 
    {
        if (method.DeclaringType.GetInterfaces().Any(i => i == typeof(IAsyncStateMachine))) 
        {
            var generatedType = method.DeclaringType;
            var originalType = generatedType.DeclaringType;
            var foundMethod = originalType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
            return foundMethod.DeclaringType.Name + "." + foundMethod.Name;
        } 
        else 
        {
            return method.DeclaringType.Name + "." + method.Name;
        }
    }
    */
}