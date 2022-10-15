using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Mediator
{
    public class TetrisMediator
    {
        private readonly ITetrisModel _tetris;
        private readonly ITetrisView _tetrisView;

        public TetrisMediator(ITetrisModel tetris, ITetrisView tetrisView)
        {
            _tetris = tetris;
            _tetrisView = tetrisView;

            _tetris.OnBoardStateChanged += _tetrisView.UpdateBoardState;

            _tetrisView.OnStartPressed += _tetris.Start;
            
            _tetrisView.OnLeftPressed += _tetris.MoveLeft;
            _tetrisView.OnRightPressed += _tetris.MoveRight;
            _tetrisView.OnDownPressed += _tetris.MoveDown;
            _tetrisView.OnRotatePressed += _tetris.Rotate;
            
            _tetris.Start();
        }
    }
}