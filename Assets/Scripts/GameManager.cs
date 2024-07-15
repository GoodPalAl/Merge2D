using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        { 
            Instance = this; 
        }
    }

    #endregion

    /// <summary>
    /// Official merge order of fruit.
    /// </summary>
    public List<GameObject> FruitOrder = new();

    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    public static List<GameObject> DroppedFruit = new();

    public GameObject GetFruit(int order) => FruitOrder[order];
    public int FruitCount() => FruitOrder.Count;

    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// </summary>
    public void ClearBoard()
    {
        foreach (var obj in DroppedFruit)
        {
            DestroyImmediate(obj, true);
        }
        DroppedFruit.Clear();
    }
    public class Constants
    {
        public static readonly string PathToPrefabs = "Assets/Prefabs";
        /// <summary>
        /// Width of the board
        /// </summary>
        public const float BoardSize = 5.5f;
        /// <summary>
        /// Position of board's left border from center 
        /// </summary>
        public const float MapBorderLeft = -(BoardSize / 2);
        /// <summary>
        /// Position of board's right border from center 
        /// </summary>
        public const float MapBorderRight = (BoardSize / 2);
        /// <summary>
        /// Height cursor is locked at
        /// </summary>
        public const float StartHeight = 4f;
    }
}
