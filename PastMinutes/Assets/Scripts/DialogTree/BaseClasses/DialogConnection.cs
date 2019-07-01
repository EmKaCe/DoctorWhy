using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DialogTree
{



    public class DialogConnection
    {

        public DialogConnectionPoint inPoint;
        public DialogConnectionPoint outPoint;
        public Action<DialogConnection> OnClickRemoveConnection;

#if UNITY_EDITOR
        public DialogConnection(DialogConnectionPoint inPoint, DialogConnectionPoint outPoint, Action<DialogConnection> OnClickRemoveConnection)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }

        public void Draw()
        {
            Handles.DrawBezier(
                inPoint.rect.center,
                outPoint.rect.center,
                inPoint.rect.center + Vector2.left * 50f,
                outPoint.rect.center - Vector2.left * 50f,
                Color.white,
                null,
                2f
             );
            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                OnClickRemoveConnection?.Invoke(this);
            }

        }
#endif
    }

}
