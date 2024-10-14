using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.parent = null;
        player.transform.position = spawnPoint.transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
