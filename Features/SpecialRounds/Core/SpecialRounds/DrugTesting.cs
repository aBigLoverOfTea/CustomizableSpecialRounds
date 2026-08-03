using System.Collections.Generic;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using MEC;
using Player = Exiled.API.Features.Player;
using Server = LabApi.Features.Wrappers.Server;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class DrugTesting : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.DrugTestingName;
        
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

        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;
            
            var spawnProtectionDuration = (ushort)Server.SpawnProtectDuration;

            if (ev.Player.IsSpawnProtected && Server.SpawnProtectDuration >= 1)
            {
                ev.Player.Broadcast(spawnProtectionDuration, "Spawn protection detected! Please, wait.", shouldClearPrevious:true);

                Timing.CallDelayed(spawnProtectionDuration + 1, () =>
                {
                    _giveRandomEffectToPlayer(ev.Player); // Usage of a MEC for every player is horrible, needs to be optimized
                });

                return;
            }
            
            _giveRandomEffectToPlayer(ev.Player);
        }
        
        private void _giveRandomEffectToPlayer(Player player)
        {
            var effect = AllowedEffects.GetRandomValue();
                    
            player.EnableEffect(effect.Key);
            player.ChangeEffectIntensity(effect.Key, effect.Value);
            
            player.Broadcast(5, BroadcastFormatter.GetFormatedDrugTestingBroadcast(effect.Key.ToString()), shouldClearPrevious:true);
        }
        
        public override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
        }

        public override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
        }
    }
}