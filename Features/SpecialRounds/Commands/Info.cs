using System;
using System.Linq;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Info : ICommand, IRemoteAdminCommand
    {
        public string Command { get; } = "info";
        
        public string[] Aliases { get; } = { "i", "information" };
        
        public string Description { get; } = "Get current plugin info (current Special Round, configurable modificators, etc.)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: couldn't find the command sender!";
                return false;
            }

            if (!playerSender.RemoteAdminAccess)
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            var allowedSpecialRounds = "";

            foreach (var specialRoundType in Plugin.Instance.SpecialRoundsManager.SpecialRoundTypes)
            {
                allowedSpecialRounds += specialRoundType
                                            .ToString()
                                            .Substring(specialRoundType
                                                .ToString()
                                                .LastIndexOf(".", StringComparison.Ordinal)+1) 
                                        + ", ";
            }

            response = $"\n---Customizable Special Rounds by {Plugin.Instance.Author}---\n" +
                       "--Plugin information--\n" +
                       $"* Version: {Plugin.Instance.Version}\n" +
                       $"* Is debug mode on: {Plugin.Instance.Config.Debug}\n" +
                       $"* Are force spawned players affected by plugin: {Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers}\n" +
                       $"* Is voting enabled: {Plugin.Instance.Config.IsVotingEnabled}\n" +
                       $"* Current Special Round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Name}\n" +
                       $"* Previous Special Round: {Plugin.Instance.SpecialRoundsManager.PreviousSpecialRoundType?.Name}\n" +
                       $"* Registered Special Rounds: {allowedSpecialRounds}\n";

            if (Plugin.Instance.SpecialRoundsManager.VotingManager != null)
            {
                response += "\n--Voting information--\n" +
                            $"* Voting duration: {Plugin.Instance.Config.VotingDuration} seconds\n" +
                            $"* Current time left: {Plugin.Instance.SpecialRoundsManager.VotingManager.VotingTimeCounter} seconds\n" +
                            $"* Is voting in progress: {Plugin.Instance.SpecialRoundsManager.VotingManager.IsVotingInProgress}\n" +
                            $"* Amount of voters: {Plugin.Instance.SpecialRoundsManager.VotingManager.GetVoteCount()}\n" +
                            $"** \"Yes\" votes: {Plugin.Instance.SpecialRoundsManager.VotingManager.GetVoteCount(VoteOption.Yes)}\n" +
                            $"** \"No\" votes: {Plugin.Instance.SpecialRoundsManager.VotingManager.GetVoteCount(VoteOption.No)}\n" +
                            $"** Abstained voters: {Plugin.Instance.SpecialRoundsManager.VotingManager.GetAbsentVotersCount()}\n";
            }

            var parameters = Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Aggregate("", (current, parameter) => current + ("* " + parameter.Key + ": " + parameter.Value + "\n"));

            response += "\n--Current Special Round Parameters--\n" + parameters;
            
            return true;
        }
    }
}