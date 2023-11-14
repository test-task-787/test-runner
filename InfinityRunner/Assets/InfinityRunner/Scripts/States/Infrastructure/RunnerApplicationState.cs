using ThirdParty.StateMachine;
using ThirdParty.StateMachine.States;

namespace InfinityRunner.Scripts.States.Infrastructure
{
    public interface IRunnerApplicationState : IState
    {
        
    }
    public class RunnerApplicationStateMachine : StateMachine<IRunnerApplicationState>
    {
    }
}