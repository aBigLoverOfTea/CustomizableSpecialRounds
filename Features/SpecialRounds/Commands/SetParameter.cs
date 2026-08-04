using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    public class SetParameter : ICommand, IRemoteAdminCommand
    {
        public string Command { get; } = "setparameter";

        public string[] Aliases { get; } = { "setparam", "sp", "setp", "set", "setpar" };

        public string Description { get; } = "Sets the specified parameter of the current Special Round.";
        
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

            if (arguments.Count != 2)
            {
                response = "Usage: setparameter <parameter name> <value>\nFull list of parameter names can be found in GitHub distro (README.md)";
                return true;
            }

            if (!Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.TrySetExisting(arguments[0],
                    arguments[1], out var error))
            {
                response = error;
                return false;
            }
            
            response = $"Parameter {arguments[0]} set to {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<string>(arguments[0])} successfully!";
            return true;
        }
    }
}