using System;
using System.Linq;
using CustomizableSpecialRounds.Features.SpecialRounds.Core;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers;
using Exiled.API.Features;

namespace CustomizableSpecialRounds
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "Customizable Special Rounds";
        
        public override string Author { get; } = "zaza";

        public override Version Version { get; } =  new Version(1, 1, 1);
        
        public static Plugin Instance { get; private set; }
        
        public SpecialRoundsManager SpecialRoundsManager { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;

            SpecialRoundsManager = new SpecialRoundsManager();
            
            Log.Debug("SpecialRoundsManager has been initialized.\n" +
                      $"Special Rounds found in plugin's assembly: {SpecialRoundsManager.SpecialRoundTypes.Count}\n" +
                      $"{Instance.SpecialRoundsManager.SpecialRoundTypes.Aggregate("", (current, specialRoundType) => current + (specialRoundType.ToString().Substring(specialRoundType.ToString().LastIndexOf(".", StringComparison.Ordinal) + 1) + "; "))}");

            Handlers.SubscribeEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            SpecialRoundsManager = null;
            
            Handlers.UnsubscribeEvents();
            
            Instance = null;
            
            base.OnDisabled();
        }
    }
}