using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviourNode : BehaviourNode
{

    public TestBehaviour testBehaviour;

    public void CreateTestBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, TestBehaviour test)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        testBehaviour = test;
        testBehaviour.text = "Hello World";
    }

    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUI.Label(rect, "Test");

    }

    public override void OnBehaviourResult(BaseBehaviour.State state)
    {
        throw new NotImplementedException();
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}
