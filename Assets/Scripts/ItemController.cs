using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Radius of fruit
    [SerializeField]
    float FruitRadius = 0.1f;

    // Next fruit in queue
    public static GameObject NextFruit;
    public static Sprite GetNextFruitSprite() => NextFruit.GetComponentInChildren<SpriteRenderer>().sprite;

    // Parent that new fruits will be put in.
    Transform Parent;

    // Flag that indicates when a fruit is dropping.
    bool Dropping = false;

    private void Start()
    {
        // Initialize this transform as the parent that the new items will spawn in.
        Parent = transform;
        NextFruit = GameManager.Instance.GetFruit(0);
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
        int index = Random.Range(0, GameManager.Instance.FruitCount());
        NextFruit = GameManager.Instance.GetFruit(index);
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

    /// <summary>
    /// Spawns a new item loaded in from NextItem.
    /// </summary>
    private void SpawnFruit()
    {
        // New item spawns where cursor is located.
        GameObject child = Instantiate(NextFruit, CursorManager.Instance.GetCursor().transform.position, Quaternion.identity);

        // Update child's name based on # of fruit in the board.
        child.name = "Fruit" + (GameManager.DroppedFruit.Count + 1).ToString();

        // Assign a parent to the new fruit.
        child.transform.SetParent(Parent.transform);

        // Add item to an array to manage it.
        GameManager.DroppedFruit.Add(child);
    }
}
