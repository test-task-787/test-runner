using ThirdParty.StateMachine;
using ThirdParty.StateMachine.States;

namespace InfinityRunner.Scripts.PlayerStates.StateMachine
{
    public class PlayerStateMachine : StateMachine<IPlayerState>, IStateMachineTickable<IPlayerState>
    {
        public void Tick()
        {
            if (ActualState.Value is IProcessableState processableState)
            {
                processableState.Process();
            }
        }
    }
}