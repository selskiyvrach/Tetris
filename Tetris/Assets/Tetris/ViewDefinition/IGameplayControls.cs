using System;

namespace Tetris.ViewDefinition
{
    public interface IGameplayControls
    {
        event Action OnLeftPressed;
        event Action OnRightPressed;
        event Action OnDownPressed;
        event Action OnRotatePressed;
    }
}