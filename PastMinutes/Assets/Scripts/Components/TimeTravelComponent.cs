using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelComponent : MonoBehaviour
{
    public GameObject present;
    public GameObject past;
    public float secondsToEnd;
    public float startEnd;
    private bool inPast;
    private bool endStarted;
    private bool endReached;
    // Start is called before the first frame update
    void Start()
    {
        if(present == null || past == null)
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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TravelTime();
        }
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
                Debug.Log("Die Welt ist untergegangen");
                EventManager.TriggerEvent(EventSystem.EndWorld(), 0, new string[] { });
            }
            if (secondsToEnd <= startEnd && !endStarted)
            {
                endStarted = true;
                Debug.Log("Tanz für mich terraformer, tanz!");
                EventManager.TriggerEvent(EventSystem.StartWorldEnd(), 0, new string[] { });
            }
            
        }
    }

    private void TravelTime()
    {
        present.SetActive(!present.activeSelf);
        past.SetActive(!past.activeSelf);
        inPast = past.activeSelf;
    }
}
