using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion
    
    /// <summary>
    /// Score of Current Game
    /// </summary>    
    int score = 0;

    /// <summary>
    /// Increment score by 1. 
    /// TODO: increment score based on fruit hierarchy.
    /// </summary>
    public void TickScore() => score++;

    /// <summary>
    /// Sets Score equal to 0
    /// </summary>
    public void ResetScore() => score = 0;

    /// <summary>
    /// Gets current score of game as a string.
    /// </summary>
    /// <returns>String of score</returns>
    public string GetScore() => score.ToString();
}
