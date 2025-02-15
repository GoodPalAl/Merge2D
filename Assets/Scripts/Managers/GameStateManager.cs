using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static GameUtility.Enums;

public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static GameStateManager Instance { get; private set; }
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

    // Variables editable in Unity
    [SerializeField]
    bool _debugMode;
    [SerializeField]
    GameStates _gameState = GameStates.Starting;
    public GameStates GetCurrentGameState() => _gameState;

    public bool IsDebugEnabled() => _debugMode;

    void Start()
    {
        ChangeGameState(GameStates.Starting);
    }

    /// <summary>
    /// Switches game state to indicate the game is running.
    /// </summary>
    public void ChangeStateToRunning() => _gameState = GameStates.Running;

    public bool IsGameRunning() => GetCurrentGameState() == GameStates.Running;

    public void ChangeGameState(GameStates gameState) => _gameState = gameState;

    /// <summary>
    /// Resets the current scene.
    /// </summary>
    public void ResetGame() 
    {
        Debug.Log("Resetting Level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Closes application.
    /// </summary>
    public void QuitGame() 
    { 
        Debug.Log("Quitting Game..."); 
        Application.Quit();
    }
}
