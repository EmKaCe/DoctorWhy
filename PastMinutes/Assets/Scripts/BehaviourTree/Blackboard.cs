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

    public void Initialize()
    {
        
        positions = new Dictionary<string, Vector3>();
        waypoints = new Dictionary<string, List<Vector3>>();
        Debug.Log("bae");

    }
}
