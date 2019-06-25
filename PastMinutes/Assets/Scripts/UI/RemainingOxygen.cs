using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingOxygen : MonoBehaviour
{
    public Text RemainingOxygenText; //Remaining Oxygen Text
    public BreathingComponent breathingComponent; //Shitty breathing component

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RemainingOxygenText.text = (breathingComponent.getCurrentSupplyCount() - 1).ToString();
    }
}
