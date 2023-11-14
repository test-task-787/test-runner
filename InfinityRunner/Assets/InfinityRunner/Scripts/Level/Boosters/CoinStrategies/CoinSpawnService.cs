using System;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;
using Zenject;

namespace InfinityRunner.Scripts.Level.Boosters.CoinStrategies
{
    /// <summary>
    /// Service, that create needed SpawnStrategy.
    /// It`s bed practice to create strategy inside installers, because u can't change them "on hot" if it control DI
    /// </summary>
    public class CoinSpawnService : IInitializable, IDisposable
    {
        private BoostersSpawnStrategy currentStrategy;
        
        public void Initialize()
        {
            Reset();
        }

        public void Reset()
        {
            currentStrategy?.Dispose();
            currentStrategy = new ExampleCoinStrategy();
        }

        public void Dispose()
        {
            currentStrategy.Dispose();
        }
    }
}