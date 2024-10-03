using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    // TODO: Timer resets when ONE fruit leaves deadzone. Should reset when there are NO fruit in deadzone.


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
        float timer = GameManager.Instance.GetDeathTimer();
        if (timer >= GameManager.Instance.GetDeathTimeThreshold())
        {
            ShowTimer(true);
            timerText.text = GameManager.Instance.GetDeathTimerAsString();
        }
        else
        {
            ShowTimer(false); 
        }
    }

    /// <summary>
    /// Enable or disable visibility on timer's text based on input
    /// </summary>
    /// <param name="x">true = show timer, false = hide timer</param>
    void ShowTimer(bool x) => timerText.enabled = x;
}
