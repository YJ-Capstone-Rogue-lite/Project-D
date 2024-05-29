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
    

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Room.focusedMinimapChar == null)
            return;
        
        mini_camera_transform = new Vector3(Room.focusedMinimapChar.transform.position.x, Room.focusedMinimapChar.transform.position.y, -40);
        mini_camera.transform.position = mini_camera_transform;
    }
}
