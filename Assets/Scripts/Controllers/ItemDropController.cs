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

    bool DebugMode => GameStateManager.Instance.IsDebugEnabled();

    private void FixedUpdate()
    {
        // If the space bar is pressed, clear the board of all dropped fruits.
        if (DebugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space bar pressed.");
            FruitManager.Instance.ClearBoard();
        }
    }


    // Created local function to utilize the invoke function.
    void ShowCursor() => CursorManager.Instance.ShowCursor();
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
        newFruit.name = queuedFruit.name + (FruitManager.GetDroppedFruitCount() + 1).ToString();

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(_parent.transform);

        // Add item to an array to manage it.
        FruitManager.AddFruitToBoard(newFruit);

        FruitManager.PrintAllFruitOnBoard();
    }
}
