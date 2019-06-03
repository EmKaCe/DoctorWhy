using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionSave : ScriptableObject
{
    public BehaviourNode startNode;
    public BehaviourNode endNode;

    public int inIndex;
    public int outIndex;

    public void Init(BehaviourConnection connection)
    {
        startNode = connection.inPoint.node;
        endNode = connection.outPoint.node;
        inIndex = connection.inPoint.index;
        outIndex = connection.outPoint.index;
    }


}
