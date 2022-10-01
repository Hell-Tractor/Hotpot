using System;
using UnityEditor;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute {
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class WritableIfDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndDisabledGroup();
    }
}