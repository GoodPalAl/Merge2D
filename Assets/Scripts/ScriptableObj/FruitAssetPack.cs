using System.Collections.Generic;
using UnityEngine;

// TODO: possibly use this to pre-load all the fruits in a list
// so i dont have to manually do it if FruitQueue ever gets edited again.
[CreateAssetMenu(fileName = "FruitAssetPack", menuName = "MyGame/FruitAssetPack")]
public class FruitAssetPack : ScriptableObject
{
    [SerializeField]
    public List<GameObject> FruitHierarchy;
}
