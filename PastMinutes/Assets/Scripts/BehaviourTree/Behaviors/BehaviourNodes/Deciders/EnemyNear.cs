using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyNearNode", menuName = "BehaviourTree/EnemyNearNode")]
public class EnemyNear : BehaviourNode
{
    List<Collider2D> collisions;
    ContactFilter2D filter;
    Faction ownFaction;
    float distance;
    string distanceKey;
    string distanceCompoundKey;
    string enemyKey;
    string positionKey;
    string compositePositionKey;
    int id;
#if UNITY_EDITOR
    public void CreateEnemyNearNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }


    

    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginArea(rectContent);

        EditorGUIUtility.labelWidth = 100;
        distanceKey = EditorGUILayout.TextField("DistanceKey", distanceKey);
        positionKey = EditorGUILayout.TextField("PositionKey", positionKey);
        GUILayout.EndArea();
    }
#endif
    public override void Run()
    {
        Debug.Log("EnemyNear " + distanceKey);
        if(!(state == BaseBehaviour.State.running))
        {
            Init();
        }
        if (Physics2D.OverlapCircle(npc.transform.position, distance, filter, collisions) > 0)
        {
            for(int i = 0; i < collisions.Count; i++)
            {
                if (collisions[i].GetType().Equals(typeof(CapsuleCollider2D)) && ownFaction.CheckHostile(collisions[i].gameObject))
                {
                    id = collisions[i].gameObject.GetComponent<EntityComponent>().entityID;
                    if (blackboard.enemies.ContainsKey(enemyKey))
                    {
                        blackboard.enemies[enemyKey] = id;
                    }
                    else
                    {
                        blackboard.enemies.Add(enemyKey, id);
                    }

                    Transform t;
                    if ((t = EntityManager.GetEntityComponent<Transform>(id) as Transform) != null)
                    {
                        if (blackboard.positions.ContainsKey(positionKey))
                        {
                            Debug.Log("Enemy pos set");
                            blackboard.positions[compositePositionKey] = t.position;
                        }
                        else
                        {
                            blackboard.positions.Add(compositePositionKey, t.position);
                        }
                    }
                   // Debug.Log("EnemyNear found enemy");
                    state = BaseBehaviour.State.success;
                    SendParentCurrentState(BaseBehaviour.State.success);
                    return;
                }
            }
        }
       // Debug.Log("EnemyNear didn't find one");
        state = BaseBehaviour.State.failure;
        SendParentCurrentState(BaseBehaviour.State.failure);
        //Debug.Log("Kein Gegner");
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //has no children
    }

    public override string GetBehaviourType()
    {
        return "EnemyNear";
    }

    public override void Init()
    {
        Debug.Log("EnemyNear init");

        ownFaction = npc.GetComponent<FactionComponent>().GetFaction();
        blackboard.distances.TryGetValue(distanceCompoundKey, out distance);
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        enemyKey = "Enemy" + npcID;
        distanceCompoundKey = distanceKey + npcID;
        compositePositionKey = positionKey + npcID;
        collisions = new List<Collider2D>();
        filter = new ContactFilter2D
        {
            // filter = filter.NoFilter();
            layerMask = npc.layer

        };
        filter.useTriggers = true;


    }
}
