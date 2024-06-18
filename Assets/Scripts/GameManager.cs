using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    public int enemyDestoryCount;

    public Camera mini_camera;
    public bool isPlaying = true;
    Room room;
    public Vector3 mini_camera_transform;
    public GameObject playerIcon;
    public AudioClip audioClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.SetMusic(audioClip);
    }

    // Update is called once per frame

    //미니맵에 표시되는 카메라
    void Update()
    {
        if (Room.focusedMinimapChar == null)
            return;
        
        mini_camera_transform = new Vector3(Room.focusedMinimapChar.transform.position.x, Room.focusedMinimapChar.transform.position.y, -40);
        mini_camera.transform.position = mini_camera_transform;
    }
}
