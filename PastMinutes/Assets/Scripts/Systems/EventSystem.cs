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

    /// <summary>
    /// Entity with entityID died
    /// </summary>
    /// <param name="entityID">entity id of entity that died</param>
    /// <param name="damageCauseID">entity id of entity that caused the damage</param>
    public static void PersonDied(int entityID, string damageCauseID)
    {
        EventManager.TriggerEvent("PersonDied", entityID, damageCauseID);
    }

    /// <summary>
    /// Entity with entityID was healed
    /// </summary>
    /// <param name="entityID">entity id of entity that was healed</param>
    /// <param name="healerID">entity id of entity that caused the heal</param>
    public static void PersonWasHealed(int entityID, string healerID)
    {
        EventManager.TriggerEvent("PersonWasHealed", entityID, healerID);
    }

    public static void RanOutOfOxygen(int entityID, string missingOxygen)
    {
        EventManager.TriggerEvent("RanOutOfOxygen", entityID, missingOxygen);
    }
}
