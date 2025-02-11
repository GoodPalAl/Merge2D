using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    GameObject m_GameOverMenu = null;

    [SerializeField]
    TextMeshProUGUI scoreText = null;

    private void Start()
    {
        // Tests to ensure that game objects are assigned in engine.
        try
        {
            UpdateScoreText();
            HideMenuUI();
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// Sequence of events when reset button is pressed. 
    /// </summary>
    public void OnResetButtonClick()
    {
        GameStateManager.Instance.ResetGame();
    }

    /// <summary>
    /// Sequence of events when quit button is pressed. 
    /// </summary>
    public void OnQuitButtonClick()
    {
        // TODO: trigger a confirmation menu
        GameStateManager.Instance.QuitGame();
    }

    /// <summary>
    /// Reveals Game Over menu UI and updates the score.
    /// </summary>
    public void TriggerGameOverScreen()
    {
        ShowMenuUI();
        UpdateScoreText();
    }

    /// <summary>
    /// Sets game object that holds all the menu UI components to active.
    /// </summary>
    void ShowMenuUI() => m_GameOverMenu.SetActive(true);

    /// <summary>
    /// Sets game object that holds all the menu UI components to inactive.
    /// </summary>
    void HideMenuUI() => m_GameOverMenu.SetActive(false);

    /// <summary>
    /// Fetches the player's score to show on Game Over screen.
    /// </summary>
    void UpdateScoreText() => scoreText.text = ScoreManager.Instance.GetScore();
}
