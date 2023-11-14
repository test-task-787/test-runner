namespace InfinityRunner.Scripts.PlayerStates.RunContext.Configs
{
    /// <summary>
    /// For DI. It`s doesn`t matter from where you get config - from ScriptableObject or Object.
    /// </summary>
    public interface IRunConfigProvider
    {
        RunConfig Config { get; }
    }
}