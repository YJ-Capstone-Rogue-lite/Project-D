using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    private static GameObject _focusedMinimapChar = null;
    public static GameObject focusedMinimapChar {
        private set {
            if (value == _focusedMinimapChar)
                return;
            if(value != _focusedMinimapChar && _focusedMinimapChar != null)
                _focusedMinimapChar.SetActive(false);

            _focusedMinimapChar = value;
            if (_focusedMinimapChar == null)
                return;
            _focusedMinimapChar.SetActive(true);
        }
        get => _focusedMinimapChar;
    }

    public delegate void RoomHandler(Room room);

    public RoomHandler roomEnter;
    public RoomHandler roomExit;

    public enum State { READY, ING, CLEAR }
    public Tilemap tilemap { get; set; }
    public State state = State.READY;
    private int enemyCount = 0;
    [SerializeField] private GameObject PlayerMinimap;

    public void OnEnable() => tilemap = gameObject.GetComponent<Tilemap>();

    public void RoomEnterTrigger() => roomEnter.Invoke(this);
    public void RoomExitTrigger()
    {
        Debug.Log("AAA : " + roomExit.Method);
        roomExit.Invoke(this);
        state = State.CLEAR;
    }

    public void EnemyTemp(int i)
    {
        enemyCount += i;
        if (enemyCount <= 0) RoomExitTrigger();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        focusedMinimapChar = PlayerMinimap;
        if (state == State.READY)
        {
            state = State.ING;
            RoomEnterTrigger();
        }
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
            focusedMinimapChar = null;

        if (state == State.ING && enemyCount <= 0)
        {
            state = State.CLEAR;
            RoomExitTrigger();
        }
    }
    protected void OnTriggerStay2D(Collider2D collider)
    {
        
        //focusedMinimapChar = PlayerMinimap;
        
    }
}
