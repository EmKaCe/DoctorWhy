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
}
