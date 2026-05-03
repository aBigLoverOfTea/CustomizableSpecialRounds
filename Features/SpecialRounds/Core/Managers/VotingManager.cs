using System.Collections.Generic;
using System.Linq;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;
using MEC;
using Server = LabApi.Features.Wrappers.Server;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers
{
    public class VotingManager
    {
        private Dictionary<int, VoteOption> _votedPlayers = new Dictionary<int, VoteOption>();

        private CoroutineHandle _votingCoroutine;
        
        public bool IsVotingInProgress = false;
        
        public int VotingTimeCounter = Plugin.Instance.Config.VotingDuration;

        public SpecialRound SpecialRoundInVoting;
        
        public void Vote(int playerId, VoteOption voteOption)
        {
            _votedPlayers[playerId] = voteOption;
        }

        public int GetVoteCount(VoteOption vote = VoteOption.Any)
        {
            if (vote == VoteOption.Any)
            {
                return _votedPlayers.Count;
            }
            
            return _votedPlayers.Count(pair => pair.Value == vote);
        }

        public int GetAbsentVotersCount()
        {
            return Player.List.Count(player => !player.IsNPC) - _votedPlayers.Count;
        }

        public bool SpecialRoundWonVoting()
        {
            return GetVoteCount(VoteOption.Yes) > (GetVoteCount(VoteOption.No) + GetAbsentVotersCount());
        }

        public void Reset()
        {
            ForceKillVotingCoroutine();
            
            _votedPlayers.Clear();

            SpecialRoundInVoting = null;
            
            VotingTimeCounter = Plugin.Instance.Config.VotingDuration;
            
            Log.Debug("Voting Manager reset.");
        }
        
        public void ForceKillVotingCoroutine()
        {
            if (Timing.IsRunning(_votingCoroutine))
            {
                Timing.KillCoroutines(_votingCoroutine);
            }
            
            Log.Debug("Voting coroutine killed forcibly.");
        }

        public void StartVoting(SpecialRound specialRound)
        {
            Round.IsLobbyLocked = true;
            
            IsVotingInProgress = true;
            
            SpecialRoundInVoting = specialRound;
            
            _votingCoroutine = Timing.RunCoroutine(_voting());
            
            Log.Debug("Voting started.");
        }
        
        public bool ResetCurrentSpecialRound(SpecialRound round)
        {
            if (!IsVotingInProgress)
            {
                return false;
            }
            
            ForceKillVotingCoroutine();
                
            Server.SendBroadcast("Special Round has been reset.\nRestarting the voting...", 3, Broadcast.BroadcastFlags.Normal, true);

            Timing.CallDelayed(3.1f, () =>
            {
                StartVoting(round);
            });

            return true;
        }
        
        private IEnumerator<float> _voting()
        {
            while (Round.IsLobbyLocked)
            {
                if (VotingTimeCounter <= 0)
                {
                    if (SpecialRoundWonVoting())
                    {
                        Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound = SpecialRoundInVoting;
                        Server.SendBroadcast(BroadcastFormatter.FormatVotingBroadcast(Plugin.Instance.Config.RoundWonVotingBroadcast), 5, shouldClearPrevious:true);
                    }
                    else
                    {
                        Server.SendBroadcast(BroadcastFormatter.FormatVotingBroadcast(Plugin.Instance.Config.RoundLostVotingBroadcast), 5, shouldClearPrevious:true);
                    }
                    
                    Log.Debug($"Voting ended.\nSpecial Round: {SpecialRoundInVoting?.Name}\nHas won voting: {SpecialRoundWonVoting()}");
                    
                    SpecialRoundInVoting = null;
                    
                    Round.IsLobbyLocked = false;
                    
                    IsVotingInProgress = false;
                    
                    yield return Timing.WaitForOneFrame;
                }
                
                Server.SendBroadcast(BroadcastFormatter.FormatVotingBroadcast(Plugin.Instance.Config.VotingProgressBroadcast), 1, shouldClearPrevious:true);

                Log.Debug($"Voting tick passed.\nSpecial Round: {SpecialRoundInVoting?.Type}\nTotal amount of voters: {GetVoteCount()}\nTime left: {VotingTimeCounter}");
                
                VotingTimeCounter -= 1;
                
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}