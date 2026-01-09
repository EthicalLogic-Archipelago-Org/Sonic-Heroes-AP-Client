
using System.Numerics;
using Sonic_Heroes_AP_Client.Definitions;
namespace Sonic_Heroes_AP_Client.LevelSpawnPosition;

public class LevelSpawnEntry (float x, float y, float z, ushort pitch = 0x0080, SpawnMode mode = SpawnMode.Normal, ushort runningtime = 0, bool secret = false, bool isdefault = false, bool bonusstage = false, ushort paddingShort = 0x0000)
{
    public Vector3 Pos = new Vector3(x, y, z);
    public ushort Pitch = pitch;
    public SpawnMode Mode = mode;
    public ushort RunningTime = runningtime;
    public bool Secret = secret;
    public bool Bonusstage = bonusstage;
    public bool IsDefault = isdefault;
    public ushort PaddingShort = paddingShort;

    public override string ToString()
    {
        return $"Pos: {this.Pos}, Pitch: {this.Pitch}, Mode: {this.Mode},  RunningTime: {this.RunningTime}, Secret: {this.Secret}, BonusStage: {this.Bonusstage}, IsDefault: {this.IsDefault}, PaddingShort: {this.PaddingShort}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not LevelSpawnEntry entry)
            return false;

        return this.Bonusstage == entry.Bonusstage && this.Secret == entry.Secret && Vector3.Distance(this.Pos, entry.Pos) < 100;
    }
}