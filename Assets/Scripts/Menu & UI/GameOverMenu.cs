using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    GameObject m_GameOverMenu;

    [SerializeField]
    TextMeshProUGUI scoreText;

    private void Start()
    {
        m_GameOverMenu.SetActive(false);
    }
    public void ShowMenuUI()
    {
        m_GameOverMenu.SetActive(true);
        scoreText.text = ScoreManager.Instance.GetScore();
    }
}
