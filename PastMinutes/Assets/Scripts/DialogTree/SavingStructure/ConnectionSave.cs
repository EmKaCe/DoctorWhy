using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{



    public class ConnectionSave : ScriptableObject
    {
        public DialogNode startNode;
        public DialogNode endNode;

        public int inIndex;
        public int outIndex;

        public void Init(DialogConnection connection)
        {
            startNode = connection.inPoint.node;
            endNode = connection.outPoint.node;
            inIndex = connection.inPoint.index;
            outIndex = connection.outPoint.index;
        }


    }

}
