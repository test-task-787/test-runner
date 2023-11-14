using InfinityRunner.CollectableAppliers;
using InfinityRunner.Scripts.Level.Boosters.CoinStrategies;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    public class CoinSpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CoinSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectableApplyService>().AsSingle();
        }
    }
}