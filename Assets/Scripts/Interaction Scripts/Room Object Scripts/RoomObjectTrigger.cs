using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomObjectTrigger : MonoBehaviour
{
    private Room parentRoom;

    protected virtual void OnEnable()
    {
        parentRoom = GetComponent<Room>();
        if(parentRoom == null) parentRoom = transform.parent.GetComponent<Room>();
        parentRoom.roomEnter += OnRoomEnter;
        parentRoom.roomExit += OnRoomExit;
    }
    protected virtual void OnDisable()
    {
        parentRoom.roomEnter -= OnRoomEnter;
        parentRoom.roomExit -= OnRoomExit;
    }

    public abstract void OnRoomEnter(Room room);
    public abstract void OnRoomExit(Room room);
}
