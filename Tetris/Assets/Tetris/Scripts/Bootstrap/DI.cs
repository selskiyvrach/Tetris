using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Tetris.Bootstrap
{
    public static class Di
    {
        public static DiContext CreateContext(DiContextConfig diContextConfig, [CanBeNull] DiContext parent)
        {
            var context = new DiContext(diContextConfig, parent);
            context.PerformInjections();
            return context;
        }
    }

    public class DiContextConfig
    {
        public IEnumerable<IInstaller> Installers { get; }
    }
    
    public interface IInstaller
    {
        void Install(DiContext context, out bool dependenciesMissing);
    }

    public class DiContext
    {
        private readonly Dictionary<Type, object> _contextElementsMap = new();
        
        [CanBeNull] private readonly DiContext _parent;
        private readonly DiContextConfig _contextConfig;

        public DiContext(DiContextConfig contextConfig, [CanBeNull]DiContext parent)
        {
            _parent = parent;
            _contextConfig = contextConfig;
        }

        [CanBeNull]
        public T Get<T>() where T : class
        {
            if (_contextElementsMap.TryGetValue(typeof(T), out var item))
                return (T)item;
            return _parent?.Get<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            for (int i = 0; i < _contextElementsMap.Count; i++)
            {
                if (_contextElementsMap.ElementAt(i).Value is T item)
                    yield return item;
            }
        }

        public void Add<T>(T item) =>
            _contextElementsMap.Add(typeof(T), item);

        internal void PerformInjections()
        {
            var installersLeft = _contextConfig.Installers.ToList();
            while (true)
            {
                PerformOneIterationOfInjection(installersLeft, out bool dependenciesInstalledThisRound);

                if (!dependenciesInstalledThisRound && installersLeft.Count > 0)
                    throw new DependenciesInjectionFailedException(installersLeft);
            }
        }

        private void PerformOneIterationOfInjection(List<IInstaller> installersLeft, out bool dependenciesInstalledThisRound)
        {
            dependenciesInstalledThisRound = false;
            for (var i = installersLeft.Count - 1; i >= 0; i--)
            {
                installersLeft[i].Install(this, out bool dependenciesWereMissing);
                if (!dependenciesWereMissing)
                    continue;
                dependenciesInstalledThisRound = true;
                installersLeft.RemoveAt(i);
            }
        }
    }

    internal class DependenciesInjectionFailedException : Exception
    {
        private readonly IEnumerable<IInstaller> _unresolvedInstallers;

        public DependenciesInjectionFailedException(IEnumerable<IInstaller> unresolvedInstallers) => 
            _unresolvedInstallers = unresolvedInstallers;

        public override string ToString() =>
            $"Failed to inject from the following installers: " +
            $"{_unresolvedInstallers.Select(n => n.GetType().Name + "\n")}";
    }
}