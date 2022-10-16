using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Mediator
{
    public class TetrisMediator
    {
        private readonly ITetrisModel _tetrisModel;
        private readonly ITetrisView _tetrisView;

        private MenuMediator _menuMediator;
        private GameplayMediator _gameplayMediator;

        public TetrisMediator(ITetrisModel tetrisModel, ITetrisView tetrisView)
        {
            _tetrisModel = tetrisModel;
            _tetrisView = tetrisView;
            SwitchToMenu();
        }

        private void SwitchToGameplay()
        {
            if (_menuMediator != null)
            {
                _menuMediator.OnGameplayRequested -= SwitchToGameplay;
                _menuMediator.Dispose();
                _menuMediator = null;
            }

            _gameplayMediator = new GameplayMediator(_tetrisModel.SwitchToGameplay(), _tetrisView.SwitchToGameplayView());
            _gameplayMediator.OnGameOver += SwitchToMenu;
        }

        private void SwitchToMenu()
        {
            if (_gameplayMediator != null)
            {
                _gameplayMediator.OnGameOver -= SwitchToMenu;
                _gameplayMediator?.Dispose();
                _gameplayMediator = null;
            }
            
            _menuMediator = new MenuMediator(_tetrisModel.SwitchToMenu(), _tetrisView.SwitchToMenuView());
            _menuMediator.OnGameplayRequested += SwitchToGameplay;
        }
    }
}