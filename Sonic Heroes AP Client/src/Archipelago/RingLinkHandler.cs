

using System.Globalization;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Logging;
using Sonic_Heroes_AP_Client.Sound;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class RingLinkHandler
{
    public static string LastRing = "The Big Bang";
    
    public static bool IsRingLinkEnabled(string taskName)
    {
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in IsRingLinkEnabled", taskName, LogLevel.Error);
            return false;
        }
        return Mod.Configuration.RingLink;
    }

    public static bool IsRingLinkOverlord(string taskName)
    {
        if (Mod.Configuration == null)
        {
            LoggingHandler.LogMessage($"Mod Configuration is Null in IsRingLinkOverlord", taskName, LogLevel.Error);
            return false;
        }
        return Mod.Configuration.RingLinkOverlord;
    }
    
    public static void SendRingPacket(int amount, string taskName)
    {
        BouncePacket packet = new BouncePacket();
        var now = DateTime.Now;
        packet.Tags =
        [
            "RingLink",
        ];
        packet.Data = new Dictionary<string, JToken>
        {
            { "time", now.ToUnixTimeStamp() },
            { "source", Mod.ArchipelagoHandler.SlotInstance },
            { "amount", amount },
        };
        Mod.ArchipelagoHandler.Session.Socket.SendPacket(packet);
    }
    
    
    public static void HandleRingLink(string source, string amountStr, string taskName)
    {
        try
        {
            if (!IsRingLinkEnabled(taskName))
                return;
            if (!IsRingLinkOverlord(taskName) && GameStateHandler.GetCurrentLevel(taskName) == LevelId.MetalOverlord)
                return;
            if (source == Mod.ArchipelagoHandler.SlotInstance.ToString(CultureInfo.InvariantCulture))
                return;
            if (!int.TryParse(amountStr, out var amount))
                return;

            if (Mod.Configuration == null)
            {
                LoggingHandler.LogMessage($"Mod Configuration is Null in HandleRingLink", taskName, LogLevel.Error);
                return;
            }
            
            var ringCount = GameStateGameWrites.GetRingCount(taskName);
            var newAmount = Math.Max(Math.Min(ringCount + amount, 999), 0);
            if (GameStateHandler.InGame(taskName) && Mod.Configuration.PlaySounds)
            {
                switch (amount)
                {
                    case 1:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1004, taskName);
                        break;
                    case > 1:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033, taskName);
                        break;
                    case < 0:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1005, taskName);
                        break;
                }
            }
            GameStateGameWrites.SetRingCount(newAmount, taskName);
        }
        catch (Exception e)
        {
            LoggingHandler.LogMessage($"{e}", taskName, LogLevel.Error);
        }
    }
}