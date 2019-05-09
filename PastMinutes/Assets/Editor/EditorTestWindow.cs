using UnityEngine;
using UnityEditor;

public class EditorTestWindow : EditorWindow
{
    string textString = "Hello World";
    HealthComponent healthComp;
    bool fold = true;


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
        healthComp = null;
            foreach (GameObject obj in Selection.gameObjects)
            {
                healthComp = obj.GetComponent<HealthComponent>();
                if (healthComp != null)
                {
                    fold = EditorGUILayout.InspectorTitlebar(fold, healthComp);
                    break;
                }
            }
        if (healthComp == null)
        {

        }
        else
        {
            if (fold)
            {
                EditorGUILayout.RectField(new Rect(0, 0, 100, 50));
                healthComp.health = EditorGUILayout.FloatField("Health", healthComp.health);
                EditorGUILayout.Space();
            }
        }
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

