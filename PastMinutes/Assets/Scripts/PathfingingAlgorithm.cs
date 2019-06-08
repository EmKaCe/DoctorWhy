using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfingingAlgorithm
{
    //https://www.raywenderlich.com/3016-introduction-to-a-pathfinding
    //https://arongranberg.com/astar/
    //List<Vector3Int> openList;
    Dictionary<Vector3Int, float> openList;
    Dictionary<Vector3Int, float> closedList;
    GridLayout layout;
    Tilemap map;
    Mask[] mask = new Mask[8];

    public class Waypoint
    {

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




    public List<Waypoint> FindPath(Vector3 startPos, Vector3 endPos, float startValue)
    {
        mask[0] = new Mask(-1, -1, 1.4f);
        mask[1] = new Mask(-1, 0, 1f);
        mask[2] = new Mask(-1, 1, 1.4f);
        mask[3] = new Mask(0, -1, 1f);
        mask[4] = new Mask(0, 1, 1f);
        mask[5] = new Mask(1, -1, 1.4f);
        mask[6] = new Mask(1, 0, 1f);
        mask[7] = new Mask(1, 1, 1.4f);




        List<Waypoint> path = new List<Waypoint>();
        //Tiles to check
        openList = new Dictionary<Vector3Int, float>();
        //Tiles do not need to be checked again
        closedList = new Dictionary<Vector3Int, float>();
        Vector3Int start = layout.WorldToCell(startPos);
        openList.Add(start, startValue);


        return path;
    }

    private void FindPath()
    {
        KeyValuePair<Vector3Int, float> start = FindClosestTile();
        openList.Remove(start.Key);
        closedList.Add(start.Key, start.Value);
        for(int i = 0; i < 8; i++)
        {
            Vector3Int point = new Vector3Int(start.Key.x + mask[i].x, start.Key.y, start.Key.z);
            if (closedList.ContainsKey(point))
            {
                //Do nothing
            }
            else if (openList.ContainsKey(point))
            {
                openList.TryGetValue(point, out float pointValue);
                float newPointValue = CalcF(start.Key, point, mask[i].weight);
                if (pointValue > newPointValue)
                {
                    openList[point] = newPointValue;
                }
            }
            else
            {
                openList.Add(point, CalcF(start.Key, point, mask[i].weight));
            }
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

    private KeyValuePair<Vector3Int, float> FindClosestTile()
    {
        Vector3Int pos = new Vector3Int();
        float distance = 10000;
        foreach(KeyValuePair<Vector3Int, float> pair in openList)
        {
            if(pair.Value < distance)
            {
                distance = pair.Value;
                pos = pair.Key;
            }
        }
        return new KeyValuePair<Vector3Int, float>(pos, distance);
    }

    private float CalcH(Vector3Int start, Vector3Int end)
    {
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.y - start.y, 2)));
    }

    private float CalcF(Vector3Int start, Vector3Int end, float endWeight)
    {
        openList.TryGetValue(start, out float startValue);
        return startValue + CalcH(start, end) + endWeight;
    }

}
