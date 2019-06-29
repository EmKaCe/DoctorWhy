﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RemainingOxygen : MonoBehaviour
{
    Text remainingOxygenText; //Remaining Oxygen Text
    public BreathingComponent breathingComponent; //Shitty breathing component

    // Start is called before the first frame update
    void Start()
    {
        remainingOxygenText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int remain = breathingComponent.GetCurrentSupplyCount();
        if(remain > 1)
        {
            remain--;
        }
        remainingOxygenText.text = remain.ToString();

    }
}
