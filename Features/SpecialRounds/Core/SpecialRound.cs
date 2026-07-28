using System;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.Enums;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core
{
    public class SpecialRound
    {
        public string Name { get; private set; }
        
        public SpecialRoundType Type { get; private set; }
        
        public SpecialRoundParameters Parameters { get; private set; }

        public static SpecialRound CreateSpecialRound(SpecialRoundType type)
        {
            var specialRound = new SpecialRound
            {
                Name = string.Empty,
                Type = type,
                Parameters = new SpecialRoundParameters()
            };

            switch (specialRound.Type) // it's robust, but gets the job done and additional abstraction isn't worth it for the size of this project
            {
                case SpecialRoundType.None:
                    Log.Warn("During creation of a Special Round, round type None has been passed.");
                    specialRound.Name = "None";
                    break;
                
                case SpecialRoundType.Payday:
                    specialRound.Name = Plugin.Instance.Config.PaydayName;
                    specialRound.Parameters.Set(SpecialRoundKeys.Payday.CoinsAtStart, Plugin.Instance.Config.PaydayCoinsAtStart);
                    break;
                
                case SpecialRoundType.VitalityShift:
                    specialRound.Name = Plugin.Instance.Config.VitalityShiftName;
                    specialRound.Parameters.Set(SpecialRoundKeys.VitalityShift.HumanRoleHealthMultiplier, Plugin.Instance.Config.VitalityShiftHumanRoleHealthMultiplier);
                    specialRound.Parameters.Set(SpecialRoundKeys.VitalityShift.ScpHealthMultiplier, Plugin.Instance.Config.VitalityShiftScpHealthMultiplier);
                    break;
                
                case SpecialRoundType.SweetTooth:
                    specialRound.Name = Plugin.Instance.Config.SweetToothName;
                    specialRound.Parameters.Set(SpecialRoundKeys.SweetTooth.PinkCandiesAtStart, Plugin.Instance.Config.SweetToothPinkCandiesAtStart);
                    break;
                
                case SpecialRoundType.ForestGump:
                    specialRound.Name = Plugin.Instance.Config.ForestGumpName;
                    specialRound.Parameters.Set(SpecialRoundKeys.ForestGump.SpeedEffectIntensity, Plugin.Instance.Config.ForestGumpSpeedEffectIntensity);
                    break;
                
                case SpecialRoundType.SuperBalling:
                    specialRound.Name = Plugin.Instance.Config.SuperBallingName;
                    specialRound.Parameters.Set(SpecialRoundKeys.SuperBalling.Scp018AtStart, Plugin.Instance.Config.SuperBallingScp018AtStart);
                    break;
                
                case SpecialRoundType.Chill:
                    specialRound.Name = Plugin.Instance.Config.ChillName;
                    specialRound.Parameters.Set(SpecialRoundKeys.Chill.Scp244AtStart, Plugin.Instance.Config.ChillScp244AtStart);
                    break;
                
                case SpecialRoundType.ZergRush:
                    specialRound.Name = Plugin.Instance.Config.ZergRushName;
                    specialRound.Parameters.Set(SpecialRoundKeys.ZergRush.RoleId, Plugin.Instance.Config.ZergRushRoleId);
                    break;
                
                case SpecialRoundType.DrugTesting:
                    specialRound.Name = Plugin.Instance.Config.DrugTestingName;
                    break;
                
                case SpecialRoundType.OneManArmy:
                    specialRound.Name = Plugin.Instance.Config.OneManArmyName;
                    specialRound.Parameters.Set(SpecialRoundKeys.OneManArmy.ChosenOneRoleId, Plugin.Instance.Config.OneManArmyChosenOneRoleId);
                    specialRound.Parameters.Set(SpecialRoundKeys.OneManArmy.ChosenOneHealth, Plugin.Instance.Config.OneManArmyChosenOneHealth);
                    specialRound.Parameters.Set(SpecialRoundKeys.OneManArmy.ScpRoleId, Plugin.Instance.Config.OneManArmyScpRoleId);
                    specialRound.Parameters.Set(SpecialRoundKeys.OneManArmy.ScpHealth, Plugin.Instance.Config.OneManArmyScpHealth);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            Log.Debug($"New Special Round created: {specialRound.Type}/{specialRound.Name}");
            
            return specialRound;
        }
    }
}