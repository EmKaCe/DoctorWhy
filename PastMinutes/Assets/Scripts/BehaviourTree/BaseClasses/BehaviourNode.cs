﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BaseBehaviour;

public abstract class BehaviourNode : ScriptableObject
{
    [HideInInspector]
    public Rect rect;
    [HideInInspector]
    public Rect rectTitle;
    [HideInInspector]
    public string title;
    [HideInInspector]
    public bool isDragged;
    [HideInInspector]
    public bool isSelected;

    [HideInInspector]
    public GUIStyle style;
    [HideInInspector]
    public GUIStyle defaultNodeStyle;
    [HideInInspector]
    public GUIStyle selectedNodeStyle;

    [HideInInspector]
    public BehaviourConnectionPoint[] inPoint;
    [HideInInspector]
    public BehaviourConnectionPoint[] outPoint;


    //New part
    public BehaviourNode[] children;
    public BehaviourNode[] parents;
    public State state;
    

    //[HideInInspector]
    //public BehaviourConnectionPoint inPoint;
    //[HideInInspector]
    //public BehaviourConnectionPoint outPoint;

    public Action<BehaviourNode> OnRemoveNode;

    public BehaviourNode parent;

    [HideInInspector]
    public readonly float rowHeight = 20f;
    [HideInInspector]
    public readonly float offset = 10;

    private readonly float titleOffset = 10;

    public int inPoints;
    public int outPoints;

    public void CreateBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints)
    {
        children = new BehaviourNode[outPoints];
        rect = new Rect(position.x, position.y, width, height);
        rectTitle = new Rect(position.x + offset, position.y + titleOffset, width - 2 * offset, rowHeight);
        style = nodeStyle;
        this.inPoints = inPoints;
        this.outPoints = outPoints;
        inPoint = new BehaviourConnectionPoint[inPoints];
        outPoint = new BehaviourConnectionPoint[outPoints];
        for(int i = 0; i < inPoint.Length; i++)
        {
            inPoint[i] = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint, i, inPoint.Length);
        }
        for(int i = 0; i < outPoint.Length; i++)
        {
            outPoint[i] = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint, i, inPoint.Length);
        }
        //inPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint);
        //outPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
    }



    public virtual void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public virtual void Draw()
    {
        GUI.Box(rect, title, style);
        foreach (BehaviourConnectionPoint i in inPoint)
        {
            i.Draw();
        }
        foreach (BehaviourConnectionPoint o in outPoint)
        {
            o.Draw();
        }
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        OnRemoveNode?.Invoke(this);
    }


    public abstract string GetBehaviourType();

    public abstract void Run();

    /// <summary>
    /// Call to initialize BehaviourTree
    /// </summary>
    /// <param name="npc"></param>
    public virtual void Initialize(GameObject npc)
    {
        foreach(BehaviourNode child in children)
        {
            child.Initialize(npc);
        }
    }

}
