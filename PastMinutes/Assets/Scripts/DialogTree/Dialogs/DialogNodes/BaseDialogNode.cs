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


        [HideInInspector]
        public Vector2 pos;

        private Vector2 scrollPos;

        public void CreateBaseDialog(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogConnectionPoint> OnClickInPoint, Action<DialogConnectionPoint> OnClickOutPoint, Action<DialogNode> OnClickRemoveNode, int inPoints, int outPoints, string nodeName)
        {
            base.CreateDialogNode(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPoints, outPoints, nodeName);
            pos = position;
            rectContent = new Rect(position.x + offset, position.y + rowHeight, width - (2 * offset), height - rowHeight);
            testArray = new string[2];
            dialogs = new List<string>
            {
                ""
            };
        }


        public override void Draw()
        {
            
            base.Draw();





            GUILayout.BeginArea(rectContent);
            //EditorGUILayout.BeginVertical();
            GUILayout.Space(rowHeight);
            EditorGUIUtility.labelWidth = 75;
            //EditorGUILayout.PropertyField(serializedBehaviour, new GUIContent("Behaviour"));
            //test = EditorGUILayout.FloatField("TestValue", test);
            GUILayoutOption s = GUILayout.Height(30);
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
            
        }

        public override List<UIDialogItem> GetDialog()
        {
            List<UIDialogItem> result = new List<UIDialogItem>();
            for(int i = 0; i < dialogs.Count; i++)
            {
                //start with i + 1 because index 0 is input of whole node
                DialogNode pre = inPoint[i + 1].connectedNode;
                //No prerequisite 
                if (pre == null)
                {
                    result.Add(new UIDialogItem(dialogs[i], i));
                }
                else if (pre.GetType().Equals(typeof(PrerequisiteNode)))
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
