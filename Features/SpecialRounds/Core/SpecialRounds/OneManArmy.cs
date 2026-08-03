using System.Collections.Generic;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class OneManArmy : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.OneManArmyName;

        private int _chosenOnePlayerId;
        
        // Key: item, value: amount of the item to give to the Chosen One
        private readonly Dictionary<ItemType, int> _chosenOneStartingItems = new Dictionary<ItemType, int>()
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
        
        public OneManArmy()
        {
            Parameters.Set(SpecialRoundKeys.OneManArmy.ChosenOneRoleId, Plugin.Instance.Config.OneManArmyChosenOneRoleId);
            Parameters.Set(SpecialRoundKeys.OneManArmy.ChosenOneHealth, Plugin.Instance.Config.OneManArmyChosenOneHealth);
            Parameters.Set(SpecialRoundKeys.OneManArmy.ChosenOneSpecialWeaponsLimit, Plugin.Instance.Config.OneManArmyChosenOneSpecialWeaponsLimit);
            Parameters.Set(SpecialRoundKeys.OneManArmy.ScpRoleId, Plugin.Instance.Config.OneManArmyScpRoleId);
            Parameters.Set(SpecialRoundKeys.OneManArmy.ScpHealth, Plugin.Instance.Config.OneManArmyScpHealth);
        }

        protected override void OnAllPlayersSpawned()
        {
            var chosenPlayer = Player.List.GetRandomValue(Helper.RunDefaultPlayerChecks);

            _chosenOnePlayerId = chosenPlayer.Id;

            var chosenOneRoleTypeId =
                (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(
                    SpecialRoundKeys.OneManArmy.ChosenOneRoleId);
            
            chosenPlayer.Role.Set(chosenOneRoleTypeId, SpawnReason.RoundStart, RoleSpawnFlags.UseSpawnpoint);
            
            chosenPlayer.MaxHealth = Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.OneManArmy.ChosenOneHealth);
            
            chosenPlayer.Health = chosenPlayer.MaxHealth;
            
            chosenPlayer.RemoveItem(item => true); // this line is here to make sure that even the items that are given by other plugins are removed
            
            chosenPlayer.SetCategoryLimit(ItemCategory.SpecialWeapon,Parameters.Get<sbyte>(SpecialRoundKeys.OneManArmy.ChosenOneSpecialWeaponsLimit));

            foreach (var pair in _chosenOneStartingItems) {
                chosenPlayer.AddItem(pair.Key, pair.Value);
            }
            
            chosenPlayer.Broadcast(5, Plugin.Instance.Config.OneManArmyChosenOneBroadcast, shouldClearPrevious:true);

            var scpRoleId =
                (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.OneManArmy.ScpRoleId);

            var scpHealth = Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.OneManArmy.ScpHealth);

            foreach (var player in Player.List)
            {
                if (!Helper.RunDefaultPlayerChecks(player) || player.Id == _chosenOnePlayerId)
                {
                    continue;
                }
                
                player.Role.Set(scpRoleId, SpawnReason.RoundStart, RoleSpawnFlags.All);

                player.MaxHealth = scpHealth;

                player.Health = player.MaxHealth;
                
                player.Broadcast(5, BroadcastFormatter.GetFormatedOneManArmyScpBroadcast(chosenPlayer.Nickname), shouldClearPrevious:true);
            }
        }

        protected override void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;
            
            ev.IsAllowed = false;
        }

        protected override void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player.Id != _chosenOnePlayerId || ev.Pickup.Type != ItemType.SCP500) return;

            ev.Player.ShowHint("The Chosen One doesn't need this", 1f);
            
            ev.IsAllowed = false;
        }
        
        public override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned += OnAllPlayersSpawned;
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawningTeam;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }

        public override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned -= OnAllPlayersSpawned;
            Exiled.Events.Handlers.Server.RespawningTeam -= OnRespawningTeam;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        }
    }
}