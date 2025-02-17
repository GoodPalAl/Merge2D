using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CursorClickController : MonoBehaviour
{
    // Must be public for this to work
    public UnityEvent ClickEvent;

    bool IsDebugOn => GameStateManager.Instance.IsDebugEnabled();

    private void Start()
    {
        UpdateCursor(true);

        // Keep these events in this order!
        // ----- CLICK EVENT --------------------------------------------------- //
        // Trigger count down to start
        if (GameObject.FindGameObjectWithTag("CursorTimer") != null)
        {
            ClickEvent.AddListener(delegate
            {
                GameObject.FindGameObjectWithTag("CursorTimer")
                .GetComponent<CursorTimerController>().ClickTriggered();
            });
        }
        // Drop the current fruit being held
        if (GameObject.FindGameObjectWithTag("ItemDropController") != null)
        {
            ClickEvent.AddListener(delegate
            {
                GameObject.FindGameObjectWithTag("ItemDropController")
                .GetComponent<ItemDropController>().DropFruit();
            });
        }
        // Queue the next fruit
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            ClickEvent.AddListener(delegate
            {
                GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<FruitQueueManager>().UpdateQuededFruit(
                    GameStateManager.Instance.IsDebugEnabled()
                );
            });
        }
        // Update the cursor to reflect the new fruit queued
        ClickEvent.AddListener(delegate
        {
            UpdateCursor(false);
        });
    }

    private void Update()
    {
        // Check if the game is running, not paused or ended.
        if (GameStateManager.Instance.IsGameRunning())
        {
            WaitForClick();
        }
    }

    void WaitForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickEvent?.Invoke();
        }
    }

    /// <summary>
    /// Updates the cursor sprite
    /// FIXME: cursor shown is not always the fruit that drops. 
    /// Is the error here or in FruitQueueManager or in DropFruit?
    /// </summary>
    /// <param name="calledInStart">Is this called in the start function or not</param>
    void UpdateCursor(bool calledInStart)
    {
        GameObject newCursor = calledInStart ?
            FruitQueueManager.Instance.GetFirstFruit(IsDebugOn) :
            FruitQueueManager.Instance.GetQueuedFruit();

        CursorManager.Instance.UpdateCursor(newCursor);
    }
}
