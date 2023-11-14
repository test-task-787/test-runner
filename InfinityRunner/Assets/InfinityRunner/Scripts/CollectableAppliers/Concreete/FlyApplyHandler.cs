using System;
using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;
using InfinityRunner.Scripts.Level.Boosters.Model;
using InfinityRunner.Scripts.Level.Interfaces;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.PlayerStates;
using InfinityRunner.Scripts.PlayerStates.FlyContext;

namespace InfinityRunner.Scripts.CollectableAppliers.Concreete
{
    /// <summary>
    /// A handler that applies a booster type "FlyBooster"
    /// </summary>
    
    public class FlyApplyHandler : TriggerApplier<IBoosterCollectable>
    {
        public override bool WorksWith(IBoosterCollectable trigger)
        {
            return trigger is { Model: { Booster: FlyBoosterModel } };
        }

        public override bool TryApply(IBoosterCollectable trigger, IPlayer player)
        {
            var model = (trigger.Model.Booster as FlyBoosterModel);
            Apply(player, model.Duration);
            return true;
        }
        
        private async void Apply(IPlayer player, float duration)
        {
            player.SetState<FlyPlayerState>();
            await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.UnscaledDeltaTime);
            player.SetState<RunPlayerState>();
        }
    }
}