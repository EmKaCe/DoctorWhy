using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviourConnection
{

    public BehaviourConnectionPoint inPoint;
    public BehaviourConnectionPoint outPoint;
    public Action<BehaviourConnection> OnClickRemoveConnection;


    public BehaviourConnection(BehaviourConnectionPoint inPoint, BehaviourConnectionPoint outPoint, Action<BehaviourConnection> OnClickRemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.OnClickRemoveConnection = OnClickRemoveConnection;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            inPoint.rect.center,
            outPoint.rect.center,
            inPoint.rect.center + Vector2.left * 50f,
            outPoint.rect.center - Vector2.left * 50f,
            Color.white,
            null,
            2f
         );
        if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            OnClickRemoveConnection?.Invoke(this);
        }

    }
}
