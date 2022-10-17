using Tetris.ModelDefinition;

namespace Tetris.Model
{
    public class TetrisModel : ITetrisModel
    {
        private readonly Menu.Menu _menu = new();
        private readonly Gameplay.Gameplay _gameplay = new();
        
        public IGameMenu SwitchToMenu()
        {
            _gameplay.Dispose();
            return _menu;
        }

        public IGameplay SwitchToGameplay()
        {
            _menu.Dispose();
            return _gameplay;
        }
    }
}
