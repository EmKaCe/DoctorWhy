using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviourTreeEditor : EditorWindow
{

    private List<BehaviourNode> nodes;
    private List<BehaviourConnection> connections;

    private GUIStyle nodeStyle;
    private GUIStyle selectedNodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private BehaviourConnectionPoint selectedInPoint;
    private BehaviourConnectionPoint selectedOutPoint;

    private Vector2 offset;
    private Vector2 drag;

    private static string folderPath;
    //private static string tempPath;
    private static BehaviourEditorSettings settings;

    public static NodeSaver nodeSaver;


    [MenuItem("Window/BehaviourTree")]
    private static void OpenWindow()
    {
        BehaviourTreeEditor window = GetWindow<BehaviourTreeEditor>();
        window.titleContent = new GUIContent("BehaviourTree Editor");
        //AssetDatabase.CreateAsset(b, "Assets/Scripts/test.asset");

        #region CreateFolder
        if (!AssetDatabase.IsValidFolder("Assets/Behaviours/BehaviourTrees"))
        {
            AssetDatabase.CreateFolder("Assets", "Behaviours");
            folderPath = AssetDatabase.CreateFolder("Assets/Behaviours", "BehaviourTrees");
            
        }
        else
        {
            folderPath = AssetDatabase.AssetPathToGUID("Assets/Behaviours/BehaviourTrees");
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

        if ((settings = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/Settings.asset", typeof(BehaviourEditorSettings)) as BehaviourEditorSettings)  == null){
            settings = ScriptableObject.CreateInstance<BehaviourEditorSettings>();
            AssetDatabase.CreateAsset(settings, AssetDatabase.GUIDToAssetPath(folderPath) + "/Settings.asset");
        }

        #endregion

        if ((nodeSaver = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset", typeof(Object)) as NodeSaver) == null)
        {
            nodeSaver = ScriptableObject.CreateInstance<NodeSaver>();
            AssetDatabase.CreateAsset(nodeSaver, AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset");
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

    private void DrawLoader()
    {
        //Load
        if(GUI.Button(new Rect(0, 0, 50, 30), "Load"))
        {
            nodes = new List<BehaviourNode>();
            connections = new List<BehaviourConnection>();

            foreach(StandardNodeSave save in nodeSaver.nodes)
            {
                save.node.inPoint = new BehaviourConnectionPoint(save.node, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint);
                save.node.outPoint = new BehaviourConnectionPoint(save.node, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint);
                save.node.OnRemoveNode = OnClickRemoveNode;
                nodes.Add(save.node);
            }
            foreach(ConnectionSave connection in nodeSaver.connections)
            {
                connections.Add(new BehaviourConnection(connection.startNode.inPoint, connection.endNode.outPoint, OnClickRemoveConnection));
            }
            
            
        
        }
        //Save
        
        if(GUI.Button(new Rect(50, 0, 50, 30), "Save"))
        {
            nodeSaver.nodes = new List<StandardNodeSave>();
            for (int i = 0; i < nodes.Count; i++)
            {
                SaveBehaviourNode(nodes[i]);
            }
            nodeSaver.connections = new List<ConnectionSave>();
            for (int i = 0; i < connections.Count; i++)
            {
                SaveBehaviourConnections(connections[i]);
            }
            AssetDatabase.SaveAssets();
        }
    }

    private void SaveBehaviourNode(BehaviourNode node)
    {       
        StandardNodeSave saveNode = CreateInstance<StandardNodeSave>();
        ConnectionPointSave saveIn = CreateInstance<ConnectionPointSave>();
        ConnectionPointSave saveOut = CreateInstance<ConnectionPointSave>();
        saveNode.node = node;
        saveNode.inPoint = saveIn;
        saveNode.outPoint = saveOut;
        nodeSaver.nodes.Add(saveNode);
        AssetDatabase.AddObjectToAsset(saveNode, nodeSaver);
        AssetDatabase.AddObjectToAsset(saveIn, saveNode);
        AssetDatabase.AddObjectToAsset(saveOut, saveNode);
    }

    private void SaveBehaviourConnections(BehaviourConnection connection)
    {
        Debug.Log("Connection: " + connection.inPoint.ToString());
        Debug.Log("Connection: " + connection.outPoint.node.ToString());
        ConnectionSave cSave = CreateInstance<ConnectionSave>();
        cSave.Init(connection);
        nodeSaver.connections.Add(cSave);
        AssetDatabase.AddObjectToAsset(cSave, nodeSaver);
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
        if(nodes != null)
        {
            for(int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if(connections != null)
        {
            //foreach(BehaviourConnection conect in connections)
            //{
            //    conect.Draw();
            //}
            for(int i  = 0; i < connections.Count; i++)
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
                if(e.button == 0)
                {
                    ClearConnectionSelection();
                }

                if(e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if(nodes != null)
        {
            for(int i = nodes.Count - 1; i >= 0; i--)
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
        if(selectedInPoint != null && selectedOutPoint == null)
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
        genericMenu.AddItem(new GUIContent("Add BehaviourNode"), false, () => OnClickAddBehaviourNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Selector"), false, () => OnClickAddSelector(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Sequence"), false, () => OnClickAddSequence(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Decorator"), false, () => OnClickAddDecorator(mousePosition));
        genericMenu.AddItem(new GUIContent("Add TestBehaviour"), false, () => OnClickAddTestBehaviour(mousePosition));
        genericMenu.ShowAsContext();
        
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;
        if(nodes != null)
        {
            for(int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }
        GUI.changed = true;
    }

    private void OnClickAddBehaviourNode(Vector2 mousePosition)
    {
        if(nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        //BaseBehaviour bh = behaviours.AddComponent<BaseBehaviour>();
        BaseBehaviourNode b = ScriptableObject.CreateInstance<BaseBehaviourNode>();     
        //AssetDatabase.CreateAsset(b, "Assets/Scripts/test.asset");
        b.CreateBaseBehaviour(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        // b.behaviour = bh;

        AssetDatabase.AddObjectToAsset(b, nodeSaver);
        //AssetDatabase.AddObjectToAsset(ns, nodeSaver);
        nodes.Add(b);

        AssetDatabase.SaveAssets();
    }

    private void OnClickAddSelector(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
    }

    private void OnClickAddSequence(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
    }

    private void OnClickAddDecorator(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
    }

    private void OnClickAddTestBehaviour(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        TestBehaviourNode t = ScriptableObject.CreateInstance<TestBehaviourNode>();
        GameObject g = new GameObject();
        t.CreateTestBehaviourNode(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, g.AddComponent(typeof(TestBehaviour)) as TestBehaviour);     
        nodes.Add(t);
    }

    private void OnClickInPoint(BehaviourConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if(selectedOutPoint != null)
        {
            if(selectedOutPoint.node != selectedInPoint.node)
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

    private void OnClickOutPoint(BehaviourConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if(selectedInPoint != null)
        {
            if(selectedOutPoint.node != selectedInPoint.node)
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

    private void OnClickRemoveNode(BehaviourNode node)
    {
        if(connections != null)
        {
            List<BehaviourConnection> connectionsToRemove = new List<BehaviourConnection>();

            foreach(BehaviourConnection connect in connections)
            {
                if(connect.inPoint == node.inPoint || connect.outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connect);
                }
            }
            foreach(BehaviourConnection connect in connectionsToRemove)
            {
                connections.Remove(connect);
            }
            connectionsToRemove = null;

        }
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        DestroyImmediate(node, true);
        AssetDatabase.SaveAssets();
    }

    private void OnClickRemoveConnection(BehaviourConnection connection)
    {
        connection.inPoint.node.parent = null;
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        if(connections == null)
        {
            connections = new List<BehaviourConnection>();
        }
        selectedInPoint.node.parent = selectedOutPoint.node;
        BehaviourConnection beCon = new BehaviourConnection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection);
        connections.Add(beCon);
    }

    private void ClearConnectionSelection()
    {
        selectedOutPoint = null;
        selectedInPoint = null;
    }
}
