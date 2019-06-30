using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinGameComponent : MonoBehaviour
{
    UnityAction<int, string[]> winGameListener;
    UnityAction<int, string[]> falseWinListener;
    UnityAction<int, string[]> forceResetListener;
    private Animator tanim;
    private bool win;
    public GameObject terrafomer;
    public GameObject AI;
    public GameObject endScreen;
    private float end;
    private float readDialog;
    public GameObject Present;
    public GameObject Past;
    private bool triggerDialog=false;
    public GameObject UI;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        tanim = terrafomer.GetComponent<Animator>();
        if (tanim == null)
        {
            Debug.Log("Terrafomer needs an Animator");
        }

        EventManager.StartListening(EventSystem.WinGame(), winGameListener);
        EventManager.StartListening(EventSystem.WinGame(), falseWinListener);
        EventManager.StartListening(EventSystem.WinGame(), forceResetListener);
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

            if (Time.time >= end) //we've waited for 5 seconds
            {
                Present.SetActive(false);
                Past.SetActive(true);
                //keep from going forward in time
                tanim.Play("notterrafomring");
               
                //don't keep looping even though we've already won.
                if (!triggerDialog) {
                    triggerDialog = true;
                    EventManager.TriggerEvent(EventSystem.BeginConversation(),
                    AI.GetComponentInParent<EntityComponent>().entityID, new string[] { AI.name });
                }

                if (Time.time >=readDialog) {
                    endScreen.SetActive(true);
                    //block screen with a win message XD
                    Debug.Log("Show end screen");
                    UI.SetActive(false);
                    //endScreen.transform.position = camera.transform.position;
                    win = false;
                }

            }

        }
        else
        {
            tanim.Play("notterrafomring");
            Debug.Log("not won yet");
        }
    }
    
    public void Win(int empty, string[] empty2)
    {
        readDialog = Time.time + 30.0f;
        end = Time.time + 5.0f;
        Debug.Log("started Win Event");
        win = true;

        //anim.Play("notterraforming");
    }
}
