using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour // Scriptable object?
{
    PolygonCollider2D trigger;
    Rigidbody2D rb;
    int id;

    public UnityEvent fruitsMerged;

    private void Start()
    {
        // Initializing variables
        id = GetInstanceID();
        trigger = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // Initializing events and listeners
        fruitsMerged.AddListener(
            GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<ScoreManager>().TickScore
            );
        fruitsMerged.AddListener(
            GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<ScoreManager>().PrintScoreToDebug
            );
    }

    private void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.GetComponent<Fruit>() != null && trigger.CompareTag(_other.tag))
        {
            Debug.Log(trigger.tag + ":" + id + " --> " + _other.tag + ":" + _other.gameObject.GetComponent<Fruit>().id);

            // Ensures this trigger is only called once when event happens.
            if (id < _other.gameObject.GetComponent<Fruit>().id)
            {
                mergeFruits(tag, gameObject, _other.gameObject);
            }
        }
    }

    void mergeFruits(string _oldName, GameObject _thisFruit, GameObject _otherFruit)
    {
        GameObject newFruit;
        try
        {
            newFruit = FruitManager.Instance.GetNextFruit(_oldName);

            Debug.Log(_thisFruit.tag  + ":" + _thisFruit.GetInstanceID()  + " + "
                    + _otherFruit.tag + ":" + _otherFruit.GetInstanceID() + " = " 
                    + newFruit.tag   + ":" + newFruit.GetInstanceID());
        }
        catch (ArgumentOutOfRangeException)
        {
            newFruit = null;
            Debug.Log(_thisFruit.tag + ":" + _thisFruit.GetInstanceID() + " + "
                    + _otherFruit.tag + ":" + _otherFruit.GetInstanceID() + " = "
                    + "POOF last fruit haha");
        }

        // Invoke event
        fruitsMerged.Invoke();

        Destroy(_thisFruit);
        Destroy(_otherFruit);

        if (newFruit != null)
        {
            Instantiate(newFruit, transform.position, Quaternion.identity);
        }
    }

    public void TestMethod()
    {
        Debug.Log("Score!");
    }
}