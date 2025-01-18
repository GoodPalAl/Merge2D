using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static TimerManager Instance { get; private set; }
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

}
