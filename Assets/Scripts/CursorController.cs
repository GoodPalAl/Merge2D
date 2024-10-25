using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Radius of fruit
    [SerializeField]
    float FruitRadius = 0.5f;

    private void Update()
    {
        FollowMouse();

        UpdateCursor();
    }

    
    void UpdateCursor()
    {
        //// Updates cursor image as a sprite
        //Sprite NewCursorSprite = ItemDropper.GetNextFruitSprite();
        //CursorManager.Instance.UpdateCursorSprite(NewCursorSprite);

        // Updates cursor image with sprite and transform scale from prefab!!!
        GameObject NewCursor = ItemDropper.GetNextFruit();
        CursorManager.Instance.UpdateCursor(NewCursor);
    }
    void ShowCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        Vector3 WorldPos = CursorManager.GetCursorInWorldPosition();
        float MapRight = GameManager.Constants.MapBorderRight;
        float MapLeft = GameManager.Constants.MapBorderLeft;
        float Height = GameManager.Constants.StartHeight;

        // Keep item within border of map.
        // Right border
        if (WorldPos.x > MapRight - FruitRadius) WorldPos.x = MapRight - FruitRadius;
        // Left border
        if (WorldPos.x < MapLeft + FruitRadius) WorldPos.x = MapLeft + FruitRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        Vector3 CursorPosition = new(WorldPos.x, Height, Camera.main.nearClipPlane);
        CursorManager.Instance.UpdateCursorPosition(CursorPosition);
    }
}
