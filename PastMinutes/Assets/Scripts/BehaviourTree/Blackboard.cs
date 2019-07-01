using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BehaviourTreeBlackboard", menuName ="BehaviourTree/BehaviourTreeBlackboard")]
public class Blackboard : ScriptableObject
{
    public Dictionary<string, Vector3> positions;
    public Dictionary<string, List<Vector3>> waypoints;
    public GridLayout layout;
    public Tilemap map;
    public List<Tilemap> collider;
    public Dictionary<string, Patrol> patrolRoutes;
    public Dictionary<string, int> enemies;
    public Dictionary<string, float> distances;
    /// <summary>
    /// should something be updated every tick
    /// </summary>
    public Dictionary<string, bool> tickUpdate;

    public void Initialize()
    {
        
        positions = new Dictionary<string, Vector3>();
        waypoints = new Dictionary<string, List<Vector3>>();
        patrolRoutes = new Dictionary<string, Patrol>();
        enemies = new Dictionary<string, int>();
        distances = new Dictionary<string, float>();
        tickUpdate = new Dictionary<string, bool>();

    }
}
