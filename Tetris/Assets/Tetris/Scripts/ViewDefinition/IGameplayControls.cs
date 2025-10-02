using System;

namespace Tetris.ViewDefinition
{
    public interface IGameplayControls
    {
        event Action<InputCommand> OnPlayerInput;
    }
}