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
    /// Scale steps in fruit hierarchy (size)
    /// </summary>
    //static readonly float FruitScaleSize = 0.25f;
    //static readonly Vector3 FruitStartSizev3 = Vector3.one;
    //static readonly Vector3 FruitScaleSizev3 = new(FruitScaleSize, FruitScaleSize, FruitScaleSize);

    ///
    /// <summary>Fruit in order as enums. First = strawberry, Last = watermelon</summary>
    ///
    public enum Fruit
    {
        Strawberry, 
        Cherry, 
        Apple, 
        Peach, 
        Pear, 
        Orange, 
        Pineapple, 
        Banana, 
        Lemon, 
        Avocado, 
        Kiwi, 
        Coconut, 
        Grape, 
        Watermelon
    }
    /// <summary>
    /// Official merge order of fruit.
    /// </summary>
    public List<GameObject> FruitOrder = new();
    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    public static List<GameObject> DroppedFruit = new();
    public GameObject GetFruitFromEnum(Fruit fruit) => FruitOrder.Find(x => x.CompareTag(fruit.ToString()));
    public int GetFruitIndexFromEnum(Fruit fruit) => FruitOrder.FindIndex(x => x.CompareTag(fruit.ToString()));
    public GameObject GetFruit(int order) => FruitOrder[order];
    public GameObject GetNextFruit(string oldFruitName) 
    {
        int nextIndex = FruitOrder.FindIndex(Fruit => Fruit.name == oldFruitName) + 1;
        return GetFruit(nextIndex);
    }
    public int FruitCount() => FruitOrder.Count;
    

    /// <summary>
    /// Score of Current Game
    /// </summary>
    private int Score = 0;
    /// <summary>
    /// Increment score by 1. TODO: icrement score based on fruit hierarchy.
    /// </summary>
    public void TickScore() => Score++; 
    /// <summary>
    /// Sets Score equal to 0
    /// </summary>
    public void ResetScore() => Score = 0; 
    /// <summary>
    /// Gets current score of game
    /// </summary>
    /// <returns>Integer of score</returns>
    public int GetScore() => Score;
    /// <summary>
    /// Gets current score of game
    /// </summary>
    /// <returns>String of score</returns>
    public string GetScoreAsString() => Score.ToString(); 


    /// <summary>
    /// Represents the time, in seconds, that has ticked since a fruit has entered the dead zone.
    /// </summary>
    private float DeathTimer = 0f;
    /// <summary>
    /// Gets Death Timer in seconds, no rounding
    /// </summary>
    /// <returns>Death Timer, in seconds</returns>
    public float GetDeathTimer() => DeathTimer;
    /// <summary>
    /// Gets Death Timer in seconds as a string, formatted to the nearest hundreth (0.00)
    /// </summary>
    /// <returns>Time in seconds</returns>
    public string GetDeathTimerAsString() => DeathTimer.ToString("0.00");
    public float TickDeathTimer() => DeathTimer += Time.deltaTime;
    /// <summary>
    /// Resets the death timer.
    /// </summary>
    public void ResetDeathTimer() => DeathTimer = 0f;

    /// <summary>
    /// Maximum allowed time, in seconds, a fruit can be in the deadzone before a game over occurs.
    /// </summary>
    private float GameOverTime = 10f;
    /// <summary>
    /// Gets time in which the game would end, in seconds.
    /// </summary>
    /// <returns>Game Over time, in seconds</returns>
    public float GetGameOverTime() => GameOverTime;


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
