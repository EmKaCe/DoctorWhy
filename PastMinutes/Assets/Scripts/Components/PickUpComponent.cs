using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityComponent), typeof(ItemComponent))]
public class PickUpComponent : InteractionComponent
{


    public override void Action(int entityId, string[] values)
    {
        PickUpItem(entityId);
    }

    private void PickUpItem(int entityID)
    {
        EventManager.TriggerEvent(EventSystem.AddItemToInventory(), gameObject.GetInstanceID(), new string[] {entityID.ToString() });
    }

    public override void Quit(int entityId, string[] values)
    {
        //do nothing
    }

    public override void ComponentHasParent()
    {
        enabled = false;
    }
}
