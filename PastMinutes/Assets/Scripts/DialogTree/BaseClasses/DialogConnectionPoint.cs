using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{



    public enum DialogConnectionPointType { In, Out }

    public class DialogConnectionPoint
    {
        [HideInInspector]
        public Rect rect;

        [HideInInspector]
        public DialogConnectionPointType type;

        [HideInInspector]
        public DialogNode node;

        [HideInInspector]
        public DialogNode connectedNode;
#if UNITY_EDITOR
        [HideInInspector]
        public GUIStyle style;
#endif
        [HideInInspector]
        public Action<DialogConnectionPoint> OnClickConnectionPoint;

        [HideInInspector]
        public int index;
        [HideInInspector]
        public int count;
        /// <summary>
        /// 
        /// </summary>
        private int contentCount;
#if UNITY_EDITOR
        public DialogConnectionPoint(DialogNode node, DialogConnectionPointType type, GUIStyle style, Action<DialogConnectionPoint> OnClickConnectionPoint, int index, int count)
        {
            this.node = node;
            this.type = type;
            this.style = style;
            this.OnClickConnectionPoint = OnClickConnectionPoint;
            rect = new Rect(0, 0, 10f, 20f);
            this.index = index;
            this.count = count;
        }


        public void Draw()
        {
            //First ConnectionInPoint. Used to connect the node to other nodes
            //other spacing needs rework! is hardcoded right now. Idea would be to change to a rect per text area to get a better positioning of the dialogs
            if(DialogConnectionPointType.In == type)
            {
                if(index == 0)
                {
                    rect.y = node.rect.y + node.rectTitle.height / 2;
                }
                else
                {
                    //rect.y = node.rectContent.y + node.rectContent.height / (count * 2 - 1) * index;
                    rect.y = node.rectContent.y + 30 * (index - 1) + ((30 - rect.height) / 2) + node.rowHeight + index * 2f + 1f;
                }
                
            }
            else if(DialogConnectionPointType.Out == type)
            {
                //rect.y = node.rectContent.y + node.rectContent.height / (count * 2 + 1) * (index * 2 + 1);
                rect.y = node.rectContent.y + 30 * index + ((30 - rect.height) / 2) + node.rowHeight + index * 2f + 1f;
            }

            switch (type)
            {
                case DialogConnectionPointType.In:
                    rect.x = node.rect.x - rect.width + 8f;
                    break;

                case DialogConnectionPointType.Out:
                    rect.x = node.rect.x + node.rect.width - 8f;
                    break;
            }

            if (GUI.Button(rect, "", style))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }

#endif
    }
}
