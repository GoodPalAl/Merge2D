using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static GameUtility.Enums;

public class FruitManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static FruitManager Instance { get; private set; }
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
    /// </summary>
    [SerializeField]
    List<GameObject> fruitOrder = new();

    [SerializeField]
    Fruits testModeStartFruit = Fruits.Strawberry;
    [SerializeField]
    Fruits maxFruitSpawn = Fruits.Banana;

    // Does this need to be static?
    static GameObject _quededFruit;

    private void Start()
    {
        var gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            var listener = gameControllerObject.GetComponent<ScoreManager>();
            BoardClearEvent.AddListener(delegate 
            {
                listener.ResetScore();
            });
        }
    }

    public GameObject GetFirstFruit(bool _isDebugOn)
        => _quededFruit = GetFruitFromList(
            _isDebugOn ? GetFruitIndexFromEnum(testModeStartFruit) : 0
            );

    int GetFruitIndexFromEnum(Fruits _fruit) 
        => fruitOrder.FindIndex(x => x.CompareTag(_fruit.ToString()));
    GameObject GetFruitFromList(int _order) => fruitOrder[_order];
    public GameObject GetFruitNextInOrder(string _oldFruitName) 
    {
        int nextIndex = fruitOrder.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        return GetFruitFromList(nextIndex);
    }

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

    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    static List<GameObject> DroppedFruit = new();
    public UnityEvent BoardClearEvent;

    public static void AddFruitToBoard(GameObject newFruit)
        => DroppedFruit.Add(newFruit);
    
    public static int GetDroppedFruitCount()
        => DroppedFruit.Count;

    public static void DestroyAndRemoveFruit(GameObject victim)
    {
        if (victim == null)
        {
            Debug.Log("<color=red>ERROR:</color> " + victim.name + " is not real.");
            return;
        }

        var victimOnBoard = DroppedFruit.Find(fruit => fruit == victim);
        if (victimOnBoard == null)
        {
            Debug.Log("<color=red>ERROR:</color> " + victimOnBoard.name + " is not in board.");
            return;
        }

        DroppedFruit.Remove(victimOnBoard);
        Destroy(victim);
    }

    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// FIXME: game will not destroy object.
    /// </summary>
    public void ClearBoard()
    {
        PrintAllFruitOnBoard();
        for (int i = 1; i < DroppedFruit.Count; i++)
        {
            if (DroppedFruit[^i].IsDestroyed())
            {
                Debug.Log("This fruit <color=red>\'" + DroppedFruit[^i].name + "\'</color> is already destroyed.");
                continue;
            }
            Debug.Log("Destroying <color=red>\'" + DroppedFruit[^i].name + "\'</color>...");
            DestroyImmediate(DroppedFruit[^i], true);
        }
        DroppedFruit.Clear();

        BoardClearEvent?.Invoke();
    }

    public static void PrintAllFruitOnBoard()
    {
        string str = "Fruits: ";
        foreach (var fruit in DroppedFruit)
        {
            str += fruit.name;
            if (fruit != DroppedFruit[^1]) // [^1] = DroppedFruit.Count - 1
            { 
                str += ", ";
            }
        }
        Debug.Log(str);
        Debug.Log("Number of Fruits Dropped: " 
            + GetDroppedFruitCount().ToString());
    }
}
