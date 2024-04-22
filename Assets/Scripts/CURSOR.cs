using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CURSOR : MonoBehaviour
{
    public Texture2D cursuricon;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        hotSpot.x = cursuricon.width / 2;
        hotSpot.y = cursuricon.height / 2;
        Cursor.SetCursor(cursuricon, hotSpot, CursorMode.Auto);
    }

   
}
