using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public List<Collider2D> FruitsInDeadZone = new();

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (firstTrigger(_collision))
        {
            //Debug.Log("Entered Dead Zone.");

            if (FruitsInDeadZone.FindAll(x => x == _collision).Count == 0)
            {
                FruitsInDeadZone.Add(_collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (firstTrigger(_collision))
        {
            DeathTimerManager.Instance.TickDeathTimer();
            //TimerManager.Instance.PrintTimerToDebug();
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (firstTrigger(_collision) && FruitsInDeadZone.FindAll(x => x == _collision).Count != 0)
        {
            //Debug.Log("Leaving Dead Zone.");
            FruitsInDeadZone.Remove(_collision);
        }
        if (FruitsInDeadZone.Count <= 0)
        {
            //Debug.Log("Dead Zone Empty. Resetting timer...");
            DeathTimerManager.Instance.ResetDeathTimer();
            //TimerManager.Instance.PrintTimerToDebug();

        }
    }
    /// <summary>
    /// Compares Deadzone tag with fruit collider tag so
    /// they do not trigger each other twice, only once.
    /// </summary>
    /// <param name="_col">Collider entering the deadzone</param>
    /// <returns>True: first trigger</returns>
    bool firstTrigger(Collider2D _col) 
        => _col != null && this.CompareTag(_col.tag);
}
