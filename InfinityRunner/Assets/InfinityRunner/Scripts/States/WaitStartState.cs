using Cysharp.Threading.Tasks;
using Dreamteck.Forever;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.States.Infrastructure;
using Zenject;

namespace InfinityRunner.Scripts.States
{
    /// <summary>
    /// State, that contains step for prepare level for run
    /// </summary>
    public class WaitStartState : IRunnerApplicationState
    {
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private PlayersProvider _playersProvider;
        [Inject] private CoinSpawnService _coinSpawnService;
        [Inject] private LevelGenerator _levelGenerator;
        [Inject] private CameraPlayerTarget _playerTarget;
        
        public async void Enter()
        {
            using (TouchLocker.Lock())
            {
                _levelGenerator.StartGeneration();
                
                var playerInstance = await SpawnPlayer();
                _playerTarget.SetTarget(playerInstance.transform);
                
                //Separated view loading
                await PrepareView(playerInstance, "view1");
            }
            _coinSpawnService.Reset();
        }
        
        public void Exit()
        {
        }
        
        private async UniTask<PlayerController> SpawnPlayer()
        {
            var playerInstance = await _playerFactory.CreatePlayer();
            _playersProvider.AddPlayer(playerInstance);
            return playerInstance;
        }

        private async UniTask PrepareView(PlayerController playerController, string viewKey)
        {
            var view = await _playerFactory.CreateView(viewKey);
            playerController.Link(view);

        }
    }
}