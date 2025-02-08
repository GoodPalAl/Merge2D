using UnityEngine;
using UnityEngine.Events;

public class DeathTimerManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static DeathTimerManager Instance { get; private set; }
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
    /// Maximum allowed time, in seconds, a fruit can be in the deadzone before a game over occurs.
    /// </summary>
    [SerializeField, Range(5f, 20f)]
    float secondsUntilGameOver = 10f;
    public float GetSecondsUntilGameOver() => secondsUntilGameOver;


    // Rate at which the countdown text should refresh per second
    float countdownRefreshRate = 0.01f;
    public float GetCountdownRefreshRate() => countdownRefreshRate;

}
