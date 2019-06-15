using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarAlgorithm
{

    List<Waypoint> openList;
    List<Waypoint> closedList;
    List<Vector3Int> bestPath; //use for backtrack
    GridLayout layout;
    Tilemap tilemap;
    public List<Tilemap> collider;
    int[,] map;

    readonly Mask[] mask = new Mask[8];



    public AStarAlgorithm(GridLayout pLayout, Tilemap pMap, int searchArea)
    {
        layout = pLayout;
        tilemap = pMap;
        map = new int[searchArea, searchArea];
    }

    public class Mask
    {
        public int x;
        public int y;
        public float weight;

        public Mask(int x, int y, float weight)
        {
            this.x = x;
            this.y = y;
            this.weight = weight;
        }
    }

    public class Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
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


    public List<Waypoint> FindPath(Vector3 startPos, Vector3 endPos)
    {
        mask[0] = new Mask(-1, -1, 1.4f);
        mask[1] = new Mask(-1, 0, 1f);
        mask[2] = new Mask(-1, 1, 1.4f);
        mask[3] = new Mask(0, -1, 1f);
        mask[4] = new Mask(0, 1, 1f);
        mask[5] = new Mask(1, -1, 1.4f);
        mask[6] = new Mask(1, 0, 1f);
        mask[7] = new Mask(1, 1, 1.4f);

        bestPath = new List<Vector3Int>();
        List<Waypoint> path = new List<Waypoint>();
        //Tiles to check
        openList = new List<Waypoint>();
        //Tiles which do not need to be checked again
        closedList = new List<Waypoint>();
        Vector3Int start = layout.WorldToCell(startPos);
        Vector3Int end = layout.WorldToCell(endPos);
        openList.Add(new Waypoint(new Coord(start.x, start.y), 0, CalcH(new Coord(start.x, start.y), new Coord(end.x, end.y)), new Coord(start.x, start.y)));
        FindPath(new Coord(end.x, end.y));

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
            //if (walkable)
            //{
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
            //}
            



        }
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


    private bool IsWalkable(Vector2Int tilePos)
    {
        if(tilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0)) == null){
            return false;
        }
        return true;
    }

}
