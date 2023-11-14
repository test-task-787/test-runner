using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates.RunContext.Configs
{
    /// <summary>
    /// Convenient and user-friendly config for run params.
    /// </summary>
    public class RunConfigSO : ScriptableObject, IRunConfigProvider
    {
        [SerializeField] private RunConfig config;

        public RunConfig Config => config;
    }
}