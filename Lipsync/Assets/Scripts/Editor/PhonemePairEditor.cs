using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PhonemePair))]
public class PhonemePairEditor : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Calculate rects
        var unitRect = new Rect(position.x, position.y, position.width/2, position.height);
        var nameRect = new Rect(position.x + position.width/2, position.y, position.width/2, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("ipa"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("group"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}