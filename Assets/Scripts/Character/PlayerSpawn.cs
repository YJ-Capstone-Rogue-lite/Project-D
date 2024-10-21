using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cameraInstance;
    void Start()
    {
        var go = GameObject.FindWithTag("Player");
        Character player = null;
        go?.TryGetComponent(out player);
        if(player == null) player = Instantiate(this.player).GetComponent<Character>();
        player.transform.parent = null;
        player.transform.position = spawnPoint.transform.position;
        player.m_shield = player.m_maxShield;
        if(!Camera.main)
        {
            var ins = Instantiate(cameraInstance);
            ins.GetComponent<Camera_Player>().player = player.gameObject;
            ins.transform.position = player.transform.position;
        }
    }
}
