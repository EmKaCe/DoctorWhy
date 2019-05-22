﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Test")]
public class BaseBehaviour : BehaviourNode
{

    public float test;
    public MonoBehaviour behaviour;

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        //rect = new Rect(position.x, position.y, width, height);
        //style = nodeStyle;
        //inPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint);
        //outPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        //defaultNodeStyle = nodeStyle;
        //selectedNodeStyle = selectedStyle;
        //OnRemoveNode = OnClickRemoveNode;
    }




    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUI.TextField(rect, test.ToString());

    }

}
