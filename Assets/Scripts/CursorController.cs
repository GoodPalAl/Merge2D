using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Radius of fruit
    [SerializeField]
    float fruitRadius = 0.5f;

    private void Update()
    {
        followMouse();

        updateCursor();
    }

    
    void updateCursor()
    {
        //// Updates cursor image as a sprite
        //Sprite NewCursorSprite = ItemDropper.GetNextFruitSprite();
        //CursorManager.Instance.UpdateCursorSprite(NewCursorSprite);

        // Updates cursor image with sprite and transform scale from prefab!!!
        GameObject _newCursor = ItemDropper.GetNextFruit();
        CursorManager.Instance.UpdateCursor(_newCursor);
    }
    void showCursor() => CursorManager.Instance.ShowCursor();

    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void followMouse()
    {
        // Convert mouse position to Unity world position
        Vector3 WorldPos = CursorManager.GetCursorInWorldPosition();
        float MapRight = GameManager.Constants.MAP_BORDER_RIGHT;
        float MapLeft = GameManager.Constants.MAP_BORDER_LEFT;
        float Height = GameManager.Constants.START_HEIGHT;

        // Keep item within border of map.
        // Right border
        if (WorldPos.x > MapRight - fruitRadius) WorldPos.x = MapRight - fruitRadius;
        // Left border
        if (WorldPos.x < MapLeft + fruitRadius) WorldPos.x = MapLeft + fruitRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        Vector3 CursorPosition = new(WorldPos.x, Height, Camera.main.nearClipPlane);
        CursorManager.Instance.UpdateCursorPosition(CursorPosition);
    }
}
