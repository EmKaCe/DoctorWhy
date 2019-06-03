using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSaver : ScriptableObject
{
    public List<StandardNodeSave> nodes;
    public List<ConnectionSave> connections;
    public string path;
}
