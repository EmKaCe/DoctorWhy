using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AITestScript : MonoBehaviour
{
    public Tilemap map;
    public GridLayout layout;
    public List<Tilemap> colliders;
    List<Vector3> path;
    bool move;
    public float speed;
    public Tilemap t;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        Vector3Int playerPos = layout.WorldToCell(new Vector3(70, 51, -1.5f));
        Vector3Int layerPos = layout.WorldToCell(new Vector3(72, 60, -1.5f));
        
        Vector3Int emptyPos = layout.WorldToCell(new Vector3(90, 60, 0));
        AStarAlgorithm a = new AStarAlgorithm(layout, map, colliders);
        path = a.FindPath(new Vector3(90, 60, 0), new Vector3(0, 0, 0));
        Vector3 goal = path[0];
        goal.z = transform.position.z;
        transform.position = goal;
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
        Vector3 goal = path[0];
        goal.z = transform.position.z;
        if (Mathf.Abs(goal.x - transform.position.x) < 0.1f && Mathf.Abs(goal.y - transform.position.y) < 0.1f)
        {
            Debug.Log("Pos Reached");
            path.RemoveAt(0);
            if(path.Count == 0)
            {
                move = false;
                return;
            }
            goal = path[0];
            goal.z = transform.position.z;
        }
        float distance = 0f;
        float walkingDistance = speed * Time.deltaTime;
        //Vector3 direction = new Vector3(path[0].x - npc.transform.position.x, path[0].y - npc.transform.position.y, 0);
        //if(distance < walkingDistance)
        //{
        //    float remaining = walkingDistance - distance;
        //}
        //else
        //{
            transform.position = Vector3.Lerp(transform.position, goal, walkingDistance);
        //}
        


    }
}
