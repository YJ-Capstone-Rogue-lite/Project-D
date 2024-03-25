using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomContainer
{
    public RoomData[] defaultRoomData;
    public RoomData[] overSizeRoomData;
    public RoomData[] specialRoomData;
}

public struct RoomData
{
    public bool[,]      roomSize;
    public string       path;
}