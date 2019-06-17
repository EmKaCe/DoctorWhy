using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionComponent : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagSystem>().Player && !triggered)
        {
            triggered = true;
            StartInteraction(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TagSystem>().Player)
        {
            triggered = false;
            StopInteraction();
        }
    }

    public abstract void StartInteraction();

    public abstract void StopInteraction();
}
