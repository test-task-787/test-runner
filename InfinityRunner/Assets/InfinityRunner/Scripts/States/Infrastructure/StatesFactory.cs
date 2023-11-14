using System.Collections.Generic;
using Zenject;

namespace InfinityRunner.Scripts.States.Infrastructure
{
    public class StatesFactory
    {
        private readonly DiContainer _container;

        public StatesFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>(IList<object> args = null) where T : IRunnerApplicationState
        {
            return args == null ? _container.Instantiate<T>() : _container.Instantiate<T>(args);
        }
    }
}