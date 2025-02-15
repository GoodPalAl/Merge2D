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
        ClickEvent.AddListener(delegate
        {
            UpdateCursor(false);
        });
        // FIXME: move this to a manager so we don't have to do
        // all of this error checking
        if (GameObject.FindGameObjectWithTag("ItemDropController") != null)
        {
            ClickEvent.AddListener(delegate
            {
                GameObject.FindGameObjectWithTag("ItemDropController")
                .GetComponent<ItemDropController>().DropFruit();
            });
        }
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
        if (GameObject.FindGameObjectWithTag("CursorTimer") != null)
        {
            ClickEvent.AddListener(delegate
            {
                GameObject.FindGameObjectWithTag("CursorTimer")
                .GetComponent<CursorTimerController>().ClickTriggered();
            });
        }
    }

    private void Update()
    {
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
