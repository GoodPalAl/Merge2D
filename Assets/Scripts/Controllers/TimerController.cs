using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerController : MonoBehaviour
{

    /* TODO: 
     * - Timer resets when ONE fruit leaves deadzone. Should reset when there are NO fruit in deadzone.
     * - Change timer to a UnityEvent instead of a Time.deltaTime timer.
     */

    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        ShowTimer(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Show timer if timer has passed 1 second
        float timer = TimerManager.Instance.GetDeathTimer();
        if (timer >= TimerManager.Instance.GetDeathTimeThreshold())
        {
            ShowTimer(true);
            timerText.text = TimerManager.Instance.GetDeathTimerAsString();
        }
        else
        {
            ShowTimer(false); 
        }
    }

    /// <summary>
    /// Enable or disable visibility on timer's text based on input
    /// </summary>
    /// <param name="_x">true = show timer, false = hide timer</param>
    void ShowTimer(bool _x) => timerText.enabled = _x;
}
