using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI textGUI;

    private void Start()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textGUI.text = GameManager.Instance.GetScoreAsString();
    }
}
