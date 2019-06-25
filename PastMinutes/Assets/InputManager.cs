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
            EventManager.TriggerEvent(EventSystem.PickUpItem(), player.GetInstanceID(), new string[] { });
            EventManager.TriggerEvent(EventSystem.StartConversation(), player.GetInstanceID(), new string[] { });
        }
    }

}
