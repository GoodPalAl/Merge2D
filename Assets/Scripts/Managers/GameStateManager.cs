using UnityEngine;
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
    public bool IsDebugEnabled() => _debugMode;

}
