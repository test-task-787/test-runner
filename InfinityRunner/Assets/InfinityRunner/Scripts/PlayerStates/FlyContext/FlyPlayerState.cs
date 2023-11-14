using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.PlayerStates.StateMachine;
using UniRx;
using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates.FlyContext
{
    public class FlyPlayerState : IPlayerState
    {
        private CompositeDisposable _disposable = new();
        private readonly PlayerStats _playerStats;
        private readonly InputModule _inputModule;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly Bounds _colliderBounds;

        public FlyPlayerState(PlayerStats playerStats,
            GameObject item, 
            InputModule inputModule)
        {
            _playerStats = playerStats;
            _inputModule = inputModule;
            _rigidbody = item.GetComponent<Rigidbody2D>();
            _transform = item.transform;
        }
        
        public void Enter()
        {
            _rigidbody.simulated = true;
            var controller = new FlyController(_playerStats, _rigidbody, _transform, _inputModule);
            
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
    }
}