using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    // Height cursor is locked at
    const float StartHeight = 4f;
    // Width of the board
    const float BoardSize = 5.5f;
    // Position of board's left border from center 
    float MapBorderLeft = -(BoardSize / 2);
    // Position of board's right border from center 
    float MapBorderRight = (BoardSize / 2);
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
        int index = Random.Range(0, 2);
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
