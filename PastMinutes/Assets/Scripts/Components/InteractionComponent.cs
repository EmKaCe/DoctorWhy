using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class InteractionComponent : MonoBehaviour
{
    bool triggered = false;
    [Header("Message shown to player when close to object")]
    public string interactionMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagSystem>().Player && !triggered)
        {
            EventManager.TriggerEvent(EventSystem.InteractionTriggered(), 0, new string[] { interactionMessage, GetType().ToString() });
            triggered = true;
            StartInteraction(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagSystem>().Player)
        {
            EventManager.TriggerEvent(EventSystem.InteractionExited(), 0, new string[] { interactionMessage, GetType().ToString() });
            triggered = false;
            StopInteraction();
        }
    }

    public abstract void StartInteraction();

    public abstract void StopInteraction();
}
