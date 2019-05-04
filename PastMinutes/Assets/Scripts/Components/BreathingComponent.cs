using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingComponent : MonoBehaviour
{
    public float oxygenUsage;
    private List<OxygenComponent> oxygenSupply;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOxygenSupply(OxygenComponent supply)
    {
        oxygenSupply.Add(supply);
    }
}
