using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityComponent), typeof(ItemComponent))]
public class PickUpComponent : InteractionComponent
{
    UnityAction<int, string[]> pickUpListener;

    public override void StartInteraction()
    {
        EventManager.StartListening(EventSystem.PickUpItem(), pickUpListener);
    }

    public override void StopInteraction()
    {
        EventManager.StopListening(EventSystem.PickUpItem(), pickUpListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventSystem.PickUpItem(), pickUpListener);
    }

    private void Awake()
    {
        pickUpListener = new UnityAction<int, string[]>(PickUpItem);
    }

    private void PickUpItem(int entityID, string[] empty2)
    {
        Debug.Log("PickUpComponent: Item would be picked up");
        EventManager.TriggerEvent(EventSystem.AddItemToInventory(), gameObject.GetInstanceID(), new string[] {entityID.ToString() });
    }
}
