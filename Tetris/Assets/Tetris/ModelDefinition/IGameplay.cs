using System;

namespace Tetris.ModelDefinition
{
    public interface IGameplay
    {
        event Action OnGameOver;
        event Action<bool[,]> OnBoardStateChanged;
        IGameplayControlsListener ControlsListener { get; }
        void Run();
    }
}