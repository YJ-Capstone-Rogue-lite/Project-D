using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class FloorLoader : MonoBehaviour
{
    private static FloorLoader              instance;
    public static FloorLoader               Instance
    {
        get { return instance; }
    }

    public const int                        floorSize = 8;
    public const int                        roomSize_Width = 4;
    public const int                        roomSize_Height = 4;

    public static readonly RoomContainer    roomContainer;

    private bool[,]                         m_sekectedRoomTablel = new bool[floorSize, floorSize];

    void Awake()
    {
        if(instance != null || instance != this) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void FloorLoad()
    {
        m_sekectedRoomTablel = new bool[8, 8];
        
    }
}
