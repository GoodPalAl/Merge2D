using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region

    #endregion
    public static Vector3 GetCursorInWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
