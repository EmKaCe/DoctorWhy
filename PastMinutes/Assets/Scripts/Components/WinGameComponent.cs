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
    private bool forceReset;
    private bool falseWin;
    public GameObject terrafomer;
    public GameObject WinAI;
    public GameObject FalseWinAI;
    public GameObject ForceResetAI;
    public GameObject endScreen;
    private float end;
    private float readDialog;
    public GameObject Present;
    public GameObject Past;
    private bool triggerDialog=false;
    public GameObject UI;

    private string AIName = "Spaceship AI";
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        forceReset = false;
        falseWin = false;
        tanim = terrafomer.GetComponent<Animator>();
        if (tanim == null)
        {
            Debug.Log("Terrafomer needs an Animator");
        }

        EventManager.StartListening(EventSystem.WinGame(), winGameListener);
        EventManager.StartListening(EventSystem.FalseWin(), falseWinListener);
        EventManager.StartListening(EventSystem.ForceReset(), forceResetListener);
    }
    private void Awake()
    {
        winGameListener = new UnityAction<int, string[]>(Win);
        falseWinListener = new UnityAction<int, string[]>(FalseWin);
        forceResetListener = new UnityAction<int, string[]>(ForceReset);
    }
    // Update is called once per frame
    void Update()
    {
        if (win) //event WinGame has been called somewhere (NPCAction WinGame in Dialog TarraformingDialog)
        {
            if (Time.time < end)
            {
                Debug.Log("WinRoutine");
                tanim.Play("terraforming");
                EventManager.TriggerEvent(EventSystem.CameraShake(), 0, new string[] { });
                //play for 5 seconds. 
            }

            if (Time.time >= end) //we've waited for 5 seconds
            {
                //Present.SetActive(false);
                //Past.SetActive(true);
                //keep from going forward in time
                tanim.Play("notterrafomring");
               
                //don't keep looping even though we've already won.
                if (!triggerDialog) {
                    triggerDialog = true;
                    EventManager.TriggerEvent(EventSystem.BeginConversation(),
                    WinAI.GetComponentInParent<EntityComponent>().entityID, new string[] { AIName });
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
            //we havent won yet, check for falseWin
            if (falseWin) //Dr Shila tricked us
            {
                Debug.Log("falseWinRoutine");
                tanim.Play("terraforming");
                //Present.SetActive(true);
                //Past.SetActive(false);
                EventManager.TriggerEvent(EventSystem.CameraShake(), 0, new string[] { });
                if (Time.time >= end) //we've waited for 5 seconds
                {
                    EventManager.TriggerEvent(EventSystem.TravelTime(), 0, new string[] { });
                    EventManager.TriggerEvent(EventSystem.BeginConversation(),
                    FalseWinAI.GetComponentInParent<EntityComponent>().entityID, new string[] { AIName });
                    Debug.Log("triggering FalseWinAi");
                    falseWin = false;
                    tanim.Play("notterrafomring");
                }

            }
            else
            { //check for ForceReset
                if (forceReset)
                {
                    Debug.Log("forceReset" + forceReset);
                    Debug.Log("forceResetRoutine");
                    tanim.Play("terraforming");
                    EventManager.TriggerEvent(EventSystem.CameraShake(), 0, new string[] { });
                    if (Time.time >= end) //we've waited for 5 seconds
                    {
                        EventManager.TriggerEvent(EventSystem.TravelTime(), 0, new string[] { });
                        EventManager.TriggerEvent(EventSystem.BeginConversation(),
                        ForceResetAI.GetComponentInParent<EntityComponent>().entityID, new string[] { AIName });
                        forceReset = false;
                        Debug.Log("force Reset over");
                        tanim.Play("notterrafomring");
                    }
                }
                else
                {
                    tanim.Play("notterrafomring");
                    Debug.Log("No time Event");
                }
            }
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
    public void ForceReset(int empty, string[] empty2)
    {
        Debug.Log("ForceReset got called");
        end = Time.time + 5.0f;
        forceReset = true;
    }
    public void FalseWin(int empty, string[] empty2)
    {
        end = Time.time + 5.0f;
        falseWin = true;
    }
}
