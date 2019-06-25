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

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = breathingComponent.GetCurrentlyUsedOxygenCompenent();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentOxygen.GetOxygenLevel() == 0)
        {
            currentOxygen = breathingComponent.GetCurrentlyUsedOxygenCompenent();
        }
        oxygenBar.value = currentOxygen.GetOxygenLevel();
    }
}
