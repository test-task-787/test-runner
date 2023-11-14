using Dreamteck.Forever;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.States.Infrastructure;

namespace InfinityRunner.Scripts.States
{
    public class ResultState : IRunnerApplicationState
    {
        private readonly RunnerApplicationStateMachine _applicationStateMachine;
        private readonly PlayersProvider _playersProvider;
        private readonly CoinSpawnService _coinSpawnService;

        public ResultState(
            RunnerApplicationStateMachine applicationStateMachine, 
            PlayersProvider playersProvider,
            CoinSpawnService coinSpawnService)
        {
            _applicationStateMachine = applicationStateMachine;
            _playersProvider = playersProvider;
            _coinSpawnService = coinSpawnService;
        }
        
        public void Enter()
        {
            EndScreen.Open();
            EndScreen.onRestartClicked += EndScreenOnonRestartClicked;
        }
        
        public void Exit()
        {
            EndScreen.onRestartClicked -= EndScreenOnonRestartClicked;
        }

        private void EndScreenOnonRestartClicked()
        {
            PrepareEnvironmentForRestart();
            _applicationStateMachine.Enter<WaitStartState>();
        }

        private void PrepareEnvironmentForRestart()
        {
            foreach (var playersProviderPlayer in _playersProvider.Players)
                playersProviderPlayer.Dispose();
            _coinSpawnService.Reset();
            LevelGenerator.instance.Restart();
        }
    }
}