using Tetris.ViewDefinition;
using UnityEngine;

namespace Tetris.View
{
    public class TetrisView : MonoBehaviour, ITetrisView
    {
        [SerializeField] private GameMenuView _menuView;
        [SerializeField] private GameplayViewFacade _gameplayViewFacade;
        
        public IGameMenuView SwitchToMenuView()
        {
            _menuView.gameObject.SetActive(true);
            _gameplayViewFacade.gameObject.SetActive(false);
            return _menuView;
        }

        public IGameplayView SwitchToGameplayView()
        {
            _menuView.gameObject.SetActive(false);
            _gameplayViewFacade.gameObject.SetActive(true);
            return _gameplayViewFacade;
        }
    }
}