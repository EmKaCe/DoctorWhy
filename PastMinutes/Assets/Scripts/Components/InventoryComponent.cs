using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{

    public List<ItemComponent> inventory;
    public int inventorySize;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<ItemComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
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
                    i.AddToStack(1);
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
                
            }
        }
        return res;
    }
}
