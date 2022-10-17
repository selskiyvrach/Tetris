namespace Tetris.ModelDefinition
{
    public interface IGameplayControlsListener
    {
        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void Rotate();
    }
}