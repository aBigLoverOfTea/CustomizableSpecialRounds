using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    public class Yes : ICommand
    {
        public string Command { get; } = "yes";

        public string[] Aliases { get; } = { "y", "1", "+" };

        public string Description { get; } = "Vote \"Yes\" for the current selected special round (works only during the voting!)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var player))
            {
                response = "Error: couldn't find the command sender!";
                return false;
            }

            if (Plugin.Instance.SpecialRoundsManager.VotingManager == null)
            {
                response = "Error: voting is disabled.";
                return false;
            }
            
            if (!Plugin.Instance.SpecialRoundsManager.VotingManager.IsVotingInProgress)
            {
                response = "You can't vote now!";
                return false;
            }
            
            Plugin.Instance.SpecialRoundsManager.VotingManager.Vote(player.Id, VoteOption.Yes);

            response = "Your vote has been accepted.";
            return true;
        }
    }
}