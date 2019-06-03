using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfingingAlgorithm
{
    //https://www.raywenderlich.com/3016-introduction-to-a-pathfinding
    //https://arongranberg.com/astar/
    List<Vector3Int> openList;
    List<Vector3Int> closedList;
    GridLayout layout;
    Tilemap map;

    public class Waypoint
    {

    }




    public List<Waypoint> FindPath(Vector3 startPos, Vector3 endPos)
    {
        List<Waypoint> path = new List<Waypoint>();
        //Tiles to check
        openList = new List<Vector3Int>();
        //Tiles do not need to be checked again
        closedList = new List<Vector3Int>();
        Vector3Int start = layout.WorldToCell(startPos);
        closedList.Add(start);
        openList.Add(new Vector3Int(start.x - 1, start.y -1, start.z));
        openList.Add(new Vector3Int(start.x, start.y - 1, start.z));
        openList.Add(new Vector3Int(start.x + 1, start.y + 1, start.z));
        openList.Add(new Vector3Int(start.x - 1, start.y, start.z));
        openList.Add(new Vector3Int(start.x + 1, start.y, start.z));
        openList.Add(new Vector3Int(start.x, start.y, start.z));
        openList.Add(new Vector3Int(start.x, start.y, start.z));
        openList.Add(new Vector3Int(start.x, start.y, start.z));

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                Vector3Int tile = new Vector3Int(start.x + x, start.y + y, start.z);
                TileBase t = map.GetTile(tile);
            }
        }




        return path;
    }

}
