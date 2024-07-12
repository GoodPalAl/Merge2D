using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 DropPosition;
    float DropHeight = 4f;
    bool HasBeenDropped = false;

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
        if (!HasBeenDropped && Input.GetMouseButtonDown(0))
        {
            DropItem();
        }
        
    }

    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        // Mouse Position:
        //  <0,0,0> = Bottom-Left of screen
        //  <Screen.width, Screen.height, 0> = Top-Right of screen
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        DropPosition = new Vector3(WorldPos.x, DropHeight, Camera.main.nearClipPlane);


        transform.position = DropPosition;
    }

    void DropItem()
    {
        HasBeenDropped = true;

        // Spawn a new item
    }
}
