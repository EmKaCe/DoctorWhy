using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Decorator
{

    public void CreateInverterNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override string GetBehaviourType()
    {
        return "Inverter";
    }
    public override void Draw()
    {
        base.Draw();
    }


    public override void Init()
    {
        state = BaseBehaviour.State.inactive;
    }

    public override void Run()
    {
        if (!(children[0].state == BaseBehaviour.State.running))
        {
            children[0].Init();
        }
        children[0].Run();
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        if(state == BaseBehaviour.State.failure)
        {
            SendParentCurrentState(BaseBehaviour.State.success);
        }
        else if(state == BaseBehaviour.State.success)
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
        }
        else if(state == BaseBehaviour.State.running)
        {
            SendParentCurrentState(BaseBehaviour.State.running);
        }
        else
        {
            Debug.Log("Inverter got Inactive as result");
        }
    }

}
