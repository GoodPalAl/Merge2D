using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

public class CountdownController : MonoBehaviour
{
    // Component conntected to the text of the gameover timer on screen
    TextMeshProUGUI countdownText;
    // Countdown starts and resets at this time in seconds
    float countdownStart;
    // Countdown should refresh every tenth of a second
    float countdownRefreshRate;
    // The amount of time remaining in seconds on the countdown
    float timeLeftOnCountdown;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        countdownStart = DeathTimerManager.Instance.GetSecondsUntilGameOver();
        countdownRefreshRate = DeathTimerManager.Instance.GetCountdownRefreshRate();
        HideCountdown();
    }

    public void TriggerStartCountDown()
    {
        Debug.Log("Countdown Start!");

        // Have the countdown reveal on a slight delay.
        Invoke(nameof(ShowCountdown), 1f);
        // Reset the countdown time & text before begin ticking
        ResetCountdown();
        UpdateCountdownText();

        InvokeRepeating(nameof(TickCountdown), 0f, countdownRefreshRate);
    }

    // FIXME: Does not properly hide count
    public void TriggerStopCountDown()
    {
        Debug.Log("Countdown Stop!");
        // Cancel both invokes to ensure they don't play out of sync
        //  Ex: fruit falls too fast and the invoke is triggered
        //  after it leaves the zone
        CancelInvoke(nameof(TickCountdown));
        CancelInvoke(nameof(ShowCountdown));
        HideCountdown();
    }

    void TickCountdown()
    {
        // The number of times per frame I want the timer to refresh at.
        float waitTime = Time.deltaTime / countdownRefreshRate;
        // Subtract that number from the remaining time on the countdown.
        timeLeftOnCountdown -= waitTime;
        UpdateCountdownText();

        // Once the timer has run out, stop invoking this repeating method.
        if (timeLeftOnCountdown < 0)
        {
            Debug.Log("Game Over!");
            // Reset time to 0 so a negative time doesn't display
            timeLeftOnCountdown = 0;
            UpdateCountdownText();
            // STOP THE COUNT
            CancelInvoke(nameof(TickCountdown));
        }
    }

    void ResetCountdown() => timeLeftOnCountdown = countdownStart;

    void UpdateCountdownText() => countdownText.text = timeLeftOnCountdown.ToString("0.00");

    void ShowCountdown() => countdownText.enabled = true;
    void HideCountdown() => countdownText.enabled = false;

}
