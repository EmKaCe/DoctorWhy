using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static BaseBehaviour;

public class AIComponent : MonoBehaviour
{

    public NodeSaver behaviourTree;
    public StartBehaviourNode start;
    private BaseBehaviour behaviour;
    

    // Start is called before the first frame update
    void Start()
    {
        FindStartNode();
        string t = start.outPoint[0].connectedNode.GetBehaviourType();
        behaviour = gameObject.AddComponent(Type.GetType(t)) as BaseBehaviour;
        behaviour.ActivateBehaviour();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindStartNode()
    {
        UnityEngine.Object[] nodes = AssetDatabase.LoadAllAssetsAtPath(behaviourTree.path);
        foreach(UnityEngine.Object node in nodes)
        {
            if (node.GetType().Equals(typeof(StartBehaviourNode))){
                start = node as StartBehaviourNode;
                return;
            }
        }
        this.enabled = false;
    }

    public void TreeDone(State state)
    {
        if(state == State.failure)
        {

        }
        else if(state == State.success)
        {

        }
    }
}
