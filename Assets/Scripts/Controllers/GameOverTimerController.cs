using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;
using System;

public class GameOverTimerController : MonoBehaviour
{
    /// <summary>
    /// GameObject component that represents the countdown's text that updates
    /// every Nth second. N = refresh rate.
    /// </summary>
    TextMeshProUGUI countdownText;

    /// <summary>
    /// Countdown should refresh at this rate.
    /// </summary>
    float refreshRate;

    /// <summary>
    /// Countdown starts at this time in seconds.
    /// </summary>
    float startTime;

    /// <summary>
    /// The amount of time remaining in seconds on the countdown.
    /// </summary>
    float remainingTime;

    /// <summary>
    /// Flag that indicates if the Game Over countdown is running or not.
    /// Can only be edited in this script.
    /// </summary>
    static bool isCountdownRunning;
    /// <summary>
    /// Public getter of flag that indicates if the Game Over countdown is
    /// running or not.
    /// </summary>
    public static bool IsCountdownRunning => isCountdownRunning;

    public UnityEvent e_GameOver;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        // TODO: Move to an Awake()?
        countdownText = GetComponent<TextMeshProUGUI>();
        startTime = DeathTimerManager.Instance.GetSecondsUntilGameOver();
        refreshRate = DeathTimerManager.Instance.GetCountdownRefreshRate();

        StopCountdown();

        // Apply listeners for events.
        // GAME OVER EVENT:
        // Change game state to lost
        // Display score to user and allow for restart
        try 
        {
            // Change game state to lose
            // CursorFollow and CursorClick checks if the game is running
            // and will disable control if it is not.
            e_GameOver.AddListener(delegate 
            { 
                GameStateManager.Instance.ChangeStateToLost();
            });

            // Show Game Over UI
            var obj_GameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas");
            if (obj_GameOverCanvas == null)
            {
                throw new NullReferenceException("\'GameOverCanvas\' tag not given or found.");
            }
            else
            {
                var listener = obj_GameOverCanvas.GetComponent<GameOverMenu>();
                e_GameOver.AddListener(delegate
                {
                    listener.ShowMenuUI();
                });
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogException(e);
        }
    }

    // TODO: have the timer stay hidden for the first 0.5 seconds
    public void TriggerStartCountDown()
    {
        //Debug.Log("Countdown Start!");
        ShowCountdown();
        ResetTimer();
        UpdateTimerText();

        InvokeRepeating(nameof(InvokeCountdown), 0, refreshRate);
    }

    public void TriggerStopCountDown()
    {
        //Debug.Log("Countdown Stop!");
        StopCountdown();
    }

    /// <summary>
    /// This function ticks the countdown down and stops itself when countdown
    /// reaches 0.
    /// Will repeat every Nth second (N = refresh rate). 
    /// </summary>
    void InvokeCountdown()
    {
        StartCountdown();
        remainingTime -= Time.deltaTime;
        UpdateTimerText();

        // Once the timer has run out, stop invoking this repeating method.
        if (remainingTime < 0)
        {
            // Trigger Game Over event.
            GameOver();

            StopCountdown();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        e_GameOver?.Invoke();
    }

    void ResetTimer() => remainingTime = startTime;

    void UpdateTimerText() => countdownText.text = remainingTime.ToString("0.00");
    
    /// <summary>
    /// Triggers the following events: flags the countdown as started.
    /// </summary>
    void StartCountdown()
    {
        isCountdownRunning = true;
    }

    /// <summary>
    /// Triggers the following events: hides the countdown, indicates the countdown
    /// is no longer running, and cancels any invoking of the repeating method.
    /// </summary>
    void StopCountdown()
    {
        HideCountdown();
        isCountdownRunning = false;
        CancelInvoke(nameof(InvokeCountdown));
    }

    /// <summary>
    /// Reveals the countdown's text to the player.
    /// </summary>
    void ShowCountdown() => countdownText.enabled = true;

    /// <summary>
    /// Hides the countdown's text from view of the player.
    /// </summary>
    void HideCountdown() => countdownText.enabled = false;


}
