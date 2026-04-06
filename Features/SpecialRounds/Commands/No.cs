using System;
using CommandSystem;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    public class No : ICommand
    {
        public string Command { get; } = "no";

        public string[] Aliases { get; } = new[] { "n", "0", "-" };

        public string Description { get; } = "Vote \"No\" for the current selected special round (works only during the voting!)";
        
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
            
            Plugin.Instance.SpecialRoundsManager.Vote(player.Id, VoteOptions.No);

            response = "Your vote has been accepted.";
            return true;
        }
    }
}