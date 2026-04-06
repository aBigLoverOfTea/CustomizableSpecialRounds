using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds
{
    public class SpecialRoundsManager
    {
        private Dictionary<int, VoteOptions> _votedPlayers = new Dictionary<int, VoteOptions>();
        
        public SpecialRoundType CurrentSpecialRound { get; set; } = SpecialRoundType.None;

        public SpecialRoundType PreviousSpecialRoundType { get; private set; } = SpecialRoundType.None;
        
        public bool FirstPlayerConnected = false;

        public int VotingTimeCounter = Plugin.Instance.Config.VotingDuration;
        
        public readonly static Dictionary<EffectType, byte> AllowedEffects = new Dictionary<EffectType, byte>()
        {
            // Negative
            { EffectType.AmnesiaItems,      1 },
            { EffectType.AmnesiaVision,     1 },
            { EffectType.Asphyxiated,       1 },
            { EffectType.Blinded,           50 },
            { EffectType.Blurred,           1 },
            { EffectType.Burned,            1 },
            { EffectType.Concussed,         1 },
            { EffectType.Deafened,          1 },
            { EffectType.Disabled,          1 },
            { EffectType.Exhausted,         1 },
            { EffectType.Hemorrhage,        1 },
            { EffectType.Slowness,          20 },
            { EffectType.Traumatized,       1 },

            // Positive
            { EffectType.BodyshotReduction, 4 },
            { EffectType.DamageReduction,   40 },
            { EffectType.Fade,              200 },
            { EffectType.Ghostly,           1 },
            { EffectType.Invigorated,       1 },
            { EffectType.Invisible,         1 },
            { EffectType.Lightweight,       100 },
            { EffectType.MovementBoost,     Plugin.Instance.Config.ForestGumoSpeedEffectIntensity },
            { EffectType.RainbowTaste,      2 },
            { EffectType.SilentWalk,        9 },
            { EffectType.Vitality,          1 },

            // Mixed
            { EffectType.AntiScp207,        1 },
            { EffectType.Scp207,            1 },
            { EffectType.Scp1853,           1 },

            // Technical
            { EffectType.HeavyFooted,       50 },
            { EffectType.InsufficientLighting, 1 },
            { EffectType.Scp1576,           1 },
        };
        
        public static List<SpecialRoundType> AllowedSpecialRoundTypes = new List<SpecialRoundType>
        {
            SpecialRoundType.Payday,
            SpecialRoundType.VitalityShift,
            SpecialRoundType.SweetTooth,
            SpecialRoundType.ForestGump,
            SpecialRoundType.SuperBalling,
            SpecialRoundType.Chill,
            SpecialRoundType.ZergRush,
            SpecialRoundType.DrugTesting,
            SpecialRoundType.OneManArmy
        };

        public static Dictionary<ItemType, int> ChosenOneStartingItems = new Dictionary<ItemType, int>()
        {
            { ItemType.MicroHID, 1 },
            { ItemType.Adrenaline, 1},
            { ItemType.ArmorHeavy, 1},
            { ItemType.GunFRMG0, 1},
            { ItemType.GunLogicer, 1},
            { ItemType.GunE11SR, 1},
            { ItemType.KeycardGuard, 1},
            { ItemType.Ammo9x19, 10},
            { ItemType.Ammo556x45, 6},
            { ItemType.Ammo762x39, 6},
            { ItemType.Ammo12gauge, 5},
            { ItemType.Ammo44cal, 11}
        };

        public void Vote(int playerId, VoteOptions voteOptions)
        {
            _votedPlayers[playerId] = voteOptions;
        }

        public int GetVoteCount(VoteOptions vote = VoteOptions.Any)
        {
            if (vote == VoteOptions.Any)
            {
                return _votedPlayers.Count;
            }
            
            return _votedPlayers.Count(pair => pair.Value == vote);
        }

        public int GetAbsentVotersCount()
        {
            return Player.List.Count(player => !player.IsNPC) - _votedPlayers.Count;
        }

        public bool SpecialRoundWonVote()
        {
            return GetVoteCount(VoteOptions.Yes) > (GetVoteCount(VoteOptions.No) + GetAbsentVotersCount());
        }

        public void Reset()
        {
            _votedPlayers = new Dictionary<int, VoteOptions>();

            PreviousSpecialRoundType = CurrentSpecialRound;
        
            CurrentSpecialRound = SpecialRoundType.None;
        
            FirstPlayerConnected = false;

            VotingTimeCounter = Plugin.Instance.Config.VotingDuration;
        }
        
        public SpecialRoundType GetRandomSpecialRound()
        {
            return AllowedSpecialRoundTypes.GetRandomValue(roundType => roundType != PreviousSpecialRoundType);
        }

        public static string GetSpecialRoundTypeName(SpecialRoundType type)
        {
            var initialUpperFlag = false;
            
            var sb = new StringBuilder();
    
            foreach (var c in type.ToString())
            {
                if (char.IsUpper(c))
                {
                    if (initialUpperFlag)
                    {
                        sb.Append(' ');
                    }
                    
                    initialUpperFlag = true;
                }
        
                sb.Append(c);
            }
    
            return sb.ToString();
        }

        public static KeyValuePair<EffectType, byte> GetRandomEffectType()
        {
            return AllowedEffects.GetRandomValue();
        }
    }
}