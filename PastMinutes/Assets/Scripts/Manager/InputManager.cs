using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetInput
        if (Input.GetKeyDown(KeyCode.E))
        {
            //EventManager.TriggerEvent(EventSystem.PickUpItem(), player.GetInstanceID(), new string[] { });
            //EventManager.TriggerEvent(EventSystem.StartConversation(), player.GetInstanceID(), new string[] { });
            EventManager.TriggerEvent(EventSystem.Interact(), player.GetInstanceID(), new string[] { "" });
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            EventManager.TriggerEvent(EventSystem.StopHoldingInteract(), player.GetInstanceID(), new string[] { });
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            EventManager.TriggerEvent(EventSystem.TravelTime(), 0, new string[] { });
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventManager.TriggerEvent(EventSystem.Reload(), player.GetInstanceID(), new string[] { });
        }
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.TriggerEvent(EventSystem.TriggerPulled(), player.GetInstanceID(), new string[] { });
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventManager.TriggerEvent(EventSystem.DrawGun(), player.GetInstanceID(), new string[] { "1" });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventManager.TriggerEvent(EventSystem.DrawGun(), player.GetInstanceID(), new string[] { "2" });
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            EventManager.TriggerEvent(EventSystem.StowGun(), player.GetInstanceID(), new string[] { });
        }
    }

}
