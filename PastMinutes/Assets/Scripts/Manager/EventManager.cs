using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

   [System.SerializableAttribute]
    public class ThisEvent : UnityEvent<int, string>
    {

    }
    private Dictionary <string, ThisEvent> eventDictionary;

    private static EventManager eventManager;


    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, ThisEvent>();
        }
    }

    /// <summary>
    /// Action "listener" gets entered into the event dicionary to be called when event "eventName" happens
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void StartListening(string eventName, UnityAction<int, string> listener)
    {
        ThisEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ThisEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int, string> listener)
    {
        if (eventManager == null) return;
        ThisEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Debug.Log(eventName);
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent (string eventName, int entityID, string value)
    {
        ThisEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(entityID, value);
        }
    }



    public static string CreateMessageString(string[] values)
    {
        string s = "";
        for(int i = 0; i < (values.Length - 1); i++)
        {
            s += values[i];
            s += '|';
        }
        s += values[(values.Length - 1)];
        return s;
    }

    public static string CreateMessageString(string[] values, char separator)
    {
        string s = "";
        for (int i = 0; i < (values.Length - 1); i++)
        {
            s += values[i];
            s += separator;
        }
        s += values[(values.Length - 1)];
        return s;
    }


    /// <summary>
    /// Splits the message-string into a string-array using '|' as separator
    /// </summary>
    /// <param name="message">message of EventSystem</param>
    /// <returns>string[]</returns>
    public static string[] SplitMessageStringInArray(string message)
    {
        string[] s = message.Split('|');
        return s;
    }

    /// <summary>
    /// Splits the message-string into a string-array using 'separator' as separator
    /// </summary>
    /// <param name="message">message of EventSystem</param>
    /// <param name="seperator">separator to seperate "message"</param>
    /// <returns>string[]</returns>
    public static string[] SplitMessageStringInArray(string message, char seperator)
    {
        string[] s = message.Split(seperator);
        return s;
    }

    /// <summary>
    /// Returns the first object of the message-string as an int. 
    /// </summary>
    /// <param name="message">message of EventSystem</param>
    /// <returns>int (usually an EntityID)</returns>
    public static int GetOtherID(string message)
    {
        return int.Parse(SplitMessageStringInArray(message)[0]);
    }

    /// <summary>
    /// Returns the value of the message on "pos" as a int
    /// Uses SplitMessageStringInArray(message)
    /// </summary>
    /// <param name="message">message of EventSystem</param>
    /// <param name="pos">position of the value in message</param>
    /// <returns>int</returns>
    public static int GetOtherInt(string message, int pos)
    {
        return int.Parse(SplitMessageStringInArray(message)[pos]);
    }

    /// <summary>
    /// Returns the value of the message on "pos" as a float
    /// Uses SplitMessageStringInArray(message)
    /// </summary>
    /// <param name="message">message of EventSystem</param>
    /// <param name="pos">position of the value in message</param>
    /// <returns>float</returns>
    public static float GetOtherValue(string message, int pos)
    {
        return float.Parse(SplitMessageStringInArray(message)[pos]);
    }

    /*
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public delegate void DamageAction();
    public static event DamageAction TookDamage;
    public delegate void FightAction();
    public static event FightAction FightHappened;
    public bool damageTaken;
    public string message;


    void Start()
    {
        damageTaken = false;
        message = "";
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
        {
            if (OnClicked != null)
                OnClicked();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTaken)
        {
            if(TookDamage != null)
            {
                TookDamage();
            }
        }
       
    }

     void SetDamageTaken(bool pDT)
    {
        damageTaken = pDT;
    }

    public void SetMessage(string pDT)
    {
        message = pDT;
    }

    */
}
