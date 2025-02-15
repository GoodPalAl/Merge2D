using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroppedFruitManager : MonoBehaviour
{
    /// <summary>
    /// Establishes only one Instance of this class.
    /// </summary>
    #region Singleton
    public static DroppedFruitManager Instance { get; private set; }
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
    /// List of all the dropped fruit in the game.
    /// </summary>
    List<GameObject> _DroppedFruit = new();

    public void AddFruitToBoard(GameObject newFruit) => _DroppedFruit.Add(newFruit);

    public int GetDroppedFruitCount() => _DroppedFruit?.Count ?? 0;

    public void DestroyFruitOnBoard(GameObject victim)
    {
        // Find the parameter in the list of dropped fruits
        var victimOnBoard = _DroppedFruit.Find(fruit => fruit == victim);
        // If the fruit was found and is not null, remove it from the list.
        if (victimOnBoard != null)
        {
            _DroppedFruit.Remove(victimOnBoard);
        }
        // If the parameter is not also null, destroy it from the board.
        if (victim != null)
        {
            Destroy(victim);
        }
    }

    public void PrintAllFruit()
    {
        string str = "Fruits: ";
        foreach (var fruit in _DroppedFruit)
        {
            // check if fruit is null
            if (fruit != null)
            {
                str += fruit.name;
                if (fruit != _DroppedFruit[^1])
                {
                    str += ", ";
                }
            }
        }
        Debug.Log(str);
        Debug.Log("Number of Fruits Dropped: "
            + GetDroppedFruitCount().ToString());
    }
}
