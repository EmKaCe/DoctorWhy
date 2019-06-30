using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinGameComponent : MonoBehaviour
{
    UnityAction<int, string[]> winGameListener;
    private Animator tanim;
    private bool win;
    public GameObject terrafomer;
    public GameObject AI;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        tanim = terrafomer.GetComponent<Animator>();
        canim = camera.GetComponent<Animator>();
        if (tanim == null)
        {
            Debug.Log("Terrafomer needs an Animator");
        }

        EventManager.StartListening(EventSystem.WinGame(), winGameListener);
    }
    private void Awake()
    {
        winGameListener = new UnityAction<int, string[]>(Win);
    }
    // Update is called once per frame
    void Update()
    {
        if (win) //event WinGame has been called somewhere (NPCAction WinGame in Dialog TarraformingDialog)
        {
            tanim.Play("terraforming");
            EventManager.TriggerEvent(EventSystem.CameraShake(), 0, new string[] { });
            //play for 5 seconds. 
            float end = Time.time + 5.0f;
            if(Time.time >= end) //we've waited for 5 seconds
            {
                tanim.Play("notterraforming");
                win = false; //dont keep looping even though we've already won.
                EventManager.TriggerEvent(EventSystem.BeginConversation(), 
                    AI.GetComponentInParent<EntityComponent>().entityID, new string[] { AI.name});
            }

        }
    }
    
    public void Win(int empty, string[] empty2)
    {
        win = true;

        //anim.Play("notterraforming");
    }
}
