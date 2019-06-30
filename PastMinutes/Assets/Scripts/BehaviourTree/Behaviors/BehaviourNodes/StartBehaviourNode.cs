using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehaviourNode : BehaviourNode
{

    public override string GetBehaviourType()
    {
        throw new NotImplementedException();
    }

    public void CreateStartBehaviour(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        base.CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override void Draw()
    {
        base.Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Run()
    {
        if(children[0].state == BaseBehaviour.State.success && children[0].state == BaseBehaviour.State.failure)
        {

        }
        else
        {
            if (children[0].state != BaseBehaviour.State.running)
            {
                
                children[0].Init();
            }
            children[0].Run();
        }
        
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //Debug.Log(state);
        SendParentCurrentState(state);
        this.state = state;
    }

    public override void Init()
    {

    }

}
