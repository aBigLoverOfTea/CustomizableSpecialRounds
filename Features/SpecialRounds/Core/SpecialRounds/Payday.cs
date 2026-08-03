using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class Payday : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.PaydayName;

        public Payday()
        {
            Parameters.Set(SpecialRoundKeys.Payday.CoinsAtStart, Plugin.Instance.Config.PaydayCoinsAtStart);
        }
        
        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;
            
            Helper.GiveItemToPlayer(ItemType.Coin,
                ev.Player,
                Parameters.Get<int>(SpecialRoundKeys.Payday.CoinsAtStart),
                Plugin.Instance.Config.PaydayItemGivenBroadcast,
                Plugin.Instance.Config.PaydayItemNotGivenBroadcast);
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