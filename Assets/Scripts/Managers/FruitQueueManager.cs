using System.Collections.Generic;
using UnityEngine;
using static GameUtility.Enums;

public class FruitQueueManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static FruitQueueManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion

    /// <summary>
    /// Allows for FruitHierarchy to be easily loaded in code.
    /// </summary>
    [SerializeField]
    FruitAssetPack fruitAssetPack;
    /// <summary>
    /// Official merge order of fruit. Initialized in Unity Editor
    /// </summary>
    [SerializeField]
    List<GameObject> FruitHierarchy = new();

    private void Start()
    {
        if (fruitAssetPack != null && fruitAssetPack.FruitHierarchy.Count != 0)
        {
            FruitHierarchy.Clear();
            foreach (var fruit in fruitAssetPack.FruitHierarchy)
            {
                FruitHierarchy.Add(fruit);
            }
        }
        // Prints the fruitHierarchy in debug.
        // If this prints as 0, then list is not preloaded in Unity Inspector.
        PrintList();
    }

    void PrintList()
    {
        string str = "(List) Prefab Fruits: ";
        foreach (var fruit in FruitHierarchy)
        {
            // check if fruit is null
            if (fruit != null)
            {
                str += fruit.name;
                if (fruit != FruitHierarchy[^1])
                {
                    str += ", ";
                }
            }
        }
        Debug.Log(str);
        Debug.Log("Number of Fruits Dropped: "
            + FruitHierarchy.Count.ToString());
    }

    [SerializeField]
    Fruits testModeStartFruit = Fruits.Strawberry;
    [SerializeField]
    Fruits maxFruitSpawn = Fruits.Banana;
    
    /// <summary>
    /// Indicates what fruit is next to be dropped. Should also be shown as Cursor.
    /// </summary>
    GameObject QuededFruit;


    public GameObject GetFirstFruit(bool _isDebugOn)
    {
        int index = _isDebugOn ? GetFruitIndexFromEnum(testModeStartFruit) : 0;

        QuededFruit = GetFruitFromList(index); 

        return QuededFruit;
    }

    public int GetFruitIndexFromEnum(Fruits _fruit) 
        => FruitHierarchy.FindIndex(x => x.CompareTag(_fruit.ToString()));

    public GameObject GetFruitFromList(int _order) => FruitHierarchy[_order];

    public GameObject GetNextFruit(string _oldFruitName) 
    {
        int nextIndex = FruitHierarchy.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        
        return GetFruitFromList(nextIndex);
    }

    public int FruitCount() => FruitHierarchy.Count;

    public GameObject GetQueuedFruit() => QuededFruit;

    public void UpdateQuededFruit(bool _debugOn)
    {
        // If TestMode enabled,
        // the first fruit in hierarchy will always load,
        // otherwise the fruit will be random.
        int index = _debugOn ?
            GetFruitIndexFromEnum(testModeStartFruit) :
            Random.Range(0, GetFruitIndexFromEnum(maxFruitSpawn));

        QuededFruit = GetFruitFromList(index);
    }
}
