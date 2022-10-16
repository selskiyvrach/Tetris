using UnityEngine;

namespace Tetris.View
{
    public class GameplayControlsKeycodes : MonoBehaviour
    {
        [SerializeField] private KeyCode _downKeyCode = KeyCode.S;
        [SerializeField] private KeyCode _rightKeyCode = KeyCode.D;
        [SerializeField] private KeyCode _leftKeyCode = KeyCode.A;
        [SerializeField] private KeyCode _rotateKeyCode = KeyCode.W;
        
        public KeyCode DownKeyCode => _downKeyCode;
        public KeyCode RightKeyCode => _rightKeyCode;
        public KeyCode LeftKeyCode => _leftKeyCode;
        public KeyCode RotateKeyCode => _rotateKeyCode;
    }
}