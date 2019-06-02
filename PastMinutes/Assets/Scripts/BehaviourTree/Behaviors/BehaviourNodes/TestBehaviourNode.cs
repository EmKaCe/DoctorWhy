﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviourNode : BehaviourNode
{

    public TestBehaviour testBehaviour;

    public void CreateTestBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, TestBehaviour test, int inPoints, int outPoints)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints);
        testBehaviour = test;
        testBehaviour.text = "Hello World";
    }

    public override void Draw()
    {
    foreach (BehaviourConnectionPoint i in inPoint)
    {
        i.Draw();
    }
    foreach (BehaviourConnectionPoint o in outPoint)
    {
        o.Draw();
    }
    GUI.Box(rect, title, style);
        GUI.Label(rect, "Test");

    }

    public override Type GetBehaviourType()
    {
        return testBehaviour.GetType();
    }


}
