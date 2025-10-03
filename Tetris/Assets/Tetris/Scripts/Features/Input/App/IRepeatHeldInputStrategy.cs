namespace Features.Input.App
{
    public interface IRepeatHeldInputStrategy
    {
        void ProcessTimePassed(float timeDelta, out bool repeat);
        void Reset();
    }
}