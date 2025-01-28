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

    GameObject _cursor;
    public GameObject GetCursor() => _cursor;

    // Variables ediable in Unity
    [SerializeField]
    float _showCursorDelay = 0.5f;

    void Start()
    {
        SetCursor();
    }

    void SetCursor()
    {
        // Ensure there is only 1 object tagged as "player"
        var objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length != 1)
        {
            string str_error = "";
            foreach (var t in objs)
            {
                str_error += t.name + "\n";
            }
            Debug.LogError("Multiple 'Players' in Scene. Please fix tags in "
                + objs.Length + " objects: \n"
                + str_error);

            _cursor = null;
            return;
        }
        if (objs[0] == null || objs.Length == 0)
        {
            Debug.LogError("No 'Player' object established. Please fix.");
            _cursor = null;
            return;
        }
        _cursor = objs[0];
    }

    public void UpdateCursorPosition(Vector3 pos) => _cursor.transform.position = pos;
    
    /// <summary>
    /// Uses fruit prefab to load in sprite and transform scale for cursor.
    /// </summary>
    /// <param name="obj">Prefab of queued fruit.</param>
    public void UpdateCursor(GameObject obj)
    {
        Sprite newFruitSprite = obj.GetComponentInChildren<SpriteRenderer>().sprite;
        Vector3 newFruitSize = obj.transform.localScale;

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
    public float GetCursorDelay() => _showCursorDelay;

}
