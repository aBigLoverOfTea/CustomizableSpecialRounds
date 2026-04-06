using System;
using CustomizableSpecialRounds.Features.SpecialRounds;
using Exiled.API.Features;
using SpecialRounds;

namespace CustomizableSpecialRounds
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "Customizable Special Rounds";
        
        public override string Author { get; } = "zaza";

        public override Version Version { get; } =  new Version(0, 9, 1);
        
        public static Plugin Instance { get; private set; }
        
        public SpecialRoundsManager SpecialRoundsManager { get; set; }

        public override void OnEnabled()
        {
            Log.Info($"Enabling Customizable Special Rounds v.{Version}...");

            Instance = this;

            SpecialRoundsManager = new SpecialRoundsManager();

            Handlers.SubscribeEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("Customizable Special Rounds disabled!");
            
            SpecialRoundsManager = null;
            
            Handlers.UnsubscribeEvents();
            
            Instance = null;
            
            base.OnDisabled();
        }
    }
}