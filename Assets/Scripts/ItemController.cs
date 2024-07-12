using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class ItemController : MonoBehaviour
{
    Vector3 DropPosition;
    bool HasBeenDropped = false;

    [SerializeField]
    float DropHeight = 4f;
    [SerializeField]
    float MapBorderLeft = -2.75f;
    [SerializeField]
    float MapBorderRight = 2.75f;
    [SerializeField]
    float ItemRadius = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (!HasBeenDropped)
        {
            FollowMouse();
        }
    }

    private void FixedUpdate()
    {
        // If the item is being held and the left-mouse button is pressed, drop the item.
        if (!HasBeenDropped && Input.GetMouseButtonDown(0))
        {
            DropItem();
        }
        // If the right-mouse button is pressed, restart the dropped item. DEBUG PURPOSES.
        if (Input.GetMouseButtonDown(1))
        {
            Restart();
        }

    }

    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        // Mouse Position:
        //  <0,0,0> = Bottom-Left of screen
        //  <Screen.width, Screen.height, 0> = Top-Right of screen
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Keep item in border of map.
        if (WorldPos.x > MapBorderRight - ItemRadius) WorldPos.x = MapBorderRight - ItemRadius;
        if (WorldPos.x < MapBorderLeft + ItemRadius) WorldPos.x = MapBorderLeft + ItemRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        DropPosition = new Vector3(WorldPos.x, DropHeight, Camera.main.nearClipPlane);
        transform.position = DropPosition;

        // Freeze rotation on z-axis.
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void DropItem()
    {
        // Flag that the item has been dropped.
        HasBeenDropped = true;
        // Unfreeze rotation on z-axis.
        GetComponent<Rigidbody2D>().freezeRotation = false;

        // Spawn a new item
    }

    private void Restart()
    {
        // Flag that the item was "picked back up".
        HasBeenDropped = false;
        FollowMouse();
    }
}
