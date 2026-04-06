using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SpecialRounds
{
    public class Config : IConfig
    {
        [Description("Should plugin be loaded on next server restart?")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Should debug mode be enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Should the players be able to vote for special round start before the round starts?")]
        public bool VotingIsAllowed { get; set; } = true;

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
        public byte ForestGumoSpeedEffectIntensity { get; set; } = 1;
        
        [Description("How many SCP-018's should the \"Super Balling\" special round give?")]
        public int SuperBallingScp018AtStart { get; set; } = 1;
        
        [Description("How many SCP-244's should the \"Chill\" special round give?")]
        public int ChillScp244AtStart { get; set; } = 1;

        [Description("Which role should every player spawn at the start during the \"Zerg Rush\" special round as?\nNote that this field uses role\'s ID. See the full list of role IDs below:\nhttps://steamcommunity.com/sharedfiles/filedetails/?id=2630379740")]
        public byte ZergRushRoleId { get; set; } = 1;
        
        [Description("Which role should every player different from the \"chosen one\" spawn at the start during the \"One Man Army\" special round as?\nNote that this field uses role\'s ID.")]
        public byte OneManArmyScpRoleId { get; set; } = 16;
        
        [Description("Which role should the \"chosen one\" spawn at the start during the \"One Man Army\" special round as?\nNote that this field uses role\'s ID.")]
        public byte OneManArmyChosenOneRoleId { get; set; } = 15;
        
        [Description("Starting health of the \"chosen one\" during the \"One Man Army\" special round.")]
        public int OneManArmyChosenOneHealth { get; set; } = 10000;
        
        [Description("Starting health of the SCPs during the \"One Man Army\" special round.")]
        public int OneManArmyScpHealth { get; set; } = 1800;
    }
}