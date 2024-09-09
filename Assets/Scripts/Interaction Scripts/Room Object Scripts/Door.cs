using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : RoomObjectTrigger
{
    public Vector3Int vector { get; set; }
    public TileBase doorTile { get; set; }
    public TileBase closeDoorTile { get; set; }

    public override void OnRoomEnter(Room room)
    {
        room.tilemap.SetTile(vector, doorTile);
    }

    public override void OnRoomExit(Room room)
    {
        StartCoroutine(DoorClossing(room));
    }

    IEnumerator DoorClossing(Room room)
    {
        room.tilemap.SetTile(vector, closeDoorTile);
        yield return new WaitForSeconds(1);
        room.tilemap.SetTile(vector, null);
    }
}
