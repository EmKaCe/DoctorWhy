using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{



    public class ConnectionPointSave : ScriptableObject
    {
        public Rect rect;
        public DialogConnectionPointType type;
        public GUIStyle style;
        public int index;
        public int count;

        public void Init(DialogConnectionPoint point)
        {
            rect = new Rect(point.rect.x, point.rect.y, point.rect.width, point.rect.height);
            type = point.type;
            style = point.style;
            index = point.index;
            count = point.count;

        }
    }
}