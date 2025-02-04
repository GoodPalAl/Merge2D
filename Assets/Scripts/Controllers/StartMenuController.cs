using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables this UI.
    /// </summary>
    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
