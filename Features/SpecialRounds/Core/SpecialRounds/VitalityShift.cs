using CustomizableSpecialRounds.Features.SpecialRounds.Core.Utility;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds
{
    public class VitalityShift : SpecialRound
    {
        public override string Name { get; protected set; } = Plugin.Instance.Config.VitalityShiftName;
        
        public VitalityShift()
        {
            Parameters.Set(SpecialRoundKeys.VitalityShift.HumanRoleHealthMultiplier, Plugin.Instance.Config.VitalityShiftHumanRoleHealthMultiplier);
            Parameters.Set(SpecialRoundKeys.VitalityShift.ScpHealthMultiplier, Plugin.Instance.Config.VitalityShiftScpHealthMultiplier);
        }

        protected override void OnSpawned(SpawnedEventArgs ev)
        {
            if (Plugin.Instance.SpecialRoundsManager.IsPaused) return;

            if (!Plugin.Instance.Config.ShouldAffectForceSpawnedPlayers && ev.Reason == SpawnReason.ForceClass) return;
            
            var multiplier = ev.Player.IsScp
                ? Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<float>(SpecialRoundKeys.VitalityShift.ScpHealthMultiplier)
                : Plugin.Instance.SpecialRoundsManager.CurrentSpecialRound.Parameters.Get<float>(SpecialRoundKeys.VitalityShift.HumanRoleHealthMultiplier);
                    
            ev.Player.MaxHealth *= multiplier;
            ev.Player.Health *= multiplier;
                    
            ev.Player.Broadcast(5, BroadcastFormatter.GetFormatedVitalityShiftBroadcast(multiplier.ToString()), shouldClearPrevious:true);
        }
        
        public override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
        }

        public override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
        }
    }
}