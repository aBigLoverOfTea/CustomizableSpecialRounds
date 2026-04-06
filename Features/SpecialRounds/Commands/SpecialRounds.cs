using System;
using CommandSystem;
using Exiled.API.Features;
using SpecialRounds.Features.SpecialRounds.Commands;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpecialRounds : ParentCommand
    {
        public SpecialRounds() => LoadGeneratedCommands();
        
        public override string Command { get; } = "specialrounds";
        
        public override string[] Aliases { get; } = new[] { "spr", "csr", "specialr", "csrounds", "srounds" };

        public override string Description { get; } = "Customizes Special Rounds' commands.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Yes());
            RegisterCommand(new No());
            RegisterCommand(new Info());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: no player found.";
                return false;
            }
            
            response = "Customizable Special Rounds subcommands:\n" +
                       "yes: vote \"yes\" for the current selected special round (works only during the voting!)\n" +
                       "no: vote \"no\" for the current selected special round (works only during the voting!)\n";

            if (!playerSender.RemoteAdminAccess)
            {
                return true;
            }

            response += "\nRA subcommands:\n" +
                        "info: get current plugin info (current special round, configurable modificators, etc.)\n";
            
            return true;
        }
    }
}