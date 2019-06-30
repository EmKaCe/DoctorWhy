using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BehaviourNode
{

    int currentNode;

    public void CreateSequenceNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override void Draw()
    {
        base.Draw();
    }


    public override void Run()
    {
        if (!(state == BaseBehaviour.State.running))
        {
            Init();
        }
        if (currentNode < children.Length)
        {
            children[currentNode].Run();
        }
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        if(state == BaseBehaviour.State.failure)
        {
            this.state = state;           
        }
        else if(state == BaseBehaviour.State.success)
        {
            currentNode++;
            //shouldn't return success unless all nodes were successful
            if (currentNode == children.Length)
            {
                SendParentCurrentState(BaseBehaviour.State.success);
            }
            SendParentCurrentState(BaseBehaviour.State.running);
            return;
        }
        SendParentCurrentState(state);
    }

    public override string GetBehaviourType()
    {
        return "Sequence";
    }

    public override void Init()
    {
        currentNode = 0;
    }
}
