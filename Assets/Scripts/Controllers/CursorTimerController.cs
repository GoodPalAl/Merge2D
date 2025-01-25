using NUnit.Framework.Internal;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class CursorTimerController : MonoBehaviour
{
    TextMeshProUGUI countdownText;


    float clickDelaySeconds;
    float countdownSpeed = 1f;
    float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        clickDelaySeconds = CursorManager.Instance.GetCursorDelay();
        ShowCountdown(false);
    }

    public void ClickTriggered()
    {
        ResetTimer();
        UpdateTimerText();
        StartCoroutine(TimerCountdown());
    }
    IEnumerator TimerCountdown()
    {
        ShowCountdown(true);
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(countdownSpeed);
            remainingTime -= countdownSpeed;
            UpdateTimerText();
        }
        ShowCountdown(false);
        Debug.Log("Cursor Countdown Finished");
    }

    void ResetTimer() => remainingTime = clickDelaySeconds;

    void UpdateTimerText() => countdownText.text = remainingTime.ToString("0.0");

    /// <summary>
    /// Enable or disable visibility on timer's text based on input
    /// </summary>
    /// <param name="_x">true = show timer, false = hide timer</param>
    void ShowCountdown(bool _x) => countdownText.enabled = _x;
}
