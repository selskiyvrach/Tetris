using Tetris.Mediator;
using Tetris.View;
using UnityEngine;

namespace Tetris.Bootstrap
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private TetrisView _tetrisView;
        
        private Model.TetrisModel _tetrisModel;
        private TetrisMediator _mediator;

        private void Start()
        {
            _tetrisModel = new Model.TetrisModel();
            _mediator = new TetrisMediator(_tetrisModel, _tetrisView);
        }
    }
}