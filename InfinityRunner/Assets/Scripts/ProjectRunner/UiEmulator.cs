using Cysharp.Threading.Tasks;
using Loaders;
using SharedModules;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProjectRunner
{
    public class UiEmulator : MonoBehaviour
    {
        private const string TargetScene = "RunnerScene";

        [Inject] private LoadingController _loadingController;
    
        public async void LaunchModule()
        {
            await _loadingController.Show(false);
            var handler = SceneManager.LoadSceneAsync(TargetScene);
            handler.allowSceneActivation = true;
        
            await handler;

            var scene = SceneManager.GetSceneByName(TargetScene);
            RunModule(scene);
        }
    
        private void RunModule(Scene scene)
        {
            var moduleRunner = FindModuleRunner(scene);
            moduleRunner?.Run();

            if (moduleRunner != null)
            {
                moduleRunner.IsLaunched.Where(x => x).Take(1).Subscribe(x =>
                {
                    _loadingController.Hide(false).Forget();
                });
            }
            else
            {
                Debug.LogError("No runner handler");
                _loadingController.Hide(false).Forget();
            }
        }

        private IModuleRunner FindModuleRunner(Scene scene)
        {
            foreach (var go in scene.GetRootGameObjects())
            {
                var runner = go.GetComponent<IModuleRunner>();
                if (runner != null)
                {
                    return runner;
                }
            }

            return null;
        }
    }
}