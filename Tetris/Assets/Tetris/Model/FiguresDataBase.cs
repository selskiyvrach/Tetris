using System;
using System.Linq;
using Tetris.Model.Figures;
using Random = UnityEngine.Random;

namespace Tetris.Model
{
    internal class FiguresDataBase : IFiguresDataBase
    {
        private static readonly Type[] FiguresTypes;

        static FiguresDataBase() =>
            FiguresTypes = typeof(Figure).Assembly.GetTypes().Where(n => 
                    n.IsSubclassOf(typeof(Figure)) && 
                    !n.IsAbstract && 
                    !n.IsGenericType)
                .ToArray();

        public Figure GetRandom() => 
            (Figure)Activator.CreateInstance(FiguresTypes[Random.Range(0, FiguresTypes.Length)]);
    }
}