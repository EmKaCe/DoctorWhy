using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourEditorNodes : ScriptableObject
{

    public List<BehaviourNode> nodes;
    public List<BehaviourConnection> connections;
    public List<NodeSaver> nodes2;

    public class NodeSaver
    {
        public BehaviourNode node;
        public BehaviourConnectionPoint inPoint;
        public BehaviourConnectionPoint outPoint;

        public NodeSaver(BehaviourNode node, BehaviourConnectionPoint inPoint, BehaviourConnectionPoint outPoint)
        {
            this.node = node;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
        }

    }


    public BehaviourEditorNodes()
    {
        nodes2 = new List<NodeSaver>();
        nodes = new List<BehaviourNode>();
        connections = new List<BehaviourConnection>();
    }
}
