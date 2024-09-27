using UnityEngine;



/// <summary>
/// This doesnt work atm. Wanted to code prefabs for each fruit so i didnt have to manually do it in unity each time.
/// </summary>
//[RequireComponent(typeof(PolygonCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
public class FruitItem : MonoBehaviour
{
    protected PolygonCollider2D Trigger;
    protected Rigidbody2D Rb;
    int ID;

    /*
    public FruitItem(int _ID)
    {
        ID = _ID;
        Trigger = null;
        Rb = null;
    }
    //*/

}

public class Apple : FruitItem
{
    /*
    public Apple(int _ID, PolygonCollider2D _Trigger, Rigidbody2D _Rb) : base(_ID)
    {
        Trigger = _Trigger;
        Rb = _Rb;
    }
    //*/
}
