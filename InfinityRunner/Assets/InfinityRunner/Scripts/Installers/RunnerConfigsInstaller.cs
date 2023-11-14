using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Installers
{
    [CreateAssetMenu]
    public class RunnerConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private List<ScriptableObject> so;
        public override void InstallBindings()
        {
            foreach (var scriptableObject in so)
            {
                if (scriptableObject != null)
                {
                    Container.BindInterfacesAndSelfTo(scriptableObject.GetType()).FromInstance(scriptableObject);
                }
            }
        }
    }
}