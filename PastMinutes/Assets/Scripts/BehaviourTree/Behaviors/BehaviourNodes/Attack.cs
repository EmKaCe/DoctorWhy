using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BehaviourNode
{
    public string enemyKey;
    private int enemyID;
    float timeOfAttack;
    float currentTime;
    float timeBetweenAttacks;
    string damage;


    public void CreateAttackNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<BehaviourConnectionPoint> OnClickInPoint, Action<BehaviourConnectionPoint> OnClickOutPoint, Action<BehaviourNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
    {
        CreateBehaviourNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
        rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
    }

    public override string GetBehaviourType()
    {
        return "Attack";
    }

    public override void Init()
    {
        state = BaseBehaviour.State.inactive;
        blackboard.enemies.TryGetValue(enemyKey, out enemyID);
        
    }

    public override void Run()
    {
        currentTime = Time.time;
        Debug.Log(currentTime);
        Debug.Log(timeOfAttack);
        if(currentTime > (timeOfAttack + timeBetweenAttacks))
        {
            timeOfAttack = currentTime;
            EventManager.TriggerEvent(EventSystem.TakeDamage(), enemyID, new string[] { damage, npcID });
            state = BaseBehaviour.State.success;
            SendParentCurrentState(state);
            return;
        }
        state = BaseBehaviour.State.failure;
        SendParentCurrentState(state);
       
    }

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        enemyKey = "Enemy" + npc.GetComponent<BehaviourComponent>().GetID();
        enemyID = 0;
        damage = npc.GetComponent<PlayerComponent>().damage.ToString();
        timeBetweenAttacks = npc.GetComponent<PlayerComponent>().timeBetweenAttacks;
        timeOfAttack = Time.time;
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        //no children
    }
}
