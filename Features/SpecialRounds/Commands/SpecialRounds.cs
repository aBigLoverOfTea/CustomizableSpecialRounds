using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpecialRounds : ParentCommand
    {
        public SpecialRounds() => LoadGeneratedCommands();
        
        public override string Command { get; } = "specialrounds";
        
        public override string[] Aliases { get; } = { "spr", "csr", "specialr", "csrounds", "srounds" };

        public override string Description { get; } = "Customizes Special Rounds' commands.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Yes());
            RegisterCommand(new No());
            RegisterCommand(new Info());
            RegisterCommand(new Pause());
            RegisterCommand(new SetParameter());
            RegisterCommand(new Reroll());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: no sender found.";
                return false;
            }

            var standardCommands = "";
            var remoteAdminCommands = "";

            foreach (var command in Commands)
            {
                if (command.Value is IRemoteAdminCommand)
                {
                    remoteAdminCommands += $"* {command.Value.Command}: {command.Value.Description}\n";
                    continue;
                }
                
                standardCommands += $"* {command.Value.Command}: {command.Value.Description}\n";
            }

            response = "---Customizable Special Rounds subcommands---\n" + standardCommands;

            if (!playerSender.RemoteAdminAccess)
            {
                return true;
            }

            response += "\n---RA subcommands---\n" + remoteAdminCommands;
            
            return true;
        }
    }
}