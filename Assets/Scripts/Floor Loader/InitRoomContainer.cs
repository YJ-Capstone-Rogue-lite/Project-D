using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FloorLoader : MonoBehaviour
{
    private static class InitRoomContainer
    {
        public static readonly RoomContainer roomContainer;
        static InitRoomContainer()
        {
            roomContainer = new() {
                defaultRoomData = new RoomData[] {
                    new RoomData{
                        roomSize = new bool[,] { { true } },
                        path = "TileMap/Rooms/Default Room 1"
                    },
                    new RoomData{
                        roomSize = new bool[,] { { true } },
                        path = "TileMap/Rooms/Default Room 2"
                    }
                },
                overSizeRoomData = new RoomData[] {
                    new RoomData{
                        roomSize = new bool[,] { { true, false }, 
                                                 { true, true } },
                        path = "TileMap/Rooms/OverSize Room 1"               
                    },
                    new RoomData{
                        roomSize = new bool[,] { { true }, 
                                                 { true } },
                        path = "TileMap/Rooms/OverSize Room 2"               
                    },
                    new RoomData{
                        roomSize = new bool[,] { { true, true }, 
                                                 { true, true } },
                        path = "TileMap/Rooms/OverSize Room 3"               
                    },
                    new RoomData{
                        roomSize = new bool[,] { { true, true, true } },
                        path = "TileMap/Rooms/OverSize Room 4"               
                    }
                },
                specialRoomData = new RoomData[] {
                    new RoomData{
                        roomSize = new bool[,] { { true } },
                        path = "TileMap/Rooms/Special Room 1"
                    }
                }
            };
        }
    }
}