using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Fruit : MonoBehaviour
{
    PolygonCollider2D Collider;
    bool isMerging = false;

    private void Start()
    {
        Collider = gameObject.GetComponent<PolygonCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Collider.CompareTag(other.tag))
        {
            Debug.Log(Collider.tag);
            if (Collider.CompareTag("Apple") && isMerging == false)
            {
                MergeFruits(tag, gameObject, other.gameObject);
            }
        }

    }

    // FIXME: spawns 6 oranges instead of 1 :^)
    void MergeFruits(string oldName, GameObject thisFruit, GameObject otherFruit)
    {
        isMerging = true;
        GameObject newFruit = GameManager.Instance.GetFruit(1);
        Destroy(thisFruit);
        Destroy(otherFruit);
        Instantiate(newFruit, transform.position, Quaternion.identity);
        isMerging = false;
    }
}
