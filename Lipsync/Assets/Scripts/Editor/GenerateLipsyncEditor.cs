using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(GenerateLipsync))]
public class GenerateLipsyncEditor : Editor
{
    //This script contains all fancy custom editor for GenerateLipsync component

    public bool debug = false;
    public string buttonText;

    public override void OnInspectorGUI()
    {
        //Get the reference to the original script
        GenerateLipsync genLipsync = (GenerateLipsync)target;

        //Create a textfield in the Custom Editor
        genLipsync.visemeGroup = EditorGUILayout.ObjectField("Viseme Group", genLipsync.visemeGroup, typeof(VisemeGroupInfo), false) as VisemeGroupInfo;
        genLipsync.savePath = EditorGUILayout.TextField("Save Path", genLipsync.savePath);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Auto Add to Animator", EditorStyles.boldLabel);
        var isAddToAnimator = serializedObject.FindProperty("isAddToAnimator");
        isAddToAnimator.boolValue = EditorGUILayout.Toggle("Auto Add to Animator?", isAddToAnimator.boolValue);
        serializedObject.ApplyModifiedProperties();
        genLipsync.animator = EditorGUILayout.ObjectField("Animator", genLipsync.animator, typeof(AnimatorController), false) as AnimatorController;
        EditorGUILayout.Space();

        //Create a button that can only be clicked if the savePath field is not empty
        if (genLipsync.savePath == "" || genLipsync.visemeGroup == null)
        {
            buttonText = "Make sure savePath and visemeGroup fields are not empty!";
            GUI.enabled = false;
        }
        else
        {
            buttonText = "Choose audio.wav Folder";
        }

        if (GUILayout.Button(buttonText, GUILayout.Height(50)))
        {
            genLipsync.Generate();
        }

        //Create a foldout menu for debug purposes
        GUI.enabled = true;
        EditorGUI.indentLevel++;
        debug = EditorGUILayout.Foldout(debug, "Debug");
        if(debug)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("nameList"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dirList"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clipList"));
        }
    }
}
