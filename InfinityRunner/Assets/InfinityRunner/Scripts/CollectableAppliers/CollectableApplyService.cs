using System.Collections.Generic;
using InfinityRunner.Scripts.CollectableAppliers.Concreete;
using InfinityRunner.Scripts.CollectableAppliers.Interfaces;
using InfinityRunner.Scripts.Level.Interfaces;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;

namespace InfinityRunner.CollectableAppliers
{
    /// <summary>
    /// A service that applies a specific booster to a specific character
    /// </summary>
    public class CollectableApplyService
    {
        private readonly List<ITriggerApplier> _handlers = new();

        public CollectableApplyService()
        {
            Put(new FlyApplyHandler());    
            Put(new SpeedBoosterApplyHandler());    
        }
        
        /// <summary>
        /// Add `ITriggerApplier` outside the service
        /// </summary>
        /// <param name="applier">handler</param>
        public void Put(ITriggerApplier applier)
        {
            _handlers.Add(applier);
        }

        public void ApplyTrigger(ILevelTrigger levelTrigger, IPlayer player)
        {
            foreach (var handler in _handlers)
            {
                if (handler.WorksWith(levelTrigger))
                {
                    if (handler.TryApply(levelTrigger, player))
                    {
                        return;
                    }
                }
            }
        }

    }
}