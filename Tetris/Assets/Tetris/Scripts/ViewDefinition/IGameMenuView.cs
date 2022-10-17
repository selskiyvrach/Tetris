using System;

namespace Tetris.ViewDefinition
{
    public interface IGameMenuView
    {
        event Action OnStartGameplayPressed;
    }
}