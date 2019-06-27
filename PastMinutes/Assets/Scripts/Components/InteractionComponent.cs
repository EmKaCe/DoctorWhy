using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class InteractionComponent : MonoBehaviour
{
    bool triggered = false;
    public bool holdToInteract;
    UnityAction<int, string[]> interactionListener;
    UnityAction<int, string[]> interactionHoldListener;
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

    public virtual void StartInteraction()
    {
        EventManager.StartListening(EventSystem.Interact(), interactionListener);
        if (holdToInteract)
        {
            Debug.Log("listening");
            EventManager.StartListening(EventSystem.StopHoldingInteract(), interactionHoldListener);
        }
        
    }

    public virtual void StopInteraction()
    {
        EventManager.StopListening(EventSystem.Interact(), interactionListener);
        if (holdToInteract)
        {
            EventManager.StopListening(EventSystem.StopHoldingInteract(), interactionHoldListener);
        }
    }

    private void OnDisable()
    {

        EventManager.StopListening(EventSystem.Interact(), interactionListener);
        if (holdToInteract)
        {
            EventManager.StopListening(EventSystem.StopHoldingInteract(), interactionHoldListener);
        }
    }

    private void Awake()
    {
        interactionListener = new UnityAction<int, string[]>(Action);
        interactionHoldListener = new UnityAction<int, string[]>(Quit);
    }

    public abstract void Action(int entityId, string[] values);

    public abstract void Quit(int entityId, string[] values);
}
