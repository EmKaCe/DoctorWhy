using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component for entities that breathe oxygen
/// </summary>
public class BreathingComponent : MonoBehaviour
{
    public float oxygenUsage;
    private Dictionary<int, OxygenComponent> oxygenSupply;
    private Dictionary<int, OxygenComponent> remainingSupply;
    private int currentlyUsed;
    private bool oxygenRemaining;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        currentlyUsed = -1;
        oxygenRemaining = false;
        oxygenSupply = new Dictionary<int, OxygenComponent>();
        remainingSupply = new Dictionary<int, OxygenComponent>();
        OxygenComponent[] oComp = gameObject.GetComponentsInChildren<OxygenComponent>() as OxygenComponent[];
        foreach(OxygenComponent o in oComp)
        {
            AddOxygenSupply(o.gameObject.GetInstanceID());
            oxygenRemaining = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oxygenRemaining)
        {
            DecreaseOxygenLevel(oxygenUsage * Time.deltaTime);
        }
        
    }

    /// <summary>
    /// Adds oxygenComponent of entity with entityID = supplyID
    /// </summary>
    /// <param name="supplyID"></param>
    public void AddOxygenSupply(int supplyID)
    {
        OxygenComponent comp = EntityManager.GetEntityComponent(supplyID, "OxygenComponent") as OxygenComponent;       
        oxygenSupply.Add(supplyID, comp);
        if(comp.GetOxygenLevel() > 0)
        {
            remainingSupply.Add(supplyID, comp);
            if(currentlyUsed == -1 || comp.GetOxygenLevel() > oxygenSupply[currentlyUsed].GetOxygenLevel())
            {
                currentlyUsed = supplyID;
            }
            else
            {
               // currentlyUsed = supplyID;
            }
        }
    }

    /// <summary>
    /// Removes oxygen supply with supplyID from dictionaries
    /// </summary>
    /// <param name="supplyID">Id of entity with oxygenComponent that should be removed from dictionary</param>
    public void RemoveOxygenSupply(int supplyID)
    {
        oxygenSupply.Remove(supplyID);
        remainingSupply.Remove(supplyID);
    }

    /// <summary>
    /// Decreases the avaivable oxygen by 'decrease' amount
    /// </summary>
    /// <param name="decrease"></param>
    public void DecreaseOxygenLevel(float decrease)
    {
        oxygenSupply[currentlyUsed].RemoveOxygen(decrease, out float remains);
        if (remains > 0)
        {
            if (FindNextOxygenSource())
            {
                DecreaseOxygenLevel(remains);
            }
            else
            {
                oxygenRemaining = false;
                EventManager.TriggerEvent(EventSystem.RanOutOfOxygen(), gameObject.GetInstanceID(), new string[] { remains.ToString() });
            }
        }
    }

    /// <summary>
    /// Finds the next biggest oxygen source on character
    /// </summary>
    /// <returns></returns>
    private bool FindNextOxygenSource()
    {
        remainingSupply.Remove(currentlyUsed);
        List<int> keys = new List<int>(remainingSupply.Keys);
        if(keys.Count > 0)
        {
            currentlyUsed = keys[0];
            foreach(int key in keys)
            {
                if(remainingSupply[key].GetOxygenLevel() > remainingSupply[currentlyUsed].GetOxygenLevel())
                {
                    currentlyUsed = key;
                }
            }
        }
        else
        {
            currentlyUsed = -1;
            return false;
        }
       
        return true;
    }

    /// <summary>
    /// returns number of non empty oxygen components on character (including currently used)
    /// </summary>
    /// <returns></returns>
    public int getCurrentSupplyCount()
    {
        return remainingSupply.Count;
    }

    //Return Oxygen container from remainingSupply
    public OxygenComponent GetCurrentlyUsedOxygenComponent()
    {
        return remainingSupply[currentlyUsed];
    }

    /// <summary>
    /// returns oxygen level
    /// </summary>
    /// <returns></returns>
    public float GetCurrentOxygenLevel()
    {
        if(currentlyUsed != -1)
        {
            return remainingSupply[currentlyUsed].GetOxygenLevel();
        }
        return 0;
        
    }

    /// <summary>
    /// returns beginning oxygen Level
    /// </summary>
    /// <returns></returns>
    public float GetMaxOxygenLevel()
    {
        if (currentlyUsed != -1)
        {
            return remainingSupply[currentlyUsed].startingOxLevel;
        }
        return 100;
    }

}
