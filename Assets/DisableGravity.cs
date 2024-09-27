using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGravity : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform) 
        {
            children.Add(child.gameObject);
        }

        Debug.Log("Disabling gravity for fruits in key. \'" + children.Count.ToString() + "\' gravity scales set to 0.");

        foreach (GameObject obj in children)
        {
            if (obj.GetComponent<Rigidbody2D>() != null)
            {
                obj.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
        }
    }
}
