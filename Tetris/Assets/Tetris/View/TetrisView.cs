using System;
using Tetris.ViewDefinition;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.View
{
    public class TetrisView : MonoBehaviour, ITetrisView
    {
        [SerializeField] private GridLayoutGroup _boardView;
        
        public event Action OnStartPressed;
        public event Action OnLeftPressed;
        public event Action OnRightPressed;
        public event Action OnDownPressed;
        public event Action OnRotatePressed;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.D))
                OnRightPressed?.Invoke();
            if(Input.GetKeyDown(KeyCode.A))
                OnLeftPressed?.Invoke();
            if(Input.GetKeyDown(KeyCode.S))
                OnDownPressed?.Invoke();
            if(Input.GetKeyDown(KeyCode.W))
                OnRotatePressed?.Invoke();
        }

        public void UpdateBoardState(bool[,] board)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            for (int x = 0; x < board.GetLength(0); x++)
                _boardView.transform.GetChild(y * board.GetLength(0) + x).GetComponent<Image>().enabled = board[x, y];
        }
    }
}