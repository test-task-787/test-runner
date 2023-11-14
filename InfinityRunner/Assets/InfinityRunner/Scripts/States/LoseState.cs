using InfinityRunner.Scripts.Infrastructure.ModuleRunner;
using InfinityRunner.Scripts.States.Infrastructure;

namespace InfinityRunner.Scripts.States
{
    public class LoseState : IRunnerApplicationState
    {
        private readonly RunnerState _runnerState;

        internal LoseState(RunnerState runnerState)
        {
            _runnerState = runnerState;
        }
        
        public void Enter()
        {
            _runnerState.StopState();
        }

        public void Exit()
        {
        }
    }
}