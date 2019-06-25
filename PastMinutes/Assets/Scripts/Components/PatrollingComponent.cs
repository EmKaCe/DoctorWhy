using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PatrollingComponent : MonoBehaviour
{
    public Tilemap map;
    public GridLayout layout;
    public List<Tilemap> colliders;
    public float speed;
    public Vector3[] waypoints;
    bool move;
    List<Vector3> path;
    Vector3 start;
    public bool goBackToStart;
    // Start is called before the first frame update
    void Start()
    {
        path = new List<Vector3>();
        move = false;
        AStarAlgorithm a = new AStarAlgorithm(layout, map, colliders);
        path.AddRange(a.FindPath(transform.position, waypoints[0]));
        for(int i = 0; i < waypoints.Length - 1; i++)
        {
            path.AddRange(a.FindPath(waypoints[i], waypoints[i + 1]));
        }
        if (goBackToStart)
        {
            path.AddRange(a.FindPath(waypoints[waypoints.Length - 1], transform.position));
        }
        Vector3 goal = path[0];
        goal.z = transform.position.z;
        transform.position = goal;
        start = goal;
        path.RemoveAt(0);
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            MoveToPosition();
        }
        
    }

    public void MoveToPosition()
    {
        Vector3 goal = NextPoint();
        float distance = (goal - transform.position).magnitude;
        float walkingDistance = speed * Time.deltaTime;
        ////Vector3 direction = new Vector3(path[0].x - npc.transform.position.x, path[0].y - npc.transform.position.y, 0);
        //if (distance < walkingDistance)
        //{
        //    float remaining = walkingDistance - distance;
        //    MoveToPosition(goal, distance);
        //    goal = NextPoint();
        //    if (move)
        //    {
        //        Debug.Log("Split Movement");
        //        MoveToPosition(goal, remaining);
        //    }

        //}
        //else
        // {
        MoveToPosition(goal, walkingDistance);
        //}

    }

    public Vector3 NextPoint()
    {
        Vector3 goal = path[0];
        goal.z = transform.position.z;
        if (Mathf.Abs(goal.x - transform.position.x) < 0.1f && Mathf.Abs(goal.y - transform.position.y) < 0.1f)
        {
            start = goal;
            path.RemoveAt(0);
            if (path.Count == 0)
            {
                move = false;
                return new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            goal = path[0];
            goal.z = transform.position.z;

        }
        return goal;
    }

    public void MoveToPosition(Vector3 goal, float distance)
    {
        transform.position = Vector3.Lerp(transform.position, goal, distance);
        //var i = 0.0;
        //var rate = 1.0 / Time.time;
        //while (i < 1.0)
        //{
        //    i += Time.deltaTime * rate;
        //    transform.position = Vector3.Lerp(transform.position, endPos, i);
    }
}
