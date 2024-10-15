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
        var player = GameObject.FindWithTag("Player").GetComponent<Character>();
        if(!player) this.player = player.gameObject;
        player.transform.parent = null;
        player.transform.position = spawnPoint.transform.position;
        player.m_shield = player.m_maxShield;
        if(!Camera.main)
        {
            var go = Instantiate(cameraInstance);
            go.GetComponent<Camera_Player>().player = player.gameObject;
            go.transform.position = player.transform.position;
        }
    }
}
