
using Sonic_Heroes_AP_Client.Definitions;

namespace Sonic_Heroes_AP_Client.Sanity.AbilityAndCharacter;

public class AbilityCharacterDefinitions
{
    public static readonly Dictionary<Team, Dictionary<FormationChar, List<Ability>>> AbilityListForTeamAndChar = new()
    {
        {
            Team.Sonic, new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        Ability.LightDash,
                        Ability.TriangleJump,
                        Ability.LightAttack,
                        //Ability.AmyHammerHover,
                        //Ability.Invisibility,
                        //Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        Ability.DummyRings,
                        //Ability.CheeseCannon,
                        //Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                        //Ability.BellyFlop,
                    }
                },
            }
        },
        {
            Team.Dark, new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        Ability.LightDash,
                        Ability.TriangleJump,
                        //Ability.LightAttack,
                        //Ability.AmyHammerHover,
                        //Ability.Invisibility,
                        //Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        Ability.DummyRings,
                        //Ability.CheeseCannon,
                        //Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                        //Ability.BellyFlop,
                    }
                },
            }
        },
        {
            Team.Rose, new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        //Ability.LightDash,
                        //Ability.TriangleJump,
                        //Ability.LightAttack,
                        Ability.AmyHammerHover,
                        //Ability.Invisibility,
                        //Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        //Ability.DummyRings,
                        Ability.CheeseCannon,
                        //Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                        Ability.BellyFlop,
                    }
                },
            }
        },
        {
            Team.Chaotix,  new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        //Ability.LightDash,
                        Ability.TriangleJump,
                        //Ability.LightAttack,
                        //Ability.AmyHammerHover,
                        Ability.Invisibility,
                        Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        //Ability.DummyRings,
                        //Ability.CheeseCannon,
                        Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                        Ability.BellyFlop,
                    }
                },
            }
        },
        {
            Team.SuperHardMode, new Dictionary<FormationChar, List<Ability>>()
            {
                {
                    FormationChar.Speed, new List<Ability>()
                    {
                        Ability.HomingAttack,
                        Ability.Tornado,
                        Ability.RocketAccel,
                        Ability.LightDash,
                        Ability.TriangleJump,
                        Ability.LightAttack,
                        //Ability.AmyHammerHover,
                        //Ability.Invisibility,
                        //Ability.Shuriken,
                    }
                },
                {
                    FormationChar.Flying, new List<Ability>()
                    {
                        Ability.Thundershoot,
                        Ability.Flight,
                        Ability.DummyRings,
                        //Ability.CheeseCannon,
                        //Ability.FlowerSting,
                    }
                },
                {
                    FormationChar.Power, new List<Ability>()
                    {
                        //Ability.PowerAttack,
                        Ability.ComboFinisher,
                        Ability.Glide,
                        Ability.FireDunk,
                        //Ability.UltimateFireDunk,
                        //Ability.BellyFlop,
                    }
                },
            }
        }
    };
}