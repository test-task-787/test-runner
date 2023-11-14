using Dreamteck.Forever;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    public class GeneratorInstaller : MonoInstaller
    {
        [SerializeField] private LevelGenerator levelGenerator;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelGenerator>().FromInstance(levelGenerator);
        }
    }
}