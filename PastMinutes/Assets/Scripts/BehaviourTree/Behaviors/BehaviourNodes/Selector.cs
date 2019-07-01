using System;
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

    public override string GetBehaviourType()
    {
        return "Selector";
    }

    public override void Run()
    {
       // Debug.Log(currentPos);
        
     //   Debug.Log("Selector: CurPos: " + currentPos);
        if (currentPos < outPoints)
        {
            if (!(children[currentPos].state == BaseBehaviour.State.running))
            {
          //      Debug.Log("Selector initialized child");
                children[currentPos].Init();
            }
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
          //  Debug.Log("Selector advance" + currentPos);
            if (currentPos < outPoints)
            {
                this.state = BaseBehaviour.State.running;
            }
            else
            {
             //   Debug.Log("Selector I got too many");
                this.state = BaseBehaviour.State.failure;
                SendParentCurrentState(this.state);
                return;
            }
        }
        else if (state == BaseBehaviour.State.success)
        {
            currentPos = 0;
            this.state = BaseBehaviour.State.success;
        }
        else
        {
            this.state = state;
        }
        SendParentCurrentState(this.state);
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        currentPos = 0;
    }

    public override void Init()
    {
     //   Debug.Log("Selector gets intitialized");
        currentPos = 0;
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
}
