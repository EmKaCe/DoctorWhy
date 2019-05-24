using System;
using System.Collections;
using System.Collections.Generic;
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

    public void CreateBaseBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, GameObject behaviourStorage)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        pos = position;
        rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
        serializedBehaviour = new SerializedObject(this).FindProperty("behaviour");
        behaviourStorage.AddComponent<BaseBehaviour>();
    }

    


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();


        GUI.Box(rect, title, style);

        GUI.Label(rectTitle, "BaseNode");
        GUILayout.BeginArea(rectContent);
        GUILayout.Space(rowHeight);
        EditorGUIUtility.labelWidth = 75;

        EditorGUILayout.PropertyField(serializedBehaviour, new GUIContent("Behaviour"));

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

    public override void Run()
    {
        throw new NotImplementedException();
    }

    public override void OnBehaviourResult(State state)
    {
        if(state == State.success)
        {
            Debug.Log("BaseBehaviourNode: Success");
        }
        else if(state == State.failure)
        {
            Debug.Log("BaseBehaviourNode: Failure");
        }
    }
}
