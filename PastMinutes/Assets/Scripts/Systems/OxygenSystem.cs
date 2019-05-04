using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSystem : MonoBehaviour
{
    /// <summary>
    /// Every entity that uses a BreathingComponent
    /// </summary>
    private List<BreathingComponent> breather;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Reduce oxygen level
        foreach(BreathingComponent breath in breather)
        {

        }
    }
}
