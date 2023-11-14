using SharedModules;
using UnityEngine;
using Zenject;

namespace ProjectRunner
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera gameCamera;
    
        public override void InstallBindings()
        {
            Container.Bind<Camera>().WithId(CameraId.UI).FromInstance(uiCamera);
            Container.Bind<Camera>().WithId(CameraId.Game).FromInstance(gameCamera);
        }
    }
}
