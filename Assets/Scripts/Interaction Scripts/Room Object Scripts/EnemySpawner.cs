using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IRoomObjectTrigger
{
    [SerializeField] private GameObject enemy;
    public void RoomEnter(Room room)
    {
<<<<<<< HEAD
        Instantiate(enemy, transform.position, Quaternion.identity);
=======
        var go = Instantiate(enemy, transform.position, Quaternion.identity);
        go.transform.SetParent(transform, true);
        transform.parent.GetComponent<Room>().EnemyTemp(1);
>>>>>>> System
    }

    public void RoomExit(Room room)
    {
        
    }
}
