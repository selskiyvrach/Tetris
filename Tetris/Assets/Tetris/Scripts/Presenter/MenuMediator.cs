using System;
using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Presenter
{
    internal class MenuMediator : IDisposable
    {
        private readonly IGameMenuView _menuView;
        private readonly IGameMenu _menu;
        
        public event Action OnGameplayRequested;
        
        public MenuMediator(IGameMenu menu, IGameMenuView menuView)
        {
            _menu = menu;
            _menuView = menuView;
            _menuView.OnStartGameplayPressed += RaiseOnGameplayRequested;
        }

        private void RaiseOnGameplayRequested() => 
            OnGameplayRequested?.Invoke();

        public void Dispose() => 
            _menuView.OnStartGameplayPressed -= RaiseOnGameplayRequested;
    }
}