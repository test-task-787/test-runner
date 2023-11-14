using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.PlayerStates.StateMachine;
using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates
{
    public class DeadPlayerState : IPlayerState
    {
        private readonly PlayerDeathApplyHandler _playerDeathApplyHandler;
        private readonly PlayerStats _playerStats;

        public DeadPlayerState(PlayerDeathApplyHandler playerDeathApplyHandler, PlayerStats playerStats)
        {
            _playerDeathApplyHandler = playerDeathApplyHandler;
            _playerStats = playerStats;
        }
        
        public void Enter()
        {
            _playerDeathApplyHandler.Dead();
            _playerStats.IsDead = true;
            _playerStats.ActualSpeed = Vector2.zero;
        }

        public void Exit()
        {
        }
    }
}