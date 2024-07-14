using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    float DropHeight = 4f;
    [SerializeField]
    float MapBorderLeft = -2.75f;
    [SerializeField]
    float MapBorderRight = 2.75f;
    [SerializeField]
    float ItemRadius = 0.1f;

    public GameObject ItemPrefab;
    public GameObject HeldItem;

    Transform Parent;

    static List<GameObject> DroppedItems = new();

    Vector3 DropPosition;
    bool Dropping = false;

    private void Start()
    {
        Parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dropping)
        {
            FollowMouse();
        }
    }

    private void FixedUpdate()
    {
        // If the item is being held and the left-mouse button is pressed, drop the item.
        if (!Dropping && Input.GetMouseButtonDown(0))
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
        Vector3 WorldPos = GameManager.GetCursorInWorldPosition();

        // Keep item in border of map.
        if (WorldPos.x > MapBorderRight - ItemRadius) WorldPos.x = MapBorderRight - ItemRadius;
        if (WorldPos.x < MapBorderLeft + ItemRadius) WorldPos.x = MapBorderLeft + ItemRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        DropPosition = new Vector3(WorldPos.x, DropHeight, Camera.main.nearClipPlane);

        // Freeze rotation on z-axis.
        HeldItem.transform.position = DropPosition;
        HeldItem.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void DropItem()
    {
        // Flag that the item is being dropped.
        Dropping = true;

        // Unfreeze rotation on z-axis.
        ItemPrefab.GetComponent<Rigidbody2D>().freezeRotation = false;

        // Spawn a new item a half-second afterwards
        Invoke(nameof(SpawnItem), 0.5f);

        // Reset flag to indicate item is no longer being dropped.
        Dropping = false;
    }

    private void SpawnItem()
    {
        GameObject child = Instantiate(ItemPrefab, Parent.position, Quaternion.identity);
        child.name = "Item" + (DroppedItems.Count + 1).ToString();
        DroppedItems.Add(child);
        HeldItem = child;
    }


    private void Restart()
    {
        // Flag that the item was "picked back up".
        Dropping = false;
        DroppedItems.Clear();
        FollowMouse();
    }
}
