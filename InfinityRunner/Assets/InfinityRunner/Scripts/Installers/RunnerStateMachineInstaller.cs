using InfinityRunner.Scripts.States.Infrastructure;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    public class RunnerStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatesFillHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<RunnerApplicationStateMachine>().AsSingle();

        }
    }
}