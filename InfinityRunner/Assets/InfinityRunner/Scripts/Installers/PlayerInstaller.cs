using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Player.Infrastructure;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CameraPlayerTarget cameraPlayerTarget;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputFactory>().AsSingle();
            
            Container.Bind<CameraPlayerTarget>().FromInstance(cameraPlayerTarget);
            Container.BindInterfacesAndSelfTo<PlayersProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerDeathApplyHandler>().AsSingle();
        }
    }
}