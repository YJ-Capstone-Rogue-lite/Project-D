using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FloorLoader : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void InitRoomContainer()
    {
        RoomContainer temp = new();
        temp.defaultRoomData = new RoomData[] {
            new RoomData{
                roomSize = new int[] { 1, 1 },
                tileTable = new TileType[,] {
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL }
                },
                objectTable = new GameObject[,] {
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }
            }
        };
        temp.overSizeRoomData = new RoomData[] {
            new RoomData{
                roomSize = new int[] { 2, 1 },
                tileTable = new TileType[,] {
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL }
                },
                objectTable = new GameObject[,] {
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }
            }
        };
        temp.specialRoomData = new RoomData[] {
            new RoomData{
                roomSize = new int[] { 1, 1 },
                tileTable = new TileType[,] {
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.FLOOR, TileType.FLOOR, TileType.WALL },
                    { TileType.WALL, TileType.WALL, TileType.WALL, TileType.WALL }
                },
                objectTable = new GameObject[,] {
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }
            }
        };
    }
}