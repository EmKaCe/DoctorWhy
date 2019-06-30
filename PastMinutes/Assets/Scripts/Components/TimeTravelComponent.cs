using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTravelComponent : MonoBehaviour
{
    public GameObject present;
    public GameObject past;
    public float secondsToEnd;
    public float startEnd;
    private bool inPast;
    private bool endStarted;
    private bool endReached;
    [Tooltip("when active, player can travel through time")]
    public bool active;
    UnityAction<int, string[]> timeTravelListener;
    UnityAction<int, string[]> itemPickUpListener;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventSystem.TravelTime(), timeTravelListener);
        if (present == null || past == null)
        {
            Debug.Log("TimeTravelComponent needs a present and past object");
        }
        inPast = past.activeSelf;
        if(secondsToEnd > startEnd)
        {
            endStarted = false;
        }
        else
        {
            endStarted = true;
        }
        if(secondsToEnd > 0)
        {
            endReached = false;
        }
        else
        {
            endReached = true;
        }
        if (!active)
        {
            EventManager.StartListening(EventSystem.ItemAdded(), itemPickUpListener);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inPast)
        {
            //reduce timer
            if(secondsToEnd > 0)
            {
                secondsToEnd -= Time.deltaTime;
            }
            else if(secondsToEnd <= 0 && !endReached)
            {
                endReached = true;
                Debug.Log("Zeit abgelaufen");
                EventManager.TriggerEvent(EventSystem.ForceReset(), 0, new string[] { });
            }
            
        }
    }

    public void TravelTime(int empty, string[] empty2)
    {
        if (active)
        {
            present.SetActive(!present.activeSelf);
            past.SetActive(!past.activeSelf);
            inPast = past.activeSelf;
            EventManager.TriggerEvent(EventSystem.TravelingTime(), 0, new string[] { });
        }
        
    }

    private void Awake()
    {
        timeTravelListener = new UnityAction<int, string[]>(TravelTime);
        itemPickUpListener = new UnityAction<int, string[]>(CheckParent);
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
