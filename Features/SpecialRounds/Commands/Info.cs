using System;
using CommandSystem;
using CustomizableSpecialRounds;
using CustomizableSpecialRounds.Features.SpecialRounds;
using Exiled.API.Features;
using PlayerRoles;

namespace SpecialRounds.Features.SpecialRounds.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Info : ICommand
    {
        public string Command { get; } = "info";
        public string[] Aliases { get; } = { "i", "information" };
        public string Description { get; } = "Get current plugin info (current special round, configurable modificators, etc.)";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out var playerSender))
            {
                response = "Error: player not found";
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
                       $"* Is voting allowed: {Plugin.Instance.Config.VotingIsAllowed}\n" +
                       $"* Current special round: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound}\n" +
                       $"* Previous special round: {Plugin.Instance.SpecialRoundsManager.PreviousSpecialRoundType}\n" +
                       $"* Allowed special rounds: {allowedSpecialRounds}\n";

            if (Plugin.Instance.Config.VotingIsAllowed)
            {
                response += "\n--Voting information--\n" +
                            $"* Voting duration: {Plugin.Instance.Config.VotingDuration} seconds\n" +
                            $"* Current time left: {Plugin.Instance.SpecialRoundsManager.VotingTimeCounter} seconds\n" +
                            $"* Has voting started: {Plugin.Instance.SpecialRoundsManager.FirstPlayerConnected}\n" +
                            $"* Has voting ended: {Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound != SpecialRoundType.None}\n" +
                            $"* Amount of voters: {Plugin.Instance.SpecialRoundsManager.GetVoteCount()}\n" +
                            $"** \"Yes\" votes: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.Yes)}\n" +
                            $"** \"No\" votes: {Plugin.Instance.SpecialRoundsManager.GetVoteCount(VoteOptions.No)}\n" +
                            $"** Abstained voters: {Plugin.Instance.SpecialRoundsManager.GetAbsentVotersCount()}\n";
            }

            response += "\n--Special Rounds Configurable Information--\n" +
                        "-Payday-\n" +
                        $"* Starting coins amount: {Plugin.Instance.Config.PaydayCoinsAtStart}\n" +
                        "\n-Vitality Shift-\n" +
                        $"* Human role health multiplier: {Plugin.Instance.Config.VitalityShiftHumanRoleHealthMultiplier}\n" +
                        $"* SCP role health multiplier: {Plugin.Instance.Config.VitalityShiftScpHealthMultiplier}\n" +
                        "\n-Sweet Tooth-\n" +
                        $"* Pink candies starting amount: {Plugin.Instance.Config.SweetToothPinkCandiesAtStart}\n" +
                        "\n-Forest Gump-\n" +
                        $"* Speed effect intensity: {Plugin.Instance.Config.ForestGumoSpeedEffectIntensity}\n" +
                        "\n-Super Balling-\n" +
                        $"* SCP-018 starting amount: {Plugin.Instance.Config.SuperBallingScp018AtStart}\n" +
                        "\n-Chill-\n" +
                        $"* SCP-244 starting amount: {Plugin.Instance.Config.ChillScp244AtStart}\n" +
                        "\n-Zerg Rush-\n" +
                        $"* Starting role: {((RoleTypeId)Plugin.Instance.Config.ZergRushRoleId).ToString()} (Role ID: {Plugin.Instance.Config.ZergRushRoleId})\n" +
                        "\n-One Man Army-\n" +
                        $"* SCP role: {((RoleTypeId)Plugin.Instance.Config.OneManArmyScpRoleId).ToString()} (Role ID: {Plugin.Instance.Config.OneManArmyScpRoleId})\n" +
                        $"* SCP starting health: {Plugin.Instance.Config.OneManArmyScpHealth}\n" +
                        $"* Chosen one role: {((RoleTypeId)Plugin.Instance.Config.OneManArmyChosenOneRoleId).ToString()} (Role ID: {Plugin.Instance.Config.OneManArmyChosenOneRoleId})\n" +
                        $"* Chosen one starting health: {Plugin.Instance.Config.OneManArmyChosenOneHealth}";
            return true;
        }
    }
}