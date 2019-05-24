using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourEditorNodes : ScriptableObject
{

    public List<BehaviourNode> nodes;



    public BehaviourEditorNodes()
    {
        nodes = new List<BehaviourNode>();
    }
}
