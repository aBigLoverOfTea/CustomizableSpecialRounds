using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using InventorySystem.Items.Usables.Scp330;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class SweetTooth : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.SweetToothName;
        
        public SweetTooth()
        {
            Parameters.Set(SpecialRoundKeys.SweetTooth.PinkCandiesAtStart, Plugin.Instance.Config.SweetToothPinkCandiesAtStart);
        }

        protected override void OnAllPlayersSpawned()
        {
            foreach (var player in Player.List) // This case block doesn't use SpecialRoundsManager.GiveItemToAllPlayers since it requires special logic
            {
                if (!Helper.RunDefaultPlayerChecks(player) || player.IsScp)
                {
                    continue;
                }

                var candyBag = (Scp330)Item.Create(ItemType.SCP330);
                        
                candyBag.RemoveAllCandy();

                var candies =
                    Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.SweetTooth.PinkCandiesAtStart);

                for (var i = 0; i < candies; i++)
                {
                    candyBag.AddCandy(CandyKindID.Pink);
                }

                try
                {
                    player.AddItem(candyBag);
                }
                catch
                {
                    player.Broadcast(5, Plugin.Instance.Config.SweetToothItemNotGivenBroadcast, shouldClearPrevious:true);
                    continue;
                }
                
                player.Broadcast(5, Plugin.Instance.Config.SweetToothItemGivenBroadcast, shouldClearPrevious:true);
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