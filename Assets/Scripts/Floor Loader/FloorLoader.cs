using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FloorLoader : MonoBehaviour
{
    private static FloorLoader              instance;
    public static FloorLoader               Instance
    {
        get { return instance; }
    }

    public const int                        floorSize               = 8;
    public const int                        roomSize_Width          = 16;
    public const int                        roomSize_Height         = 12;

    public static readonly RoomContainer    roomContainer           = InitRoomContainer.roomContainer;

    private bool[,]                         m_selectedRoomTablel    = new bool[floorSize, floorSize];
    private int[,]                          m_roomNumberTablel      = new int[floorSize, floorSize];
    private RoomData[,]                     m_RoomTablel            = new RoomData[floorSize, floorSize];

    void Awake()
    {
        // if(instance != this) 
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        // instance = this;
    }

    void Start() => FloorLoad();

    private int roomIdx;
    public void FloorLoad()
    {
        roomIdx = 0;
        CreateDefaultRoom();
        ChangeOverSizeRoom();
        SelectPorintRoom();
        ChangeSpacialRoom();
        CreateRooms();
    }


    private void CreateDefaultRoom()
    {
        int x = 0, y = 0;
        for(int i=0; i<floorSize*floorSize; i++)
        {
            x = i % floorSize;
            y = i / floorSize;
            m_RoomTablel[y, x] = roomContainer.defaultRoomData[Random.Range(0, roomContainer.defaultRoomData.Length)];
            m_roomNumberTablel[y, x] = roomIdx++;
        }
    }
    private void ChangeOverSizeRoom()
    {
        int x = 0, y = 0;
        foreach(var roomData in roomContainer.overSizeRoomData)
        {
            do
            {                
                x = Random.Range(0, floorSize - roomData.roomSize.GetLength(0));
                y = Random.Range(0, floorSize - roomData.roomSize.GetLength(1));
            } while(SelectedRoomCheck(roomData, x, y));

            m_RoomTablel[y, x] = roomData;
            m_roomNumberTablel[y, x] = roomIdx++;
            for(int i=y; i<y+roomData.roomSize.GetLength(0); i++)
            {
                for(int j=x; j<x+roomData.roomSize.GetLength(1); j++)
                {
                    if(roomData.roomSize[i-y, j-x])m_selectedRoomTablel[i, j] = true;
                }
            }
        }
    }
    private void SelectPorintRoom()
    {
        int x = 0, y = 0;
        for(int i=0; i<4; i++)
        {
            do
            {                
                x = Random.Range(0, floorSize);
                y = Random.Range(0, floorSize);
            } while(SelectedRoomCheck(x, y));

            m_selectedRoomTablel[y, x] = true;
            m_roomNumberTablel[y, x] = roomIdx++;
        }
    }
    private void ChangeSpacialRoom()
    {
        int x = 0, y = 0;
        foreach(var roomData in roomContainer.specialRoomData)
        {
            do
            {                
                x = Random.Range(0, floorSize);
                y = Random.Range(0, floorSize);
            } while(SelectedRoomCheck(x, y));

            m_RoomTablel[y, x] = roomData;
            m_roomNumberTablel[y, x] = roomIdx++;
            m_selectedRoomTablel[y, x] = true;
        }
    }



    private void CreateRooms()
    {
        HashSet<int> ints = new();
        for(int i=0; i<floorSize; i++)
        {
            for(int j=0; j<floorSize; j++)
            {
                if(m_selectedRoomTablel[j, i] && !ints.Contains(m_roomNumberTablel[j, i]))
                {
                    GameObject temp = Resources.Load(m_RoomTablel[j, i].path) as GameObject;
                    temp = Instantiate(temp, gameObject.transform);
                    temp.transform.position = new Vector2(i*roomSize_Width, j*roomSize_Height);
                    ints.Add(m_roomNumberTablel[j, i]);
                }
            }
        }
    }

    private bool SelectedRoomCheck(int x, int y)
    {
        return m_selectedRoomTablel[y, x];
    }
    private bool SelectedRoomCheck(RoomData roomData, int x, int y)
    {
        bool check = false;
        for(int i=y; i<y+roomData.roomSize.GetLength(0); i++)
        {
            for(int j=x; j<x+roomData.roomSize.GetLength(1); j++)
            {
                if(m_selectedRoomTablel[i, j]) check = true;
            }
        }

        return check;
    }
}
