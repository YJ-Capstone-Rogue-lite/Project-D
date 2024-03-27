using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        StringBuilder temp = new();
        for(int i=0; i< m_roomNumberTablel.GetLength(0); i++)
        {
            for(int j=0; j<m_roomNumberTablel.GetLength(1); j++)
            {
                temp.Append(m_roomNumberTablel[i, j] + " ");
            }
            temp.AppendLine();
        }
        Debug.Log(temp);
    }


    private void CreateDefaultRoom()
    {
        int x = 0, y = 0;
        for(int i=0; i<floorSize*floorSize; i++)
        {
            x = i / floorSize;
            y = i % floorSize;
            m_RoomTablel[x, y] = roomContainer.defaultRoomData[Random.Range(0, roomContainer.defaultRoomData.Length)];
            m_roomNumberTablel[x, y] = roomIdx++;
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

            for(int i=x; i<x+roomData.roomSize.GetLength(0); i++)
            {
                for(int j=y; j<y+roomData.roomSize.GetLength(1); j++)
                {
                    if(roomData.roomSize[i-x, j-y])
                    {
                        m_RoomTablel[i, j] = roomData;
                        m_roomNumberTablel[i, j] = roomIdx;
                        m_selectedRoomTablel[i, j] = true;
                    }
                }
            }
            roomIdx++;
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

            m_selectedRoomTablel[x, y] = true;
            m_roomNumberTablel[x, y] = roomIdx++;
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

            m_RoomTablel[x, y] = roomData;
            m_roomNumberTablel[x, y] = roomIdx++;
            m_selectedRoomTablel[x, y] = true;
        }
    }
    private void Triangulator()
    {
        
    }


    private void CreateRooms()
    {
        StringBuilder st = new();
        HashSet<int> ints = new();
        for(int i=0; i<floorSize; i++)
        {
            for(int j=0; j<floorSize; j++)
            {
                // if(m_selectedRoomTablel[i, j])
                if(m_selectedRoomTablel[i, j] && !ints.Contains(m_roomNumberTablel[i, j]))
                {
                    GameObject temp = Resources.Load(m_RoomTablel[i, j].path) as GameObject;
                    temp = Instantiate(temp, gameObject.transform);
                    temp.transform.position = new Vector2(j*roomSize_Width, -(i*roomSize_Height));
                    ints.Add(m_roomNumberTablel[i, j]);
                }
                st.Append(m_selectedRoomTablel[i, j] + " ");
            }
            st.AppendLine();
        }
        Debug.Log(st);
    }

    private bool SelectedRoomCheck(int x, int y)
    {
        return m_selectedRoomTablel[x, y];
    }
    private bool SelectedRoomCheck(RoomData roomData, int x, int y)
    {
        bool check = false;
        for(int i=x; i<x+roomData.roomSize.GetLength(0); i++)
        {
            for(int j=y; j<y+roomData.roomSize.GetLength(1); j++)
            {
                if(m_selectedRoomTablel[i, j]) check = true;
            }
        }

        return check;
    }
}
