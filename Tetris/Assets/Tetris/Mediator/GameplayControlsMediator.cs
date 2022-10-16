using System;
using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Mediator
{
    public class GameplayControlsMediator : IDisposable
    {
        private readonly IGameplayControls _controls;
        private readonly IGameplayControlsListener _listener;

        public GameplayControlsMediator(IGameplayControls controls, IGameplayControlsListener listener)
        {
            _controls = controls;
            _listener = listener;
            _controls.OnDownPressed += _listener.MoveDown;
            _controls.OnLeftPressed += _listener.MoveLeft;
            _controls.OnRightPressed += _listener.MoveRight;
            _controls.OnRotatePressed += _listener.Rotate;
        }

        public void Dispose()
        {
            _controls.OnDownPressed -= _listener.MoveDown;
            _controls.OnLeftPressed -= _listener.MoveLeft;
            _controls.OnRightPressed -= _listener.MoveRight;
            _controls.OnRotatePressed -= _listener.Rotate;
        }
    }
}