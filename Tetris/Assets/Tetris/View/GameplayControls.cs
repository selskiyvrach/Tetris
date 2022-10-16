using System;
using Tetris.ViewDefinition;
using UnityEngine;

namespace Tetris.View
{
    public class GameplayControls : MonoBehaviour, IGameplayControls
    {
        [SerializeField] private GameplayControlsKeycodes _keyCodes;
        [SerializeField] private float _downRepeatTime = 0.1f;
        private float _downTimeHeld;

        public event Action OnLeftPressed;
        public event Action OnRightPressed;
        public event Action OnDownPressed;
        public event Action OnRotatePressed;
        
        private void Update()
        {
            if(Input.GetKeyDown(_keyCodes.RightKeyCode))
                OnRightPressed?.Invoke();
            if(Input.GetKeyDown(_keyCodes.LeftKeyCode))
                OnLeftPressed?.Invoke();
            if(Input.GetKeyDown(_keyCodes.RotateKeyCode))
                OnRotatePressed?.Invoke();
            UpdateDownCommand();
        }

        private void UpdateDownCommand()
        {
            if (Input.GetKeyDown(_keyCodes.DownKeyCode))
                OnDownPressed?.Invoke();
            else if (Input.GetKeyUp(_keyCodes.DownKeyCode))
                _downTimeHeld = 0;
            if (!Input.GetKey(_keyCodes.DownKeyCode)) 
                return;
            if((_downTimeHeld += Time.deltaTime) < _downRepeatTime)
                return;
            OnDownPressed?.Invoke();
            _downTimeHeld = 0;
        }
    }
}