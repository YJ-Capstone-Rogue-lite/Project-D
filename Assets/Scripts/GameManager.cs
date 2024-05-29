using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    public Camera mini_camera;
    public bool isPlaying = true;
    Room room;
    public Vector3 mini_camera_transform;
    public GameObject playerIcon;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerIcon = GameObject.FindWithTag("player_mini");
        mini_camera_transform = new Vector3(playerIcon.transform.position.x, playerIcon.transform.position.y, -40);
        mini_camera.transform.position = mini_camera_transform;
    }
}
