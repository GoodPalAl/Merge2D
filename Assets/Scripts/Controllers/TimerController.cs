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



    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        //deathCountdownStart = DeathTimerManager.Instance.GetDeathTimer();
        HideCountdown();
    }

    // Timer should refresh every tenth of a second
    float timerRefreshRate = .1f;
    // Countdown starts at this time in seconds
    float deathCountdownStart = 10f;
    // The amount of time remaining in seconds on the countdown
    float remainingTime;
    public void TriggerStartCountDown()
    {
        Debug.Log("Countdown Start!");
        Invoke(nameof(ShowCountdown), .1f);
        ResetTimer();
        UpdateTimerText();

        InvokeRepeating(nameof(InvokeTimer), 0, timerRefreshRate);
    }

    // FIXME: Won't rehide the timer??
    public void TriggerStopCountDown()
    {
        Debug.Log("Countdown Stop!");
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

    void ResetTimer() => remainingTime = deathCountdownStart;

    void UpdateTimerText() => countdownText.text = remainingTime.ToString("0.00");

    void ShowCountdown() => countdownText.enabled = true;

    void HideCountdown() => countdownText.enabled = false;

    void oldTimer()
    {
        // Show timer if timer has passed 1 second
        float timer = DeathTimerManager.Instance.GetDeathTimer();
        if (timer >= DeathTimerManager.Instance.GetDeathTimeThreshold())
        {
            ShowCountdown();
            countdownText.text = DeathTimerManager.Instance.GetDeathTimerAsString();
        }
        else
        {
            HideCountdown();
        }
    }

}
