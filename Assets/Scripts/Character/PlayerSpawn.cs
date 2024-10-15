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
        var player = GameObject.FindWithTag("Player");
        if(!player) this.player = player;
        player.transform.parent = null;
        player.transform.position = spawnPoint.transform.position;
        if(!Camera.main)
        {
            var go = Instantiate(cameraInstance);
            go.GetComponent<Camera_Player>().player = player;
            go.transform.position = player.transform.position;
        }
    }
}
