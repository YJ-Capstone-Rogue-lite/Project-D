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

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        mini_camera.transform.position = GameObject.FindWithTag("player_mini").transform.position;
    }
}
