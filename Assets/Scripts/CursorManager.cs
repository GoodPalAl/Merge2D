using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Singleton

    public static CursorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this;
    }
    #endregion

    static GameObject _cursor;
    [SerializeField]
    float showCursorDelay = 0.5f;

    private void Start()
    {
        if (_cursor == null)
            _cursor = GameObject.Find("Cursor");
    }


    public GameObject GetCursor() => _cursor;
    public void UpdateCursorPosition(Vector3 pos) => _cursor.transform.position = pos;
    public void UpdateCursorSprite(Sprite s)
    {
        _cursor.GetComponentInChildren<SpriteRenderer>().sprite = s;
    }
    
    /// <summary>
    /// Uses fruit prefab to load in sprite and transform scale for cursor.
    /// </summary>
    /// <param name="g">Prefab of queued fruit.</param>
    public void UpdateCursor(GameObject g)
    {
        Sprite newFruitSprite = g.GetComponentInChildren<SpriteRenderer>().sprite;
        Vector3 newFruitSize = g.transform.localScale;

        _cursor.GetComponentInChildren<SpriteRenderer>().sprite = newFruitSprite;
        _cursor.GetComponentInChildren<Transform>().localScale = newFruitSize;

    }


    /// <summary>
    /// Convert mouse position to Unity world position
    /// Mouse Position:
    ///  <0,0,0> = Bottom-Left of screen
    ///  <Screen.width, Screen.height, 0> = Top-Right of screen
    /// </summary>
    /// <returns>Mouse position in Unity world position vector3</returns>
    public static Vector3 GetCursorInWorldPosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public void HideCursor() => _cursor.SetActive(false);
    public void ShowCursor() => _cursor.SetActive(true);
    public float GetCursorDelay() => showCursorDelay;

}
