using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CursorClickController : MonoBehaviour
{
    public UnityEvent ClickEvent;

    private void Start()
    {
        UpdateCursor(true);
        ClickEvent.AddListener(
            GameObject.FindGameObjectWithTag("ItemDropController")
            .GetComponent<ItemDropController>().DropFruit
            );
        ClickEvent.AddListener(delegate
        {
            GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<FruitManager>()
            .UpdateQuededFruit(ItemDropManager.Instance.IsDebugEnabled());
        });
    }

    private void Update()
    {
        StartCoroutine(nameof(DelayClick));
    }

    bool IsDebugOn => ItemDropManager.Instance.IsDebugEnabled();

    /// <summary>
    /// Coroutine that waits for some seconds 
    /// before allowing player to click.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayClick()
    {
        // Click delay should happen slightly after the cursor is revealed
        float clickDelaySeconds = CursorManager.Instance.GetCursorDelay() + 0.1f;

        yield return new WaitForSeconds(clickDelaySeconds);

        // Now that time has passed, get input.
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }
    private void Click()
    {
        ClickEvent.Invoke();
        UpdateCursor(false);
    }

    /// <summary>
    /// Updates the cursor sprite
    /// </summary>
    /// <param name="_onStart">Is this called in the start function or not</param>
    void UpdateCursor(bool _onStart)
    {
        GameObject newCursor = _onStart ?
            FruitManager.Instance.GetFirstFruit(IsDebugOn) :
            FruitManager.Instance.GetQueuedFruit();

        CursorManager.Instance.UpdateCursor(newCursor);
    }
}
