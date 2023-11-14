using Cysharp.Threading.Tasks;
using SharedModules;

namespace InfinityRunner.Scripts.Infrastructure.ModuleRunner
{
    /// <summary>
    /// Infrastructure logic. Just for MODULE architecture 
    /// </summary>
    internal class RunnerState
    {
        private readonly GameModuleRunner _gameModuleRunner;

        public RunnerState(GameModuleRunner gameModuleRunner)
        {
            _gameModuleRunner = gameModuleRunner;
        }
        
        public void MarkLoaded()
        {
            _gameModuleRunner.MarkLaunched();
        }

        public void StopState()
        {
        }
        
        public UniTask<ModuleState> GetResultState()
        {
            var a = new UniTaskCompletionSource<ModuleState>();
            return a.Task;
        }
    }
}