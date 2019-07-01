using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BehaviourNode
{

    protected int currentNode;
#if UNITY_EDITOR
    public void CreateSequenceNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginArea(rectContent);
        if (GUILayout.Button("Add", GUILayout.Width(50)))
        {
            AddExitPoint();
        }

        GUILayout.EndArea();
    }

    public void AddExitPoint()
    {
        AddChild();
        //children.Add(null);
        AddConnectionPoint(BehaviourConnectionPointType.Out);
    }

    public void AddChild()
    {
        BehaviourNode[] newChildren = new BehaviourNode[children.Length + 1];
        for (int i = 0; i < children.Length; i++)
        {
            newChildren[i] = children[i];
        }
        children = newChildren;

    }

    public void AddConnectionPoint(BehaviourConnectionPointType type)
    {
        BehaviourConnectionPoint[] newPoints;
        if (type == BehaviourConnectionPointType.In)
        {
            newPoints = new BehaviourConnectionPoint[inPoints + 1];
            //Copy array
            for (int i = 0; i < inPoints; i++)
            {
                newPoints[i] = inPoint[i];
                newPoints[i].count++;
            }
            newPoints[inPoints] = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint, inPoints, inPoints + 1);
            inPoint = newPoints;
            inPoints++;
        }
        else if (type == BehaviourConnectionPointType.Out)
        {
            newPoints = new BehaviourConnectionPoint[outPoints + 1];
            //Copy array
            for (int i = 0; i < outPoints; i++)
            {
                newPoints[i] = outPoint[i];
                newPoints[i].count++;
            }
            newPoints[outPoints] = new BehaviourConnectionPoint(this, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint, outPoints, outPoints + 1);
            outPoint = newPoints;
            outPoints++;
        }
    }

#endif
    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        currentNode = 0;
    }

    public override void Run()
    {
        Debug.Log(nodeName + ": Node " + currentNode);
        
        if (currentNode < children.Length)
        {
            Debug.Log(children[currentNode].nodeName + " " + children[currentNode].state);
            if (!(children[currentNode].state == BaseBehaviour.State.running))
            {
                Debug.Log("Sequence initialized child");
                children[currentNode].Init();
            }
            children[currentNode].Run();
        }
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //
        if(state == BaseBehaviour.State.failure)
        {
            this.state = state;           
        }
        else if(state == BaseBehaviour.State.success)
        {
           // Debug.Log(nodeName + " crtNode before " + currentNode);
            currentNode++;
           
            //shouldn't return success unless all nodes were successful
            if (currentNode == children.Length)
            {
                currentNode = 0;
                this.state = BaseBehaviour.State.success;
                SendParentCurrentState(BaseBehaviour.State.success);
            }
            //Debug.Log(nodeName + " crtNode after " + currentNode);
            this.state = BaseBehaviour.State.running;
            SendParentCurrentState(BaseBehaviour.State.running);
            return;
        }
        this.state = state;
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
