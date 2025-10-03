namespace Features.Input.App
{
    public interface IInboundInputPort
    {
        void Push(Commands input);
    }
}