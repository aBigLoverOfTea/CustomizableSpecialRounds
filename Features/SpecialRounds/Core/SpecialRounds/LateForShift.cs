using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Random = System.Random;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class LateForShift : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.LateForShiftName;

        private ItemType[] GivableItems { get; } = Enum.GetValues(typeof(ItemType))
            .Cast<ItemType>()
            .Where(type => {
                if (type == ItemType.None || type == ItemType.DebugRagdollMover) return false;

                var itemBase = type.GetItemBase();
                if (itemBase is null) return false;
                
                var itemCategory = itemBase.Category;
                
                return itemCategory != ItemCategory.Keycard
                       && itemCategory != ItemCategory.Ammo
                       && itemCategory != ItemCategory.Armor;
            }).ToArray();
        
        private Random Random { get; } = new Random();

        public LateForShift()
        {
            Parameters.Set(SpecialRoundKeys.LateForShift.ShouldGiveAmmo, Plugin.Instance.Config.LateForShiftShouldGiveAmmo);
        }

        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused || ev.Player.IsDead) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;
            
            ev.Player.Broadcast(5, Plugin.Instance.Config.LateForShiftOnSpawnedBroadcast, shouldClearPrevious:true);
            
            if (ev.Player.Items.IsEmpty())
            {
                _giveRandomItem(ev.Player);
                return;
            }
            
            var itemsToRemove = ev.Player.Items.Where(item => !item.IsArmor && !item.IsKeycard && !item.IsAmmo).ToList();
            
            foreach (var item in itemsToRemove)
            {
                ev.Player.RemoveItem(item);
            }

            for (var i = itemsToRemove.Count(); i > 0; i--)
            {
                _giveRandomItem(ev.Player);
            }

            if (!ev.Player.IsCHI && !ev.Player.IsFoundationForces && ev.Player.Role.Type != RoleTypeId.FacilityGuard) return;

            if (ev.Player.CurrentArmor == null) return;

            var ammoTypes = Enum.GetValues(typeof(AmmoType)).Cast<AmmoType>();
            
            foreach (var ammoType in ammoTypes)
            {
                ev.Player.SetAmmo(ammoType, ev.Player.GetAmmoLimit(ammoType));
            }
        }

        private void _giveRandomItem(Player player)
        {
            ItemType item;
            ItemCategory itemCategory = ItemCategory.None;

            do
            {
                item = GivableItems[Random.Next(GivableItems.Length)];
                
                if (item.GetItemBase() == null) continue;
                
                itemCategory = item.GetCategory();
                
            } while ( itemCategory == ItemCategory.None || player.CountItem( itemCategory ) >= player.GetCategoryLimit( itemCategory ) );
            
            player.AddItem(item);

            if (item.IsWeapon() && Parameters.Get<bool>(SpecialRoundKeys.LateForShift.ShouldGiveAmmo))
            {
                var ammoType = item.GetAmmoType();

                if (ammoType == AmmoType.None) return;
                
                player.SetAmmo(ammoType, player.GetAmmoLimit(ammoType));
            };
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