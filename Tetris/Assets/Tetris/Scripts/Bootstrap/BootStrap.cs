using Tetris.Model;
using Tetris.Presenter;
using Tetris.View;
using UnityEngine;

namespace Tetris.Bootstrap
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private TetrisView _tetrisView;
        
        private TetrisModel _tetrisModel;
        private TetrisMediator _mediator;

        private void Start()
        {
            _tetrisModel = new TetrisModel();
            _mediator = new TetrisMediator(_tetrisModel, _tetrisView);
        }
    }
}