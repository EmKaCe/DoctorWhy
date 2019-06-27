using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRefillComponent : InteractionComponent
{

    public bool unlimitedSupply;
    public float supply;
    public float unitsPerSecond;

    private bool refill;
    private BreathingComponent b;

    public override void Action(int entityId, string[] values)
    {
        Refill(entityId);
    }

    private void Refill(int entityId)
    {
        Debug.Log("Begin refill");
        b = EntityManager.GetEntityComponent<BreathingComponent>(entityId) as BreathingComponent;
        refill = true;
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        refill = false;
        Debug.Log("Refill end");
    }

    private void Update()
    {
        if (refill)
        {
            if(b != null)
            {
                float amount = unitsPerSecond *Time.deltaTime; ;
                if (!unlimitedSupply)
                {
                    if(supply == 0)
                    {
                        
                        EventManager.TriggerEvent(EventSystem.RanOutOfOxygen(), gameObject.GetComponent<EntityComponent>().entityID, new string[] { amount.ToString()});
                        amount = 0;
                    }
                    if(amount > supply)
                    {
                        amount = supply;
                    }
                    supply -= amount;
                }
                b.Refill(amount);
            }
        }
    }

    public override void Quit(int entityId, string[] values)
    {
        refill = false;
    }
}
