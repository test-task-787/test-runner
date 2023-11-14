using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Level.DeathHandlers;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.PlayerStates.RunContext;
using InfinityRunner.Scripts.PlayerStates.RunContext.Configs;
using InfinityRunner.Scripts.PlayerStates.StateMachine;
using ThirdParty.StateMachine.States;
using UniRx;
using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates
{
    public class RunPlayerState : IPlayerState, IProcessableState
    {
        private readonly RunConfig _runConfig;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerStats _playerStats;
        private readonly InputModule _inputModule;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly Bounds _colliderBounds;
        private PlayerDeadZoneHandler _deadHandlerByHeight;
        private PlayerDeadColliderHandler _deadHandlerByPlace;
        
        private readonly CompositeDisposable _disposable = new();

        public RunPlayerState(
            RunConfig runConfig,
            PlayerStateMachine playerStateMachine,
            PlayerStats playerStats,
            GameObject item, 
            InputModule inputModule)
        {
            _runConfig = runConfig;
            _playerStateMachine = playerStateMachine;
            _playerStats = playerStats;
            _inputModule = inputModule;
            _rigidbody = item.GetComponent<Rigidbody2D>();
            _transform = item.transform;
            
            
            _colliderBounds = item.GetComponent<CapsuleCollider2D>().bounds;
            _colliderBounds.center = _transform.InverseTransformPoint(_colliderBounds.center);
            _colliderBounds.size = _transform.InverseTransformVector(_colliderBounds.size);
        }
        
        public void Enter()
        {
            _disposable.Clear();

            _rigidbody.simulated = true;
            var controller = new RunController(_runConfig, _playerStats, _rigidbody, _transform, _colliderBounds,
                _inputModule);
            
            _deadHandlerByHeight = new PlayerDeadZoneHandler(_transform);
            _deadHandlerByPlace = new PlayerDeadColliderHandler(_transform, _colliderBounds).AddTo(_disposable);

            Observable.EveryFixedUpdate().Subscribe((_) => controller.OnPhysicsTick(Time.fixedDeltaTime))
                .AddTo(_disposable);
            Observable.EveryUpdate().Subscribe((_) => controller.OnTick(Time.fixedDeltaTime))
                .AddTo(_disposable);
        }

        public void Exit()
        {
            _rigidbody.simulated = false;
            _disposable.Clear();
        }

        public void Process()
        {
            if (_deadHandlerByHeight.IsInDeadZone || _deadHandlerByPlace.IsDead)
            {
                _playerStateMachine.Enter<DeadPlayerState>();
            }
        }
    }
}