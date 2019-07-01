using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(FactionComponent), typeof(EntityComponent), typeof(CapsuleCollider2D))]
public class BehaviourComponent : MonoBehaviour
{
    //Change to StartNode
    public StartBehaviourNode startNode;
    public GridLayout layout;
    public Tilemap map;
    public List<Tilemap> collision;
    public Blackboard blackboard;
    public float viewDistance;
    public float movementSpeed;
    public Patrol patrol;
    private string id;
    void Start()
    {
        blackboard.Initialize();

        List<Vector3> waypoints = new List<Vector3>
        {
            new Vector3(2, 2, 0),
            new Vector3(29, -10, 0),
            new Vector3(30, 1, 0)
        };
        id = gameObject.GetInstanceID().ToString();
        blackboard.waypoints.Add("Waypoint", waypoints);
        blackboard.map = map;
        blackboard.layout = layout;
        blackboard.collider = collision;
        blackboard.patrolRoutes.Add("Waypoint" + id, patrol);
        blackboard.distances.Add("Visual" + id, viewDistance);
        blackboard.tickUpdate.Add("Enemy" + id, true);
        blackboard.tickUpdate.Add("Patrol" + id, false);
        blackboard.distances.Add("Melee" + id, gameObject.GetComponent<PlayerComponent>().meleeRange);
        startNode.Initialize(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        startNode.Init();
        startNode.Run();
    }

    public string GetID()
    {
        return id;
    }

}
