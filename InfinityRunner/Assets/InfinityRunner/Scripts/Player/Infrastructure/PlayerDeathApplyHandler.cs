using InfinityRunner.Scripts.States;
using InfinityRunner.Scripts.States.Infrastructure;
using Zenject;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    /// <summary>
    /// Handler, that change application state when player is dead;
    /// </summary>
    public class PlayerDeathApplyHandler : IInitializable
    {
        [Inject] private RunnerApplicationStateMachine _stateMachine;
        
        public void Initialize()
        {
        }

        public void Dead()
        {
            _stateMachine.Enter<ResultState>();
        }
    }
}