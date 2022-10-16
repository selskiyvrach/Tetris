namespace Tetris.ModelDefinition
{
    public interface ITetrisModel
    {
        IGameMenu SwitchToMenu();
        IGameplay SwitchToGameplay();
    }
}