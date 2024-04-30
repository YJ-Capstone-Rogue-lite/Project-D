using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public enum State { RAEDY, ING, CLEAR }
    public Tilemap tilemap { get; set; }
    public State state = State.RAEDY;
    private int enemyCount = 0;

    public void OnEnable() => tilemap = gameObject.GetComponent<Tilemap>();

    public void RoomEnterTrigger() => BroadcastMessage("RoomEnter", this, SendMessageOptions.DontRequireReceiver);
    public void RoomExitTrigger()
    {
        BroadcastMessage("RoomExit", this, SendMessageOptions.DontRequireReceiver);
        state = State.CLEAR;
    }

    public void EnemyTemp(int i)
    {
        enemyCount += i;
        if (enemyCount <= 0) RoomExitTrigger();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.CompareTag("Player")) return;
        if(state == State.RAEDY)
        {
            state = State.ING;
            RoomEnterTrigger();
        }
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        if(state == State.ING) state = State.CLEAR;
    }
}
