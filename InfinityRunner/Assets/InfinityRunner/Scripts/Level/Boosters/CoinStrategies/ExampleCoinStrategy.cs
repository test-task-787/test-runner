using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Level.Boosters.BoosterSpawnConfigure;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies.Base;
using InfinityRunner.Scripts.Level.Boosters.Model;
using UniRx;
using UnityEngine.AddressableAssets;

namespace InfinityRunner.Scripts.Level.Boosters.CoinStrategies
{
    
    /// <summary>
    /// Example Coins spawn strategy
    /// </summary>
    public class ExampleCoinStrategy : BoostersSpawnStrategy, IDisposable
    {
        private CompositeDisposable _disposable = new();
        private int internalCounter = 0;
        private List<IDisposable> views = new();
        private int spawned = 0;
        private bool isDisposed = false;
        
        public ExampleCoinStrategy()
        {
            SegmentCoinPointsContainer.OnSpawned.Subscribe(OnContainerReady).AddTo(_disposable);
        }
        
        public override void Dispose()
        {
            _disposable.Clear();
            foreach (var view in views)
            {
                view.Dispose();
            }
            views.Clear();
            isDisposed = true;
        }
        
        private void OnContainerReady(SegmentCoinPointsContainer segmentCoinPointsContainer)
        {
            foreach (var element in segmentCoinPointsContainer.Elements)
            {
                var model = ProcessSpawn();
                Spawn(model, element);
            }
        }
        
        /// <summary>
        /// Create monobehaviour wrapper for booster. Then Send action to fill booster view
        /// </summary>
        /// <param name="model"></param>
        /// <param name="point"></param>
        private async void Spawn(BoosterSpawnModel model, BoosterSpawnPoint point)
        {
            if (model == null)
                return;

            var handlerLoader = Addressables.InstantiateAsync("booster-controller");
            await handlerLoader;
            if (isDisposed)
            {
                handlerLoader.Result.GetComponent<BoosterPresenter>().Dispose();
                return;
            }
            var controller = handlerLoader.Result.GetComponent<BoosterPresenter>();
            controller.gameObject.name += $"_{spawned}";
            views.Add(controller);
            controller.transform.position = point.transform.position;
            controller.Initialize(model);
            spawned++;
        }
        
        /// <summary>
        /// Spawn sequence: empty|yellow|red|fly_booster
        /// </summary>
        /// <returns></returns>
        private BoosterSpawnModel ProcessSpawn()
        {
            internalCounter++;
            return (internalCounter % 4) switch
            {
                0 => null,
                1 => new BoosterSpawnModel()
                {
                    ViewKey = "yellow_coin",
                    Booster = new SpeedBoosterModel() { Duration = 5f, SpeedAffector = 0.8f }
                },
                2 => new BoosterSpawnModel()
                {
                    ViewKey = "red_coin",
                    Booster = new SpeedBoosterModel() { Duration = 5f, SpeedAffector = 1.5f }
                },
                3 => new BoosterSpawnModel()
                {
                    ViewKey = "fly_booster",
                    Booster = new FlyBoosterModel() { Duration = 10f } 
                },
                _ => null
            };
        }
    }
}