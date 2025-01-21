using System.Collections.Generic;
using UnityEngine;

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

    ///
    /// <summary>Fruit in order as enums. First = strawberry, Last = watermelon</summary>
    ///
    public enum Fruit
    {
        Strawberry, 
        Cherry, 
        Apple, 
        Peach, 
        Pear, 
        Orange, 
        Pineapple, 
        Banana, 
        Lemon, 
        Avocado, 
        Kiwi, 
        Coconut, 
        Grape, 
        Watermelon
    }

    [SerializeField]
    Fruit _testModeStartFruit = Fruit.Strawberry;
    [SerializeField]
    Fruit _maxFruitSpawn = Fruit.Banana;

    // Does this need to be static?
    static GameObject _quededFruit;

    public GameObject GetFirstFruit(bool _isDebugOn)
        => _quededFruit = GetFruitFromList(
            _isDebugOn ? GetFruitIndexFromEnum(_testModeStartFruit) : 0
            );



    /// <summary>
    /// Official merge order of fruit. Initialized in Unity Editor
    /// </summary>
    [SerializeField]
    List<GameObject> _fruitOrder = new();

    public int GetFruitIndexFromEnum(Fruit _fruit) 
        => _fruitOrder.FindIndex(x => x.CompareTag(_fruit.ToString()));
    public GameObject GetFruitFromList(int _order) => _fruitOrder[_order];
    public GameObject GetNextFruit(string _oldFruitName) 
    {
        int nextIndex = _fruitOrder.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        return GetFruitFromList(nextIndex);
    }
    public int FruitCount() => _fruitOrder.Count;

    public GameObject GetQueuedFruit() => _quededFruit;

    public void UpdateQuededFruit(bool _debugOn)
    {
        // If TestMode enabled,
        // the first fruit in hierarchy will always load,
        // otherwise the fruit will be random.
        int index = _debugOn ?
            GetFruitIndexFromEnum(_testModeStartFruit) :
            Random.Range(0, GetFruitIndexFromEnum(_maxFruitSpawn));
        _quededFruit = GetFruitFromList(index);
    }

    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    public static List<GameObject> DroppedFruit = new();

    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// </summary>
    public void ClearBoard()
    {
        foreach (var obj in DroppedFruit)
        {
            Destroy(obj);
        }
        DroppedFruit.Clear();

        // FIXME: use unity event instead of having a dependency
        ScoreManager.Instance.ResetScore();
    }
}
