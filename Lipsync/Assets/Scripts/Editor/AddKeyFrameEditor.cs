using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AddKeyFrame))]
public class AddKeyFrameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AddKeyFrame addKeyframe = (AddKeyFrame)target;
        if (GUILayout.Button("Create Animation", GUILayout.Height(50)))
        {
            addKeyframe.CreateAnimation();
        }

        DrawDefaultInspector();
    }
}
