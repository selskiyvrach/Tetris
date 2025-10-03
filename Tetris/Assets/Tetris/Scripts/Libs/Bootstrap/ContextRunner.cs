using Libs.Core;
using UnityEngine;

namespace Libs.Bootstrap
{
    [DisallowMultipleComponent, RequireComponent(typeof(RunnableContext))]
    public class ContextRunner : MonoBehaviour
    {
        [SerializeField] private RunnableContext _context;
        private ExceptionHandler _exceptionHandler;

        private void OnValidate() => 
            _context ??= GetComponent<RunnableContext>();

        private void Start()
        {
            _exceptionHandler.RunSafely(_context.RunInstallers);
            _exceptionHandler.ThrowIfAnyAndReset();
            
            _exceptionHandler.RunSafely(_context.RunInitializables);
            _exceptionHandler.ThrowIfAnyAndReset();
        }

        private void Update()
        {
            _exceptionHandler.RunSafely(Tick);
            _exceptionHandler.ThrowIfAnyAndReset();
            return;

            void Tick() => 
                _context.RunTickables(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            _exceptionHandler.RunSafely(_context.RunDisposables);
            _exceptionHandler.ThrowIfAnyAndReset();
        }
    }
}