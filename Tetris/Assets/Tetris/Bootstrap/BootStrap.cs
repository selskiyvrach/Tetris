using Tetris.Mediator;
using Tetris.View;
using UnityEngine;

namespace Tetris.Bootstrap
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private TetrisView _tetrisView;
        
        private Model.Tetris _tetris;
        private TetrisMediator _mediator;

        private void Start()
        {
            _tetris = new Model.Tetris();
            _mediator = new TetrisMediator(_tetris, _tetrisView);
        }
    }
}