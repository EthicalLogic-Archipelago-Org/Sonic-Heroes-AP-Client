

using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class BouncePacketHandler
{
    
    public static void BouncePacketReceived(BouncePacket packet)
    {
        if (DeathLinkHandler.IsDeathLinkEnabled())
            ProcessBouncePacket(packet, "DeathLink", ref DeathLinkHandler.lastDeath, (source, data) =>
                DeathLinkHandler.HandleDeathLink(source, data["cause"]?.ToString() ?? "Unknown")); 

        if (RingLinkHandler.IsRingLinkEnabled())
            ProcessBouncePacket(packet, "RingLink", ref RingLinkHandler.lastRing, (source, data) =>
                RingLinkHandler.HandleRingLink(source, data["amount"]?.ToString() ?? "0"));
    }
    
    private static void ProcessBouncePacket(BouncePacket packet, string tag, ref string lastTime, Action<string, Dictionary<string, JToken>> handler)
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
            Console.WriteLine($"Received Bounce Packet with Tag: {tag} :: {cause}");
        }
        handler(source, packet.Data);
    }
    
    
    
    
    
    
    
}