using System;
using System.Collections.Generic;
using Libs.Core;
using UnityEngine;

namespace Libs.Bootstrap
{
    [DisallowMultipleComponent]
    public class RunnableContext : MonoBehaviour
    {
        private readonly Dictionary<Type, object> _instances = new();
        private readonly HashSet<IInitializable> _initializables = new();
        private readonly HashSet<ITickable> _tickables = new();
        private readonly HashSet<IDisposable> _disposables = new();

        public void RunInstallers()
        {
            foreach (var installer in GetComponents<Installer>()) 
                installer.Install(this);
        }

        public T Get<T>() => 
            (T)_instances[typeof(T)];

        public void Register<TContract>(TContract instance)
        {
            _instances.Add(typeof(TContract), instance);
            
            if(instance is IInitializable initializable)
                _initializables.Add(initializable);
            if(instance is ITickable tickable)
                _tickables.Add(tickable);
            if(instance is IDisposable disposable)
                _disposables.Add(disposable);
        }

        public void RunInitializables()
        {
            foreach (var initializable in _initializables) 
                initializable.Initialize();
        }

        public void RunTickables(float timeDelta)
        {
            foreach (var tickable in _tickables) 
                tickable.Tick(timeDelta);
        }

        public void RunDisposables()
        {
            foreach (var disposable in _disposables) 
                disposable.Dispose();
        }
    }
}