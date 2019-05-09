using UnityEngine;
using UnityEditor;

public class EditorTestWindow : EditorWindow
{
    string textString = "Hello World";
    HealthComponent health;
    bool value = true;

    [MenuItem("Window/EditorTest")]
    public static void ShowWindow()
    {
        var window = GetWindow<EditorTestWindow>("EditorTest");
        window.Show();
    }

    private void OnGUI()
    {
        //EditorGUILayout when editing fields and properties
        //GUILayout for labels, spaces between properties and buttons
        //If function in both try EditorGUILayout first
    
        GUILayout.Label("This is a label.", EditorStyles.boldLabel);
        textString = EditorGUILayout.TextField("Name", textString);


        if(GUILayout.Button("Press Me"))
        {
            health = null;
            foreach(GameObject obj in Selection.gameObjects)
            {
                health = obj.GetComponent<HealthComponent>();
                if(health != null)
                {
                    Debug.Log(obj.name);
                    break;
                }
            }
            if(health == null)
            {

            }
            else
            {
                value = EditorGUILayout.InspectorTitlebar(value, health);
                if (value)
                {
                    Debug.Log("Test");
                    textString = EditorGUILayout.TextField("Health", health.health.ToString());
                    health.health = EditorGUILayout.FloatField("Health" ,health.health);
                    this.Repaint();
                }
            }
        }
    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }



}
