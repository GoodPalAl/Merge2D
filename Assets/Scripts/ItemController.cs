using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Height cursor is locked at
    [SerializeField]
    float StartHeight = 4f;
    // Position of board's left border from center 
    [SerializeField]
    float MapBorderLeft = -2.75f;
    // Position of board's right border from center 
    [SerializeField]
    float MapBorderRight = 2.75f;
    // Radius of fruit
    [SerializeField]
    float FruitRadius = 0.1f;
    // Delay of cursor reveal in seconds
    [SerializeField]
    float ShowCursorDelay = 0.5f;

    // Next fruit in queue
    public GameObject NextFruit;

    // Parent that new fruits will be put in.
    Transform Parent;

    // Flag that indicates when a fruit is dropping.
    bool Dropping = false;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        Parent = transform; 
    }

    // Update is called once per frame
    void Update()
    {
        // Cursor must always follow the mouse x-position.
        // Keep this in Update to avoid strange glitching.
        FollowMouse();
    }

    private void FixedUpdate()
    {
        // If the item is being held and the left-mouse button is pressed, drop the item.
        if (!Dropping && Input.GetMouseButtonDown(0))
        {
            DropFruit();
        }
        // If the right-mouse button is pressed, clear the board of all dropped fruits.
        if (Input.GetMouseButtonDown(1))
        {
            // Reset flag.
            Dropping = false;
            GameManager.Instance.ClearBoard();
        }

    }

    // TODO: Migrate to a "cursor controller"
    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        Vector3 WorldPos = GameManager.GetCursorInWorldPosition();

        // Keep item within border of map.
        // Right border
        if (WorldPos.x > MapBorderRight - FruitRadius) WorldPos.x = MapBorderRight - FruitRadius;
        // Left border
        if (WorldPos.x < MapBorderLeft + FruitRadius) WorldPos.x = MapBorderLeft + FruitRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        Vector3 CursorPosition = new Vector3(WorldPos.x, StartHeight, Camera.main.nearClipPlane);
        GameManager.Instance.Cursor.transform.position = CursorPosition;
    }

    /// <summary>
    /// Toggles cursor visibility and calls for a new fruit to spawn.
    /// </summary>
    void DropFruit()
    {
        // Flag that the item is being dropped.
        Dropping = true;

        // Hide cursor.
        GameManager.Instance.HideCursor();

        // Spawn a new item.
        SpawnFruit();

        // Invoke show cursor so there is a small delay.
        Invoke(nameof(ShowCursor), ShowCursorDelay);

        // Reset flag to indicate item is no longer being dropped.
        Dropping = false;
    }

    /// <summary>
    /// Seperate function so the Invoke() function can be used.
    /// </summary>
    void ShowCursor() => GameManager.Instance.ShowCursor();

    /// <summary>
    /// Spawns a new item loaded in from NextItem.
    /// </summary>
    private void SpawnFruit()
    {
        // New item spawns where cursor is located.
        GameObject child = Instantiate(NextFruit, GameManager.Instance.Cursor.transform.position, Quaternion.identity);
        // Update child's name based on # of fruit in the board.
        child.name = "Fruit" + (GameManager.DroppedFruit.Count + 1).ToString();
        // Assign a parent to the new fruit.
        child.transform.SetParent(Parent.transform);
        // Add item to an array to manage it.
        GameManager.DroppedFruit.Add(child);
    }
}
