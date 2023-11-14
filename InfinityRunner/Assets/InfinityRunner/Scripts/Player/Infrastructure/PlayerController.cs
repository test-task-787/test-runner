using System;
using InfinityRunner.Scripts.Player.View;
using InfinityRunner.Scripts.Player.View.Interfaces;
using InfinityRunner.Scripts.PlayerStates;
using InfinityRunner.Scripts.PlayerStates.StateMachine;
using UniRx;
using UnityEngine;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private Transform viewRoot;
        
        private AnimationController _animationController;
        private Subject<PlayerController> _wasDisposed = new();
        private PlayerStats _playerStats;
        private PlayerStateMachine _stateMachine;
        
        public IObservable<IPlayer> OnDisposed => _wasDisposed;
        public PlayerStats Stats => _playerStats;

        /// <summary>
        /// Initialize method, that get all neded for "simulation" data. View is not critical for this step
        /// </summary>
        /// 
        public void Initialize(
            PlayerStateMachine stateMachine,
            PlayerStats playerStats,
            AnimationController animationController
            )
        {
            stateMachine.Put(new IdlePlayerState(stateMachine));
            _wasDisposed = new();
            _animationController = animationController;
            _playerStats = playerStats;
            _stateMachine = stateMachine;
            
            SetState<IdlePlayerState>();
            Observable.EveryUpdate().Subscribe(_ => stateMachine.Tick()).AddTo(this);
        }
        
        /// <summary>
        /// Link state is separated by initialization time.
        /// </summary>
        /// <param name="playerView"></param>
        public void Link(PlayerView playerView)
        {
            var t = playerView.transform;
            t.parent = viewRoot;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            playerView.Link(_animationController);
        }
        
        public void SetState<T>() where T : IPlayerState
        {
            _stateMachine.Enter<T>();
        }

        public void Dispose()
        {
            _wasDisposed.OnNext(this);
            Destroy(gameObject);
            _wasDisposed.Dispose();
        }
    }
}
