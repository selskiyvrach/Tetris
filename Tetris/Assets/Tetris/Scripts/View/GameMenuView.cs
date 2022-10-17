using System;
using Tetris.ViewDefinition;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.View
{
    public class GameMenuView : MonoBehaviour, IGameMenuView
    {
        [SerializeField] private Button _startGameButton;
        
        public event Action OnStartGameplayPressed;

        private void OnEnable() =>
            _startGameButton.onClick.AddListener(RaiseOnStartGamePressed);

        private void OnDisable() =>
            _startGameButton.onClick.RemoveListener(RaiseOnStartGamePressed);

        private void RaiseOnStartGamePressed() => 
            OnStartGameplayPressed?.Invoke();
    }
}