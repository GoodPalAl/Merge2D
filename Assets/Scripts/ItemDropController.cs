using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    // Variables editable in Unity
    // TODO : Move "test mode" into its own script
    [SerializeField]
    bool testMode;
    [SerializeField]
    FruitManager.Fruit testModeStartFruit = FruitManager.Fruit.Strawberry;
    [SerializeField]
    FruitManager.Fruit maxFruitSpawn = FruitManager.Fruit.Banana;

    // Public fields
    /// <summary>
    /// Next fruit in queue
    /// </summary>
    public static GameObject QueuedFruit;
    public static Sprite GetNextFruitSprite() => QueuedFruit.GetComponentInChildren<SpriteRenderer>().sprite;
    public static GameObject GetNextFruit() => QueuedFruit;

    // Parent that new fruits will be put in.
    Transform parent;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        parent = transform;
        QueuedFruit = FruitManager.Instance.GetFruit(testMode ? FruitManager.Instance.GetFruitIndexFromEnum(testModeStartFruit) : 0);
    }
        
    private void FixedUpdate()
    {
        clickEvent();

        // If the space bar is pressed, clear the board of all dropped fruits.
        if (testMode && Input.GetKeyDown(KeyCode.Space))
        {
            FruitManager.Instance.ClearBoard();
        }
    }

    // TODO: Use UnityEvents instead of Time.deltaTime
    float timeLastClick = 0f;
    private void clickEvent()
    {
        // Click delay should happen slightly after the cursor is revealed
        float ClickDelay = CursorManager.Instance.GetCursorDelay() + 0.1f;

        // Click delay is applied
        // This prevents from multiple items spawning at the same time.
        if (timeLastClick < ClickDelay)
        {
            timeLastClick += Time.deltaTime;
        }
        // If left-mouse button is pressed and delay has passed, drop the item.
        if (Input.GetMouseButtonDown(0) && timeLastClick >= ClickDelay)
        {
            Debug.Log("CLICK");
            dropFruit();
            // Reset timer
            timeLastClick = 0;
        }
    }

    // Created local function to utilize the invoke function.
    void showCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Toggles cursor visibility and calls for a new fruit to spawn.
    /// </summary>
    void dropFruit()
    {
        // Hide cursor.
        CursorManager.Instance.HideCursor();

        // Spawn a new item.
        spawnFruit();

        // Invoke show cursor so there is a small delay.
        Invoke(nameof(showCursor), CursorManager.Instance.GetCursorDelay());

        // Queue next fruit
        // If TestMode enabled, the first fruit in hierarchy will always load, otherwise the fruit will be random.
        int index;
        if (testMode)
        {
            index = FruitManager.Instance.GetFruitIndexFromEnum(testModeStartFruit);
        }
        else
        {
            index = Random.Range(0, FruitManager.Instance.GetFruitIndexFromEnum(maxFruitSpawn));
        }
        QueuedFruit = FruitManager.Instance.GetFruit(index);
    }

    /// <summary>
    /// Spawns a new fruit object at cursor's position
    /// </summary>
    private void spawnFruit()
    {
        // Subtract a bit of y-axis position so the fruit is dropped below trigger box.
        Vector3 pos = CursorManager.Instance.GetCursor().transform.position + new Vector3(0f,-0.5f,0f);

        // New item spawns where cursor is located.
        GameObject newFruit = Instantiate(QueuedFruit, pos, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        newFruit.name = QueuedFruit.name + (FruitManager.DroppedFruit.Count + 1).ToString();

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(parent.transform);

        // Add item to an array to manage it.
        FruitManager.DroppedFruit.Add(newFruit);
    }
}
