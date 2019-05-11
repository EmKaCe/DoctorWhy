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
    /// <para>int: entityID of entity that run out of oxygen</para>
    /// <para>string[]: amount of oxygen that was missing</para>
    /// </summary>
    /// <returns>"RanOutOfOxygen"</returns>
    public static string RanOutOfOxygen()
    {
        return "RanOutOfOxygen";
    }

    /// <summary>
    /// Entity switched gun
    /// <para>int: entityID of entity that switched the gun</para>
    /// <para>string[]: name of the gun</para>
    /// </summary>
    /// <returns>"GunSwitched"</returns>
    public static string GunSwitched()
    {
        return "GunSwitched";
    }

    /// <summary>
    /// Entity changed Attachment
    /// <para>int: entityID of entity that changed Attachment</para>
    /// <para>string[]: name of the gun, attachmentSlot, name of attachment</para>
    /// </summary>
    /// <returns>"AttachmentChanged"</returns>
    public static string AttachmentChanged()
    {
        return "AttachmentChanged";
    }

    /// <summary>
    /// Entity added attachment
    /// <para>int: entityID of entity that added attachment</para>
    /// <para>string[]: name of the gun, attachmentSlot, name of attachment</para>
    /// </summary>
    /// <returns>"AttachmentAdded"</returns>
    public static string AttachmentAdded()
    {
        return "AttachmentAdded";
    }

    /// <summary>
    /// Entity removed attachment
    /// <para>int: entityID of entity that removed attachment</para>
    /// <para>string[]: name of the gun, attachmentSlot, name of attachment</para>
    /// </summary>
    /// <returns>"AttachmentRemoved"</returns>
    public static string AttachmentRemoved()
    {
        return "AttachmentRemoved";
    }

    /// <summary>
    /// Entity pulled Trigger
    /// <para>int: entityID of entity that pulled Trigger</para>
    /// <para>string[]: name of the gun</para>
    /// </summary>
    /// <returns>"AttachmentRemoved"</returns>
    public static string TriggerPulled()
    {
        return "TriggerPulled";
    }

    /// <summary>
    /// Shooting component gets initialized (called on start)
    /// <para>int: entityID of entity that contains ShootingComponent</para>
    /// <para>string[]: 0</para>
    /// </summary>
    /// <returns>"InitializingShootingComponent"/></returns>
    public static string InitializingShootingComponent()
    {
        return "InitializingShootingComponent";
    }



    
}
