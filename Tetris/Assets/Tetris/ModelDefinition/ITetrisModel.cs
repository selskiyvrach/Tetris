using System;

namespace Tetris.ModelDefinition
{
    public interface ITetrisModel
    {
        event Action<bool[,]> OnBoardStateChanged;
        void Start();
        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void Rotate();
    }
}