

using System.Globalization;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using Sonic_Heroes_AP_Client.Definitions;
using Sonic_Heroes_AP_Client.GameState;
using Sonic_Heroes_AP_Client.Sound;

namespace Sonic_Heroes_AP_Client.Archipelago;

public static class RingLinkHandler
{
    public static string lastRing = "The Big Bang";
    
    public static bool IsRingLinkEnabled()
    {
        try
        {
            return Mod.Configuration!.RingLink;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }

    public static bool IsRingLinkOverlord()
    {
        try
        {
            return Mod.Configuration.RingLinkOverlord;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
    
    public static void SendRingPacket(int amount)
    {
        try
        {
            BouncePacket packet = new BouncePacket();
            var now = DateTime.Now;
            packet.Tags = new List<string>();
            packet.Tags.Add("RingLink");
            packet.Data = new Dictionary<string, JToken>();
            packet.Data.Add("time", now.ToUnixTimeStamp());
            packet.Data.Add("source", Mod.ArchipelagoHandler.SlotInstance);
            packet.Data.Add("amount", amount);
            Mod.ArchipelagoHandler._session.Socket.SendPacket(packet);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public static void HandleRingLink(string source, string amountStr)
    {
        try
        {
            if (!IsRingLinkEnabled())
                return;
            if (!IsRingLinkOverlord() && GameStateHandler.GetCurrentLevel() == LevelId.MetalOverlord)
                return;
            if (source == Mod.ArchipelagoHandler.SlotInstance.ToString(CultureInfo.InvariantCulture))
                return;
            if (!int.TryParse(amountStr, out var amount))
                return;
            var ringCount = GameStateGameWrites.GetRingCount();
            var newAmount = Math.Max(Math.Min(ringCount + amount, 999), 0);
            if (GameStateHandler.InGame() && Mod.Configuration!.PlaySounds)
            {
                switch (amount)
                {
                    case 1:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1004);
                        break;
                    case > 1:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1033);
                        break;
                    case < 0:
                        SoundHandler.PlaySound((int)Mod.ModuleBase, 0x1005);
                        break;
                }
            }
            GameStateGameWrites.SetRingCount(newAmount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}