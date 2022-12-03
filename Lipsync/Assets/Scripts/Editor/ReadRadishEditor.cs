using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;

[CustomEditor(typeof(ReadRadish))]
public class ReadRadishEditor : Editor
{
    //This custom editor is just to make it neat and simple in the inspector
    //Hiding all the debug private fields under a foldout menu

    public bool debug;

    public override void OnInspectorGUI()
    {
        EditorGUI.indentLevel++;
        debug = EditorGUILayout.Foldout(debug, "Debug");
        if (debug)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("phonemeDataList"));
        }

        //DrawDefaultInspector();
    }
}