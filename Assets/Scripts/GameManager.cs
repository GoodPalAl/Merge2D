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
    public GameObject GetFruitFromEnum(Fruit _fruit) => FruitOrder.Find(x => x.CompareTag(_fruit.ToString()));
    public int GetFruitIndexFromEnum(Fruit _fruit) => FruitOrder.FindIndex(x => x.CompareTag(_fruit.ToString()));
    public GameObject GetFruit(int _order) => FruitOrder[_order];
    public GameObject GetNextFruit(string _oldFruitName) 
    {
        int nextIndex = FruitOrder.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        return GetFruit(nextIndex);
    }
    public int FruitCount() => FruitOrder.Count;


    // TODO: Redo timer to use UnityEvent instead of Time.deltaTime
    /// <summary>
    /// Represents the time, in seconds, that has ticked since a fruit has entered the dead zone.
    /// </summary>
    float deathTimer = 0f;

    /// <summary>
    /// Gets Death Timer in seconds, no rounding
    /// </summary>
    /// <returns>Death Timer, in seconds</returns>
    public float GetDeathTimer() => deathTimer;

    /// <summary>
    /// Gets Death Timer in seconds as a string, formatted to the nearest hundreth (0.00)
    /// </summary>
    /// <returns>Time in seconds</returns>
    public string GetDeathTimerAsString() => deathTimer.ToString("0.00");
    public float TickDeathTimer() => deathTimer += Time.deltaTime;

    /// <summary>
    /// Resets the death timer.
    /// </summary>
    public void ResetDeathTimer() => deathTimer = 0f;

    /// <summary>
    /// Time between the timer being shown to the player 
    /// and the time fruit linger in the dead zone. 
    /// Accounts for dropping fruit.
    /// </summary>
    readonly float deathTimeThreshold = 1f;
    public float GetDeathTimeThreshold() => deathTimeThreshold;

    /// <summary>
    /// Maximum allowed time, in seconds, a fruit can be in the deadzone before a game over occurs.
    /// </summary>
    readonly float gameOverTime = 10f;

    /// <summary>
    /// Gets time in which the game would end, in seconds.
    /// </summary>
    /// <returns>Game Over time, in seconds</returns>
    public float GetGameOverTime() => gameOverTime;


    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// </summary>
    public void ClearBoard()
    {
        foreach (var obj in DroppedFruit)
        {
            Destroy(obj);
        }
        DroppedFruit.Clear();
        ScoreManager.Instance.ResetScore();
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
