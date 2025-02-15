using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitAssetPack", menuName = "MyGame/FruitAssetPack")]
public class FruitAssetPack : ScriptableObject
{
    [SerializeField]
    public List<GameObject> FruitHierarchy;
}
