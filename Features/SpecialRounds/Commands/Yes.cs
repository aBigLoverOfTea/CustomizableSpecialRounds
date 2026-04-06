using System;
using CommandSystem;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    public class Yes : ICommand
    {
        public string Command { get; } = "yes";

        public string[] Aliases { get; } = new[] { "y", "1", "+" };

        public string Description { get; } = "Vote \"Yes\" for the current selected special round (works only during the voting!)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var player))
            {
                response = "Error: couldn't find the command sender!";
                return false;
            }

            if (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound != SpecialRoundType.None)
            {
                response = "You can't vote now!";
                return false;
            }
            
            Plugin.Instance.SpecialRoundsManager.Vote(player.Id, VoteOptions.Yes);

            response = "Your vote has been accepted.";
            return true;
        }
    }
}