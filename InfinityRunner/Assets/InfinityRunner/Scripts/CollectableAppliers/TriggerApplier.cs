using InfinityRunner.Scripts.CollectableAppliers.Interfaces;
using InfinityRunner.Scripts.Level.Interfaces;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;

namespace InfinityRunner.Scripts.CollectableAppliers
{
    /// <summary>
    /// Infrastructure hack: all triggers implement a general interface but work with specifics inside.
    /// </summary>
    /// <typeparam name="T">ILevelTrigger</typeparam>
    public abstract class TriggerApplier<T> : ITriggerApplier where T : ILevelTrigger
    {
        bool ITriggerApplier.WorksWith(ILevelTrigger trigger) => WorksWith((T)trigger);

        bool ITriggerApplier.TryApply(ILevelTrigger trigger, IPlayer playerController) =>
            TryApply((T)trigger, playerController);

        public abstract bool WorksWith(T trigger);
        public abstract bool TryApply(T trigger, IPlayer playerController);
    }
}