using System;
using CommandSystem;
using CustomizableSpecialRounds.Features.SpecialRounds.Commands.Interfaces;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Info : ICommand, IRemoteAdminCommand
    {
        public string Command { get; } = "info";
        
        public string[] Aliases { get; } = { "i", "information" };
        
        public string Description { get; } = "Get current plugin info (current special round, configurable modificators, etc.)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: no sender found.";
                return false;
            }

            if (!playerSender.RemoteAdminAccess)
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            var allowedSpecialRounds = "";

            foreach (var specialRoundType in SpecialRoundsManager.AllowedSpecialRoundTypes)
            {
                allowedSpecialRounds += specialRoundType.ToString() + ", ";
            }

            response = $"\n---Customizable Special Rounds by {Plugin.Instance.Author}---\n" +
                       "--Plugin information--\n" +
                       $"* Version: {Plugin.Instance.Version}\n" +
                       $"* Is debug mode on: {Plugin.Instance.Config.Debug}\n" +
                       $"* Are force spawned players affected by plugin: {Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers}\n" +
                       $"* Is voting enabled: {Plugin.Instance.Config.IsVotingEnabled}\n" +
                       $"* Current special round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Name}\n" +
                       $"* Previous special round: {Plugin.Instance.SpecialRoundsManager.PreviousSpecialRoundType}\n" +
                       $"* Allowed special rounds: {allowedSpecialRounds}\n";

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

            var parameters = "";

            foreach (var parameter in Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters)
            {
                parameters += "* " + parameter.Key + ": " + parameter.Value + "\n";
            }

            response += "\n--Current Special Round Parameters--\n" + parameters;
            
            return true;
        }
    }
}