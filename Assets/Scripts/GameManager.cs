using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
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
    public GameObject GetFruitFromEnum(Fruit fruit) 
        => FruitOrder.Find(x => x.CompareTag(fruit.ToString()));
    public int GetFruitIndexFromEnum(Fruit fruit) 
        => FruitOrder.FindIndex(x => x.CompareTag(fruit.ToString()));
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
    int _score = 0;
    /// <summary>
    /// Increment score by 1. TODO: icrement score based on fruit hierarchy.
    /// </summary>
    public void TickScore() => _score++; 
    /// <summary>
    /// Sets Score equal to 0
    /// </summary>
    public void ResetScore() => _score = 0; 
    /// <summary>
    /// Gets current score of game
    /// </summary>
    /// <returns>Integer of score</returns>
    public int GetScore() => _score;
    /// <summary>
    /// Gets current score of game
    /// </summary>
    /// <returns>String of score</returns>
    public string GetScoreAsString() => _score.ToString(); 


    /// <summary>
    /// Represents the time, in seconds, that has ticked since a fruit has entered the dead zone.
    /// </summary>
    float _deathTimer = 0f;
    /// <summary>
    /// Gets Death Timer in seconds, no rounding
    /// </summary>
    /// <returns>Death Timer, in seconds</returns>
    public float GetDeathTimer() => _deathTimer;
    /// <summary>
    /// Gets Death Timer in seconds as a string, formatted to the nearest hundreth (0.00)
    /// </summary>
    /// <returns>Time in seconds</returns>
    public string GetDeathTimerAsString() => _deathTimer.ToString("0.00");
    public float TickDeathTimer() => _deathTimer += Time.deltaTime;
    /// <summary>
    /// Resets the death timer.
    /// </summary>
    public void ResetDeathTimer() => _deathTimer = 0f;
    /// <summary>
    /// Time between the timer being shown to the player 
    /// and the time fruit linger in the dead zone. 
    /// Accounts for dropping fruit.
    /// </summary>
    float _deathTimeThreshold = 1f;
    public float GetDeathTimeThreshold() => _deathTimeThreshold;

    /// <summary>
    /// Maximum allowed time, in seconds, a fruit can be in the deadzone before a game over occurs.
    /// </summary>
    float _gameOverTime = 10f;
    /// <summary>
    /// Gets time in which the game would end, in seconds.
    /// </summary>
    /// <returns>Game Over time, in seconds</returns>
    public float GetGameOverTime() => _gameOverTime;


    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// </summary>
    public void ClearBoard()
    {
        foreach (var _obj in DroppedFruit)
        {
            Destroy(_obj);
        }
        DroppedFruit.Clear();
        ResetScore();
    }


    public class Constants
    {
        public static readonly string PATH_TO_PREFABS = "Assets/Prefabs";
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
