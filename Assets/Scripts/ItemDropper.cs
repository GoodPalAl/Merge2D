
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    // Variables editable in Unity
    // TODO : Move "test mode" into its own script
    [SerializeField]
    bool testMode;
    [SerializeField]
    GameManager.Fruit testModeStartFruit = GameManager.Fruit.Strawberry;
    [SerializeField]
    GameManager.Fruit maxFruitSpawn = GameManager.Fruit.Banana;

    // Public fields
    /// <summary>
    /// Next fruit in queue
    /// </summary>
    public static GameObject QueuedFruit;
    public static Sprite GetNextFruitSprite() => QueuedFruit.GetComponentInChildren<SpriteRenderer>().sprite;
    public static GameObject GetNextFruit() => QueuedFruit;

    // Parent that new fruits will be put in.
    Transform Parent;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        Parent = transform;
        QueuedFruit = GameManager.Instance.GetFruit(testMode ? GameManager.Instance.GetFruitIndexFromEnum(testModeStartFruit) : 0);
    }
        
    private void FixedUpdate()
    {
        ClickEvent();

        // If the space bar is pressed, clear the board of all dropped fruits.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ClearBoard();
        }
    }

    public float TimeLastClick = 0f;
    private void ClickEvent()
    {
        // Click delay should happen slightly after the cursor is revealed
        float ClickDelay = CursorManager.Instance.GetCursorDelay() + 0.1f;

        // Click delay is applied
        // This prevents from multiple items spawning at the same time.
        if (TimeLastClick < ClickDelay)
        {
            TimeLastClick += Time.deltaTime;
        }
        // If left-mouse button is pressed and delay has passed, drop the item.
        if (Input.GetMouseButtonDown(0) && TimeLastClick >= ClickDelay)
        {
            Debug.Log("CLICK");
            DropFruit();
            // Reset timer
            TimeLastClick = 0;
        }
    }

    // Created local function to utilize the invoke function.
    void ShowCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Toggles cursor visibility and calls for a new fruit to spawn.
    /// </summary>
    void DropFruit()
    {
        // Hide cursor.
        CursorManager.Instance.HideCursor();

        // Spawn a new item.
        SpawnFruit();

        // Invoke show cursor so there is a small delay.
        Invoke(nameof(ShowCursor), CursorManager.Instance.GetCursorDelay());

        // Queue next fruit
        // If TestMode enabled, the first fruit in hierarchy will always load, otherwise the fruit will be random.
        int index;
        if (testMode)
        {
            index = GameManager.Instance.GetFruitIndexFromEnum(testModeStartFruit);
        }
        else
        {
            index = Random.Range(0, GameManager.Instance.GetFruitIndexFromEnum(maxFruitSpawn));
        }
        QueuedFruit = GameManager.Instance.GetFruit(index);
    }

    /// <summary>
    /// Spawns a new fruit object at cursor's position
    /// </summary>
    private void SpawnFruit()
    {
        // Subtract a bit of y-axis position so the fruit is dropped below trigger box.
        Vector3 pos = CursorManager.Instance.GetCursor().transform.position + new Vector3(0f,-0.5f,0f);

        // New item spawns where cursor is located.
        GameObject newFruit = Instantiate(QueuedFruit, pos, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        newFruit.name = QueuedFruit.name + (GameManager.DroppedFruit.Count + 1).ToString();

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(Parent.transform);

        // Add item to an array to manage it.
        GameManager.DroppedFruit.Add(newFruit);
    }
}
