using System.Collections.Generic;
using System.Linq;
using Flags = System.Reflection.BindingFlags;

namespace Features.Gameplay
{
    /// <summary>
    /// Each shape is encoded as a 16-bit mask representing a 4×4 grid.<br/>
    /// Bits are read row by row, from top to bottom, left to right:<br/>
    /// 
    /// <para>Row 1: bits 15..12  (0000 .... .... ....)<br/>
    ///       Row 2: bits 11..8   (.... 0000 .... ....)<br/>
    ///       Row 3: bits 7..4    (.... .... 0000 ....)<br/>
    ///       Row 4: bits 3..0    (.... .... .... 0000)</para>
    /// </summary>
    public sealed class Shape
    {
        private ushort _mask;

        private Shape(ushort mask) => _mask = mask;
        
        public static readonly Shape I = new(0x0F00); // 0000 1111 0000 0000
        public static readonly Shape T = new(0x0E40); // 0000 1110 0100 0000
        public static readonly Shape O = new(0x0660); // 0000 0110 0110 0000
        public static readonly Shape S = new(0x06C0); // 0000 0110 1100 0000
        public static readonly Shape Z = new(0x0C60); // 0000 1100 0110 0000
        public static readonly Shape L = new(0x08E0); // 0000 1000 1110 0000
        public static readonly Shape J = new(0x02E0); // 0000 0010 1110 0000

        public static Shape Random => 
            DefaultShapes[UnityEngine.Random.Range(0, DefaultShapes.Count)];

        private static readonly IReadOnlyList<Shape> DefaultShapes = typeof(Shape)
            .GetFields(Flags.Public | Flags.Static)
            .Where(f => f.FieldType == typeof(Shape))
            .Select(f => (Shape)f.GetValue(null)!)
            .ToArray();
        
        public IEnumerable<(int x, int y)> Cells()
        {
            for (var y = 0; y < 4; y++)
            for (var x = 0; x < 4; x++)
                if (ContainsPoint(_mask, x, y))
                    yield return new (x, y);
        }

        public bool ContainsPoint(int x, int y) => 
            ContainsPoint(_mask, x, y);

        public void RotateCW()
        {
            ushort resultMask = 0;
            for (var y = 0; y < 4; y++)
            for (var x = 0; x < 4; x++)
            {
                if (!ContainsPoint(_mask, x, y)) 
                    continue;

                var ny = 3 - x;
                var bitIndex = 15 - (ny * 4 + y);
                resultMask |= (ushort)(1 << bitIndex);
            }
            _mask = resultMask;
        }

        private static bool ContainsPoint(ushort mask, int x, int y) => 
            ((mask >> 15 - (y * 4 + x)) & 1) != 0;
    }
}