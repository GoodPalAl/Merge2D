using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGravity : MonoBehaviour
{
    void Start()
    {
        List<GameObject> FruitInKey = new();
        foreach (Transform child in transform) 
        {
            FruitInKey.Add(child.gameObject);
        }

        Debug.Log("Disabling gravity for fruits in key. \'" 
            + FruitInKey.Count.ToString() + "\' gravity scales set to 0.");

        foreach (GameObject obj in FruitInKey)
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
