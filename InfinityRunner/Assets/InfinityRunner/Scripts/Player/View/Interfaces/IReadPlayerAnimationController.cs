using UniRx;

namespace InfinityRunner.Scripts.Player.View.Interfaces
{
    public interface IReadPlayerAnimationController
    {
        IReadOnlyReactiveProperty<bool> IsDead { get; }
        IReadOnlyReactiveProperty<int> StateId { get; }
        IReadOnlyReactiveProperty<float> MovementSpeed { get; }
    }
}