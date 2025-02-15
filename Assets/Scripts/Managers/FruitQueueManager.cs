using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
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
    /// Official merge order of fruit. Initialized in Unity Editor
    /// FIXME: make it so i dont have to initialize this list everytime i change FruitQueueManager
    /// </summary>
    [SerializeField]
    List<GameObject> fruitOrder = new();

    private void Start()
    {
        //// LoadAll returns array, not list.
        //// Initialize a sublist then convert each in sublist to GameObject,
        //// then save in fruitOrder.
        //GameObject[] subListObjects = Resources.LoadAll<GameObject>("Fruits");
        //PrintArray(subListObjects);

        //foreach (GameObject subListObj in subListObjects) 
        //{ 
        //    GameObject localObj = (GameObject)subListObj;
        //    fruitOrder.Add(localObj);
        //}
        PrintList();
    }

    void PrintArray(Object[] arr)
    {
        string str = "(Array) Prefab Fruits: ";
        foreach (var fruit in arr)
        {
            // check if fruit is null
            if (fruit != null)
            {
                str += fruit.name;
                if (fruit != arr[^1])
                {
                    str += ", ";
                }
            }
        }
        Debug.Log(str);
        Debug.Log("Number of Fruits Dropped: "
            + arr.Length.ToString());
    }

    void PrintList()
    {
        string str = "(List) Prefab Fruits: ";
        foreach (var fruit in fruitOrder)
        {
            // check if fruit is null
            if (fruit != null)
            {
                str += fruit.name;
                if (fruit != fruitOrder[^1])
                {
                    str += ", ";
                }
            }
        }
        Debug.Log(str);
        Debug.Log("Number of Fruits Dropped: "
            + fruitOrder.Count.ToString());
    }

    [SerializeField]
    Fruits testModeStartFruit = Fruits.Strawberry;
    [SerializeField]
    Fruits maxFruitSpawn = Fruits.Banana;
    
    // Does this need to be static?
    GameObject _quededFruit;


    public GameObject GetFirstFruit(bool _isDebugOn)
    {
        int index = _isDebugOn ? GetFruitIndexFromEnum(testModeStartFruit) : 0;

        _quededFruit = GetFruitFromList(index); 

        return _quededFruit;
    }

    public int GetFruitIndexFromEnum(Fruits _fruit) 
        => fruitOrder.FindIndex(x => x.CompareTag(_fruit.ToString()));

    public GameObject GetFruitFromList(int _order) => fruitOrder[_order];

    public GameObject GetNextFruit(string _oldFruitName) 
    {
        int nextIndex = fruitOrder.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        
        return GetFruitFromList(nextIndex);
    }

    public int FruitCount() => fruitOrder.Count;

    public GameObject GetQueuedFruit() => _quededFruit;

    public void UpdateQuededFruit(bool _debugOn)
    {
        // If TestMode enabled,
        // the first fruit in hierarchy will always load,
        // otherwise the fruit will be random.
        int index = _debugOn ?
            GetFruitIndexFromEnum(testModeStartFruit) :
            Random.Range(0, GetFruitIndexFromEnum(maxFruitSpawn));

        _quededFruit = GetFruitFromList(index);
    }
}
