using CustomizableSpecialRounds.Features.SpecialRounds.Core;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using Server = LabApi.Features.Wrappers.Server;

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
            Log.Info($"Starting the round with the Special Round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Name}");

            switch (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Type)
            {
                case SpecialRoundType.SweetTooth: // This case block doesn't use SpecialRoundsManager.GiveItemToAllPlayers since it requires special logic
                    foreach (var player in Player.List)
                    {
                        if (!SpecialRoundsManager.RunDefaultPlayerChecks(player) || player.IsScp)
                        {
                            continue;
                        }

                        var candyBag = (Scp330)Item.Create(ItemType.SCP330);
                        
                        candyBag.RemoveAllCandy();

                        var candies =
                            Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(
                                SpecialRoundKeys.SweetTooth.PinkCandiesAtStart);

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
                    break;
                
                case SpecialRoundType.SuperBalling:
                    SpecialRoundsManager.GiveItemToAllPlayers(ItemType.SCP018,
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.SuperBalling.Scp018AtStart),
                        Plugin.Instance.Config.SuperBallingItemGivenBroadcast,
                        Plugin.Instance.Config.SuperBallingItemNotGivenBroadcast);
                    break;
                
                case SpecialRoundType.Chill:
                    SpecialRoundsManager.GiveItemToAllPlayers(ItemType.SCP244a,
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.Chill.Scp244AtStart),
                        Plugin.Instance.Config.ChillItemGivenBroadcast,
                        Plugin.Instance.Config.ChillItemNotGivenBroadcast);
                    break;
                
                case SpecialRoundType.ZergRush:
                    var zergRushRoleTypeId = (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters
                        .Get<int>(SpecialRoundKeys.ZergRush.RoleId);
                    
                    foreach (var player in Player.List)
                    {
                        if (!SpecialRoundsManager.RunDefaultPlayerChecks(player) || player.IsScp)
                        {
                            continue;
                        }
                        
                        player.Role.Set(zergRushRoleTypeId,
                            SpawnReason.RoundStart,
                            RoleSpawnFlags.All);
                        
                        player.Broadcast(5, BroadcastFormatter.GetFormatedZergRushBroadcast(), shouldClearPrevious:true);
                    }
                    break;
                
                case SpecialRoundType.OneManArmy:
                    var chosenPlayer = Player.List.GetRandomValue(SpecialRoundsManager.RunDefaultPlayerChecks);

                    var chosenOneRoleTypeId =
                        (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(
                            SpecialRoundKeys.OneManArmy.ChosenOneRoleId);
                    
                    chosenPlayer.Role.Set(chosenOneRoleTypeId,
                        SpawnReason.RoundStart,
                        RoleSpawnFlags.All);

                    chosenPlayer.MaxHealth = Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.OneManArmy.ChosenOneHealth);

                    chosenPlayer.Health = chosenPlayer.MaxHealth;
                    
                    chosenPlayer.RemoveItem(item => true);

                    foreach (var pair in SpecialRoundsManager.ChosenOneStartingItems)
                    {
                        chosenPlayer.AddItem(pair.Key, pair.Value);
                    }
                    
                    chosenPlayer.Broadcast(5, Plugin.Instance.Config.OneManArmyChosenOneBroadcast, shouldClearPrevious:true);
                    
                    var scpRoleId =
                        (RoleTypeId)Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(
                            SpecialRoundKeys.OneManArmy.ScpRoleId);
                    
                    var scpHealth = Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.OneManArmy.ScpHealth);

                    foreach (var player in Player.List)
                    {
                        if (!SpecialRoundsManager.RunDefaultPlayerChecks(player) || player.Id == chosenPlayer.Id)
                        {
                            continue;
                        }
                        
                        player.Role.Set(scpRoleId, SpawnReason.RoundStart, RoleSpawnFlags.All);

                        player.MaxHealth = scpHealth;

                        player.Health = player.MaxHealth;
                        
                        player.Broadcast(5, BroadcastFormatter.GetFormatedOneManArmyScpBroadcast(chosenPlayer.Nickname), shouldClearPrevious:true);
                    }
                    break;
            }
        }

        private static void SpecialRoundsOnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused)
            {
                return;
            }

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass)
            {
                return;
            }
            
            switch (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Type)
            {
                case SpecialRoundType.Payday:
                    SpecialRoundsManager.GiveItemToPlayer(ItemType.Coin,
                        ev.Player,
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys.Payday.CoinsAtStart),
                        Plugin.Instance.Config.PaydayItemGivenBroadcast,
                        Plugin.Instance.Config.PaydayItemNotGivenBroadcast);
                    break;
                
                case SpecialRoundType.VitalityShift:
                    var multiplier = ev.Player.IsScp
                        ? Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<float>(SpecialRoundKeys.VitalityShift.ScpHealthMultiplier)
                        : Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<float>(SpecialRoundKeys.VitalityShift.HumanRoleHealthMultiplier);
                    
                    ev.Player.MaxHealth *= multiplier;
                    ev.Player.Health *= multiplier;
                    
                    ev.Player.Broadcast(5, BroadcastFormatter.GetFormatedVitalityShiftBroadcast(multiplier.ToString()), shouldClearPrevious:true);
                    break;
                
                case SpecialRoundType.ForestGump:
                    ev.Player.EnableEffect<MovementBoost>();

                    var effectIntensity =
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<byte>(SpecialRoundKeys
                            .ForestGump.SpeedEffectIntensity);
                    
                    ev.Player.ChangeEffectIntensity<MovementBoost>(effectIntensity);
                    ev.Player.Broadcast(5, BroadcastFormatter.GetFormatedForestGumpBroadcast(effectIntensity.ToString()));
                    break;
                
                case SpecialRoundType.DrugTesting:
                    var spawnProtectionDuration = (ushort)Server.SpawnProtectDuration;

                    if ( ev.Player.IsSpawnProtected && Server.SpawnProtectDuration >= 1 )
                    {
                        ev.Player.Broadcast(spawnProtectionDuration, "Spawn protection detected! Please, wait.", shouldClearPrevious:true);

                        Timing.CallDelayed(spawnProtectionDuration + 1, () =>
                        {
                            Plugin.Instance.SpecialRoundsManager.GiveRandomEffectToPlayer(ev.Player.Id); // Usage of a MEC for every player is suboptimal, needs to be optimized
                        });

                        break;
                    }
                    
                    Plugin.Instance.SpecialRoundsManager.GiveRandomEffectToPlayer(ev.Player.Id);
                    
                    break;
                
                case SpecialRoundType.None:
                    Log.Warn("Warning: current Special Round type is None.");
                    break;
            }
        }

        private static void SpecialRoundsOnRespawning(RespawningTeamEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused)
            {
                return;
            }
            
            if (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Type != SpecialRoundType.OneManArmy)
            {
                return;
            }
            
            ev.IsAllowed = false;
        }

        private static void SpecialRoundOnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused)
            {
                return;
            }
            
            switch (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Type)
            {
                case SpecialRoundType.OneManArmy:

                    var chosenOneRoleType = (RoleTypeId)
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<int>(SpecialRoundKeys
                            .OneManArmy.ChosenOneRoleId);
                    
                    if (ev.Player.Role.Type != chosenOneRoleType)
                    {
                        return;
                    }

                    switch (ev.Pickup.Type)
                    {
                        case ItemType.SCP500:
                            ev.Player.ShowHint("The Chosen One doesn't need this", 1f);
                            ev.IsAllowed = false;
                            break;
                    }
                    break;
            }
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
            
            Log.Debug("First connection detected.");

            var specialRound = Plugin.Instance.SpecialRoundsManager.GetRandomSpecialRound();
            
            if (Plugin.Instance.SpecialRoundsManager.VotingManager == null)
            {
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound = specialRound;
                return;
            }

            Timing.CallDelayed(3f, () =>
            {
                Plugin.Instance.SpecialRoundsManager.VotingManager.StartVoting(specialRound);
            });
        }
    }
}