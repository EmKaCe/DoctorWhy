using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Test")]
public class BaseBehaviour : BehaviourNode
{

    public float test;
    public MonoBehaviour behaviour;
    public Rect rectUnlockLabel;
    public Rect rectTitle;
    public Rect rectBehaviour;
    public Rect rectFloatValue;


    private Vector2 pos;

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        pos = position;
        rectUnlockLabel = new Rect(position.x + offset, position.y + 3 * rowHeight, width / 2, rowHeight);
        rectTitle = new Rect(position.x + width / 2, position.y, width, rowHeight);
        rectBehaviour = new Rect(position.x + offset, position.y + rowHeight, width, rowHeight);
        rectFloatValue = new Rect(position.x + offset, position.y + 2 * rowHeight, width, rowHeight);
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
        //bool fold = true;
        inPoint.Draw();
        outPoint.Draw();

        //EditorGUI.InspectorTitlebar(new Rect(pos.x, pos.y, 100, 200), true, this, true);


        //fold = EditorGUILayout.InspectorTitlebar(fold, this);
        //if (fold)
        //{
        //    test = EditorGUILayout.FloatField(test);
        //}

        //EditorGUILayout.Space();

        //EditorGUILayout.LabelField("Add state to modify:");

        //GUILayout.Window(100, rect, DrawWindow, "TestWindow");


        GUI.Box(rect, title, style);
        GUI.Label(rectUnlockLabel, "Unlocked: ");
        GUI.Label(rectTitle, "BaseNode");
        GUILayout.BeginArea(rectFloatValue);
        //GUILayout.TextField(test.ToString());
        test = EditorGUILayout.FloatField(test);
        GUILayout.EndArea();

        //GUI.BeginGroup(rect);
        ////GUI.Box(rect, title, style);
        //GUI.TextField(new Rect(0, 0, rect.width, rect.height / 2), test.ToString());
        //GUI.EndGroup();

    }

    private void DrawWindow(int windowID)
    {
        GUI.TextField(rect, test.ToString());
    }

    public override void Drag(Vector2 delta)
    {
        base.Drag(delta);
        rectUnlockLabel.position += delta;
        rectTitle.position += delta;
        rectBehaviour.position += delta;
        rectFloatValue.position += delta;
    }


}
