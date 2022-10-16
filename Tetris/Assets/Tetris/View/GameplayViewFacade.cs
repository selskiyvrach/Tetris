using Tetris.ViewDefinition;
using UnityEngine;

namespace Tetris.View
{
    public class GameplayViewFacade : MonoBehaviour, IGameplayView
    {
        [SerializeField] private OneBitScreen _oneBitScreen;
        [SerializeField] private GameplayControls _gameplayControls;

        public IGameplayControls GameplayControls => _gameplayControls;
        
        public void UpdateBoardState(bool[,] board) => 
            _oneBitScreen.UpdateScreenContent(board);
    }
}