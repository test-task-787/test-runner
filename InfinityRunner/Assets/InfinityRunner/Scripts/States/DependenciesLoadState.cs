using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Infrastructure.ModuleRunner;
using InfinityRunner.Scripts.States.Infrastructure;

namespace InfinityRunner.Scripts.States
{
    public class DependenciesLoadState : IRunnerApplicationState
    {
        private readonly RunnerState _runnerState;
        private readonly RunnerApplicationStateMachine _stateMachine;

        internal DependenciesLoadState(RunnerState runnerState, RunnerApplicationStateMachine stateMachine)
        {
            _runnerState = runnerState;
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            //Load generator

            await UniTask.DelayFrame(1);
            _stateMachine.Enter<WaitStartState>();
            //
        }

        public void Exit()
        {
            _runnerState.MarkLoaded();
        }
    }
}