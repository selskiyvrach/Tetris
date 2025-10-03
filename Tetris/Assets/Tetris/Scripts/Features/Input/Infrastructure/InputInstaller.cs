using Features.Input.App;
using Libs.Bootstrap;
using UnityEngine;

namespace Features.Input.Infrastructure
{
    public class InputInstaller : Installer
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private UnityInputAdapter _unityInputAdapter;
        
        public override void Install(RunnableContext context)
        {
            var useCase = new ProcessInputEventsUseCase(new ConfigurableRepeatInputStrategy(_inputConfig), new LastPressedWinsResolveStrategy());
            _unityInputAdapter.Construct(listener: useCase, _inputConfig);
            
            context.Register<IGameInputCommandsDispatcher>(useCase);
        }
    }
}