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
    UnityAction<int, string[]> interactionExitedListener;
    [Header("Message shown to player when close to object")]
    public string interactionMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GlobalStateManager.interacting == null)
        {
            GlobalStateManager.interacting = this;
            if (collision.gameObject.GetComponent<TagSystem>().Player && !triggered)
            {
                EventManager.TriggerEvent(EventSystem.InteractionTriggered(), 0, new string[] { interactionMessage, GetType().ToString() });
                triggered = true;
                StartInteraction();
            }
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagSystem>().Player && triggered)
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
        if (GlobalStateManager.interacting == this)
        {
            GlobalStateManager.interacting = null;
        }
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
        //interactionExitedListener = new UnityAction<int, string[]>(CheckCollision);
    }

    public abstract void Action(int entityId, string[] values);

    public abstract void Quit(int entityId, string[] values);

    //public void CheckCollision(int i, string[] s)
    //{
    //    Collider2D[] collider = EntityManager.GetEntityComponent<EntityComponent>(i).GetComponents<Collider2D>();
    //    foreach(Collider2D c in collider)
    //    {
    //        if(Physics2D.IsTouching(gameObject.GetComponent<Collider2D>(), c))
    //        {
    //            OnTriggerEnter2D(c);
    //            return;
    //        }
    //    }
        
    //}
}
