using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CreateAnimationCurve))]
public class CreateAnimationCurveEditor : Editor
{
    public bool debug = false;
    public bool curve = false;

    public override void OnInspectorGUI()
    {
        CreateAnimationCurve createAnimCurve = (CreateAnimationCurve)target;

        //Debug foldout
        EditorGUI.indentLevel++;
        debug = EditorGUILayout.Foldout(debug, "Debug");
        if (debug)
        {
            EditorGUI.indentLevel++;

            //Curve foldout
            curve = EditorGUILayout.Foldout(curve, "Curve");
            if (curve)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveA"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveI"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveU"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveE"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveO"));
            }
        }

        //DrawDefaultInspector();
    }
}