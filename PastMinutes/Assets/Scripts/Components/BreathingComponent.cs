using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingComponent : MonoBehaviour
{
    public float oxygenUsage;
    private Dictionary<int, OxygenComponent> oxygenSupply;
    private Dictionary<int, OxygenComponent> remainingSupply;
    private int currentlyUsed;

    // Start is called before the first frame update
    void Start()
    {
        oxygenSupply = new Dictionary<int, OxygenComponent>();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOxygenSupply(int supplyID)
    {
        OxygenComponent comp = EntityManager.GetEntityComponent(supplyID, "OxgenComponent") as OxygenComponent;
        oxygenSupply.Add(supplyID, comp);
        if(comp.GetOxygenLevel() > 0)
        {
            remainingSupply.Add(supplyID, comp);
            if(comp.GetOxygenLevel() > oxygenSupply[currentlyUsed].GetOxygenLevel())
            {
                currentlyUsed = supplyID;
            }
        }
    }

    public void RemoveOxygenSupply(int supplyID)
    {
        oxygenSupply.Remove(supplyID);
    }

    public void DecreaseOxygenLevel(float decrease)
    {
        float remains = oxygenSupply[currentlyUsed].RemoveOxygen(decrease);
        if (remains > 0)
        {
            if (FindNextOxygenSource())
            {
                DecreaseOxygenLevel(remains);
            }
            else
            {
                Debug.Log("BreathingComponent: No oxygen remaining");
                EventSystem.RanOutOfOxygen(gameObject.GetInstanceID(), remains.ToString());
            }
        }
    }

    private bool FindNextOxygenSource()
    {
        remainingSupply.Remove(currentlyUsed);
        List<int> keys = new List<int>(remainingSupply.Keys);
        if(keys.Count > 0)
        {
            currentlyUsed = keys[0];
        }
        else
        {
            currentlyUsed = -1;
            return false;
        }
       
        return true;
    }
}
