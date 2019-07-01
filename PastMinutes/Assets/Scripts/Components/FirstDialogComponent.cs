using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstDialogComponent : MonoBehaviour
{
    public GameObject StartAI;
    public GameObject Past;
    private bool called = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!called)
        {
            EventManager.TriggerEvent(EventSystem.TriggerDialog(), StartAI.GetComponentInParent<EntityComponent>().entityID, new string[] { StartAI.GetComponentInParent<EntityComponent>().entityID + ""});
            called = true;
        }
    }
}
