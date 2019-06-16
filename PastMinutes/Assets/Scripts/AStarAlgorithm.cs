using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarAlgorithm
{

    List<Waypoint> openList;
    List<Waypoint> closedList;
    GridLayout layout;
    Tilemap tilemap;
    public List<Tilemap> collider;

    readonly Mask[] mask = new Mask[8];



    public AStarAlgorithm(GridLayout pLayout, Tilemap pMap, List<Tilemap> collider)
    {
        layout = pLayout;
        tilemap = pMap;
        this.collider = collider;
    }

    public class Mask
    {
        public int x;
        public int y;
        public float weight;
        public bool diagonal;

        public Mask(int x, int y, float weight, bool diagonal)
        {
            this.x = x;
            this.y = y;
            this.weight = weight;
            this.diagonal = diagonal;
        }
    }

    public class Coord : IEquatable<Coord>
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public bool Equals(Coord c)
        {
            if(x == c.x && y == c.y)
            {
                return true;
            }
            return false;
        }
    }

    public class Waypoint : IEquatable<Waypoint>
    {
        public Coord position;
        public float g;
        public float h;
        public Coord parent;

        public Waypoint(Coord position, float g, float h, Coord parent)
        {
            this.position = position;
            this.g = g;
            this.h = h;
            this.parent = parent;
        }

        public bool Equals(Waypoint p)
        {
            if (position.Equals(p.position))
            {
                return true;
            }
            return false;
        }
    }


    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
    {
        if(startPos == endPos)
        {
            return new List<Vector3>();
        }
        mask[0] = new Mask(-1, -1, 1.4f, true);
        mask[1] = new Mask(-1, 0, 1f, false);
        mask[2] = new Mask(-1, 1, 1.4f, true);
        mask[3] = new Mask(0, -1, 1f, false);
        mask[4] = new Mask(0, 1, 1f, false);
        mask[5] = new Mask(1, -1, 1.4f, true);
        mask[6] = new Mask(1, 0, 1f, false);
        mask[7] = new Mask(1, 1, 1.4f, true);

        List<Vector3> path = new List<Vector3>();
        //Tiles to check
        openList = new List<Waypoint>();
        //Tiles which do not need to be checked again
        closedList = new List<Waypoint>();
        Vector3Int start = layout.WorldToCell(startPos);
        Vector3Int end = layout.WorldToCell(endPos);
        openList.Add(new Waypoint(new Coord(start.x, start.y), 0, CalcH(new Coord(start.x, start.y), new Coord(end.x, end.y)), new Coord(start.x, start.y)));
        FindPath(new Coord(end.x, end.y));

        path = Backtrack();
        path.Reverse();
        foreach(Vector3 v in path)
        {
            Debug.Log(v);
        }

        return path;
    }

    public void FindPath(Coord end)
    {
        Waypoint start = FindClosestTile();
        openList.Remove(start);
        closedList.Add(start);
        for(int i = 0; i < 8; i++)
        {
            Waypoint p = new Waypoint(new Coord(start.position.x + mask[i].x, start.position.y + mask[i].y), start.g + mask[i].weight, 0, start.position);
            //return if end is reached
            if (p.position.Equals(end))
            {
                closedList.Add(p);
                return;
            }
            float z = tilemap.GetComponent<Transform>().position.z;

            //Calc H in case that p isn't in openList or is replacing old one
            p.h = CalcH(p.position, end);
            if (IsWalkable(new Vector2Int(p.position.x, p.position.y), new Vector2Int(start.position.x, start.position.y), mask[i].diagonal))
            {
                if (closedList.Contains(p))
                {

                }
                else if (openList.Contains(p))
                {
                    Waypoint oldP = openList.Find(w => w.Equals(p));
                    float distance = CalcF(p);
                    if(distance < CalcF(oldP))
                    {
                        openList.Remove(oldP);
                        openList.Add(p);
                    }
                }
                else
                {
                    openList.Add(p);
                }
            }
        }
        FindPath(end);

        
    }

    private Waypoint FindClosestTile()
    {
        Waypoint waypoint = new Waypoint(new Coord(0, 0), 10000, 10000, new Coord(0,1));
        float distance = 10000;
        foreach (Waypoint w in openList)
        {
            if ((w.g + w.h) <= distance)
            {
                distance = (w.g + w.h);
                waypoint = w;
            }
        }
        return waypoint;
    }

    private float CalcH(Coord start, Coord end)
    {
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.y - start.y, 2)));
    }

    private float CalcF(Waypoint start)
    {
        return start.g + start.h;
    }


    public bool IsWalkable(Vector2Int tilePos, Vector2Int startPos, bool diagonal)
    {
        if(tilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0)) == null){
            return false;
        }
        foreach(Tilemap map in collider)
        {
            if (map.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0)) != null)
            {
                return false;
            }
            if (diagonal && !CheckNeighbouringTiles(startPos, tilePos, map))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks neighbouring tiles when moving diagonal
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    private bool CheckNeighbouringTiles(Vector2Int origin, Vector2Int goal, Tilemap map)
    {
        if(map.GetTile(new Vector3Int(origin.x,goal.y, 0)) != null || map.GetTile(new Vector3Int(goal.x, origin.y, 0)))
        {
            return false;
        }
        return true;
    }

    private List<Vector3> Backtrack()
    {
        List<Vector3> path = new List<Vector3>();
        //First element has to be starting square
        Coord start = closedList[0].position;
        //Last element has to be final square        
        Waypoint currentTile = closedList[closedList.Count - 1];

        path = Backtrack(currentTile, start, path);
        return path;
    }

    private List<Vector3> Backtrack(Waypoint currentWaypoint, Coord startPos, List<Vector3> path)
    {
        //path.Add(layout.CellToWorld(new Vector3Int(currentWaypoint.position.x, currentWaypoint.position.y, 0)));
        path.Add(tilemap.GetCellCenterWorld(new Vector3Int(currentWaypoint.position.x, currentWaypoint.position.y, 0)));
        if (!currentWaypoint.parent.Equals(startPos))
        {
            currentWaypoint = closedList.Find(w => w.position.Equals(currentWaypoint.parent));
            Backtrack(currentWaypoint, startPos, path);
        }
        else
        {
            Waypoint wp = closedList.Find(w => w.position.Equals(startPos));
            path.Add(tilemap.GetCellCenterWorld(new Vector3Int(wp.position.x, wp.position.y, 0)));
           // path.Add(layout.CellToWorld(new Vector3Int(wp.position.x, wp.position.y, 0)));
        }
        return path;
    }

}
