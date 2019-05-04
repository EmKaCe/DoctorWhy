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

    }

    public int GetOxygenDisplayValue()
    {
        return displayValue;
    }

    public float GetOxygenLevel()
    {
        return oxygenLevel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decrease"></param>
    /// <returns>returns missing oxygen if decrease is greater than oxygenLevel</returns>
    public float RemoveOxygen(float decrease)
    {
        if(oxygenLevel < decrease)
        {
            decrease = decrease - oxygenLevel;
            oxygenLevel = 0;
            return decrease;
        }
        else
        {
            oxygenLevel -= decrease;
            return 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refill"></param>
    /// <returns>returns surplus oxygen,if existing</returns>
    public float RefillOxygen(float refill)
    {
        float rest = oxygenLevel + refill - startingOxLevel;
        if (rest > 0)
        {
            oxygenLevel = startingOxLevel;
            return rest;
        }
        else
        {
            oxygenLevel += refill;
            return 0;
        }
    }
}
