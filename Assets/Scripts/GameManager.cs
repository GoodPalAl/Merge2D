using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        { 
            Instance = this; 
        }
    }

    #endregion

    public GameObject Cursor;
    /// <summary>
    /// List of all the dropped fruit in the game.
    /// </summary>
    public static List<GameObject> DroppedFruit = new();
    private void Start()
    {
        Cursor = GameObject.Find("Cursor");
    }

    /// <summary>
    /// Convert mouse position to Unity world position
    /// Mouse Position:
    ///  <0,0,0> = Bottom-Left of screen
    ///  <Screen.width, Screen.height, 0> = Top-Right of screen
    /// </summary>
    /// <returns>Mouse position in Unity world position vector3</returns>
    public static Vector3 GetCursorInWorldPosition() 
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public void HideCursor() => Cursor.SetActive(false);
    public void ShowCursor() => Cursor.SetActive(true);

    /// <summary>
    /// Clear the board of all fruits. DEBUG ONLY.
    /// </summary>
    public void ClearBoard()
    {
        foreach (var obj in DroppedFruit)
        {
            DestroyImmediate(obj, true);
        }
        DroppedFruit.Clear();
    }
}
