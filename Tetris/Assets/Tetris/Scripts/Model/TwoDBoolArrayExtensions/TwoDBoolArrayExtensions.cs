using UnityEngine;

namespace Tetris.Model.TwoDBoolArrayExtensions
{
    public static class TwoDBoolArrayExtensions
    {
        public static bool OutOfBounds(this bool[,] array, int x, int y) => 
            x < 0 || x >= array.GetLength(0) || y < 0 || y >= array.GetLength(1);

        public static bool OverlapsOrOutOfBounds(this bool[,] arrayA, bool[,] arrayB, Vector2Int offset)
        {
            for (var x = 0; x < arrayA.GetLength(0); x++)
            for (int y = 0; y < arrayA.GetLength(1); y++)
                if (arrayB.OutOfBounds(x + offset.x, y + offset.y) || 
                    arrayB[x + offset.x, y + offset.y] && arrayA[x, y])
                    return true;
            return false;
        }

        public static void AddProjection(this bool[,] arrayA, bool[,] arrayB, Vector2Int offset)
        {
            for (var x = 0; x < arrayB.GetLength(0); x++)
            for (int y = 0; y < arrayB.GetLength(1); y++)
                arrayA[x + offset.x, y + offset.y] = arrayA[x + offset.x, y + offset.y] || arrayB[x, y];
        }

        public static void RemoveProjection(this bool[,] arrayA, bool[,] arrayB, Vector2Int offset)
        {
            for (var x = 0; x < arrayB.GetLength(0); x++)
            for (int y = 0; y < arrayB.GetLength(1); y++)
                if (arrayB[x, y])
                    arrayA[x + offset.x, y + offset.y] = false;
        }

        public static void Clear(this bool[,] array)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                array[x, y] = false;
        }

        public static bool RowIsEmpty(this bool[,] array, int row)
        {
            for (int column = 0; column < array.GetLength(0); column++)
            {
                if (array[column, row])
                    return false;
            }
            return true;
        }

        public static void ClearRow(this bool[,] array, int row)
        {
            for (int column = 0; column < array.GetLength(0); column++) 
                array[column, row] = false;
        }

        public static void SwapRows(this bool[,] array, int rowA, int rowB)
        {
            for (int x = 0; x < array.GetLength(0); x++)
                (array[x, rowA], array[x, rowB]) = (array[x, rowB], array[x, rowA]);
        }
        
        public static bool[,] RotateCounterClockwise(this bool[,] oldMatrix)
        {
            var newMatrix = new bool[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (var oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (var oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }
    }
}