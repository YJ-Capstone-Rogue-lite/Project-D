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
    public int[ , ]         roomSize;
    public TileType[ , ]    tileTable;
    public GameObject[ , ]  objectTable;
}

public enum TileType
{
    VOID, FLOOR, WALL, DOOR
}