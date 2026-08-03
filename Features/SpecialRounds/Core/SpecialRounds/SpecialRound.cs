using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class SpecialRound
    {
        public virtual string Name { get; protected set; }

        public SpecialRoundParameters Parameters { get; protected set; } = new SpecialRoundParameters();

        protected virtual void OnAllPlayersSpawned() {}

        protected virtual void OnSpawned(SpawnedEventArgs ev) {}

        protected virtual void OnRespawningTeam(RespawningTeamEventArgs ev) {}
        
        protected virtual void OnPickingUpItem(PickingUpItemEventArgs ev) {}
        public virtual void SubscribeEvents() {}
        public virtual void UnsubscribeEvents() {}
    }
}