﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BehaviourNode
{

    BaseBehaviour.State childState;
    int currentPos;

    public void CreateSelectorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }



    public override string GetBehaviourType()
    {
        return "Selector";
    }

    public override void Run()
    {
        if(currentPos < outPoints)
        {
            children[currentPos].Run();
        }
        else
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
            currentPos = 0;
        }


    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        if(state == BaseBehaviour.State.failure)
        {
            currentPos++;
            return;
        }
        else if (state == BaseBehaviour.State.success)
        {
            currentPos = 0;
        }
        this.state = state;
        SendParentCurrentState(state);
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        currentPos = 0;
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }
}