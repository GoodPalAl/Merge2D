using UnityEngine;

public class DisableGravity : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Disabling gravity for fruits in key. \'" 
            + transform.childCount.ToString() + "\' gravity scales set to 0.");

        //foreach (GameObject obj in FruitInKey)
        foreach (Transform _obj in transform)
        {
            if (_obj.GetComponent<Rigidbody2D>() != null)
            {
                var _rb = _obj.GetComponent<Rigidbody2D>();
                // Keeps fruits from inside of key from moving or falling.
                _rb.gravityScale = 0f;
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
