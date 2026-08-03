using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class ZergRush : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.ZergRushName;
        
        public ZergRush()
        {
            Parameters.Set(SpecialRoundKeys.ZergRush.RoleId, Plugin.Instance.Config.ZergRushRoleId);
        }

        protected override void OnAllPlayersSpawned()
        {
            var zergRushRoleTypeId = (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters
                .Get<int>(SpecialRoundKeys.ZergRush.RoleId);
                    
            foreach (var player in Player.List)
            {
                if (!Helper.RunDefaultPlayerChecks(player) || player.IsScp)
                {
                    continue;
                }
                        
                player.Role.Set(zergRushRoleTypeId, SpawnReason.RoundStart, RoleSpawnFlags.All);
                        
                player.Broadcast(5, BroadcastFormatter.GetFormatedZergRushBroadcast(), shouldClearPrevious:true);
            }
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