using UnityEngine;

public class DisableGravity : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Disabling gravity for fruits in key. \'" 
            + transform.childCount.ToString() + "\' gravity scales set to 0.");

        //foreach (GameObject obj in FruitInKey)
        foreach (Transform obj in transform)
        {
            if (obj.GetComponent<Rigidbody2D>() != null)
            {
                var rb = obj.GetComponent<Rigidbody2D>();
                // Keeps fruits from inside of key from moving or falling.
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
