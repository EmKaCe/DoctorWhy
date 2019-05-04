using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenComponent : MonoBehaviour
{
    /// <summary>
    /// Usage of oxygen per second
    /// </summary>
    public float oxygenUsage;
    public float startingOxLevel;
    private float oxygenLevel;
    private int displayValue;

    // Start is called before the first frame update
    void Start()
    {
        oxygenLevel = startingOxLevel;
    }

    // Update is called once per frame
    void Update()
    {
        oxygenLevel -= oxygenUsage * Time.deltaTime;
        displayValue = (int) oxygenLevel;
    }

    public int GetOxygenDisplayValue()
    {
        return displayValue;
    }

    public float GetOxygenLevel()
    {
        return oxygenLevel;
    }
}
