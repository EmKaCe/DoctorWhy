using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{

    public List<ItemComponent> inventory;
    public int inventorySize;
    [Tooltip("Amount of item slots in the inventory screen, could be for weapons, armor, etc.")]
    public int amountItemSlots = 7;
    ItemComponent[] itemSlots;


    private void Awake()
    {
        inventory = new List<ItemComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        itemSlots = new ItemComponent[amountItemSlots];
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
                    Destroy(i.gameObject);
                    return true;
                }
            }
        }
        if(inventory.Count < inventorySize)
        {
            inventory.Add(item);
            int pos;
            if((pos = CheckSlot(item.category)) > -1) 
            {
                AddItemToSlot(pos, item);              
            }
            else
            {
                item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                item.gameObject.transform.parent = transform;
            }
            
            item.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// returns slot number if slot is empty, otherwise -1
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    private int CheckSlot(ItemComponent.ItemType itemType)
    {
        if (itemType == ItemComponent.ItemType.Clothing)
        {
            for(int i = 3; i < 7; i++)
            {
                if(itemSlots[i] == null)
                {
                    return i;
                }
            }
        }
        else if(itemType == ItemComponent.ItemType.Weapon)
        {
            if(itemSlots[1] == null)
            {
                return 1;
            }
            else if (itemSlots[2] == null)
            {
                return 2;
            }
        }
        else if(itemType == ItemComponent.ItemType.TimeGauntlet)
        {
            if (itemSlots[0] == null)
            {
                return 0;
            }
        }
        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns>Amount that was removed</returns>
    public int RemoveItem(ItemComponent item, int amount)
    {
        RemoveItemFromSlot(item);
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

    public int GetAmmunition(PartFindingSystem.AmmoType ammoType, int neededAmmo)
    {
        foreach(ItemComponent i in inventory)
        {
            if(i.category == ItemComponent.ItemType.Ammunition)
            {
                if(i.gameObject.GetComponent<AmmunitionComponent>().ammoType == ammoType)
                {
                    return RemoveItem(i, neededAmmo);
                }
            }
        }
        return 0;
    }

    public void AddItemToSlot(int slot, ItemComponent item)
    {
        if(slot < amountItemSlots && slot > 0)
        {
            ItemComponent removed = itemSlots[slot];
            if(removed != null)
            {
                RemoveItemFromSlot(removed);
            }
            itemSlots[slot] = item;
            if(0 < slot && slot < 3)
            {
                Debug.Log("waffe");
                
                item.transform.parent = transform.parent.Find("Arms").transform;
                item.transform.localPosition = new Vector3(0.53f, 0, 0.7f);

            }
            EventManager.TriggerEvent(EventSystem.AddedItemToSlot(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[]{ item.gameObject.GetComponent<EntityComponent>().entityID.ToString(),
                                                                                                                                                 item.itemName, slot.ToString()});
        }
    }

    public ItemComponent GetItemInSlot(int slot)
    {
        return itemSlots[slot];
    }

    private void RemoveItemFromSlot(ItemComponent item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Equals(item))
            {
                item.transform.parent = transform;
                item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                EventManager.TriggerEvent(EventSystem.RemovedItemFromSlot(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[] { item.gameObject.GetComponent<EntityComponent>().entityID.ToString(),
                                                                                                                                                 item.itemName, i.ToString()});
                return;

            }
        }
    }

    
}
