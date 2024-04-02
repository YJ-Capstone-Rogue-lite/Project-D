using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomObjectTrigger
{
    public abstract void RoomEnter(Room room);
    public abstract void RoomExit(Room room);
}
