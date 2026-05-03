using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Reroll : ICommand, IRemoteAdminCommand
    {
        public string Command { get; } = "reroll";

        public string[] Aliases { get; } = { "rr", "roll", "change" };

        public string Description { get; } = "Reroll current Special Round (works only during the voting!)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: couldn't find the command sender!";
                return false;
            }
            
            if (!playerSender.RemoteAdminAccess)
            {
                response = "You don't have permission to run this command.";
                return false;
            }

            if (Plugin.Instance.SpecialRoundsManager.VotingManager == null)
            {
                response = "Error: voting is disabled.";
                return false;
            }
            
            if (!Plugin.Instance.SpecialRoundsManager.VotingManager.IsVotingInProgress)
            {
                response = "Special Round can't be rerolled outside of a voting.";
                return false;
            }
            
            var newSpecialRound = Plugin.Instance.SpecialRoundsManager.GetRandomSpecialRound();

            if (!Plugin.Instance.SpecialRoundsManager.VotingManager.ResetCurrentSpecialRound(newSpecialRound))
            {
                response = "Error: couldn't reroll current special round!";
                return false;
            }

            response = "Reroll successful!";
            return true;
        }
    }
}