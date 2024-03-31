using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Tilemap tilemap { get; set; }

    public void OnEnable() => tilemap = gameObject.GetComponent<Tilemap>();

    public void RoomEnterTrigger() => BroadcastMessage("RoomEnter", this, SendMessageOptions.DontRequireReceiver);
    public void RoomExitTrigger() => BroadcastMessage("RoomExit", this, SendMessageOptions.DontRequireReceiver);
}
