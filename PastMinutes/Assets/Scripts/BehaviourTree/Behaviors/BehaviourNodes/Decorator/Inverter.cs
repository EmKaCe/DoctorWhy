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
        throw new System.NotImplementedException();
    }

    public override void Run()
    {
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
    }

}
