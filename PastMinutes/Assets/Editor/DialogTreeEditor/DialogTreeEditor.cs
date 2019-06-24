using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DialogTree
{



    public class DialogTreeEditor : EditorWindow
    {

        private List<DialogNode> nodes;
        private List<DialogConnection> connections;

        private GUIStyle nodeStyle;
        private GUIStyle selectedNodeStyle;
        private GUIStyle inPointStyle;
        private GUIStyle outPointStyle;
        private GUIStyle saveNameBoxStyle;

        private DialogConnectionPoint selectedInPoint;
        private DialogConnectionPoint selectedOutPoint;

        private Vector2 offset;
        private Vector2 drag;

        private static string folderPath;
        //private static string tempPath;
        private static DialogEditorSettings settings;

       // public static NodeSaver nodeSaver;

        public static NodeSaver currentSave;

        [HideInInspector]
        public string savefileName;
        private bool showModal;
        [HideInInspector]
        public Rect windowRect = new Rect(20, 20, 120, 50);


        [MenuItem("Window/DialogTree")]
        private static void OpenWindow()
        {
            DialogTreeEditor window = GetWindow<DialogTreeEditor>();
            window.titleContent = new GUIContent("DialogTree Editor");
            //AssetDatabase.CreateAsset(b, "Assets/Scripts/test.asset");

            #region CreateFolder
            if (!AssetDatabase.IsValidFolder("Assets/Dialogs/DialogTrees"))
            {
                AssetDatabase.CreateFolder("Assets", "Dialogs");
                folderPath = AssetDatabase.CreateFolder("Assets/Dialogs", "DialogTrees");

            }
            else
            {
                folderPath = AssetDatabase.AssetPathToGUID("Assets/Dialogs/DialogTrees");
            }
            //if (!AssetDatabase.IsValidFolder("Assets/Behaviours/BehaviourTrees/temp"))
            //{
            //    tempPath = AssetDatabase.CreateFolder("Assets/Behaviours/BehaviourTrees", "temp");
            //}
            //else
            //{
            //    tempPath = AssetDatabase.AssetPathToGUID("Assets/Behaviours/BehaviourTrees/temp");
            //}
            #endregion

            #region Creation of EditorSettings

            if ((settings = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/Settings.asset", typeof(DialogEditorSettings)) as DialogEditorSettings) == null)
            {
                settings = ScriptableObject.CreateInstance<DialogEditorSettings>();
                AssetDatabase.CreateAsset(settings, AssetDatabase.GUIDToAssetPath(folderPath) + "/Settings.asset");
            }

            #endregion

            //if ((nodeSaver = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset", typeof(Object)) as NodeSaver) == null)
            //{
            //    nodeSaver = ScriptableObject.CreateInstance<NodeSaver>();
            //    AssetDatabase.CreateAsset(nodeSaver, AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset");
            //}


        }

        private void SaveNodeTree(bool saveAs)
        {
            if (saveAs)
            {
                return;
            }
            //User loaded a existing tree
            if (currentSave != null)
            {
                for(int i = 0; i < nodes.Count; i++)
                {
                    AssetDatabase.RemoveObjectFromAsset(nodes[i]);
                }
                AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset");

            }
            currentSave = CreateInstance<NodeSaver>();
            currentSave.name = savefileName;
            AssetDatabase.CreateAsset(currentSave, AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset");

            //Create Save
            currentSave.nodes = new List<StandardNodeSave>();

            for (int i = 0; i < nodes.Count; i++)
            {
                StandardNodeSave s = SaveNode(nodes[i], currentSave);
                currentSave.nodes.Add(s);
            }
            currentSave.connections = new List<ConnectionSave>();
            if(connections != null)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    AssetDatabase.AddObjectToAsset(SaveConnections(connections[i], currentSave), currentSave);
                }
            }
            //delete old object to avoid unnecessary objects
            
            AssetDatabase.SaveAssets();



        }

        private ConnectionSave SaveConnections(DialogConnection connection, NodeSaver saver)
        {
            ConnectionSave cSave = CreateInstance<ConnectionSave>();
            cSave.Init(connection);
            saver.connections.Add(cSave);
            return cSave;
        }

        private StandardNodeSave SaveNode(DialogNode node, NodeSaver saver) 
        {
            AssetDatabase.AddObjectToAsset(node, saver);
            StandardNodeSave save = CreateInstance<StandardNodeSave>();
            save.name = node.name;
            save.node = node;
            save.inPoint = new List<ConnectionPointSave>();
            save.outPoint = new List<ConnectionPointSave>();
            AssetDatabase.AddObjectToAsset(save, saver);
            foreach (DialogConnectionPoint i in node.inPoint)
            {
                ConnectionPointSave saveIn = CreateInstance<ConnectionPointSave>();
                saveIn.Init(i);
                save.inPoint.Add(saveIn);
                AssetDatabase.AddObjectToAsset(saveIn, save);
            }
            foreach (DialogConnectionPoint o in node.outPoint)
            {
                ConnectionPointSave saveOut = CreateInstance<ConnectionPointSave>();
                saveOut.Init(o);
                save.outPoint.Add(saveOut);
                AssetDatabase.AddObjectToAsset(saveOut, save);
            }
            return save;
        }

        private void DrawLoader()
        {
            
            savefileName = GUI.TextArea(new Rect(168, 9, 350, 20), savefileName);
            //savefileName = GUI.TextField(new Rect(100, 0, 50, 30), savefileName);
            //Load
            if (GUI.Button(new Rect(4, 4, 50, 30), "Load"))
            {
                LoadNodeTree();
            }
            if (GUI.Button(new Rect(57, 4, 50, 30), "Save"))
            {
                SaveNodeTree(false);
            }
            if (GUI.Button(new Rect(110, 4, 55, 30), "SaveAs"))
            {
                SaveNodeTree(true);
            }

        }

        private void LoadNodeTree()
        {
            //Name empty
            if (savefileName.Equals(""))
            {
                Debug.Log("Please enter the name of a DialogTree");
                return;
            }
            //Save with that name not existing
            if ((currentSave = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset", typeof(Object)) as NodeSaver) == null)
            {
                //NodeTree doesn't exist
                //Diplay Message
                Debug.Log("DialogTree " + savefileName + ".asset at " + AssetDatabase.GUIDToAssetPath(folderPath) + " doesn't exist.");
                return;
            }
            //else
            nodes = new List<DialogNode>();
            connections = new List<DialogConnection>();

            foreach (StandardNodeSave save in currentSave.nodes)
            {

                save.node.inPoint = new DialogConnectionPoint[save.node.inPoints];
                save.node.outPoint = new DialogConnectionPoint[save.node.outPoints];
                for (int i = 0; i < save.node.inPoint.Length; i++)
                {
                    save.node.inPoint[i] = new DialogConnectionPoint(save.node, DialogConnectionPointType.In, inPointStyle, OnClickInPoint, i, save.node.inPoint.Length);
                }
                for (int o = 0; o < save.node.outPoint.Length; o++)
                {
                    save.node.outPoint[o] = new DialogConnectionPoint(save.node, DialogConnectionPointType.Out, outPointStyle, OnClickOutPoint, o, save.node.outPoint.Length); ;
                }
                save.node.OnRemoveNode = OnClickRemoveNode;
                nodes.Add(save.node);
            }
            foreach (ConnectionSave connection in currentSave.connections)
            {
                connection.startNode.inPoint[connection.inIndex].connectedNode = connection.endNode;
                connection.endNode.outPoint[connection.outIndex].connectedNode = connection.startNode;
                connections.Add(new DialogConnection(connection.startNode.inPoint[connection.inIndex], connection.endNode.outPoint[connection.outIndex], OnClickRemoveConnection));
            }
        }

       
        private void OnEnable()
        {

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);

            saveNameBoxStyle = new GUIStyle();

            //saveNameBoxStyle.alignment = TextAnchor.MiddleLeft;
        }

        private void OnGUI()
        {

            DrawLoader();

            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(Event.current);

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            if (GUI.changed) Repaint();
        }

       
        
            

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Draw();
                }
            }
        }

        private void DrawConnections()
        {
            if (connections != null)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    connections[i].Draw();
                }
            }
        }

        //hier alle anderen events rein
        private void ProcessEvents(Event e)
        {
            drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        ClearConnectionSelection();
                    }

                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if (nodes != null)
            {
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = nodes[i].ProcessEvents(e);

                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }
        }

        private void DrawConnectionLine(Event e)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    3f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );
                GUI.changed = true;
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add StartNode"), false, () => OnClickAddStartNode(mousePosition));
            genericMenu.AddItem(new GUIContent("Add DialogNode"), false, () => OnClickAddDialogNode(mousePosition));
            genericMenu.AddItem(new GUIContent("Add PrerequisiteNode"), false, () => OnClickAddPrerequisiteNode(mousePosition));
            genericMenu.AddItem(new GUIContent("Add ExitNode"), false, () => OnClickAddExitNode(mousePosition));
            //genericMenu.AddItem(new GUIContent("Add Decorator"), false, () => OnClickAddDecorator(mousePosition));
            //genericMenu.AddItem(new GUIContent("Add TestDialog"), false, () => OnClickAddTestDialog(mousePosition));
            genericMenu.ShowAsContext();

        }

        private void OnDrag(Vector2 delta)
        {
            drag = delta;
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Drag(delta);
                }
            }
            GUI.changed = true;
        }

        private void OnClickAddPrerequisiteNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<DialogNode>();
            }
            PrerequisiteNode b = CreateInstance<PrerequisiteNode>();
            b.CreatePrerequisiteDialog(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, "PrerequisiteNode");
            nodes.Add(b);
        }

        /// <summary>
        /// Adds BaseDialogNode
        /// </summary>
        /// <param name="mousePosition"></param>
        private void OnClickAddDialogNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<DialogNode>();
            }
            BaseDialogNode b = CreateInstance<BaseDialogNode>();
            b.CreateBaseDialog(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 2, 1, "DialogNode");
            nodes.Add(b);
        }

        private void OnClickAddStartNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<DialogNode>();
            }
            StartDialogNode b = CreateInstance<StartDialogNode>();
            b.CreateStartDialog(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, "StartNode");
            nodes.Add(b);
        }

        private void OnClickAddExitNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<DialogNode>();
            }
            ExitNode b = CreateInstance<ExitNode>();
            b.CreateExitDialog(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, "ExitNode");
            nodes.Add(b);
        }

        private void OnClickInPoint(DialogConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickOutPoint(DialogConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickRemoveNode(DialogNode node)
        {
            if (connections != null)
            {
                List<DialogConnection> connectionsToRemove = new List<DialogConnection>();

                foreach (DialogConnection connect in connections)
                {
                    foreach (DialogConnectionPoint i in node.inPoint)
                    {
                        if (connect.inPoint == i)
                        {
                            connectionsToRemove.Add(connect);
                        }
                    }
                    foreach (DialogConnectionPoint o in node.outPoint)
                    {
                        if (connect.outPoint == o)
                        {
                            connect.outPoint.connectedNode = null;
                            connectionsToRemove.Add(connect);
                        }
                    }
                }
                foreach (DialogConnection connect in connectionsToRemove)
                {
                    connections.Remove(connect);
                }
                connectionsToRemove = null;

            }
            nodes.Remove(node);
            /*AssetDatabase.RemoveObjectFromAsset(node);
            DestroyImmediate(node, true);*/
            //AssetDatabase.SaveAssets();
        }

        private void OnClickRemoveConnection(DialogConnection connection)
        {
            connection.inPoint.connectedNode = null;
            connection.inPoint.node.parents.Remove(connection.outPoint.node);
            connection.outPoint.connectedNode = null;
            connections.Remove(connection);
        }

        private void CreateConnection()
        {
            if (connections == null)
            {
                connections = new List<DialogConnection>();
            }
            selectedInPoint.node.parents.Add(selectedOutPoint.node);
            selectedInPoint.connectedNode = selectedOutPoint.node;
            selectedOutPoint.connectedNode = selectedInPoint.node;
            DialogConnection beCon = new DialogConnection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection);
            connections.Add(beCon);
        }

        private void ClearConnectionSelection()
        {
            selectedOutPoint = null;
            selectedInPoint = null;
        }
    }
}
 