using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Faction", menuName = "Faction", order = 15)]
public class Faction : ScriptableObject
{
    public string factionName;
    public List<Faction> friendly;
    public List<Faction> neutral;
    public List<Faction> hostile;

    public bool CheckHostile(GameObject npc)
    {
        FactionComponent f;
        if((f = npc.GetComponent<FactionComponent>()) != null)
        {
            if (hostile.Contains(f.GetFaction()))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckHostile(Faction faction)
    {
        if (hostile.Contains(faction))
        {
            return true;
        }
        return false;
    }

    public bool CheckNeutral(GameObject npc)
    {
        FactionComponent f;
        if ((f = npc.GetComponent<FactionComponent>()) != null)
        {
            if (neutral.Contains(f.GetFaction()))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckNeutral(Faction faction)
    {
        if (neutral.Contains(faction))
        {
            return true;
        }
        return false;
    }

    public bool CheckFriendly(GameObject npc)
    {
        FactionComponent f;
        if ((f = npc.GetComponent<FactionComponent>()) != null)
        {
            if (friendly.Contains(f.GetFaction()))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckFriendly(Faction faction)
    {
        if (friendly.Contains(faction))
        {
            return true;
        }
        return false;
    }
}
