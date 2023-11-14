using System;
using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.PlayerStates.StateMachine;

namespace InfinityRunner.Scripts.PlayerStates
{
    public class IdlePlayerState : IPlayerState
    {
        private readonly PlayerStateMachine _stateMachine;

        public IdlePlayerState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            // Just example logic. After 2s enter to state RUN. Its bad practice.
            // Best practice to create UI and UI controller MUST send event
            // For start game and then Change PlayerState from IDLE to RUN.
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _stateMachine.Enter<RunPlayerState>();
        }

        public void Exit()
        {
        }
    }
}