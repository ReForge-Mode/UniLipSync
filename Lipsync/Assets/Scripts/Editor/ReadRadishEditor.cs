using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ReadRadish))]
public class ReadRadishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        ReadRadish readRadish = (ReadRadish)target;
        if (GUILayout.Button("Load Radish Data", GUILayout.Height(50)))
        {
            readRadish.StartRead();
        }

        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("phonemeDataList"));
    }
}