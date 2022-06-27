using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private bool isInitialCursorSet = false;

    void Update()
    {
    }
}
