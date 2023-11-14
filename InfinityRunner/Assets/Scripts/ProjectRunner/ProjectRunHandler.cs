using Loaders;
using UnityEngine;
using Zenject;

namespace ProjectRunner
{
    public class ProjectRunHandler : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        [SerializeField] private LoadingController loadingController;
        [SerializeField] private UiEmulator uiEmulator;
    
        private void Start()
        {
            sceneContext.Run();
            loadingController.gameObject.SetActive(false);
            uiEmulator.LaunchModule();
        }
    }
}
