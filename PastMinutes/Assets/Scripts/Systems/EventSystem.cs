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
    /// Called when timer reaches zero
    /// </summary>
    /// <returns></returns>
    public static string EndWorld()
    {
        return "EndWorld";
    }

    public static string StartWorldEnd()
    {
        return "StartWorldEnd";
    }

    /// <summary>
    /// Entity with entityID died
    /// <para>int: entity id of entity that died</para>
    /// <para>string: amount of damage that was caused, entity id of entity that caused the damage</para>
    /// </summary>
    public static string PersonDied()
    {
        return "PersonDied";
    }

    /// <summary>
    /// Entity with entityID took damage
    /// <para>int: entity id of entity that took damage</para>
    /// <para>string: amount of damage that was caused, entity id of entity that caused the damage</para>
    /// </summary>
    public static string DamageTaken()
    {
        return "DamageTaken";
    }

    /// <summary>
    /// Entity with entityID should receive a heal
    /// <para>int: entityID of entity that should receive heal</para>
    /// <para>string: amount the person should be healed, entity id of entity that causes the heal</para>
    /// </summary>
    /// <returns></returns>
    public static string HealPerson()
    {
        return "HealPerson";
    }

    /// <summary>
    /// Entity with entityID was healed
    /// <para>int: entity id of entity that was healed</para>
    /// <para>string[]: amount the person was healed, entity id of entity that caused the heal</para>
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

    /// <summary>
    /// Button for interaction was pressed or npc wants to interact
    /// <para>int: entityID of person who wants to interact with entity (usually player)</para>
    /// </summary>
    /// <returns>"PickUpItem"</returns>
    public static string Interact()
    {
        return "Interact";
    }

    /// <summary>
    /// Button for interaction was released or npc stops to interact
    /// <para>int: entityID of person who stops to hold-interact with entity (usually player)</para>
    /// </summary>
    /// <returns>"PickUpItem"</returns>
    public static string StopHoldingInteract()
    {
        return "StopHoldingInteract";
    }

    /// <summary>
    /// Triggered after PickUpItem. Answer of Item containing item entityID and person entityID
    /// <para>int: entityID of item that should be picked up</para>
    /// <para>string[]: entityID of player/npc who wants to pick up item</para>
    /// </summary>
    /// <returns></returns>
    public static string AddItemToInventory()
    {
        return "AddItemToInventory";
    }

    /// <summary>
    /// Called when new inventory is initialized
    /// <para>int: entityID of object that gets new inventory</para>
    /// </summary>
    /// <returns></returns>
    public static string InventoryAdded()
    {
        return "InventoryAdded";
    }

    /// <summary>
    /// Inventory is too full to add item
    /// <para>int: entityID of item that couldn't be added</para>
    /// <para>string[]: name of item</para>
    /// </summary>
    /// <returns></returns>
    public static string InventoryFull()
    {
        return "InventoryFull";
    }

    /// <summary>
    /// Called when Item was added
    /// <para>int: entityID of object that was added</para>
    /// <para>string[]: entityID of player/npc, name of item</para>
    /// </summary>
    /// <returns></returns>
    public static string ItemAdded()
    {
        return "ItemAdded";
    }

    /// <summary>
    /// Called when item should be removed from inventory
    /// <para>int: entityID of object that gets removed</para>
    /// <para>string[]: entityID of player/npc, amount that should be removed</para>
    /// </summary>
    /// <returns></returns>
    public static string RemoveItemFromInventory()
    {
        return "RemoveItemFromInventory";
    }

    /// <summary>
    /// Called when Item was removed
    /// <para>int: entityID of object that was removed</para>
    /// <para>string[]: entityID of player/npc, name of item, amount that was removed</para>
    /// </summary>
    /// <returns></returns>
    public static string ItemRemoved()
    {
        return "ItemRemoved";
    }

    /// <summary>
    /// Called by DialogComponent when Conversation gets started
    /// </summary>
    /// <returns></returns>
    public static string BeginConversation()
    {
        return "BeginConversation";
    }

    /// <summary>
    /// Triggered by DialogComponent. Contains string array with dialog options
    /// (each entry like the following scheme: "index:dialog")
    /// and with the index of the last selected answer
    /// </summary>
    /// <returns></returns>
    public static string SendDialogOptions()
    {
        return "SendDialogOptions";
    }

    public static string GetDialogOptions()
    {
        return "GetDialogOptions";
    }

    /// <summary>
    /// Called by DialogComponent when Conversation was ended
    /// </summary>
    /// <returns></returns>
    public static string EndConversation()
    {
        return "EndConversation";
    }

    /// <summary>
    /// Called by interactionComponent (or classes that derive from interactionComponent) when interaction area is entered
    /// int: 0;
    /// string[] {Interaction message for player, type of interactionComponent}
    /// </summary>
    /// <returns></returns>
    public static string InteractionTriggered()
    {
        return "InteractionTriggered";
    }

    /// <summary>
    /// Called by interactionComponent (or classes that derive from interactionComponent) when interaction area is exited
    /// int: collider entityID;
    /// string[] {Interaction message for player, type of interactionComponent}
    /// </summary>
    /// <returns></returns>
    public static string InteractionExited()
    {
        return "InteractionExited";
    }

    /// <summary>
    /// Called by inputManager
    /// <para>int and string[] are empty</para>
    /// </summary>
    /// <returns></returns>
    public static string TravelTime()
    {
        return "TravelTime";
    }

    /// <summary>
    /// Called by TimeTravelComponent when time travel is engaged
    /// <para>int and string[] are empty</para>
    /// </summary>
    /// <returns></returns>
    public static string TravelingTime()
    {
        return "TravelingTime";
    }

    /// <summary>
    /// Called by InputManager or AI-Script to trigger reload
    /// </summary>
    /// <returns></returns>
    public static string Reload()
    {
        return "Reload";
    }
}
