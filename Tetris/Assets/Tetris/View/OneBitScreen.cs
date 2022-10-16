using UnityEngine;
using UnityEngine.UI;

namespace Tetris.View
{
    public class OneBitScreen : MonoBehaviour
    {
        [SerializeField] private Image[] _pixels;

        public void UpdateScreenContent(bool[,] screenContent)
        {
            for (int y = 0; y < screenContent.GetLength(1); y++)
            for (int x = 0; x < screenContent.GetLength(0); x++)
                _pixels[y * screenContent.GetLength(0) + x].enabled = screenContent[x, y];
        }
    }
}