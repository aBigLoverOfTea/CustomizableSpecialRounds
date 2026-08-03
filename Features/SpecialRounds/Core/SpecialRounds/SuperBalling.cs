using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class SuperBalling : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.SuperBallingName;
        
        public SuperBalling()
        {
            Parameters.Set(SpecialRoundKeys.SuperBalling.Scp018AtStart, Plugin.Instance.Config.SuperBallingScp018AtStart);
        }

        protected override void OnAllPlayersSpawned()
        {
            Helper.GiveItemToAllPlayers(ItemType.SCP018,
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.SuperBalling.Scp018AtStart),
                Plugin.Instance.Config.SuperBallingItemGivenBroadcast,
                Plugin.Instance.Config.SuperBallingItemNotGivenBroadcast);
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