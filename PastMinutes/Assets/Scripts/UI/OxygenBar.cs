using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    /* Careful: This will be one of those classes that magically work and you shouldn't touch it.
     * Else everything will probably break and nobody will know why it worked or why it broke.
     */
    public Slider oxygenBar; //Oxygen UI Slider
    public BreathingComponent breathingComponent; //Shitty breathing component
    private OxygenComponent currentOxygen; //currently used oxygen component
    private int remainingOxygenComponents; //remaining amount of oxygen components

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = breathingComponent.GetCurrentlyUsedOxygenCompenent();
        remainingOxygenComponents = breathingComponent.getCurrentSupplyCount() - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
