using UnityEngine;

public class CursorFollowController : MonoBehaviour
{
    // Radius of fruit
    // TODO: fetch this from Fruit.cs instead of a hardcoded number
    [SerializeField]
    float fruitRadius = 0.5f;

    private void Update()
    {
        if (GameStateManager.Instance.GetCurrentGameState() == GameUtility.Enums.GameStates.Running)
        {
            FollowMouse();
        }
    }

    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void FollowMouse()
    {
        // Convert mouse position to Unity world position
        Vector3 worldPos = CursorManager.GetCursorInWorldPosition();
        float mapRight = GameUtility.Constants.MAP_BORDER_RIGHT;
        float mapLeft = GameUtility.Constants.MAP_BORDER_LEFT;
        float height = GameUtility.Constants.START_HEIGHT;

        // Keep item within border of map.
        // Right border
        if (worldPos.x > mapRight - fruitRadius) worldPos.x = mapRight - fruitRadius;
        // Left border
        if (worldPos.x < mapLeft + fruitRadius) worldPos.x = mapLeft + fruitRadius;

        // Set item transform to appropriate position:
        // x = x of mouse curser within map boundaries,
        // y = height of map,
        // z = distance of near clipping plane from Camera
        Vector3 cursorPosition = new(worldPos.x, height, Camera.main.nearClipPlane);
        CursorManager.Instance.UpdateCursorPosition(cursorPosition);
    }
}
