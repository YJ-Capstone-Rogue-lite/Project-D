using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : RoomObjectTrigger
{
    [SerializeField] private GameObject enemy;
    private GameObject enemyInstance;

    protected override void OnEnable()
    {
        base.OnEnable();

        enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
        enemyInstance.transform.SetParent(transform, true);
        enemyInstance.SetActive(false);
    }

    public override void OnRoomEnter(Room room)
    {
        enemyInstance.SetActive(true);
        room.EnemyTemp(1);
    }

    public override void OnRoomExit(Room room)
    {
        
    }
}
