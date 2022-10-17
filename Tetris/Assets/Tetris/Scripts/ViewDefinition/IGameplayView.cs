namespace Tetris.ViewDefinition
{
    public interface IGameplayView
    {
        IGameplayControls GameplayControls { get; }
        void UpdateBoardState(bool[,] board);
    }
}