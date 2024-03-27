using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public void RoomEnter() => BroadcastMessage("RoomEnter");
    public void RoomExit() => BroadcastMessage("RoomExit");
}
