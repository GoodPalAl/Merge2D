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

    /// <summary>
    /// Official merge order of fruit.
    /// </summary>
    public List<GameObject> FruitOrder = new();
    public GameObject GetFruitFromEnum(Fruit _fruit) => FruitOrder.Find(x => x.CompareTag(_fruit.ToString()));
    public int GetFruitIndexFromEnum(Fruit _fruit) => FruitOrder.FindIndex(x => x.CompareTag(_fruit.ToString()));
    public GameObject GetFruit(int _order) => FruitOrder[_order];
    public GameObject GetNextFruit(string _oldFruitName) 
    {
        int nextIndex = FruitOrder.FindIndex(Fruit => Fruit.name == _oldFruitName) + 1;
        return GetFruit(nextIndex);
    }
    public int FruitCount() => FruitOrder.Count;

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
