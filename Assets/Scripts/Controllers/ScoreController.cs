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
        // Make string empty if game is not running.
        if (!GameStateManager.Instance.IsGameRunning())
        {
            _scoreText.text = string.Empty;
        }
        else
        {
            _scoreText.text = ScoreManager.Instance.GetScore();
        }
    }
}
