using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Pause : ICommand, IRemoteAdminCommand
    {
        public string Command { get; } = "pause";
        
        public string[] Aliases { get; } = { "p", "stop", "unpause", "up", "resume" };

        public string Description { get; } = "Pause or unpause current Special Round (works only during the round!)";
        
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

            if (Plugin.Instance.SpecialRoundsManager.VotingManager != null && Plugin.Instance.SpecialRoundsManager.VotingManager.IsVotingInProgress)
            {
                response = "Command can't be used during voting!";
                return false;
            }

            if (Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Type == SpecialRoundType.None)
            {
                response = "Error: Special Round not found.";
                return false;
            }

            if (Plugin.Instance.SpecialRoundsManager.IsPaused)
            {
                Plugin.Instance.SpecialRoundsManager.IsPaused = false;
                
                response = "Special Round resumed!";
                return true;
            }

            Plugin.Instance.SpecialRoundsManager.IsPaused = true;
            
            response = "Special Round paused!";
            return true;
        }
    }
}