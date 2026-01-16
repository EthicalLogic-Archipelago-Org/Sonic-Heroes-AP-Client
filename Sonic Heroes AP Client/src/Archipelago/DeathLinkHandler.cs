using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.UI;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class DeathLinkHandler
{
    public static DateTime LastDeathLinkPacketTime = DateTime.Now;
    public static string lastDeath = "The Big Bang";
    public static bool SomeoneElseDied = false;
    
    private static Random _random = new();
    private static readonly string[] _deathMessages =
    {
        "had a skill issue (died)",
        "couldn't even beat Gamma or Beta (died)",
        "said it wasn't their fault (died)",
        "had a run in with those Eggman's robots (died)",
        "tried to code an AP game (died)",
        "didn't want to live in the same world as Charmy (died)",
        "received too many ring checks (died)",
        "tried to spin dash (died)",
        "discovered those weren't the easy ones (died)",
        "underestimated Sonic speed (died)",
        "died - MARRIAGE???? No way!",
        "didn't have just enough to pass (died)",
        "died - ANNIHILATION COMPLETED",
        "died - WORTHLESS CONSUMER MODELS",
        "couldn't even impress Sonic (died)",
        "couldn't count on Cream (died)",
        "made fun of Big's friends (died)",
        "should have had more ninja training (died)",
        "witnessed Espio's ninja power (died)",
        "was stung by Charmy (died)",
        "tried to con team Chaotix (died)",
        "tried to steal Charmy's honey (died)",
        "didn't play to win (died)",
        "was nothing but talk (died)",
        "had a lackluster performance I'd say (died)",
        "regrets leaving it to Tails (died)",
        "should have been careful not to fall (died)",
        "forgot to pay the electric bill for the office (died)",
        "should have picked Team Rose (died)"
    };
    
    public static bool IsDeathLinkEnabled()
    {
        try
        {
            return Mod.Configuration.DeathLink;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
    
    public static void SendDeath()
    {
        BouncePacket packet = new BouncePacket();
        var now = DateTime.Now;

        if (now - LastDeathLinkPacketTime < TimeSpan.FromSeconds(1))
            return;
        
        packet.Tags = new List<string> { "DeathLink" };
        packet.Data = new Dictionary<string, JToken>
        {
            { "time", now.ToUnixTimeStamp() },
            { "source", Mod.ArchipelagoHandler.Slot },
            { "cause", $"{Mod.ArchipelagoHandler.Slot} {_deathMessages[_random.Next(_deathMessages.Length)]}" }
        };

        if (packet.Data.TryGetValue("source", out var sourceObj))
        {
            var source = sourceObj?.ToString() ?? "Unknown";
            if (packet.Data.TryGetValue("cause", out var causeObj))
            {
                var cause = causeObj?.ToString() ?? "Unknown";
                if (packet.Data.TryGetValue("time", out var timeObj))
                {
                    var time = timeObj?.ToString() ?? "Unknown";
                    Console.WriteLine(
                        $"Sending DeathLink Packet: {source} {cause} :: with time: {time}");
                }
            }
        }
        Mod.ArchipelagoHandler._session.Socket.SendPacket(packet);
        LastDeathLinkPacketTime = now;
    }
    
    public static void HandleDeathLink(string source, string cause)
    {
        if (!IsDeathLinkEnabled())
            return;
        LoggerWindow.Log($"{cause}");
        Console.WriteLine($"{cause}");
        if (source == Mod.ArchipelagoHandler.Slot)
            return;
        if (!GameStateHandler.InGame())
            return;
        //Need to check if InGame here otherwise SomeoneElseDied is set to true
        //(if a deathlink packet is received while in a menu or paused) but
        //doesnt get set back to false resulting in ignoring a client death
        
        SomeoneElseDied = true;
        GameStateGameWrites.Kill();
    }
}