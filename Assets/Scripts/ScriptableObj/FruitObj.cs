using UnityEngine;
using static GameUtility.Enums;

[CreateAssetMenu(fileName = "New Fruit", menuName = "Fruit")]
public class FruitObj : ScriptableObject
{
    public Fruits TypeOfFruit;
    public GameObject fruit;


}
