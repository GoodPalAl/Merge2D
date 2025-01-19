using UnityEngine;
using UnityEngine.Events;

public class CursorController : MonoBehaviour
{
    // Radius of fruit
    // TODO: fetch this from Fruit.cs instead of a hardcoded number
    [SerializeField]
    float fruitRadius = 0.5f;

    public UnityEvent click;

    private void Start()
    {
        updateCursor(true);
        click.AddListener(
            GameObject.FindGameObjectWithTag("ItemDropController")
            .GetComponent<ItemDropController>().DropFruit
            );
        click.AddListener(
            GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<FruitManager>().UpdateQuededFruit
            );
    }

    private void Update()
    {
        followMouse(); 
        clickEvent();
    }

    bool isDebugOn => ItemDropManager.Instance.IsDebugEnabled();

    // TODO: Use UnityEvents instead of Time.deltaTime
    float timeLastClick = 0f;
    private void clickEvent()
    {
        // Click delay should happen slightly after the cursor is revealed
        float ClickDelay = CursorManager.Instance.GetCursorDelay() + 0.1f;

        // Click delay is applied
        // This prevents from multiple items spawning at the same time.
        if (timeLastClick < ClickDelay)
        {
            timeLastClick += Time.deltaTime;
        }
        // If left-mouse button is pressed and delay has passed, drop the item.
        if (Input.GetMouseButtonDown(0) && timeLastClick >= ClickDelay)
        {
            Debug.Log("TESTING THEORY: " + FruitManager.Instance.FruitOrder.Count + "\n " 
                + FruitManager.Instance.GetQueuedFruit(isDebugOn).name);
            click.Invoke();
            updateCursor(false);
            // Reset timer
            timeLastClick = 0;
        }
    }

    /// <summary>
    /// Updates the cursor sprite
    /// </summary>
    /// <param name="_onStart">Is this called in the start function or not</param>
    void updateCursor(bool _onStart)
    {
        GameObject newCursor = _onStart ?
            FruitManager.Instance.GetFirstFruit(isDebugOn) :
            FruitManager.Instance.GetQueuedFruit(isDebugOn);

        CursorManager.Instance.UpdateCursor(newCursor);
    }

    /// <summary>
    /// Calculates mouse position in the world and has cursor 
    /// follow it within a set boundary.
    /// </summary>
    void followMouse()
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
