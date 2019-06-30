using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourComponent : MonoBehaviour
{
    //Change to StartNode
    public BehaviourNode startNode;
    public Blackboard blackboard;
    void Start()
    {
        blackboard.Initialize();
        startNode.Initialize(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        startNode.Run();
    }
}
