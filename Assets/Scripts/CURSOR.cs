using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CURSOR : MonoBehaviour
{
    public Texture2D cursuricon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursuricon, Vector2.zero, CursorMode.Auto);
    }

   
}
