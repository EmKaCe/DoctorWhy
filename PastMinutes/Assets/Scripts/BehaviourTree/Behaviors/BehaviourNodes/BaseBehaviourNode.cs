using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BaseBehaviour;

public class BaseBehaviourNode : BehaviourNode
{

    public float test;
    public BaseBehaviour behaviour;
    

    private Vector2 pos;

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        pos = position;
        rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
    }


    public override string GetBehaviourType()
    {
        return behaviour.GetType().ToString();
    }
    


    public override void Draw()
    {
        base.Draw();


       

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
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }

    public override void SetChildState(State state, BehaviourNode childNode)
    {
        throw new NotImplementedException();
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }
}
