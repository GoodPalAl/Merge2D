using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        HideTimer();
    }

    void HideTimer() => timerText.enabled = false;    
    void ShowTimer() => timerText.enabled = true;

    // Update is called once per frame
    void Update()
    {
        float timer = GameManager.Instance.GetDeathTimer();
        if (timer != 0)
        {
            ShowTimer();
            timerText.text = GameManager.Instance.GetDeathTimerAsString();
        }
        else
        { 
            HideTimer(); 
        }
    }
}
