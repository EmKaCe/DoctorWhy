using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a static method for every event in the game. Method returns the event name as a string.
/// </summary>
public class EventSystem : MonoBehaviour {

    #region example method


    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="value2"></param>
    //public static void EventName(int value, string value2)
    //{
    //    EventManager.TriggerEvent("EventName", value, value2);
    //}

    #endregion


    /// <summary>
    /// Entity with entityID takes damage 
    /// </summary>
    /// <param name="entityID">entity id of object that takes damage</param>
    /// <param name="damage">damage as float</param>
    public static void TakeDamage(int entityID, string damage)
    {
        EventManager.TriggerEvent("TakeDamage", entityID, damage);
    }


}
