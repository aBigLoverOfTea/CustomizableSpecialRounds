using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CustomizableSpecialRounds.Features.SpecialRounds.Core.SpecialRounds;
using Exiled.API.Extensions;
using Exiled.API.Features;
using JetBrains.Annotations;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core.Managers
{
    public class SpecialRoundsManager
    {
        public SpecialRound CurrentSpecialRound;

        [CanBeNull]
        public Type PreviousSpecialRoundType { get; private set; }

        public bool IsPaused;
        
        public bool FirstPlayerConnected;

        [CanBeNull]
        public readonly VotingManager VotingManager = Plugin.Instance.Config.IsVotingEnabled ? new VotingManager() : null;

        public readonly List<Type> SpecialRoundTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(SpecialRound)) && !t.IsAbstract)
            .ToList();
        
        public SpecialRound CreateSpecialRound(Type type)
        {
            var specialRound = (SpecialRound)Activator.CreateInstance(type);
            
            Log.Debug($"New Special Round created: {specialRound.GetType()}/{specialRound.Name}");

            return specialRound;
        }
        
        public SpecialRound GetRandomSpecialRound()
        {
            var specialRoundType = SpecialRoundTypes.GetRandomValue(type => type != PreviousSpecialRoundType);
            
            return CreateSpecialRound(specialRoundType);
        }

        public void Reset()
        {
            VotingManager?.Reset();

            PreviousSpecialRoundType = CurrentSpecialRound.GetType();
            
            CurrentSpecialRound.UnsubscribeEvents();
        
            FirstPlayerConnected = false;
            
            Log.Debug("Special Rounds Manager has been reset.");
        }
    }
}