namespace Libs.Core
{
    public static class BitmaskOperations
    {
        public static bool IsSingleFlag(int mask) => 
            (mask & (mask - 1)) == 0;
        
        public static bool EmptyOrSingleFlag(int mask) =>
            Empty(mask) || IsSingleFlag(mask);

        public static bool AreDisjoint(int a, int b) => 
            (a & b) == 0;

        public static int FirstSetFlag(int mask) => 
            mask & -mask;
        
        public static bool ContainsFlag(int mask, int flag) =>
            (mask & flag) != 0;
        
        public static int ClearFlags(int mask, int flags) =>
            mask & ~flags;

        public static bool Empty(int mask) => 
            mask == 0;

        public static bool NotEmpty(int mask) => 
            !Empty(mask);
    }
}