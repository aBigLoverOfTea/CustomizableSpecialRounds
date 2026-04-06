using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using Round = Exiled.API.Features.Round;

namespace CustomizableSpecialRounds.Features.SpecialRounds
{
    public static class Handlers
    {
        public static void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned += SpecialRoundsOnAllPlayerSpawned;
            Exiled.Events.Handlers.Player.Verified += SpecialRoundsOnVerified;
            Exiled.Events.Handlers.Player.Spawned += SpecialRoundsOnSpawned;
            Exiled.Events.Handlers.Server.EndingRound += SpecialRoundsOnEndingRound;
            Exiled.Events.Handlers.Server.RestartingRound += SpecialRoundsOnRestartingRound;
            Exiled.Events.Handlers.Server.RespawningTeam += SpecialRoundsOnRespawning;
            Exiled.Events.Handlers.Player.PickingUpItem += SpecialRoundOnPickingUpItem;
        }

        public static void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned -= SpecialRoundsOnAllPlayerSpawned;
            Exiled.Events.Handlers.Player.Verified -= SpecialRoundsOnVerified;
            Exiled.Events.Handlers.Player.Spawned -= SpecialRoundsOnSpawned;
            Exiled.Events.Handlers.Server.EndingRound -= SpecialRoundsOnEndingRound;
            Exiled.Events.Handlers.Server.RestartingRound -= SpecialRoundsOnRestartingRound;
            Exiled.Events.Handlers.Server.RespawningTeam -= SpecialRoundsOnRespawning;
            Exiled.Events.Handlers.Player.PickingUpItem -= SpecialRoundOnPickingUpItem;
        }
        
        private static void SpecialRoundsOnAllPlayerSpawned()
        {
            Log.Info($"Starting the round with the special round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound}");
            
            Map.ClearBroadcasts();

            switch (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound)
            {
                case SpecialRoundType.Payday:
                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.IsScp)
                        {
                            break;
                        }

                        try
                        {
                            player.AddItem(ItemType.Coin, Plugin.Instance.Config.PaydayCoinsAtStart);
                        }
                        catch
                        {
                            player.Broadcast(5, "Seems like you don't have enough place for your paycheck...");
                            break;
                        }
                        player.Broadcast(5, "You suddenly feel a little bit richer...");
                    }
                    break;
                case SpecialRoundType.SweetTooth:
                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.IsScp)
                        {
                            break;
                        }

                        Scp330 candyBag = (Scp330)Item.Create(ItemType.SCP330);
                        
                        candyBag.RemoveAllCandy();

                        for (var i = 0; i < Plugin.Instance.Config.SweetToothPinkCandiesAtStart; i++)
                        {
                            candyBag.AddCandy(CandyKindID.Pink);
                        }

                        try
                        {
                            player.AddItem(candyBag);
                        }
                        catch
                        {
                            player.Broadcast(5, "Seems like you don't have enough place for sweets...");
                            break;
                        }
                        player.Broadcast(5, "Let chaos reign!");
                    }
                    break;
                case SpecialRoundType.SuperBalling:
                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.IsScp)
                        {
                            break;
                        }

                        try
                        {
                            player.AddItem(ItemType.SCP018, Plugin.Instance.Config.SuperBallingScp018AtStart);
                        }
                        catch
                        {
                            player.Broadcast(5, "Seems like you don't have enough place for the ball...");
                            break;
                        }
                        player.Broadcast(5, "Time to play catch!");
                    }
                    break;
                case SpecialRoundType.Chill:
                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.IsScp)
                        {
                            break;
                        }

                        try
                        {
                            player.AddItem(ItemType.SCP244a, Plugin.Instance.Config.ChillScp244AtStart);
                        }
                        catch
                        {
                            player.Broadcast(5, "Seems like you don't have enough place for the chill ghost...");
                            break;
                        }
                        player.Broadcast(5, "Freeze \'em all!");
                    }
                    break;
                case SpecialRoundType.ZergRush:
                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.IsScp)
                        {
                            break;
                        }
                        
                        player.Role.Set((RoleTypeId)Plugin.Instance.Config.ZergRushRoleId, SpawnReason.RoundStart, RoleSpawnFlags.AssignInventory);
                        
                        player.Broadcast(5, "ZERG RUSH!!!");
                    }
                    break;
                case SpecialRoundType.OneManArmy:
                    var chosenPlayer = Player.List.GetRandomValue(DefaultPlayerChecks);
                    
                    chosenPlayer.Role.Set((RoleTypeId)Plugin.Instance.Config.OneManArmyChosenOneRoleId, SpawnReason.RoundStart, RoleSpawnFlags.All);

                    chosenPlayer.MaxHealth = Plugin.Instance.Config.OneManArmyChosenOneHealth;

                    chosenPlayer.Health = chosenPlayer.MaxHealth;
                    
                    chosenPlayer.RemoveItem(item => true);

                    foreach (var pair in SpecialRoundsManager.ChosenOneStartingItems)
                    {
                        chosenPlayer.AddItem(pair.Key, pair.Value);
                    }
                    
                    chosenPlayer.Broadcast(5, "<color=red><b>YOU'RE THE STAR OF THE SHOW NOW, BABY!</b></color>");

                    foreach (var player in Player.List)
                    {
                        if (!DefaultPlayerChecks(player) || player.Id == chosenPlayer.Id)
                        {
                            break;
                        }
                        
                        player.Role.Set((RoleTypeId)Plugin.Instance.Config.OneManArmyScpRoleId, SpawnReason.RoundStart, RoleSpawnFlags.All);
                        
                        player.MaxHealth = Plugin.Instance.Config.OneManArmyScpHealth;

                        player.Health = player.MaxHealth;
                        
                        player.Broadcast(5, "<color=red><b>BRING HIM DOWN!</b></color>");
                    }
                    break;
            }
        }

        private static void SpecialRoundsOnSpawned(SpawnedEventArgs ev)
        {
            switch (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound)
            {
                case SpecialRoundType.VitalityShift:
                    var multiplier = ev.Player.IsScp
                        ? Plugin.Instance.Config.VitalityShiftScpHealthMultiplier
                        : Plugin.Instance.Config.VitalityShiftHumanRoleHealthMultiplier;
                    ev.Player.MaxHealth *= multiplier;
                    ev.Player.Health *= multiplier;
                    break;
                case SpecialRoundType.ForestGump:
                    ev.Player.EnableEffect<MovementBoost>();
                    ev.Player.ChangeEffectIntensity<MovementBoost>(Plugin.Instance.Config.ForestGumoSpeedEffectIntensity);
                    break;
                case SpecialRoundType.DrugTesting:
                    var effect = SpecialRoundsManager.GetRandomEffectType();
                    
                    ev.Player.EnableEffect(effect.Key);
                    ev.Player.ChangeEffectIntensity(effect.Key, effect.Value);
                    ev.Player.Broadcast(3, $"You got: {effect.Key.ToString()}!");
                    break;
            }
        }

        private static void SpecialRoundsOnRespawning(RespawningTeamEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound != SpecialRoundType.OneManArmy)
            {
                return;
            }
            
            ev.IsAllowed = false;
        }

        private static void SpecialRoundOnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound != SpecialRoundType.OneManArmy)
            {
                return;
            }

            if (ev.Pickup.Type != ItemType.SCP500)
            {
                return;
            }

            if (ev.Player.Role.Type != (RoleTypeId)Plugin.Instance.Config.OneManArmyChosenOneRoleId)
            {
                return;
            }
            
            ev.Player.ShowHint("You don't need this.", 1f);

            ev.IsAllowed = false;
        }

        private static void SpecialRoundsOnEndingRound(EndingRoundEventArgs ev)
        {
            if (!ev.IsAllowed)
            {
                return;
            }
            
            Plugin.Instance.SpecialRoundsManager.Reset();
        }

        private static void SpecialRoundsOnRestartingRound()
        {
            Plugin.Instance.SpecialRoundsManager.Reset();
        }

        private static void SpecialRoundsOnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.FirstPlayerConnected)
            {
                return;
            }
            
            Plugin.Instance.SpecialRoundsManager.FirstPlayerConnected = true;

            var specialRound = Plugin.Instance.SpecialRoundsManager.GetRandomSpecialRound();
            
            if (!Plugin.Instance.Config.VotingIsAllowed)
            {
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound = specialRound;
                return;
            }

            Timing.CallDelayed(3f, () =>
            {
                Round.IsLobbyLocked = true;
                
                Map.ClearBroadcasts();

                Timing.RunCoroutine(Voting(specialRound));
            });
        }

        private static IEnumerator<float> Voting(SpecialRoundType specialRound)
        {
            while (Round.IsLobbyLocked)
            {
                if (Plugin.Instance.SpecialRoundsManager.VotingTimeCounter <= 0)
                {
                    var lastString = "Vote failed! Special round disabled for this round.";
                    
                    if (Plugin.Instance.SpecialRoundsManager.SpecialRoundWonVote())
                    {
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound = specialRound;
                        lastString = "Vote passed! Special round enabled for this round.";
                    }
                    
                    Map.ClearBroadcasts();
                    
                    var absentVoters = Plugin.Instance.SpecialRoundsManager.GetAbsentVotersCount();
                    
                    Map.Broadcast(5, "The voting has ended!\n" + SpecialRoundsManager.GetSpecialRoundTypeName(specialRound) + "\n" +
                                     $"Yes: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.Yes)}, No: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.No) + absentVoters}\n" +
                                     lastString);
                    
                    Round.IsLobbyLocked = false;
                    
                    yield return Timing.WaitForOneFrame;
                }
                
                Map.Broadcast(1, "Voting for special round:\n" +
                                 SpecialRoundsManager.GetSpecialRoundTypeName(specialRound) + "\n" +
                                 $"Yes: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.Yes)}, No: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.No)}\n" +
                                 $"Time left: {Plugin.Instance.SpecialRoundsManager.VotingTimeCounter} seconds.");

                Plugin.Instance.SpecialRoundsManager.VotingTimeCounter -= 1;
                
                yield return Timing.WaitForSeconds(1f);
            }
        }

        private static bool DefaultPlayerChecks(Player player)
        {
            var response = Plugin.Instance.Config.Debug ? !player.IsTutorial : !(player.IsNPC || player.IsTutorial);
            return response;
        }
    }
}