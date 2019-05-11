using UnityEngine;
using UnityEditor;

public class EditorTestWindow : EditorWindow
{
    string textString = "Hello World";
    Node node;
    bool fold = true;

    private float x = 5;
    private float y = 100;

    Rect all = new Rect(-5, -5, 10000, 10000);


    [MenuItem("Window/EditorTest")]
    public static void ShowWindow()
    {
        GetWindow<EditorTestWindow>("EditorTest");
    }

    private void OnGUI()
    {
        //EditorGUILayout when editing fields and properties
        //GUILayout for labels, spaces between properties and buttons
        //If function in both try EditorGUILayout first

        GUILayout.Label("This is a label.", EditorStyles.boldLabel);
        textString = EditorGUILayout.TextField("Name", textString);
        //Texture2D myTexture = AssetPreview.GetAssetPreview();
        //GUILayout.Label(myTexture);


        //if (GUILayout.Button("Press Me"))
        //{
        node = null;
            foreach (GameObject obj in Selection.gameObjects)
            {
                node = obj.GetComponent<Node>();
                if (node != null)
                {
                    if (node.root || node.parentNode == null)
                    {
                        node.root = true;
                        fold = EditorGUILayout.InspectorTitlebar(fold, node);
                        break;
                    }                    
                }
            }
        if (node == null)
        {

        }
        else
        {
            if (fold)
            {
                //EditorGUILayout.RectField(new Rect(0, 0, 100, 50));
                node.name = EditorGUILayout.TextField("Name", node.name);
                EditorGUILayout.Space();
                CreateTreeItem(node);
            }
        }
    }


    private void CreateTreeItem(Node node)
    {
        GUILayout.BeginArea(new Rect(x, y, 500 , 50));
        BeginWindows();
        Debug.Log(node.GetType());
        if (typeof(NodeWithChildState).IsAssignableFrom(node.GetType()) && !typeof(ExtendedNode).IsAssignableFrom(node.GetType()))
        {
                NodeWithChildState test = node as NodeWithChildState;
                node.name = EditorGUILayout.TextField("Decorator", node.name);
                //Decorator

            
        }
        else if (typeof(ExtendedNode).IsAssignableFrom(node.GetType()))
        {
            //Sequence or Selector
            ExtendedNode test = node as ExtendedNode;
            node.name = EditorGUILayout.TextField("ExtendedNode", node.name);
        }
        else
        {
            //Behaviour
            node.name = EditorGUILayout.TextField("Behaviour", node.name);
        }

        EndWindows();
        GUILayout.EndArea();
    }


    //void OnInspectorUpdate()
    //{
    //    this.Repaint();
    //}

    //void OnGUI()
    //{
    //    if (Selection.activeGameObject)
    //    {
    //        selectedTransform = Selection.activeGameObject.transform;
    //        health = Selection.activeGameObject.GetComponent<HealthComponent>();

    //        fold = EditorGUILayout.InspectorTitlebar(fold, selectedTransform);
    //        if (fold)
    //        {
    //            selectedTransform.position =
    //                EditorGUILayout.Vector3Field("Position", selectedTransform.position);
    //            EditorGUILayout.Space();
    //            rotationComponents =
    //                EditorGUILayout.Vector4Field("Detailed Rotation",
    //                    QuaternionToVector4(selectedTransform.localRotation));
    //            EditorGUILayout.Space();
    //            selectedTransform.localScale =
    //                EditorGUILayout.Vector3Field("Scale", selectedTransform.localScale);
    //            health.health = EditorGUILayout.FloatField("Health", health.health);
    //        }

    //        selectedTransform.localRotation = ConvertToQuaternion(rotationComponents);
    //        EditorGUILayout.Space();
    //    }
    //}

    //Quaternion ConvertToQuaternion(Vector4 v4)
    //{
    //    return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    //}

    //Vector4 QuaternionToVector4(Quaternion q)
    //{
    //    return new Vector4(q.x, q.y, q.z, q.w);
    //}

    void OnInspectorUpdate()
    {
        this.Repaint();
    }
}

