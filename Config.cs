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
        
        [Description("Should the players who are force spawned (either via RA, commands or via other plugins) be affected by the Special Round on-spawn effects?")]
        public bool ShouldAffectForceSpawnedPlayers { get; set; } = true;

        [Description("Should the players be able to vote for special round start before the round starts?")]
        public bool IsVotingEnabled { get; set; } = true;

        [Description("How long the voting stage should last (in seconds)?")]
        public int VotingDuration { get; set; } = 20;

        [Description("How many coins should the \"Payday\" special round give?")]
        public int PaydayCoinsAtStart { get; set; } = 1;
        
        [Description("HP multiplier of human roles for the \"Vitality Shift\" special round.")]
        public float VitalityShiftHumanRoleHealthMultiplier { get; set; } = 2.0f;
        
        [Description("HP multiplier of the SCP-subjects for the \"Vitality Shift\" special round.")]
        public float VitalityShiftScpHealthMultiplier { get; set; } = 2.0f;
        
        [Description("How many pink candies should the \"Sweet Boom-Tooth\" special round give.")]
        public int SweetToothPinkCandiesAtStart { get; set; } = 1;
        
        [Description("Power of the speed effect during the \"Forest Gump\" special round.")]
        public byte ForestGumpSpeedEffectIntensity { get; set; } = 1;
        
        [Description("How many SCP-018's should the \"Super Balling\" special round give?")]
        public int SuperBallingScp018AtStart { get; set; } = 1;
        
        [Description("How many SCP-244's should the \"Chill\" special round give?")]
        public int ChillScp244AtStart { get; set; } = 1;

        [Description("Which role should every player spawn at the start during the \"Zerg Rush\" special round as?\nNote that this field uses role\'s ID. See the full list of role IDs below:\nhttps://steamcommunity.com/sharedfiles/filedetails/?id=2630379740")]
        public int ZergRushRoleId { get; set; } = 1;
        
        [Description("Which role should every player different from the \"chosen one\" spawn at the start during the \"One Man Army\" special round as?\nNote that this field uses role\'s ID.")]
        public int OneManArmyScpRoleId { get; set; } = 16;
        
        [Description("Which role should the \"chosen one\" spawn at the start during the \"One Man Army\" special round as?\nNote that this field uses role\'s ID.")]
        public int OneManArmyChosenOneRoleId { get; set; } = 15;
        
        [Description("Starting health of the \"chosen one\" during the \"One Man Army\" special round.")]
        public int OneManArmyChosenOneHealth { get; set; } = 10000;
        
        [Description("Starting health of the SCPs during the \"One Man Army\" special round.")]
        public int OneManArmyScpHealth { get; set; } = 1800;
        
        [Description("Time in seconds after which the invisibility effect will be reapplied during the \"Phantoms\" and \"Drug Testing\" special round.")]
        public byte PhantomsInvisibilityRestorationTime { get; set; } = 2;
        
        [Description("Additional health that will be added to every SCP during the \"Phantoms\" special round.")]
        public int PhantomsScpHealthBonus { get; set; } = 400;
        
        [Description("Custom display name for the \"Payday\" special round.")]
        public string PaydayName { get; set; } = "Payday";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string VitalityShiftName { get; set; } = "Vitality Shift";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string SweetToothName { get; set; } = "Sweet Tooth";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string ForestGumpName { get; set; } = "Forest Gump";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string SuperBallingName { get; set; } = "Super Balling";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string ChillName { get; set; } = "Chill";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string ZergRushName { get; set; } = "Zerg Rush";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string DrugTestingName { get; set; } = "Drug Testing";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string OneManArmyName { get; set; } = "One Man Army";

        [Description("Custom display name for the \"Payday\" special round.")]
        public string PhantomsName { get; set; } = "Phantoms";
        
        [Description("Broadcast that is sent every sencond to all players during the voting.")]
        public string VotingProgressBroadcast { get; set; } = "Voting for special round:\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}\nTime left: {time_left} seconds.";

        [Description("Broadcast that is sent after a Special Round wins the voting.")]
        public string RoundWonVotingBroadcast { get; set; } =
            "Vote passed!\nSpecial Round enabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}";

        [Description("Broadcast that is sent after a Special Round loses the voting.")]
        public string RoundLostVotingBroadcast { get; set; } =
            "Vote failed!\nSpecial Round disabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}";

        [Description("Broadcast sent to a player when they successfully received their coins during the \"Payday\" special round.")]
        public string PaydayItemGivenBroadcast { get; set; } = "You suddenly feel a little bit richer...";

        [Description("Broadcast sent to a player when they couldn't receive their coins during the \"Payday\" special round.")]
        public string PaydayItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for your paycheck...";
        
        [Description("Broadcast sent to a player when they successfully received their candies during the \"Sweet Tooth\" special round.")]
        public string SweetToothItemGivenBroadcast { get; set; } = "Let chaos reign!";

        [Description("Broadcast sent to a player when they couldn't receive their candies during the \"Sweet Tooth\" special round.")]
        public string SweetToothItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for sweets...";
        
        [Description("Broadcast sent to a player when they successfully received their SCP-018s during the \"Super Balling\" special round.")]
        public string SuperBallingItemGivenBroadcast { get; set; } = "Time to play catch!";

        [Description("Broadcast sent to a player when they couldn't receive their SCP-018s during the \"Super Balling\" special round.")]
        public string SuperBallingItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for the ball...";
        
        [Description("Broadcast sent to a player when they successfully received their SCP-244s during the \"Chill\" special round.")]
        public string ChillItemGivenBroadcast { get; set; } = "Freeze 'em all!";

        [Description("Broadcast sent to a player when they couldn't receive their SCP-244s during the \"Chill\" special round.")]
        public string ChillItemNotGivenBroadcast { get; set; } = "Seems like you don't have enough place for the chill ghost...";
        
        [Description("Broadcast sent to every player when the \"Vitality Shift\" special round has started.")]
        public string VitalityShiftBroadcast { get; set; } = "You feel {health_multiplier} times healthier!!";
        
        [Description("Broadcast sent to every player when the \"Forest Gump\" special round has started.")]
        public string ForestGumpBroadcast { get; set; } = "Run! Run {speed_intensity} times faster than ever before!";
        
        [Description("Broadcast sent to every player when the \"Zerg Rush\" special round has started.")]
        public string ZergRushBroadcast { get; set; } = "ZERG RUSH!!!";
        
        [Description("Broadcast sent to every player when the \"Phantoms\" special round has started.")]
        public string PhantomsBroadcast { get; set; } = "You feel yourself fading away...";
        
        [Description("Broadcast sent to every player when the \"Drug Testing\" special round has started.")]
        public string DrugTestingBroadcast { get; set; } = "You got: {effect}!";
        
        [Description("Broadcast sent to the Chosen One when the \"One Man Army\" special round has started.")]
        public string OneManArmyChosenOneBroadcast { get; set; } = "<color=red><b>YOU'RE THE STAR OF THE SHOW NOW, BABY!</b></color>";

        [Description("Broadcast sent to the players when the \"One Man Army\" special round has started.")]
        public string OneManArmyScpBroadcast { get; set; } = "<color=red><b>BRING {chosen_name} DOWN!</b></color>";
    }
}