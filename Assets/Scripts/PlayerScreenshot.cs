using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScreenshot : MonoBehaviour
{
    public Camera screenshot_camera;
    public GameObject player;

    private void Start()
    {

    }
    private void Update()
    {
        
        if(player == null) player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) screenshot_camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3);
    }
}
