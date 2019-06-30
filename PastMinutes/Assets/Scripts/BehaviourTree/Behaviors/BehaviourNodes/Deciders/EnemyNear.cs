using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyNearNode", menuName = "BehaviourTree/EnemyNearNode")]
public class EnemyNear : BehaviourNode
{
    List<Collider2D> collisions;
    ContactFilter2D filter;
    Faction ownFaction;

    public void CreateEnemyNearNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }


    public override string GetBehaviourType()
    {
        return "EnemyNear";
    }

    public override void Init()
    {
        collisions = new List<Collider2D>();
        filter = new ContactFilter2D
        {
            // filter = filter.NoFilter();
            layerMask = npc.layer
            
        };
        filter.useTriggers = true;
        ownFaction = npc.GetComponent<FactionComponent>().GetFaction();
    }

    public override void Draw()
    {
        base.Draw();
    }

    public override void Run()
    {
        if(!(state == BaseBehaviour.State.running))
        {
            Init();
        }
        if (Physics2D.OverlapCircle(npc.transform.position, npc.GetComponent<BehaviourComponent>().viewDistance, filter, collisions) > 0)
        {
            for(int i = 0; i < collisions.Count; i++)
            {
                if (collisions[i].GetType().Equals(typeof(CapsuleCollider2D)) && ownFaction.CheckHostile(collisions[i].gameObject))
                {
                    SendParentCurrentState(BaseBehaviour.State.success);
                }
            }
        }
        SendParentCurrentState(BaseBehaviour.State.failure);
        //Debug.Log("Kein Gegner");
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //has no children
    }
}
