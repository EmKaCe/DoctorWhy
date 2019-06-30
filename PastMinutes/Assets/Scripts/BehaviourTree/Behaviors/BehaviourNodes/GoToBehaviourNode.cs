using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GoToNode", menuName = "BehaviourTree/GoToNode")]
public class GoToBehaviourNode : BehaviourNode
{

    [Tooltip("only public for easier bughunting")]
    public GameObject npc;
    [Tooltip("only public for easier bughunting")]
    public Blackboard blackboard;

    public string positionKey;
    public float movementSpeed;
    

    public void CreateGoToBehaviourNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints);
    }


    
    

    public override string GetBehaviourType()
    {
        return "GoToBehaviourNode";
    }

    public override void Run()
    {
        //Get position -> lerp to it
        blackboard.positions.TryGetValue(positionKey, out Vector3 position);
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, position, movementSpeed * Time.deltaTime);
        if (npc.transform.position == position)
        {
            state = BaseBehaviour.State.running;
            SendParentCurrentState(BaseBehaviour.State.running);
        }
        else
        {
            state = BaseBehaviour.State.success;
            SendParentCurrentState(BaseBehaviour.State.success);
        }
    }

    

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        this.npc = npc;
        blackboard = npc.GetComponent<BehaviourComponent>().blackboard;
        blackboard.positions.Add(positionKey, new Vector3(1, 0, 0));
        //movementSpeed = npc.GetComponent<AIComponent>()
    }

    private void OnDisable()
    {
        npc = null;
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        throw new NotImplementedException();
    }
}
