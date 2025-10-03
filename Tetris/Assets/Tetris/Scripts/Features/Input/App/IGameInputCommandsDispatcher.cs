using System;

namespace Features.Input.App
{
    public interface IGameInputCommandsDispatcher
    {
        event Action<Commands> OnNewCommand;
    }
}