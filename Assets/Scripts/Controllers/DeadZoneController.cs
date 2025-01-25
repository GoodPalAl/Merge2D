using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadZoneController : MonoBehaviour
{
    public List<Collider2D> FruitsInDeadZone = new();

    public UnityEvent e_FruitInDeadZone;
    public UnityEvent e_DeadZoneEmpty;

    private void Start()
    {
        var countdownObject = GameObject.FindGameObjectWithTag("CountdownUntilDeath");
        if (countdownObject != null)
        {
            var listener = countdownObject.GetComponent<CountdownController>();
            
            e_FruitInDeadZone.AddListener(delegate
            {
                listener.TriggerStartCountDown();
            });
            e_DeadZoneEmpty.AddListener(delegate
            {
                listener.TriggerStopCountDown();
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (firstTrigger(_collision))
        {
            //Debug.Log("Entered Dead Zone.");

            if (FruitsInDeadZone.FindAll(x => x == _collision).Count == 0)
            {
                FruitsInDeadZone.Add(_collision);
            }
            if (_collision.GetInstanceID() < GetInstanceID())
            {
                e_FruitInDeadZone?.Invoke();
            }
        }
    }
    

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (firstTrigger(_collision))
        {
            if (_collision.GetInstanceID() < GetInstanceID())
            {
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (firstTrigger(_collision))
        {
            if (FruitsInDeadZone.FindAll(x => x == _collision).Count != 0)
            {
                Debug.Log("Leaving Dead Zone.");
                FruitsInDeadZone.Remove(_collision);
            }
            if (FruitsInDeadZone.Count <= 0)
            {
                Debug.Log("Dead Zone Empty. Resetting timer...");
                e_DeadZoneEmpty?.Invoke();
            }
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
