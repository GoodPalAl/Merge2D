using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Singleton

    public static CursorManager Instance { get; private set; }

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

    static GameObject Cursor;
    [SerializeField]
    float ShowCursorDelay = 0.5f;

    private void Start()
    {
        if (Cursor == null)
            Cursor = GameObject.Find("Cursor");
    }


    public GameObject GetCursor() => Cursor;
    public void UpdateCursorPosition(Vector3 pos) => Cursor.transform.position = pos;
    public void UpdateCursorSprite(Sprite s) => Cursor.GetComponentInChildren<SpriteRenderer>().sprite = s;


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

    public float GetShowCursorDelay() => ShowCursorDelay;

}
