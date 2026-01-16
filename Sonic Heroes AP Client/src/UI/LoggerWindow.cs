

using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Archipelago.MultiClient.Net.Converters;
using DearImguiSharp;
using Reloaded.Imgui.Hook;
using Sonic_Heroes_AP_Client.GameState;

namespace Sonic_Heroes_AP_Client.UI;

public partial class LoggerWindow
{
    // Define the RECT structure to hold the window dimensions
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    struct LogMessage 
    {
        public string Message;
        public double Timestamp;

        public LogMessage(string message, double timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }
    };
    
    private struct Word
    {
        public string Text;
        public uint Color;

        public Word(string text, uint color)
        {
            Text = text;
            Color = color;
        }
    }
    
    private static readonly Dictionary<string, uint> KeywordColors = new() {
        {"emblem", ColorToHex(Color.FromArgb(0xff, 0xff, 0xdc, 0x3f))},
        {"green chaos emerald", ColorToHex(Color.FromArgb(0xff, 0x2d, 0xff, 0x4d))}, 
        {"blue chaos emerald", ColorToHex(Color.FromArgb(0xff, 0x30, 0x89, 0xff))},
        {"yellow chaos emerald", ColorToHex(Color.FromArgb(0xff, 0xff, 0xe6, 0x4f))},
        {"white chaos emerald", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0xff))},
        {"cyan chaos emerald", ColorToHex(Color.FromArgb(0xff, 0x51, 0xff, 0xff))},
        {"purple chaos emerald", ColorToHex(Color.FromArgb(0xff, 0xC0, 0x61, 0xff))},
        {"red chaos emerald", ColorToHex(Color.FromArgb(0xff, 0xff, 0x51, 0x51))},
        {"playable sonic", ColorToHex(Color.FromArgb(0xff, 0x30, 0x89, 0xff))},
        {"playable tails", ColorToHex(Color.FromArgb(0xff, 0xff, 0xf4, 0x37))},
        {"playable knuckles", ColorToHex(Color.FromArgb(0xff, 0xff, 0x30, 0x35))},
        {"progressive sonic", ColorToHex(Color.FromArgb(0xff, 0x30, 0x89, 0xff))},
        {"progressive tails", ColorToHex(Color.FromArgb(0xff, 0xff, 0xf4, 0x37))},
        {"progressive knuckles", ColorToHex(Color.FromArgb(0xff, 0xff, 0x30, 0x35))},
        
        //{"ocean", ColorToHex(Color.FromArgb(0xff, 0xA5, 0xFD, 0xe9))},
        //{"city", ColorToHex(Color.FromArgb(0xff, 0xE7, 0x17, 0x0D))},
        //{"casino", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0x30))},
        //{"train", ColorToHex(Color.FromArgb(0xff, 0x74, 0x4F, 0x39))},
        //{"bigplant", ColorToHex(Color.Green)},
        //{"ghost", ColorToHex(Color.FromArgb(0xff, 0xA5, 0xFD, 0xe9))},
        //{"sky", ColorToHex(Color.FromArgb(0xff, 0x18, 0x2E, 0x36))},
        //{"allregion", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0xff))},
        
        {"extra life", ColorToHex(Color.FromArgb(0xff, 0xff, 0xbf, 0x62))},
        {"5 rings", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0x30))}, 
        {"10 rings", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0x30))},
        {"20 rings", ColorToHex(Color.FromArgb(0xff, 0xff, 0xff, 0x30))},
        {"speed level up", ColorToHex(Color.FromArgb(0xff, 0x30, 0x89, 0xff))},
        {"power level up", ColorToHex(Color.FromArgb(0xff, 0xff, 0x30, 0x35))}, 
        {"flying level up", ColorToHex(Color.FromArgb(0xff, 0xff, 0xf4, 0x37))}, 
        {"team level up", ColorToHex(Color.FromArgb(0xff, 0xff, 0x50, 0xff))}, 
        {"shield", ColorToHex(Color.FromArgb(0xff, 0x4b, 0xff, 0x48))}, 
        {"ring trap", ColorToHex(Color.FromArgb(0xff, 0xff, 0x40, 0x40))},
        {"charmy trap", ColorToHex(Color.FromArgb(0xff, 0xff, 0x40, 0x40))},
        {"no swap trap", ColorToHex(Color.FromArgb(0xff, 0xff, 0x40, 0x40))},
        {"freeze trap", ColorToHex(Color.FromArgb(0xff, 0xff, 0x40, 0x40))},
        {"stealth trap", ColorToHex(Color.FromArgb(0xff, 0xff, 0x40, 0x40))}
    };

    private static uint ColorToHex(Color color)
    {
        return (uint)((color.A << 24) | (color.B << 16) | (color.G << 8) | color.R);
    }

    private bool _isOpen = true;
    private List<LogMessage> VisibleMessages = new();
    private Queue<LogMessage> CachedMessages = new();
    private static Regex _keywordPattern;

    public LoggerWindow()
    {
        var patterns = KeywordColors.Keys.Select(Regex.Escape);
        var combinedPattern = string.Join("|", patterns);
        _keywordPattern = new Regex(combinedPattern, RegexOptions.IgnoreCase);
    }

    private DateTime _timeSinceLastUpdate;
    public unsafe void Draw(float outerWidth, float outerHeight, float uiScale)
    {
        if (DateTime.Now.Subtract(_timeSinceLastUpdate).TotalMilliseconds >= Mod.Configuration.LogMessageDelay)
        {
            UpdateVisibleMessages();
            _timeSinceLastUpdate = DateTime.Now;
        }

        var windowWidth = 0.27f * outerWidth;
        var windowHeight = 0.6f * outerHeight;
        var padding = 0.01f * outerHeight;
        var lifeCountOffset = GameStateHandler.InGame() ? 0.25f * outerHeight : 0.1f * outerHeight;
        //var lifeCountOffset = 0.25f *  outerHeight;
        var logPos = new ImVec2.__Internal {
            x = padding, 
            y = outerHeight - windowHeight - (padding * 2.25f) - lifeCountOffset
        };
        var logPivot = new ImVec2.__Internal { x = 0, y = 0 };
        var logSize = new ImVec2.__Internal { x = windowWidth, y = windowHeight };
        ImGui.__Internal.SetNextWindowPos(logPos, (int)ImGuiCond.Always, logPivot);
        ImGui.__Internal.SetNextWindowSize(logSize, (int)ImGuiCond.Always);
        ImGui.__Internal.Begin("Log", null, 
            (int)(ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBackground));
        ImGui.SetWindowFontScale(uiScale + 0.3f);
        
        var windowPos = new ImVec2();
        ImGui.GetCursorScreenPos(windowPos);
        var windowSize = new ImVec2();
        ImGui.GetContentRegionAvail(windowSize);

        var yPos = windowPos.Y + windowSize.Y;

        for (var i = VisibleMessages.Count - 1; i >= 0; --i)
        {
            var message = VisibleMessages[i];
            var xPos = windowPos.X;
            var maxWidth = windowSize.X;
            
            if (string.IsNullOrEmpty(message.Message))
                continue;
            
            var lines = message.Message.Split('\n');
            foreach (var line in lines)
            {
                var matches = _keywordPattern.Matches(line);
                var words = new List<Word>();
                var currentColor = 0xffffffff;
                var currentWord = "";
                var currentMatchLength = 0;
                var currentMatchIndex = 0;
                for (var cIndex = 0; cIndex < line.Length; cIndex++)
                {
                    var match = matches.FirstOrDefault(x => x.Index == cIndex);
                    if (match != null)
                    {
                        currentColor = KeywordColors[match.Value.ToLower()];
                        currentMatchLength = match.Length;
                        currentMatchIndex = cIndex;
                    }
                    currentWord += line[cIndex];
                    if (line[cIndex] != ' ' && cIndex != line.Length - 1) 
                        continue;
                    words.Add(new Word(currentWord.Trim(), currentColor));
                    if (cIndex >= currentMatchIndex + currentMatchLength)
                        currentColor = 0xffffffff;
                    currentWord = "";
                }

                var currentLine = "";
                var currentLineWords = new List<Word>();
                var wrappedLines = new List<List<Word>>();
                float maxLineWidth = 0;
                
                foreach (var word in words)
                {
                    var testLine = string.IsNullOrEmpty(currentLine) ? word.Text : currentLine + " " + word.Text;
                    var textSize = new ImVec2.__Internal();
                    ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), testLine, null, false, -1.0f);
                    
                    if (textSize.x <= maxWidth || string.IsNullOrEmpty(currentLine))
                    {
                        currentLineWords.Add(word);
                        currentLine = testLine;
                    }
                    else
                    {
                        wrappedLines.Add(new List<Word>(currentLineWords));
                        var lineTextSize = new ImVec2.__Internal();
                        ImGui.__Internal.CalcTextSize((IntPtr) (&lineTextSize), GetWordString(currentLineWords), null, false, -1.0f);
                        maxLineWidth = float.Max(maxLineWidth, lineTextSize.x);
                        
                        currentLineWords.Clear();
                        currentLineWords.Add(word);
                        currentLine = word.Text;
                    }
                }

                if (currentLineWords.Count > 0)
                {
                    wrappedLines.Add(currentLineWords);
                    var textSize = new ImVec2();
                    ImGui.CalcTextSize(textSize, GetWordString(currentLineWords), null, false, -1.0f);
                    maxLineWidth = float.Max(maxLineWidth, textSize.X);
                }
                
                for (var lineIndex = wrappedLines.Count - 1; lineIndex >= 0; lineIndex--)
                {
                    var wrappedLine = wrappedLines[lineIndex];
                    var textSize = new ImVec2.__Internal();
                    ImGui.__Internal.CalcTextSize((IntPtr) (&textSize), GetWordString(wrappedLine), 
                        null, false, -1.0f);
                    
                    var scrollOffset = 0f;
                    var elapsed = DateTime.Now.ToUnixTimeStamp() - message.Timestamp;
                    var animationDuration = 0.5f;

                    if (elapsed < animationDuration)
                    {
                        var t = (float)(elapsed / animationDuration);
                        t = 1f - MathF.Pow(1f - t, 2f);
                        scrollOffset = (1f - t) * 30f;
                    }
                    
                    var boxMin = new ImVec2.__Internal { x = windowPos.X - 1, y = yPos + scrollOffset - textSize.y - 1 };
                    var boxMax = new ImVec2.__Internal { x = maxLineWidth + windowPos.X + 1, y = yPos + scrollOffset + 1 };
                    ImGui.__Internal.ImDrawListAddRectFilled(ImGui.__Internal.GetWindowDrawList(), boxMin, boxMax, 
                        0xB0000000, 0.0f, 0);
                    
                    yPos -= textSize.y + 2 - scrollOffset / 2;
                    
                    xPos = windowPos.X;
                    var renderPos = new ImVec2.__Internal() { x = xPos, y = yPos + scrollOffset };
                    
                    foreach (var word in wrappedLine)
                        DrawText(word.Text + " ", ref xPos, renderPos, word.Color);
                }
            }
            yPos -= 5.0f;
        }
        ImGui.End();
    }

    private string GetWordString(IEnumerable<Word> words)
    {
        var line = "";
        foreach (var word in words)
            line += word.Text + " ";
        return line;
    }
    
    private unsafe void DrawText(string text, ref float xPos, ImVec2.__Internal renderPos, uint color)
    {
        var pos = new ImVec2.__Internal { x = xPos, y = renderPos.y };
        ImGui.__Internal.ImDrawListAddTextVec2(ImGui.__Internal.GetWindowDrawList(), pos, color, text, null);

        var textSize = new ImVec2.__Internal();
        ImGui.__Internal.CalcTextSize((IntPtr)(&textSize), text, null, false, -1.0f);
        xPos += textSize.x;
    }

    private void UpdateVisibleMessages()
    {
        var now = DateTime.Now.ToUnixTimeStamp();
        VisibleMessages.RemoveAll(msg => ((now - msg.Timestamp) > Mod.Configuration.LogMessageTime));
        if (!CachedMessages.Any() || VisibleMessages.Count >= Mod.Configuration.LogMessageCount)
            return;
        var message = CachedMessages.Dequeue();
        message.Timestamp = now;
        VisibleMessages.Add(message);
    }

    public static void Log(string text)
    {
        if (!ImguiHook.Initialized)
        {
            Console.WriteLine(text);
            return;
        }
        var message = new LogMessage(text, DateTime.Now.ToUnixTimeStamp());
        Mod.UserInterface.LoggerWindow.CachedMessages.Enqueue(message);
    }

    [GeneratedRegex(@"(\[color=#[0-9A-Fa-f]{6,8}\])")]
    private static partial Regex ColorTagRegex();
}