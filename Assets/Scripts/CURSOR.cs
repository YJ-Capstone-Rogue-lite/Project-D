using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CURSOR : MonoBehaviour
{
    public Texture2D cursuricon;
    public Texture2D inventory_and_ESC_CursorIcon; // 인벤토리 and esc 열 때 사용할 커서 아이콘
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 Change_hotSpot = Vector2.zero;

    void Start()
    {
        hotSpot.x = cursuricon.width / 2;
        hotSpot.y = cursuricon.height / 2;
        Cursor.SetCursor(cursuricon, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        cursor_Change();
    }

    public void cursor_Change()
    {
        //Debug.Log(IngameUI.single.openOption +","+ IngameUI.single.inv_slot_active_bool);
        if(IngameUI.single.inv_slot_active_bool == true || IngameUI.single.openOption == false)
        {
            Cursor.SetCursor(inventory_and_ESC_CursorIcon, Change_hotSpot, CursorMode.Auto);

        }
        else
        {
            Cursor.SetCursor(cursuricon, hotSpot, CursorMode.Auto);

        }
    }
   
}
