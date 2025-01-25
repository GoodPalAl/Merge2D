using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour // Scriptable object?
{
    PolygonCollider2D trigger;
    Rigidbody2D rigidBody;
    int id;
    Transform fruitCollector;

    public UnityEvent fruitsMerged;

    private void Start()
    {
        // Initializing variables
        id = GetInstanceID();
        trigger = GetComponent<PolygonCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        fruitCollector = GameObject.FindGameObjectWithTag("ItemDropController").transform;

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
            //Debug.Log(_trigger.tag + ":" + _iD + " --> " + _other.tag + ":" + _other.gameObject.GetComponent<Fruit>()._iD);

            // Ensures this trigger is only called once when event happens.
            if (id < _other.gameObject.GetComponent<Fruit>().id)
            {
                MergeFruits(tag, gameObject, _other.gameObject);
            }
        }
    }

    void MergeFruits(string _oldName, GameObject _thisFruit, GameObject _otherFruit)
    {
        GameObject newFruit;
        try
        {
            newFruit = FruitManager.Instance.GetNextFruit(_oldName);
            /*
            Debug.Log(_thisFruit.tag  + ":" + _thisFruit.GetInstanceID()  + " + "
                    + _otherFruit.tag + ":" + _otherFruit.GetInstanceID() + " = " 
                    + newFruit.tag   + ":" + newFruit.GetInstanceID());
            //*/
        }
        catch (ArgumentOutOfRangeException)
        {
            newFruit = null;
            /*
            Debug.Log(_thisFruit.tag + ":" + _thisFruit.GetInstanceID() + " + "
                    + _otherFruit.tag + ":" + _otherFruit.GetInstanceID() + " = "
                    + "POOF last fruit haha");
            //*/
        }

        // Invoke event
        fruitsMerged?.Invoke();

        // Remove merged fruits from board.
        FruitManager.DeactivateFruit(_thisFruit);
        FruitManager.DeactivateFruit(_otherFruit);

        if (newFruit != null)
        {
            Instantiate(newFruit, transform.position, Quaternion.identity, fruitCollector);
            FruitManager.AddFruitToBoard(newFruit);
        }
    }
}