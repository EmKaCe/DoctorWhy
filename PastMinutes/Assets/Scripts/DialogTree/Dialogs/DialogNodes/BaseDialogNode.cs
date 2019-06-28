using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BaseBehaviour;

namespace DialogTree
{


    //[CreateAssetMenu(menuName ="Test")]
    public class BaseDialogNode : DialogNode
    {

        public float test;
        

        public string[] testArray;

        public List<string> dialogs;

        public Rect rectAnswer;


        [HideInInspector]
        public Vector2 pos;

        public string answer;

        private Vector2 scrollPos;

        public void CreateBaseDialog(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
        {
            base.CreateDialogNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
            pos = position;
            rectContent = new Rect(position.x + offset, position.y + 2 * rowHeight, width - (2 * offset), height - rowHeight);
            rectAnswer = new Rect(position.x + offset, position.y + 2 * rowHeight - 5f, width - (2 * offset), rowHeight);
            testArray = new string[2];
            dialogs = new List<string>
            {
                ""
            };
        }


        public override void Draw()
        {
            
            base.Draw();

            GUILayout.BeginArea(rectAnswer);
            EditorGUIUtility.labelWidth = 75;
            GUILayoutOption s = GUILayout.Height(30);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Answer:", GUILayout.Width(50));
            answer = EditorGUILayout.TextArea(answer, s);
            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();

            GUILayout.BeginArea(rectContent);
            //EditorGUILayout.BeginVertical();
            GUILayout.Space(rowHeight);

            //EditorGUILayout.PropertyField(serializedBehaviour, new GUIContent("Behaviour"));
            //test = EditorGUILayout.FloatField("TestValue", test);
            
            //style.fixedWidth = 20;
            ////scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);
            ////EditorGUILayout.BeginVertical();
            for (int i = 0; i < dialogs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i + 1).ToString() + ":", GUILayout.Width(20));
                dialogs[i] = EditorGUILayout.TextArea(dialogs[i], s);
                EditorGUILayout.EndHorizontal();
            }
            //EditorGUILayout.EndVertical();
            //EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Add", GUILayout.Width(50))){
                AddDialog();
            }
            //EditorGUILayout.EndVertical();
            GUILayout.EndArea();


        }

        public void AddDialog()
        {
            dialogs.Add("");
            AddConnectionPoint(DialogConnectionPointType.In);
            AddConnectionPoint(DialogConnectionPointType.Out);
        }

        public void AddConnectionPoint(DialogConnectionPointType type)
        {
            DialogConnectionPoint[] newPoints;
            if (type == DialogConnectionPointType.In)
            {
                newPoints = new DialogConnectionPoint[inPoints + 1];
                //Copy array
                for(int i = 0; i < inPoints; i++)
                {
                    newPoints[i] = inPoint[i];
                    newPoints[i].count++;
                }
                newPoints[inPoints] = new DialogConnectionPoint(this, DialogConnectionPointType.In, inPointStyle, OnClickInPoint, inPoints, inPoints + 1);
                inPoint = newPoints;
                inPoints++;
            }
            else if(type == DialogConnectionPointType.Out)
            {
                newPoints = new DialogConnectionPoint[outPoints + 1];
                //Copy array
                for (int i = 0; i < outPoints; i++)
                {
                    newPoints[i] = outPoint[i];
                    newPoints[i].count++;
                }
                newPoints[outPoints] = new DialogConnectionPoint(this, DialogConnectionPointType.Out, outPointStyle, OnClickOutPoint, outPoints, outPoints + 1);
                outPoint = newPoints;
                outPoints++; 
            }
        }

        public override void Drag(Vector2 delta)
        {
            base.Drag(delta);
            rectAnswer.position += delta;
        }

        public override List<UIDialogItem> GetDialog(NodeSaver save, DialogNode node)
        {
            List<UIDialogItem> result = new List<UIDialogItem>();
            result.Add(new UIDialogItem(answer, -1));
            for(int i = 0; i < dialogs.Count; i++)
            {
                //start with i + 1 because index 0 is input of whole node
                // DialogNode pre = inPoint[i + 1].connectedNode;
                ConnectionSave test = save.connections.Find(c => c.startNode.Equals(node) && c.inIndex == (i + 1));
                //No prerequisite 
                if (test == null)
                {
                    //Output nodes start with 0 (no whole node output)
                    result.Add(new UIDialogItem(dialogs[i], i));
                    continue;
                }
                DialogNode pre = save.connections.Find(c => c.startNode.Equals(node) && c.inIndex == (i + 1)).endNode;
                if (pre.GetType().Equals(typeof(PrerequisiteNode)))
                {
                    if((pre as PrerequisiteNode).prerequisite == null)
                    {
                        continue;
                    }
                    if((pre as PrerequisiteNode).prerequisite.IsFullfilled())
                    {
                        result.Add(new UIDialogItem(dialogs[i], i));
                    }
                }
                //Well shit happens and someone attached a different Node to this InPoint
                else
                {
                    continue;
                }
            }
            return result;
        }
    }

}
