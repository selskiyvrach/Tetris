using System;
using Libs.Core;

namespace Features.Input.App
{
    public class ProcessInputEventsUseCase : IInboundInputPort, ITickable, IGameInputCommandsDispatcher
    {
        private readonly IRepeatHeldInputStrategy _repeatInputStrategy;
        private readonly IResolveInputStrategy _resolveStrategy;
        
        private Commands _activeCommand;
        private Commands _lastCommandsMask;

        public event Action<Commands> OnNewCommand;

        public ProcessInputEventsUseCase(IRepeatHeldInputStrategy repeatInputStrategy, IResolveInputStrategy resolveStrategy)
        {
            _repeatInputStrategy = repeatInputStrategy;
            _resolveStrategy = resolveStrategy;
        }

        public void Tick(float deltaTime)
        {
            if(_activeCommand == Commands.None)
                return;
            
            _repeatInputStrategy.ProcessTimePassed(deltaTime, out var repeat);
            if(repeat)
                OnNewCommand?.Invoke(_activeCommand);
        }

        public void Push(Commands commandsMask)
        {
            var newActiveCommand = _resolveStrategy.Resolve(activeFlag: _activeCommand, lastMask: _lastCommandsMask, currentMask: commandsMask);
            
            if (newActiveCommand != _activeCommand)
            {
                _activeCommand = newActiveCommand;
                _repeatInputStrategy.Reset();
                if(_activeCommand != Commands.None)
                    OnNewCommand?.Invoke(_activeCommand);
            }

            _lastCommandsMask = commandsMask;
        }
    }
}