using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehaviourNode : BehaviourNode
{
    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public override string GetBehaviourType()
    {
        throw new NotImplementedException();
    }

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}
