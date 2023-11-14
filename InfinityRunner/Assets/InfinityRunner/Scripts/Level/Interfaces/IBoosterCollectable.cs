using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;

namespace InfinityRunner.Scripts.Level.Interfaces
{
    /// <summary>
    /// Interface, that implements all boosters on level
    /// </summary>
    public interface IBoosterCollectable : ILevelTrigger
    {
        BoosterSpawnModel Model { get; } 
    }
}