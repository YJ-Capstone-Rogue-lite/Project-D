using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    private static FloorManager instance;
    public static FloorManager Instance
    {
        get { return instance; }
    }

    public FloorItemTable[] floorItemTables;
    private int floorIdx = 0;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void FloorUp()
    {
        var ao = SceneManager.LoadSceneAsync("Floor");
        ao.completed += (ao)=>{
            floorIdx++;
        };
    }
}
