using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

[CustomEditor(typeof(CreateAnimationCurve))]
public class CreateAnimationCurveEditor : Editor
{
    public bool debug = false;
    public bool curve = false;

    public override void OnInspectorGUI()
    {
        CreateAnimationCurve createAnimCurve = (CreateAnimationCurve)target;

        //createAnimCurve.closeMouthAfter = EditorGUILayout.FloatField(new GUIContent("Close Mouth After (seconds)", 
        //                                                                            "Time after a phoneme is spoken to completely close the mouth"), 
        //                                                                            createAnimCurve.closeMouthAfter);

        string tooltipText1 = "The speed of mouth opening in seconds. Keep this low.";
        createAnimCurve.attackTime = EditorGUILayout.FloatField(new GUIContent("Attack Time", tooltipText1),
                                                                               createAnimCurve.attackTime);

        string tooltipText2 = "The duration where the mouth should stay open in seconds. " +
                              "If the duration between phoneme is bigger than this range, close the mouth";
        createAnimCurve.holdTime = EditorGUILayout.FloatField(new GUIContent("Hold Time", tooltipText2),
                                                                             createAnimCurve.holdTime);

        string tooltipText3 = "The speed of mouth closing in seconds. Keep this low.";
        createAnimCurve.releaseTime = EditorGUILayout.FloatField(new GUIContent("Release Time", tooltipText3),
                                                                               createAnimCurve.releaseTime);

        string tooltipText4 = "If there are two phonemes that are too close together," +
                              "combine and average their values";
        createAnimCurve.smoothingTime = EditorGUILayout.FloatField(new GUIContent("Smoothing Time", tooltipText4),
                                                                               createAnimCurve.smoothingTime);

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