using UnityEngine;

public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static CursorManager Instance { get; private set; }
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

    // Private instance variables
    static GameObject cursor;

    // Variables ediable in Unity
    [SerializeField]
    float showCursorDelay = 0.5f;

    void Start()
    {
        if (cursor == null)
            cursor = GameObject.Find("Cursor");
    }


    public GameObject GetCursor() => cursor;
    public void UpdateCursorPosition(Vector3 _pos) => cursor.transform.position = _pos;
    public void UpdateCursorSprite(Sprite _s) 
        => cursor.GetComponentInChildren<SpriteRenderer>().sprite = _s;
    
    /// <summary>
    /// Uses fruit prefab to load in sprite and transform scale for cursor.
    /// </summary>
    /// <param name="_g">Prefab of queued fruit.</param>
    public void UpdateCursor(GameObject _g)
    {
        Sprite newFruitSprite = _g.GetComponentInChildren<SpriteRenderer>().sprite;
        Vector3 newFruitSize = _g.transform.localScale;

        cursor.GetComponentInChildren<SpriteRenderer>().sprite = newFruitSprite;
        cursor.GetComponentInChildren<Transform>().localScale = newFruitSize;
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

    public void HideCursor() => cursor.SetActive(false);
    public void ShowCursor() => cursor.SetActive(true);
    public float GetCursorDelay() => showCursorDelay;

}
