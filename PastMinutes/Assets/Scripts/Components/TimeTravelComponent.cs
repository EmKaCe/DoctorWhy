using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeTravelComponent : MonoBehaviour
{
    public GameObject present;
    public GameObject past;
    private float timeInPast; //total possible time in the time loop 
    private float secondsToEnd; //time left till ForceReset
    private bool inPast;
    private float block=0.0f;
    [Tooltip("when active, player can travel through time")]
    public bool active;
    UnityAction<int, string[]> timeTravelListener;
    UnityAction<int, string[]> itemPickUpListener;
    UnityAction<int, string[]> winGameListener;
    UnityAction<int, string[]> falseWinListener;
    private float loop=0;
    public TextMeshProUGUI currentTimeLoopTextBox;
    public TextMeshProUGUI catastropheTextTextBox;
    public TextMeshProUGUI catastropheValueTextBox;
    private bool ForceTriggered = false;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeLoopTextBox.text = loop.ToString();
        timeInPast = 60.0f;
        secondsToEnd = timeInPast;
        EventManager.StartListening(EventSystem.TravelTime(), timeTravelListener);
        EventManager.StartListening(EventSystem.WinGame(), winGameListener);
        EventManager.StartListening(EventSystem.FalseWin(), falseWinListener);

        if (present == null || past == null)
        {
            Debug.Log("TimeTravelComponent needs a present and past object");
        }
        inPast = past.activeSelf;

        if (!active)
        {
            EventManager.StartListening(EventSystem.ItemAdded(), itemPickUpListener);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("in past" + inPast);
        Debug.Log("time left:" + secondsToEnd);
        block -= Time.deltaTime;


        if(inPast)
        {
            if(!catastropheTextTextBox.IsActive())
            {
                catastropheTextTextBox.gameObject.SetActive(true);
                catastropheValueTextBox.gameObject.SetActive(true);
            }
            catastropheValueTextBox.text = Math.Round((Decimal)secondsToEnd, 1, MidpointRounding.AwayFromZero).ToString();
        }
        else
        {
            if(catastropheTextTextBox.IsActive())
            {
                catastropheTextTextBox.gameObject.SetActive(false);
                catastropheValueTextBox.gameObject.SetActive(false);
            }
        }

        if (inPast)
        {
            //reduce timer
            if(secondsToEnd > 0.0f&&!win)
            {
                Debug.Log("time left:"+ secondsToEnd);
                secondsToEnd -= Time.deltaTime;
            }
            else if(secondsToEnd<=0.0f) {
                if (!ForceTriggered) {
                    ForceTriggered = true;
                    Debug.Log("Zeit abgelaufen");
                    block = 4.7f;
                    EventManager.TriggerEvent(EventSystem.ForceReset(), 0, new string[] { });
                }
                
                //Done
                secondsToEnd -= Time.deltaTime;
                if (secondsToEnd <= -5.0f)
                {
                    ForceTriggered = false;
                    //TravelTime(0, new string[] { });
                    secondsToEnd = timeInPast;
                    loop++;

                    //Emre setloop in UI
                    currentTimeLoopTextBox.text = loop.ToString();
                }
            }

        }
        
    }

    public void TravelTime(int empty, string[] empty2)
    {
        if (active)
        {
            if (block <= 0.0f)
            {
                Debug.Log("TravelTime");
                present.SetActive(!present.activeSelf);
                past.SetActive(!past.activeSelf);
                inPast = past.activeSelf;
                EventManager.TriggerEvent(EventSystem.TravelingTime(), 0, new string[] { });
            }
            else
            {
                Debug.Log("blocked travel attempt");
            }
        }
        
    }

    private void Awake()
    {
        timeTravelListener = new UnityAction<int, string[]>(TravelTime);
        itemPickUpListener = new UnityAction<int, string[]>(CheckParent);
        winGameListener = new UnityAction<int, string[]>(BlockInfinite);
        falseWinListener = new UnityAction<int, string[]>(FalseWin);
        
    }
    public void BlockInfinite (int empty, string[] empty2)
    {
        block = 20f;
        //secondsToEnd = 0.1f;
        win = true;
    }
    public void FalseWin(int empty, string[] empty2)
    {
        block = 4.7f;
        secondsToEnd = timeInPast+5.0f;
        loop++;

        //Emre setloopIn UI
        currentTimeLoopTextBox.text = loop.ToString();
        //Done
    }

    public void CheckParent(int itemID, string[] entityIDandItemName)
    {
        if (EntityManager.GetEntityComponent<EntityComponent>(itemID).gameObject.Equals(gameObject)){
            active = true;
            EventManager.StopListening(EventSystem.ItemAdded(), itemPickUpListener);
            //EventManager.StartListening(EventSystem.TravelTime(), timeTravelListener);
        }

    }
}
