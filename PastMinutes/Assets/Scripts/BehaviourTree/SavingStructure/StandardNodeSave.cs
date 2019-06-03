using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardNodeSave : ScriptableObject
{

    public BehaviourNode node;
    public List<ConnectionPointSave> inPoint;
    public List<ConnectionPointSave> outPoint;


}
