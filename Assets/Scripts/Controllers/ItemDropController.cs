using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    /// <summary>
    /// Next fruit in queue
    /// </summary>
    //GameObject QueuedFruit;
    // Parent that new fruits will be put in.
    Transform _parent;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        _parent = transform;
        //QueuedFruit = FruitManager.Instance.GetFirstFruit(debugMode());
    }

    bool DebugMode() => ItemDropManager.Instance.IsDebugEnabled();

    private void FixedUpdate()
    {
        // If the space bar is pressed, clear the board of all dropped fruits.
        if (DebugMode() && Input.GetKeyDown(KeyCode.Space))
        {
            FruitManager.Instance.ClearBoard();
        }
    }


    // Created local function to utilize the invoke function.
    void ShowCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Toggles cursor visibility and calls for a new fruit to spawn.
    /// </summary>
    public void DropFruit()
    {
        // Hide cursor.
        CursorManager.Instance.HideCursor();

        // Spawn a new item.
        SpawnFruit();

        // Invoke show cursor so there is a small delay.
        Invoke(nameof(ShowCursor), CursorManager.Instance.GetCursorDelay());

        // Queue next fruit
        //QueuedFruit = FruitManager.Instance.GetQueuedFruit(debugMode());
    }

    /// <summary>
    /// Spawns a new fruit object at cursor's position
    /// </summary>
    private void SpawnFruit()
    {
        // Subtract a bit of y-axis position so the fruit is dropped below trigger box.
        Vector3 pos = CursorManager.Instance.GetCursor().transform.position + new Vector3(0f,-0.5f,0f);
        GameObject queuedFruit = FruitManager.Instance.GetQueuedFruit();
        
        // New item spawns where cursor is located.
        GameObject newFruit = Instantiate(queuedFruit, pos, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        newFruit.name = queuedFruit.name + (FruitManager.DroppedFruit.Count + 1).ToString();

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(_parent.transform);

        // Add item to an array to manage it.
        FruitManager.DroppedFruit.Add(newFruit);
    }
}
