using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DelaunatorSharp;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class FloorLoader : MonoBehaviour
{
    private static FloorLoader                          instance;
    public static FloorLoader                           Instance
    {
        get { return instance; }
    }

<<<<<<< HEAD
    public const int                                    floorSize               = 10;
=======
    public const int                                    floorSize               = 8;
>>>>>>> System
    public const int                                    roomSize_Width          = 16;
    public const int                                    roomSize_Height         = 12;
    public const int                                    selectRoomInt           = 3;

    public static readonly RoomContainer                roomContainer           = InitRoomContainer.roomContainer;

    private bool[,]                                     m_selectedRoomTablel    = new bool[floorSize, floorSize];
    private int[,]                                      m_roomNumberTablel      = new int[floorSize, floorSize];
    private RoomData[,]                                 m_RoomTablel            = new RoomData[floorSize, floorSize];
    Dictionary<Point, List<Point>>                      nodes                   = new();
    Dictionary<int, System.Action<Tilemap, int, int>>   RoomAction              = new();
    private int                                         roomIdx                 = 0;
    Point                                               startPoint;
    Point                                               bossPoint;

<<<<<<< HEAD
=======
    public GameObject                                   player;

>>>>>>> System
    [SerializeField] private TileBase[]                 floorTiles;
    [SerializeField] private TileBase[]                 doorTiles;
    private Queue<(Tilemap, Vector3Int, TileBase)>      waittingDoorQueue       = new();

    private class Point : IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if(obj is not IPoint) return false;
            IPoint temp = (IPoint) obj;
            return X == temp.X && Y == temp.Y;
        }
        public override int GetHashCode()
        {
            return System.HashCode.Combine(X, Y);
        }

        public static double GetABLineLength(IPoint l1, IPoint l2)
        {
            return System.Math.Abs(l1.X - l2.X) + System.Math.Abs(l1.Y - l2.Y);
        }
    }

    void Awake()
    {
        // if(instance != this) 
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        // instance = this;

        RoomAction.Add(0, (tilemap, x, y)=>{
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+6, -(x*roomSize_Height)-1, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+9, -(x*roomSize_Height)-1, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+6, -(x*roomSize_Height)+0, 0), floorTiles[1]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+9, -(x*roomSize_Height)+0, 0), floorTiles[2]);

            tilemap.SetTile(new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)+0, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)+0, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)-1, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)-1, 0), null);

            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)+0, 0), doorTiles[0]));
            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)+0, 0), doorTiles[0]));
        });
        RoomAction.Add(1, (tilemap, x, y)=>{
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+0, -(x*roomSize_Height)-5, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+0, -(x*roomSize_Height)-4, 0), floorTiles[1]);
            
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+0, -(x*roomSize_Height)-6, 0), null);

            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+0, -(x*roomSize_Height)-6, 0), doorTiles[1]));
        });
        RoomAction.Add(2, (tilemap, x, y)=>{
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+15, -(x*roomSize_Height)-5, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+15, -(x*roomSize_Height)-4, 0), floorTiles[2]);
            
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+15, -(x*roomSize_Height)-6, 0), null);

            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+15, -(x*roomSize_Height)-6, 0), doorTiles[1]));
        });
        RoomAction.Add(3, (tilemap, x, y)=>{
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+6, -(x*roomSize_Height)-11, 0), floorTiles[3]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+9, -(x*roomSize_Height)-11, 0), floorTiles[4]);

            tilemap.SetTile(new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)-11, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)-11, 0), null);

            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)-11, 0), doorTiles[0]));
            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)-11, 0), doorTiles[0]));
        });
<<<<<<< HEAD
=======
        RoomAction.Add(4, (tilemap, x, y)=>{
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+6, -(x*roomSize_Height)-1, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+9, -(x*roomSize_Height)-1, 0), floorTiles[0]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+6, -(x*roomSize_Height)+0, 0), floorTiles[1]);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+9, -(x*roomSize_Height)+0, 0), floorTiles[2]);

            tilemap.SetTile(new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)+0, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)+0, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)-1, 0), null);
            tilemap.SetTile(new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)-1, 0), null);

            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+7, -(x*roomSize_Height)+0, 0), doorTiles[2]));
            waittingDoorQueue.Enqueue((tilemap, new Vector3Int(y*roomSize_Width+8, -(x*roomSize_Height)+0, 0), doorTiles[2]));
        });
>>>>>>> System
    }

    void Start() => FloorLoad();

    void FloorLoad()
    {
        roomIdx = 0;
        
        CreateDefaultRoom();
        SelectPorintRoom();
        CreateBossRoom();
        CreateSpecialRoom();
        ChangeOverSizeRoom();
        ChangeStartRoom();
        Triangulator();
        MinimumSpanningTree();
        ConnenctRooms();
        CreateRooms();
        DoorPP();
<<<<<<< HEAD
=======
        player.transform.position = new Vector3((float)(startPoint.Y*roomSize_Width)+roomSize_Width/2, (float)-(startPoint.X*roomSize_Height)-(roomSize_Height/2));
>>>>>>> System
        // Destroy(this);
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
            OverSizeInject(roomData, x, y);
        }
    }
    private void SelectPorintRoom()
    {
        int x = 0, y = 0;
        for(int i=0; i<selectRoomInt; i++)
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
    private void CreateBossRoom()
    {
        int x = 0, y = 0;
        var roomData = roomContainer.specialRoomData[1];
        do
        {                
<<<<<<< HEAD
            x = Random.Range(0, floorSize - roomData.roomSize.GetLength(0));
            y = floorSize - 1;
=======
            x = floorSize - 1;
            y = Random.Range(0, floorSize - roomData.roomSize.GetLength(1));
>>>>>>> System
        } while(SelectedRoomCheck(x, y));
        m_selectedRoomTablel[x, y] = true;
        m_roomNumberTablel[x, y] = roomIdx++;
        bossPoint = new(x, y);
    }
    private void ChangeStartRoom()
    {
        int x = 0, y = 0;
        do
        {
            x = Random.Range(0, floorSize);
            y = Random.Range(0, floorSize);
        } while(SelectedRoomCheck(x, y));

        m_RoomTablel[x, y] = roomContainer.specialRoomData[0];
        m_roomNumberTablel[x, y] = roomIdx++;
        m_selectedRoomTablel[x, y] = true;
        startPoint = new(x, y);
    }
    private void Triangulator()
    {
        HashSet<IPoint> points = new();
        HashSet<int> ints = new();
        for(int i=0; i<floorSize; i++)
        {
            for(int j=0; j<floorSize; j++)
            {
                if(m_RoomTablel[i, j].Equals(roomContainer.specialRoomData)) ints.Add(m_roomNumberTablel[i, j]);
                if(m_selectedRoomTablel[i, j] && !ints.Contains(m_roomNumberTablel[i, j]))
                {
                    points.Add(new Point(i, j));
                    ints.Add(m_roomNumberTablel[i, j]);
                }
            }
        }
        Dictionary<Point, HashSet<Point>> nodes = new();
        Delaunator delaunator = new(points.ToArray());
        delaunator.ForEachTriangleEdge((edge)=>{
            if(!nodes.ContainsKey((Point)edge.P))nodes.Add((Point)edge.P, new());
            nodes[(Point)edge.P].Add((Point)edge.Q);
            if(!nodes.ContainsKey((Point)edge.Q))nodes.Add((Point)edge.Q, new());
            nodes[(Point)edge.Q].Add((Point)edge.P);
        });
        foreach(Point hashPoint in nodes.Keys)
            this.nodes.Add(hashPoint, nodes[hashPoint].ToList());
        this.nodes1 = nodes;
    }
    private void MinimumSpanningTree()
    {
        var mstEdges = new List<(Point, Point)>();
        var selectedPoints = new HashSet<Point>();
        var edges = new List<(Point point, Point neighbor, double distance)>();

        selectedPoints.Add(startPoint);
        foreach (var neighbor in nodes[startPoint])
        {
            edges.Add((startPoint, neighbor, Point.GetABLineLength(startPoint, neighbor)));
        }

        while (edges.Count > 0)
        {
            // 리스트를 거리에 따라 정렬
            edges.Sort((a, b) => a.distance.CompareTo(b.distance));

            var (currentPoint, nearestNeighbor, _) = edges[0];
            edges.RemoveAt(0);

            if (selectedPoints.Contains(nearestNeighbor))
            {
                continue;
            }

            selectedPoints.Add(nearestNeighbor);
            mstEdges.Add((currentPoint, nearestNeighbor));

            foreach (var neighbor in nodes[nearestNeighbor])
            {
                if (!selectedPoints.Contains(neighbor))
                {
                    edges.Add((nearestNeighbor, neighbor, Point.GetABLineLength(nearestNeighbor, neighbor)));
                }
            }
        }

        nodes = new();
        foreach(var hashPoint in mstEdges)
        {
            if(!nodes.ContainsKey(hashPoint.Item1)) nodes.Add(hashPoint.Item1, new());
            nodes[hashPoint.Item1].Add(hashPoint.Item2);
            if(!nodes.ContainsKey(hashPoint.Item2)) nodes.Add(hashPoint.Item2, new());
            nodes[hashPoint.Item2].Add(hashPoint.Item1);
        }
    }
    private void ConnenctRooms()
    {
        foreach(Point hashPoint1 in nodes.Keys)
        {
            foreach(Point hashPoint2 in nodes[hashPoint1])
            {
                for (int x = System.Math.Min((int)hashPoint1.X, (int)hashPoint2.X); x <= System.Math.Max((int)hashPoint1.X, (int)hashPoint2.X); x++)
                {
                    m_selectedRoomTablel[x, (int)hashPoint1.Y] = true;
                }
                for (int y = System.Math.Min((int)hashPoint1.Y, (int)hashPoint2.Y); y <= System.Math.Max((int)hashPoint1.Y, (int)hashPoint2.Y); y++)
                {
                    m_selectedRoomTablel[(int)hashPoint1.X, y] = true;
                }
            }
        }
    }
    private void CreateSpecialRoom()
    {
        int x = 0, y = 0;
        for(int i=2; i<roomContainer.specialRoomData.Length; i++)
        {
            var roomData = roomContainer.specialRoomData[i];
            do
            {                
                x = Random.Range(0, floorSize - roomData.roomSize.GetLength(0));
                y = Random.Range(0, floorSize - roomData.roomSize.GetLength(1)-1);
            } while(SelectedRoomCheck(x, y) || SelectedRoomCheck(roomData, x, y+1));
            m_selectedRoomTablel[x, y] = true;
            m_roomNumberTablel[x, y] = roomIdx++;
            OverSizeInject(roomData, x, ++y);
        }
    }
    private void CreateRooms()
    {
        HashSet<int> ints = new();
        for(int i=0; i<floorSize; i++)
        {
            for(int j=0; j<floorSize; j++)
            {
                // if(m_selectedRoomTablel[i, j])
                if((int)bossPoint.X == i && (int)bossPoint.Y == j) ints.Add(m_roomNumberTablel[i, j]);
                if(m_selectedRoomTablel[i, j] && !ints.Contains(m_roomNumberTablel[i, j]))
                {
                    GameObject temp = Instantiate(Resources.Load(m_RoomTablel[i, j].path) as GameObject, gameObject.transform);
                    temp.transform.position = new Vector2(j*roomSize_Width, -(i*roomSize_Height));
                    
                    SetDoors(temp, i, j);

                    ints.Add(m_roomNumberTablel[i, j]);
<<<<<<< HEAD
=======
                    if(i == startPoint.X && j == startPoint.Y) temp.GetComponent<Room>().state = Room.State.CLEAR;
>>>>>>> System
                }
            }
        }
        GameObject bossEnter = Instantiate(Resources.Load(m_RoomTablel[(int)bossPoint.X, (int)bossPoint.Y].path) as GameObject, gameObject.transform);
        bossEnter.transform.position = new Vector2(((int)bossPoint.Y)*roomSize_Width, -((int)bossPoint.X*roomSize_Height));
        SetDoors(bossEnter, (int)bossPoint.X, (int)bossPoint.Y);
<<<<<<< HEAD
        RoomAction[2](bossEnter.GetComponent<Tilemap>(), 0, 0);

        GameObject boss = Instantiate(Resources.Load(roomContainer.specialRoomData[1].path) as GameObject, gameObject.transform);
        boss.transform.position = new Vector2(((int)bossPoint.Y+1)*roomSize_Width, -((int)bossPoint.X*roomSize_Height));
        
        var tilemap = boss.GetComponent<Tilemap>();
        RoomAction[1](tilemap, 0, 0);
=======
        RoomAction[3](bossEnter.GetComponent<Tilemap>(), 0, 0);

        GameObject boss = Instantiate(Resources.Load(roomContainer.specialRoomData[1].path) as GameObject, gameObject.transform);
        boss.transform.position = new Vector2(((int)bossPoint.Y-1)*roomSize_Width, -(((int)bossPoint.X+roomContainer.specialRoomData[1].roomSize.GetLength(1)-2)*roomSize_Height));
        
        var tilemap = boss.GetComponent<Tilemap>();
        RoomAction[4](tilemap, 0, 1);
>>>>>>> System

        // Debug.Log(System.S)tring.Join(" ",ints.ToArray()));
    }
    private void OverSizeInject(RoomData roomData, int x, int y)
    {
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
    private void SetDoors(GameObject room, int x, int y)
    {
        var tilemap = room.GetComponent<Tilemap>();
        for(int i=x; i<x+m_RoomTablel[x, y].roomSize.GetLength(0); i++)
        {
            for(int j=y; j<y+m_RoomTablel[x, y].roomSize.GetLength(1); j++)
            {
                if(m_RoomTablel[x, y].roomSize[i-x, j-y])
                {
                    bool[] nextRooms = NextRoomCheck(i, j);
                    for(int k=0; k<4; k++)
                    {
                        if(nextRooms[k])
                        {
                            RoomAction[k](tilemap, i-x, j-y);
                        }
                    }
                }
            }
        }
    }
    private void DoorPP()
    {
        while(waittingDoorQueue.Count > 0)
        {
            var temp = waittingDoorQueue.Dequeue();
            Door door = temp.Item1.gameObject.AddComponent<Door>();
            door.doorTile = temp.Item3;
            door.vector = temp.Item2;
            temp.Item1.gameObject.transform.parent = temp.Item1.gameObject.transform;
        }
    }

    private bool[] NextRoomCheck(int x, int y)
    {
        bool[] temp = new bool[4];
        int idx = 0;
        for(int i= x-1; i<=x+1; i++)
        {
            for(int j=y-1; j<=y+1; j++)
            {
                if(System.Math.Abs(x-i) != System.Math.Abs(y-j))
                {
                    if(SelectedRoomCheck(i, j) && m_roomNumberTablel[x, y] != m_roomNumberTablel[i, j]) temp[idx++] = SelectedRoomCheck(i, j);
                    else temp[idx++] = false;
                }
            }
        }
        return temp;
    }

    private bool SelectedRoomCheck(int x, int y)
    {
        try { return m_selectedRoomTablel[x, y]; }
        catch { return false; }
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

    Dictionary<Point, HashSet<Point>>          nodes1                   = new();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(var keys in nodes1.Keys)
        {
            foreach(var line in nodes1[keys])
            {
                var p1 = new Vector3((float)keys.Y*roomSize_Width + 6, -((float)keys.X*roomSize_Height) - 9, -1);
                var p2 = new Vector3((float)line.Y*roomSize_Width + 6, -((float)line.X*roomSize_Height) - 9, -1);
                Gizmos.DrawLine(p1, p2);
            }
        }
        Gizmos.color = Color.green;

        foreach(var keys in nodes.Keys)
        {
            foreach(var line in nodes[keys])
            {
                var p1 = new Vector3((float)keys.Y*roomSize_Width + 6, -((float)keys.X*roomSize_Height) - 9, -1);
                var p2 = new Vector3((float)line.Y*roomSize_Width + 6, -((float)line.X*roomSize_Height) - 9, -1);
                Gizmos.DrawLine(p1, p2);
            }
        }

        for(int i=0; i<floorSize; i++)
        {
            for(int j=0; j<floorSize; j++)
            {
                if(m_selectedRoomTablel[i, j])
                {
                    var p = new Vector3((float)j*roomSize_Width + 6, -((float)i*roomSize_Height) - 9, -1);
                    Gizmos.DrawWireSphere(p, 3);
                }
            }
        }
        if(startPoint != null)
        {
            Gizmos.color = Color.blue;
            var sp = new Vector3((float)startPoint.Y*roomSize_Width + 6, -((float)startPoint.X*roomSize_Height) - 9, -1);
            Gizmos.DrawWireSphere(sp, 3);
        }
    }
}