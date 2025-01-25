using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    // Must be public for this to work
    public UnityEvent e_Click;
    UnityEvent e_SpacePress;

    bool IsDebugOn => GameStateManager.Instance.IsDebugEnabled();

    private void Start()
    {
        UpdateCursor(true);
        AddingListenersForEvent_UserInput();
        AddingListenersForEvent_DevInput();
    }

    /// <summary>
    /// Adding listeners for when the user clicks
    /// </summary>
    void AddingListenersForEvent_UserInput()
    {
        // Use "delegate{}" to allow for parameters to be passed through
        //  Also to make code a little easier to read.
        e_Click.AddListener(delegate
        {
            UpdateCursor(false);
        });
        // Each "if != null" statement checks to make sure that game object exists,
        //  and is attached with the proper tag and component.
        var itemDropObject = GameObject.FindGameObjectWithTag("ItemDropController");
        if (itemDropObject != null)
        {
            var listener = itemDropObject.GetComponent<ItemDropController>();
            e_Click.AddListener(delegate
            {
                listener.DropFruit();
            });
        }
        var gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameManagerObject != null)
        {
            var listener = gameManagerObject.GetComponent<FruitManager>();
            e_Click.AddListener(delegate
            {
                // This may not work due to when IsDebugOn instantiated
                listener.UpdateQuededFruit(IsDebugOn);
            });
        }
        var cursorTimerObject = GameObject.FindGameObjectWithTag("CursorTimer");
        if (cursorTimerObject != null)
        {
            var listener = cursorTimerObject.GetComponent<CursorTimerController>();
            e_Click.AddListener(delegate
            {
                listener.ClickTriggered();
            });
        }
    }

    /// <summary>
    /// Adding listeners for when debug mode is on and user gives game input 
    /// that would normally not trigger any functions.
    /// </summary>
    void AddingListenersForEvent_DevInput()
    {
        var gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameManagerObject != null)
        {
            var listener = gameManagerObject.GetComponent<FruitManager>();
            e_SpacePress.AddListener(delegate
            {
                listener.ClearBoard();
            });
        }
    }

    private void Update()
    {
        WaitForUserInput();
        if (IsDebugOn)
        {
            WaitForDevInput();
        }    
    }

    void WaitForUserInput()
    {
        // Left Mouse
        if (Input.GetMouseButtonDown(0))
        {
            e_Click?.Invoke();
        }
    }
    /******* DEBUG CONTROLS ONLY *******/
    void WaitForDevInput()
    {
        // Space Bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            e_SpacePress?.Invoke();
        }
    }

    /// <summary>
    /// Updates the cursor sprite
    /// </summary>
    /// <param name="calledInStart">Is this called in the start function or not</param>
    void UpdateCursor(bool calledInStart)
    {
        GameObject newCursor = calledInStart ?
            FruitManager.Instance.GetFirstFruit(IsDebugOn) :
            FruitManager.Instance.GetQueuedFruit();

        CursorManager.Instance.UpdateCursor(newCursor);
    }
}
