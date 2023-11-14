using System;

namespace InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base
{
    /// <summary>
    /// Strategy pattern for spawn boosters
    /// </summary>
    public abstract class BoostersSpawnStrategy : IDisposable
    {
        public abstract void Dispose();
    }
}
