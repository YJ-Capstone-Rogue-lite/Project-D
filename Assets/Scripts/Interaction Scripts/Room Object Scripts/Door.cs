using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour, IRoomObjectTrigger
{
    public Vector3Int vector { get; set; }
    public TileBase doorTile { get; set; }
    public TileBase closeDoorTile { get; set; }

    public void RoomEnter(Room room)
    {
        room.tilemap.SetTile(vector, doorTile);
    }

    public void RoomExit(Room room)
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
