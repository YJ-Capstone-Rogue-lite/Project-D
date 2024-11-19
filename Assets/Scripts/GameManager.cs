using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject player;
    public static GameManager Instance 
    { 
        get
        {
            if(instance == null) instance = new GameManager();
            return instance;
        } 
    }
    public int enemyDestoryCount;

    public GameObject playerspawn;
    public Camera mini_camera;
    public bool isPlaying = true;
    Room room;
    public Vector3 mini_camera_transform;
    public GameObject playerIcon;
    public AudioClip audioClip;
    public float timer;
    public bool gamestart = false;
    public TextMeshProUGUI time;

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
        Debug.Log(gamestart);
        if (!gamestart) return;
        timer += Time.deltaTime;
        time.text = SetTime((int)timer);
        Debug.Log(gamestart);
        if (Room.focusedMinimapChar == null)
            return;

        mini_camera_transform = new Vector3(Room.focusedMinimapChar.transform.position.x, Room.focusedMinimapChar.transform.position.y, -40);
        mini_camera.transform.position = mini_camera_transform;



    }
    public string SetTime(int t)
    {
        //string min = (t / 60).ToString();

        //if(int.Parse(min) < 10)
        //{
        //    min = "0" + min;
        //}
        
        string sec = (t % 60).ToString();
        
        if(int.Parse(sec) < 100)
        {
            sec = "0" + sec;
        }
        return sec;
    }

    public void StartGame()
    {
        Instance.timer = 0;
        Instance.gamestart = true;
    }

    public void EndGame()
    {
        Instance.gamestart = false;
        Instance.time.text = "";
    }
}
