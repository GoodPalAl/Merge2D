using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject m_StartMenu = null;

    private void Start()
    {
        try 
        {
            m_StartMenu.SetActive(true); 
        } catch (UnassignedReferenceException e)
        {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// Disables this UI.
    /// </summary>
    public void HideUI()
    {
        m_StartMenu.SetActive(false);
    }
}
