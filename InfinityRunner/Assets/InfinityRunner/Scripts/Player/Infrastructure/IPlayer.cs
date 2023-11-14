using System;
using InfinityRunner.Scripts.PlayerStates.StateMachine;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    /// <summary>
    /// Interface, that indicate that this object is player instance.
    /// Provide mutable Stats
    /// Provide interface to change state from outside
    /// </summary>
    public interface IPlayer : IDisposable
    {
        IObservable<IPlayer> OnDisposed { get; }
        PlayerStats Stats { get; }
        void SetState<T>() where T : IPlayerState;
    }
}