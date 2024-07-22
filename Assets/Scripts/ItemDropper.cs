using System.Linq;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    // Next fruit in queue
    public static GameObject QueuedFruit;
    public static Sprite GetNextFruitSprite() => QueuedFruit.GetComponentInChildren<SpriteRenderer>().sprite;

    // Flag that indicates when a fruit is dropping.
    public bool Dropping = false;

    // Parent that new fruits will be put in.
    Transform Parent;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        Parent = transform;
        QueuedFruit = GameManager.Instance.GetFruit(0);
    }
        
    private void FixedUpdate()
    {
        // If the item is being held and the left-mouse button is pressed, drop the item.
        if (!Dropping && Input.GetMouseButtonDown(0))
        {
            DropFruit();
            LoadNextFruit();
        }
        // If the right-mouse button is pressed, clear the board of all dropped fruits.
        if (Input.GetMouseButtonDown(1))
        {
            // Reset flag.
            Dropping = false;
            GameManager.Instance.ClearBoard();
        }
    }


    void LoadNextFruit()
    {
        int index = 0; //Random.Range(0, GameManager.Instance.FruitCount());
        QueuedFruit = GameManager.Instance.GetFruit(index);
    }


    /// <summary>
    /// Toggles cursor visibility and calls for a new fruit to spawn.
    /// </summary>
    void DropFruit()
    {
        // Flag that the item is being dropped.
        Dropping = true;

        // Hide cursor.
        CursorManager.Instance.HideCursor();

        // Spawn a new item.
        SpawnFruit();

        // Invoke show cursor so there is a small delay.
        Invoke(nameof(ShowCursor), CursorManager.Instance.GetShowCursorDelay());

        // Reset flag to indicate item is no longer being dropped.
        Dropping = false;
    }

    void ShowCursor() => CursorManager.Instance.ShowCursor();

    // FIXME: two fruits spawn at the beginning of the game for some reason.
    /// <summary>
    /// Spawns a new item loaded in from NextItem.
    /// </summary>
    private void SpawnFruit()
    {
        // New item spawns where cursor is located.
        GameObject newFruit = Instantiate(QueuedFruit, CursorManager.Instance.GetCursor().transform.position, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        newFruit.name = "Fruit" + (GameManager.DroppedFruit.Count + 1).ToString();

        // Apply gravity

        // Assign a parent to the new fruit.
        newFruit.transform.SetParent(Parent.transform);

        // Add item to an array to manage it.
        GameManager.DroppedFruit.Add(newFruit);
    }
}
