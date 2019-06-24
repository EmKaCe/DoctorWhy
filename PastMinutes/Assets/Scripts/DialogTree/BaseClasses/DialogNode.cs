using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DialogTree
{



    public abstract class DialogNode : ScriptableObject
    {
        [HideInInspector]
        public Rect rect;
        [HideInInspector]
        public Rect rectStandardTitle;
        [HideInInspector]
        public Rect rectTitle;
        [HideInInspector]
        public Rect rectContent;
        [HideInInspector]
        public string title;
        [HideInInspector]
        public bool isDragged;
        [HideInInspector]
        public bool isSelected;

        [HideInInspector]
        public GUIStyle style;
        [HideInInspector]
        public GUIStyle defaultNodeStyle;
        [HideInInspector]
        public GUIStyle selectedNodeStyle;

        [HideInInspector]
        public DialogConnectionPoint[] inPoint;
        [HideInInspector]
        public DialogConnectionPoint[] outPoint;

        public Action<DialogNode> OnRemoveNode;

        public Action<DialogConnectionPoint> OnClickInPoint;
        public Action<DialogConnectionPoint> OnClickOutPoint;

        public GUIStyle inPointStyle;
        public GUIStyle outPointStyle;

        public List<DialogNode> parents;

        [HideInInspector]
        public readonly float rowHeight = 20f;
        [HideInInspector]
        public readonly float offset = 10;
        [HideInInspector]
        public readonly float titleOffset = 10;

        public int inPoints;
        public int outPoints;
        /// <summary>
        /// StandardNodeName
        /// </summary>
        [HideInInspector]
        public string nodeName;




        public void CreateDialogNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
        {
            this.nodeName = nodeName;
            parents = new List<DialogNode>();
            rect = new Rect(position.x, position.y, width, height);
            rectStandardTitle = new Rect(position.x + offset, position.y + titleOffset, (width - 2 * offset) / 2, rowHeight);
            rectTitle = new Rect(position.x + offset + rectStandardTitle.width, position.y + titleOffset, (width - 2 * offset) / 2, rowHeight);
            style = nodeStyle;
            this.inPoints = inPoints;
            this.outPoints = outPoints;
            inPoint = new DialogConnectionPoint[inPoints];
            outPoint = new DialogConnectionPoint[outPoints];
            for (int i = 0; i < inPoints; i++)
            {
                inPoint[i] = new DialogConnectionPoint(this, DialogConnectionPointType.In, inPointStyle, OnClickInPoint, i, inPoints);
            }
            for (int i = 0; i < outPoints; i++)
            {
                outPoint[i] = new DialogConnectionPoint(this, DialogConnectionPointType.Out, outPointStyle, OnClickOutPoint, i, outPoints);
            }
            defaultNodeStyle = nodeStyle;
            selectedNodeStyle = selectedStyle;
            OnRemoveNode = OnClickRemoveNode;
            this.OnClickInPoint = OnClickInPoint;
            this.OnClickOutPoint = OnClickOutPoint;
            this.inPointStyle = inPointStyle;
            this.outPointStyle = outPointStyle;
        }



        public virtual void Drag(Vector2 delta)
        {
            rect.position += delta;
            rectStandardTitle.position += delta;
            rectTitle.position += delta;
            rectContent.position += delta;
        }

        public virtual void Draw()
        {
            GUI.Box(rect, "", style);

            GUI.Label(rectStandardTitle, nodeName);
            title = GUI.TextField(rectTitle, title);
            foreach (DialogConnectionPoint i in inPoint)
            {
                i.Draw();
            }
            foreach (DialogConnectionPoint o in outPoint)
            {
                o.Draw();
            }
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                        }
                    }

                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }

        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        private void OnClickRemoveNode()
        {
            OnRemoveNode?.Invoke(this);
        }


        public abstract List<UIDialogItem> GetDialog();

    }

}
