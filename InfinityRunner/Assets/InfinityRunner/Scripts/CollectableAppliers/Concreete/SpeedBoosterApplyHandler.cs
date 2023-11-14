using System;
using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;
using InfinityRunner.Scripts.Level.Boosters.Model;
using InfinityRunner.Scripts.Level.Interfaces;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;

namespace InfinityRunner.Scripts.CollectableAppliers.Concreete
{
    /// <summary>
    /// A handler that applies a booster type "SpeedBoosterModel"
    /// </summary>
    
    public class SpeedBoosterApplyHandler : TriggerApplier<IBoosterCollectable>
    {
        public override bool WorksWith(IBoosterCollectable trigger)
        {
            return trigger is { Model: { Booster: SpeedBoosterModel } };
        }

        public override bool TryApply(IBoosterCollectable trigger, IPlayer player)
        {
            var model = (trigger.Model.Booster as SpeedBoosterModel);
            Apply(player, model.SpeedAffector, model.Duration);
            return true;
        }
        
        private async void Apply(IPlayer player, float multiplier, float duration)
        {
            player.Stats.SpeedMultipiers.Add(multiplier);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.UnscaledDeltaTime);

            var stats = player.Stats;
            
            // Костыль. Лень лезть в лайфсайкл, но отхэндлить то, что перс умер - можно запросто
            if (stats == null)
                return;
            
            player.Stats.SpeedMultipiers.Remove(multiplier);
        }
    }
}