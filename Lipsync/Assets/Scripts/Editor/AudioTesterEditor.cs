using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioTester))]
public class AudioTesterEditor : Editor
{
    private AudioTester script;

    private SerializedProperty animator;
    private SerializedProperty audioClip;

    private void OnEnable()
    {
        script = (AudioTester)target;
        animator = serializedObject.FindProperty("animator");
        audioClip = serializedObject.FindProperty("audioClip");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(animator);
        EditorGUILayout.PropertyField(audioClip);

        if (!EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("To test the lip sync animation, please enter Play Mode", MessageType.Info);
        }
        else if (audioClip.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Please fill in audioClip with the lip sync audio file", MessageType.Error);
        }
        else
        {
            if (GUILayout.Button("Play Lipsync", GUILayout.Height(50)))
            {
                script.PlaySoundWithLipSync();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
