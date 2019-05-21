using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourConnectionPointType { In, Out}

public class BehaviourConnectionPoint
{
    [HideInInspector]
    public Rect rect;

    [HideInInspector]
    public BehaviourConnectionPointType type;

    [HideInInspector]
    public BehaviourNode node;

    [HideInInspector]
    public GUIStyle style;

    [HideInInspector]
    public Action<BehaviourConnectionPoint> OnClickConnectionPoint;

    public BehaviourConnectionPoint(BehaviourNode node, BehaviourConnectionPointType type, GUIStyle style, Action<BehaviourConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 10f, 20f);
    }

    public void Draw()
    {
        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

        switch (type)
        {
            case BehaviourConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case BehaviourConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            OnClickConnectionPoint?.Invoke(this);
        }
    }


}
