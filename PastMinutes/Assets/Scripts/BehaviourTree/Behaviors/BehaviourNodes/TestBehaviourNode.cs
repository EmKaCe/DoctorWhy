using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviourNode : BehaviourNode
{

    public TestBehaviour testBehaviour;



#if UNITY_EDITOR
    public void CreateTestBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, TestBehaviour test, int inPoints, int outPoints, string nodeName)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        testBehaviour = test;
        testBehaviour.text = "Hello World";
    }

    public override void Draw()
    {
        base.Draw();
        GUI.Label(rect, "Test");

    }
#endif
    public override string GetBehaviourType()
    {
        return testBehaviour.GetType().ToString();
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        throw new NotImplementedException();
    }
}
