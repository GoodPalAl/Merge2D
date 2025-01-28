using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _scoreText.text = ScoreManager.Instance.GetScore();
    }
}
