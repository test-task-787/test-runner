using Cysharp.Threading.Tasks;
using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Player.View;
using InfinityRunner.Scripts.Player.View.Interfaces;
using InfinityRunner.Scripts.PlayerStates;
using InfinityRunner.Scripts.PlayerStates.FlyContext;
using InfinityRunner.Scripts.PlayerStates.RunContext.Configs;
using InfinityRunner.Scripts.PlayerStates.StateMachine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    
    /// <summary>
    /// Factory for Character and View.
    /// </summary>
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly InputFactory _inputFactory;
        private readonly PlayerDeathApplyHandler _playerDeathApplyHandler;
        private readonly IRunConfigProvider _runConfigProvider;

        public PlayerFactory(
            DiContainer container, 
            InputFactory inputFactory,
            PlayerDeathApplyHandler playerDeathApplyHandler,
            IRunConfigProvider runConfigProvider)
        {
            _container = container;
            _inputFactory = inputFactory;
            _playerDeathApplyHandler = playerDeathApplyHandler;
            _runConfigProvider = runConfigProvider;
        }
        
        // create async low-weight player instance 
        public async UniTask<PlayerController> CreatePlayer()
        {
            var handler = Addressables.InstantiateAsync("Player");
            await handler;
            var instance = handler.Result;
            
            var controller = instance.GetComponent<PlayerController>();
            _container.InjectGameObject(instance);
            
            // Create needed dependencies. Its better to control this "by hand" - not DI. Step-by-step in one place
            // Its just Rules for configure.
            var playerStats = new PlayerStats();
            var animationController = new AnimationController(playerStats);
            var playerState = new PlayerStateMachine();
            var inputModule = _inputFactory.Create();
            
            playerState.Put(new RunPlayerState(_runConfigProvider.Config, playerState, playerStats, instance, inputModule));
            playerState.Put(new FlyPlayerState(playerStats, instance, inputModule));
            playerState.Put(new DeadPlayerState(_playerDeathApplyHandler, playerStats));

            controller.Initialize(playerState, playerStats, animationController);
            return controller;
        }
        
        // Separated view spawn logic
        public async UniTask<PlayerView> CreateView(string key)
        {
            var handler = Addressables.InstantiateAsync(key);
            await handler;
            var view = handler.Result.GetComponent<PlayerView>();
            _container.InjectGameObject(view.gameObject);
            return view;
        }
        


    }
}