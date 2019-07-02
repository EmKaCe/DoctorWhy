using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
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


    public static StartBehaviourNode currentSave;

    [HideInInspector]
    public string savefileName;

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

        //if ((nodeSaver = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset", typeof(Object)) as NodeSaver) == null)
        //{
        //    nodeSaver = ScriptableObject.CreateInstance<NodeSaver>();
        //    AssetDatabase.CreateAsset(nodeSaver, AssetDatabase.GUIDToAssetPath(folderPath) + "/Nodes.asset");
        //}


    }

    private void OnEnable()
    {
        GUISkin nodeSkin = AssetDatabase.LoadAssetAtPath("Assets/Materials/EditorGUIStyles/StandardEditorStyle.guiskin", typeof(GUISkin)) as GUISkin;
        nodeStyle = nodeSkin.GetStyle("StandardNode");
        //nodeStyle.normal.background = Texture2D.normalTexture;
        //nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        //nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = nodeSkin.GetStyle("StandardNode");
        //selectedNodeStyle.normal.background = Texture2D.whiteTexture;
        // selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        //selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = nodeSkin.GetStyle("StandardInPoint");
        //inPointStyle.normal.background = Texture2D.whiteTexture;
        //inPointStyle.active.background = Texture2D.whiteTexture;
        // inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        //inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
       // inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = nodeSkin.GetStyle("StandardOutPoint");
        //outPointStyle.normal.background = Texture2D.whiteTexture;
        //outPointStyle.active.background = Texture2D.whiteTexture;
        // outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        // outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        //outPointStyle.border = new RectOffset(4, 4, 12, 12);
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

    private void LoadNodeTree()
    {
        //Name empty
        if (savefileName.Equals(""))
        {
            Debug.Log("Please enter the name of a DialogTree");
            return;
        }
        //Save with that name not existing
        if ((currentSave = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset", typeof(Object)) as StartBehaviourNode) == null)
        {
            //NodeTree doesn't exist
            //Diplay Message
            Debug.Log("BehaviourTree " + savefileName + ".asset at " + AssetDatabase.GUIDToAssetPath(folderPath) + " doesn't exist.");
            return;
        }
        //else
        nodes = new List<BehaviourNode>();
        connections = new List<BehaviourConnection>();
        Object[] n = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset");
        foreach(BehaviourNode s in n)
        {
            nodes.Add(s as BehaviourNode);
        }
        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i].inPoint = new BehaviourConnectionPoint[nodes[i].inPoints];
            nodes[i].outPoint = new BehaviourConnectionPoint[nodes[i].outPoints];
            InitConnectionPoints(nodes[i]);            
        }
        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i].OnRemoveNode = OnClickRemoveNode;
            InitConnections(nodes[i]);
        }
    }

    public void InitConnectionPoints(BehaviourNode node)
    {
        for(int i = 0; i < node.inPoints; i++)
        {
            node.inPoint[i] = new BehaviourConnectionPoint(node, BehaviourConnectionPointType.In, inPointStyle, OnClickInPoint, i, node.inPoints);
        }
        for(int i = 0; i < node.outPoints; i++)
        {
            node.outPoint[i] = new BehaviourConnectionPoint(node, BehaviourConnectionPointType.Out, outPointStyle, OnClickOutPoint, i, node.outPoints);
        }
    }

    public void InitConnections(BehaviourNode node)
    {
        //Only for out points, parent deals with inPoints
        for(int i = 0; i < node.outPoints; i++)
        {
            if(node.children[i] != null)
            {
                node.outPoint[i].connectedNode = node.children[i];
                node.outPoint[i].connectedNode.inPoint[0].connectedNode = node;
                //each node has only one parent
                connections.Add(new BehaviourConnection(node.children[i].inPoint[0], node.outPoint[i], OnClickRemoveConnection));
            }
        }
    }

    public void SaveNodeTree(bool saveAs)
    {
        if (saveAs)
        {
            return;
        }
        //User loaded a existing tree
        if (currentSave != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                AssetDatabase.RemoveObjectFromAsset(nodes[i]);
            }

        }

        //save doesnt exist
        if (currentSave == null || !AssetDatabase.Contains(currentSave))
        {
            
            currentSave = nodes.Find(n => n.GetType().Equals(typeof(StartBehaviourNode))) as StartBehaviourNode;
            if(currentSave == null)
            {
                Debug.Log("Behaviour needs a BehaviourStartNode");
                return;
            }
            currentSave.name = savefileName;
            AssetDatabase.CreateAsset(currentSave, AssetDatabase.GUIDToAssetPath(folderPath) + "/" + savefileName + ".asset");
        }

        foreach(BehaviourNode node in nodes)
        {
            if (node.Equals(currentSave))
            {
                continue;
            }
            AssetDatabase.AddObjectToAsset(node, currentSave);
        }
        AssetDatabase.SaveAssets();
    }

    public void ResetNodePosition()
    {
        if (nodes.Count >= 1)
        {
            Vector2 offset = nodes[0].rect.position;
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(-offset);
            }
        }
    }

    private void DrawLoader()
    {
        savefileName = GUI.TextArea(new Rect(168, 9, 350, 20), savefileName);
        //Load
        if (GUI.Button(new Rect(0, 0, 50, 30), "Load"))
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
        if (GUI.Button(new Rect(4, 34, 55, 30), "ResetPosition"))
        {
            ResetNodePosition();
        }


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
                if(nodes[i] != null)
                {
                    nodes[i].Draw();
                }
                
            }
        }
    }

    private void DrawConnections()
    {
        if(connections != null)
        {
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
        genericMenu.AddItem(new GUIContent("Add BehaviourStartNode"), false, () => OnClickAddStartBehaviourNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add BehaviourNode"), false, () => OnClickAddBehaviourNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Selector"), false, () => OnClickAddSelector(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Sequence"), false, () => OnClickAddSequence(mousePosition));
        genericMenu.AddItem(new GUIContent("Add ConditionalSequence"), false, () => OnClickAddConditionalSequence(mousePosition));
        genericMenu.AddItem(new GUIContent("Iterator"), false, () => OnClickAddIterator(mousePosition));
        genericMenu.AddItem(new GUIContent("Inverter"), false, () => OnClickAddInverter(mousePosition));
        genericMenu.AddItem(new GUIContent("Add EnemyNear"), false, () => OnClickAddEnemyNear(mousePosition));
        genericMenu.AddItem(new GUIContent("Add GoToNode"), false, () => OnClickAddGoToNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add AttackNode"), false, () => OnClickAddAttackNode(mousePosition));
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
        BaseBehaviourNode b = ScriptableObject.CreateInstance<BaseBehaviourNode>();     
        b.CreateBaseBehaviour(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 1, "BaseBehaviour");
        nodes.Add(b);

        AssetDatabase.SaveAssets();
    }

    private void OnClickAddSelector(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        Selector s = CreateInstance<Selector>();
        s.CreateSelectorNode(mousePosition, 250, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 2, "Selector");
        nodes.Add(s);
    }

    private void OnClickAddSequence(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        Sequence s = CreateInstance<Sequence>();
        s.CreateSequenceNode(mousePosition, 250, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 2, "Sequence");
        nodes.Add(s);
    }

    private void OnClickAddConditionalSequence(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        ConditionalSequence s = CreateInstance<ConditionalSequence>();
        s.CreateConditionalSequenceNode(mousePosition, 250, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 2, "ConditionalSequence");
        nodes.Add(s);
    }

    private void OnClickAddStartBehaviourNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        StartBehaviourNode s = CreateInstance<StartBehaviourNode>();
        s.CreateStartBehaviour(mousePosition, 250, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 0, 1, "BehaviourStart");
        nodes.Add(s);
    }

    private void OnClickAddIterator(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        Iterator t = CreateInstance<Iterator>();
        t.CreateIteratorNode(mousePosition, 200, 200, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 1, "Iterator");
        nodes.Add(t);
    }

    private void OnClickAddEnemyNear(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        EnemyNear t = CreateInstance<EnemyNear>();
        t.CreateEnemyNearNode(mousePosition, 225, 175, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 0, "EnemyNear");
        nodes.Add(t);
    }

    private void OnClickAddGoToNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        GoToBehaviourNode t = CreateInstance<GoToBehaviourNode>();
        t.CreateGoToBehaviourNode(mousePosition, 225, 200, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 0, "GoTo");
        nodes.Add(t);
    }

    private void OnClickAddAttackNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        Attack t = CreateInstance<Attack>();
        t.CreateAttackNode(mousePosition, 150, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 0, "Attack");
        nodes.Add(t);
    }

    private void OnClickAddInverter(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BehaviourNode>();
        }
        Inverter t = CreateInstance<Inverter>();
        t.CreateInverterNode(mousePosition, 150, 175, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, 1, 1, "Inverter");
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
                foreach(BehaviourConnectionPoint i in node.inPoint)
                {
                    if(connect.inPoint == i)
                    {
                        connectionsToRemove.Add(connect);
                    }
                }
                foreach(BehaviourConnectionPoint o in node.outPoint)
                {
                    if (connect.outPoint == o)
                    {
                        connect.outPoint.connectedNode = null;
                        connectionsToRemove.Add(connect);
                    }
                }
            }
            foreach(BehaviourConnection connect in connectionsToRemove)
            {
                // connections.Remove(connect);
                OnClickRemoveConnection(connect);
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
        connection.outPoint.node.children[connection.outPoint.index] = null;
        connection.inPoint.connectedNode = null;
        connection.inPoint.node.parent = null;
        connection.outPoint.connectedNode = null;
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        if(connections == null)
        {
            connections = new List<BehaviourConnection>();
        }
        selectedInPoint.node.parent = selectedOutPoint.node;
        selectedInPoint.connectedNode = selectedOutPoint.node;
        selectedOutPoint.connectedNode = selectedInPoint.node;
        selectedOutPoint.node.children[selectedOutPoint.index] = selectedInPoint.node;
        BehaviourConnection beCon = new BehaviourConnection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection);
        connections.Add(beCon);
    }

    private void ClearConnectionSelection()
    {
        selectedOutPoint = null;
        selectedInPoint = null;
    }
#endif
}
