using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GoToNode", menuName = "BehaviourTree/GoToNode")]
public class GoToBehaviourNode : BehaviourNode
{



    public string positionKey;
    public float movementSpeed;
    private Vector3 position;

    List<Vector3> path;
    AStarAlgorithm pathfinder;


    public void CreateGoToBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }


    
    

    public override string GetBehaviourType()
    {
        return "GoToBehaviourNode";
    }

    public override void Run()
    {
        if (path.Count > 0)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, path[0], movementSpeed * Time.deltaTime);
            if (npc.transform.position == position)
            {
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

    

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);       
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
        state = BaseBehaviour.State.inactive;
        if (!positionKey.Equals("Pathpoint"))
        {
            positionKey = "Pathpoint";
        }
        //Calculate way -> calculate at the beginning, while running to goal without problem -> continues
        //failure -> new way gets calculated/decorator should calcultate way -> node only takes way once
        blackboard.positions.TryGetValue(positionKey, out position);
        path = pathfinder.FindPath(npc.transform.position, position);
        if(path.Count > 0)
        {
            path.RemoveAt(0);
        }
        

    }
}
