using System;
using System.Collections.Generic;
using UniRx;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    //Service, that contains ALL players in current time. 
    public class PlayersProvider : IDisposable
    {
        private List<IPlayer> _players = new();
        private CompositeDisposable _disposable = new();
        public IReadOnlyList<IPlayer> Players => new List<IPlayer>(_players);
        
        public void AddPlayer(PlayerController playerController)
        {
            playerController.OnDisposed.Subscribe(x =>
            {
                _players.Remove(x);
            }).AddTo(_disposable);
            _players.Add(playerController);
        }

        public void Dispose()
        {
            _disposable.Clear();
        }
    }
}