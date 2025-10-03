namespace Features.Input.App
{
    public interface IResolveInputStrategy
    {
        Commands Resolve(Commands activeFlag, Commands lastMask, Commands currentMask);
    }
}