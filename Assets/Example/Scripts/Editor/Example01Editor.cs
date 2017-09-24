using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Example01))]
public class Example01Editor : Editor
{
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Test Request"))
        {
            EditorWWW.RequestSync("http://example.org",(success, response)=>
            {
                if(success)
                {
                    Debug.Log("Response: " + response);
                }
                else
                {
                    Debug.LogError("Error: " + response);
                }
            });
        }
    }
}

