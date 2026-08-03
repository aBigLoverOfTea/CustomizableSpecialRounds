using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class Chill : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.ChillName;

        public Chill()
        {
            Parameters.Set(SpecialRoundKeys.Chill.Scp244AtStart, Plugin.Instance.Config.ChillScp244AtStart);
        }

        protected override void OnAllPlayersSpawned()
        {
            Helper.GiveItemToAllPlayers(ItemType.SCP244a,
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.Chill.Scp244AtStart),
                Plugin.Instance.Config.ChillItemGivenBroadcast,
                Plugin.Instance.Config.ChillItemNotGivenBroadcast);
        }

        public override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned += OnAllPlayersSpawned;
        }

        public override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned -= OnAllPlayersSpawned;
        }
    }
}