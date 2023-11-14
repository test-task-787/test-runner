using InfinityRunner.Scripts.Infrastructure.ModuleRunner;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    public class RunnerStateInstaller : MonoInstaller
    {
        [SerializeField] private GameModuleRunner gameModuleRunner;
        public override void InstallBindings()
        {
            Container.Bind<RunnerState>().AsSingle().WithArguments(gameModuleRunner);
        }
    }
}