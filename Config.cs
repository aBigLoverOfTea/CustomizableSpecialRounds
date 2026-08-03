using System.ComponentModel;
using Exiled.API.Interfaces;

namespace CustomizableSpecialRounds
{
    public class Config : IConfig
    {
        [Description("Should plugin be loaded on next server restart?")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Should debug mode be enabled?")]
        public bool Debug { get; set; } = false;
        
        [Description("Should the players who are force spawned (either via RA, commands or other plugins) be affected by the Special Round's on-spawn effects?")]
        public bool ShouldAffectForceSpawnedPlayers { get; set; } = true;

        [Description("Should the players be able to vote for Special Round start before the round starts?")]
        public bool IsVotingEnabled { get; set; } = true;

        [Description("How long the voting stage should last (in seconds)?")]
        public int VotingDuration { get; set; } = 20;

        [Description("How many coins should the \"Payday\" Special Round give?")]
        public int PaydayCoinsAtStart { get; set; } = 1;
        
        [Description("HP multiplier of human roles for the \"Vitality Shift\" Special Round.")]
        public float VitalityShiftHumanRoleHealthMultiplier { get; set; } = 2.0f;
        
        [Description("HP multiplier of the SCP-subjects for the \"Vitality Shift\" Special Round.")]
        public float VitalityShiftScpHealthMultiplier { get; set; } = 2f;
        
        [Description("How many pink candies should the \"Sweet Tooth\" Special Round give.")]
        public int SweetToothPinkCandiesAtStart { get; set; } = 1;
        
        [Description("Power of the speed effect during the \"Forest Gump\" Special Round.")]
        public byte ForestGumpSpeedEffectIntensity { get; set; } = 1;
        
        [Description("How many SCP-018's should the \"Super Balling\" Special Round give?")]
        public int SuperBallingScp018AtStart { get; set; } = 1;
        
        [Description("How many SCP-244's should the \"Chill\" Special Round give?")]
        public int ChillScp244AtStart { get; set; } = 1;

        [Description("Which role should every player spawn at the start during the \"Zerg Rush\" Special Round as?\nNote that this field uses role\'s ID. See the full list of role IDs below:\nhttps://steamcommunity.com/sharedfiles/filedetails/?id=2630379740")]
        public int ZergRushRoleId { get; set; } = 1;
        
        [Description("Which role should every player different from the \"chosen one\" spawn at the start during the \"One Man Army\" Special Round as?\nNote that this field uses role\'s ID.")]
        public int OneManArmyScpRoleId { get; set; } = 16;
        
        [Description("Which role should the \"chosen one\" spawn at the start during the \"One Man Army\" Special Round as?\nNote that this field uses role\'s ID.")]
        public int OneManArmyChosenOneRoleId { get; set; } = 15;
        
        [Description("Starting health of the \"chosen one\" during the \"One Man Army\" Special Round.")]
        public int OneManArmyChosenOneHealth { get; set; } = 10000;
        
        [Description("Starting health of the SCPs during the \"One Man Army\" Special Round.")]
        public int OneManArmyScpHealth { get; set; } = 1800;

        [Description("How many special items weapons can the Chosen One carry during the \"One Man Army\" Special Round?")]
        public sbyte OneManArmyChosenOneSpecialWeaponsLimit { get; set; } = 2;
        
        [Description("HP multiplier of the SCP-subjects for the \"Lights Out\" Special Round.")]
        public float LightsOutScpHealthMultiplier { get; set; } = 0.7f;

        [Description("Should the \"Late For Shift\" Special Round also give maximum amount of fitting ammo if it gives a weapon to a player?")]
        public bool LateForShiftShouldGiveAmmo { get; set; } = false;
        
        [Description("Custom display name for the \"Payday\" Special Round.")]
        public string PaydayName { get; set; } = "Payday";

        [Description("Custom display name for the \"Vitality Shift\" Special Round.")]
        public string VitalityShiftName { get; set; } = "Vitality Shift";

        [Description("Custom display name for the \"Sweet Tooth\" Special Round.")]
        public string SweetToothName { get; set; } = "Sweet Tooth";

        [Description("Custom display name for the \"Forest Gump\" Special Round.")]
        public string ForestGumpName { get; set; } = "Forest Gump";

        [Description("Custom display name for the \"Super Balling\" Special Round.")]
        public string SuperBallingName { get; set; } = "Super Balling";

        [Description("Custom display name for the \"Chill\" Special Round.")]
        public string ChillName { get; set; } = "Chill";

        [Description("Custom display name for the \"Zerg Rush\" Special Round.")]
        public string ZergRushName { get; set; } = "Zerg Rush";

        [Description("Custom display name for the \"Drug Testing\" Special Round.")]
        public string DrugTestingName { get; set; } = "Drug Testing";

        [Description("Custom display name for the \"One Man Army\" Special Round.")]
        public string OneManArmyName { get; set; } = "One Man Army";

        [Description("Custom display name for the \"Lights Out\" Special Round.")]
        public string LightsOutName { get; set; } = "Lights Out";
        
        [Description("Custom display name for the \"Late For Shift\" Special Round.")]
        public string LateForShiftName { get; set; } = "Late For Shift";
        
        [Description("Broadcast that is sent every sencond to all players during the voting.")]
        public string VotingProgressBroadcast { get; set; } = "Voting for Special Round:\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}\nTime left: {time_left} seconds.";

        [Description("Broadcast that is sent after a Special Round wins the voting.")]
        public string RoundWonVotingBroadcast { get; set; } =
            "Vote passed!\nSpecial Round enabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}";

        [Description("Broadcast that is sent after a Special Round loses the voting.")]
        public string RoundLostVotingBroadcast { get; set; } =
            "Vote failed!\nSpecial Round disabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}";

        [Description("Broadcast sent to a player when they successfully received their coins during the \"Payday\" Special Round.")]
        public string PaydayItemGivenBroadcast { get; set; } = "You suddenly feel a little bit richer...";

        [Description("Broadcast sent to a player when they couldn't receive their coins during the \"Payday\" Special Round.")]
        public string PaydayItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for your paycheck...";
        
        [Description("Broadcast sent to a player when they successfully received their candies during the \"Sweet Tooth\" Special Round.")]
        public string SweetToothItemGivenBroadcast { get; set; } = "Let chaos reign!";

        [Description("Broadcast sent to a player when they couldn't receive their candies during the \"Sweet Tooth\" Special Round.")]
        public string SweetToothItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for sweets...";
        
        [Description("Broadcast sent to a player when they successfully received their SCP-018s during the \"Super Balling\" Special Round.")]
        public string SuperBallingItemGivenBroadcast { get; set; } = "Time to play catch!";

        [Description("Broadcast sent to a player when they couldn't receive their SCP-018s during the \"Super Balling\" Special Round.")]
        public string SuperBallingItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for the ball...";
        
        [Description("Broadcast sent to a player when they successfully received their SCP-244s during the \"Chill\" Special Round.")]
        public string ChillItemGivenBroadcast { get; set; } = "Freeze 'em all!";

        [Description("Broadcast sent to a player when they couldn't receive their SCP-244s during the \"Chill\" Special Round.")]
        public string ChillItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for the chill ghost...";
        
        [Description("Broadcast sent to every player when the \"Vitality Shift\" Special Round has started.")]
        public string VitalityShiftBroadcast { get; set; } = "You feel {health_multiplier} times healthier!!";
        
        [Description("Broadcast sent to every player when the \"Forest Gump\" Special Round has started.")]
        public string ForestGumpBroadcast { get; set; } = "Run! Run {speed_intensity} times faster than ever before!";
        
        [Description("Broadcast sent to every player when the \"Zerg Rush\" Special Round has started.")]
        public string ZergRushBroadcast { get; set; } = "ZERG RUSH!!!";
        
        [Description("Broadcast sent to every player when the \"Drug Testing\" Special Round has started.")]
        public string DrugTestingBroadcast { get; set; } = "You got: {effect}!";
        
        [Description("Broadcast sent to the Chosen One when the \"One Man Army\" Special Round has started.")]
        public string OneManArmyChosenOneBroadcast { get; set; } = "<color=red><b>YOU'RE THE STAR OF THE SHOW NOW, BABY!</b></color>";

        [Description("Broadcast sent to the players when the \"One Man Army\" Special Round has started.")]
        public string OneManArmyScpBroadcast { get; set; } = "<color=red><b>BRING {chosen_name} DOWN!</b></color>";

        [Description("Broadcast sent to a player when they successfully received a flashlight OR already have a flashlight during the \"Lights Out\" Special Round.")]
        public string LightsOutFlashlightGivenBroadcast { get; set; } = "<i>Darkness constricts you...</i>";
        
        [Description("Broadcast sent to a player when they haven't received a flashlight when they should've during the \"Lights Out\" Special Round.")]
        public string LightsOutFlashlightNotGivenBroadcast { get; set; } = "<i>Darkness constricts you...</i>";

        [Description("Broadcast sent to a player when they are spawned during the \"Late For Shift\" Special Round.")]
        public string LateForShiftOnSpawnedBroadcast { get; set; } = "You probably shouldn't have overslept...";
    }
}