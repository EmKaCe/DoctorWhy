using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{

    Dictionary<int, InventoryComponent> inventories;
    UnityAction<int, string[]> addInventoryListener;
    UnityAction<int, string[]> addItemListener;
    UnityAction<int, string[]> removeItemListener;

    private void Awake()
    {
        inventories = new Dictionary<int, InventoryComponent>();
        addInventoryListener = new UnityAction<int, string[]>(AddInventory);
        addItemListener = new UnityAction<int, string[]>(AddItemToInventory);
        removeItemListener = new UnityAction<int, string[]>(RemoveItemFromInventory);
    }

    private void AddItemToInventory(int itemEntityID, string[] playerEntityID)
    {
        //gets EntitiyID of player
        int.TryParse(playerEntityID[0], out int res);
        //gets inventory of player
        inventories.TryGetValue(res, out InventoryComponent inventory);
        ItemComponent item = EntityManager.GetEntityComponent<ItemComponent>(itemEntityID) as ItemComponent;
        if (inventory.AddItem(item))
        {
            EventManager.TriggerEvent(EventSystem.ItemAdded(), itemEntityID, new string[] { res.ToString(), item.name });
        }
        else
        {
            EventManager.TriggerEvent(EventSystem.InventoryFull(), itemEntityID, new string[] { item.name });
        }
    }

    private void RemoveItemFromInventory(int itemEntityID, string[] playerEntityID)
    {
        //gets EntitiyID of player
        int.TryParse(playerEntityID[0], out int playerID);
        int.TryParse(playerEntityID[1], out int amount);
        //gets inventory of player
        inventories.TryGetValue(playerID, out InventoryComponent inventory);
        ItemComponent item = EntityManager.GetEntityComponent<ItemComponent>(itemEntityID) as ItemComponent;
        int actualAmount = inventory.RemoveItem(item, amount);
        if (actualAmount > 0)
        {
            item.gameObject.transform.parent = null;
            item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            item.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //Add item to game object of InventoryComponent
            
            EventManager.TriggerEvent(EventSystem.ItemRemoved(), itemEntityID, new string[] { playerID.ToString(), item.name, amount.ToString() });
        }
        else
        {
            Debug.Log("Item doesnt exist in inventory");
        }
    }

    private void AddInventory(int entityID, string[] empty)
    {
        InventoryComponent i = EntityManager.GetEntityComponent<InventoryComponent>(entityID) as InventoryComponent;
        inventories.Add(entityID, i);
        i.StartUp();

    }

    private void OnEnable()
    {
        EventManager.StartListening(EventSystem.InventoryAdded(), addInventoryListener);
        EventManager.StartListening(EventSystem.AddItemToInventory(), addItemListener);
        EventManager.StartListening(EventSystem.RemoveItemFromInventory(), removeItemListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventSystem.InventoryAdded(), addInventoryListener);
        EventManager.StopListening(EventSystem.AddItemToInventory(), addItemListener);
        EventManager.StartListening(EventSystem.RemoveItemFromInventory(), removeItemListener);
    }
}
