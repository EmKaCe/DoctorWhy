using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalSequence : Sequence
{
#if UNITY_EDITOR
    public void CreateConditionalSequenceNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }
#endif
    public override void Init()
    {
        //Debug.Log("ConditionalSequence Init");
        base.Init();
       // Debug.Log("CS: CurrentNode: " + currentNode);
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
    }

    public override string GetBehaviourType()
    {
        return "ConditionalSequence";
    }

    public override void Run()
    {
        //Debug.Log("ConditionalSequence Node " + currentNode);
        base.Run();
        
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //Condition
        //Debug.Log("ConSeq: cur:abs" + currentNode + ":" + children.Length);
        if(currentNode < children.Length - 1)
        {
            this.state = state;
            if (state == BaseBehaviour.State.success)
            {
                //if condition successful -> advance and leave Sequence in running state
                this.state = BaseBehaviour.State.running;
                currentNode++;
            }
        }
        else
        {
            //not condition anymore start over and advance to behaviour -> behaviour stays at running state if running
           // Debug.Log("CondititonalSequence behaviour " + state);
            this.state = state;
            currentNode = 0;
        }
       // Debug.Log("ConSeq ended with " + this.state.ToString());
        SendParentCurrentState(this.state);
    }

}
