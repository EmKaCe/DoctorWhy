﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public float weight;
    [Header("Size in cm:                       length             width            height")]
    [Tooltip("Arbitary text message")]
    public Vector3 size;
    public bool stackable;
    [Header("Amount of Items on Stack. 1 if stackable is false")]
    [SerializeField]
    private int amount;


    // Start is called before the first frame update
    void Start()
    {
        if (!stackable)
        {
            amount = 1;
        }
    }

    public void AddToStack(int pAmount)
    {
        if (stackable)
        {
            amount += pAmount;
        }
    }

    /// <summary>
    /// returns Amount that was taken
    /// </summary>
    /// <param name="pAmount"></param>
    /// <returns></returns>
    public int TakeFromStack(int pAmount)
    {
        if(amount > pAmount)
        {
            amount -= pAmount;
            return pAmount;
        }
        //just to save amount that should be returned before setting amount in inventory to 0
        pAmount = amount;
        amount = 0;
        return pAmount;
    }

    public int GetAmount()
    {
        return amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
