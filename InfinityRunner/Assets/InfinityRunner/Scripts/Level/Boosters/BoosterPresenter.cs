using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;
using InfinityRunner.Scripts.Level.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace InfinityRunner.Scripts.Level.Boosters
{
    /// <summary>
    /// MonoBehaviour presenter for Boosters. 
    /// Provide BoosterModel for Trigger Collider 
    /// </summary>
    public class BoosterPresenter : MonoBehaviour, IDisposable, IBoosterCollectable
    {
        private readonly CompositeDisposable _disposable = new();
        private CancellationTokenSource _cts = new();
        
        public BoosterSpawnModel Model { get; private set; }
        
        public void Initialize(BoosterSpawnModel boosterSpawnModel)
        {
            _cts = new();
            _disposable.Clear();
            Model = boosterSpawnModel;
            SpawnView(_cts.Token);
        }

        public void Dispose()
        {
            _cts.Cancel();
            _disposable.Clear();
            Destroy(gameObject);
        }

        private async void SpawnView(CancellationToken cancellationToken)
        {
            var handler = Addressables.InstantiateAsync(Model.ViewKey, transform);
            await handler;
            if (cancellationToken.IsCancellationRequested)
            {
                Destroy(handler.Result.gameObject);
            }
        }
    }
}