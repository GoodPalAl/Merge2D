using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public List<Collider2D> FruitsInDeadZone = new();
    //public float TimerInDeadZone = 0f;

    // TODO: When the player clicks, the timer should either pause or reset to account for "dropping" fruit
    public void ClickEvent()
    {

    }

    // FIXME: Triggers twice
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision != null)
        {
            Debug.Log("Entered Dead Zone");

            if (FruitsInDeadZone.FindAll(x => x == _collision).Count == 0)
            {
                FruitsInDeadZone.Add(_collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision != null)
        {
            GameManager.Instance.TickDeathTimer();
            Debug.Log("Timer: " + GameManager.Instance.GetDeathTimerAsString());
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision != null && FruitsInDeadZone.FindAll(x => x == _collision).Count != 0)
        {
            Debug.Log("Leaving Dead Zone");

            FruitsInDeadZone.Remove(_collision);

            GameManager.Instance.ResetDeathTimer();
        }
    }
}
