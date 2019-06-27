using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component for items that store oxygen
/// </summary>
public class OxygenComponent : MonoBehaviour
{

    public float startingOxLevel;
    private float oxygenLevel;

    private void Awake()
    {
        oxygenLevel = startingOxLevel;
    }

    // Update is called once per frame
    void Update()
    {

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
    public void RemoveOxygen(float decrease, out float remains)
    {
        if(oxygenLevel < decrease)
        {
            decrease = decrease - oxygenLevel;
            oxygenLevel = 0;
            remains = decrease;
        }
        else
        {
            oxygenLevel -= decrease;
            remains = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refill"></param>
    /// <returns>returns surplus oxygen,if existing</returns>
    public void RefillOxygen(float refill, out float remains)
    {
        float rest = oxygenLevel + refill - startingOxLevel;
        if (rest > 0)
        {
            oxygenLevel = startingOxLevel;
            remains = rest;
        }
        else
        {
            oxygenLevel += refill;
            remains = 0;
        }
    }

}
