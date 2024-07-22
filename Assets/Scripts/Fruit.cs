using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D Trigger;

    int ID;

    private void Start()
    {
        ID = GetInstanceID();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Fruit>() != null && Trigger.CompareTag(other.tag)) //&& Trigger.CompareTag("Apple"))
        {
            Debug.Log(Trigger.tag);

            // Ensures this trigger is only called once.
            if (ID < other.gameObject.GetComponent<Fruit>().ID)
            {
                MergeFruits(tag, gameObject, other.gameObject);
            }
        }

    }

    void MergeFruits(string oldName, GameObject thisFruit, GameObject otherFruit)
    {
        GameObject newFruit = GameManager.Instance.GetNextFruit(oldName);
        Destroy(thisFruit);
        Destroy(otherFruit);
        Instantiate(newFruit, transform.position, Quaternion.identity);
    }
}
