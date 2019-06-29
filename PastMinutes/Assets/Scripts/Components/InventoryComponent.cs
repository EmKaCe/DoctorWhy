using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryComponent : MonoBehaviour
{

    public List<ItemComponent> inventory;
    public int inventorySize;


    private void Awake()
    {
        inventory = new List<ItemComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerEvent(EventSystem.InventoryAdded(), gameObject.GetComponentInParent<EntityComponent>().gameObject.GetInstanceID(), new string[] { });
        
    }


    public void StartUp()
    {
        ItemComponent[] items = gameObject.GetComponentsInChildren<ItemComponent>();
        foreach (ItemComponent item in items)
        {
            EventManager.TriggerEvent(EventSystem.AddItemToInventory(), item.gameObject.GetInstanceID(), new string[] { gameObject.GetComponentInParent<EntityComponent>().gameObject.GetInstanceID().ToString() });
        }
    }

    /// <summary>
    /// Adds item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if space in inventory/ false if not</returns>
    public bool AddItem(ItemComponent item)
    {
        if (item.stackable)
        {
            foreach(ItemComponent i in inventory)
            {
                if (i.itemName.Equals(item.itemName)){
                    i.AddToStack(item.GetAmount());
                    return true;
                }
            }
        }
        if(inventory.Count < inventorySize)
        {
            inventory.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns>Amount that was removed</returns>
    public int RemoveItem(ItemComponent item, int amount)
    {
        int res = 0;
        foreach(ItemComponent i in inventory)
        {
            if (i.itemName.Equals(item.itemName))
            {
                res = i.TakeFromStack(amount);
                if(i.GetAmount() == 0)
                {
                    inventory.Remove(i);
                }
                return res;
                
            }
        }
        return res;
    }

    /// <summary>
    /// Returns all ItemComponents as list
    /// </summary>
    /// <returns></returns>
    public List<ItemComponent> ShowInventory()
    {
        return inventory;
    }

    public bool ContainsItem(string name)
    {
        foreach(ItemComponent i in inventory)
        {
            if (i.Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    
}
