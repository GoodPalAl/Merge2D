using UnityEngine;

namespace GameUtility
{
    public class Constants
    {
        public static readonly string PATH_TO_PREFABS = "Assets/Prefabs";
        public static readonly string PATH_TO_FRUIT_PREFABS = PATH_TO_PREFABS + "/Fruits";

        /// <summary>
        /// Width of the board
        /// </summary>
        public const float BOARD_SIZE = 5.5f;

        /// <summary>
        /// Position of board's left border from center 
        /// </summary>
        public const float MAP_BORDER_LEFT = -(BOARD_SIZE / 2);

        /// <summary>
        /// Position of board's right border from center 
        /// </summary>
        public const float MAP_BORDER_RIGHT = BOARD_SIZE / 2;

        /// <summary>
        /// Height cursor is locked at
        /// </summary>
        public const float START_HEIGHT = 4f;

    }
}