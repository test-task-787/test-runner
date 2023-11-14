using Cysharp.Threading.Tasks;
using SharedModules;
using UniRx;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Infrastructure.ModuleRunner
{
    /// <summary>
    /// Infrastructure logic. Just for MODULE architecture 
    /// </summary>
    /// 
    internal class GameModuleRunner : MonoBehaviour, IModuleRunner
    {
        [SerializeField] private SceneContext sceneContext;
        [Inject] private RunnerState runnerState;

        private readonly ReactiveProperty<bool> _isLaunched = new();
        
        public IReadOnlyReactiveProperty<bool> IsLaunched => _isLaunched;

        public UniTask<ModuleState> Run()
        {
            sceneContext.Run();
            return runnerState.GetResultState();
        }

        public void MarkLaunched()
        {
            _isLaunched.Value = true;
        }
    }
}
