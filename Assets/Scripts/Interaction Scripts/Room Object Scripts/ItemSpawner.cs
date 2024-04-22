using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour, IRoomObjectTrigger
{
    public Item item;
    public void RoomEnter(Room room)
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }

    public void RoomExit(Room room)
    {
        
    }
}
