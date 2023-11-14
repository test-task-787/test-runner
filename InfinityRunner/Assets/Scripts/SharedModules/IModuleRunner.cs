using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace SharedModules
{

    public enum ModuleState
    {
        
    }
    
    public interface IModuleRunner
    {
        IReadOnlyReactiveProperty<bool> IsLaunched { get; }
        UniTask<ModuleState> Run();
    }
}