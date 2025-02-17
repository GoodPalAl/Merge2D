using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    // Parent that new fruits will be put in.
    Transform _parent;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        _parent = transform;
    }

    /// <summary>
    /// Fetches if DebugMode is enabled or not
    /// </summary>
    /// <returns>true = debug is on. false = debug is off</returns>
    bool DebugMode() => GameStateManager.Instance.IsDebugEnabled();

    private void FixedUpdate()
    {
        // If the space bar is pressed, clear the board of all dropped fruits.
        if (DebugMode() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space bar pressed.");
            //FruitQueueManager.Instance.ClearBoard();
            GameStateManager.Instance.ResetGame();
        }
    }


    /// <summary>
    /// Created local function to utilize the invoke function.
    /// </summary>
    void ShowCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Coroutine function to show cursor with added delay
    /// NOT IMPLEMENTED YET
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowCursorWithDelay()
    {
        yield return new WaitForSeconds(CursorManager.Instance.GetCursorDelay());
        CursorManager.Instance.ShowCursor();
    }


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
        // TODO: convert to a coroutine
        Invoke(nameof(ShowCursor), CursorManager.Instance.GetCursorDelay());
    }

    Vector3 cursorOffset = new Vector3(0f, -0.5f, 0f);
    /// <summary>
    /// Spawns a new fruit object at cursor's position
    /// </summary>
    private void SpawnFruit()
    {
        // Subtract a bit of y-axis position so the fruit is dropped below trigger box.
        Vector3 pos = CursorManager.Instance.GetCursor().transform.position + cursorOffset;
        GameObject queuedFruit = FruitQueueManager.Instance.GetQueuedFruit();
        
        // New item spawns where cursor is located.
        GameObject newFruit = Instantiate(queuedFruit, pos, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        int nameSuffix = DroppedFruitManager.Instance.GetDroppedFruitCount() + 1;
        newFruit.name = queuedFruit.name + nameSuffix.ToString();

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(_parent.transform);

        // Add item to an array to manage it.
        DroppedFruitManager.Instance.AddFruitToBoard(newFruit);

        // DEBUG: Prints names of fruits after each drop
        DroppedFruitManager.Instance.PrintAllFruit();
    }
}
