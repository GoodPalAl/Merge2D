using NUnit.Framework.Internal;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class CursorTimerController : MonoBehaviour
{
    TextMeshProUGUI timerText;

    float timerSpeed = 0.1f;
    float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        ShowTimer(true);
    }

    public void ClickTriggered(float delay)
    {
        remainingTime = delay;
        UpdateTimerText();
        StartCoroutine(TimerCountdown());
    }
    IEnumerator TimerCountdown()
    {
        ShowTimer(true);
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(timerSpeed);
            remainingTime -= timerSpeed;
            UpdateTimerText();
        }
        ShowTimer(false);
        Debug.Log("Cursor Countdown Finished");
    }

    void UpdateTimerText() => timerText.text = remainingTime.ToString("0.0");

    /// <summary>
    /// Enable or disable visibility on timer's text based on input
    /// </summary>
    /// <param name="_x">true = show timer, false = hide timer</param>
    void ShowTimer(bool _x) => timerText.enabled = _x;
}
