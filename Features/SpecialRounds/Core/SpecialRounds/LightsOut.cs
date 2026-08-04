using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class LightsOut : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.LightsOutName;

        public LightsOut()
        {
            Parameters.Set(SpecialRoundKeys.LightsOut.ScpHealthMultiplier, Plugin.Instance.Config.LightsOutScpHealthMultiplier);
        }

        protected override void OnAllPlayersSpawned()
        {
            Map.TurnOffAllLights(float.MaxValue);
        }

        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;

            if (ev.Player.IsScp)
            {
                ev.Player.MaxHealth *= Parameters.Get<float>(SpecialRoundKeys.LightsOut.ScpHealthMultiplier);
                ev.Player.Health = ev.Player.MaxHealth;
                return;
            }
            
            Helper.GiveItemToPlayer(ItemType.Flashlight,
                ev.Player,
                1,
                Plugin.Instance.Config.LightsOutFlashlightGivenBroadcast,
                Plugin.Instance.Config.LightsOutFlashlightNotGivenBroadcast,
                true);
        }

        public override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned += OnAllPlayersSpawned;
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
        }

        public override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned -= OnAllPlayersSpawned;
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
        }
    }
}