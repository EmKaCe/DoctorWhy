using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionSave : ScriptableObject
{
    public BehaviourNode startNode;
    public BehaviourNode endNode;

    public void Init(BehaviourConnection connection)
    {
        startNode = connection.inPoint.node;
        endNode = connection.outPoint.node;
    }


}
