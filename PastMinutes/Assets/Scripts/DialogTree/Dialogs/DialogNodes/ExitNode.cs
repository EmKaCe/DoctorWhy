using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogTree
{

    public class ExitNode : DialogNode
    {
        public void CreateExitDialog(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, string nodeName)
        {
            base.CreateDialogNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 0, nodeName);
            rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
        }

        public override void Draw()
        {
            base.Draw();
            //GUI.Label(rectStandardTitle, "StartNode");

            GUILayout.BeginArea(rectContent);
            GUILayout.EndArea();

        }

        public override List<UIDialogItem> GetDialog(NodeSaver save, DialogNode node)
        {
            return new List<UIDialogItem>();
        }

        public override void Drag(Vector2 delta)
        {
            base.Drag(delta);
        }
    }

}
