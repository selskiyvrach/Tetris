using System;

namespace Tetris.ViewDefinition
{
    public interface ITetrisView
    {
        event Action OnStartPressed;
        event Action OnLeftPressed;
        event Action OnRightPressed;
        event Action OnDownPressed;
        event Action OnRotatePressed;
        void UpdateBoardState(bool[,] board);
    }
}