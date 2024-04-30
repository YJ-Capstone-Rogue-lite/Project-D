using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public enum State { READY, ING, CLEAR }
    public Tilemap tilemap { get; set; }
    public State state = State.READY;
    private int enemyCount = 0;
    [SerializeField] GameObject PlayerMinimap;

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
        if(state == State.READY)
        {
            PlayerMinimap.SetActive(true);
            state = State.ING;
            RoomEnterTrigger();
        }
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        if(state == State.ING) state = State.CLEAR;
    }
}
