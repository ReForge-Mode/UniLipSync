using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PhonemeData))]
public class PhonemeDataEditor : PropertyDrawer
{
    //Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Using BeginProperty / EndProperty on the parent property means that
        //prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        //Calculate rects
        var aRect = new Rect(position.x                           , position.y, (position.width / 5    ) - 3, position.height);
        var bRect = new Rect(position.x + (position.width     / 5), position.y, (position.width * 2 / 5) - 3, position.height);
        var cRect = new Rect(position.x + (position.width * 3 / 5), position.y, (position.width * 2 / 5) - 3, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(aRect, property.FindPropertyRelative("phoneme"), GUIContent.none);
        EditorGUI.PropertyField(bRect, property.FindPropertyRelative("start"), GUIContent.none);
        EditorGUI.PropertyField(cRect, property.FindPropertyRelative("end"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
