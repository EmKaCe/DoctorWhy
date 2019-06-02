﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BaseBehaviour;

[CreateAssetMenu(menuName ="Test")]
public class BaseBehaviourNode : BehaviourNode
{

    public float test;
    public BaseBehaviour behaviour;
    public Rect rectContent;

    private SerializedProperty serializedBehaviour;

    private Vector2 pos;

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints);
        pos = position;
        rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
        serializedBehaviour = new SerializedObject(this).FindProperty("behaviour");
    }


    public override Type GetBehaviourType()
    {
        return behaviour.GetType();
    }
    


    public override void Draw()
    {
        //inPoint.Draw();
        //outPoint.Draw();
        foreach(BehaviourConnectionPoint i in inPoint)
        {
            i.Draw();
        }
        foreach(BehaviourConnectionPoint o in outPoint)
        {
            o.Draw();
        }


        GUI.Box(rect, title, style);

        GUI.Label(rectTitle, "BaseNode");
        GUILayout.BeginArea(rectContent);
        GUILayout.Space(rowHeight);
        EditorGUIUtility.labelWidth = 75;

        //EditorGUILayout.PropertyField(serializedBehaviour, new GUIContent("Behaviour"));

        test = EditorGUILayout.FloatField("TestValue", test);
        GUILayout.EndArea();


    }

    private void DrawWindow(int windowID)
    {
        GUI.TextField(rect, test.ToString());
    }

    public override void Drag(Vector2 delta)
    {
        base.Drag(delta);
        rectTitle.position += delta;
        rectContent.position += delta;
    }


}
