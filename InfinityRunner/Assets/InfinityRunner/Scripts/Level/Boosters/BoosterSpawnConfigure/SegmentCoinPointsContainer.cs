using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace InfinityRunner.Scripts.Level.Boosters.BoosterSpawnConfigure
{
    /// <summary>
    /// Wrapper for Generator. Contains Any points for spawn boosters in needed points by needed strategy
    /// </summary>
    public class SegmentCoinPointsContainer : MonoBehaviour
    {
        private static readonly Subject<SegmentCoinPointsContainer> _wasSpawned = new();
        private List<BoosterSpawnPoint> _elements = new();
        private CancellationTokenSource cts = new();
        
        public static IObservable<SegmentCoinPointsContainer> OnSpawned => _wasSpawned;
        public IReadOnlyCollection<BoosterSpawnPoint> Elements => _elements;

        private void OnEnable()
        {
            NotifySpawned();
        }

        private void OnDisable()
        {
            cts.Cancel();
            cts = new();
        }

        /// <summary>
        /// After spawned\enabled MonoBehaviour notify by static event that positions is ready
        /// for spawn in them boosters.
        /// Delay == hack for DreamTeck lifecycle
        /// </summary>
        private async void NotifySpawned()
        {
            var token = cts.Token;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token).SuppressCancellationThrow();
            if (token.IsCancellationRequested)
                return;
            
            _elements = GetComponentsInChildren<BoosterSpawnPoint>().ToList();
            _elements.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
            _wasSpawned.OnNext(this);
        }
    }
}