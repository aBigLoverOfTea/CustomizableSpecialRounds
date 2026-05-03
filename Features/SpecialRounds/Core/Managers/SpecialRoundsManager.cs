using System;
using System.Collections.Generic;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using JetBrains.Annotations;
using MEC;
using Server = LabApi.Features.Wrappers.Server;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers
{
    public class SpecialRoundsManager
    {
        public SpecialRound CurrentSpecialRound;

        public SpecialRoundType PreviousSpecialRoundType { get; private set; } = SpecialRoundType.None;

        public bool IsPaused;
        
        public bool FirstPlayerConnected;

        [CanBeNull]
        public readonly VotingManager VotingManager = Plugin.Instance.Config.IsVotingEnabled ? new VotingManager() : null;
        
        // Key is ID of a player who should remain invisible, and value is a CoroutineHandle of a coroutine that re-applies invisibility
        public readonly Dictionary<int, CoroutineHandle?> InvisiblePlayers = new Dictionary<int, CoroutineHandle?>();
        
        private static readonly Dictionary<EffectType, byte> AllowedEffects = new Dictionary<EffectType, byte>()
        {
            // Negative
            { EffectType.AmnesiaItems,      1 },
            { EffectType.AmnesiaVision,     1 },
            { EffectType.Blinded,           50 },
            { EffectType.Blurred,           1 },
            { EffectType.Burned,            1 },
            { EffectType.Concussed,         1 },
            { EffectType.Deafened,          1 },
            { EffectType.Exhausted,         1 },
            { EffectType.Hemorrhage,        1 },
            { EffectType.Slowness,          25 },

            // Positive
            { EffectType.BodyshotReduction, 4 },
            { EffectType.DamageReduction,   40 },
            { EffectType.Fade,              200 },
            { EffectType.Ghostly,           1 },
            { EffectType.Invigorated,       1 },
            { EffectType.Invisible,         1 },
            { EffectType.Lightweight,       100 },
            { EffectType.MovementBoost,     Plugin.Instance.Config.ForestGumpSpeedEffectIntensity },
            { EffectType.RainbowTaste,      2 },
            { EffectType.SilentWalk,        9 },
            { EffectType.Vitality,          1 },

            // Mixed
            { EffectType.AntiScp207,        1 },
            { EffectType.Scp207,            1 },
            { EffectType.Scp1853,           1 },

            // Technical
            { EffectType.HeavyFooted,       50 },
        };
        
        public static readonly List<SpecialRoundType> AllowedSpecialRoundTypes = new List<SpecialRoundType>
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

        public static readonly Dictionary<ItemType, int> ChosenOneStartingItems = new Dictionary<ItemType, int>()
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

        public void Reset()
        {
            VotingManager?.Reset();
            
            InvisiblePlayers.Clear();

            PreviousSpecialRoundType = CurrentSpecialRound.Type;
        
            CurrentSpecialRound = SpecialRound.CreateSpecialRound(SpecialRoundType.None);
        
            FirstPlayerConnected = false;
        }
        
        public SpecialRound GetRandomSpecialRound()
        {
            var type = AllowedSpecialRoundTypes.GetRandomValue(roundType => roundType != PreviousSpecialRoundType);
            
            return SpecialRound.CreateSpecialRound(type);
        }
        
        public static void GiveItemToAllPlayers(ItemType itemType, int amount=1, string successBroadcast="", string failBroadcast="")
        {
            foreach (var player in Player.List)
            {
                GiveItemToPlayer(itemType, player, amount, successBroadcast, failBroadcast);
            }
        }

        public static void GiveItemToPlayer(ItemType itemType, Player player, int amount = 1, string successBroadcast = "", string failBroadcast = "")
        {
            if (!RunDefaultPlayerChecks(player) || player.IsScp)
            {
                return;
            }

            try
            {
                player.AddItem(itemType, amount);
            }
            catch (Exception e)
            {
                Log.Debug($"Giving item to player with ID {player.Id} failed. Exception:\n{e.Message}");
                player.Broadcast(5, failBroadcast, shouldClearPrevious:true);
                return;
            }
            
            player.Broadcast(5, successBroadcast, shouldClearPrevious:true);
        }

        public void GiveRandomEffectToPlayer(int playerId)
        {
            var effect = _getRandomEffectType();

            if (effect.Key == EffectType.Invisible)
            {
                InvisiblePlayers.Add(playerId, null);
            }
            
            var player = Player.Get(playerId);
                    
            player.EnableEffect(effect.Key);
            player.ChangeEffectIntensity(effect.Key, effect.Value);
            player.Broadcast(5, BroadcastFormatter.GetFormatedDrugTestingBroadcast(effect.Key.ToString()), shouldClearPrevious:true);
        }
        
        public static bool RunDefaultPlayerChecks(Player player)
        {
            return Plugin.Instance.Config.Debug ? !player.IsTutorial : !(player.IsNPC || player.IsTutorial);
        }
        
        private static KeyValuePair<EffectType, byte> _getRandomEffectType()
        {
            return AllowedEffects.GetRandomValue();
        }
    }
}