using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    public class No : ICommand
    {
        public string Command { get; } = "no";

        public string[] Aliases { get; } = { "n", "0", "-" };

        public string Description { get; } = "Vote \"No\" for the current selected special round (works only during the voting!)";
        
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
            
            Plugin.Instance.SpecialRoundsManager.VotingManager.Vote(player.Id, VoteOption.No);

            response = "Your vote has been accepted.";
            return true;
        }
    }
}