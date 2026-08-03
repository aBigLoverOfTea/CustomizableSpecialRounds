using System;
using System.Collections.Generic;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using PlayerRoles;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility
{
    public static class BroadcastFormatter
    {
        private static readonly Dictionary<string, Func<string>> VotingPlaceholders = new Dictionary<string, Func<string>>
        {
            { "{votes_yes}",                () => Plugin.Instance.SpecialRoundsManager.VotingManager?.GetVoteCount(VoteOption.Yes).ToString() },
            { "{votes_no}",                 () => Plugin.Instance.SpecialRoundsManager.VotingManager?.GetVoteCount(VoteOption.No).ToString() },
            { "{votes_absent}",             () => Plugin.Instance.SpecialRoundsManager.VotingManager?.GetAbsentVotersCount().ToString() },
            { "{votes}",                    () => Plugin.Instance.SpecialRoundsManager.VotingManager?.GetVoteCount().ToString() },
            { "{time_left}",                () => Plugin.Instance.SpecialRoundsManager.VotingManager?.VotingTimeCounter.ToString() },
            { "{round_in_voting}",          () => Plugin.Instance.SpecialRoundsManager.VotingManager?.SpecialRoundInVoting.Name },
        };
        
        public static string FormatVotingBroadcast(string broadcast)
        {
            foreach (var placeholder in VotingPlaceholders)
            {
                broadcast = broadcast.Replace(placeholder.Key, placeholder.Value());
            }
            
            return broadcast;
        }

        public static string GetFormatedDrugTestingBroadcast(string effectName)
        {
            return Plugin.Instance.Config.DrugTestingBroadcast.Replace("{effect}", effectName);
        }

        public static string GetFormatedOneManArmyScpBroadcast(string chosenOneName)
        {
            return Plugin.Instance.Config.OneManArmyScpBroadcast.Replace("{chosen_name}", chosenOneName);
        }

        public static string GetFormatedVitalityShiftBroadcast(string healthMultiplier)
        {
            return Plugin.Instance.Config.VitalityShiftBroadcast.Replace("{health_multiplier}", healthMultiplier);
        }

        public static string GetFormatedForestGumpBroadcast(string speedIntensity)
        {
            return Plugin.Instance.Config.ForestGumpBroadcast.Replace("{speed_intensity}", speedIntensity);
        }

        public static string GetFormatedZergRushBroadcast()
        {
            return Plugin.Instance.Config.ZergRushBroadcast.Replace("{zerg_role}", ((RoleTypeId)Plugin.Instance.Config.ZergRushRoleId).ToString());
        }
    }
}