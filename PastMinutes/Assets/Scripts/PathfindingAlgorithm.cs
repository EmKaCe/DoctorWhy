using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingAlgorithm
{
    //https://www.raywenderlich.com/3016-introduction-to-a-pathfinding
    //https://arongranberg.com/astar/
    //List<Vector3Int> openList;
    //Dictionary<Vector3Int, float> openList;
    //Dictionary<Vector3Int, float> closedList;
    List<Waypoint> openList;
    List<Waypoint> closedList;
    List<Vector3Int> bestPath; //use for backtrack
    GridLayout layout;
    Tilemap map;
    readonly Mask[] mask = new Mask[8];

    public class Waypoint
    {
        public Vector3Int tile;
        public float distance;
        public Direction parent;

        public Waypoint(Vector3Int pTile, float pDistance, Direction pParent)
        {
            tile = pTile;
            distance = pDistance;
            parent = pParent;
        }
        public Waypoint()
        {
            distance = 10000;
        }
    }

    public enum Direction
    {
        north,
        northEast,
        east,
        southEast,
        south,
        southWest,
        west,
        northWest,
        start,
    }

    public class Mask
    {
        public int x;
        public int y;
        public float weight;
        public Direction direction;

        public Mask(int x, int y, float weight, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.weight = weight;
            this.direction = direction;
        }
    }


    public PathfindingAlgorithm(GridLayout pLayout, Tilemap pMap)
    {
        layout = pLayout;
        map = pMap;
    }

    public List<Waypoint> FindPath(Vector3 startPos, Vector3 endPos)
    {
        mask[0] = new Mask(-1, -1, 1.4f, Direction.northWest);
        mask[1] = new Mask(-1, 0, 1f, Direction.west);
        mask[2] = new Mask(-1, 1, 1.4f, Direction.southWest);
        mask[3] = new Mask(0, -1, 1f, Direction.north);
        mask[4] = new Mask(0, 1, 1f, Direction.south);
        mask[5] = new Mask(1, -1, 1.4f, Direction.northEast);
        mask[6] = new Mask(1, 0, 1f, Direction.east);
        mask[7] = new Mask(1, 1, 1.4f, Direction.southEast);



        bestPath = new List<Vector3Int>();
        List<Waypoint> path = new List<Waypoint>();
        //Tiles to check
        openList = new List<Waypoint>();
        //Tiles which do not need to be checked again
        closedList = new List<Waypoint>();
        Vector3Int start = layout.WorldToCell(startPos);
        Vector3Int end = layout.WorldToCell(endPos);
        openList.Add(new Waypoint(start, 0, Direction.start));
        FindPath(end);

        //backtracking
        Waypoint w = closedList[closedList.Count];
        Vector3Int parentTile = new Vector3Int(0,0,0);
        bestPath.Add(w.tile);
        while(w.parent != Direction.start)
        {
            switch (w.parent)
            {
                case Direction.north:
                    parentTile = new Vector3Int(w.tile.x, w.tile.y + 1, w.tile.z);
                    break;
                case Direction.northEast:
                    parentTile = new Vector3Int(w.tile.x - 1, w.tile.y + 1, w.tile.z);
                    break;
                case Direction.east:
                    parentTile = new Vector3Int(w.tile.x - 1, w.tile.y, w.tile.z);
                    break;
                case Direction.southEast:
                    parentTile = new Vector3Int(w.tile.x - 1, w.tile.y - 1, w.tile.z);
                    break;
                case Direction.south:
                    parentTile = new Vector3Int(w.tile.x, w.tile.y - 1, w.tile.z);
                    break;
                case Direction.southWest:
                    parentTile = new Vector3Int(w.tile.x + 1, w.tile.y - 1, w.tile.z);
                    break;
                case Direction.west:
                    parentTile = new Vector3Int(w.tile.x + 1, w.tile.y, w.tile.z);
                    break;
                case Direction.northWest:
                    parentTile = new Vector3Int(w.tile.x + 1, w.tile.y + 1, w.tile.z);
                    break;
            }
            w = closedList.Find(wp => wp.tile == parentTile);
            bestPath.Add(w.tile);
        }
        bestPath.Reverse();
        foreach(Vector3Int t in bestPath)
        {
            Debug.Log(t.ToString() + " " + layout.CellToWorld(t));
        }
        return path;
    }

    private void FindPath(Vector3Int endPos)
    {
        Waypoint start = FindClosestTile();
        openList.Remove(start);
        closedList.Add(start);
        for(int i = 0; i < 8; i++)
        {
            Vector3Int point = new Vector3Int(start.tile.x + mask[i].x, start.tile.y + mask[i].y, start.tile.z);
            if(point == endPos)
            {
                closedList.Add(new Waypoint(point, 0, mask[i].direction));
                return;
            }
            if (closedList.Exists(w => w.tile.Equals(point)))
            {
                //Do nothing
            }
            else if (openList.Exists(w => w.tile.Equals(point)))
            {
                Waypoint wp = openList.Find(w => w.tile.Equals(point));
                float pointValue = wp.distance;
                float newPointValue = CalcF(start, wp.tile, mask[i].weight);
                if (pointValue > newPointValue)
                {
                    openList.Remove(wp);
                    wp.parent = mask[i].direction;
                    openList.Add(wp);
                }
            }
            else
            {
                openList.Add(new Waypoint(point, CalcF(start, point, mask[i].weight), mask[i].direction));
            }
            FindPath(endPos);
        }


        //openList.Add(new Vector3Int(start.Key.x - 1, start.Key.y - 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x - 1, start.Key.y - 1, start.Key.z), 1.4f));
        //openList.Add(new Vector3Int(start.Key.x, start.Key.y - 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x, start.Key.y - 1, start.Key.z), 1f));
        //openList.Add(new Vector3Int(start.Key.x + 1, start.Key.y - 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x + 1, start.Key.y - 1, start.Key.z), 1.4f));
        //openList.Add(new Vector3Int(start.Key.x - 1, start.Key.y, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x - 1, start.Key.y, start.Key.z), 1f));
        //openList.Add(new Vector3Int(start.Key.x + 1, start.Key.y, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x + 1, start.Key.y, start.Key.z), 1f));
        //openList.Add(new Vector3Int(start.Key.x - 1, start.Key.y + 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x - 1, start.Key.y + 1, start.Key.z), 1.4f));
        //openList.Add(new Vector3Int(start.Key.x, start.Key.y + 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x, start.Key.y + 1, start.Key.z), 1f));
        //openList.Add(new Vector3Int(start.Key.x + 1, start.Key.y + 1, start.Key.z), CalcF(start.Key, new Vector3Int(start.Key.x + 1, start.Key.y + 1, start.Key.z), 1.4f));
    }

    private Waypoint FindClosestTile()
    {
        Waypoint waypoint = new Waypoint();
        float distance = 10000;
        foreach(Waypoint w in openList)
        {
            if(w.distance <= distance)
            {
                distance = w.distance;
                waypoint = w;
            }
        }
        return waypoint;
    }

    private float CalcH(Waypoint start, Vector3Int end)
    {
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(end.x - start.tile.x, 2) + Mathf.Pow(end.y - start.tile.y, 2)));
    }

    private float CalcF(Waypoint start, Vector3Int end, float endWeight)
    {
        return start.distance + CalcH(start, end) + endWeight;
    }

}
