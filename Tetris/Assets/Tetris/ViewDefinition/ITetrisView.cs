namespace Tetris.ViewDefinition
{
    public interface ITetrisView
    {
        IGameMenuView SwitchToMenuView();
        IGameplayView SwitchToGameplayView();
    }
}