using System;
using CustomizableSpecialRounds.Features.SpecialRounds;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers;
using Exiled.API.Features;

namespace CustomizableSpecialRounds
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "Customizable Special Rounds";
        
        public override string Author { get; } = "zaza";

        public override Version Version { get; } =  new Version(1, 0, 0);
        
        public static Plugin Instance { get; private set; }
        
        public SpecialRoundsManager SpecialRoundsManager { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;

            SpecialRoundsManager = new SpecialRoundsManager();

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