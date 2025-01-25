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

    public UnityEvent e_FruitsMerged;

    private void Start()
    {
        // Initializing variables
        id = GetInstanceID();
        trigger = GetComponent<PolygonCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        fruitCollector = GameObject.FindGameObjectWithTag("ItemDropController").transform;

        // Initializing events and listeners
        var gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            var listener = gameControllerObject.GetComponent<ScoreManager>();
            e_FruitsMerged.AddListener(delegate 
            {
                listener.TickScore();
            });
            e_FruitsMerged.AddListener(delegate 
            {
                listener.PrintScoreToDebug();
            }); 
        }
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

    void MergeFruits(string _oldTag, GameObject _thisFruit, GameObject _otherFruit)
    {
        GameObject newFruit;
        try
        {
            newFruit = FruitManager.Instance.GetFruitNextInOrder(_oldTag);
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
        e_FruitsMerged?.Invoke();

        // Remove merged fruits from board.
        //FruitManager.DestroyAndRemoveFruit(_thisFruit);
        //FruitManager.DestroyAndRemoveFruit(_otherFruit);
        Destroy(_thisFruit);
        Destroy(_otherFruit);

        // Add the next fruit in order as long as it wasn't the last.
        // This should have been handled in the try-catch.
        if (newFruit != null)
        {

            // Update child's name based on # of fruit in the board.
            newFruit.name = newFruit.name 
                + (FruitManager.GetDroppedFruitCount() + 1).ToString() 
                + "(Merged)";

            Instantiate(newFruit, transform.position, Quaternion.identity, fruitCollector);
            FruitManager.AddFruitToBoard(newFruit);
        }
    }
}