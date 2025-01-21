using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static ItemDropManager Instance { get; private set; }
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

    // FIXME: make an event that triggers whenever debug is toggled.
    // Variables editable in Unity
    [SerializeField]
    bool DebugMode;
    public bool IsDebugEnabled() => DebugMode;

}
