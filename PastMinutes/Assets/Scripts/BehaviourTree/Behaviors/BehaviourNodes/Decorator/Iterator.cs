using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Iterator : Decorator
{

    List<Vector3> waypoints;
    public string waypointKey;
    public Patrol patrolPath;
    string pathpointKey;
    string compositePathpointKey;
    string compositeWaypointKey;
    bool looped;

#if UNITY_EDITOR
    public void CreateIteratorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }


    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginArea(rectContent);
        EditorGUIUtility.labelWidth = 100;
        waypointKey = EditorGUILayout.TextField("WaypointKey", waypointKey);
        pathpointKey = EditorGUILayout.TextField("PathpointKey", pathpointKey);

        GUILayout.EndArea();
    }
    #endif

    public override string GetBehaviourType()
    {
        return "Iterator";
    }

    public override void Init()
    {
        bool test = blackboard.patrolRoutes.TryGetValue(compositeWaypointKey,out patrolPath);
        //Debug.Log("patrol: " + test);
        state = BaseBehaviour.State.inactive;
        if(looped && !patrolPath.loop)
        {
            SendParentCurrentState(BaseBehaviour.State.success);
            state = BaseBehaviour.State.success;
            return;
        }
        waypoints = patrolPath.GetWaypoints();
       // blackboard.waypoints.TryGetValue(waypointKey, out waypoints);
        if (waypoints.Count == 0)
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
            return;
        }
        if (blackboard.positions.ContainsKey(compositePathpointKey))
        {
            blackboard.positions[compositePathpointKey] = waypoints[0];
        }
        else
        {
            blackboard.positions.Add(compositePathpointKey, waypoints[0]);
        }
    }

    public override void Run()
    {
       // Debug.Log("Iterator: " + waypointKey + " WP: " + waypoints.Count);
        if(waypoints.Count > 0)
        {
            if (children[0].state != BaseBehaviour.State.running)
            {
                children[0].Init();
            }
            children[0].Run();
        }
        else
        {
            SendParentCurrentState(BaseBehaviour.State.failure);
        }
        
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        looped = false;
        state = BaseBehaviour.State.inactive;
        waypoints = new List<Vector3>();
        compositePathpointKey = pathpointKey + npcID;
        compositeWaypointKey = waypointKey + npcID;


    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        if(state == BaseBehaviour.State.running)
        {
            this.state = BaseBehaviour.State.running;
          //  Debug.Log("Iterator return " + this.state.ToString());
            SendParentCurrentState(BaseBehaviour.State.running);
        }
        else if(state == BaseBehaviour.State.failure)
        {
            this.state = BaseBehaviour.State.failure;
          //  Debug.Log("Iterator return " + this.state.ToString());
            SendParentCurrentState(BaseBehaviour.State.failure);
        }
        else if(state == BaseBehaviour.State.success)
        {
            
            
            if(waypoints.Count > 1)
            {
                this.state = BaseBehaviour.State.running;
                waypoints.RemoveAt(0);
                //put new point in blackboard
                blackboard.positions[compositePathpointKey] = waypoints[0];
                SendParentCurrentState(this.state);
                //children[0].Init();
            }
            else
            {
                looped = true;
                this.state = BaseBehaviour.State.success;
            //    Debug.Log("Iterator return " + this.state.ToString());
                SendParentCurrentState(BaseBehaviour.State.success);
            }
        }
    }
}
