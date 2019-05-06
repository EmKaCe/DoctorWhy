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
    ///// [Description]
    ///// <para>int: </para>
    ///// <para>string: </para>
    ///// </summary>
    //public static string EventName()
    //{
    //    return "EventName";
    //}

    #endregion


    /// <summary>
    /// Entity with entityID takes damage 
    /// <para>int: entity id of object that takes damage</para>
    /// <para>string[]: damage as float, entityID of entity that caused damage</para>
    /// </summary>
    public static string TakeDamage()
    {
        return "TakeDamage";
    }

    /// <summary>
    /// Entity with entityID died
    /// <para>int: entity id of entity that died</para>
    /// <para>string: entity id of entity that caused the damage</para>
    /// </summary>
    public static string PersonDied()
    {
        return "PersonDied";
    }

    /// <summary>
    /// Entity with entityID was healed
    /// <para>int: entity id of entity that was healed</para>
    /// <para>string[]: entity id of entity that caused the heal</para>
    /// </summary>
    public static string PersonWasHealed()
    {
        return "PersonWasHealed";
    }

    /// <summary>
    /// Entity ran out of oxygen
    /// <para>int: entitiyID of entity that run out of oxygen</para>
    /// <para>string[]: amount of oxygen that was missing</para>
    /// </summary>
    /// <returns></returns>
    public static string RanOutOfOxygen()
    {
        return "RanOutOfOxygen";
    }


    
}
