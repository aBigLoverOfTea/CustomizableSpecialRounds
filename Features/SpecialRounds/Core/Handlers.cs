using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MEC;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core
{
    public static class Handlers
    {
        public static void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned += SpecialRoundsOnAllPlayersSpawned;
            Exiled.Events.Handlers.Player.Verified += SpecialRoundsOnVerified;
            Exiled.Events.Handlers.Server.EndingRound += SpecialRoundsOnEndingRound;
            Exiled.Events.Handlers.Server.RestartingRound += SpecialRoundsOnRestartingRound;
        }

        public static void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.AllPlayersSpawned -= SpecialRoundsOnAllPlayersSpawned;
            Exiled.Events.Handlers.Player.Verified -= SpecialRoundsOnVerified;
            Exiled.Events.Handlers.Server.EndingRound -= SpecialRoundsOnEndingRound;
            Exiled.Events.Handlers.Server.RestartingRound -= SpecialRoundsOnRestartingRound;
        }

        private static void SpecialRoundsOnAllPlayersSpawned()
        {
            Log.Info($"Starting the round with the Special Round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Name}");
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
                
                Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.SubscribeEvents();
                
                return;
            }

            Timing.CallDelayed(3f, () =>
            {
                Plugin.Instance.SpecialRoundsManager.VotingManager.StartVoting(specialRound);
            });
        }
    }
}