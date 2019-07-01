using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class GoToBehaviourNode : BehaviourNode
{



    public string positionKey;
    public float movementSpeed;
    private Vector3 position;

    List<Vector3> path;
    AStarAlgorithm pathfinder;
    //should the path be calculated every tick or only once
    string tickUpdateKey;
    string compositeTickUpdateKey;
    string compositePositionKey;
    bool tickUpdate;
#if UNITY_EDITOR
    public void CreateGoToBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }


    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginArea(rectContent);
        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.HelpBox("PositionKey: key where Vector3 is saved.", MessageType.Info);
        positionKey = EditorGUILayout.TextField("PositionKey", positionKey);
        EditorGUILayout.HelpBox("TickUpdateKey: key where bool if behaviour should update position every tick is saved.", MessageType.Info);
        tickUpdateKey = EditorGUILayout.TextField("TickUpdateKey", tickUpdateKey);

        GUILayout.EndArea();
    }
#endif

    public override string GetBehaviourType()
    {
        return "GoToBehaviourNode";
    }

    public override void Run()
    {
        Debug.Log("GoTo " + positionKey + " tick: " + tickUpdate);
        if (tickUpdate)
        {
            CheckForNewPos();
        }
        if (path.Count > 0)
        {
            Debug.Log("Goal: " + path[0]);
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, path[0], movementSpeed * Time.deltaTime);
            if (npc.transform.position == position)
            {
                Debug.Log("Position reached");
                state = BaseBehaviour.State.success;
                SendParentCurrentState(BaseBehaviour.State.success);
                return;
            }
            else if(npc.transform.position == path[0])
            {
                
                path = pathfinder.FindPath(npc.transform.position, position);             
                path.RemoveAt(0);

            }
            state = BaseBehaviour.State.running;
            SendParentCurrentState(BaseBehaviour.State.running);
        }
        else
        {
            if(npc.transform.position == position)
            {
                state = BaseBehaviour.State.success;
                SendParentCurrentState(BaseBehaviour.State.success);
            }
            else
            {
                state = BaseBehaviour.State.failure;
                SendParentCurrentState(BaseBehaviour.State.failure);
            }
            
        }


    }

    public void CheckForNewPos()
    {
        blackboard.positions.TryGetValue(compositePositionKey, out Vector3 newPos);
        Debug.Log("Distance " + Vector3.Distance(newPos, position));
        if(Vector3.Distance(newPos, position) > 2)
        {
            position = newPos;
            path = pathfinder.FindPath(npc.transform.position, position);
            if (path.Count > 0)
            {
               // Debug.Log(npc.transform.position + " " + path[0] + " " + path[path.Count - 1]);
                path.RemoveAt(0);
            }
        }
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        compositePositionKey = positionKey + npcID;
        compositeTickUpdateKey = tickUpdateKey + npcID;
        Debug.Log("Key test goto: " + compositePositionKey);
        movementSpeed = npc.GetComponent<BehaviourComponent>().movementSpeed;
        pathfinder = new AStarAlgorithm(blackboard.layout, blackboard.map, blackboard.collider);
        path = new List<Vector3>();
    }


    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        throw new NotImplementedException();
    }

    public override void Init()
    {
        Debug.Log("GoTO init");
        state = BaseBehaviour.State.inactive;
        blackboard.tickUpdate.TryGetValue(compositeTickUpdateKey, out tickUpdate);
        //Calculate way -> calculate at the beginning, while running to goal without problem -> continues
        //failure -> new way gets calculated/decorator should calcultate way -> node only takes way once
        blackboard.positions.TryGetValue(compositePositionKey, out position);
        path = pathfinder.FindPath(npc.transform.position, position);
        if(path.Count > 0)
        {
            Debug.Log(npc.transform.position + " " + path[0] + " " + path[path.Count - 1]);
            path.RemoveAt(0);
        }
        

    }
}
