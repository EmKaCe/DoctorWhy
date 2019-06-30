using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iterator : Decorator
{

    List<Vector3> waypoints;
    public string waypointKey;
    

    public void CreateIteratorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override void Draw()
    {
        base.Draw();
    }

    public override string GetBehaviourType()
    {
        return "Iterator";
    }

    public override void Init()
    {
        state = BaseBehaviour.State.inactive;
        if (waypointKey.Equals(""))
        {
            waypointKey = "Waypoint";
        }
        blackboard.waypoints.TryGetValue(waypointKey, out waypoints);
        if (waypoints.Count == 0)
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
            return;
        }
        if (blackboard.positions.ContainsKey("Pathpoint"))
        {
            blackboard.positions["Pathpoint"] = waypoints[0];
        }
        else
        {
            blackboard.positions.Add("Pathpoint", waypoints[0]);
        }
        
        




    }

    public override void Run()
    {
        if(waypoints.Count > 0)
        {
            if (children[0].state != BaseBehaviour.State.running)
            {
                children[0].Init();
            }
            children[0].Run();
        }
        
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        waypoints = new List<Vector3>();


    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        if(state == BaseBehaviour.State.running)
        {
            SendParentCurrentState(BaseBehaviour.State.running);
        }
        else if(state == BaseBehaviour.State.failure)
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
        }
        else if(state == BaseBehaviour.State.success)
        {
            
            
            if(waypoints.Count > 1)
            {
                waypoints.RemoveAt(0);
                //put new point in blackboard
                blackboard.positions["Pathpoint"] = waypoints[0];
                //children[0].Init();
            }
            else
            {
                state = BaseBehaviour.State.success;
                SendParentCurrentState(BaseBehaviour.State.success);
            }
        }
    }
}
