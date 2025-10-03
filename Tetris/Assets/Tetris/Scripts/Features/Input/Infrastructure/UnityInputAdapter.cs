using System;
using System.Linq;
using Features.Input.App;
using Libs.Core;
using UnityEngine;

namespace Features.Input.Infrastructure
{
    internal sealed class UnityInputAdapter : MonoBehaviour, ITickable, IInitializable
    {
        private readonly ExceptionHandler _exceptionHandler = new();
        
        private IInboundInputPort _listener;
        private IInputCommandsToKeyCodesMapping _keyMapping;
        private (Commands command, KeyCode key)[] _keys;
        private Commands _commandsBuffer;

        internal void Construct(IInboundInputPort listener, IInputCommandsToKeyCodesMapping keyMapping)
        {
            _keyMapping = keyMapping;
            _listener = listener;
        }

        public void Initialize() => 
            _keys = Enum.GetValues(typeof(Commands)).Cast<Commands>().Select(n => (n, _keyMapping.GetKeyCode(n))).ToArray();

        public void Tick(float deltaTime)
        {
            _exceptionHandler.ThrowIfAnyAndReset();
            
            _listener.Push(_commandsBuffer);
            _commandsBuffer = Commands.None;
        }

        private void Update()
        {
            _exceptionHandler.RunSafely(UpdateLogic);
            return;

            void UpdateLogic()
            {
                foreach (var (command, key) in _keys)
                {
                    if(UnityEngine.Input.GetKey(key))
                        _commandsBuffer |= command;
                }
            }
        }
    }
}