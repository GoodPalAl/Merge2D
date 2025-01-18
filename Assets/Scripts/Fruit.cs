using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour // Scriptable object?
{
    PolygonCollider2D trigger;
    Rigidbody2D rb;
    int id;


    private void Start()
    {
        id = GetInstanceID();
        trigger = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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

        Destroy(_thisFruit);
        Destroy(_otherFruit);

        if (newFruit != null)
        {
            Instantiate(newFruit, transform.position, Quaternion.identity);
        }

        // Increase score
        ScoreManager.Instance.TickScore();
        Debug.Log("SCORE: " + ScoreManager.Instance.GetScore());
    }
}