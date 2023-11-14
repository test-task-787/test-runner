using System;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.Player.View.Interfaces;
using UniRx;

namespace InfinityRunner.Scripts.Player.View
{
    // This is an animation wrapper (non-monobehaviour), that used to link logic layer with view layer
    // Idea: Player model was spawned and already played, but view is not loaded - when view was loaded it
    // immediatelly get needed state
    // ALSO
    // for hot-change in runtime view is good practice
    public class AnimationController : IReadPlayerAnimationController, IDisposable
    {
        private readonly PlayerStats _playerStats;
        private readonly CompositeDisposable _disposable;
        public ReactiveProperty<bool> IsDead { get; } = new();
        public ReactiveProperty<int> StateId { get; } = new();
        public ReactiveProperty<float> MovementSpeed { get; } = new();
        
        
        IReadOnlyReactiveProperty<bool> IReadPlayerAnimationController.IsDead => IsDead ;
        IReadOnlyReactiveProperty<int> IReadPlayerAnimationController.StateId => StateId ;
        IReadOnlyReactiveProperty<float> IReadPlayerAnimationController.MovementSpeed => MovementSpeed;

        public AnimationController(PlayerStats playerStats)
        {
            _disposable = new CompositeDisposable();
            _playerStats = playerStats;
            Observable.EveryUpdate().Subscribe(x =>
            {
                MovementSpeed.Value = playerStats.ActualSpeed.x;
                IsDead.Value = playerStats.IsDead;
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Clear();
        }
    }
}