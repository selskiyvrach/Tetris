using System;
using Tetris.ViewDefinition;
using UnityEngine;

namespace Tetris.View
{
    public class GameplayControls : MonoBehaviour, IGameplayControls
    {
        [SerializeField] private KeyCode _downKeyCode = KeyCode.S;
        [SerializeField] private KeyCode _rightKeyCode = KeyCode.D;
        [SerializeField] private KeyCode _leftKeyCode = KeyCode.A;
        [SerializeField] private KeyCode _rotateKeyCode = KeyCode.W;
        [SerializeField] private float _downRepeatTime = 0.1f;
        private float _downTimeHeld;

        public event Action<InputCommand> OnPlayerInput; 

        private void Update()
        {
            if(Input.GetKeyDown(_rightKeyCode))
                OnRightPressed?.Invoke();
            if(Input.GetKeyDown(_leftKeyCode))
                OnLeftPressed?.Invoke();
            if(Input.GetKeyDown(_rotateKeyCode))
                OnRotatePressed?.Invoke();
            UpdateDownCommand();
        }

        private void UpdateDownCommand()
        {
            if (Input.GetKeyDown(_downKeyCode))
                OnDownPressed?.Invoke();
            else if (Input.GetKeyUp(_downKeyCode))
                _downTimeHeld = 0;
            if (!Input.GetKey(_downKeyCode)) 
                return;
            if((_downTimeHeld += Time.deltaTime) < _downRepeatTime)
                return;
            OnDownPressed?.Invoke();
            _downTimeHeld = 0;
        }
    }
}