using InfinityRunner.Scripts.Level.Interfaces;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;

namespace InfinityRunner.Scripts.CollectableAppliers.Interfaces
{
    public interface ITriggerApplier
    {
        bool WorksWith(ILevelTrigger trigger);
        bool TryApply(ILevelTrigger trigger, IPlayer playerController);
    }
}