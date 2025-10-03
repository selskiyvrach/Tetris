using Libs.Core;

namespace Features.Input.App
{
    public class LastPressedWinsResolveStrategy : IResolveInputStrategy
    {
        public Commands Resolve(Commands activeFlag, Commands lastMask, Commands currentMask)
        {
            // nothing changed
            if(currentMask == lastMask)
                return activeFlag;
            
            // zero or exactly one button pressed -> return it as is
            if(BitmaskOperations.EmptyOrSingleFlag((int)currentMask))
                return currentMask;

            // take  what buttons appeared that weren't pressed the last frame
            var newCommands = BitmaskOperations.ClearFlags(mask: (int)currentMask, flags: (int)lastMask);
            
            if(BitmaskOperations.NotEmpty(newCommands))
                // if multiple new keys were pressed in the same frame (extremely rare) - take the first one by enum value
                return (Commands)BitmaskOperations.FirstSetFlag(newCommands);
            
            // no new buttons, but the active one is still being held
            if(BitmaskOperations.ContainsFlag(mask: (int)currentMask, (int)activeFlag))
                return activeFlag;
            
            // no new buttons, but the active one released - take the first one from those still held
            return (Commands)BitmaskOperations.FirstSetFlag((int)currentMask);
        }
    }
}