using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogTree
{

    

    public class StartDialogNode : DialogNode
    {


        

        public void CreateStartDialog(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, string nodeName)
        {
            base.CreateDialogNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 0, 3, nodeName);
            rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
        }

        public override void Draw()
        {
            base.Draw();
            //GUI.Label(rectStandardTitle, "StartNode");

            GUILayout.BeginArea(rectContent);

            GUILayout.Space(rowHeight);
            EditorGUIUtility.labelWidth = 75;
            EditorGUILayout.LabelField("First interaction: ");
            GUILayout.Space(rowHeight);
            EditorGUILayout.LabelField("Other: ");
            GUILayout.Space(rowHeight);
            EditorGUILayout.LabelField("Always: ");



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
