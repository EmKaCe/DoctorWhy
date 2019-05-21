﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChildStateBehaviourNode : BehaviourNode
{
    public ChildStateBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode)
        : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

    }
}
