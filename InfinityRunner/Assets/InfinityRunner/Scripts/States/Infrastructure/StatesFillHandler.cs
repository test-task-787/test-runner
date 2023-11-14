using Zenject;

namespace InfinityRunner.Scripts.States.Infrastructure
{
    public class StatesFillHandler : IInitializable
    {
        private readonly RunnerApplicationStateMachine _stateMachine;
        private readonly StatesFactory _statesFactory;

        public StatesFillHandler(RunnerApplicationStateMachine stateMachine, StatesFactory statesFactory)
        {
            _stateMachine = stateMachine;
            _statesFactory = statesFactory;
        }
        
        public void Initialize()
        {
            _stateMachine.Put(_statesFactory.Create<DependenciesLoadState>());
            _stateMachine.Put(_statesFactory.Create<WaitStartState>());
            _stateMachine.Put(_statesFactory.Create<ResultState>());
            _stateMachine.Put(_statesFactory.Create<LoseState>());
            
            
            _stateMachine.Enter<DependenciesLoadState>();
        }
        
    }
}