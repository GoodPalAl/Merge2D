using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
