using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
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

        //// is this the best place for this?
        //for (int i = 0; i < FruitOrder.Count; i++)
        //{
        //    var fruit = FruitOrder[i];
        //    fruit.transform.localScale = FruitStartSizev3 + (FruitScaleSizev3 * i);
        //}
    }

    #endregion

    /// <summary>
    /// Official merge order of fruit.
    /// </summary>
    public List<GameObject> FruitOrder = new();

    /// <summary>
    /// Scale steps in fruit hierarchy (size)
    /// </summary>
    static readonly float FruitScaleSize = 0.25f;
    static readonly Vector3 FruitStartSizev3 = Vector3.one;
    static readonly Vector3 FruitScaleSizev3 = new Vector3(FruitScaleSize, FruitScaleSize, FruitScaleSize);

    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    public static List<GameObject> DroppedFruit = new();

    public enum Fruit
    {
        Strawberry, Apple, Pear, Orange, Banana, Avocado, Cherry, Coconut, Kiwi, Grape, Lemon, Peach, Pineapple, Watermelon
    }

    public GameObject GetFruitFromEnum(Fruit fruit) => FruitOrder.Find(x => x.CompareTag(fruit.ToString()));
    public int GetFruitIndexFromEnum(Fruit fruit) => FruitOrder.FindIndex(x => x.CompareTag(fruit.ToString()));

    public GameObject GetFruit(int order) => FruitOrder[order];
    public GameObject GetNextFruit(string oldFruitName) 
    {
        int nextIndex = FruitOrder.FindIndex(Fruit => Fruit.name == oldFruitName) + 1;
        return GetFruit(nextIndex);
    }
    
    private int Score = 0;

    public int FruitCount() => FruitOrder.Count;

    public void TickScore() { Score++; }
    public void ResetScore() { Score = 0; }
    public int GetScore() { return Score; }
    public string GetScoreAsString() { return Score.ToString(); }

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
        ResetScore();
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
