using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour
{
    PolygonCollider2D Trigger;
    Rigidbody2D Rb;

    int ID;

    private void Start()
    {
        ID = GetInstanceID();
        Trigger = GetComponent<PolygonCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Fruit>() != null && Trigger.CompareTag(other.tag)) //&& Trigger.CompareTag("Apple"))
        {
            Debug.Log(Trigger.tag + ":" + ID + " --> " + other.tag + ":" + other.gameObject.GetComponent<Fruit>().ID);

            // Ensures this trigger is only called once when event happens.
            if (ID < other.gameObject.GetComponent<Fruit>().ID)
            {
                MergeFruits(tag, gameObject, other.gameObject);
            }
        }
    }

    // FIXME: game doesnt make last fruit merge vanish.
    void MergeFruits(string oldName, GameObject thisFruit, GameObject otherFruit)
    {
        GameObject newFruit;
        try
        {
            newFruit = GameManager.Instance.GetNextFruit(oldName);

            Debug.Log(thisFruit.tag  + ":" + thisFruit.GetInstanceID()  + " + "
                    + otherFruit.tag + ":" + otherFruit.GetInstanceID() + " = " 
                    + newFruit.tag   + ":" + newFruit.GetInstanceID());
        }
        catch (ArgumentOutOfRangeException)
        {
            newFruit = null;
            Debug.Log(thisFruit.tag + ":" + thisFruit.GetInstanceID() + " + "
                    + otherFruit.tag + ":" + otherFruit.GetInstanceID() + " = "
                    + "POOF last fruit haha");
        }

        Destroy(thisFruit);
        Destroy(otherFruit);

        if (newFruit != null)
        {
            Instantiate(newFruit, transform.position, Quaternion.identity);
        }

        // Increase score
        GameManager.Instance.TickScore();
        Debug.Log("SCORE: " + GameManager.Instance.GetScoreAsString());
    }
}