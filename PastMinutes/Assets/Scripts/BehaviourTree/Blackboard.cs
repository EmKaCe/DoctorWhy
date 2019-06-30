using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourTreeBlackboard", menuName ="BehaviourTree/BehaviourTreeBlackboard")]
public class Blackboard : ScriptableObject
{
    public Dictionary<string, Vector3> positions;

    public void Initialize()
    {
        positions = new Dictionary<string, Vector3>();
    }
}
