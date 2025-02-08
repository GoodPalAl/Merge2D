using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

public class GameOverTimerController : MonoBehaviour
{

    /* TODO: 
     * - Timer resets when ONE fruit leaves deadzone. Should reset when there are NO fruit in deadzone.
     * - Change timer to a UnityEvent instead of a Time.deltaTime timer.
     */

    TextMeshProUGUI countdownText;
    // Timer should refresh every tenth of a second
    float timerRefreshRate;
    // Countdown starts at this time in seconds
    float countdownStart;
    // The amount of time remaining in seconds on the countdown
    float remainingTime;

    static bool isCountdownRunning;
    public static bool IsCountdownRunning => isCountdownRunning;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        countdownStart = DeathTimerManager.Instance.GetSecondsUntilGameOver();
        timerRefreshRate = DeathTimerManager.Instance.GetCountdownRefreshRate();
        StopCountdown();
    }

    public void TriggerStartCountDown()
    {
        //Debug.Log("Countdown Start!");
        ShowCountdown();
        ResetTimer();
        UpdateTimerText();

        InvokeRepeating(nameof(InvokeTimer), 0, timerRefreshRate);
    }

    public void TriggerStopCountDown()
    {
        //Debug.Log("Countdown Stop!");
        StopCountdown();
    }

    void InvokeTimer()
    {
        StartCountdown();
        remainingTime -= Time.deltaTime;
        UpdateTimerText();

        // Once the timer has run out, stop invoking this repeating method.
        if (remainingTime < 0)
        {
            Debug.Log("Game Over!");
            StopCountdown();
        }
    }

    void ResetTimer() => remainingTime = countdownStart;

    void UpdateTimerText() => countdownText.text = remainingTime.ToString("0.00");
    void StartCountdown()
    {
        isCountdownRunning = true;
    }
    void StopCountdown()
    {
        HideCountdown();
        isCountdownRunning = false;
        CancelInvoke(nameof(InvokeTimer));
    }

    void ShowCountdown() => countdownText.enabled = true;

    void HideCountdown() => countdownText.enabled = false;


}
