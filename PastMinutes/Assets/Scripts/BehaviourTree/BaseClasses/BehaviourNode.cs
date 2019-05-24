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

    public BehaviourConnectionPoint inPoint;
    public BehaviourConnectionPoint outPoint;

    public Action<BehaviourNode> OnRemoveNode;

    [HideInInspector]
    public readonly float rowHeight = 20f;
    [HideInInspector]
    public readonly float offset = 10;

    private readonly float titleOffset = 10;



    public void CreateBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode)
    {
        rect = new Rect(position.x, position.y, width, height);
        rectTitle = new Rect(position.x + offset, position.y + titleOffset, width - 2 * offset, rowHeight);
        style = nodeStyle;
        inPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
    }



    public virtual void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public abstract void Draw();

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
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }

    public abstract void Run();

    /// <summary>
    /// Should react to return values of behaviour(s)
    /// </summary>
    public abstract void OnBehaviourResult(State state);

}
