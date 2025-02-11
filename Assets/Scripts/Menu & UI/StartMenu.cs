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
            m_StartMenu.SetActive(true); 
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
    /// Disables this UI.
    /// </summary>
    void HideMenuUI()
    {
        m_StartMenu.SetActive(false);
    }
}
