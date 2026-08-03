using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility
{
    public static class Helper
    {
        public static void GiveItemToAllPlayers(ItemType itemType, int amount=1, string successBroadcast="", string failBroadcast="", bool skipIfPlayerHasItem=false)
        {
            foreach (var player in Player.List)
            {
                GiveItemToPlayer(itemType, player, amount, successBroadcast, failBroadcast);
            }
        }

        public static void GiveItemToPlayer(ItemType itemType, Player player, int amount = 1, string successBroadcast = "", string failBroadcast = "", bool skipIfPlayerHasItem=false)
        {
            if (!RunDefaultPlayerChecks(player) || player.IsScp)
            {
                return;
            }

            if (player.HasItem(itemType) && skipIfPlayerHasItem)
            {
                player.Broadcast(5, successBroadcast, shouldClearPrevious:true);
                return;
            }

            try
            {
                player.AddItem(itemType, amount);
            }
            catch (Exception e)
            {
                Log.Debug($"Giving item to player with ID {player.Id} failed. Exception:\n{e.Message}");
                player.Broadcast(5, failBroadcast, shouldClearPrevious:true);
                return;
            }
            
            player.Broadcast(5, successBroadcast, shouldClearPrevious:true);
        }

        public static bool RunDefaultPlayerChecks(Player player)
        {
            return Plugin.Instance.Config.Debug ? !(player.IsTutorial || player.IsDead) : !(player.IsNPC || player.IsTutorial || player.IsDead);
        }
    }
}