using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogTree
{



    public class NodeSaver : ScriptableObject
    {
        public List<StandardNodeSave> nodes;
        public List<ConnectionSave> connections;
        public string path;
    }

}
