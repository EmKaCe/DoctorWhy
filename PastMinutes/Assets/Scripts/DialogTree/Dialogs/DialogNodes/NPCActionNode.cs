using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace DialogTree
{

    public class NPCActionNode : DialogNode
    {

        public NPCAction action;


        public void CreateNPCActionDialog(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, string nodeName)
        {
            base.CreateDialogNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 1, nodeName);
            rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
        }

        public override void Draw()
        {
            base.Draw();


            GUILayout.BeginArea(rectContent);
            GUILayout.Space(rowHeight);
            action = EditorGUILayout.ObjectField("NPC-Action:", action, typeof(NPCAction), true) as NPCAction;

            GUILayout.EndArea();

        }

        public override void Drag(Vector2 delta)
        {
            base.Drag(delta);
        }


        public override List<UIDialogItem> GetDialog(NodeSaver save, DialogNode node)
        {
            return new List<UIDialogItem>();
        }
    }

}
