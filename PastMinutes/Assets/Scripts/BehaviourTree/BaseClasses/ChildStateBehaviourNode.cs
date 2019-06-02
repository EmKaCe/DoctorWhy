using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChildStateBehaviourNode : BehaviourNode
{
    public void CreateChildStateBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints);
    }
}
