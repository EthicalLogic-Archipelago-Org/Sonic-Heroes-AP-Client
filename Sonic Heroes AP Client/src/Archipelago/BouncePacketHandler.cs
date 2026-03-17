

using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.Logging;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class BouncePacketHandler
{
    
    public static void BouncePacketReceived(BouncePacket packet, string taskName)
    {
        if (DeathLinkHandler.IsDeathLinkEnabled(taskName))
            ProcessBouncePacket(packet, "DeathLink", ref DeathLinkHandler.lastDeath, (source, data) =>
                DeathLinkHandler.HandleDeathLink(source, data["cause"].ToString() ?? "Unknown", taskName), taskName); 

        if (RingLinkHandler.IsRingLinkEnabled(taskName))
            ProcessBouncePacket(packet, "RingLink", ref RingLinkHandler.LastRing, (source, data) =>
                RingLinkHandler.HandleRingLink(source, data["amount"].ToString() ?? "0", taskName), taskName);
    }
    
    private static void ProcessBouncePacket(BouncePacket packet, string tag, ref string lastTime, Action<string, Dictionary<string, JToken>> handler, string taskName)
    {
        if (!packet.Tags.Contains(tag)) return;
        if (!packet.Data.TryGetValue("time", out var timeObj)) 
            return;
        if (lastTime == timeObj.ToString())
            return;
        lastTime = timeObj.ToString();
        if (!packet.Data.TryGetValue("source", out var sourceObj)) 
            return;
        var source = sourceObj?.ToString() ?? "Unknown";
        if (packet.Data.TryGetValue("cause", out var causeObj))
        {
            var cause = causeObj?.ToString() ?? "Unknown";
            LoggingHandler.LogMessage($"Received Bounce Packet with Tag: {tag} :: {cause}", taskName, LogLevel.SuperDebug);
        }
        handler(source, packet.Data);
    }
    
    
    
    
    
    
    
}