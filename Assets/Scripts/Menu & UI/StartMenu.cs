using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    GameObject m_StartMenu = null;

    private void Start()
    {
        // Tests to ensure that game objects are assigned in engine.
        try
        {
            ShowMenuUI();
        } 
        catch (UnassignedReferenceException e)
        {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// Sequence of events when start button is pressed. 
    /// </summary>
    public void OnStartButtonClick()
    {
        GameStateManager.Instance.ChangeStateToRunning();
        HideMenuUI();
    }

    /// <summary>
    /// Sets game object that holds all the menu UI components to active.
    /// </summary>
    void ShowMenuUI() => m_StartMenu.SetActive(true);

    /// <summary>
    /// Sets game object that holds all the menu UI components to inactive.
    /// </summary>
    void HideMenuUI() => m_StartMenu.SetActive(false);
}
