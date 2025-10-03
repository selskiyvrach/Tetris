namespace Features.Input.App
{
    /// <summary>
    /// Repeats held input indefinitely after a config-sourced delay each config-sourced interval   
    /// </summary>
    public class ConfigurableRepeatInputStrategy : IRepeatHeldInputStrategy
    {
        private readonly IRepeatInputStrategyConfig _config;
        private float _heldTime;
        private int _repeatCount;
        
        public ConfigurableRepeatInputStrategy(IRepeatInputStrategyConfig config) => 
            _config = config;

        public void ProcessTimePassed(float timeDelta, out bool repeat)
        {
            repeat = false;
            _heldTime += timeDelta;
            if (_heldTime < _config.StartRepeatDelay + _config.RepeatInterval * _repeatCount) 
                return;
            repeat = true;
            _repeatCount++;
        }

        public void Reset()
        {
            _heldTime = 0;
            _repeatCount = 0;
        }
    }
}