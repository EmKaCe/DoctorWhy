using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patrol", menuName = "BehaviourTree/Patrol")]
public class Patrol : ScriptableObject
{
    public bool loop;
    public List<Vector3> waypoints;

    public List<Vector3> GetWaypoints()
    {
        return new List<Vector3>(waypoints);
    }

}
