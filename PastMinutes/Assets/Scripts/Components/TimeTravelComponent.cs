﻿using System.Collections;
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
    private float block = 0.0f;
    [Tooltip("when active, player can travel through time")]
    public bool active;
    UnityAction<int, string[]> timeTravelListener;
    UnityAction<int, string[]> itemPickUpListener;
    UnityAction<int, string[]> winGameListener;
    UnityAction<int, string[]> falseWinListener;
    UnityAction<int, string[]> freezeListener;
    UnityAction<int, string[]> thawListener;
    private float loop = 0;
    public TextMeshProUGUI currentTimeLoopTextBox;
    public TextMeshProUGUI catastropheTextBox;
    private bool ForceTriggered = false;
    private bool freeze = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeLoopTextBox.text = loop.ToString();
        timeInPast = 60.0f;
        secondsToEnd = timeInPast;
        EventManager.StartListening(EventSystem.TravelTime(), timeTravelListener);
        EventManager.StartListening(EventSystem.WinGame(), winGameListener);
        EventManager.StartListening(EventSystem.FalseWin(), falseWinListener);
        EventManager.StartListening(EventSystem.FreezeTimer(), freezeListener);
        EventManager.StartListening(EventSystem.ThawTimer(), thawListener);

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
        //Debug.Log(gameObject.name);
        //Debug.Log("in past" + inPast);
        //Debug.Log("time left:" + secondsToEnd);
        block -= Time.deltaTime;
        catastropheTextBox.text = ((int)secondsToEnd).ToString();

        if (inPast)
        {
            //reduce timer
            if (secondsToEnd > 0.0f && !freeze)
            {
                Debug.Log("time left:" + secondsToEnd);
                secondsToEnd -= Time.deltaTime;
            }
            else if (secondsToEnd <= 0.0f)
            {
                if (!ForceTriggered)
                {
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
        if (active&&!freeze)
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
        freezeListener = new UnityAction<int, string[]>(Freeze);
        thawListener = new UnityAction<int, string[]>(Thaw);

    }
    public void BlockInfinite(int empty, string[] empty2)
    {
        block = 20f;
        //secondsToEnd = 0.1f;
        freeze = true;
    }
    public void FalseWin(int empty, string[] empty2)
    {
        block = 4.7f;
        secondsToEnd = timeInPast + 5.0f;
        loop++;

        //Emre setloopIn UI
        currentTimeLoopTextBox.text = loop.ToString();
        //Done
    }

    public void CheckParent(int itemID, string[] entityIDandItemName)
    {
        if (EntityManager.GetEntityComponent<EntityComponent>(itemID).gameObject.Equals(gameObject))
        {
            active = true;
            EventManager.StopListening(EventSystem.ItemAdded(), itemPickUpListener);
            //EventManager.StartListening(EventSystem.TravelTime(), timeTravelListener);
        }

    }
    public void Freeze(int empty, string[] empty2)
    {
        freeze = true;
    }
    public void Thaw(int empty, string[] empty2)
    {
        freeze = false;
    }
}
