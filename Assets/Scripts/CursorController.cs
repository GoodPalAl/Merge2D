using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Radius of fruit
    [SerializeField]
    float FruitRadius = 0.1f;

    private void Update()
    {
        FollowMouse();
        //LoadNextFruitIntoCursor();
    }

    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        Vector3 WorldPos = GameManager.GetCursorInWorldPosition();

        // Keep item within border of map.
        // Right border
        if (WorldPos.x > GameManager.Constants.MapBorderRight - FruitRadius) WorldPos.x = GameManager.Constants.MapBorderRight - FruitRadius;
        // Left border
        if (WorldPos.x < GameManager.Constants.MapBorderLeft + FruitRadius) WorldPos.x = GameManager.Constants.MapBorderLeft + FruitRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        Vector3 CursorPosition = new Vector3(WorldPos.x, GameManager.Constants.StartHeight, Camera.main.nearClipPlane);
        GameManager.Instance.Cursor.transform.position = CursorPosition;
    }
}
