using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IRoomObjectTrigger
{
    [SerializeField] private GameObject enemy;
    public void RoomEnter(Room room)
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    public void RoomExit(Room room)
    {
        
    }
}
