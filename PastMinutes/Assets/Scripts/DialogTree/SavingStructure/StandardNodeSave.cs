using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{



    public class StandardNodeSave : ScriptableObject
    {

        public DialogNode node;
        public List<ConnectionPointSave> inPoint;
        public List<ConnectionPointSave> outPoint;


    }

}
