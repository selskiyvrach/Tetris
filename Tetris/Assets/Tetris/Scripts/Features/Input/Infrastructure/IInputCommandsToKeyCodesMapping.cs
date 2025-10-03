using Features.Input.App;
using UnityEngine;

namespace Features.Input.Infrastructure
{
    internal interface IInputCommandsToKeyCodesMapping
    {
        KeyCode GetKeyCode(Commands command);
    }
}