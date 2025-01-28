using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

public class TimerController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        countdownStart = DeathTimerManager.Instance.GetSecondsUntilGameOver();
        timerRefreshRate = DeathTimerManager.Instance.CountdownRefreshRate;
        HideCountdown();
    }

    public void TriggerStartCountDown()
    {
        //Debug.Log("Countdown Start!");
        ShowCountdown();
        ResetTimer();
        UpdateTimerText();

        InvokeRepeating(nameof(InvokeTimer), 0, timerRefreshRate);
    }

    // FIXME: Won't rehide the timer??
    public void TriggerStopCountDown()
    {
        //Debug.Log("Countdown Stop!");
        HideCountdown();
        CancelInvoke(nameof(InvokeTimer));
    }

    void InvokeTimer()
    {
        remainingTime -= timerRefreshRate * (Time.deltaTime / timerRefreshRate);
        UpdateTimerText();

        // Once the timer has run out, stop invoking this repeating method.
        if (remainingTime < 0)
        {
            Debug.Log("Game Over!");
            HideCountdown();
            CancelInvoke(nameof(InvokeTimer));
        }
    }

    void ResetTimer() => remainingTime = countdownStart;

    void UpdateTimerText() => countdownText.text = remainingTime.ToString("0.00");

    void ShowCountdown() => countdownText.enabled = true;
    void HideCountdown() => countdownText.enabled = false;

}
