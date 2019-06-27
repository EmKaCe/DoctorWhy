using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FactionComponent : MonoBehaviour
{
    [SerializeField]
    protected Faction faction;
    ///// <summary>
    ///// not used right now
    ///// </summary>
    //Dictionary<Faction, float> standings;

    public Faction GetFaction()
    {
        return faction;
    }

    //private void Start()
    //{
    //    standings.Add(faction, 100);
    //}

}
