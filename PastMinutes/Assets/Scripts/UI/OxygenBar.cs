using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OxygenBar : MonoBehaviour
{
    /* Careful: This will be one of those classes that magically work and you shouldn't touch it.
     * Else everything will probably break and nobody will know why it worked or why it broke.
     */
    Slider oxygenBar; //Oxygen UI Slider
    public BreathingComponent breathingComponent; //Shitty breathing component
   // private int remainingOxygenComponents; //remaining amount of oxygen components

    // Start is called before the first frame update
    void Start()
    {
        oxygenBar = gameObject.GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // remainingOxygenComponents = breathingComponent.getCurrentSupplyCount() - 1;
        oxygenBar.value = breathingComponent.GetCurrentOxygenLevel();
        oxygenBar.maxValue = breathingComponent.GetMaxOxygenLevel();
    }
}
