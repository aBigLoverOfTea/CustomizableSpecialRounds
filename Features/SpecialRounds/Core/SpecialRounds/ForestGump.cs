using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class ForestGump : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.ForestGumpName;

        public ForestGump()
        {
            Parameters.Set(SpecialRoundKeys.ForestGump.SpeedEffectIntensity, Plugin.Instance.Config.ForestGumpSpeedEffectIntensity);
        }

        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;
            
            ev.Player.EnableEffect<MovementBoost>();

            var effectIntensity =
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<byte>(SpecialRoundKeys
                    .ForestGump.SpeedEffectIntensity);
                    
            ev.Player.ChangeEffectIntensity<MovementBoost>(effectIntensity);
            
            ev.Player.Broadcast(5, BroadcastFormatter.GetFormatedForestGumpBroadcast(effectIntensity.ToString()));
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